using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class Menu
    {
        public Menu()
        {
            InverseParentMenu = new HashSet<Menu>();
            RoleMenuAccesses = new HashSet<RoleMenuAccess>();
        }

        public int MenuID { get; set; }
        public int? ParentMenuID { get; set; }
        public int DisplaySeq { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public bool IsHide { get; set; }
        public ReportType Reporttype { get; set; }


        public virtual Menu ParentMenu { get; set; }
        public virtual ICollection<Menu> InverseParentMenu { get; set; }
        public virtual ICollection<RoleMenuAccess> RoleMenuAccesses { get; set; }
    }
}
