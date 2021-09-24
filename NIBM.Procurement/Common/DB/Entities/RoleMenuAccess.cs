using System;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class RoleMenuAccess
    {
        public int RoleID { get; set; }
        public int MenuID { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Role Role { get; set; }
    }
}
