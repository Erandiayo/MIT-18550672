using System;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class ProcurementReqItem
    {
        public int ProReqItemID { get; set; }
        public int ProReqId { get; set; }
        public string ItemDesc { get; set; }
        public decimal ReqQty { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] RowVersion { get; set; }
        public int? ItemCategoryId { get; set; }

        public virtual ProcuremenetRequest ProReq { get; set; }
    }
}
