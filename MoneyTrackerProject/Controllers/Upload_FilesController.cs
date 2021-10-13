using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoneyTrackerProject.Models;

namespace MoneyTrackerProject.Controllers
{
    [Authorize]
    public class Upload_FilesController : Controller
    {
        private UploadFileModel db = new UploadFileModel();

        // GET: Upload_Files
        public ActionResult Index()
        {
            return View(db.Upload_Files.ToList());
        }

        // GET: Upload_Files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload_Files upload_Files = db.Upload_Files.Find(id);
            if (upload_Files == null)
            {
                return HttpNotFound();
            }
            return View(upload_Files);
        }

        // GET: Upload_Files/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Upload_Files/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Upload_Files file, HttpPostedFileBase postedFile)
        {
            ModelState.Clear();
            file.Path = file.Name;
            TryValidateModel(file);
            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/Uploads/");
                if (postedFile != null)
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    if (fileExtension != ".pdf")
                    {
                        ViewBag.Result = "Please select a PDF file.";
                        ModelState.Clear();
                        return View();
                    }
                    else
                    {
                        string filePath = file.Path + fileExtension;
                        file.Path = filePath;
                        postedFile.SaveAs(serverPath + file.Path);
                        db.Upload_Files.Add(file);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.Result = "Please select a file.";
                    return View();
                }
            }
            else
            {
                return View(file);
            }
        }

        // GET: Upload_Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload_Files upload_Files = db.Upload_Files.Find(id);
            if (upload_Files == null)
            {
                return HttpNotFound();
            }
            return View(upload_Files);
        }

        // POST: Upload_Files/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,Name")] Upload_Files upload_Files)
        {
            if (ModelState.IsValid)
            {
                db.Entry(upload_Files).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(upload_Files);
        }

        // GET: Upload_Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload_Files upload_Files = db.Upload_Files.Find(id);
            if (upload_Files == null)
            {
                return HttpNotFound();
            }
            return View(upload_Files);
        }

        // POST: Upload_Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Upload_Files upload_Files = db.Upload_Files.Find(id);
            db.Upload_Files.Remove(upload_Files);
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
