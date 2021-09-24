using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class Role
    {
        public Role()
        {
            RoleMenuAccesses = new HashSet<RoleMenuAccess>();
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<RoleMenuAccess> RoleMenuAccesses { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
