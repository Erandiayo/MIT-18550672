using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class ProcuremenetRequest
    {
        public ProcuremenetRequest()
        {
            ProcurementReqItems = new HashSet<ProcurementReqItem>();
        }

        public int ProReqID { get; set; }
        public DateTime ReqDate { get; set; }
        public int ReqBy { get; set; }
        public string Description { get; set; }
        public ReqItemCateory ReqItemCateory { get; set; }
        public string ReqSubject { get; set; }
        public int DivisionHead { get; set; }
        public string AttachmentLink { get; set; }
        public string RecommenedSpecification { get; set; }
        public int? SpecRecomnedBy { get; set; }
        public DateTime? SpecRequestedOn { get; set; }
        public DateTime? SpecRecomnedOn { get; set; }
        public int? TenderID { get; set; }
        public ProcurementReqStatus Status { get; set; }
        public DateTime? DivHeadAppORRejDate { get; set; }
        public DateTime? DGAppORRejDate { get; set; }
        public DateTime? HRAppRecommendORRejDate { get; set; }       
        public DateTime? ProcDeptReceivedDate { get; set; }
        public ProcurementProcessType? ProcessType { get; set; }
        public int? AwardedVendorId { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string SupervisorComment { get; set; }   
        public DateTime? ProcurementProcessStartedDate { get; set; }
        public decimal? AmountPurchased { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string UserFeedback { get; set; }


        public virtual Tender Tender { get; set; }
        public virtual Employee DivHead { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<ProcurementReqItem> ProcurementReqItems { get; set; }
    }
}
