using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace NIBM.Procurement.Areas.Admin.Models
{
    [Serializable]
    public class SubDepartmentVM : IModel<SubDepartment, SubDepartmentVM>
    {
        public SubDepartmentVM()
        {
            mappings = new ObjMappings<SubDepartment, SubDepartmentVM>();
            mappings.Add(x => x.Department.Description, x => x.DepartmentDesc);
        }
        public SubDepartmentVM(SubDepartment obj)
            : this()
        {
            this.SetEntity(obj);
        }
        public ObjMappings<SubDepartment, SubDepartmentVM> mappings { get; set; }

        public int SubDeptID { get; set; }
        public string Description { get; set; }
        public NIBM.Procurement.ActiveState Status { get; set; }
        [DisplayName("Department")]
        public Nullable<int> DeptID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] RowVersion { get; set; }
        [DisplayName("Department")]
        public string DepartmentDesc { get; set; }

        public virtual Department Department { get; set; }
    }
}