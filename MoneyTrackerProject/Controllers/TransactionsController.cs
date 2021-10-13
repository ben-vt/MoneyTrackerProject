using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoneyTrackerProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MoneyTrackerProject.Controllers
{
    [Authorize]
    [RequireHttps]
    public class TransactionsController : Controller
    {
        private MoneyTrackerDBModelContainer db = new MoneyTrackerDBModelContainer();

        // GET: Transactions
        public ActionResult Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userId = User.Identity.GetUserId();
            IEnumerable<String> roleId = userManager.FindById(userId).Roles.Select(r => r.RoleId);
            try
            {
                int role_id = Convert.ToInt32(roleId.ElementAt(0));
                //here 4 is the roleId for Administrator.Administrator can view all transactions.
                if (role_id == 4 || role_id == 5)
                {
                    var transactions = db.Transactions.Include(t => t.Department).Include(t => t.Employee).Include(t => t.TransactionMode);
                    return View(transactions.ToList());
                }
                else if (roleId != null && roleId.GetEnumerator().MoveNext())
                {
                    var transactions = db.Transactions.Where(t => t.FKDeptId == role_id);
                    //var transactions = db.Transactions.Include(t => t.Department).Include(t => t.Employee).Include(t => t.TransactionMode);
                    return View(transactions.ToList());
                }
                else
                {
                    return View("~/Views/Home/Unauthorized.cshtml");
                }
            }
            catch
            {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userId = User.Identity.GetUserId();
            IEnumerable<String> roleId = userManager.FindById(userId).Roles.Select(r => r.RoleId);
            int role_id = Convert.ToInt32(roleId.ElementAt(0));
            //here 4 is the roleId for Administrator.Administrator can view all transactions.
            if (role_id == 4)
            {
                ViewBag.FKDeptId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            }
            else if (roleId != null && roleId.GetEnumerator().MoveNext())
            {
                ViewBag.FKDeptId = new SelectList(db.Departments.Where(d=>d.DepartmentId == role_id), "DepartmentId", "DepartmentName");
            }
            ViewBag.FKEmpId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.FKTransModeId = new SelectList(db.TransactionModes, "ModeId", "Mode");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "TransactionId,ExpenseAmount,TransactionDate,FKDeptId,FKEmpId,FKTransModeId,TransactionDescription")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                var currentDept = db.Departments.SingleOrDefault(d => d.DepartmentId == transaction.FKDeptId);
                if (transaction.FKTransModeId == 1)
                {
                    currentDept.DepartmentFund -= transaction.ExpenseAmount;
                }
                else
                {
                    currentDept.DepartmentFund += transaction.ExpenseAmount;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKDeptId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", transaction.FKDeptId);
            ViewBag.FKEmpId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", transaction.FKEmpId);
            ViewBag.FKTransModeId = new SelectList(db.TransactionModes, "ModeId", "Mode", transaction.FKTransModeId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKDeptId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", transaction.FKDeptId);
            ViewBag.FKEmpId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", transaction.FKEmpId);
            ViewBag.FKTransModeId = new SelectList(db.TransactionModes, "ModeId", "Mode", transaction.FKTransModeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionId,ExpenseAmount,TransactionDate,FKDeptId,FKEmpId,FKTransModeId,TransactionDescription")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                var currentDept = db.Departments.SingleOrDefault(d => d.DepartmentId == transaction.FKDeptId);
                if (transaction.FKTransModeId == 1)
                {
                    currentDept.DepartmentFund -= transaction.ExpenseAmount;
                }
                else
                {
                    currentDept.DepartmentFund += transaction.ExpenseAmount;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKDeptId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", transaction.FKDeptId);
            ViewBag.FKEmpId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", transaction.FKEmpId);
            ViewBag.FKTransModeId = new SelectList(db.TransactionModes, "ModeId", "Mode", transaction.FKTransModeId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            var currentDept = db.Departments.SingleOrDefault(d => d.DepartmentId == transaction.FKDeptId);
            if (transaction.FKTransModeId == 1)
            {
                currentDept.DepartmentFund += transaction.ExpenseAmount;
            }
            else
            {
                currentDept.DepartmentFund -= transaction.ExpenseAmount;
            }
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
