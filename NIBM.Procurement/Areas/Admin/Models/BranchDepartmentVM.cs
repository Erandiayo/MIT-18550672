using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NIBM.Procurement.Areas.Admin.Models
{
    [Serializable]
    public class BranchDepartmentVM : IModel<BranchDepartment, BranchDepartmentVM>
    {
        public BranchDepartmentVM()
        {
            mappings = new ObjMappings<BranchDepartment, BranchDepartmentVM>();
            mappings.Add(x=> x.Branch.Description, x=> x.BranchDesc);
            mappings.Add(x=> x.Department.Description , x=> x.DepartmentDesc);
        }
        public BranchDepartmentVM(BranchDepartment obj)
            : this()
        {
            this.SetEntity(obj);
        }

        public ObjMappings<BranchDepartment, BranchDepartmentVM> mappings { get; set; }
        
        [DisplayName("Branch"), Required]
        public int BranchID { get; set; }
        [DisplayName("Department"), Required]
        public int DepartmentID { get; set; }

        [DisplayName("Telephone 1")]
        [RegularExpression(@"^(0\d{9})$", ErrorMessage = "Invalid Number")]
        public string TelNo_1 { get; set; }
        [DisplayName("Telephone 2")]
        [RegularExpression(@"^(0\d{9})$", ErrorMessage = "Invalid Number")]
        public string TelNo_2 { get; set; }
        [DisplayName("Fax 1")]
        public string FaxNo_1 { get; set; }
        [DisplayName("Fax 2")]
        public string FaxNo_2 { get; set; }
        public NIBM.Procurement.ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        [DisplayName("Branch")]
        public string BranchDesc { get; set; }
        [DisplayName("Department")]
        public string DepartmentDesc { get; set; }
    }
}
