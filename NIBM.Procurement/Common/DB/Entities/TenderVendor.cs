using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class TenderVendor
    {
        public TenderVendor()
        {

        }

        public int TenderVendorID { get; set; }
        public int TenderID { get; set; }
        public int VendorID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Tender Tender { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
