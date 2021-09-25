using NIBM.Procurement.Areas.Base.Models;
using NIBM.Procurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NIBM.Procurement.Common;
using NIBM.Procurement.Areas.Procurement.Models;

namespace NIBM.Procurement.Areas.Base.Controllers
{
    public class DashBoardController : BaseController
    {    
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ViewBag.DashBoardAction = this.ControllerContext.RouteData.Values["action"].ToString();
        }

        public ActionResult Home()
        {
            var ProcurementDept = (Session[sskCurUsrRoles].ConvertTo<string>()).Contains("ProcurementDept");

            if (ProcurementDept)
            {
                return RedirectToAction("ProcurementProcessIndex", "ProcurementProcess", new { area = "Procurement" });
            }

            var CurUser = db.Users.Find(CurUserID);
            var CurEmpId = CurUser.EmployeeID;
            var curEmpdesigId = CurUser.Employee.DesignationID;
            int pendingCount = 0;
            int pendingDivCount = 0;
            int pendingFeedbacksCount = 0;
            int totRequests = 0;

            totRequests = db.ProcuremenetRequests.Where(x => CurEmpId.HasValue && x.ReqBy == CurEmpId).Count();
            
            pendingFeedbacksCount = db.ProcuremenetRequests.Where(x => CurEmpId.HasValue && 
            x.ReqBy == CurEmpId && x.Status == ProcurementReqStatus.PaymentComplete && x.UserFeedback == null).Count();
           
            pendingCount = db.ProcuremenetRequests.Where(x => CurEmpId.HasValue &&
            (x.Status == ProcurementReqStatus.PendingApproval && x.DivisionHead == CurEmpId)).Count();

            //----------------- Pending HR Approvals count ------------------- 
            if (curEmpdesigId == 3)
            {
                pendingDivCount = db.ProcuremenetRequests.Where(x => CurEmpId.HasValue && 
                (x.Status == ProcurementReqStatus.ProcurementDeptReceived)).Count();
                pendingCount = pendingDivCount + pendingCount;
            }

            //----------------- Pending DG Approvals  ------------------- 
            if (curEmpdesigId == 4)
            {
                pendingCount = db.ProcuremenetRequests.Where(x => CurEmpId.HasValue && 
                (x.Status == ProcurementReqStatus.HRRecommended || (x.Status == ProcurementReqStatus.PendingApproval 
                    && x.DivisionHead == CurEmpId))).Count();
            }

            var vm = new DashBoardVM()
            {
                TotalRequests = totRequests == 0 ? "0" : totRequests.ToString("#,#"),
                DivApprovalPending = pendingCount == 0 ? "0" : pendingCount.ToString("#,#"),
                PendingFeedbacks = pendingFeedbacksCount == 0 ? "0" : pendingFeedbacksCount.ToString("#,#"),
            };

            ViewBag.AdminRole = AdminRole;
            return View(vm);
        }

        public ActionResult ChildProcRequests(BaseViewModel<ProcuremenetRequestVM> vm)
        {
            var CurEmpID = db.Users.Find(CurUserID).EmployeeID;
            vm.SetList(db.ProcuremenetRequests.Where(x => x.ReqBy == CurEmpID).AsQueryable(), "ReqDate");
            return PartialView("_ChildProcRequests", vm);
        }

        public ActionResult HomeProcurement()
        {
            var CurUser = db.Users.Find(CurUserID);
            var CurEmpId = CurUser.EmployeeID;
            var curEmpdesigId = CurUser.Employee.DesignationID;
            //DateTime startDate = new DateTime(DateTime.Now.Year, 01, 01, 00, 00, 00);
            //DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
           // var qry = db.ProcuremenetRequests.Where(y => y.ReqDate >= startDate && y.ReqDate <= endDate).AsQueryable();

            List<ProcurementReqStatus> approvalStatusList = new List<ProcurementReqStatus>() { ProcurementReqStatus.PendingApproval, ProcurementReqStatus.DivisionHeadApproved,
                    ProcurementReqStatus.HRRecommended, ProcurementReqStatus.ProcurementDeptReceived };

            List<ProcurementReqStatus> onProgressProcStatus = new List<ProcurementReqStatus>() { ProcurementReqStatus.DGApproved, ProcurementReqStatus.HRApproved, 
                    ProcurementReqStatus.ReqforSpec };

            List<ProcurementReqStatus> tenderStatus = new List<ProcurementReqStatus>() { ProcurementReqStatus.DGApproved, ProcurementReqStatus.HRApproved,
                    ProcurementReqStatus.SpecRecommended };

            int totRequests = db.ProcuremenetRequests.Where(x => CurEmpId.HasValue && x.ReqBy == CurEmpId).Count();

            var approvalTab = db.ProcuremenetRequests.Where(x => approvalStatusList.Contains(x.Status)).Count();

            var onProgressTab = db.ProcuremenetRequests.Where(x => onProgressProcStatus.Contains(x.Status) && x.Tender == null && x.ProcessType == null).Count();

            var tenderTab = db.ProcuremenetRequests.ToList().Where(x => tenderStatus.Contains(x.Status) && ((x.Tender == null && x.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess)
                    || (x.Tender != null && x.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess && x.Tender.TenderStatus != TenderStatus.BoardApproved &&
                    x.Tender.TenderStatus != TenderStatus.BoardRejected))).Count();

            var completionTab = db.ProcuremenetRequests.Where(x => x.Status == ProcurementReqStatus.ItemReceived || (x.ProcessType == ProcurementProcessType.GoingthroughProcurementProcess 
                    && x.Tender != null && x.Tender.TenderStatus == TenderStatus.BoardApproved) || (x.ProcessType == ProcurementProcessType.Last3monthprocess && x.Status == ProcurementReqStatus.SpecRecommended)
                    || x.ProcessType == ProcurementProcessType.CompletedByAdvance || x.ProcessType == ProcurementProcessType.CompletedByPettyCash).Count();

            var divPendingCount = db.ProcuremenetRequests.AsQueryable().Where(x => CurEmpId.HasValue && (x.Status == ProcurementReqStatus.PendingApproval && x.DivisionHead == CurEmpId)).Count();

            var vm = new DashBoardVM()
            {
                ApprovalPending = approvalTab == 0 ? "0" : approvalTab.ToString("#,#"),
                OnProcess = onProgressTab == 0 ? "0" : onProgressTab.ToString("#,#"),
                OnTenderProcess = tenderTab == 0 ? "0" : tenderTab.ToString("#,#"),
                PendingCompletion = completionTab == 0 ? "0" : completionTab.ToString("#,#"),
                TotalRequests = totRequests == 0 ? "0" : totRequests.ToString("#,#"),
                DivApprovalPending = divPendingCount == 0 ? "0" : divPendingCount.ToString("#,#"),
            };

            ViewBag.AdminRole = AdminRole;
            return View(vm);
        }

    }
}