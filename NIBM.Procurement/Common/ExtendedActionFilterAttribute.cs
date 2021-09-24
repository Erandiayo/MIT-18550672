using NIBM.Procurement.Areas.Base.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace NIBM.Procurement.Common
{
    public class ExtendedActionFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["DisableSSO"].ConvertTo<bool>())
            {
                var cas = filterContext.ActionDescriptor.GetCustomAttributes(true);

                if (cas.Where(x => x is AllowAnonymousAttribute).Count() == 0 && !filterContext.IsChildAction)
                {
                    if (HttpContext.Current.Session == null || HttpContext.Current.Session[BaseController.sskCurUsrID] == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                        { "action", "SignIn" },
                        { "controller", "Home" },
                        { "area", "Base" },
                        { "ReturnUrl", filterContext.GetLocalUrl() }
                    });
                        return;
                    }
                }
                base.OnActionExecuting(filterContext);
                return;
            }

            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var email = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase))?.Value;

            if (email != null)
                AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;
            else
                AntiForgeryConfig.UniqueClaimTypeIdentifier = string.Empty;

            if (filterContext.HttpContext.Request.IsAuthenticated && HttpContext.Current.Session?[BaseController.sskCurUsrID] == null)
            {
                if (!string.IsNullOrEmpty(email))
                {
                    if (filterContext.Controller is HomeController && filterContext.ActionDescriptor.ActionName.In("Index", "SignIn"))
                    {
                        base.OnActionExecuting(filterContext);
                        return;
                    }

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "action", "Index" },
                            { "controller", "Home" },
                            { "area", "Base" },
                            { "ReturnUrl", filterContext.GetLocalUrl() }
                        });
                    return;
                }
                else
                {
                    var cas = filterContext.ActionDescriptor.GetCustomAttributes(true);

                    if (cas.Where(x => x is AllowAnonymousAttribute).Count() == 0 && !filterContext.IsChildAction)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                                { "action", "SignIn" },
                                { "controller", "Home" },
                                { "area", "Base" },
                                { "ReturnUrl", filterContext.GetLocalUrl() }
                            });
                        return;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                HttpStatusCodeResult httpStatusCodeResult = filterContext.Result as HttpStatusCodeResult;
                int statusCode;
                if (httpStatusCodeResult != null && (statusCode = httpStatusCodeResult.StatusCode).In(404, 400))
                {
                    HttpContext.Current.Server.TransferRequest("~/Base/Home/Error/" + statusCode, true);
                    filterContext.ExceptionHandled = true;
                }
            }
            base.OnActionExecuted(filterContext);
        }

        public void OnException(ExceptionContext filterContext)
        {
            HttpException httpException = filterContext.Exception as HttpException;
            int statusCode;
            if (httpException != null && (statusCode = httpException.GetHttpCode()).In(404, 400))
            {
                HttpContext.Current.Server.TransferRequest("~/Base/Home/Error/" + statusCode, true);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}