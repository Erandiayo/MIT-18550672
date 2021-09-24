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
    public class BranchDepartmentsController : BaseController
    {
        public ActionResult Index(BaseViewModel<BranchDepartmentVM> vm)
        {
            vm.SetList(db.BranchDepartments.AsQueryable(), "BranchDesc");
            return View(vm);
        }

        public ActionResult Details(int? branchId, int? departmentId)
        {
            if (branchId == null || departmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchDepartment branchDepartment = db.BranchDepartments.Find(branchId, departmentId);
            if (branchDepartment == null)
            {
                return HttpNotFound();
            }
            return View(new BranchDepartmentVM(branchDepartment));
        }

        public ActionResult Create()
        {
            var branchDepartment = new BranchDepartmentVM() { Status = ActiveState.Active };
            return View(branchDepartment);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "BranchID,DepartmentID,TelNo_1,TelNo_2,FaxNo_1,FaxNo_2,Status")] BranchDepartmentVM branchDepartment)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    branchDepartment.CreatedBy = this.GetCurrUser();
                    branchDepartment.CreatedDate = DateTime.Now;
                    db.BranchDepartments.Add(branchDepartment.GetEntity());
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Branch Department created successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(branchDepartment);
        }

        public ActionResult Edit(int? branchId, int? departmentId)
        {
            if (branchId == null || departmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchDepartment branchDepartment = db.BranchDepartments.Find(branchId, departmentId);
            if (branchDepartment == null)
            {
                return HttpNotFound();
            }
            return View(new BranchDepartmentVM(branchDepartment));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "BranchID,DepartmentID,TelNo_1,TelNo_2,FaxNo_1,FaxNo_2,Status,RowVersion")] BranchDepartmentVM branchDepartment)
        {
            byte[] curRowVersion = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = db.BranchDepartments.Find(branchDepartment.BranchID, branchDepartment.DepartmentID);
                    if (obj == null)
                    { throw new DbUpdateConcurrencyException(); }

                    curRowVersion = obj.RowVersion;
                    var modObj = branchDepartment.GetEntity();
                    modObj.CopyContent(obj, "TelNo_1,TelNo_2,FaxNo_1,FaxNo_2,Status");

                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;

                    db.Entry(obj).OriginalValues["RowVersion"] = branchDepartment.RowVersion;
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Branch Department modified successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex);
                branchDepartment.RowVersion = curRowVersion;
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(branchDepartment);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(BranchDepartmentVM branchDepartment)
        {
            try
            {
                var obj = db.BranchDepartments.Find(branchDepartment.BranchID, branchDepartment.DepartmentID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }
                db.Detach(obj);

                db.Entry(branchDepartment.GetEntity()).State = EntityState.Deleted;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Branch Department deleted successfully.");
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
            return RedirectToAction("Details", new { branchID = branchDepartment.BranchID, departmentID = branchDepartment.DepartmentID });
        }
    }
}
