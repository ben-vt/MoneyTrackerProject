using MoneyTrackerProject.Utilities;
using MoneyTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyTrackerProject.Controllers
{
    public class SendEmailsController : Controller
    {
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
        public ActionResult SendEmails(SendEmailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    SendEmail es = new SendEmail();
                    es.Send(toEmail, subject, contents);

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
    }
}