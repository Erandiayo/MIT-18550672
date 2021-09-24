using Microsoft.EntityFrameworkCore;
using NIBM.Procurement.Areas.Admin.Models;
using NIBM.Procurement.Areas.Base.Controllers;
using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NIBM.Procurement.Areas.Admin.Controllers
{
    [ExtendedAuthorize(Roles = UserRoles.AdminUser)]
    public class UsersController : BaseController
    {
        public ActionResult Index(BaseViewModel<UserVM> vm)
        {
            vm.SetList(db.Users.AsQueryable(), "UserName");
            return View(vm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new UserVM(user));
        }

        public ActionResult ChildDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVM obj = Session.GetObject<UserVM>();
            UserRoleVM userRole = obj.DetailsList.Where(x => x.UserRoleID == id.Value).FirstOrDefault();
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ChildDetails", userRole);
        }

        public ActionResult Create()
        {
            var user = new UserVM() { Status = ActiveState.Active };
            Session.SetObject(user);
            return View(user);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "UserID,UserName,Password,EmployeeID,Status,DetailsList,BranchID,DepartmentID")] UserVM user)
        {
            try
            {
                var obj = Session.GetObject<UserVM>();
                var existingUser = 0;

                if (user.EmployeeID != null)
                    existingUser = db.Users.Where(e => e.EmployeeID == user.EmployeeID).Count();

                var  exUserName= db.Users.Where(e => e.UserName == user.UserName).FirstOrDefault();

                if (user.Password == null)
                { ModelState.AddModelError("Password", "Password is required."); }
                if (existingUser >0)
                { ModelState.AddModelError("EmployeeID", "User Account Already Exists for the selected Employee."); }
                else if (exUserName !=null)
                { ModelState.AddModelError("UserName", "User Name Already Exists."); }

                if (ModelState.IsValid)
                {
                    user.CreatedBy = this.GetCurrUser();
                    user.CreatedDate = DateTime.Now;
                    user.DetailsList = obj.DetailsList;
                    var newObj = db.Users.Add(user.GetEntity()).Entity;
                    
                    foreach (var det in user.DetailsList)
                    {
                        det.Role = null;
                        newObj.UserRoles.Add(det.GetEntity());
                    }
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "User created successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(user);
        }

        public ActionResult ChildCreate(int? userID)
        {
            if (userID != 0)
            {
                if (userID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(userID);
                if (user == null)
                {
                    return HttpNotFound();
                }
            }

            var obj = Session.GetObject<UserVM>();
            ViewBag.UserRoles = obj.DetailsList.Select(x => x.RoleID).ToList();

            var userRole = new UserRoleVM() { UserID = userID.Value };
            return PartialView("_ChildCreate", userRole);
        }

        [HttpPost]
        public ActionResult ChildCreate([Bind(Include = "UserRoleID,UserID,RoleID,RoleName")] UserRoleVM userRole)
        {
            UserVM obj;
            try
            {
                if (ModelState.IsValid)
                {
                    obj = Session.GetObject<UserVM>();
                    userRole.LoadReference(x => x.Role);
                    userRole.UserRoleID = Math.Min(obj.DetailsList.Select(x => x.UserRoleID).MinOrDefault(), 0) - 1;
                    obj.DetailsList.Add(userRole);
                    Session.SetObject(obj);

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "User Role added successfully.");
                    string url = Url.Action("ChildIndex", new { id = userRole.UserID, isToEdit = true });
                    return Json(new { success = true, url = url });
                }
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            obj = Session.GetObject<UserVM>();
            ViewBag.UserRoles = obj.DetailsList.Select(x => x.RoleID).ToList();

            return PartialView("_ChildCreate", userRole);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var obj = new UserVM(user);
            Session.SetObject(obj);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Password,EmployeeID,Status,RowVersion,BranchID,DepartmentID")] UserVM user)
        {
            byte[] curRowVersion = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var sUser = Session.GetObject<UserVM>();

                    var obj = db.Users.Find(user.UserID);
                    if (obj == null)
                    { throw new DbUpdateConcurrencyException(); }

                    curRowVersion = obj.RowVersion;
                    var modObj = user.GetEntity();
                    var props = "EmployeeID,Status,BranchID,DepartmentID";
                    if (!user.Password.IsBlank())
                    { props += ",Password"; }
                    modObj.CopyContent(obj, props);

                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;

                    db.Entry(obj).OriginalValues["RowVersion"] = obj.RowVersion;

                    db.UserRoles.RemoveRange(obj.UserRoles.Where(x =>
                        !sUser.DetailsList.Select(y => y.UserRoleID).ToList().Contains(x.UserRoleID)));

                    foreach (var det in sUser.DetailsList)
                    {
                        var objDet = db.UserRoles.Find(det.UserRoleID);
                        if (objDet == null)
                        {
                            det.Role = null;
                            db.UserRoles.Add(det.GetEntity());
                        }
                    }
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "User modified successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex);
                user.RowVersion = curRowVersion;
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(user);
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(UserVM user)
        {
            try
            {
                var obj = db.Users.Find(user.UserID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }
              
                db.Detach(obj);

                var entry = db.Entry(user.GetEntity());
                entry.State = EntityState.Unchanged;
                entry.Collection(x => x.UserRoles).Load();
                db.UserRoles.RemoveRange(entry.Entity.UserRoles);
                entry.State = EntityState.Deleted;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "User deleted successfully.");
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("Index"); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("Details", new { id = user.UserID });
        }

        [HttpPost, ActionName("ChildDelete")]
        public ActionResult ChildDeleteConfirmed(int id)
        {
            string msg = string.Empty;
            try
            {
                var objSession = Session.GetObject<UserVM>();
                var lst = objSession.DetailsList;
                var obj = lst.FirstOrDefault(x => x.UserRoleID == id);
                lst.Remove(obj);
                Session.SetObject(objSession);

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "User Role removed successfully.");
            }
            catch (Exception ex)
            {
                msg = ex.GetInnerException().Message;
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, msg);
            }
            string url = "";
            if (msg.IsBlank())
            { url = Url.Action("ChildIndex", new { isToEdit = true }); }
            return Json(new { success = true, url = url, msg });
        }

        public ActionResult ChildIndex(int? id, bool isToEdit = false)
        {
            UserVM obj;

            if (isToEdit && Session[sskCrtdObj] is UserVM)
            { obj = Session.GetObject<UserVM>(); }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
                if (user == null)
                {
                    return HttpNotFound();
                }
                obj = new UserVM(user);
            }

            ViewBag.IsToEdit = isToEdit;
            ViewBag.UserID = obj.UserID;
            return PartialView("_ChildIndex", obj.DetailsList.OrderBy(x => x.RoleName));
        }

        public ActionResult EncryptUserPasswords()
        {
            try
            {
                foreach (var usr in db.Users)
                {
                    var pw = usr.Password.Decrypt();
                    if (pw == usr.Password)
                    {
                        usr.Password = usr.Password.Encrypt();
                    }
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
    }
}
