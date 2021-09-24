using NIBM.Procurement.Common;
using NIBM.Procurement;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NIBM.Procurement.Areas.Admin.Models
{
    [Serializable]
    public class BranchVM : IModel<Branch, BranchVM>
    {
        public BranchVM()
        {
            mappings = new ObjMappings<Branch, BranchVM>();
        }
        public BranchVM(Branch obj)
            : this()
        {
            this.SetEntity(obj);
        }

        public ObjMappings<Branch, BranchVM> mappings { get; set; }

        public int BranchID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [DisplayName("Telephone 1")]
        [Required]
        [RegularExpression(@"^(0\d{9})$", ErrorMessage = "Invalid Number")]
        public string TelNo_1 { get; set; }
        [DisplayName("Telephone 2")]
        [RegularExpression(@"^(0\d{9})$", ErrorMessage = "Invalid Number")]
        public string TelNo_2 { get; set; }
        [DisplayName("Fax 1")]
        public string FaxNo_1 { get; set; }
        [DisplayName("Fax 2")]
        public string FaxNo_2 { get; set; }
        public ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<BranchDepartment> BranchDepartments { get; set; }

    }
}
