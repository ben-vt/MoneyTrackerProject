using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using MoneyTrackerProject.Models;

namespace MoneyTrackerProject.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PieChart()
        {
            MoneyTrackerDBModelContainer db = new MoneyTrackerDBModelContainer();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var data = db.Departments.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
            finally
            {
                db.Configuration.ProxyCreationEnabled = proxyCreation;
            }
        }

        public ActionResult BarChart()
        {
            MoneyTrackerDBModelContainer db = new MoneyTrackerDBModelContainer();
            var data = db.Database.SqlQuery<TotalPE>("exec TotalPE").ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public class TotalPE
        {
            public string DEPTNAME { get; set; }
            public Nullable<decimal> EXPENSE { get; set; }
            public Nullable<decimal> PROFIT { get; set; }

        }
    }
}