using NIBM.Procurement;
using NIBM.Procurement.DB;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace NIBM.Procurement.Areas.Base.Controllers
{
    public class DataController : Controller
    {
        private ActionResult GetDataPaginated<T>(IQueryable<T> qry, string sortBy = null, bool inReverse = false, int startIndex = 0, int pageSize = 5, Dictionary<string, string> lstSortColMap = null, Func<T, object> selFunc = null)
        {
            int rowCount = qry.Count();
            if (pageSize <= 0)
            {
                pageSize = 10;
                startIndex = 0;
            }

            if (startIndex > rowCount)
            { startIndex = 0; }

            var qrySortBy = (lstSortColMap ?? new Dictionary<string, string>()).Where(x => x.Key == sortBy).Select(x => x.Value).FirstOrDefault() ?? sortBy;

            qry = qry.OrderBy("(" + qrySortBy + ")" + (inReverse ? " DESC" : "")).Skip(startIndex);

            if (pageSize > 0)
            { qry = qry.Take(pageSize); }

            var data = qry.ToList().Select(selFunc ?? (x => x)).ToList();

            var obj = new { RowCount = rowCount, SortBy = sortBy, InReverse = inReverse, Data = data };
            return Json(obj);
        }

        public ActionResult GetEmployees(string filter = null, string sortBy = null, bool inReverse = false, int startIndex = 0, int pageSize = 5
            , bool searchForKey = false, int branchID = 0, int departmentID = 0, bool HeadOfDivision = false, int curEmpSupID = 0, List<int> idsToExcluede = null)
        {
            using (var dbctx = new dbNIBMContext())
            {
                var qry = dbctx.Employees.Where(x => x.Status == ActiveState.Active).AsQueryable();

                if (departmentID != 0 && branchID != 0)
                {
                    qry = qry.Where(e => e.BranchID == branchID && e.DepartmentID == departmentID).AsQueryable();
                }
                else if (branchID != 0)
                {
                    qry = qry.Where(e => e.BranchID == branchID).AsQueryable();
                }

                if (HeadOfDivision)
                {
                    List<Employee> qryList = new List<Employee>();

                    var immediateSup = qry.Where(y => y.ImmediateSupervisor1 != null).Select(x => x.ImmediateSupervisor1).Distinct().ToList();
                    qry = qry.Where(x => immediateSup.Contains(x.EmployeeID));
                    sortBy = "EPFNo";
                }


                if (!filter.IsBlank())
                {
                    qry = qry.ToList().AsQueryable().Where("EmployeeID.ToString().Contains(@0)" + (searchForKey ? "" : @" ||
                        (FirstName != null && FirstName.ToLower().Contains(@0)) ||
                        (MiddleName != null && MiddleName.ToLower().Contains(@0)) ||
                        (LastName != null && LastName.ToLower().Contains(@0)) ||
                        (EPFNo != null && EPFNo.ToString().ToLower().Contains(@0))"), filter.ToLower());
                }

                if (idsToExcluede != null)
                {
                    foreach (var id in idsToExcluede)
                    { qry = qry.Where("EmployeeID != @0", id); }
                }

                if (sortBy.IsBlank())
                { sortBy = "First_Name"; }

                var lstSortColMap = new Dictionary<string, string>()
                {
                    { "Employee_ID","EmployeeID" } ,
                    { "Full_Name", "EPFNo.ToString()+Initials+LastName" },
                    { "First_Name", "FirstName" }
                };

                return GetDataPaginated(qry, sortBy, inReverse, startIndex, pageSize, lstSortColMap,
                    x => new
                    {
                        Employee_ID = x.EmployeeID,
                        Full_Name = x.EPFNo + " - " + x.Title + ". " + x.Initials.Trim() + " " + x.LastName,
                        First_Name = x.FirstName
                    });
            }
        }

        public ActionResult GetBranches(string filter = null, string sortBy = null, bool inReverse = false, int startIndex = 0, int pageSize = 5, bool searchForKey = false, bool fromBranchDept = false)
        {
            using (var dbctx = new dbNIBMContext())
            {
                var qry = dbctx.Branches.Where(x => x.Status == ActiveState.Active).AsQueryable();

                if (fromBranchDept)
                { qry = qry.Join(dbctx.BranchDepartments, x => x.BranchID, x => x.BranchID, (x, y) => x).Distinct(); }

                if (!filter.IsBlank())
                { qry = qry.Where("BranchID.ToString().Contains(@0)" + (searchForKey ? "" : " || Description.ToLower().Contains(@0)"), filter.ToLower()); }

                if (sortBy.IsBlank())
                { sortBy = "Description"; }

                var lstSortColMap = new Dictionary<string, string>()
                {
                    { "Branch_ID","BranchID" }
                };

                return GetDataPaginated(qry, sortBy, inReverse, startIndex, pageSize, lstSortColMap,
                    x => new
                    {
                        Branch_ID = x.BranchID,
                        x.Description,
                        x.Address
                    });
            }
        }

        public ActionResult GetDepartments(string filter = null, string sortBy = null, bool inReverse = false, int startIndex = 0, int pageSize = 5, bool searchForKey = false, bool fromBranchDept = false, int? branchID = null)
        {
            using (var dbctx = new dbNIBMContext())
            {
                var qry = dbctx.Departments.Where(x => x.Status == ActiveState.Active).AsQueryable();

                if (fromBranchDept)
                {
                    var brdepts = dbctx.BranchDepartments.AsQueryable();
                    if (branchID.HasValue)
                    { brdepts = brdepts.Where(x => x.BranchID == branchID && x.Department.Status == ActiveState.Active); }

                    qry = qry.Join(brdepts, x => x.DepartmentID, x => x.DepartmentID, (x, y) => x).Distinct();
                }

                if (!filter.IsBlank())
                { qry = qry.Where("DepartmentID.ToString().Contains(@0)" + (searchForKey ? "" : " || Description.ToLower().Contains(@0)"), filter.ToLower()); }

                if (sortBy.IsBlank())
                { sortBy = "Description"; }

                var lstSortColMap = new Dictionary<string, string>()
                {
                    { "Department_ID","DepartmentID" }
                };

                return GetDataPaginated(qry, sortBy, inReverse, startIndex, pageSize, lstSortColMap,
                    x => new
                    {
                        Department_ID = x.DepartmentID,
                        x.Description
                    });
            }
        }

        public ActionResult GetUserRoles(string filter = null, string sortBy = null, bool inReverse = false, int startIndex = 0, int pageSize = 5, bool searchForKey = false, List<int> idsToExcluede = null)
        {
            using (var dbctx = new dbNIBMContext())
            {
                var qry = dbctx.Roles.AsQueryable();

                if (!filter.IsBlank())
                { qry = qry.Where(searchForKey ? "RoleID.ToString().Contains(@0)" : "Name.ToLower().Contains(@0)", filter.ToLower()); }
                if (idsToExcluede != null)
                {
                    foreach (var id in idsToExcluede)
                    { qry = qry.Where("RoleID != @0", id); }
                }

                int rowCount = qry.Count();
                if (pageSize <= 0)
                {
                    pageSize = 10;
                    startIndex = 0;
                }

                if (startIndex > rowCount)
                { startIndex = 0; }

                if (sortBy.IsBlank())
                { sortBy = "Name"; }

                var lstSortColMap = new Dictionary<string, string>()
                {
                    { "Role_ID", "RoleID" },
                    { "Role_Name", "Name" }
                };

                return GetDataPaginated(qry, sortBy, inReverse, startIndex, pageSize, lstSortColMap,
                    x => new
                    {
                        Role_ID = x.RoleID,
                        Role_Name = x.Name
                    });
            }
        }

        public ActionResult GetDesignations(string filter = null, string sortBy = null, bool inReverse = false, int startIndex = 0, int pageSize = 5, bool searchForKey = false)
        {
            using (var dbctx = new dbNIBMContext())
            {
                var qry = dbctx.Designations.AsQueryable();

                if (!filter.IsBlank())
                { qry = qry.Where("DesignationID.ToString().Contains(@0)" + (searchForKey ? "" : " || Description.ToLower().Contains(@0)"), filter.ToLower()); }

                if (sortBy.IsBlank())
                { sortBy = "Description"; }

                var lstSortColMap = new Dictionary<string, string>()
                {
                    { "Designation_ID","DesignationID" }
                };

                return GetDataPaginated(qry, sortBy, inReverse, startIndex, pageSize, lstSortColMap,
                    x => new
                    {
                        Designation_ID = x.DesignationID,
                        x.Description
                    });
            }
        }

        public ActionResult GetTenders(string filter = null, string sortBy = null, bool inReverse = false, int startIndex = 0, int pageSize = 5, bool searchForKey = false)
        {
            using (var dbctx = new dbNIBMContext())
            {
                var qry = dbctx.Tenders.Where(x => x.TenderStatus == TenderStatus.BoardApproved && x.ProcuremenetRequests.Count > 0).Select(x => new
                {
                    x.TenderID,
                    ReqSubject = x.ProcuremenetRequests.FirstOrDefault().ReqSubject,
                    Description = x.ProcuremenetRequests.FirstOrDefault().Description,
                    TenderBoardApprovedDate = x.TenderBoardApprovedDate == null ? "" : x.TenderBoardApprovedDate.Value.ToString("yyyy-MM-dd"),
                    AwardedTo = x.SelectedVendorId == null ? "" : dbctx.Vendors.Where(y => y.VendorID == x.SelectedVendorId.Value).FirstOrDefault().Name,
                    x.TenderStatus,
                    AmountPurchased = x.ProcuremenetRequests.FirstOrDefault().AmountPurchased
                }).AsQueryable();

                if (!filter.IsBlank())
                { qry = qry.Where("TenderID.ToString().Contains(@0)" + (searchForKey ? "" : " || TenderBoardApprovedDate.Contains(@0) || ReqSubject.Contains(@0) || Description.ToLower().Contains(@0)" +
                    " || AwardedTo.ToLower().Contains(@0) || TenderStatus.ToEnumChar().ToLower().Contains(@0) || TenderStatus.ToEnumChar().ToLower().Contains(@0)"), filter.ToLower()); }

                if (sortBy.IsBlank())
                { sortBy = "ReqSubject"; }

                var lstSortColMap = new Dictionary<string, string>()
                {
                    { "Tender_ID","TenderID" },
                    { "Request_Subject","ReqSubject" },
                    { "Board_Approved_On","TenderBoardApprovedDate" },
                    { "Description","Description" },
                    { "Awarded_To","AwardedTo" },
                    { "Status","TenderStatus.ToEnumChar()" },
                    { "Amount","AmountPurchased" }
                };

                return GetDataPaginated(qry, sortBy, inReverse, startIndex, pageSize, lstSortColMap,
                    x => new
                    {
                        Tender_ID = x.TenderID,
                        Request_Subject = x.ReqSubject,
                        x.Description,
                        Board_Approved_On = x.TenderBoardApprovedDate,
                        Awarded_To = x.AwardedTo,
                        Status = x.TenderStatus.ToEnumChar(),
                        Amount = x.AmountPurchased
                    });
            }
        }


        public ActionResult GetVendors(string filter = null, string sortBy = null, bool inReverse = false, int startIndex = 0, int pageSize = 5, bool searchForKey = false, int? tenderID = null)
        {
            using (var dbctx = new dbNIBMContext())
            {
                var qry = dbctx.Vendors.Where(x => x.Status == ActiveState.Active).AsQueryable();

                if (tenderID.HasValue)
                { qry = qry.Where(x => x.TenderVendors.Where(y => y.TenderID == tenderID).Count() > 0); }

                if (!filter.IsBlank())
                { qry = qry.Where("VendorID.ToString().Contains(@0)" + (searchForKey ? "" : " || TelNo.ToString().ToLower().Contains(@0) || Name.ToLower().Contains(@0)"), filter.ToLower()); }

                if (sortBy.IsBlank())
                { sortBy = "Vendor_Name"; }

                var lstSortColMap = new Dictionary<string, string>()
                {
                    { "Vendor_Name","Name" }
                };

                return GetDataPaginated(qry, sortBy, inReverse, startIndex, pageSize, lstSortColMap,
                    x => new
                    {
                        x.VendorID,
                        Vendor_Name = x.Name,
                        Address = x.Address,
                        Contact_No = x.TelNo
                    });
            }
        }

        public ActionResult GetProcRequests(string filter = null, string sortBy = null, bool inReverse = false, int startIndex = 0, int pageSize = 5, bool searchForKey = false, int? tenderID = null, bool tenderAvailable = false)
        {
            using (var dbctx = new dbNIBMContext())
            {
                var qry = dbctx.ProcuremenetRequests.AsQueryable();

                if (tenderAvailable)
                { qry = qry.Where(x => x.Tender != null); }

                if (!filter.IsBlank())
                { qry = qry.Where("ProReqID.ToString().Contains(@0)" + (searchForKey ? "" : " || TelNo.ToString().ToLower().Contains(@0) || Name.ToLower().Contains(@0)"), filter.ToLower()); }

                if (sortBy.IsBlank())
                { sortBy = "Request_Date"; }

                var lstSortColMap = new Dictionary<string, string>()
                {
                    { "ProReqID","ProReqID" },
                    { "Request_Date","ReqDate" },
                    { "Request_Subject","ReqSubject" },
                    { "Request_By","ReqBy" },
                };

                return GetDataPaginated(qry, sortBy, inReverse, startIndex, pageSize, lstSortColMap,
                    x => new
                    {
                        x.ProReqID,
                        Request_Date = x.ReqDate.ToString("yyyy-MM-dd"),
                        Request_Subject = x.ReqSubject,
                        Request_By = x.ReqBy
                    });
            }
        }
    }
}