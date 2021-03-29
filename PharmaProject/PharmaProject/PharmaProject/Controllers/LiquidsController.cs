using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PharmaProject.Models;
using System.IO;

namespace PharmaProject.Controllers
{
      [Authorize]
    public class LiquidsController : Controller
    {
        private pharmadbEntities db = new pharmadbEntities();

        // GET: Liquids
          [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Liquid.ToList());
        }

        // GET: Liquids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquid liquid = db.Liquid.Find(id);
            if (liquid == null)
            {
                return HttpNotFound();
            }
            return View(liquid);
        }

        // GET: Liquids/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Liquids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Liquid liquid)
        {


            if (ModelState.IsValid)
            {
                string imagename = Path.GetFileNameWithoutExtension(liquid.Image2.FileName);
                string extension = Path.GetExtension(liquid.Image2.FileName);
                HttpPostedFileBase serverimage = liquid.Image2;
                int size = serverimage.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (size <= 2000000)
                    {
                        imagename = imagename + extension;
                        liquid.img = "~/liquidfiles/" + imagename;
                        imagename = Path.Combine(Server.MapPath("~/liquidfiles/"), imagename);
                        liquid.Image2.SaveAs(imagename);
                        db.Liquid.Add(liquid);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.msg = "<script>alert('invalid error')</script>";
                    }
                }
                else
                {
                    ViewBag.msg = "<script>alert('Wrong Extension')</script>";
                }
            }

            return View();
        }
       

        // GET: Liquids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquid liquid = db.Liquid.Find(id);
            if (liquid == null)
            {
                return HttpNotFound();
            }
            return View(liquid);
        }

        // POST: Liquids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Air_pressure,Air_volume,Filling_speed,Filling_range_,capacity_,Price")] Liquid liquid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liquid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(liquid);
        }

        // GET: Liquids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquid liquid = db.Liquid.Find(id);
            if (liquid == null)
            {
                return HttpNotFound();
            }
            return View(liquid);
        }

        // POST: Liquids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Liquid liquid = db.Liquid.Find(id);
            db.Liquid.Remove(liquid);
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
