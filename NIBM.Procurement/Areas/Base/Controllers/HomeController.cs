using Microsoft.EntityFrameworkCore;
using NIBM.Procurement.Areas.Base.Models;
using NIBM.Procurement.Areas.Procurement.Models;
using NIBM.Procurement.Common;
using NIBM.Procurement;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IdentityModel.Claims;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace NIBM.Procurement.Areas.Base.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {
            return RedirectToAction("Home", "DashBoard");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignIn()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated && Session[sskCurUsrID] != null)
            { return RedirectToAction("Home", "Dashboard"); }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn([Bind(Include = "UserName,PassWord,RememberMe")] SignInVM signInVM, string ReturnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                { return View(signInVM); }

                if (signInVM.UserName != null && signInVM.PassWord != null)
                {
                    var lst = db.Users.Where(x => x.UserName.ToLower() == signInVM.UserName.ToLower()).ToList();
                    var obj = lst.Where(x => x.Password.Decrypt() == signInVM.PassWord).FirstOrDefault();

                    if (obj == null)
                    {
                        AddAlert(AlertStyles.danger, "The user name or password provided is incorrect.");
                        return View(signInVM);
                    }
                    if (obj.Status == ActiveState.Inactive)
                    {
                        AddAlert(AlertStyles.warning, "The user \"" + obj.UserName + "\" is inactive. Please contact IT Administrator.");
                        return View(signInVM);
                    }

                    var jser = new JavaScriptSerializer();
                    var lstRoles = db.UserRoles.Include(x => x.Role).Where(x => x.UserID == obj.UserID).Select(x => x.Role.Code).ToList();

                    var authTicket = new FormsAuthenticationTicket(
                        1,
                        obj.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(20),
                        signInVM.RememberMe,
                        jser.Serialize(new { userName = obj.UserName, roles = lstRoles }));

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    Session[sskCurUsrID] = obj.UserID;
                    Session[sskCurUsrRoles] = string.Join(",", lstRoles);
                    AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;

                    if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                        && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                    { return Redirect(ReturnUrl); }
                    else
                    { return RedirectToAction("Home", "Dashboard"); }
                }
                else
                {
                    AddAlert(AlertStyles.danger, "The user name or password cannot be empty.");
                    return View(signInVM);
                }
            }
            catch (Exception)
            {
                AddAlert(AlertStyles.danger, "Error while communicating with the system. Please contact the Administrator.");
                throw;
            }
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session[sskCurUsrID] = null;
            return RedirectToAction("SignIn", "Home");
        }

        public ActionResult Error(int? id)
        {
            return View("~/Areas/Base/Views/Home/Error.cshtml", id);
        }

        public ActionResult ChangePassword()
        {
            var user = db.Users.Find(CurUserID);
            if (user == null)
            {
                return HttpNotFound();
            }

            return PartialView("_ChangePassword", new SignInVM(user));
        }

        [HttpPost]
        public ActionResult ChangePassword([Bind(Include = "UserID,PassWord,NewPassword,ConfirmPassword")] SignInVM signInVM)
        {
            var objUser = db.Users.Find(signInVM.UserID);

            if (objUser.Password.Decrypt() != signInVM.PassWord)
            { ModelState.AddModelError("Password", "Incorrect password."); }
            else if (signInVM.PassWord == signInVM.NewPassword)
            { ModelState.AddModelError("NewPassword", "New password is same as the current password."); }
            else if (signInVM.ConfirmPassword != signInVM.NewPassword)
            { ModelState.AddModelError("ConfirmPassword", "Confirm password should be equal to new password."); }

            if (ModelState.IsValid)
            {
                objUser.Password = signInVM.NewPassword.Encrypt();
                objUser.ModifiedBy = this.GetCurrUser();
                objUser.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return Json(new { success = true });
            }

            return PartialView("_ChangePassword");
        }


        public ActionResult GetProcurementProcess()
        {
            var CurEmpId = db.Users.Find(CurUserID).EmployeeID;
            bool roleProcurement = db.UserRoles.Where(y => y.Role.Code == "ProcurementSupervisor").Count() > 0 ? true : false;

            //----------------- Procurement Request Pending Approvals ------------------- 
            var procurementReqPendingApp = db.ProcuremenetRequests.AsQueryable().Where(x =>
            CurEmpId.HasValue && (x.Status == ProcurementReqStatus.PendingApproval || x.Status == ProcurementReqStatus.HRRecommended || x.Status == ProcurementReqStatus.ProcurementDeptReceived) && (AdminRole || roleProcurement)).Count();

            //----------------- Procurement New Requests ------------------- 

            var procurementNewReq = db.ProcuremenetRequests.AsQueryable().Where(x =>
            CurEmpId.HasValue && (x.Status == ProcurementReqStatus.DivisionHeadApproved) && (AdminRole || roleProcurement))
            .Count();

            //----------------- Procurement Aprroved and Pending Completion Requests ------------------- 

            var ApprovedAndPendingCompletion = db.ProcuremenetRequests.AsQueryable().Where(x =>
            CurEmpId.HasValue && (x.Status == ProcurementReqStatus.DGApproved || x.Status == ProcurementReqStatus.HRApproved) && (AdminRole || roleProcurement))
            .Count();

            //----------------- Procurement Completed Requests ------------------- 

            var ProcCompletedReq = db.ProcuremenetRequests.AsQueryable().Where(x =>
            CurEmpId.HasValue && (x.Status == ProcurementReqStatus.ItemReceived) && (AdminRole || roleProcurement))
            .Count();

            var total = procurementReqPendingApp + procurementNewReq + ApprovedAndPendingCompletion + ProcCompletedReq;

            return Json(new { count = total }, JsonRequestBehavior.AllowGet);

        }
    }
}