using System;
using System.Collections.Generic;

namespace NIBM.Procurement.DB.Entities
{
    [Serializable]
    public partial class Employee
    {
        public Employee()
        {
            TECMembers = new HashSet<TECMember>();
            InverseImmediateSupervisor1Navigation = new HashSet<Employee>();
            ProcuremenetRequestsDivisionHeadNavigation = new HashSet<ProcuremenetRequest>();
            ProcuremenetRequestsReqByNavigation = new HashSet<ProcuremenetRequest>();
            Users = new HashSet<User>();
        }

        public int EmployeeID { get; set; }
        public int EPFNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Title Title { get; set; }
        public Gender Gender { get; set; }
        public string Initials { get; set; }
        public int DesignationID { get; set; }
        public int BranchID { get; set; }
        public int DepartmentID { get; set; }
        public ActiveState Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public int? ImmediateSupervisor1 { get; set; }

        public virtual BranchDepartment BranchDepartment { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual Employee Supervisor1 { get; set; }

        public virtual ICollection<TECMember> TECMembers { get; set; }
        public virtual ICollection<Employee> InverseImmediateSupervisor1Navigation { get; set; }
        public virtual ICollection<ProcuremenetRequest> ProcuremenetRequestsDivisionHeadNavigation { get; set; }
        public virtual ICollection<ProcuremenetRequest> ProcuremenetRequestsReqByNavigation { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
