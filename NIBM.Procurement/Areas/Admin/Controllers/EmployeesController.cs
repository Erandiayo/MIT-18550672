using Microsoft.EntityFrameworkCore;
using NIBM.Procurement.Areas.Admin.Models;
using NIBM.Procurement.Areas.Base.Controllers;
using NIBM.Procurement.Areas.Base.Models;
using NIBM.Procurement.Common;
using NIBM.Procurement.DB.Entities;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NIBM.Procurement.Areas.Admin.Controllers
{
    public class EmployeesController : BaseController
    {
        public ActionResult Index(BaseViewModel<EmployeeVM> vm)
        {
            var qry = db.Employees.AsQueryable();
            vm.SetList(qry, "EPFNo");
            return View(vm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(new EmployeeVM(employee));
        }

        public ActionResult Create()
        {
            if (TempData.ContainsKey("Copy"))
            {
                var obj = Session.GetObject<EmployeeVM>();
                TempData.Remove("Copy");
                return View(obj);
            }

            var employee = new EmployeeVM() { Status = ActiveState.Active };
            Session.SetObject(employee);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "EmployeeID,EPFNo,FirstName,MiddleName,LastName,FullName,DesignationID,BranchID,DepartmentID,Title,Gender,Initials,Status,ImmediateSupervisor1")] EmployeeVM employee)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var obj = Session.GetObject<EmployeeVM>();
                    var existingEmployeeEPF = db.Employees.Where(e => e.EPFNo == employee.EPFNo).FirstOrDefault();

                    if (existingEmployeeEPF != null)
                    { ModelState.AddModelError("EPFNo", "Employee EPF Number Already Exist"); }

                    if (employee.EPFNo <1000 || employee.EPFNo >9999)
                    { ModelState.AddModelError("EPFNo", "Invalid EPF No."); }


                    if (ModelState.IsValid)
                    {
                        employee.CreatedBy = this.GetCurrUser();
                        employee.CreatedDate = DateTime.Now;
                        var objEmp = db.Employees.Add(employee.GetEntity()).Entity;
                        db.SaveChanges();

                        db.SaveChanges();
                        dbTransaction.Commit();

                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Employee created successfully.");
                        return RedirectToAction("Details", new { id = objEmp.EmployeeID });
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

            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            EmployeeVM employeeVm = new EmployeeVM(employee);
            Session.SetObject(employeeVm);
            return View(employeeVm);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "EmployeeID,EPFNo,FirstName,MiddleName,LastName,FullName,DesignationID,BranchID,DepartmentID,Title,Gender,Initials,Status,RowVersion,ImmediateSupervisor1")] EmployeeVM employee)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                byte[] curRowVersion = null;
                try
                {
                    var sEmployee = Session.GetObject<EmployeeVM>();

                    if (ModelState.IsValid)
                    {
                        var obj = db.Employees.Find(employee.EmployeeID);
                        if (obj == null)
                        { throw new DbUpdateConcurrencyException(); }

                        curRowVersion = obj.RowVersion;
                        var modObj = employee.GetEntity();
                        modObj.CopyContent(obj, "EPFNo,FirstName,MiddleName,LastName,FullName,DesignationID,BranchID,DepartmentID,Title,Gender,Initials,Status,ImmediateSupervisor1");

                        obj.ModifiedBy = this.GetCurrUser();
                        obj.ModifiedDate = DateTime.Now;
                        db.Entry(obj).OriginalValues["RowVersion"] = employee.RowVersion;

                        db.SaveChanges();
                        dbTransaction.Commit();

                        AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Employee modified successfully.");
                        return RedirectToAction("Details", new { id = obj.EmployeeID });
                    }

                    var aa = ModelState.Values.Where(x => x.Errors.Any()).ToList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.ShowConcurrencyErrors(ex);
                    employee.RowVersion = curRowVersion;
                    dbTransaction.Rollback();
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
            return View(employee);
        }
                
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(EmployeeVM employee)
        {
            try
            {
                var obj = db.Employees.Find(employee.EmployeeID);
                if (obj == null)
                { throw new DbUpdateConcurrencyException(""); }
                db.Detach(obj);

                var entry = db.Entry(employee.GetEntity());
                entry.State = EntityState.Unchanged;
                entry.State = EntityState.Deleted;
                db.SaveChanges();

                AddAlert(NIBM.Procurement.Common.AlertStyles.success, "Employee deleted successfully.");
                return RedirectToAction("Index");
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
            return RedirectToAction("Details", new { id = employee.EmployeeID });
        }
    }
}
