using Microsoft.EntityFrameworkCore;
using NIBM.Procurement.Areas.Base.Controllers;
using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using NIBM.Procurement.Areas.Procurement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NIBM.Procurement.Areas.Procurement.Controllers
{
    public class ProcurementProcessController : BaseController
    {
        // GET: Procurement/ProcurementProcess
        private const string ProcurementSpecAttachmentsFolder = "ProcurementSpecAttachments";
        public ActionResult ProcurementProcessIndex(BaseViewModel<ProcuremenetRequestVM> vm)
        {
            var CurUser = db.Users.Find(CurUserID);
            var CurEmpId = CurUser.EmployeeID;
            var qry = db.ProcuremenetRequests.AsQueryable();
            vm.boolVar1 = true;
            vm.SetList(qry, "ReqSubject");

            if (vm.FilterBy == "ReqSubject" && vm.Filter != null)
            {
                qry = qry.Where(x => x.ReqSubject.Contains(vm.Filter)).AsQueryable();

            }
            if (vm.FilterBy == "FromDepart" && vm.Filter != null)
            {
                var Shdesc = qry.Where(x => x.Employee.BranchDepartment.Department.Description.Contains(vm.Filter)).AsQueryable();
                var desc = qry.Where(x => x.Employee.BranchDepartment.Department.Description.Contains(vm.Filter)).AsQueryable();
                if (Shdesc.Count() > 0)
                { qry = Shdesc; }
                else { qry = desc; }
            }
            if (vm.FilterBy == "FromBranch" && vm.Filter != null)
            {
                qry = qry.Where(x => x.Employee.BranchDepartment.Branch.Description == vm.Filter).AsQueryable();

            }

            if (vm.FilterBy == "Description" && vm.Filter != null)
            {
                qry = qry.Where(x => x.Description.Contains(vm.Filter)).AsQueryable();

            }

            bool roleProcurement = CurUser.UserRoles.Where(y => y.Role.Code == "ProcurementSupervisor").Count() > 0 ? true : false;
            var HrRecommandName = db.Employees.Where(x => x.DesignationID == 3).Select(x => x.Title.ToEnumChar("") + ". " + x.Initials.Trim() + " " + x.LastName).FirstOrDefault();
            var DGInitials = db.Employees.Where(x => x.DesignationID == 4).Select(x => x.Title.ToEnumChar("") + ". " + x.Initials.Trim() + " " + x.LastName).FirstOrDefault();

            if (roleProcurement || AdminRole)
            {
                var procurementReq = qry.Where(x => CurEmpId.HasValue && (AdminRole || roleProcurement))
                .ToList().Select(e => (new
                {
                    e.ProReqID,
                    e.ReqSubject,
                    e.ReqDate,
                    FromDepart = e.Employee.BranchDepartment.Department.Description != null ? e.Employee.BranchDepartment.Department.Description : e.Employee.BranchDepartment.Department.Description,
                    FromBranch = e.Employee.BranchDepartment.Branch.Description,
                    RequestedBy = e.Employee.Title.ToEnumChar("") + ". " + e.Employee.Initials.Trim() + " " + e.Employee.LastName,
                    SpecRecomnedBy = e.SpecRecomnedBy != null ? db.Employees.Find(e.SpecRecomnedBy).Title.ToEnumChar("") + ". " + db.Employees.Find(e.SpecRecomnedBy).Initials.Trim() + " " + db.Employees.Find(e.SpecRecomnedBy).LastName : "",
                    e.CompletedDate,
                    e.ProcurementProcessStartedDate,
                    e.DivHeadAppORRejDate,
                    DGAppORRejDate = e.DGAppORRejDate == null ? e.HRAppRecommendORRejDate : e.DGAppORRejDate,
                    DivHead = (e.Status == ProcurementReqStatus.PendingApproval || e.Status == ProcurementReqStatus.ReqforSpec) ? e.DivHead.Title.ToEnumChar("") + ". " + e.DivHead.Initials.Trim() + " " + e.DivHead.LastName :
                            e.Status == ProcurementReqStatus.SpecRecommended ? e.Employee.Title.ToEnumChar("") + ". " + e.Employee.Initials.Trim() + " " + e.Employee.LastName :
                            e.Status == ProcurementReqStatus.HRRecommended ? DGInitials : e.Status == ProcurementReqStatus.ProcurementDeptReceived ? HrRecommandName : "",
                    Approvedby = e.Status == ProcurementReqStatus.DGApproved ? DGInitials : HrRecommandName,
                    Status = e.Status.ToEnumChar(),
                    e.AttachmentLink,
                    e.RecommenedSpecification,
                    e.Description,
                    e.Comments,
                    e.SupervisorComment,
                    e.Tender,
                    TenderStatus = e.Tender == null ? "" : e.Tender.TenderStatus.ToEnumChar(),
                    TenderClosedDate = e.Tender == null ? "" : e.Tender.TenderClosedDate.ToString("yyyy-MM-dd"),
                    e.ProcessType,
                    ProcuItemDetails = e.ProcurementReqItems.ToList()
                                   .Select(a => (new
                                   {
                                       ItemDesc = a.ItemDesc,
                                       ReqQty = a.ReqQty
                                   }).ToDynamic()).ToList()

                }).ToDynamic()).ToList().OrderBy(y => y.ReqDate);

                List<string> approvalStatusList = new List<string>() { ProcurementReqStatus.PendingApproval.ToEnumChar(), ProcurementReqStatus.DivisionHeadApproved.ToEnumChar(),
                    ProcurementReqStatus.HRRecommended.ToEnumChar(), ProcurementReqStatus.ProcurementDeptReceived.ToEnumChar() };

                List<string> onProgressProcStatus = new List<string>() { ProcurementReqStatus.DGApproved.ToEnumChar(), ProcurementReqStatus.HRApproved.ToEnumChar(),
                    ProcurementReqStatus.ReqforSpec.ToEnumChar() };

                List<string> tenderStatus = new List<string>() { ProcurementReqStatus.DGApproved.ToEnumChar(), ProcurementReqStatus.HRApproved.ToEnumChar(),
                    ProcurementReqStatus.SpecRecommended.ToEnumChar() };


                var approvalTab = procurementReq.ToList().Where(x => approvalStatusList.Contains(x.Status));

                var onProgressTab = procurementReq.ToList().Where(x => onProgressProcStatus.Contains(x.Status) && x.Tender == null && x.ProcessType == null);

                var tenderTab = procurementReq.ToList().Where(x => tenderStatus.Contains(x.Status) && ((x.Tender == null && x.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess)
                    || (x.Tender != null && x.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess && x.TenderStatus != TenderStatus.BoardApproved.ToEnumChar() && 
                    x.TenderStatus != TenderStatus.BoardRejected.ToEnumChar())));

                var completionTab = procurementReq.ToList().Where(x => x.Status == ProcurementReqStatus.ItemReceived.ToEnumChar() || 
                    (x.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess && x.Tender != null && x.Tender.TenderStatus == TenderStatus.BoardApproved)
                    || (x.ProcessType == ProcurementProcessType.Last3monthprocess && x.Status == ProcurementReqStatus.SpecRecommended.ToEnumChar())
                    || x.ProcessType == ProcurementProcessType.CompletedByAdvance || x.ProcessType == ProcurementProcessType.CompletedByPettyCash);

                ViewBag.procApproval = approvalTab;
                ViewBag.procApprovalCount = approvalTab.Count();
                ViewBag.procOnProgress = onProgressTab;
                ViewBag.procOnProgressCount = onProgressTab.Count();
                ViewBag.procTender = tenderTab;
                ViewBag.procTenderCount = tenderTab.Count();
                ViewBag.procCompletion = completionTab;
                ViewBag.procCompletionCount = completionTab.Count();
            }

            SetCardNotifications();
            return View(vm);
        }




        private void SetCardNotifications()
        {
            var CurUser = db.Users.Find(CurUserID);
            var CurEmpId = CurUser.EmployeeID;
            var curEmpdesigId = CurUser.Employee.DesignationID;
            DateTime startDate = new DateTime(DateTime.Now.Year, 01, 01, 00, 00, 00);
            DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
            var qry = db.ProcuremenetRequests.Where(y => y.ReqDate >= startDate && y.ReqDate <= endDate).AsQueryable();

            List<ProcurementReqStatus> approvalStatusList = new List<ProcurementReqStatus>() { ProcurementReqStatus.PendingApproval, ProcurementReqStatus.DivisionHeadApproved,
                    ProcurementReqStatus.HRRecommended, ProcurementReqStatus.ProcurementDeptReceived };

            List<ProcurementReqStatus> onProgressProcStatus = new List<ProcurementReqStatus>() { ProcurementReqStatus.DGApproved, ProcurementReqStatus.HRApproved,
                    ProcurementReqStatus.ReqforSpec };

            List<ProcurementReqStatus> tenderStatus = new List<ProcurementReqStatus>() { ProcurementReqStatus.DGApproved, ProcurementReqStatus.HRApproved,
                    ProcurementReqStatus.SpecRecommended };

            int totRequests = qry.AsQueryable().Where(x => CurEmpId.HasValue && x.ReqBy == CurEmpId).Count();

            var approval = qry.Where(x => approvalStatusList.Contains(x.Status)).Count();

            var onProgress = qry.Where(x => onProgressProcStatus.Contains(x.Status) && x.Tender == null && x.ProcessType == null).Count();

            var tender = qry.ToList().Where(x => tenderStatus.Contains(x.Status) && ((x.Tender == null && x.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess)
                    || (x.Tender != null && x.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess && x.Tender.TenderStatus != TenderStatus.BoardApproved &&
                    x.Tender.TenderStatus != TenderStatus.BoardRejected))).Count();

            var completion = qry.Where(x => x.Status == ProcurementReqStatus.ItemReceived || (x.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess
                    && x.Tender != null && x.Tender.TenderStatus == TenderStatus.BoardApproved) || (x.ProcessType == ProcurementProcessType.Last3monthprocess && x.Status == ProcurementReqStatus.SpecRecommended)
                    || x.ProcessType == ProcurementProcessType.CompletedByAdvance || x.ProcessType == ProcurementProcessType.CompletedByPettyCash).Count();

            var divPendingCount = qry.AsQueryable().Where(x => CurEmpId.HasValue && (x.Status == ProcurementReqStatus.PendingApproval && x.DivisionHead == CurEmpId)).Count();


            ViewBag.ApprovalPending = approval == 0 ? "0" : approval.ToString("#,#");
            ViewBag.OnProcess = onProgress == 0 ? "0" : onProgress.ToString("#,#");
            ViewBag.OnTenderProcess = tender == 0 ? "0" : tender.ToString("#,#");
            ViewBag.PendingCompletion = completion == 0 ? "0" : completion.ToString("#,#");
            ViewBag.TotalRequests = totRequests == 0 ? "0" : totRequests.ToString("#,#");
            ViewBag.DivApprovalPending = divPendingCount == 0 ? "0" : divPendingCount.ToString("#,#");

            ViewBag.AdminRole = AdminRole;
        }


        public ActionResult ChildIndex(int? id, bool isToEdit = false)
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
            return PartialView("_ChildIndex", obj.ProcumentReqItemsDetails);
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
        [HttpPost]
        public ActionResult DepartmentRecieve(ProcuremenetRequestVM procuremenetRequest)
        {
            var objSession = Session.GetObject<ProcuremenetRequestVM>();
            try
            {

                var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }

                var lst = new List<ProcurementReqStatus> { ProcurementReqStatus.DivisionHeadApproved };

                if (lst.Contains(obj.Status))
                { 
                    obj.Status = ProcurementReqStatus.ProcurementDeptReceived;
                    obj.ProcDeptReceivedDate = DateTime.Now;
                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                }
                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Request successfully received for department.");

                return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 1 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 1 }); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 1 });
        }

        [HttpPost]
        public ActionResult Incomplete(ProcuremenetRequestVM procuremenetRequest)
        {
            var objSession = Session.GetObject<ProcuremenetRequestVM>();
            try
            {
                var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }

                var lst = new List<ProcurementReqStatus> { ProcurementReqStatus.DivisionHeadApproved };

                if (lst.Contains(obj.Status))
                { 
                    obj.Status = ProcurementReqStatus.Incomplete;
                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                }

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Status updated and successfully sent back to the User.");

                return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 1 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 1 }); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 1 });
        }

        [HttpPost]
        public ActionResult RejectForUser(ProcuremenetRequestVM procuremenetRequest)
        {
            var objSession = Session.GetObject<ProcuremenetRequestVM>();
            try
            {
                var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }

                var lst = new List<ProcurementReqStatus> { ProcurementReqStatus.ProcurementDeptReceived };

                if (lst.Contains(obj.Status))
                { 
                    obj.Status = ProcurementReqStatus.HRRejected;
                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                }

                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, "Request successfully rejected.");

                return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 1 });

            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 1 }); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("Details", new { id = procuremenetRequest.ProReqID, TempNo = 1, tile = false });
        }

        [HttpPost]
        public ActionResult CloseTenderIndex(ProcuremenetRequestVM procuremenetRequest)
        {
            try
            {

                var objProc = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                if (objProc == null)
                { throw new DbUpdateConcurrencyException(""); }

                if (objProc.Tender.TenderStatus == TenderStatus.TenderOpen)
                {
                    objProc.Tender.TenderStatus = TenderStatus.TenderClosed;
                    objProc.Tender.ModifiedBy = this.GetCurrUser();
                    objProc.Tender.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                }
                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Tender Closed successfully.");

                return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 3 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 3 }); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 3 });
        }

        [HttpPost]
        public ActionResult ItemsReceivedIndex(ProcuremenetRequestVM procuremenetRequest)
        {
            var objSession = Session.GetObject<ProcuremenetRequestVM>();
            try
            {

                var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }

                if ((obj.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess && obj.Tender != null && obj.Tender.TenderStatus == TenderStatus.BoardApproved)
                        || obj.ProcessType == ProcurementProcessType.CompletedByAdvance || obj.ProcessType == ProcurementProcessType.CompletedByPettyCash)
                { 
                    obj.Status = ProcurementReqStatus.ItemReceived;
                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                }
                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Items Received successfully.");

                return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 4 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 4 }); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 4 });
        }
        
        [HttpPost]
        public ActionResult PaymentComplete(ProcuremenetRequestVM procuremenetRequest)
        {
            var objSession = Session.GetObject<ProcuremenetRequestVM>();
            try
            {
                var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }

                if (obj.Status == ProcurementReqStatus.ItemReceived)
                { 
                    obj.Status = ProcurementReqStatus.PaymentComplete;
                    obj.CompletedDate = DateTime.Now;
                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                }
                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Payment completed successfully.");

                return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 4 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 4 }); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 4 });
        }

        public ActionResult RequestForSpecs(int? id)
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
            var obj = new ProcuremenetRequestVM(ProcuremenetRequests);
            var RequestID = (int)ProcuremenetRequests.ReqBy;
            var requestedby = db.Employees.Where(x => x.EmployeeID == RequestID).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.RequestBY = requestedby;
            obj.DivisionHead = ProcuremenetRequests.DivisionHead;
            ViewBag.idsToExcluede = db.Employees.Where(x => x.EmployeeID == RequestID).Select(x => x.EmployeeID).ToList();
            Session.SetObject(obj);
            return View(obj);
        }

        [HttpPost]
        public ActionResult RequestForSpecs([Bind(Include = "ProReqID,SpecRecomnedBy,RequestBY,ReqSubject,Description,DivHeadName,AttachmentLink")] ProcuremenetRequestVM procuremenetRequest)
        {
            byte[] curRowVersion = null;
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    ModelState.Clear();
                    var sessionProcurement = Session.GetObject<ProcuremenetRequestVM>();

                    if (procuremenetRequest.SpecRecomnedBy == null)
                    { ModelState.AddModelError("SpecRecomnedBy", "Spec Requesting From is required."); }

                    if (ModelState.IsValid)
                    {
                        var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                        if (obj == null)
                        { throw new DbUpdateConcurrencyException(); }

                        curRowVersion = obj.RowVersion;

                        obj.ModifiedBy = this.GetCurrUser();
                        obj.ModifiedDate = DateTime.Now;
                        var modObj = procuremenetRequest.GetEntity();
                        modObj.CopyContent(obj, "SpecRecomnedBy");
                        obj.SpecRequestedOn = DateTime.Today;
                        obj.Status = ProcurementReqStatus.ReqforSpec;

                        db.SaveChanges();
                        dbTransaction.Commit();
                        AddAlert(AlertStyles.success, "Spec Requested successfully.");
                        return RedirectToAction("RequestForSpecDetails", new { id = obj.ProReqID });
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.ShowConcurrencyErrors(ex);
                    procuremenetRequest.RowVersion = curRowVersion;
                    dbTransaction.Rollback();
                }
                catch (DbEntityValidationException dbEx)
                { this.ShowEntityErrors(dbEx); dbTransaction.Rollback(); }
                catch (Exception ex)
                { AddAlert(AlertStyles.danger, ex.GetInnerException().Message); dbTransaction.Rollback(); }
            }
            return View(procuremenetRequest);
        }

        public ActionResult SpecReceived(int? id)
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
            var obj = new ProcuremenetRequestVM(ProcuremenetRequests);
            var RequestID = (int)ProcuremenetRequests.ReqBy;
            var requestedby = db.Employees.Where(x => x.EmployeeID == RequestID).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.SpecRequstedFromName = db.Employees.Where(x => x.EmployeeID == ProcuremenetRequests.SpecRecomnedBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.RequestBY = requestedby;
            obj.DivisionHead = ProcuremenetRequests.DivisionHead;
            obj.SpecRecomnedOn = DateTime.Today;
            Session.SetObject(obj);
            return View(obj);
        }

        [HttpPost]
        public ActionResult SpecReceived([Bind(Include = "ProReqID,TenderID,ProcessType,SpecRecomnedOn,SpecRecomnedBy,RequestBY,ReqSubject,Description,DivHeadName,AttachmentLink,FileName,SpecRequstedFromName")] ProcuremenetRequestVM procuremenetRequest, HttpPostedFileBase file)
        {
            byte[] curRowVersion = null;
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    ModelState.Clear();

                    if (procuremenetRequest.SpecRecomnedOn == null)
                    { ModelState.AddModelError("SpecRecomnedOn", "Spec Received On Date is required."); }

                    if (procuremenetRequest.ProcessType == ProcurementProcessType.Last3monthprocess && procuremenetRequest.TenderID == null)
                    { ModelState.AddModelError("TenderID", "Tender must be selected for proceed with Last 3 Month Process Type."); }

                    if (procuremenetRequest.ProcessType == null)
                    { ModelState.AddModelError("ProcessType", "Process Type is required."); }

                    var sessionProcurement = Session.GetObject<ProcuremenetRequestVM>();
                    
                    if (file != null)
                    {
                        byte[] bytes;
                        using (var ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            bytes = ms.ToArray();
                        }

                        procuremenetRequest.Base64FileContent = Convert.ToBase64String(bytes);
                        procuremenetRequest.FileName = file.FileName;
                    }
                    else { ModelState.AddModelError("RecommenedSpecification", "Specification Attachment cannot be empty"); }

                    if (ModelState.IsValid)
                    {
                        var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                        if (obj == null)
                        { throw new DbUpdateConcurrencyException(); }

                        curRowVersion = obj.RowVersion;

                        obj.ModifiedBy = this.GetCurrUser();
                        obj.ModifiedDate = DateTime.Now;
                        var modObj = procuremenetRequest.GetEntity();
                        modObj.CopyContent(obj, "SpecRecomnedOn,ProcessType");
                        obj.Status = ProcurementReqStatus.SpecRecommended;

                        if (!procuremenetRequest.Base64FileContent.IsBlank())
                        {
                            byte[] binData = Convert.FromBase64String(procuremenetRequest.Base64FileContent);
                            var filePath = $"{obj.ProReqID}-{procuremenetRequest.FileName}";

                            GraphApiHelper.SaveProcurementAttachment(binData, obj.ProReqID, filePath, ProcurementSpecAttachmentsFolder);
                            obj.RecommenedSpecification = GraphApiHelper.GetProcurementAttachment(obj.ProReqID, filePath, ProcurementSpecAttachmentsFolder);
                        }

                        if (procuremenetRequest.ProcessType == ProcurementProcessType.Last3monthprocess && procuremenetRequest.TenderID != null)
                        {
                            var pastTendar = db.Tenders.Find(procuremenetRequest.TenderID);
                            if (pastTendar != null)
                            {
                                obj.TenderID = pastTendar.TenderID;
                                obj.AwardedVendorId = pastTendar.SelectedVendorId;
                            }
                        }

                        db.SaveChanges();
                        dbTransaction.Commit();
                        AddAlert(AlertStyles.success, "Spec Received successfully.");
                        return RedirectToAction("SpecReceivedDetails", new { id = obj.ProReqID });
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.ShowConcurrencyErrors(ex);
                    procuremenetRequest.RowVersion = curRowVersion;
                    dbTransaction.Rollback();
                }
                catch (DbEntityValidationException dbEx)
                { this.ShowEntityErrors(dbEx); dbTransaction.Rollback(); }
                catch (Exception ex)
                { AddAlert(AlertStyles.danger, ex.GetInnerException().Message); dbTransaction.Rollback(); }
            }
            return View(procuremenetRequest);
        }

        public ActionResult RequestForSpecDetails(int? id)
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

            var obj = new ProcuremenetRequestVM(ProcuremenetRequests);
            var RequestID = (int)ProcuremenetRequests.ReqBy;
            var requestedby = db.Employees.Where(x => x.EmployeeID == RequestID).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.SpecRequstedFromName = db.Employees.Where(x => x.EmployeeID == ProcuremenetRequests.SpecRecomnedBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.RequestBY = requestedby;
            obj.DivisionHead = ProcuremenetRequests.DivisionHead;
            Session.SetObject(obj);
            return View(obj);
        }

        public ActionResult SpecReceivedDetails(int? id)
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

            var obj = new ProcuremenetRequestVM(ProcuremenetRequests);
            var RequestID = (int)ProcuremenetRequests.ReqBy;
            var requestedby = db.Employees.Where(x => x.EmployeeID == RequestID).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.SpecRequstedFromName = db.Employees.Where(x => x.EmployeeID == ProcuremenetRequests.SpecRecomnedBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.RequestBY = requestedby;
            obj.DivisionHead = ProcuremenetRequests.DivisionHead;
            obj.TenderDesc = obj.TenderID != null ? (obj.Tender.ProcuremenetRequests.FirstOrDefault().ReqSubject + "-" + obj.Tender.ProcuremenetRequests.FirstOrDefault().Description) : "";
            Session.SetObject(obj);
            return View(obj);
        }
        public ActionResult CompletePayment(int? id, bool tile = false)
        {
            var procReq = db.ProcuremenetRequests.Find(id);
            //if (procReq.Tender == null)
            //{
            //    return HttpNotFound();
            //}
            var objProcReqr = new ProcuremenetRequestVM(procReq);
            objProcReqr.IsTile = tile;
            Session.SetObject(objProcReqr);
            return View(objProcReqr);
        }

        [HttpPost]
        public ActionResult CompletePayment([Bind(Include = "ProReqID,IsTile,AmountPurchased,ProcessType,AwardedVendor,TenderOpenedDate,TenderClosedDate,TecApprovedDate,TenderBoardApprovedDate," +
            "EmpName,ReqDate,Comments,ReqSubject,Description,DivHeadName,UserFeedback,AttachmentLink")] ProcuremenetRequestVM procReq)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                ModelState.Clear();
                try
                {
                    if (procReq.AmountPurchased == null || procReq.AmountPurchased == 0)
                    { ModelState.AddModelError("AmountPurchased", "Amount Purchased is required."); }
                    if (ModelState.IsValid)
                    {
                        var objProcurement = db.ProcuremenetRequests.Find(procReq.ProReqID);
                        objProcurement.ModifiedBy = this.GetCurrUser();
                        objProcurement.ModifiedDate = DateTime.Now;
                        objProcurement.Status = ProcurementReqStatus.PaymentComplete;
                        objProcurement.CompletedDate = DateTime.Now;
                        objProcurement.AmountPurchased = procReq.AmountPurchased;

                        db.SaveChanges();
                        dbTransaction.Commit();
                        Session.Remove(sskCrtdObj);
                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Payment Completed successfully.");
                        return RedirectToAction("ProcurementProcessIndex", new { numericVar1 = 4 });
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
            return View(procReq);
        }

    }
}