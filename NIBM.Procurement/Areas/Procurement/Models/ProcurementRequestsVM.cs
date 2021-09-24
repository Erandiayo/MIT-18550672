using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NIBM.Procurement.Areas.Procurement.Models
{

    [Serializable]
    public class ProcuremenetRequestVM : IModel<ProcuremenetRequest, ProcuremenetRequestVM>
    {
        public ProcuremenetRequestVM()
        {
            ProcumentReqItemsDetails = new List<ProcurementReqItemsVM>();
            mappings = new ObjMappings<ProcuremenetRequest, ProcuremenetRequestVM>();
            mappings.Add(x => x.ProcurementReqItems.Select(y => new ProcurementReqItemsVM(y)).ToList(), x => x.ProcumentReqItemsDetails);
            mappings.Add(x => x.Employee.ImmediateSupervisor1, x => x.DivisionHead);
            mappings.Add(x => x.Employee.Title + " " + x.Employee.Initials + " " + x.Employee.LastName, x => x.EmpName);
            mappings.Add(x => x.DivHead.Title + " " + x.DivHead.Initials + " " + x.DivHead.LastName, x => x.DivHeadName);
            mappings.Add(x => x.Employee.BranchDepartment.Department.Description, x => x.FromDepart);
            mappings.Add(x => x.Employee.BranchDepartment.Branch.Description, x => x.FromBranch);
            mappings.Add(x => x.Vendor.Name, x => x.AwardedVendor);
            mappings.Add(x => x.Tender.TenderOpenedDate, x => x.TenderOpenedDate);
            mappings.Add(x => x.Tender.TenderClosedDate, x => x.TenderClosedDate);
            mappings.Add(x => x.Tender.TecApprovedDate, x => x.TecApprovedDate);
            mappings.Add(x => x.Tender.TenderBoardApprovedDate, x => x.TenderBoardApprovedDate);
        }

        public ProcuremenetRequestVM(ProcuremenetRequest obj)
              : this()
        {
            this.SetEntity(obj);
        }
        public ObjMappings<ProcuremenetRequest, ProcuremenetRequestVM> mappings { get; set; }
        public int ProReqID { get; set; }
        public DateTime? FromDate { get; set; }
        [DisplayName("Requested Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime ReqDate { get; set; }
        [DisplayName("Request By")]
        public int ReqBy { get; set; }
        [DisplayName("Request in Brief"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("Subject"), DataType(DataType.MultilineText)]
        public string ReqSubject { get; set; }
        [DisplayName("immediate Supervisor ")]
        public int DivisionHead { get; set; }
        [DisplayName("Attachment")]
        public string AttachmentLink { get; set; }
        public string Base64FileContent { get; set; }
        public NIBM.Procurement.ProcurementReqStatus Status { get; set; }
        [DisplayName("Approved Or Rejected Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DivHeadAppORRejDate { get; set; }
        [DisplayName(" DG Approved Or Rejected Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DGAppORRejDate { get; set; }
        [DisplayName("HR Recommand Or Approved Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> HRAppRecommendORRejDate { get; set; }
        [DisplayName("Comments"), DataType(DataType.MultilineText)]
        public string Comments { get; set; }
        [DisplayName("Supervisor Comment"), DataType(DataType.MultilineText)]
        public string SupervisorComment { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public byte[] RowVersion { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ProcDeptReceivedDate { get; set; }
        [DisplayName("Process Type")]
        public Nullable<NIBM.Procurement.ProcurementProcessType> ProcessType { get; set; }
        [DisplayName("Division Head")]
        public string DivisionalHead { get; set; }
        [DisplayName("Employee Name")]
        public string EmpName { get; set; }
        [DisplayName("Division Head")]
        public string DivHeadName { get; set; }
        [DisplayName("Item Description")]
        public string ItemDesc { get; set; }
        [DisplayName("Attachment (If available)")]
        public string Attachment { get; set; }
        [DisplayName("Requested By")]
        public string RequestBY { get; set; }
        public string RequestedBy { get; set; }
        public string ApprovedOrRejectByName { get; set; }
        public bool IsTile { get; set; }
        public string FileName { get; set; }
        [DisplayName("To Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }
        public int TempNo { get; set; }
        [DisplayName("Approved Or Rejected Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CompletedDate { get; set; }
        [Required, DisplayName("User Feedback"), DataType(DataType.MultilineText)]
        public string UserFeedback { get; set; }
        public string FromDepart { get; set; }
        public string FromBranch { get; set; }
        public string HrRejectedName { get; set; }
        public string DGInitials { get; set; }
        public string StringPara1 { get; set; }
        public int ValId { get; set; }
        [DisplayName("Spec Requesting From")]
        public int? SpecRecomnedBy { get; set; }
        public string RecommenedSpecification { get; set; }
        [DisplayName("Spec Requested On")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SpecRequestedOn { get; set; }
        [DisplayName("Spec Received On")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SpecRecomnedOn { get; set; }
        [DisplayName("Spec Requesting From")]
        public string SpecRequstedFromName { get; set; }
        [DisplayName("Past Tender")]
        public int? TenderID { get; set; }
        [DisplayName("Past Tender")]
        public string TenderDesc { get; set; }
        public int? AwardedVendorId { get; set; }
        [DisplayName("Awarded Vendor")]
        public string AwardedVendor { get; set; }
        public ReqItemCateory ReqItemCateory { get; set; }
        //public int? ReqNowithin3Months { get; set; }
        public DateTime? ProcurementProcessStartedDate { get; set; }
        [DisplayName("Amount Purchased")]
        public decimal? AmountPurchased { get; set; }
        [DisplayName("Tender Opened Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TenderOpenedDate { get; set; }
        [DisplayName("Tender Closed Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TenderClosedDate { get; set; }
        [DisplayName("TEC Approved Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TecApprovedDate { get; set; }
        [DisplayName("Tender Board Approved Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TenderBoardApprovedDate { get; set; }

        public ICollection<ProcurementReqItemsVM> ProcumentReqItemsDetails { get; set; }
        public virtual ICollection<ProcurementReqItem> ProcurementReqItems { get; set; }
        public virtual Employee DivHead { get; set; }
        public virtual Tender Tender { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Vendor Vendor { get; set; }

    }
}