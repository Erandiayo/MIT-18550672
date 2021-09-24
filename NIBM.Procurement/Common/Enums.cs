using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NIBM.Procurement
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string HrUser = "HrUser";
        public const string ProgOfficeUser = "ProgOfficeUser";
        public const string LecturerUser = "LecturerUser";
        public const string AdminUser = "AdminUser";
        public const string FinanceUser = "FinanceUser";
        public const string ExamUser = "ExamUser";
    }

    public enum ActiveState
    {
        [Description("Active")]
        Active = 1,
        [Description("Inactive")]
        Inactive = 0
    }

    public enum Gender
    {
        [Description("Male")]
        Male = 0,
        [Description("Female")]
        Female = 1
    }

    public enum Title
    {
        [Description("Prof")]
        Prof = 1,
        [Description("Dr")]
        Dr = 2,
        [Description("Mr")]
        Mr = 3,
        [Description("Ms")]
        Ms = 4
    }

    public enum Feedback
    {
        [Description("None")]
        None = 0,
        [Description("Satisfied")]
        Satisfied = 1,
        [Description("Neutral")]
        Neutral = 2,
        [Description("Dissatisfied")]
        UnSatisfied = 3
    }
   
    public enum ProcurementReqStatus
    {
        [Description("Drafted")]
        Drafted = 0,
        [Description("Pending Approval")]
        PendingApproval = 1,
        [Description("Division Head Approved")]
        DivisionHeadApproved = 2,
        [Description("Division Head Rejected")]
        DivisionHeadRejected = 3,
        [Description("Procurement Deptartment Received")]
        ProcurementDeptReceived = 4,
        [Description("HR Approved")]
        HRApproved = 5,
        [Description("HR Rejected")]
        HRRejected = 6,
        [Description("HR Recommended")]
        HRRecommended = 7,
        [Description("DG Approved")]
        DGApproved = 8,
        [Description("DG Rejected")]
        DGRejected = 9,
        [Description("Incomplete")]
        Incomplete = 10,
        [Description("Re Opened")]
        ReOpened = 11,
        [Description("Req for Spec")]
        ReqforSpec = 12,
        [Description("Spec Recommended")]
        SpecRecommended = 13,
        [Description("Item Received")]
        ItemReceived = 14,
        [Description("Payment Complete")]
        PaymentComplete = 15
    }
    public enum TenderStatus
    {
        [Description("Tender Open")]
        TenderOpen = 0,
        [Description("TEC Assigned")]
        TECSelected = 1,
        [Description("TEC Recommend")]
        TECRecommend = 2,
        [Description("Board Approved")]
        BoardApproved = 3,
        [Description("Board Rejected")]
        BoardRejected = 4,
        [Description("Tender Closed")]
        TenderClosed = 5
    }
    public enum ProcurementProcessType
    {
        [Description("Completed By Advance")]
        CompletedByAdvance = 0,
        [Description("Completed By PettyCash")]
        CompletedByPettyCash = 1,
        [Description("Going through Procurement Process")]
        GoingthroughProcurementProcess = 2,
        [Description("Last 3 month process")]
        Last3monthprocess = 3
    }

    public enum NICType
    {
        [Description("Postal")]
        PostalID = 0,
        [Description("NIC")]
        NIC = 1,
        [Description("Passport")]
        PassportID = 2
    }
    public enum ReportType
    {
        [Description("Both")]
        Both = 0,
        [Description("PDF")]
        PDF = 1,
        [Description("Excel")]
        Excel = 2
    }
    public enum ReqItemCateory
    {
        [Description("Financial Services")]
        FinancialServices = 0,
        [Description("IT and Telecoms")]
        ITTelecoms = 1,
        [Description("Marketing ")]
        Marketing = 2,
        [Description("Furniture ")]
        Furniture = 3,
        [Description("Repairing ")]
        Repairing = 4,
        [Description("Stationary ")]
        Stationary = 5
    }

}
