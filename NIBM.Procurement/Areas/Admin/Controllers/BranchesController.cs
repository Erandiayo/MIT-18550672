using Microsoft.EntityFrameworkCore;
using NIBM.Procurement.Areas.Admin.Models;
using NIBM.Procurement.Areas.Base.Controllers;
using NIBM.Procurement.Common;
using NIBM.Procurement;
using NIBM.Procurement.DB.Entities;
using System;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace NIBM.Procurement.Areas.Admin.Controllers
{
    public class BranchesController : BaseController
    {
        public ActionResult Index(BaseViewModel<BranchVM> vm)
        {
            vm.SetList(db.Branches.AsQueryable(), "Description");
            return View(vm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(new BranchVM(branch));
        }

        public ActionResult Create()
        {
            var branch = new BranchVM() { Status = ActiveState.Active };
            return View(branch);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "BranchID,Description,Address,TelNo_1,TelNo_2,FaxNo_1,FaxNo_2,Status")] BranchVM branch)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {

                    var existingBranch = db.Branches.Where(e=>e.Description==branch.Description).FirstOrDefault();
                    if (existingBranch != null)
                    { ModelState.AddModelError("Description", "Branch Already Exist"); }

                    if (ModelState.IsValid)
                    {
                        branch.CreatedBy = this.GetCurrUser();
                        branch.CreatedDate = DateTime.Now;
                        db.Branches.Add(branch.GetEntity());
                        db.SaveChanges();

                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Branch created successfully.");
                        return RedirectToAction("Index");
                    }
                }
                catch (DbEntityValidationException dbEx)
                { this.ShowEntityErrors(dbEx); }
                catch (Exception ex)
                { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }
            }
            return View(branch);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(new BranchVM(branch));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "BranchID,Description,Address,TelNo_1,TelNo_2,FaxNo_1,FaxNo_2,Status,RowVersion")] BranchVM branch)
        {
            byte[] curRowVersion = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = db.Branches.Find(branch.BranchID);
                    if (obj == null)
                    { throw new DbUpdateConcurrencyException(); }

                    curRowVersion = obj.RowVersion;
                    var modObj = branch.GetEntity();
                    modObj.CopyContent(obj, "Description,Address,TelNo_1,TelNo_2,FaxNo_1,FaxNo_2,Status");

                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;

                    db.Entry(obj).OriginalValues["RowVersion"] = branch.RowVersion;
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Branch modified successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex);
                branch.RowVersion = curRowVersion;
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(BranchVM branch)
        {
            try
            {
                var obj = db.Branches.Find(branch.BranchID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }
                db.Detach(obj);

                db.Entry(branch.GetEntity()).State = EntityState.Deleted;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Branch deleted successfully.");
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
            return RedirectToAction("Details", new { id = branch.BranchID });
        }
    }
}
