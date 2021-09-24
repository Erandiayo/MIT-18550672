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
using System.Linq.Dynamic;

namespace NIBM.Procurement.Controllers
{ 
    public class ReportsController  : BaseController
    {
        [HttpPost]
        public ActionResult ProcurementRequestSummaryReport(ReportParameterVM para)
        {
            Session.Remove(sskCrtdObj);

            if (para.FromDate == null)
            { ModelState.AddModelError("FromDate", "From date must be selected"); }
            if (para.ToDate == null)
            { ModelState.AddModelError("ToDate", "To date must be selected"); }
            if (para.FromDate > para.ToDate)
            { ModelState.AddModelError("ToDate", "To date must be greator than or equal to from date"); }

            if (!ModelState.IsValid)
            { return View(para); }

            Session.SetObject(para);
            return Json(new { viewIt = true });
        }

        public ActionResult ProcurementRequestSummaryReport(bool? viewIt)
        
        {
            if (viewIt != true || !(Session[sskCrtdObj] is ReportParameterVM))
            {
                return View(new ReportParameterVM() { FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), ToDate = DateTime.Today });
            }
            var para = Session.GetObject<ReportParameterVM>();

            StringBuilder sb = new StringBuilder("CreatedDate >= @0 && CreatedDate <= @1");
            if (para.BranchID.HasValue)
            { sb.Append(" && Employee.BranchDepartment.Branch.BranchID == " + para.BranchID); }
            if (para.DepartmentID.HasValue && para.DepartmentID != 0)
            { sb.Append(" && Employee.BranchDepartment.Department.DepartmentID == " + para.DepartmentID); }

            DateTime? FromDate = new DateTime(para.FromDate.Value.Year, para.FromDate.Value.Month, para.FromDate.Value.Day, 00, 01, 00);
            DateTime? ToDate = new DateTime(para.ToDate.Value.Year, para.ToDate.Value.Month, para.ToDate.Value.Day, 23, 59, 00);

            var ProcDetails = db.ProcuremenetRequests.Where(sb.ToString(), FromDate, ToDate).AsQueryable().ToList();
            
          var lst = ProcDetails.Select(x => new
                {
                    Branch = x.Employee.BranchDepartment.Branch.Description,
                    Department = x.Employee.BranchDepartment.Department.Description,
                    CreatedDate = x.CreatedDate,
                    ReqBy  = db.Employees.Where(z => z.EmployeeID == x.ReqBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault(),
                    x.ReqSubject,
                    x.Description,
                    DivisionHead =  db.Employees.Where(z => z.EmployeeID == x.DivisionHead).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault(),
                    ProcDeptReceivedDate = x.ProcDeptReceivedDate != null ? x.ProcDeptReceivedDate.ToDateString("yyyy-MM-dd") : " - ",
                    Status = x.Status.ToEnumChar(),
                    Comments = x.Comments != null ?x.Comments : "" ,
                    Feedback = x.UserFeedback != null ? x.UserFeedback : "",
                    x.SupervisorComment,
                    HRAppRecommendORRejDate = x.HRAppRecommendORRejDate != null ? x.HRAppRecommendORRejDate.ToDateString("yyyy-MM-dd") : " - " ,
                    DGAppORRejDate = x.DGAppORRejDate != null ? x.DGAppORRejDate.ToDateString("yyyy-MM-dd") : " - " ,
                    ProcurementProcessStartedDate = x.ProcurementProcessStartedDate != null ? x.ProcurementProcessStartedDate.ToDateString("yyyy-MM-dd") : " - ",
                    CompletedDate = x.CompletedDate != null ? x.CompletedDate.ToDateString("yyyy-MM-dd") : " - ",
                    CompletetionDurationOn = x.CompletedDate == null ? 0 :((x.CompletedDate.Value.Date) - (x.CreatedDate.Date)).Days.ConvertTo<Int32>(),
                    Completed = ProcDetails.Where(y => y.Status == ProcurementReqStatus.ItemReceived).Count(),
                    Received = ProcDetails.Where(y => y.Status == ProcurementReqStatus.ProcurementDeptReceived).Count(),

                }).OrderBy(y => y.CreatedDate).ToList();

            LocalReport report = new LocalReport();
            report.ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/ProcurementRequestSummaryReport.rdlc");

            report.SetParameters(new ReportParameter("FromDate", para.FromDate.Value.ToString("yyyy-MM-dd")));
            report.SetParameters(new ReportParameter("ToDate", para.ToDate.Value.ToString("yyyy-MM-dd")));
            report.SetParameters(new ReportParameter("Branch", para.BranchID == null ? "-- All Branches --" : db.Branches.Find(para.BranchID).Description));
            report.SetParameters(new ReportParameter("Department", para.DepartmentID == null || para.DepartmentID == 0 ? "-- All Departments --" : db.Departments.Find(para.DepartmentID).Description));
            
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsProcuremntRequestSummary";
            rds.Value = lst;
            report.DataSources.Add(rds);

            Byte[] mybytes = report.Render("PDF");

            Response.AppendHeader("content-disposition", "inline; filename=file.pdf");
            return new FileStreamResult(new MemoryStream(mybytes), "application/pdf");
        }

        [HttpPost]
        public ActionResult MonthlyProgressReport(ReportParameterVM para)
        {
            Session.Remove(sskCrtdObj);

            if (para.FromDate == null)
            { ModelState.AddModelError("FromDate", "From date must be selected"); }
            if (para.ToDate == null)
            { ModelState.AddModelError("ToDate", "To date must be selected"); }
            if (para.FromDate > para.ToDate)
            { ModelState.AddModelError("ToDate", "To date must be greator than or equal to from date"); }

            if (!ModelState.IsValid)
            { return View(para); }

            Session.SetObject(para);
            return Json(new { viewIt = true });
        }

        public ActionResult MonthlyProgressReport(bool? viewIt)

        {
            if (viewIt != true || !(Session[sskCrtdObj] is ReportParameterVM))
            {
                return View(new ReportParameterVM() { FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), ToDate = DateTime.Today });
            }
            var para = Session.GetObject<ReportParameterVM>();

            DateTime FromDate = new DateTime(para.FromDate.Value.Year, para.FromDate.Value.Month, para.FromDate.Value.Day, 00, 00, 00);
            DateTime ToDate = new DateTime(para.ToDate.Value.Year, para.ToDate.Value.Month, para.ToDate.Value.Day, 23, 59, 59);

            var Requests = db.ProcuremenetRequests.Where(x => x.ReqDate >= FromDate && x.ReqDate <= ToDate).AsQueryable();

            var CompletedRequests = Requests.Where(x => x.Status == ProcurementReqStatus.PaymentComplete)
                .Select(x => new {
                    ReqDate = x.ReqDate,
                    ReqBy = x.Employee.Initials + " " + x.Employee.LastName,
                    Branch = x.Employee.BranchDepartment.Branch.Description,
                    Subject = x.ReqSubject,
                    DGApprovedOn = x.DGAppORRejDate.ToDateString("yyyy-MM-dd"),
                    ProcessType = x.ProcessType.ToEnumChar(""),
                    CompletedOn = x.CompletedDate.ToDateString("yyyy-MM-dd"),
                    ProcessDays = Math.Round((x.CompletedDate.Value - x.ReqDate).TotalDays, 2)
                }).OrderBy(x => x.ReqDate).ToList();

            var OnProcessRequests = Requests.Where(x => x.Status != ProcurementReqStatus.PaymentComplete)
                .Select(x => new
                {
                    ReqDate = x.ReqDate,
                    ReqBy = x.Employee.Initials + " " + x.Employee.LastName,
                    Branch = x.Employee.BranchDepartment.Branch.Description,
                    Subject = x.ReqSubject,
                    DGApprovedOn = x.DGAppORRejDate.ToDateString("yyyy-MM-dd"),
                    ProcessType = x.ProcessType.ToEnumChar(""),
                    Status = x.Status.ToEnumChar(""),
                    ProcessDays = Math.Round((DateTime.Today - x.ReqDate).TotalDays, 2)
                }).OrderBy(x => x.ReqDate).ToList();

            LocalReport report = new LocalReport();
            report.ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/MonthlyProgressReport.rdlc");

            report.SetParameters(new ReportParameter("FromDate", FromDate.ToString("yyyy-MM-dd")));
            report.SetParameters(new ReportParameter("ToDate", ToDate.ToString("yyyy-MM-dd")));

            ReportDataSource rds;
            rds = new ReportDataSource();
            rds.Name = "dsMonthlyProgressCompletedReq";
            rds.Value = CompletedRequests;
            report.DataSources.Add(rds);

            rds = new ReportDataSource();
            rds.Name = "dsMonthlyProgress";
            rds.Value = OnProcessRequests;
            report.DataSources.Add(rds);

            Byte[] mybytes = report.Render("PDF");

            Response.AppendHeader("content-disposition", "inline; filename=file.pdf");
            return new FileStreamResult(new MemoryStream(mybytes), "application/pdf");
        }

        [HttpPost]
        public ActionResult TenderDetailsOfTenderReport(ReportParameterVM para)
        {
            Session.Remove(sskCrtdObj);

            if (para.NumericPara1 == null || para.NumericPara1 == 0)
            { ModelState.AddModelError("NumericPara1", "Please select a request."); }

            if (!ModelState.IsValid)
            { return View(para); }

            Session.SetObject(para);
            return Json(new { viewIt = true });
        }

        public ActionResult TenderDetailsOfTenderReport(bool? viewIt)
        {
            if (viewIt != true || !(Session[sskCrtdObj] is ReportParameterVM))
            {
                return View(new ReportParameterVM());
            }
            var para = Session.GetObject<ReportParameterVM>();

            var lstHeader = db.ProcuremenetRequests.Where(x => x.ProReqID == para.NumericPara1).ToList()
                .Select(x => new {
                    x.TenderID,
                    x.ReqSubject,
                    TenderOpenedDate = x.Tender.TenderOpenedDate.ToString("yyyy-MM-dd"),
                    TenderClosedDate = x.Tender.TenderClosedDate.ToString("yyyy-MM-dd"),
                    ReqBy = db.Employees.Where(z => z.EmployeeID == x.ReqBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault() 
                          + " [ Through " + x.DivHead.Title.ToEnumChar() + ". " + x.DivHead.Initials + " " + x.DivHead.LastName + " ]",
                    TenderBoardApprovedOn = x.Tender.TenderBoardApprovedDate.HasValue ? x.Tender.TenderBoardApprovedDate.Value.ToString("yyyy-MM-dd") : "",
                    AwardedTo = x.AwardedVendorId == null ? "" : x.Vendor.Name + ". " + x.Vendor.Address,
                    AwardedAmount = x.AmountPurchased,
                    x.ProcurementReqItems,
                    x.Tender.TECMembers,
                    x.Tender.TenderVendors
                });

            var itemLst = lstHeader.FirstOrDefault().ProcurementReqItems
                .Select(x => new {
                    x.ItemDesc,
                    x.ReqQty,
                    UnitType = "Pc"
                });

            var TECLst = lstHeader.FirstOrDefault().TECMembers
                .Select(x => new {
                    Name = x.Employee.Title + ". " + x.Employee.Initials + " " + x.Employee.LastName,
                    Designation = x.Employee.Designation.Description,
                });

            var VendorLst = lstHeader.FirstOrDefault().TenderVendors
                .Select(x => new {
                    x.Vendor.Name,
                    x.Vendor.TelNo,
                    x.Vendor.Address,
                });

            LocalReport report = new LocalReport();
            report.ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/TenderDetailsOfTender.rdlc");

            ReportDataSource rds;
            rds = new ReportDataSource();
            rds.Name = "dsTenderDetailsHdr";
            rds.Value = lstHeader;
            report.DataSources.Add(rds);

            rds = new ReportDataSource();
            rds.Name = "dsTenderDetailsItems";
            rds.Value = itemLst;
            report.DataSources.Add(rds);

            rds = new ReportDataSource();
            rds.Name = "dsTenderDetailsTEC";
            rds.Value = TECLst;
            report.DataSources.Add(rds);

            rds = new ReportDataSource();
            rds.Name = "dsTenderDetailsVendors";
            rds.Value = VendorLst;
            report.DataSources.Add(rds);

            Byte[] mybytes = report.Render("PDF");

            Response.AppendHeader("content-disposition", "inline; filename=file.pdf");
            return new FileStreamResult(new MemoryStream(mybytes), "application/pdf");
        }

        [HttpPost]
        public ActionResult ProcurementRequestDetailReport(ReportParameterVM para)
        {
            Session.Remove(sskCrtdObj);

            if (para.NumericPara1 == null || para.NumericPara1 == 0)
            { ModelState.AddModelError("NumericPara1", "Please select a request."); }

            if (!ModelState.IsValid)
            { return View(para); }

            Session.SetObject(para);
            return Json(new { viewIt = true });
        }

        public ActionResult ProcurementRequestDetailReport(bool? viewIt)
        {
            if (viewIt != true || !(Session[sskCrtdObj] is ReportParameterVM))
            {
                return View(new ReportParameterVM());
            }
            var para = Session.GetObject<ReportParameterVM>();

            var proREq = db.ProcuremenetRequests.Where(x => x.ProReqID == para.NumericPara1).AsQueryable();
            var proItems = db.ProcurementReqItems.Where(x => x.ProReqId == para.NumericPara1).AsQueryable();

            LocalReport report = new LocalReport();
            report.ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/ProcurementRequestReport.rdlc");

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsProcuremntRequestSummary";
            report.EnableHyperlinks = true;
            if (proItems.Count() > 0)
            {
                var lst = proItems.ToList()
                    .Select(x => new
                    {
                        Branch = x.ProReq.Employee.BranchDepartment.Branch.Description,
                        Department = x.ProReq.Employee.BranchDepartment.Department.Description,
                        CreatedDate = x.CreatedDate.ToString("yyyy-MM-dd"),
                        ReqBy = db.Employees.Where(z => z.EmployeeID == x.ProReq.ReqBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault(),
                        ReqSubject = x.ProReq.ReqSubject,
                        x.ProReq.Description,
                        DivisionHead = db.Employees.Where(z => z.EmployeeID == x.ProReq.DivisionHead).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName + "  - " + y.Designation.Description).FirstOrDefault(),
                        ProcDeptReceivedDate = x.ProReq.ProcDeptReceivedDate,
                        Status = x.ProReq.Status.ToEnumChar(),
                        Comments = x.ProReq.Comments != null ? x.ProReq.Comments : "",
                        ItemDesc = x.ItemDesc,
                        ReqQty = x.ReqQty,
                        DivHeadAppORRejDate = x.ProReq.DivHeadAppORRejDate,
                        AttachmentUrl = x.ProReq.AttachmentLink != null ? x.ProReq.AttachmentLink : "",
                        HRAppRecommendORRejDate = x.ProReq.HRAppRecommendORRejDate.ToDateString("yyyy-MM-dd"),
                        DGAppORRejDate = x.ProReq.DGAppORRejDate.ToDateString("yyyy-MM-dd"),
                        ProcurementProcessStartedDate = x.ProReq.ProcurementProcessStartedDate.ToDateString("yyyy-MM-dd"),

                    }).OrderBy(y => y.CreatedDate).ToList();
                rds.Value = lst;
                report.DataSources.Add(rds);
            }

            else
            {
                var lst = proREq.ToList()
                    .Select(x => new
                    {
                        Branch = x.Employee.BranchDepartment.Branch.Description,
                        Department = x.Employee.BranchDepartment.Department.Description,
                        CreatedDate = x.CreatedDate.ToString("yyyy-MM-dd"),
                        ReqBy = db.Employees.Where(z => z.EmployeeID == x.ReqBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault(),
                        ReqSubject = x.ReqSubject,
                        x.Description,
                        DivisionHead = db.Employees.Where(z => z.EmployeeID == x.DivisionHead).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault(),
                        ProcDeptReceivedDate = x.ProcDeptReceivedDate,
                        Status = x.Status.ToEnumChar(),
                        Comments = x.Comments != null ? x.Comments : "",
                        DivHeadAppORRejDate = x.DivHeadAppORRejDate,
                        AttachmentUrl = x.AttachmentLink != null ? x.AttachmentLink : "",
                        HRAppRecommendORRejDate = x.HRAppRecommendORRejDate.ToDateString("yyyy-MM-dd"),
                        DGAppORRejDate = x.DGAppORRejDate.ToDateString("yyyy-MM-dd"),
                        ProcurementProcessStartedDate = x.ProcurementProcessStartedDate.ToDateString("yyyy-MM-dd")

                    }).OrderBy(y => y.CreatedDate).ToList();
                rds.Value = lst;
                report.DataSources.Add(rds);
            }

            Byte[] mybytes = report.Render("PDF");

            Response.AppendHeader("content-disposition", "inline; filename=file.pdf");
            return new FileStreamResult(new MemoryStream(mybytes), "application/pdf");
        }


        //public ActionResult ProcurementReportsMenu(ReportParameterVM para)
        //{
        //    var CurUser = db.Users.Find(CurUserID);
        //    var CardPermission = CurUser.UserRoles.SelectMany(x => x.Role.RoleMenuAccesses).Where(y => y.Menu.ParentMenuID == 19).ToList().AsQueryable().Select(e => (new
        //    {
        //        MenuID = e.MenuID,
        //        ReportName = e.Menu.Text,
        //        Action = e.Menu.Area + "/" + e.Menu.Controller + "/" + e.Menu.Action,
        //        ParentMenuID = e.Menu.ParentMenuID,
        //        Description = e.Menu.Description,
        //        Icon = e.Menu.Reporttype.ToString(),
        //    })).ToList();

        //    ViewBag.cardPermission = CardPermission;
        //    return View();
        //}
    }
}
