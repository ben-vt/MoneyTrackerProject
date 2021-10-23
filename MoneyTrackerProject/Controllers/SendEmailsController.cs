using MoneyTrackerProject.Utilities;
using MoneyTrackerProject.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace MoneyTrackerProject.Controllers
{
    [Authorize]
    public class SendEmailsController : Controller
    {
        private MoneyTrackerDBModelContainer db = new MoneyTrackerDBModelContainer();
        // GET: SendEmails
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendEmails()
        {
            return View(new SendEmailsViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SendEmails(SendEmailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    HttpPostedFileBase pathToFile = model.PathToFile;
                    SendEmail es = new SendEmail();
                    es.Send(toEmail, subject, contents, pathToFile);

                    ViewBag.Result = "Email has been sent.";
                    ModelState.Clear();
                    return View(new SendEmailsViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
        // GET: SendBulkEmails
        public ActionResult SendBulk()
        {
            return View(new SendBulkEmailsViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SendBulk(SendBulkEmailsViewModel model)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userId = User.Identity.GetUserId();
            IEnumerable<String> roleId = userManager.FindById(userId).Roles.Select(r => r.RoleId);
            List<string> EmployeeList = new List<string>();
            try
            {
                int role_id = Convert.ToInt32(roleId.ElementAt(0));
                //here 4 is the roleId for Administrator. Administrator can view all transactions.
                if (role_id == 4 || role_id == 5)
                {
                    var employees = db.Employees.Include(d => d.Department);
                    foreach (var i in employees)
                    {
                        EmployeeList.Add(i.EmployeeEmail);
                    }
                }
                else if (roleId != null && roleId.GetEnumerator().MoveNext())
                {
                    var employees = db.Employees.Where(e => e.DeptId == role_id);
                    foreach (var i in employees)
                    {
                        EmployeeList.Add(i.EmployeeEmail);
                    }
                }
                else
                {
                    return View("~/Views/Home/Unauthorized.cshtml");
                }
                List<String> toEmail = EmployeeList;
                if (ModelState.IsValid)
                {
                    try
                    {
                        String subject = model.Subject;
                        String contents = model.Contents;
                        SendBulkEmail es = new SendBulkEmail();
                        HttpPostedFileBase pathToFile = model.PathToFile;
                        es.Send(toEmail, subject, contents, pathToFile);

                        ViewBag.Result = "Email has been sent.";
                        ModelState.Clear();
                        return View(new SendBulkEmailsViewModel());
                    }
                    catch
                    {
                        return View();
                    }
                }
                return View();
            }
            catch
            {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }
    }
}