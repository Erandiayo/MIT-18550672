using NIBM.Procurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NIBM.Procurement.Common
{
    public class AllowCrossSiteAttribute : ActionFilterAttribute
    {
        string[] Origins;
        public AllowCrossSiteAttribute(params string[] origins)
        {
            Origins = origins;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isLocal = filterContext.HttpContext.Request.IsLocal;
            var originHeader = filterContext.HttpContext.Request.Headers.Get("Origin");
            var response = filterContext.HttpContext.Response;

            if (!string.IsNullOrWhiteSpace(originHeader) &&
                (isLocal || Origins.Where(Origin => originHeader.StartsWith(Origin.Replace("/*", ""))).Count() > 0))
            {
                response.AddHeader("Access-Control-Allow-Origin", originHeader);
                response.AddHeader("Access-Control-Allow-Headers", "*");
                response.AddHeader("Access-Control-Allow-Credentials", "true");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}