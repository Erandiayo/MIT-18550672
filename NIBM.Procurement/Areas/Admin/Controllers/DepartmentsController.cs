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
    public class DepartmentsController : BaseController
    {
        public ActionResult Index(BaseViewModel<DepartmentVM> vm)
        {
            vm.SetList(db.Departments.AsQueryable(), "Description");
            return View(vm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(new DepartmentVM(department));
        }

        public ActionResult Create()
        {
            var department = new DepartmentVM() { Status = ActiveState.Active };
            return View(department);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "DepartmentID,Description,IsAcademic,Status")] DepartmentVM department)
        {
            try
            {
                var existingDepartment = db.Departments.Where(e => e.Description == department.Description).FirstOrDefault();

                if (existingDepartment != null)
                { ModelState.AddModelError("Description", "Department Name Already Exist"); }

                if (ModelState.IsValid)
                {
                    department.CreatedBy = this.GetCurrUser();
                    department.CreatedDate = DateTime.Now;
                    db.Departments.Add(department.GetEntity());
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Department created successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(department);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(new DepartmentVM(department));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "DepartmentID,Description,IsAcademic,Status,RowVersion")] DepartmentVM department)
        {
            byte[] curRowVersion = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = db.Departments.Find(department.DepartmentID);
                    if (obj == null)
                    { throw new DbUpdateConcurrencyException(); }

                    curRowVersion = obj.RowVersion;
                    var modObj = department.GetEntity();
                    modObj.CopyContent(obj, "Description,IsAcademic,Status");

                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;

                    db.Entry(obj).OriginalValues["RowVersion"] = department.RowVersion;
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Department modified successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex);
                department.RowVersion = curRowVersion;
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(DepartmentVM department)
        {
            try
            {
                var obj = db.Departments.Find(department.DepartmentID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }
                db.Detach(obj);

                db.Entry(department.GetEntity()).State = EntityState.Deleted;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Department deleted successfully.");
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
            return RedirectToAction("Details", new { id = department.DepartmentID });
        }
    }
}