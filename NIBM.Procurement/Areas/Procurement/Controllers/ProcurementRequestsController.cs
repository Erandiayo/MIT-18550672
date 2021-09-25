using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WebForms;
using NIBM.Procurement.Areas.Base.Controllers;
using NIBM.Procurement.Areas.Base.Models;
using NIBM.Procurement.Areas.Procurement.Models;
using NIBM.Procurement.Common;
using NIBM.Procurement;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NIBM.Procurement.Controllers
{
    public class ProcurementRequestsController : BaseController
    {
        private const string ProcurementAttachmentsFolder = "ProcurementReqAttachments";

        public ActionResult Index(BaseViewModel<ProcuremenetRequestVM> vm)
        {
            var CurEmpID = db.Users.Find(CurUserID).EmployeeID;
            vm.SetList(db.ProcuremenetRequests.Where(x => x.ReqBy == CurEmpID).AsQueryable(), "ReqDate");
            return View(vm);
        }
        public ActionResult ApproveIndex(BaseViewModel<ProcuremenetRequestVM> vm)
        {
            var CurEmp = db.Users.Find(CurUserID);
            vm.SetList(db.ProcuremenetRequests.Where(e => e.Status == ProcurementReqStatus.PendingApproval)
                .AsQueryable(), "ReqDate");
            return View(vm);
        }

        public ActionResult Create()
        {
            var ProcurementRequests = new ProcuremenetRequestVM();
            if (Session[sskCrtdObj] is ProcuremenetRequestVM)
            {
                var temp = (ProcuremenetRequestVM)Session[sskCrtdObj];
                if (temp.Base64FileContent != null)
                { ProcurementRequests = temp; }
            }

            ProcurementRequests.ReqDate = DateTime.Now;
            var Employee = db.Users.Find(CurUserID).Employee;
            ProcurementRequests.RequestBY = Employee.Title.ToString() + ". " + Employee.Initials.Trim() + " " + Employee.LastName;
            var SupervisorID = Employee.ImmediateSupervisor1;
            ProcurementRequests.DivisionHead = (int)SupervisorID;
            Session[sskCrtdObj] = ProcurementRequests;
            return View(ProcurementRequests);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ProReqID,RequestBY,ReqDate,ReqSubject,Description,DivisionHead,DivisionalHead,FileName,RowVersion")] ProcuremenetRequestVM procuremenetRequest, HttpPostedFileBase file)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var obj = Session.GetObject<ProcuremenetRequestVM>();
                    if (file != null)
                    {
                        byte[] bytes;
                        using (var ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            bytes = ms.ToArray();
                        }

                        obj.Base64FileContent = Convert.ToBase64String(bytes);
                        obj.FileName = file.FileName;
                        procuremenetRequest.FileName = obj.FileName;
                    }

                    if (procuremenetRequest.ReqSubject == null)
                    { ModelState.AddModelError("ReqSubject", "Subject is required."); }
                    if (procuremenetRequest.Description == null)
                    { ModelState.AddModelError("Description", "Description is required."); }
                    if (obj.FileName != null && procuremenetRequest.ReqSubject == null || obj.FileName != null && procuremenetRequest.Description == null)
                    { ModelState.AddModelError("", "Please select file again."); }
                    if (obj.ProcumentReqItemsDetails.Count() == 0 && file == null)
                    { ModelState.AddModelError("", "Attachment or item list is required to make the request."); }
                    ModelState["UserFeedback"].Errors.Clear();

                    if (ModelState.IsValid)
                    {
                        var empID = db.Users.Find(CurUserID).EmployeeID;
                        procuremenetRequest.CreatedBy = this.GetCurrUser();
                        procuremenetRequest.CreatedDate = DateTime.Now;
                        procuremenetRequest.ReqBy = (int)empID;
                        var objprocurement = db.ProcuremenetRequests.Add(procuremenetRequest.GetEntity()).Entity;

                        if (!obj.Base64FileContent.IsBlank())
                        {
                            byte[] binData = Convert.FromBase64String(obj.Base64FileContent);
                            var filePath = $"{obj.ProReqID}-{obj.FileName}";

                            GraphApiHelper.SaveProcurementAttachment(binData, obj.ProReqID, filePath, ProcurementAttachmentsFolder);
                            objprocurement.AttachmentLink = GraphApiHelper.GetProcurementAttachment(obj.ProReqID, filePath, ProcurementAttachmentsFolder);
                        }

                        db.SaveChanges();

                        foreach (var det in obj.ProcumentReqItemsDetails)
                        {
                            det.ProReqId = objprocurement.ProReqID;
                            det.CreatedBy = this.GetCurrUser();
                            det.CreatedDate = DateTime.Now;
                            objprocurement.ProcurementReqItems.Add(det.GetEntity());
                        }
                        db.SaveChanges();
                        dbTransaction.Commit();
                        Session.Remove(sskCrtdObj);
                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Procurement Requests created successfully.");
                        return RedirectToAction("Details", new { id = objprocurement.ProReqID, TempNo = 0, tile = false });
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    this.ShowEntityErrors(dbEx);
                    dbTransaction.Rollback();
                }
                catch (Exception ex)
                {
                    AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
                    dbTransaction.Rollback();
                }
            }
            return View(procuremenetRequest);
        }


        public ActionResult Details(int? id, int TempNo, bool tile = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcuremenetRequest ProcuremenetRequests = db.ProcuremenetRequests.Find(id);
            if (ProcuremenetRequests == null)
            {
                return HttpNotFound();
            }

            var obj = new ProcuremenetRequestVM(ProcuremenetRequests);
            var ReqByID = (int)obj.ReqBy;
            var Emp = db.Employees.AsQueryable();
            obj.RequestBY = Emp.Where(x => x.EmployeeID == ReqByID).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.HrRejectedName = Emp.Where(x => x.EmployeeID == 14).Select(x => x.Title.ToEnumChar("") + ". " + x.Initials.Trim() + " " + x.LastName).FirstOrDefault();
            obj.DGInitials = Emp.Where(x => x.EmployeeID == 115).Select(x => x.Title.ToEnumChar("") + ". " + x.Initials.Trim() + " " + x.LastName).FirstOrDefault();
            obj.ApprovedOrRejectByName = (obj.DivHead.Title.ToString() + ". " + obj.DivHead.Initials.Trim() + " " + obj.DivHead.LastName);
            obj.DivisionalHead = obj.DivHeadName;
            obj.IsTile = tile;
            obj.TempNo = TempNo;
            obj.CompletedDate = obj.CompletedDate != null ? obj.CompletedDate.Value.Date : DateTime.Now;
            Session.SetObject(obj);
            return View(obj);
        }

        public ActionResult ApproveDetails(int? id, bool tile = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcuremenetRequest ProcuremenetRequests = db.ProcuremenetRequests.Find(id);
            if (ProcuremenetRequests == null)
            {
                return HttpNotFound();
            }

            var obj = new ProcuremenetRequestVM(ProcuremenetRequests);
            var requestBy = (int)obj.ReqBy;
            obj.RequestBY = db.Employees.Where(x => x.EmployeeID == requestBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.ApprovedOrRejectByName = db.Employees.Where(x => x.EmployeeID == obj.DivisionHead).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.DivisionalHead = obj.ApprovedOrRejectByName;
            obj.IsTile = tile;
            return View(obj);
        }

        [HttpPost]
        public ActionResult Approve(ProcuremenetRequestVM procuremenetRequestVM)
        {
            try
            {
                var obj = db.ProcuremenetRequests.Find(procuremenetRequestVM.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }


                var lst = new List<ProcurementReqStatus> { ProcurementReqStatus.PendingApproval, ProcurementReqStatus.HRRecommended, ProcurementReqStatus.ProcurementDeptReceived };

                if (obj.Status == ProcurementReqStatus.PendingApproval)
                {
                    obj.Status = ProcurementReqStatus.DivisionHeadApproved;
                    obj.SupervisorComment = procuremenetRequestVM.Comments;
                    obj.DivHeadAppORRejDate = DateTime.Now;
                }

                else if (obj.Status == ProcurementReqStatus.ProcurementDeptReceived)
                {
                    obj.Status = ProcurementReqStatus.HRApproved;
                    obj.HRAppRecommendORRejDate = DateTime.Now;
                    obj.SupervisorComment = procuremenetRequestVM.Comments;
                }

                else if (obj.Status == ProcurementReqStatus.HRRecommended)
                {
                    obj.Status = ProcurementReqStatus.DGApproved;
                    obj.SupervisorComment = procuremenetRequestVM.Comments;
                    obj.DGAppORRejDate = DateTime.Now;
                }

               
               
                    obj.ModifiedBy = this.GetCurrUser();
                    obj.ModifiedDate = DateTime.Now;
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Selected request(s) approved successfully.");
                     return RedirectToAction("Requests");

            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ApproveIndex"); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ApproveDetails", new { id = procuremenetRequestVM.ProReqID });

        }

        public ICollection<ProcuremenetRequestVM> ProcurementJson(string dataJson)
        {
            ICollection<ProcuremenetRequestVM> lstProcReq = new List<ProcuremenetRequestVM>();
            try
            {
                var lst = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(dataJson);

                ProcuremenetRequestVM obj;
                foreach (var det in lst)
                {
                    obj = new ProcuremenetRequestVM();
                    obj.ProReqID = det["ProReqID"].ConvertTo<int>();
                    obj.ValId = det["ValId"].ConvertTo<int>();
                    if (det["Comments"].ToString() != null)
                    { obj.Comments = det["Comments"].ToString(); }

                    if (obj.ProReqID != 0)
                    {
                        lstProcReq.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return lstProcReq;
            }

            return lstProcReq;
        }

        public ActionResult Reject(ProcuremenetRequestVM procuremenetRequestVM)
        {
            try
            {
                var obj = db.ProcuremenetRequests.Find(procuremenetRequestVM.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }

                if (obj.Status == ProcurementReqStatus.HRRecommended)
                {
                    obj.Status = ProcurementReqStatus.DGRejected;
                    obj.DGAppORRejDate = DateTime.Now;
                }

                else if (obj.Status == ProcurementReqStatus.ProcurementDeptReceived)
                {
                    obj.Status = ProcurementReqStatus.HRRejected;
                    obj.HRAppRecommendORRejDate = DateTime.Now;
                }

                else
                {
                    obj.Status = ProcurementReqStatus.DivisionHeadRejected;
                    obj.DivHeadAppORRejDate = DateTime.Now;
                }

                obj.ModifiedBy = this.GetCurrUser();
                obj.SupervisorComment = procuremenetRequestVM.SupervisorComment;
                obj.ModifiedDate = DateTime.Now;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, "Request rejected successfully.");
                
                return RedirectToAction("Requests", "ApproveRequest"); 

            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("ApproveIndex"); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("ApproveDetails", new { id = procuremenetRequestVM.ProReqID });
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(ProcuremenetRequestVM procuremenetRequest)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                    if (obj == null)
                    { throw new DbUpdateConcurrencyException(""); }
                    db.Detach(obj);

                    var entry = db.Entry(procuremenetRequest.GetEntity());
                    entry.State = EntityState.Unchanged;
                    entry.Collection(x => x.ProcurementReqItems).Load();
                    db.ProcurementReqItems.RemoveRange(entry.Entity.ProcurementReqItems);
                    entry.State = EntityState.Deleted;
                    db.SaveChanges();
                    dbTransaction.Commit();
                    AddAlert(AlertStyles.success, "Requests deleted successfully.");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.ShowConcurrencyErrors(ex, true);
                    if (ex.Message == "")
                    { return RedirectToAction("Index"); }
                    dbTransaction.Rollback();
                }
                catch (Exception ex)
                {
                    AddAlert(AlertStyles.danger, ex.GetInnerException().Message);
                    dbTransaction.Rollback();
                }
            }
            return RedirectToAction("Details", new { id = procuremenetRequest.ProReqID, TempNo = 0, tile = false });
        }


        [HttpPost, ActionName("ChildDelete")]
        public ActionResult ChildDeleteConfirmed(int id)
        {
            string msg = string.Empty;
            try
            {
                var objSession = Session.GetObject<ProcuremenetRequestVM>();
                var lst = objSession.ProcumentReqItemsDetails;
                var obj = lst.FirstOrDefault(x => x.ProReqItemID == id);
                lst.Remove(obj);
                Session.SetObject(objSession);

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Item successfully removed.");
            }
            catch (Exception ex)
            {
                msg = ex.GetInnerException().Message;
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, msg);
            }
            string url = "";
            if (msg.IsBlank())
            { url = Url.Action("ChildIndex", new { isToEdit = true }); }
            return Json(new { success = true, url = url, msg });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcuremenetRequest ProcuremenetRequests = db.ProcuremenetRequests.Find(id);
            if (ProcuremenetRequests == null)
            {
                return HttpNotFound();
            }
            var obj = new ProcuremenetRequestVM(ProcuremenetRequests);
            var RequestID = (int)ProcuremenetRequests.ReqBy;
            var requestedby = db.Employees.Where(x => x.EmployeeID == RequestID).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.RequestBY = requestedby;
            obj.DivisionHead = ProcuremenetRequests.DivisionHead;
            Session.SetObject(obj);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ProReqID,ProReqItemID,RequestBY,ReqDate,ReqSubject,Description,DivisionHead,DivisionalHead,ItemDesc,ReqQty,RowVersion")] ProcuremenetRequestVM procuremenetRequest, HttpPostedFileBase file)
        {
            byte[] curRowVersion = null;
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sessionProcurement = Session.GetObject<ProcuremenetRequestVM>();
                    if (file != null)
                    {
                        byte[] bytes;
                        using (var ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            bytes = ms.ToArray();
                        }

                        sessionProcurement.Base64FileContent = Convert.ToBase64String(bytes);
                        sessionProcurement.FileName = file.FileName;
                        procuremenetRequest.FileName = sessionProcurement.FileName;
                    }

                    var attachmentlink = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID).AttachmentLink;
                    if (procuremenetRequest.ReqSubject == null)
                    { ModelState.AddModelError("ReqSubject", "Subject is required."); }
                    if (procuremenetRequest.Description == null)
                    { ModelState.AddModelError("Description", "Description is required."); }
                    if (sessionProcurement.ProcumentReqItemsDetails.Count() == 0 && attachmentlink == null)
                    { ModelState.AddModelError("", "Attachment or item list is required to make the request."); }
                    ModelState["UserFeedback"].Errors.Clear();

                    if (ModelState.IsValid)
                    {
                        var empID = db.Users.Find(CurUserID).EmployeeID;
                        procuremenetRequest.ReqBy = (int)empID;
                        var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                        if (obj == null)
                        { throw new DbUpdateConcurrencyException(); }

                        curRowVersion = obj.RowVersion;

                        if (!sessionProcurement.Base64FileContent.IsBlank())
                        {
                            byte[] binData = Convert.FromBase64String(sessionProcurement.Base64FileContent);
                            var filePath = $"{sessionProcurement.ProReqID}-{sessionProcurement.FileName}";

                            GraphApiHelper.SaveProcurementAttachment(binData, obj.ProReqID, filePath, ProcurementAttachmentsFolder);
                            obj.AttachmentLink = GraphApiHelper.GetProcurementAttachment(obj.ProReqID, filePath, ProcurementAttachmentsFolder);
                        }

                        db.ProcurementReqItems.RemoveRange(obj.ProcurementReqItems.Where(x =>
                            !sessionProcurement.ProcumentReqItemsDetails.Select(y => y.ProReqItemID).ToList().Contains(x.ProReqItemID)));

                        if (obj.Status.In(ProcurementReqStatus.DGRejected, ProcurementReqStatus.DivisionHeadRejected, ProcurementReqStatus.Incomplete, ProcurementReqStatus.HRRejected))
                        { obj.Status = ProcurementReqStatus.ReOpened; }

                        foreach (var det in sessionProcurement.ProcumentReqItemsDetails)
                        {
                            var objDet = db.ProcurementReqItems.Find(det.ProReqItemID);
                            if (objDet == null)
                            {
                                det.CreatedBy = this.GetCurrUser();
                                det.CreatedDate = DateTime.Now;
                                obj.ProcurementReqItems.Add(det.GetEntity());
                            }
                            else
                            {
                                var modObjDet = det.GetEntity();
                                modObjDet.CopyContent(objDet, "ItemDesc,ReqQty");

                                objDet.ModifiedBy = this.GetCurrUser();
                                objDet.ModifiedDate = DateTime.Now;
                            }
                        }

                        obj.ModifiedBy = this.GetCurrUser();
                        obj.ModifiedDate = DateTime.Now;
                        var modObj = procuremenetRequest.GetEntity();
                        modObj.CopyContent(obj, "ReqBy,ReqDate,ReqSubject,Description,DivisionHead");

                        db.SaveChanges();
                        dbTransaction.Commit();
                        AddAlert(AlertStyles.success, "Procurement Requests modified successfully.");
                        return RedirectToAction("Details", new { id = procuremenetRequest.ProReqID, TempNo = 0, tile = false });
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.ShowConcurrencyErrors(ex);
                    procuremenetRequest.RowVersion = curRowVersion;
                    dbTransaction.Rollback();
                }
                catch (DbEntityValidationException dbEx)
                { this.ShowEntityErrors(dbEx); dbTransaction.Rollback(); }
                catch (Exception ex)
                { AddAlert(AlertStyles.danger, ex.GetInnerException().Message); dbTransaction.Rollback(); }
            }
            return View(procuremenetRequest);
        }

        public ActionResult ChildIndex(int? id, bool isToEdit = false)
        {
            ProcuremenetRequestVM obj;

            if (isToEdit && Session[sskCrtdObj] is ProcuremenetRequestVM)
            {
                obj = (ProcuremenetRequestVM)Session[sskCrtdObj];
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProcuremenetRequest ProcuReqItm = db.ProcuremenetRequests.Where(x => x.ProReqID == id).FirstOrDefault();
                if (ProcuReqItm == null)
                {
                    return HttpNotFound();
                }
                obj = new ProcuremenetRequestVM(ProcuReqItm);
            }

            ViewBag.IsToEdit = isToEdit;
            ViewBag.ProReqID = obj.ProReqID;
            return PartialView("_ChildIndex", obj.ProcumentReqItemsDetails);
        }


        public ActionResult ChildCreate(int? proReqID)
        {
            if (proReqID != 0)
            {
                if (proReqID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProcuremenetRequest procuremenetRequest = db.ProcuremenetRequests.Find(proReqID);
                if (procuremenetRequest == null)
                {
                    return HttpNotFound();
                }
            }
            var obj = Session.GetObject<ProcuremenetRequestVM>();
            var ProcurementDetail = new ProcurementReqItemsVM() { ProReqId = proReqID.Value };
            return PartialView("_ChildCreate", ProcurementDetail);
        }

        [HttpPost]
        public ActionResult ChildCreate([Bind(Include = "ProReqItemID,ProReqId,ItemDesc,ReqQty")] ProcurementReqItemsVM procurementReqItems)
        {
            ProcuremenetRequestVM obj;
            try
            {
                obj = Session.GetObject<ProcuremenetRequestVM>();
                if (procurementReqItems.ItemDesc == null)
                { ModelState.AddModelError("ItemDesc", "Item Description is required."); }
                if (procurementReqItems.ReqQty == 0)
                { ModelState.AddModelError("ReqQty", "Quantity is required."); }

                if (ModelState.IsValid)
                {
                    procurementReqItems.CreatedBy = this.GetCurrUser();
                    procurementReqItems.CreatedDate = DateTime.Now;
                    procurementReqItems.ProReqItemID = Math.Min(obj.ProcumentReqItemsDetails.Select(x => x.ProReqItemID).MinOrDefault(), 0) - 1;
                    obj.ProcumentReqItemsDetails.Add(procurementReqItems);
                    Session.SetObject(obj);
                    AddAlert(AlertStyles.success, "Item added successfully.");
                    string url = Url.Action("ChildIndex", new { id = procurementReqItems.ProReqId, isToEdit = true });
                    return Json(new { success = true, url = url });

                }

            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(AlertStyles.danger, ex.GetInnerException().Message); }


            return PartialView("_ChildCreate", procurementReqItems);

        }

        [HttpPost]
        public ActionResult ChildEdit([Bind(Include = "ProReqItemID,ProReqId,ItemDesc,ReqQty")] ProcurementReqItemsVM procurementReqItems)
        {
            try
            {
                var objSession = (Session.GetObject<ProcuremenetRequestVM>());

                if (procurementReqItems.ItemDesc == null)
                { ModelState.AddModelError("ItemDesc", "Item Description is required."); }
                if (procurementReqItems.ReqQty == 0)
                { ModelState.AddModelError("ReqQty", "Quantity should is required."); }

                if (ModelState.IsValid)
                {
                    var obj = objSession.ProcumentReqItemsDetails.FirstOrDefault(x => x.ProReqItemID == procurementReqItems.ProReqItemID);
                    procurementReqItems.CopyContent(obj, "ItemDesc,ReqQty");
                    Session.SetObject(objSession);

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Request modified successfully.");
                    string url = Url.Action("ChildIndex", new { id = procurementReqItems.ProReqId, isToEdit = true });
                    return Json(new { success = true, url = url });
                }
            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return PartialView("_ChildEdit", procurementReqItems);
        }


        public ActionResult ChildEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcurementReqItemsVM procurementReqItems = (Session.GetObject<ProcuremenetRequestVM>()).ProcumentReqItemsDetails.FirstOrDefault(x => x.ProReqItemID == id);
            if (procurementReqItems == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ChildEdit", procurementReqItems);
        }

        public ActionResult SendForApproval(ProcuremenetRequestVM procuremenetRequest)
        {
            var objSession = Session.GetObject<ProcuremenetRequestVM>();
            try
            {
                var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }

                var lst = new List<ProcurementReqStatus> { ProcurementReqStatus.Drafted, ProcurementReqStatus.DivisionHeadRejected, ProcurementReqStatus.Incomplete, ProcurementReqStatus.HRRejected, ProcurementReqStatus.DGRejected, ProcurementReqStatus.ReOpened };

                if (lst.Contains(obj.Status))
                { obj.Status = ProcurementReqStatus.PendingApproval; }
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Request successfully sent for approval.");

                if (procuremenetRequest.IsTile)
                { return RedirectToAction("Home", new { area = "Base", controller = "DashBoard" }); }
                else
                { return RedirectToAction("Index"); }

            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                {
                    if (procuremenetRequest.IsTile)
                    { return RedirectToAction("Home", new { area = "Base", controller = "DashBoard" }); }
                    else
                    { return RedirectToAction("Index"); }
                }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            if (procuremenetRequest.IsTile)
            { return RedirectToAction("Index", new { area = "Base", controller = "DashBoard" }); }
            else
            { return RedirectToAction("Details", new { id = procuremenetRequest.ProReqID, TempNo = 0, tile = false }); }
        }

        [HttpPost]
        public ActionResult UploadAttachment(HttpPostedFileBase file)
        {
            ProcuremenetRequestVM procuremenetRequest = new ProcuremenetRequestVM();
            try
            {
                if (file != null)
                {
                    var obj = Session.GetObject<ProcuremenetRequestVM>();
                    procuremenetRequest.ProReqID = obj.ProReqID;

                    byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        bytes = ms.ToArray();
                    }

                    obj.Base64FileContent = Convert.ToBase64String(bytes);
                    obj.FileName = file.FileName;

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Attachment added successfully.");
                    return RedirectToAction("Create", new { id = obj.ProReqID });
                }

            }
            catch (DbEntityValidationException dbEx)
            { this.ShowEntityErrors(dbEx); }
            catch (Exception ex)
            { AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message); }

            return RedirectToAction("Details", new { id = procuremenetRequest.ProReqID, TempNo = 0, tile = false });
        }

        public ActionResult FeedbackEdit(int? id, bool tile = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcuremenetRequest ProcuremenetRequests = db.ProcuremenetRequests.Find(id);
            if (ProcuremenetRequests == null)
            {
                return HttpNotFound();
            }

            var obj = new ProcuremenetRequestVM(ProcuremenetRequests);
            var ReqByID = (int)obj.ReqBy;
            obj.RequestBY = db.Employees.Where(x => x.EmployeeID == ReqByID).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault();
            obj.ApprovedOrRejectByName = (obj.DivHead.Title.ToString() + ". " + obj.DivHead.Initials.Trim() + " " + obj.DivHead.LastName);
            obj.DivisionalHead = obj.DivHeadName;
            obj.IsTile = tile;
            obj.CompletedDate = obj.CompletedDate != null ? obj.CompletedDate.Value.Date : DateTime.Now;
            Session.SetObject(obj);
            return View(obj);
        }

        [HttpPost]
        public ActionResult FeedbackEdit(ProcuremenetRequestVM procuremenetRequest)
        {
            var objSession = Session.GetObject<ProcuremenetRequestVM>();
            try
            {
                if (procuremenetRequest.UserFeedback == null)
                { ModelState.AddModelError("UserFeedback", "UserFeedback is required."); }

                if (ModelState.IsValid)
                {
                    var obj = db.ProcuremenetRequests.Find(procuremenetRequest.ProReqID);
                    if (obj == null)
                    { throw new DbUpdateConcurrencyException(""); }

                    obj.UserFeedback = procuremenetRequest.UserFeedback;
                    db.SaveChanges();

                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Feedback saved successfully.");
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("Index"); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("FeedbackEdit", new { id = procuremenetRequest.ProReqID, TempNo = 2, tile = false });
        }

        public ICollection<ProcuremenetRequestVM> ProcCompleteJson(string dataJson)
        {
            ICollection<ProcuremenetRequestVM> lstProcReq = new List<ProcuremenetRequestVM>();
            try
            {
                var lst = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(dataJson);

                ProcuremenetRequestVM obj;
                foreach (var det in lst)
                {
                    obj = new ProcuremenetRequestVM();
                    obj.ProReqID = det["ProReqID"].ConvertTo<int>();
                    obj.Comments = det["Comments"].ToString();

                    if (obj.ProReqID != 0)
                    {
                        lstProcReq.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return lstProcReq;
            }

            return lstProcReq;
        }

        public ActionResult Print(int? id)
        {
            //var para = (ProcuremenetRequestVM)Session[sskCrtdObj];

            var proREq = db.ProcuremenetRequests.Where(x => x.ProReqID == id).AsQueryable();
            var proItems = db.ProcurementReqItems.Where(x => x.ProReqId == id).AsQueryable();
            //var proItem =  

            LocalReport report = new LocalReport();
            report.ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/ProcurementRequestReport.rdlc");

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsProcuremntRequestSummary";
            report.EnableHyperlinks = true;
            if (proItems.Count() > 0)
            {
                var lst = proItems.ToList()
                    .Select(x => new
                    {
                        Branch = x.ProReq.Employee.BranchDepartment.Branch.Description,
                        Department = x.ProReq.Employee.BranchDepartment.Department.Description,
                        CreatedDate = x.CreatedDate.ToString("yyyy-MM-dd"),
                        ReqBy = db.Employees.Where(z => z.EmployeeID == x.ProReq.ReqBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault(),
                        ReqSubject = x.ProReq.ReqSubject,
                        x.ProReq.Description,
                        DivisionHead = db.Employees.Where(z => z.EmployeeID == x.ProReq.DivisionHead).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName + "  - " + y.Designation.Description).FirstOrDefault(),
                        ProcDeptReceivedDate = x.ProReq.ProcDeptReceivedDate,
                        Status = x.ProReq.Status.ToEnumChar(),
                        Comments = x.ProReq.Comments != null ? x.ProReq.Comments : "",
                        ItemDesc = x.ItemDesc,
                        ReqQty = x.ReqQty,
                        DivHeadAppORRejDate = x.ProReq.DivHeadAppORRejDate,
                        AttachmentUrl = x.ProReq.AttachmentLink != null ? x.ProReq.AttachmentLink : "",
                        HRAppRecommendORRejDate = x.ProReq.HRAppRecommendORRejDate.ToDateString("yyyy-MM-dd"),
                        DGAppORRejDate = x.ProReq.DGAppORRejDate.ToDateString("yyyy-MM-dd"),
                        ProcurementProcessStartedDate = x.ProReq.ProcurementProcessStartedDate.ToDateString("yyyy-MM-dd"),

                    }).OrderBy(y => y.CreatedDate).ToList();
                rds.Value = lst;
                report.DataSources.Add(rds);
            }

            else
            {
                var lst = proREq.ToList()
                    .Select(x => new
                    {
                        Branch = x.Employee.BranchDepartment.Branch.Description,
                        Department = x.Employee.BranchDepartment.Department.Description,
                        CreatedDate = x.CreatedDate.ToString("yyyy-MM-dd"),
                        ReqBy = db.Employees.Where(z => z.EmployeeID == x.ReqBy).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault(),
                        ReqSubject = x.ReqSubject,
                        x.Description,
                        DivisionHead = db.Employees.Where(z => z.EmployeeID == x.DivisionHead).Select(y => y.Title.ToString() + ". " + y.Initials.Trim() + " " + y.LastName).FirstOrDefault(),
                        ProcDeptReceivedDate = x.ProcDeptReceivedDate,
                        Status = x.Status.ToEnumChar(),
                        Comments = x.Comments != null ? x.Comments : "",
                        DivHeadAppORRejDate = x.DivHeadAppORRejDate,
                        AttachmentUrl = x.AttachmentLink != null ? x.AttachmentLink : "",
                        HRAppRecommendORRejDate = x.HRAppRecommendORRejDate.ToDateString("yyyy-MM-dd"),
                        DGAppORRejDate = x.DGAppORRejDate.ToDateString("yyyy-MM-dd"),
                        ProcurementProcessStartedDate = x.ProcurementProcessStartedDate.ToDateString("yyyy-MM-dd")

                    }).OrderBy(y => y.CreatedDate).ToList();
                rds.Value = lst;
                report.DataSources.Add(rds);
            }

            Byte[] mybytes = report.Render("PDF");

            Response.AppendHeader("content-disposition", "inline; filename=file.pdf");
            return new FileStreamResult(new MemoryStream(mybytes), "application/pdf");
        }

        public ActionResult PendingFeedbacks()
        {
            var CurUser = db.Users.Find(CurUserID);
            var CurEmpId = CurUser.EmployeeID;

            var reqList = new List<ProcuremenetRequestVM>();
            reqList = db.ProcuremenetRequests.Where(x => CurEmpId.HasValue && x.ReqBy == CurEmpId && x.Status == ProcurementReqStatus.PaymentComplete && x.UserFeedback == null).AsEnumerable()
                        .Select(x => new ProcuremenetRequestVM(x)).OrderByDescending(x => x.CompletedDate).ToList();
            return View(reqList);
        }

        [HttpPost]
        public ActionResult AddFeedbacks(ProcuremenetRequestVM procuremenetRequestVM)
        {
            try
            {
                var obj = db.ProcuremenetRequests.Find(procuremenetRequestVM.ProReqID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }

                obj.ModifiedBy = this.GetCurrUser();
                obj.ModifiedDate = DateTime.Now;
                obj.UserFeedback = procuremenetRequestVM.UserFeedback;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Feedback is added to selected request successfully.");
                return RedirectToAction("PendingFeedbacks");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("PendingFeedbacks"); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("PendingFeedbacks");

        }

        public ICollection<ProcuremenetRequestVM> FeedbacksJson(string dataJson)
        {
            ICollection<ProcuremenetRequestVM> lstProcReq = new List<ProcuremenetRequestVM>();
            try
            {
                var lst = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(dataJson);

                ProcuremenetRequestVM obj;
                foreach (var det in lst)
                {
                    obj = new ProcuremenetRequestVM();
                    obj.ProReqID = det["ProReqID"].ConvertTo<int>();
                    if (det["Comments"].ToString() != null)
                    { obj.UserFeedback = det["Comments"].ToString(); }

                    if (obj.ProReqID != 0)
                    {
                        lstProcReq.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return lstProcReq;
            }

            return lstProcReq;
        }

        [HttpPost]
        public ActionResult AddFeedbacksSelected([Bind(Include = "StringPara1")] ProcuremenetRequestVM procuremenetRequestVM)
        {
            try
            {
                var selectedReq = FeedbacksJson(procuremenetRequestVM.StringPara1);
                if (selectedReq.Count() > 0)
                {
                    foreach (var item in selectedReq)
                    {
                        var obj = db.ProcuremenetRequests.Find(item.ProReqID);
                        if (obj == null)
                        { throw new DbUpdateConcurrencyException(""); }

                        obj.ModifiedBy = this.GetCurrUser();
                        obj.ModifiedDate = DateTime.Now;
                        obj.UserFeedback = item.UserFeedback;
                        db.SaveChanges();
                    }
                    AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Feedback(s) is added to Selected request(s) successfully.");
                }
                else
                {
                    AddAlert(NIBM.Procurement.Common.AlertStyles.danger, "Please select request(s) to add feedback(s).");
                }
                return RedirectToAction("PendingFeedbacks");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.ShowConcurrencyErrors(ex, true);
                if (ex.Message == "")
                { return RedirectToAction("PendingFeedbacks"); }
            }
            catch (Exception ex)
            {
                AddAlert(NIBM.Procurement.Common.AlertStyles.danger, ex.GetInnerException().Message);
            }
            return RedirectToAction("PendingFeedbacks");

        }
    }
}
