using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class Vendor
    {
        public Vendor()
        {
            TenderVendors = new HashSet<TenderVendor>();
            ProcuremenetRequests = new HashSet<ProcuremenetRequest>();
        }

        public int VendorID { get; set; }
        public string NavVendorID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TelNo { get; set; }
        public string FaxNo { get; set; }
        public ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<ProcuremenetRequest> ProcuremenetRequests { get; set; }
        public virtual ICollection<TenderVendor> TenderVendors { get; set; }
    }
}
