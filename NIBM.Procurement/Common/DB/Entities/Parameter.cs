using System;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class Parameter
    {
        public int ParameterID { get; set; }
        public string ParameterCode { get; set; }
        public string ParameterValue { get; set; }
    }
}
