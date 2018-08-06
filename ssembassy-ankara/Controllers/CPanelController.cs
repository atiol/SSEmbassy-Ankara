﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    [Authorize]
    [RequireHttps]
    public class CPanelController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CPanelController()
        {
            _db = new ApplicationDbContext();
        }
        // GET: CPanel Home
        public ActionResult Index()
        {
            var staffs = GetAllStaff();
            ViewBag.Staff = staffs.OrderByDescending(x => x.Position).ToList();
            ViewBag.StaffCount = staffs.Count;

            var allArticles = GetArticles();
            ViewBag.Articles = allArticles.Take(5).ToList();
            ViewBag.ArticleCount = allArticles.Count;

            ViewBag.Announcements = GetAnnouncementsDesc();

            var userInquiries = GetUserInquiries();
            ViewBag.Top5Inquiries = userInquiries.Take(5).ToList();
            ViewBag.TotalUserInquiries = userInquiries.Count;

            return View();
        }

        // Get: All staff
        public List<ApplicationUser> GetAllStaff()
        {
            return _db.Users.ToList();
        }

        // Get: Top 5 articles in descending order
        public List<article> GetArticles()
        {
            return _db.Articles.OrderByDescending(x => x.published).ToList();
        }

        // Get: Announcements descending
        public List<ImportantNotice> GetAnnouncementsDesc()
        {
            return _db.ImportantNotice.OrderByDescending(x => x.CreatedOn).ToList();
        }

        // Get: Top 5 user inquiries
        public List<messages> GetUserInquiries()
        {
            return _db.Messages.OrderByDescending(x => x.id).ToList();
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
                Id = loggedInUser.Id,
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

        // GET: Welcome messages
        public ActionResult WelcomeMessages()
        {
            return View(_db.WelcomeMessage.ToList());
        }

        public ActionResult CreateWelcomeMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWelcomeMessage([Bind(Include = "Message")]WelcomeMessage model)
        {
            if (ModelState.IsValid)
            {
                _db.WelcomeMessage.Add(model);
                _db.SaveChanges();
                return RedirectToAction("WelcomeMessages", "CPanel");
            }

            return View(model);
        }

        // Edit: Welcome message
        public ActionResult EditWelcomeMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var message = _db.WelcomeMessage.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditWelcomeMessage([Bind(Include = "Id,Message")]WelcomeMessage model)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("WelcomeMessages");
            }

            return View(model);
        }

        // Display: Welcome Message
        public ActionResult DisplayWelcomeMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var message = _db.WelcomeMessage.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            return View(message);
        }

        // Delete: Welcome message
        public ActionResult DeleteWelcomeMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = _db.WelcomeMessage.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            _db.WelcomeMessage.Remove(message);
            _db.SaveChanges();
            return RedirectToAction("WelcomeMessages");
        }
    }
}