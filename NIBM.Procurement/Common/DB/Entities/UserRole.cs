using System;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class UserRole
    {
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
