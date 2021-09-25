using NIBM.Procurement.Common;
using NIBM.Procurement;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NIBM.Procurement.Areas.Admin.Models
{
    [Serializable]
    public class EmployeeVM : IModel<Employee, EmployeeVM>
    {
        public EmployeeVM()
        {
            var yr = DateTime.Now.Year;

            mappings = new ObjMappings<Employee, EmployeeVM>();
            mappings.Add(x => x.Designation == null ? "" : x.Designation.Description, x => x.DesignationDesc);
            mappings.Add(x => x.BranchDepartment == null ? "" : x.BranchDepartment.Branch.Description, x => x.BranchDesc);
            mappings.Add(x => x.BranchDepartment == null ? "" : x.BranchDepartment.Department.Description, x => x.DepartmentDesc);
            mappings.Add(x => x.ImmediateSupervisor1 != null ? x.Supervisor1.EPFNo + " - " + x.Supervisor1.Title + ". " + x.Supervisor1.Initials + " " + x.Supervisor1.LastName : "", x => x.ImmediateSupervisor1Desc);
            mappings.Add(x => $"{x.Title.ToEnumChar("")}. {x.Initials.Trim()} {x.LastName}", x => x.SupervisorName);
            mappings.Add(x => x.EPFNo + " - " + x.Title.ToEnumChar("") + ". " + x.Initials.Trim() + " " + x.LastName, x => x.EmpDesc);

        }
        public EmployeeVM(Employee obj)
            : this()
        {
            this.SetEntity(obj);
        }

        public ObjMappings<Employee, EmployeeVM> mappings { get; set; }

        [DisplayName("Employee")]
        public int EmployeeID { get; set; }
        [DisplayName("EPF No")]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greator than zero")]
        public int EPFNo { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [Required,DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required,DisplayName("Full Name"), DataType(DataType.MultilineText)]
        public string FullName { get; set; }

        [DisplayName("Designation")]
        public int DesignationID { get; set; }
        [DisplayName("Branch")]
        public int BranchID { get; set; }
        [DisplayName("Department")]
        public int DepartmentID { get; set; }
        public NIBM.Procurement.Title Title { get; set; }
        [Required,DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public NIBM.Procurement.Gender Gender { get; set; }
        [Required]
        public string Initials { get; set; }
        public NIBM.Procurement.ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        [DisplayName("Designation")]
        public string DesignationDesc { get; set; }
        [DisplayName("Branch")]
        public string BranchDesc { get; set; }
        [DisplayName("Department")]
        public string DepartmentDesc { get; set; }

        [DisplayName("Employee")]
        public string EmpDesc { get; set; }

        public int RoleID { get; set; }
        [DisplayName("Immediate Supervisor")]
        public Nullable<int> ImmediateSupervisor1 { get; set; }
        public string ImmediateSupervisor1Desc { get; set; }
        [DisplayName("Sub Department")]
        public Nullable<int> SubDeptID { get; set; }
        [DisplayName("Sub Department")]
        public string SubDepartmentDesc { get; set; }
        public string SupervisorName { get; set; }
    }
}
