using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace NIBM.Procurement.Areas.Procurement.Models
{
    [Serializable]
    public class VendorVM : IModel<Vendor, VendorVM>
    {
        public VendorVM()
        {
            mappings = new ObjMappings<Vendor, VendorVM>();
        }



        public VendorVM(Vendor obj)
              : this()
        {
            this.SetEntity(obj);
        }
        public ObjMappings<Vendor, VendorVM> mappings { get; set; }



        public int VendorID { get; set; }
        [DisplayName("Nav Code")]
        public string NavVendorID { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
        [DisplayName("Address")]
        [Required(ErrorMessage = "Address cannot be empty")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [DisplayName("Telephone No")]
        [Required(ErrorMessage = "Telephone No cannot be empty")]
        [RegularExpression(@"^(0\d{9})$", ErrorMessage = "Invalid Number")]
        public string TelNo { get; set; }
        [DisplayName("Fax No")]
        public string FaxNo { get; set; }
        public ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ProcuremenetRequest ProcuremenetRequest { get; set; }
    }
}