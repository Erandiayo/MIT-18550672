using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class Department
    {
        public Department()
        {
            BranchDepartments = new HashSet<BranchDepartment>();
            SubDepartments = new HashSet<SubDepartment>();
            Users = new HashSet<User>();
        }

        public int DepartmentID { get; set; }
        public string Description { get; set; }
        public ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<BranchDepartment> BranchDepartments { get; set; }
        public virtual ICollection<SubDepartment> SubDepartments { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
