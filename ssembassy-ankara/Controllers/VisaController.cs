using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    public class VisaController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Visa
        public ActionResult Index()
        {
            var visaInfo = _db.VisaInfo.OrderByDescending(x => x.DateCreated).First();

            return View( new VisaInfo
            {
                Id = visaInfo.Id,
                InfoEn = visaInfo.InfoEn,
                InfoTr = visaInfo.InfoTr,
                DateCreated = visaInfo.DateCreated
            });
        }

        // GET: Visa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaInfo visaInfo = _db.VisaInfo.Find(id);
            if (visaInfo == null)
            {
                return HttpNotFound();
            }
            return View(visaInfo);
        }

        // GET: Visa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Visa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InfoEn,InfoTr")] VisaInfo visaInfo)
        {
            visaInfo.DateCreated = DateTime.Now.Date;
            if (ModelState.IsValid)
            {
                _db.VisaInfo.Add(visaInfo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(visaInfo);
        }

        // GET: Visa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaInfo visaInfo = _db.VisaInfo.Find(id);
            if (visaInfo == null)
            {
                return HttpNotFound();
            }
            return View(visaInfo);
        }

        // POST: Visa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InfoEn,InfoTr,DateCreated")] VisaInfo visaInfo)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(visaInfo).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visaInfo);
        }

        // GET: Visa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaInfo visaInfo = _db.VisaInfo.Find(id);
            if (visaInfo == null)
            {
                return HttpNotFound();
            }
            return View(visaInfo);
        }

        // POST: Visa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VisaInfo visaInfo = _db.VisaInfo.Find(id);
            _db.VisaInfo.Remove(visaInfo);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
