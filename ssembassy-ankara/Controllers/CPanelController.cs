using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    [Authorize(Roles = "Admin, Content Manager")]
    [RequireHttps]
    public class CPanelController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CPanelController()
        {
            _db = new ApplicationDbContext();
        }
        // GET: CPanel
        public ActionResult Index()
        {
            var users = _db.Users.ToList();

            ViewBag.Users = users;
            return View();
        }

        // GET: logged in user details
        public PartialViewResult LoggedInUserPartial()
        {
            var loggedInUserViewModel = GetLoggedInUser();
            return PartialView("_LoggedInUserPartial", loggedInUserViewModel);
        }

        public PartialViewResult UserPanelPartial()
        {
            var userPanel = GetLoggedInUser();
            if (userPanel == null)
                return PartialView("_UserPanelPartial", null);

            return PartialView("_UserPanelPartial", userPanel);
        }

        public LoggedInUser GetLoggedInUser()
        {
            var userId = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return null;

            var loggedInUser = _db.Users.First(x => x.Id == userId);

            var userViewModal = new LoggedInUser
            {
                FullName = loggedInUser.FullName,
                Position = loggedInUser.Position,
                ImageUrl = loggedInUser.ImgUrl
            };

            return userViewModal;
        }

        // Get: All Notices
        public ActionResult Notices()
        {
            return View(_db.ImportantNotice.ToList());
        }

        // Create: Notice
        public ActionResult CreateNotice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNotice([Bind(Include = "Title,Status,MessageEn, MessageTr")]ImportantNotice notice)
        {
            notice.CreatedOn = DateTime.Now.Date;
            if (ModelState.IsValid)
            {
                _db.ImportantNotice.Add(notice);
                _db.SaveChanges();
                return RedirectToAction("Notices", "CPanel");
            }

            return View(notice);
        }

        // GET: Notice with given id
        public ActionResult EditNotice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notice = _db.ImportantNotice.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            
            return View(notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNotice([Bind(Include = "Id,Title,MessageEn,MessageTr,CreatedOn,Status")]ImportantNotice notice)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(notice).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Notices", "CPanel");
            }

            return View(notice);
        }

        // GET: Notice Details
        public ActionResult NoticeDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notice = _db.ImportantNotice.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }

            return View(notice);
        }

        public ActionResult DeleteNotice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notice = _db.ImportantNotice.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }

            _db.ImportantNotice.Remove(notice);
            _db.SaveChanges();

            return RedirectToAction("Notices");
        }
    }
}