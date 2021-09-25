using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WebForms;
using NIBM.Procurement.Areas.Base.Controllers;
using NIBM.Procurement.Areas.Base.Models;
using NIBM.Procurement.Areas.Procurement.Models;
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

namespace NIBM.Procurement.Areas.Procurement.Controllers
{
    public class TenderOpenController : BaseController
    {
        private const string ProcurementTECReportFolder = "ProcurementTECReport";
        public ActionResult Index(BaseViewModel<TenderVM> vm)
        {
            vm.SetList(db.Tenders.AsQueryable(), "TenderOpenedDate");
            return View(vm);

        }

        public ActionResult CreateTender(int? id, bool tile = false)
        {
            var objTender = new TenderVM();
            var procReq = db.ProcuremenetRequests.Find(id);
            objTender.ReqByName = procReq.Employee.Title.ToString() + ". " + procReq.Employee.Initials.Trim() + " " + procReq.Employee.LastName;
            objTender.DGInitials = db.Employees.Where(x => x.DesignationID == 4 && x.Status == ActiveState.Active).Select(x => x.Title.ToEnumChar("") + ". " + x.Initials.Trim() + " " + x.LastName).FirstOrDefault();
            objTender.ApprovedOrRejectByName = procReq.DivHead.Title.ToString() + ". " + procReq.DivHead.Initials.Trim() + " " + procReq.DivHead.LastName; 
            objTender.IsTile = tile;
            objTender.ProcReqId = procReq.ProReqID;
            objTender.DGAppORRejDate = procReq.DGAppORRejDate == null ? procReq.HRAppRecommendORRejDate : procReq.DGAppORRejDate;
            objTender.CompletedDate = procReq.CompletedDate != null ? procReq.CompletedDate.Value.Date : DateTime.Today;
            objTender.TenderOpenedDate = DateTime.Now;
            objTender.TenderClosedDate = DateTime.Today.AddMonths(1);
            objTender.ReqSubject = procReq.ReqSubject;
            objTender.ReqDate = procReq.ReqDate;
            objTender.Description = procReq.Description;
            objTender.AttachmentLink = procReq.AttachmentLink;
            objTender.Comments = procReq.Comments;
            objTender.DivHeadName= procReq.DivHead.Title.ToString() + ". " + procReq.DivHead.Initials.Trim() + " " + procReq.DivHead.LastName;
            Session.SetObject(objTender);
            return View(objTender);
        }

        [HttpPost]
        public ActionResult CreateTender([Bind(Include = "TenderID,ProcReqId,TenderOpenedDate,TenderClosedDate,ReqByName,ReqDate,CompletedDate,Comments,ReqSubject,Description,DivHeadName,UserFeedback,AttachmentLink,IsTile,RowVersion")] TenderVM tender)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var obj = Session.GetObject<TenderVM>();


                    if (tender.TenderOpenedDate == null)
                    { ModelState.AddModelError("TenderOpenedDate", "Tender open date is required."); }
                    if (tender.TenderClosedDate == null)
                    { ModelState.AddModelError("TenderClosedDate", "Tender closed date is required."); }
                    if(tender.TenderClosedDate < tender.TenderOpenedDate)
                    { ModelState.AddModelError("TenderClosedDate", "Tender closed date should be greater than the tender opened date."); }
                    
                    if (obj.TenderVendors.Count == 0)
                    { ModelState.AddModelError("", "Vendors connot be empty."); }
                  

                    if (ModelState.IsValid)
                    {
                        tender.CreatedBy = this.GetCurrUser();
                        tender.CreatedDate = DateTime.Now;                      
                        var objtender = db.Tenders.Add(tender.GetEntity()).Entity;
                        db.SaveChanges();

                        var procReq = db.ProcuremenetRequests.Find(tender.ProcReqId);
                        procReq.TenderID = objtender.TenderID;
                        procReq.ModifiedBy = this.GetCurrUser();
                        procReq.ModifiedDate = DateTime.Now;

                        foreach (var det in obj.TenderVendors)
                        {
                            det.TenderID = objtender.TenderID;
                            det.CreatedBy = this.GetCurrUser();
                            det.CreatedDate = DateTime.Now;
                            objtender.TenderVendors.Add(det.GetEntity());
                        }
                        db.SaveChanges();
                        dbTransaction.Commit();
                        Session.Remove(sskCrtdObj);
                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Tendor opened successfully.");
                        return RedirectToAction("Details", new { id = objtender.TenderID, IsTile = tender.IsTile });
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
            return View(tender);
        }

        public ActionResult Details(int? id, bool IsTile = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tender tenderdetails = db.Tenders.Find(id);
            if (tenderdetails == null)
            {
                return HttpNotFound();
            }

            var obj = new TenderVM(tenderdetails);
            obj.IsTile = IsTile;

            Session.SetObject(obj);
            return View(obj);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tender tenderdeails = db.Tenders.Find(id);
            if (tenderdeails == null)
            {
                return HttpNotFound();
            }
            var obj = new TenderVM(tenderdeails);
            obj.CompletedDate = obj.CompletedDate != null ? obj.CompletedDate.Value.Date : DateTime.Today;
            Session.SetObject(obj);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "TenderID,ProcReqId,TenderOpenedDate,TenderClosedDate,ReqByName,ReqDate,CompletedDate,Comments,ReqSubject,Description,DivHeadName,UserFeedback,AttachmentLink,RowVersion")] TenderVM tender)
        {
            byte[] curRowVersion = null;
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sessionTender = Session.GetObject<TenderVM>();

                    if (tender.TenderOpenedDate == null)
                    { ModelState.AddModelError("TenderOpenedDate", "Tender open date is required."); }
                    if (tender.TenderClosedDate == null)
                    { ModelState.AddModelError("TenderClosedDate", "Tender closed date is required."); }

                    if (ModelState.IsValid)
                    {
                        curRowVersion = sessionTender.RowVersion;
                        var obj = db.Tenders.Find(tender.TenderID);

                        obj.ModifiedBy = this.GetCurrUser();
                        obj.ModifiedDate = DateTime.Now;
                        var modObj = tender.GetEntity();
                        modObj.CopyContent(obj, "TenderID,ProcReqId,TenderOpenedDate,TenderClosedDate");

                        db.TenderVendors.RemoveRange(obj.TenderVendors.Where(x =>
                           !sessionTender.TenderVendors.Select(y => y.TenderVendorID).ToList().Contains(x.TenderVendorID)));

                        foreach (var det in sessionTender.TenderVendors)
                        {
                            var objTenVen = db.TenderVendors.Find(det.TenderVendorID);
                            if (objTenVen == null)
                            {
                                det.TenderID = obj.TenderID;
                                det.CreatedBy = this.GetCurrUser();
                                det.CreatedDate = DateTime.Now;
                                obj.TenderVendors.Add(det.GetEntity());
                            }
                            else
                            {
                                var modObjDet = det.GetEntity();
                                modObjDet.CopyContent(objTenVen, "VendorID");

                                objTenVen.ModifiedBy = this.GetCurrUser();
                                objTenVen.ModifiedDate = DateTime.Now;
                            }
                        }

                        db.SaveChanges();
                        dbTransaction.Commit();

                        AddAlert(AlertStyles.success, "Tender modified successfully.");
                        return RedirectToAction("Details", new { id = tender.TenderID});
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.ShowConcurrencyErrors(ex);
                    tender.RowVersion = curRowVersion;
                    dbTransaction.Rollback();
                }
                catch (DbEntityValidationException dbEx)
                { this.ShowEntityErrors(dbEx); dbTransaction.Rollback(); }
                catch (Exception ex)
                { AddAlert(AlertStyles.danger, ex.GetInnerException().Message); dbTransaction.Rollback(); }
            }
            return View(tender);
        }

        public ActionResult ChildIndex(int? id, bool isToEdit = false)
        {
            TenderVM obj;
            
            if (isToEdit && Session[sskCrtdObj] is TenderVM)
            {
                obj = (TenderVM)Session[sskCrtdObj];
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tender Tenderdetails = db.Tenders.Where(x => x.TenderID == id).FirstOrDefault();
                if (Tenderdetails == null)
                {
                    return HttpNotFound();
                }
                obj = new TenderVM(Tenderdetails);
            }

            ViewBag.IsToEdit = isToEdit;
            ViewBag.TenderId = obj.TenderID;
            return PartialView("_ChildIndex", obj.TenderVendors);
        }

        public ActionResult ChildCreate(int? TenderID)
        {
            if (TenderID != 0)
            {
                if (TenderID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tender tenderdetail = db.Tenders.Find(TenderID);
                if (tenderdetail == null)
                {
                    return HttpNotFound();
                }
            }
            var TenderDetails = new TenderVendorVM { TenderID = TenderID.Value };
            return PartialView("_ChildCreate", TenderDetails);
        }

        [HttpPost]
        public ActionResult ChildCreate([Bind(Include = "VendorID,TenderID")] TenderVendorVM tenderVendor)
        {
            TenderVM obj;
            try
            {
                obj = Session.GetObject<TenderVM>();
                if (tenderVendor.VendorID == 0)
                { ModelState.AddModelError("VendorID", "Vendor must be selected."); }

                if (ModelState.IsValid)
                {
                    tenderVendor.TenderVendorID = Math.Min(obj.TenderVendors.Select(x => x.TenderVendorID).MinOrDefault(), 0) - 1;

                    var vendor = db.Vendors.Find(tenderVendor.VendorID);
                    tenderVendor.Name = vendor.Name;
                    tenderVendor.Address = vendor.Address;
                    tenderVendor.TelNo = vendor.TelNo;

                    obj.TenderVendors.Add(tenderVendor);
                    Session.SetObject(obj);

                    AddAlert(AlertStyles.success, "Vendor added successfully.");
                    string url = Url.Action("ChildIndex", new { id = tenderVendor.TenderID, isToEdit = true });
                    return Json(new { success = true, url = url });
                }
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(AlertStyles.danger, ex.GetInnerException().Message); }

            return PartialView("_ChildCreate", tenderVendor);
        }


        [HttpPost, ActionName("ChildDelete")]
        public ActionResult ChildDeleteConfirmed(int id)
        {
            string msg = string.Empty;
            try
            {
                var objSession = Session.GetObject<TenderVM>();
                var lst = objSession.TenderVendors;
                var obj = lst.FirstOrDefault(x => x.TenderVendorID == id);
                lst.Remove(obj);
                Session.SetObject(objSession);

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Vednor successfully removed.");
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

        public ActionResult AppointTec(int? id, bool tile = false)
        {
            
            var procReq = db.ProcuremenetRequests.Find(id);
            if(procReq.Tender == null)
            {
                return HttpNotFound();
            }
            var objTender = new TenderVM(procReq.Tender);
            objTender.IsTile = tile;
            Session.SetObject(objTender);
            return View(objTender);
        }

        [HttpPost]
        public ActionResult AppointTec([Bind(Include = "TenderID,ReqByName,ReqDate,CompletedDate,Comments,ReqSubject,Description,DivHeadName,UserFeedback,TenderOpenedDate,TenderClosedDate,AttachmentLink,IsTile")] TenderVM tenderVM)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var obj = Session.GetObject<TenderVM>();
                    if (obj.TECMembers.Count == 0)
                    { ModelState.AddModelError("", "TEC Member must be selected."); }

                    if (ModelState.IsValid)
                    {
                        var objtender = db.Tenders.Find(tenderVM.TenderID);
                        objtender.ModifiedBy = this.GetCurrUser();
                        objtender.ModifiedDate = DateTime.Now;
                        objtender.TecTeamAssigedOn = DateTime.Now;
                        objtender.TenderStatus = TenderStatus.TECSelected;

                        db.SaveChanges();

                        foreach (var det in obj.TECMembers)
                        {
                            var objDet = db.TECMembers.Find(det.TECMemberID);
                            if (objDet == null)
                            {
                                det.ProcReqId = objtender.TenderID;
                                det.CreatedBy = this.GetCurrUser();
                                det.CreatedDate = DateTime.Now;
                                objtender.TECMembers.Add(det.GetEntity());
                            }
                            else
                            {
                                var modObjDet = det.GetEntity();
                                modObjDet.CopyContent(objDet, "EmployeeId");

                                objDet.ModifiedBy = this.GetCurrUser();
                                objDet.ModifiedDate = DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                        dbTransaction.Commit();
                        Session.Remove(sskCrtdObj);
                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "TEC Members Added successfully.");
                        return RedirectToAction("Details", new { id = objtender.TenderID, IsTile = tenderVM.IsTile });
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
            return View(tenderVM);
        }

        public ActionResult CloseTender(TenderVM tenderVM)
        {
            try
            {
                var obj = db.Tenders.Find(tenderVM.TenderID);

                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }
                if (obj.TenderClosedDate <= DateTime.Today && obj.TenderStatus == TenderStatus.TenderOpen)
                {
                    obj.TenderStatus = TenderStatus.TenderClosed;
                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Tender Closed successfully.");
                    if (tenderVM.IsTile)
                    {
                        return RedirectToAction("ProcurementProcessIndex", "ProcurementProcess", new { numericVar1 = 3 });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
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
            return RedirectToAction("Index");

        }

        public ActionResult TECComplete(TenderVM tenderVM)
        {
            try
            {
                var obj = db.Tenders.Find(tenderVM.TenderID);

                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }
                if(obj.TenderStatus == TenderStatus.TECSelected)
                {
                    obj.TenderStatus = TenderStatus.TECRecommend;
                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "TEC Completed successfully.");
                    if (tenderVM.IsTile)
                    {
                        return RedirectToAction("ProcurementProcessIndex", "ProcurementProcess", new { numericVar1 = 3 });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
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
            return RedirectToAction("Index");

        }

        public ActionResult ChildtecIndex(int? id, bool isToEdit = false)
        {
            TenderVM obj;

            if (isToEdit && Session[sskCrtdObj] is TenderVM)
            {
                obj = (TenderVM)Session[sskCrtdObj];
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tender Tenderdetails = db.Tenders.Where(x => x.TenderID == id).FirstOrDefault();
                if (Tenderdetails == null)
                {
                    return HttpNotFound();
                }
                obj = new TenderVM(Tenderdetails);
            }

            ViewBag.IsToEdit = isToEdit;
            ViewBag.TenderId = obj.TenderID;
            return PartialView("_ChildtecIndex", obj.TECMembers);
        }


        public ActionResult ChildTecCreate(int? tenderID)
        {
            if (tenderID != 0)
            {
                if (tenderID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tender tender = db.Tenders.Find(tenderID);
                if (tender == null)
                {
                    return HttpNotFound();
                }
            }
            var tecmVM = new TecMemberVM() { ProcReqId = tenderID.Value };
            return PartialView("_ChildTecCreate", tecmVM);
        }

        [HttpPost]
        public ActionResult ChildTecCreate([Bind(Include = "TECMemberID,ProcReqId,EmployeeId")] TecMemberVM tecMembers)
        {
            TenderVM obj;
            try
            {
                obj = Session.GetObject<TenderVM>();
                if (tecMembers.EmployeeId == 0)
                { ModelState.AddModelError("EmployeeId", "Member is required."); }


                if (ModelState.IsValid)
                {
                    var employee = db.Employees.Find(tecMembers.EmployeeId);

                    tecMembers.TECMemberID = Math.Min(obj.TECMembers.Select(x => x.TECMemberID).MinOrDefault(), 0) - 1;
                    tecMembers.EmpName = employee.Title.ToEnumChar()+". "+ employee.Initials+ " " + employee.LastName;
                    tecMembers.DesignationDesc = employee.Designation.Description;
                    tecMembers.BranchDesc = employee.BranchDepartment.Branch.Description;
                    obj.TECMembers.Add(tecMembers);
                    Session.SetObject(obj);
                    AddAlert(AlertStyles.success, "TEC Member added successfully.");
                    string url = Url.Action("ChildtecIndex", new { id = tecMembers.TECMemberID, isToEdit = true });
                    return Json(new { success = true, url = url });

                }

            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(AlertStyles.danger, ex.GetInnerException().Message); }


            return PartialView("_ChildTecCreate", tecMembers );

        }

        [HttpPost, ActionName("ChildtecDelete")]
        public ActionResult ChildTecDeleteConfirmed(int id)
        {
            string msg = string.Empty;
            try
            {
                var objSession = Session.GetObject<TenderVM>();
                var lst = objSession.TECMembers;
                var obj = lst.FirstOrDefault(x => x.TECMemberID == id);
                lst.Remove(obj);
                Session.SetObject(objSession);

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "TEC Member successfully removed.");
            }
            catch (Exception ex)
            {
                msg = ex.GetInnerException().Message;
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, msg);
            }
            string url = "";
            if (msg.IsBlank())
            { url = Url.Action("ChildTecIndex", new { isToEdit = true }); }
            return Json(new { success = true, url = url, msg });
        }

        public ActionResult BoardApprove(int? id, bool tile = false)
        {
            var procReq = db.ProcuremenetRequests.Find(id);
            if (procReq.Tender == null)
            {
                return HttpNotFound();
            }
            var objTender = new TenderVM(procReq.Tender);
            objTender.IsTile = tile;
            Session.SetObject(objTender);
            return View(objTender);
        }

        [HttpPost]
        public ActionResult BoardApprove([Bind(Include = "TenderID,IsTile,ReqByName,ReqDate,CompletedDate,Comments,ReqSubject,Description,DivHeadName,UserFeedback,TenderOpenedDate," +
            "TenderClosedDate,AttachmentLink,RecommenedSpecification,TECReportAttachment,SelectedVendorId")] TenderVM tenderVM)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (tenderVM.SelectedVendorId == null || tenderVM.SelectedVendorId == 0)
                    { ModelState.AddModelError("SelectedVendorId", "Selected Vendor must be selected."); }

                    var obj = Session.GetObject<TenderVM>();

                    if (ModelState.IsValid)
                    {
                        var objtender = db.Tenders.Find(tenderVM.TenderID);
                        objtender.ModifiedBy = this.GetCurrUser();
                        objtender.ModifiedDate = DateTime.Now;
                        objtender.TenderBoardApprovedDate = DateTime.Today;
                        objtender.TenderStatus = TenderStatus.BoardApproved;
                        objtender.SelectedVendorId = tenderVM.SelectedVendorId;

                        var objProcReq = db.ProcuremenetRequests.Find(objtender.ProcuremenetRequests.FirstOrDefault().ProReqID);
                        objProcReq.AwardedVendorId = tenderVM.SelectedVendorId;
                        objProcReq.ModifiedBy = this.GetCurrUser();
                        objProcReq.ModifiedDate = DateTime.Now;

                        db.SaveChanges();
                        dbTransaction.Commit();
                        Session.Remove(sskCrtdObj);
                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Board approved successfully.");
                        if (tenderVM.IsTile)
                        {
                            return RedirectToAction("ProcurementProcessIndex", "ProcurementProcess", new { numericVar1 = 3 });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
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
            return View(tenderVM);
        }


        public ActionResult BoardReject([Bind(Include = "TenderID,IsTile,SelectedVendorId")] TenderVM tenderVM)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var obj = Session.GetObject<TenderVM>();

                    if (ModelState.IsValid)
                    {
                        var objtender = db.Tenders.Find(tenderVM.TenderID);
                        objtender.ModifiedBy = this.GetCurrUser();
                        objtender.ModifiedDate = DateTime.Now;
                        objtender.TenderStatus = TenderStatus.BoardRejected;

                        db.SaveChanges();
                        dbTransaction.Commit();
                        Session.Remove(sskCrtdObj);
                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Board rejected successfully.");
                        if (tenderVM.IsTile)
                        {
                            return RedirectToAction("ProcurementProcessIndex", "ProcurementProcess", new { numericVar1 = 3 });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
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
            return View(tenderVM);
        }

        public ActionResult TECReportReceived(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcuremenetRequest ProcuremenetRequests = db.ProcuremenetRequests.Find(id);
            if (ProcuremenetRequests == null)
            {
                return HttpNotFound();
            }
            var obj = new TenderVM(ProcuremenetRequests.Tender);
            var RequestID = (int)ProcuremenetRequests.ReqBy;
            var requestedby = db.Employees.Where(x => x.EmployeeID == RequestID).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.ReqByName = requestedby;
            obj.DivisionHead = ProcuremenetRequests.DivisionHead;
            Session.SetObject(obj);
            return View(obj);
        }

        [HttpPost]
        public ActionResult TECReportReceived([Bind(Include = "TenderID,ProcReqId,TecTeamAssigedOn,FileName,TECReportAttachment,ReqByName,ReqSubject,Description,DivHeadName,AttachmentLink,FileName")] TenderVM tender, HttpPostedFileBase file)
        {
            byte[] curRowVersion = null;
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    ModelState.Clear();

                    var sessionProcurement = Session.GetObject<ProcuremenetRequestVM>();
                    if (file != null)
                    {
                        byte[] bytes;
                        using (var ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            bytes = ms.ToArray();
                        }

                        tender.Base64FileContent = Convert.ToBase64String(bytes);
                        tender.FileName = file.FileName;
                    }
                    else { ModelState.AddModelError("TECReportAttachment", "TEC Report Attachment cannot be empty"); }

                    if (ModelState.IsValid)
                    {
                        var obj = db.Tenders.Where(x => x.ProcuremenetRequests.FirstOrDefault().ProReqID == tender.ProcReqId).FirstOrDefault();
                        if (obj == null)
                        { throw new DbUpdateConcurrencyException(); }

                        curRowVersion = obj.RowVersion;

                        obj.ModifiedBy = this.GetCurrUser();
                        obj.ModifiedDate = DateTime.Now;
                        obj.TecApprovedDate = DateTime.Today;
                        obj.TenderStatus = TenderStatus.TECRecommend;

                        if (!tender.Base64FileContent.IsBlank())
                        {
                            byte[] binData = Convert.FromBase64String(tender.Base64FileContent);
                            var filePath = $"{obj.ProcuremenetRequests.FirstOrDefault().ProReqID}-{tender.FileName}";

                            GraphApiHelper.SaveProcurementAttachment(binData, obj.ProcuremenetRequests.FirstOrDefault().ProReqID, filePath, ProcurementTECReportFolder);
                            obj.TECReportAttachment = GraphApiHelper.GetProcurementAttachment(obj.ProcuremenetRequests.FirstOrDefault().ProReqID, filePath, ProcurementTECReportFolder);
                        }

                        db.SaveChanges();
                        dbTransaction.Commit();
                        AddAlert(AlertStyles.success, "TEC Report Attached successfully.");
                        return RedirectToAction("ProcurementProcessIndex", "ProcurementProcess", new { numericVar1 = 3 });
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.ShowConcurrencyErrors(ex);
                    tender.RowVersion = curRowVersion;
                    dbTransaction.Rollback();
                }
                catch (DbEntityValidationException dbEx)
                { this.ShowEntityErrors(dbEx); dbTransaction.Rollback(); }
                catch (Exception ex)
                { AddAlert(AlertStyles.danger, ex.GetInnerException().Message); dbTransaction.Rollback(); }
            }
            return View(tender);
        }

        public ActionResult ChildItemIndex(int? id, bool isToEdit = false)
        {
            ProcuremenetRequestVM obj;

            if (isToEdit && Session[sskCrtdObj] is ProcuremenetRequestVM)
            {
                obj = (ProcuremenetRequestVM)Session[sskCrtdObj];
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProcuremenetRequest ProcuReqItm = db.ProcuremenetRequests.Where(x => x.ProReqID == id).FirstOrDefault();
                if (ProcuReqItm == null)
                {
                    return HttpNotFound();
                }
                obj = new ProcuremenetRequestVM(ProcuReqItm);
            }

            ViewBag.IsToEdit = isToEdit;
            ViewBag.ProReqID = obj.ProReqID;
            return PartialView("_ChildItemIndex", obj.ProcumentReqItemsDetails);
        }

    }
}