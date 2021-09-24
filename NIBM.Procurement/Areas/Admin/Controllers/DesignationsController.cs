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
    public class DesignationsController : BaseController
    {
        public ActionResult Index(BaseViewModel<DesignationVM> vm)
        {
            vm.SetList(db.Designations.AsQueryable(), "Description");
            return View(vm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(new DesignationVM(designation));
        }

        public ActionResult Create()
        {
            var designation = new DesignationVM();
            return View(designation);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "DesignationID,Description")] DesignationVM designation)
        {
            try
            {
                var existingDesignation = db.Designations.Where(e => e.Description == designation.Description).FirstOrDefault();

                if (existingDesignation != null)
                { ModelState.AddModelError("Description", "Designation Already Exist"); }

                if (ModelState.IsValid)
                {
                    designation.CreatedBy = this.GetCurrUser();
                    designation.CreatedDate = DateTime.Now;
                    db.Designations.Add(designation.GetEntity());
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Designation created successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(designation);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(new DesignationVM(designation));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "DesignationID,Description,RowVersion")] DesignationVM designation)
        {
            byte[] curRowVersion = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = db.Designations.Find(designation.DesignationID);
                    if (obj == null)
                    { throw new DbUpdateConcurrencyException(); }

                    curRowVersion = obj.RowVersion;
                    var modObj = designation.GetEntity();
                    modObj.CopyContent(obj, "Description");

                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;

                    db.Entry(obj).OriginalValues["RowVersion"] = designation.RowVersion;
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Designation modified successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex);
                designation.RowVersion = curRowVersion;
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(designation);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(DesignationVM designation)
        {
            try
            {
                var obj = db.Designations.Find(designation.DesignationID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }
                db.Detach(obj);

                db.Entry(designation.GetEntity()).State = EntityState.Deleted;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Designation deleted successfully.");
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
            return RedirectToAction("Details", new { id = designation.DesignationID });
        }
    }
}