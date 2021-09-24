using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.ComponentModel;
using System.Web;

namespace NIBM.Procurement.Areas.Procurement.Models
{
    [Serializable]
    public class ProcurementReqItemsVM : IModel<ProcurementReqItem, ProcurementReqItemsVM>
    {
        public ProcurementReqItemsVM()
        {
            mappings = new ObjMappings<ProcurementReqItem, ProcurementReqItemsVM>();

        }

        public ProcurementReqItemsVM(ProcurementReqItem obj)
              : this()
        {
            this.SetEntity(obj);
        }
        public ObjMappings<ProcurementReqItem, ProcurementReqItemsVM> mappings { get; set; }

        public int ProReqItemID { get; set; }
        public int ProReqId { get; set; }
        [DisplayName("Item Description")]
        public string ItemDesc { get; set; }
        [DisplayName("Quantity")]
        public decimal ReqQty { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ProcuremenetRequest ProcuremenetRequest { get; set; }
    }
}
