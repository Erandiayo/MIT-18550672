using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class BranchDepartment
    {
        public BranchDepartment()
        {
            Employees = new HashSet<Employee>();
        }

        public int BranchID { get; set; }
        public int DepartmentID { get; set; }
        public string TelNo_1 { get; set; }
        public string TelNo_2 { get; set; }
        public string FaxNo_1 { get; set; }
        public string FaxNo_2 { get; set; }
        public ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
