using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NIBM.Procurement.Areas.Base.Models
{
    [Serializable]
    public class DashBoardVM
    {
        public bool HideMenu { get; set; } = false;
        public ChartConfig ProcurementRequest { get; set; }
        public ChartConfig MonthlyCompletion { get; set; }
        public ChartConfig RequestsByBranch { get; set; }
        public string DivApprovalPending { get; set; }
        public string ApprovalPending { get; set; }
        public string TotalRequests { get; set; }
        public string PendingFeedbacks { get; set; }
        public string PendingApproval { get; set; }
        public string OnProcess { get; set; }
        public string OnTenderProcess { get; set; }
        public string PendingCompletion { get; set; }



        
    }

    public class ChartConfig
    {
        public string type { get; set; }
        public object data { get; set; }
        public object options { get; set; }
        public object scales { get; set; }
    }
}