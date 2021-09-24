using Microsoft.EntityFrameworkCore;
using NIBM.Procurement.Areas.Admin.Models;
using NIBM.Procurement.Areas.Base.Controllers;
using NIBM.Procurement.Common;
using NIBM.Procurement;
using NIBM.Procurement.DB.Entities;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NIBM.Procurement.Areas.Admin.Controllers
{
    public class SubDepartmentController : BaseController
    {
        public ActionResult Index(BaseViewModel<SubDepartmentVM> vm)
        {
            vm.SetList(db.SubDepartments.AsQueryable(), "Description");
            return View(vm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDepartment subDept = db.SubDepartments.Find(id);
            if (subDept == null)
            {
                return HttpNotFound();
            }
            return View(new SubDepartmentVM(subDept));
        }

        public ActionResult Create()
        {
            var subDept = new SubDepartmentVM() { Status = ActiveState.Active };
            return View(subDept);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "SubDeptID,Description,DeptID,Status,DepartmentDesc")] SubDepartmentVM subDept)
        {
            try
            {
                if (subDept.Description == null)
                { ModelState.AddModelError("Description", "Sub Department cannot be null"); }
                if (subDept.DeptID == 0)
                { ModelState.AddModelError("DeptID", "Department should be selected"); }
                var existSubDepDep = db.SubDepartments.Where(x => x.SubDeptID != subDept.SubDeptID && x.Description == subDept.Description && x.DeptID == subDept.DeptID).Count();
                if (existSubDepDep > 0)
                { ModelState.AddModelError("DeptID", "Sub Department already exist for the same Department"); }

                if (ModelState.IsValid)
                {
                    subDept.CreatedBy = this.GetCurrUser();
                    subDept.CreatedDate = DateTime.Now;
                    db.SubDepartments.Add(subDept.GetEntity());
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Sub Department created successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(subDept);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDepartment subDept = db.SubDepartments.Find(id);
            if (subDept == null)
            {
                return HttpNotFound();
            }
            return View(new SubDepartmentVM(subDept));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "SubDeptID,Description,DeptID,Status,DepartmentDesc,RowVersion")] SubDepartmentVM subDept)
        {
            byte[] curRowVersion = null;
            try
            {
                var existSubDepDep = db.SubDepartments.Where(x => x.SubDeptID != subDept.SubDeptID && x.Description == subDept.Description && x.DeptID == subDept.DeptID).Count();
                if (existSubDepDep > 0)
                { ModelState.AddModelError("DeptID", "Sub Department already exist for the same Department"); }

                if (ModelState.IsValid)
                {
                    var obj = db.SubDepartments.Find(subDept.SubDeptID);
                    if (obj == null)
                    { throw new DbUpdateConcurrencyException(); }

                    curRowVersion = obj.RowVersion;
                    var modObj = subDept.GetEntity();
                    modObj.CopyContent(obj, "Description,DeptID,Status");

                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;

                    db.Entry(obj).OriginalValues["RowVersion"] = subDept.RowVersion;
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Sub Department modified successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex);
                subDept.RowVersion = curRowVersion;
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(subDept);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(SubDepartmentVM subDept)
        {
            try
            {
                var obj = db.SubDepartments.Find(subDept.SubDeptID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }
                db.Detach(obj);

                db.Entry(subDept.GetEntity()).State = EntityState.Deleted;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Sub Department deleted successfully.");
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
            return RedirectToAction("Details", new { id = subDept.SubDeptID });
        }
    }
}