using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WebForms;
using NIBM.Procurement.Areas.Base.Controllers;
using NIBM.Procurement.Areas.Base.Models;
using NIBM.Procurement.Common;
using NIBM.Procurement;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NIBM.Procurement.Areas.Procurement.Models;

namespace NIBM.Procurement.Areas.Procurement.Controllers
{
    public class VendorController : BaseController
    {
        // GET: Procurement/Vendor
        public ActionResult Index(BaseViewModel<VendorVM> vm)
        {
            vm.SetList(db.Vendors.AsQueryable(), "Name");
            return View(vm);
        }

        public ActionResult Create()
        {
            var vendor = new VendorVM();
            vendor.Status = ActiveState.Active;
            return View(vendor);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "VendorID,NavVendorID,Name,Address,TelNo,FaxNo,Status,RowVersion")] VendorVM vendorVM)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        vendorVM.CreatedBy = this.GetCurrUser();
                        vendorVM.CreatedDate = DateTime.Now;
                        var objVendor = db.Vendors.Add(vendorVM.GetEntity()).Entity;
                        db.SaveChanges();

                        dbTransaction.Commit();
                        Session.Remove(sskCrtdObj);
                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Vendor created successfully.");
                        return RedirectToAction("Details", new { id = objVendor.VendorID });
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    this.ShowEntityErrors(dbEx);
                    dbTransaction.Rollback();
                }
                catch (Exception ex)
                {
                    AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
                    dbTransaction.Rollback();
                }
            }
            return View(vendorVM);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }

            var obj = new VendorVM(vendor);
            return View(obj);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            var obj = new VendorVM(vendor);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "VendorID,NavVendorID,Name,Address,TelNo,FaxNo,Status,RowVersion")] VendorVM vendorVM)
        {
            byte[] curRowVersion = null;
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var obj = db.Vendors.Find(vendorVM.VendorID);
                        if (obj == null)
                        { throw new DbUpdateConcurrencyException(); }

                        curRowVersion = obj.RowVersion;

                        obj.ModifiedBy = this.GetCurrUser();
                        obj.ModifiedDate = DateTime.Now;
                        var modObj = vendorVM.GetEntity();
                        modObj.CopyContent(obj, "NavVendorID,Name,Address,TelNo,FaxNo,Status");

                        db.SaveChanges();
                        dbTransaction.Commit();
                        AddAlert(AlertStyles.success, "Vendor modified successfully.");
                        return RedirectToAction("Details", new { id = obj.VendorID });
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.ShowConcurrencyErrors(ex);
                    vendorVM.RowVersion = curRowVersion;
                    dbTransaction.Rollback();
                }
                catch (DbEntityValidationException dbEx)
                { this.ShowEntityErrors(dbEx); dbTransaction.Rollback(); }
                catch (Exception ex)
                { AddAlert(AlertStyles.danger, ex.GetInnerException().Message); dbTransaction.Rollback(); }
            }
            return View(vendorVM);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(VendorVM vendorVM)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var obj = db.Vendors.Find(vendorVM.VendorID);
                    if (obj == null)
                    { throw new DbUpdateConcurrencyException(""); }
                    db.Detach(obj);

                    var entry = db.Entry(vendorVM.GetEntity());
                    entry.State = EntityState.Deleted;
                    db.SaveChanges();
                    dbTransaction.Commit();
                    AddAlert(AlertStyles.success, "Vendor deleted successfully.");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.ShowConcurrencyErrors(ex, true);
                    if (ex.Message == "")
                    { return RedirectToAction("Index"); }
                    dbTransaction.Rollback();
                }
                catch (Exception ex)
                {
                    AddAlert(AlertStyles.danger, ex.GetInnerException().Message);
                    dbTransaction.Rollback();
                }
            }
            return RedirectToAction("Details", new { id = vendorVM.VendorID });
        }
    }
}