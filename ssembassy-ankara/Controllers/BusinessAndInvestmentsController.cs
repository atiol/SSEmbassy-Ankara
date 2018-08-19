using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    public class BusinessAndInvestmentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BusinessAndInvestmentsController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: BusinessAndInvestments
        [AllowAnonymous]
        public ActionResult Index()
        {
            var content = _db.BusinessInvestments.FirstOrDefault();
            return View(content);
        }

        // For admin panel
        [Authorize(Roles = "Admin, Content Manager")]
        public ActionResult PreviewBusinessInvestments()
        {
            var preview = _db.BusinessInvestments.FirstOrDefault();
            return View(preview);
        }

        // Edit
        [Authorize(Roles = "Admin, Content Manager")]
        public ActionResult Edit(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var model = _db.BusinessInvestments.Find(id);
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,body")]BusinessInvestments model)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("PreviewBusinessInvestments");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, Content Manager")]
        public ActionResult Create()
        {
            var model = _db.BusinessInvestments.FirstOrDefault();
            if (model != null)
            {
                ViewBag.Message = "Model available";
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,body")]BusinessInvestments model)
        {
            if (ModelState.IsValid)
            {
                _db.BusinessInvestments.Add(model);
                _db.SaveChanges();
                return RedirectToAction("PreviewBusinessInvestments");
            }

            return View(model);
        }
    }
}