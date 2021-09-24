using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NIBM.Procurement.Areas.Admin.Models
{
    [Serializable]
    public class DesignationVM : IModel<Designation, DesignationVM>
    {
        public DesignationVM()
        {
            mappings = new ObjMappings<Designation, DesignationVM>();
        }
        public DesignationVM(Designation obj)
            : this()
        {
            this.SetEntity(obj);
        }

        public ObjMappings<Designation, DesignationVM> mappings { get; set; }

        public int DesignationID { get; set; }
        [Required]
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
