using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class SubDepartment
    {
        public SubDepartment()
        {
            Employees = new HashSet<Employee>();
        }

        public int SubDeptID { get; set; }
        public string Description { get; set; }
        public ActiveState Status { get; set; }
        public int? DeptID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
