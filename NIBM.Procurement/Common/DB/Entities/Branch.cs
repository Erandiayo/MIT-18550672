using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class Branch
    {
        public Branch()
        {
            BranchDepartments = new HashSet<BranchDepartment>();
            Users = new HashSet<User>();
        }

        public int BranchID { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
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

        public virtual ICollection<BranchDepartment> BranchDepartments { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
