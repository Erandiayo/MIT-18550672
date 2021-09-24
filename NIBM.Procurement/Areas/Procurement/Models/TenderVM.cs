using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace NIBM.Procurement.Areas.Procurement.Models
{
    [Serializable]
    public class TenderVM : IModel<Tender, TenderVM>
    {
        public TenderVM()
        {
            TenderVendors = new List<TenderVendorVM>();
            TECMembers = new List<TecMemberVM>();


            mappings = new ObjMappings<Tender, TenderVM>();
            mappings.Add(x => x.TECMembers.Select(y => new TecMemberVM(y)).ToList(), y => y.TECMembers);
            mappings.Add(x => x.TenderVendors.Select(y => new TenderVendorVM(y)).ToList(), y => y.TenderVendors);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().Employee.Title + " " + x.ProcuremenetRequests.FirstOrDefault().Employee.Initials + " " + x.ProcuremenetRequests.FirstOrDefault().Employee.LastName, x => x.ReqByName);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().DivHead.Title+ " " + x.ProcuremenetRequests.FirstOrDefault().DivHead.Initials + " " + " " + x.ProcuremenetRequests.FirstOrDefault().DivHead.LastName, x => x.DivHeadName);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().Employee.BranchDepartment.Department.Description, x => x.FromDepart);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().Employee.BranchDepartment.Branch.Description, x => x.FromBranch);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().Employee.ImmediateSupervisor1, x => x.DivisionHead);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().ReqDate, x => x.ReqDate);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().CompletedDate, x => x.CompletedDate);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().Comments, x => x.Comments);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().ReqSubject, x => x.ReqSubject);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().UserFeedback, x => x.UserFeedback);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().DGAppORRejDate, x => x.DGAppORRejDate);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().Description, x => x.Description);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().AttachmentLink, x => x.AttachmentLink);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().RecommenedSpecification, x => x.RecommenedSpecification);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().CompletedDate, x => x.CompletedDate);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().ReqDate, x => x.ReqDate);
            mappings.Add(x => x.ProcuremenetRequests.FirstOrDefault().ProReqID, x => x.ProcReqId);
        }

        public TenderVM(Tender obj)
              : this()
        {
            this.SetEntity(obj);
        }
        public ObjMappings<Tender, TenderVM> mappings { get; set; }


        public int TenderID { get; set; }
        public int ProcReqId { get; set; }
        [DisplayName("Tender Open Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TenderOpenedDate { get; set; }
        [DisplayName("Tender Closed Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TenderClosedDate { get; set; }
        [DisplayName("TEC Team Assiged On")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TecTeamAssigedOn { get; set; }
        [DisplayName("TEC Approved Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TecApprovedDate { get; set; }
        [DisplayName("TEC Report Attachment")]
        public string TECReportAttachment { get; set; }
        [DisplayName("Tender Board Approved Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TenderBoardApprovedDate { get; set; }
        [DisplayName("Selected Vendor")]
        public int? SelectedVendorId { get; set; }
        [DisplayName("Item Received By Vendor On")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ItemReceivedByVendorOn { get; set; }
        public TenderStatus TenderStatus { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        [DisplayName("Requested By")]
        public string ReqByName { get; set; }
        [DisplayName("Division Head")]
        public string DivHeadName { get; set; }
        public string FromDepart { get; set; }
        public string FromBranch { get; set; }
        [DisplayName("immediate Supervisor ")]
        public int DivisionHead { get; set; }
        [DisplayName("Requested Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime ReqDate { get; set; }
        [DisplayName("Approved Or Rejected Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CompletedDate { get; set; }
        [DisplayName("Comments"), DataType(DataType.MultilineText)]
        public string Comments { get; set; }
        [DisplayName("Subject"), DataType(DataType.MultilineText)]
        public string ReqSubject { get; set; }
        [DisplayName("User Feedback"), DataType(DataType.MultilineText)]
        public string UserFeedback { get; set; }

        [DisplayName(" DG Approved Or Rejected Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DGAppORRejDate { get; set; }
        public string DGInitials { get; set; }
        public string ApprovedOrRejectByName { get; set; }
        public bool IsTile { get; set; }
        public string FileName { get; set; }
        public string Base64FileContent { get; set; }
        public int TempNo { get; set; }
        public string Description { get; set; }
        [DisplayName("Attachment")]
        public string AttachmentLink { get; set; }
        [DisplayName("Recommened Specification")]
        public string RecommenedSpecification { get; set; }
        public virtual ICollection<TenderVendorVM> TenderVendors { get; set; }
        public virtual ProcuremenetRequest ProcuremenetRequest { get; set; }
        public ICollection <TecMemberVM> TECMembers { get; set; }
       
    }
}