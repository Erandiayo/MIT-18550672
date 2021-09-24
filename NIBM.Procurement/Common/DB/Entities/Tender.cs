using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class Tender
    {
        public Tender()
        {
            TECMembers = new HashSet<TECMember>();
            TenderVendors = new HashSet<TenderVendor>();
            ProcuremenetRequests = new HashSet<ProcuremenetRequest>();
        }

        public int TenderID { get; set; }
        public DateTime TenderOpenedDate { get; set; }
        public DateTime TenderClosedDate { get; set; } 
        public DateTime? TecTeamAssigedOn { get; set; }
        public DateTime? TecApprovedDate { get; set; }
        public string TECReportAttachment { get; set; }
        public DateTime? TenderBoardApprovedDate { get; set; }
        public int? SelectedVendorId { get; set; }
        public DateTime? ItemReceivedByVendorOn { get; set; }
        public TenderStatus TenderStatus { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<ProcuremenetRequest> ProcuremenetRequests { get; set; }
        public virtual ICollection<TECMember> TECMembers { get; set; }
        public virtual ICollection<TenderVendor> TenderVendors { get; set; }
    }
}
