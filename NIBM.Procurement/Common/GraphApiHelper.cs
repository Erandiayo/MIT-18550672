using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace NIBM.Procurement.Common
{
    public class GraphApiHelper
    {
        private static readonly string ServiceClientId = ConfigurationManager.AppSettings["SSO_ClientId"];
        private const string ServiceClientSecret = "._3aU65~Wr3iCG.pN~AnK4lNc_qr.36aWS";
        private static readonly string ServiceTenantId = ConfigurationManager.AppSettings["SSO_TenantId"];
        private const string UserEmail = "erandi@nibm.lk";


        private static Task<GraphServiceClient> GetGraphClientAsync()
        {
            GraphApiAccess.InitAccess(ServiceClientId, ServiceClientSecret, ServiceTenantId);
            return GraphApiAccess.GetGraphClientAsync();
        }

        public static string GetProcurementAttachment(int ProReqID, string uniqueFileName, string folderPath)
        {
            string path = null;

            Task.Run(async () =>
            {
                path = await GetProcurementachmentAsync(ProReqID, uniqueFileName, folderPath);
            }).GetAwaiter().GetResult();

            return path;
        }

        public static async Task<string> GetProcurementachmentAsync(int ProReqID, string uniqueFileName, string folderPath)
        {
            await GetGraphClientAsync();

            try
            {
                var item = await AzureActiveDirectoryTokenFormat.Instance.graphServiceClient.Users[UserEmail].Drive
                            .Root
                            .ItemWithPath($"{folderPath}/{ProReqID}-{uniqueFileName}")
                            .Request()
                            .GetAsync();

                var permission = await AzureActiveDirectoryTokenFormat.Instance.graphServiceClient.Users[UserEmail].Drive
                    .Items[item.Id].CreateLink("edit", "anonymous").Request().PostAsync();

                return permission.Link.WebUrl;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public static void SaveProcurementAttachment(byte[] binData, int ProReqID, string uniqueFileName, string folderPath)
        {
            Task.Run(async () =>
            {
                await SaveProcurementAttachmentAsync(binData, ProReqID, uniqueFileName, folderPath);
            }).GetAwaiter().GetResult();
        }

        public static async Task SaveProcurementAttachmentAsync(byte[] binData, int ProReqID, string uniqueFileName, string folderPath)
        {
            await GetGraphClientAsync();

            using (var inStream = new MemoryStream(binData))
            {
                var uploadedItem = await AzureActiveDirectoryTokenFormat.Instance.graphServiceClient.Users[UserEmail].Drive
                                        .Root
                                        .ItemWithPath($"{folderPath}/{ProReqID}-{uniqueFileName}")
                                        .Content
                                        .Request()
                                        .PutAsync<DriveItem>(inStream);
            }
        }

    }
}