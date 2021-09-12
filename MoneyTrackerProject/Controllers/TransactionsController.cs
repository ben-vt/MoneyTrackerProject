using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoneyTrackerProject.Models;

namespace MoneyTrackerProject.Controllers
{
    public class TransactionsController : Controller
    {
        private MoneyTrackerDBModelContainer db = new MoneyTrackerDBModelContainer();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Department).Include(t => t.Employee).Include(t => t.TransactionMode);
            return View(transactions.ToList());
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
            ViewBag.FKDeptId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.FKEmpId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.FKTransModeId = new SelectList(db.TransactionModes, "ModeId", "Mode");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionId,ExpenseAmount,TransactionDate,FKDeptId,FKEmpId,FKTransModeId,TransactionDescription")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
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
