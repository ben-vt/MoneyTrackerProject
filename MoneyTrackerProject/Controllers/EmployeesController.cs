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
    public class EmployeesController : Controller
    {
        private MoneyTrackerDBModelContainer db = new MoneyTrackerDBModelContainer();

        // GET: Employees
        public ActionResult Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userId = User.Identity.GetUserId();
            IEnumerable<String> roleId = userManager.FindById(userId).Roles.Select(r => r.RoleId);
            try
            {
                int role_id = Convert.ToInt32(roleId.ElementAt(0));
                //here 4 is the roleId for Administrator. Administrator can view all transactions.
                if (role_id == 4 || role_id == 5)
                {
                    var employees = db.Employees.Include(e => e.Department);
                    return View(employees.ToList());
                }
                else if (roleId != null && roleId.GetEnumerator().MoveNext())
                {
                    var employees = db.Employees.Where(e => e.DeptId == role_id);
                    return View(employees.ToList());
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

        // GET: Employees/Details/5
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
            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.DeptId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "EmployeeId,EmployeeName,EmployeeRole,DeptId,EmployeeEmail")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DeptId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Administrator")]
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
            ViewBag.DeptId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DeptId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,EmployeeName,EmployeeRole,DeptId,EmployeeEmail")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DeptId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
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
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetEmployeeList(int DepartmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Employee> EmployeeList = db.Employees.Where(emp => emp.DeptId == DepartmentId).ToList();
            return Json(EmployeeList, JsonRequestBehavior.AllowGet);
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
