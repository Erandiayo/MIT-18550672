using Microsoft.EntityFrameworkCore;
using NIBM.Procurement.Areas.Base.Controllers;
using NIBM.Procurement.Common;
using NIBM.Procurement.Areas.Procurement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NIBM.Procurement.Areas.Procurement.Controllers
{
    public class ApproveRequestController : BaseController
    {
        // GET: Procurement/ApproveRequest

        public ActionResult Requests(BaseViewModel<ProcuremenetRequestVM> vm)
        {
            var CurUser = db.Users.Find(CurUserID);
            var CurEmpId = CurUser.EmployeeID;
            var curEmpdesigId = CurUser.Employee.DesignationID;
            bool IsHR = false;
            bool IsDG = false;

            var qry = db.ProcuremenetRequests.AsQueryable();

            vm.SetList(qry, "ReqSubject");

            if (vm.FilterBy == "ReqSubject" && vm.Filter != null)
            {
                qry = qry.Where(x => x.ReqSubject.Contains(vm.Filter)).AsQueryable();
            }
            if (vm.FilterBy == "FromBranch" && vm.Filter != null)
            {
                qry = qry.Where(x => x.Employee.BranchDepartment.Branch.Description == vm.Filter).AsQueryable();
            }
            if (vm.FilterBy == "ReqBy" && vm.Filter != null)
            {
                var empID = db.Employees.Where(x => x.LastName.Contains(vm.Filter)).Select(y => y.EmployeeID).FirstOrDefault();
                qry = qry.Where(x => x.ReqBy == empID).AsQueryable();
            }
            if (vm.FilterBy == "Description" && vm.Filter != null)
            {
                qry = qry.Where(x => x.Description.Contains(vm.Filter)).AsQueryable();
            }

            var filterReq = qry.AsQueryable().Where(x => CurEmpId.HasValue)
            .ToList().Select(e => (new
            {
                e.ProReqID,
                e.ReqSubject,
                e.Description,
                FromDepart = e.Employee.BranchDepartment.Department.Description != null ? e.Employee.BranchDepartment.Department.Description : e.Employee.BranchDepartment.Department.Description,
                FromBranch = e.Employee.BranchDepartment.Branch.Description,
                RequestedBy = e.Employee.Title.ToEnumChar("") + ". " + e.Employee.Initials.Trim() + " " + e.Employee.LastName,
                e.ReqDate,
                DivHead = e.DivHead.Title + " " + e.DivHead.Initials + " " + e.DivHead.LastName,
                Status = e.Status.ToEnumChar(),
                e.AttachmentLink,
                e.RowVersion,
                e.SupervisorComment,
                e.DivisionHead,
                ProcuItemDetails = e.ProcurementReqItems.ToList()
                                   .Select(a => (new
                                   {
                                       ItemDesc = a.ItemDesc,
                                       ReqQty = a.ReqQty
                                   }).ToDynamic()).ToList()
            }).ToDynamic()).ToList().OrderBy(y => y.ReqDate);

            //----------------- Procurement Request Approvals count ------------------- 
            var procurementApp = filterReq.Where(x => x.Status == ProcurementReqStatus.ProcurementDeptReceived.ToEnumChar());

            var procurementAppPendingApp = filterReq.Where(x => x.Status == ProcurementReqStatus.PendingApproval.ToEnumChar() && x.DivisionHead == CurEmpId);

            //----------------- Pending HR Approvals count ------------------- 
            if (curEmpdesigId == 3)
            {
                procurementApp = filterReq.Where(x => x.Status == ProcurementReqStatus.ProcurementDeptReceived.ToEnumChar());
            }

            //----------------- Pending DG Approvals  ------------------- 
            if (curEmpdesigId == 4)
            {
                procurementApp = filterReq.Where(x => x.Status == ProcurementReqStatus.HRRecommended.ToEnumChar());
            }
            if (curEmpdesigId == 3) { IsHR = true; }            
            if (curEmpdesigId == 4) { IsDG = true; }            

            ViewBag.ProcurementApp = procurementApp;
            ViewBag.procurementAppPendingApp = procurementAppPendingApp;
            ViewBag.procurementAppPendingAppCount = procurementAppPendingApp.Count();
            ViewBag.ProcurementAppCount = procurementApp.Count();
            ViewBag.IsHR = IsHR;
            ViewBag.IsDG = IsDG;

            return View();
        }

        [HttpPost]
        public ActionResult Approve([Bind(Include = "StringPara1,ValId")] ProcuremenetRequestVM procuremenetRequestVM)
        {
            try
            {
                var selectedReq = ProcurementJson(procuremenetRequestVM.StringPara1);
                if (selectedReq.Count() > 0)
                {
                    foreach (var item in selectedReq)
                    {
                        procuremenetRequestVM.ValId = item.ValId;
                        var obj = db.ProcuremenetRequests.Find(item.ProReqID);
                        if (obj == null)
                        { throw new DbUpdateConcurrencyException(""); }


                        var lst = new List<ProcurementReqStatus> { ProcurementReqStatus.PendingApproval, ProcurementReqStatus.HRRecommended, ProcurementReqStatus.ProcurementDeptReceived };

                        if (obj.Status == ProcurementReqStatus.PendingApproval)
                        {
                            obj.Status = ProcurementReqStatus.DivisionHeadApproved;
                            obj.SupervisorComment = item.Comments;
                            obj.DivHeadAppORRejDate = DateTime.Now;
                        }

                        else if (obj.Status == ProcurementReqStatus.ProcurementDeptReceived)
                        {
                            obj.Status = ProcurementReqStatus.HRApproved;
                            obj.HRAppRecommendORRejDate = DateTime.Now;
                            obj.SupervisorComment = item.Comments;
                        }

                        else if (obj.Status == ProcurementReqStatus.HRRecommended)
                        {
                            obj.Status = ProcurementReqStatus.DGApproved;
                            obj.SupervisorComment = item.Comments;
                            obj.DGAppORRejDate = DateTime.Now;
                        }

                        obj.ModifiedBy = this.GetCurrUser();
                        obj.ModifiedDate = DateTime.Now;
                        obj.ProcurementProcessStartedDate = DateTime.Now;
                        db.SaveChanges();
                    }
                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Selected request(s) approved successfully.");
                }
                else
                {
                    AddAlert(NIBM.Procurement.Common.AlertStyles.danger, "Please select request(s) to approve.");
                }
                return RedirectToAction("Requests");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ApproveIndex"); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ApproveDetails", new { id = procuremenetRequestVM.ProReqID });

        }

        [HttpPost]
        public ActionResult Recommend([Bind(Include = "StringPara1")] ProcuremenetRequestVM procuremenetRequest)
        {
            var objSession = Session.GetObject<ProcuremenetRequestVM>();
            try
            {
                var selectedReq = ProcurementJson(procuremenetRequest.StringPara1);
                if (selectedReq.Count() > 0)
                {
                    foreach (var item in selectedReq)
                    {
                        var obj = db.ProcuremenetRequests.Find(item.ProReqID);
                        if (obj == null)
                        { throw new DbUpdateConcurrencyException(""); }

                        var lst = new List<ProcurementReqStatus> { ProcurementReqStatus.ProcurementDeptReceived };

                        if (lst.Contains(obj.Status))
                        {
                            obj.Status = ProcurementReqStatus.HRRecommended;
                            obj.HRAppRecommendORRejDate = DateTime.Now;
                            obj.SupervisorComment = item.Comments;
                        }
                        db.SaveChanges();
                    }
                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Request(s) successfully recommended.");
                }
                else { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, "Please select request(s) to recommend."); }
                return RedirectToAction("Requests");

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
            return RedirectToAction("ApproveDetails", new { id = procuremenetRequest.ProReqID });
        }

        [HttpPost]
        public ActionResult ApproveSelectIndex(ProcuremenetRequestVM procuremenetRequestVM)
        {
            try
            {
                var obj = db.ProcuremenetRequests.Find(procuremenetRequestVM.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }


                var lst = new List<ProcurementReqStatus> { ProcurementReqStatus.PendingApproval, ProcurementReqStatus.HRRecommended, ProcurementReqStatus.ProcurementDeptReceived };

                if (obj.Status == ProcurementReqStatus.PendingApproval)
                {
                    obj.Status = ProcurementReqStatus.DivisionHeadApproved;
                    obj.Comments = procuremenetRequestVM.Comments;
                    obj.DivHeadAppORRejDate = DateTime.Now;
                }

                else if (obj.Status == ProcurementReqStatus.ProcurementDeptReceived)
                {
                    obj.Status = ProcurementReqStatus.HRApproved;
                    obj.HRAppRecommendORRejDate = DateTime.Now;
                    obj.Comments = procuremenetRequestVM.SupervisorComment;
                }

                else if (obj.Status == ProcurementReqStatus.HRRecommended)
                {
                    obj.Status = ProcurementReqStatus.DGApproved;
                    obj.SupervisorComment = procuremenetRequestVM.Comments;
                    obj.DGAppORRejDate = DateTime.Now;
                }

                obj.ModifiedBy = this.GetCurrUser();
                obj.ModifiedDate = DateTime.Now;
                obj.ProcurementProcessStartedDate = DateTime.Now;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Selected request approved successfully.");
                if (obj.Status.In(ProcurementReqStatus.HRApproved, ProcurementReqStatus.DGApproved))
                {
                    return RedirectToAction("ProcurementProcessIndex", "ProcurementProcess");
                }
                else { return RedirectToAction("Requests"); }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ApproveIndex"); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ApproveDetails", new { id = procuremenetRequestVM.ProReqID });

        }

        public ICollection<ProcuremenetRequestVM> ProcurementJson(string dataJson)
        {
            ICollection<ProcuremenetRequestVM> lstProcReq = new List<ProcuremenetRequestVM>();
            try
            {
                var lst = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(dataJson);

                ProcuremenetRequestVM obj;
                foreach (var det in lst)
                {
                    obj = new ProcuremenetRequestVM();
                    obj.ProReqID = det["ProReqID"].ConvertTo<int>();
                    obj.ValId = det["ValId"].ConvertTo<int>();
                    if (det["Comments"].ToString() != null)
                    { obj.Comments = det["Comments"].ToString(); }

                    if (obj.ProReqID != 0)
                    {
                        lstProcReq.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return lstProcReq;
            }

            return lstProcReq;
        }

        public ActionResult Reject(ProcuremenetRequestVM procuremenetRequestVM)
        {
            try
            {
                var obj = db.ProcuremenetRequests.Find(procuremenetRequestVM.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }

                if (obj.Status == ProcurementReqStatus.HRRecommended)
                {
                    obj.Status = ProcurementReqStatus.DGRejected;
                    obj.DGAppORRejDate = DateTime.Now;
                }

                else if (obj.Status == ProcurementReqStatus.ProcurementDeptReceived)
                {
                    obj.Status = ProcurementReqStatus.HRRejected;
                    obj.HRAppRecommendORRejDate = DateTime.Now;
                    //obj.SupervisorComment = procuremenetRequestVM.SupervisorComment;
                }

                else
                {
                    obj.Status = ProcurementReqStatus.DivisionHeadRejected;
                    obj.DivHeadAppORRejDate = DateTime.Now;
                }

                obj.ModifiedBy = this.GetCurrUser();
                obj.SupervisorComment = procuremenetRequestVM.SupervisorComment;
                obj.ModifiedDate = DateTime.Now;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, "Request rejected successfully.");

                return RedirectToAction("Requests");


            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ApproveIndex"); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ApproveDetails", new { id = procuremenetRequestVM.ProReqID });
        }

    }
}