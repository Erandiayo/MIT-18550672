using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.ComponentModel;
using System.Web;



namespace NIBM.Procurement.Areas.Procurement.Models
{
    public class TecMemberVM : IModel<TECMember, TecMemberVM>
    {
        public TecMemberVM()
        {
            mappings = new ObjMappings<TECMember, TecMemberVM>();
            mappings.Add(x => x.Employee.Title + " " + x.Employee.Initials + " " + x.Employee.LastName, x => x.EmpName);
            mappings.Add(x => x.Employee.Designation.Description, x => x.DesignationDesc);
            mappings.Add(x => x.Employee.BranchDepartment.Branch.Description, x => x.BranchDesc);





        }



        public TecMemberVM(TECMember obj)
              : this()
        {
            this.SetEntity(obj);
        }
        public ObjMappings<TECMember, TecMemberVM> mappings { get; set; }


        public int TECMemberID { get; set; }
        public int ProcReqId { get; set; }
        [DisplayName("Member")]
        public int EmployeeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }

        [DisplayName("Member")]
        public string EmpName { get; set; }
        [DisplayName("Designation")]
        public string DesignationDesc { get; set; }
        [DisplayName("Branch")]
        public string BranchDesc { get; set; }


        public virtual Tender Tender { get; set; }

        public virtual Employee Employee { get; set; }
    }
}