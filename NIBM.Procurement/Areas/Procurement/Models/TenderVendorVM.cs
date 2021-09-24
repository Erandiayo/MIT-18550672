using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NIBM.Procurement.Areas.Procurement.Models
{
    public class TenderVendorVM : IModel<TenderVendor, TenderVendorVM>
    {
        public TenderVendorVM()
        {
            mappings = new ObjMappings<TenderVendor, TenderVendorVM>();
            mappings.Add(x => x.Vendor.Name, x => x.Name);
            mappings.Add(x => x.Vendor.Address, x => x.Address);
            mappings.Add(x => x.Vendor.TelNo, x => x.TelNo);
        }

        public TenderVendorVM(TenderVendor obj)
              : this()
        {
            this.SetEntity(obj);
        }
        public ObjMappings<TenderVendor, TenderVendorVM> mappings { get; set; }

        public int TenderVendorID { get; set; }
        public int TenderID { get; set; }
        [DisplayName("Vendor")]
        public int VendorID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        [DisplayName("Telephone No")]
        public string TelNo { get; set; }

        public virtual Tender Tender { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}