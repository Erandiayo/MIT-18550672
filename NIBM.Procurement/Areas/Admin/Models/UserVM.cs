using NIBM.Procurement.Common;
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
    public class UserVM : IModel<User, UserVM>
    {
        public UserVM()
        {
            DetailsList = new List<UserRoleVM>();
            mappings = new ObjMappings<User, UserVM>();
            mappings.Add(x => x.Employee == null ? x.UserName : x.Employee.FirstName + " " + x.Employee.LastName , x => x.ShortName);
            mappings.Add(x => x.Employee == null ? "-" : x.Employee.EPFNo + " - " + x.Employee.Title.ToEnumChar("") + ". " + x.Employee.Initials.Trim() + " " + x.Employee.LastName, x => x.EmpDspStr);
            mappings.Add(x => x.UserRoles.Select(y => new UserRoleVM(y)).ToList(), x => x.DetailsList);
            mappings.Add(x => x.Password, x => x.Password.Encrypt());
            mappings.Add(x => x.Password.Decrypt(), x => x.Password);
            mappings.Add(x => x.Password.Decrypt(), x => x.Password);
            mappings.Add(x => x.Branch == null ? x.Employee.BranchID : x.Branch.BranchID, x => x.BranchID);
            mappings.Add(x => x.Department == null ? x.Employee.DepartmentID : x.Department.DepartmentID, x => x.DepartmentID);
            mappings.Add(x => x.Branch == null ? x.Employee.BranchDepartment.Branch.Description : x.Branch.BranchDepartments.FirstOrDefault().Branch.Description, x => x.BranchDesc);
            mappings.Add(x => x.Department == null ? x.Employee.BranchDepartment.Department.Description : x.Department.BranchDepartments.FirstOrDefault().Department.Description, x => x.DepartmentDesc);
        }
        public UserVM(User obj)
            : this()
        {
            this.SetEntity(obj);
        }

        public ObjMappings<User, UserVM> mappings { get; set; }

        public int UserID { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [PasswordPropertyText(true)]
        public string Password { get; set; }
        [DisplayName("Employee")]
        public Nullable<int> EmployeeID { get; set; }
        public NIBM.Procurement.ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public string ShortName { get; set; }

        [DisplayName("Employee")]
        public string EmpDspStr { get; set; }

        [DisplayName("Branch")]
        public Nullable<int> BranchID { get; set; }
        [DisplayName("Department")]
        public Nullable<int> DepartmentID { get; set; }
        [DisplayName("Branch")]
        public string BranchDesc { get; set; }
        [DisplayName("Department")]
        public string DepartmentDesc { get; set; }
        public virtual ICollection<UserRoleVM> DetailsList { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Department Department { get; set; }
    }
}
