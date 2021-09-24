using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.ComponentModel;
using System.Web;

namespace NIBM.Procurement.Areas.Admin.Models
{
    [Serializable]
    public class UserRoleVM : IModel<UserRole, UserRoleVM>
    {
        public UserRoleVM()
        {
            mappings = new ObjMappings<UserRole, UserRoleVM>();
            mappings.Add(x => x.Role == null ? "-" : x.Role.Name, x => x.RoleName);
        }
        public UserRoleVM(UserRole obj)
            : this()
        {
            this.SetEntity(obj);
        }

        public ObjMappings<UserRole, UserRoleVM> mappings { get; set; }

        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        [DisplayName("Role")]
        public int RoleID { get; set; }

        [DisplayName("Role")]
        public string RoleName { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
