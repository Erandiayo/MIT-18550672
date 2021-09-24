using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NIBM.Procurement.Areas.Admin.Models
{
    [Serializable]
    public class DepartmentVM : IModel<Department, DepartmentVM>
    {
        public DepartmentVM()
        {
            mappings = new ObjMappings<Department, DepartmentVM>();
        }
        public DepartmentVM(Department obj)
            : this()
        {
            this.SetEntity(obj);
        }

        public ObjMappings<Department, DepartmentVM> mappings { get; set; }

        public int DepartmentID { get; set; }
        [Required]
        public string Description { get; set; }
        public NIBM.Procurement.ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        [DisplayName("Is Academic")]
        public bool IsAcademic { get; set; }
    }
}
