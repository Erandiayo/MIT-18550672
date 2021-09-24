using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NIBM.Procurement.Common
{
    public class GraphApiAccess
    {
        public static string ServiceClientId;
        public static string ServiceClientSecret;
        public static string ServiceTenantId;

        private static string accessToken = null;
        private static DateTime? accessTokenExpiresAt = null;
        private static GraphServiceClient graphClient = null;

        public static void InitAccess(string ServiceClientId, string ServiceClientSecret, string ServiceTenantId)
        {
            GraphApiAccess.ServiceClientId = ServiceClientId;
            GraphApiAccess.ServiceClientSecret = ServiceClientSecret;
            GraphApiAccess.ServiceTenantId = ServiceTenantId;
        }

        public static async Task<GraphServiceClient> GetGraphClientAsync()
        {
            if (graphClient == null || accessTokenExpiresAt < DateTime.Now)
            {
                var token = await GetAccessTokenAsync();

                graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                   async (requestMessage) =>
                   {
                       await Task.Run(() =>
                       {
                           requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                       });
                   }));
            }

            AzureActiveDirectoryTokenFormat.Instance.graphServiceClient = graphClient;
            return graphClient;
        }

        private static async Task<string> GetAccessTokenAsync()
        {
            if (accessToken != null && accessTokenExpiresAt > DateTime.Now)
                return accessToken;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(
                                      "https://login.microsoftonline.com/{0}/oauth2/v2.0/token",
                                      ServiceTenantId));

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            string postData = @"grant_type=client_credentials";
            postData += "&scope=" + HttpUtility.UrlEncode("https://graph.microsoft.com/.default");
            postData += "&client_id=" + HttpUtility.UrlEncode(ServiceClientId);
            postData += "&client_secret=" + HttpUtility.UrlEncode(ServiceClientSecret);
            byte[] data = encoding.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AzureActiveDirectoryTokenFormat));
                    AzureActiveDirectoryTokenFormat tokenResp =
                        (AzureActiveDirectoryTokenFormat)(serializer.ReadObject(stream));

                    string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", tokenResp.token_type, " ",
                        tokenResp.access_token);

                    accessToken = tokenResp.access_token;
                    accessTokenExpiresAt = DateTime.Now.AddSeconds(tokenResp.expires_in);
                    AzureActiveDirectoryTokenFormat.Instance.access_token = tokenResp.access_token;
                }
            }
            return accessToken;
        }

    }

    [DataContract]
    public class AzureActiveDirectoryTokenFormat
    {
        [DataMember]
        internal string token_type { get; set; }
        [DataMember]
        internal string access_token { get; set; }
        [DataMember]
        internal int expires_in { get; set; }

        public IGraphServiceClient graphServiceClient { get; set; }

        public DriveItem folder = new DriveItem();

        public List<string> userEmails = new List<string>();

        public static AzureActiveDirectoryTokenFormat Instance { get; } = new AzureActiveDirectoryTokenFormat();

        public string CreateToken()
        {
            return Instance.access_token;
        }
    }
}
