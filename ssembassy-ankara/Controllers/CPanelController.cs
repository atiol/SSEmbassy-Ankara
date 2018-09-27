using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.XPath;
using Microsoft.AspNet.Identity;
using ssembassy_ankara.Models;
using PagedList;

namespace ssembassy_ankara.Controllers
{
    [Authorize]
    //[RequireHttps]
    public class CPanelController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string _defaultArticleImageUrl;

        public CPanelController()
        {
            _db = new ApplicationDbContext();
            _defaultArticleImageUrl = "~/Content/img/articles/no-image.jpg";
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

        // Get: Articles in descending order
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
        public ActionResult Notices(int? page)
        {
            int pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var notices = _db.ImportantNotice.ToList();
            var noticesList = notices.ToPagedList(pageIndex, pageSize);

            return View(noticesList);
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

        // Create: South Sudan History Page
        public ActionResult CreateSSHistoryPage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSSHistoryPage([Bind(Include = "ContentEn,ContentTr")]Page model)
        {
            if (!ModelState.IsValid) return View(model);
            var historyPage = new SSHistory
            {
                Body = model.ContentEn
            };
            _db.SSHistory.Add(historyPage);
            return RedirectToAction("Index");
        }

        // preview History page
        public ActionResult PreviewHistoryPage()
        {
            return View(_db.SSHistory.FirstOrDefault());
        }

        // Edit History Page
        public ActionResult EditHistoryPage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.SSHistory.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHistoryPage([Bind(Include = "Id,Body")]SSHistory model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("PreviewHistoryPage");
        }

        public ActionResult DeleteHistory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.SSHistory.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            _db.SSHistory.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("PreviewHistoryPage");
        }

        public ActionResult CreateEmbassyMission()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmbassyMission([Bind(Include = "Body")]EmbassyMission model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.EmbassyMission.Add(model);
            _db.SaveChanges();

            return RedirectToAction("PreviewEmbassyMission");
        }

        // Edit EmbassyMission Page
        public ActionResult EditEmbassyMission(int? id)
        {
            if(id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var model = _db.EmbassyMission.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmbassyMission([Bind(Include = "Id,Body")]EmbassyMission model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("PreviewEmbassyMission");

        }

        public ActionResult PreviewEmbassyMission()
        {
            var model = _db.EmbassyMission.FirstOrDefault();
            return View(model);
        }

        // Delete EmbassyMission Page
        public ActionResult DeleteEmbassyMission(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.EmbassyMission.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            _db.EmbassyMission.Remove(model);
            _db.SaveChanges();

            return RedirectToAction("PreviewEmbassyMission");
        }

        // GET: Articles/Details/5
        public ActionResult PreviewArticle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            article.imageUrl = string.IsNullOrEmpty(article.imageUrl) ? _defaultArticleImageUrl : article.imageUrl;
            return View(article);
        }

        // Preview Education and Culture
        public ActionResult PreviewEducationAndCulture()
        {
            var model = _db.EducationAndCulture.FirstOrDefault();
            return View(model);
        }

        public ActionResult EditEducationPageAndCulture(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var model = _db.EducationAndCulture.Find(id);
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEducationPageAndCulture(EducationAndCulture model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("PreviewEducationAndCulture");
        }

        public ActionResult DeleteEducationAndCulture(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.EmbassyMission.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            _db.EmbassyMission.Remove(model);
            _db.SaveChanges();

            return RedirectToAction("PreviewEducationAndCulture");
        }

        public ActionResult CreateEducationAndCulture()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEducationAndCulture([Bind(Include = "Body")]EducationAndCulture model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.EducationAndCulture.Add(model);
            _db.SaveChanges();

            return RedirectToAction("PreviewEducationAndCulture");
        }

        [HttpGet]
        public ActionResult VisaApplicants(int? page)
        {
            const int pageSize = 10;
            var pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var applicants = _db.OnlineVisaApplication.Include(p => p.VisaTypeRequested).OrderByDescending(o => o.Id).ToList();
            var applicantsList = new List<ApplicantsViewModel>();
            foreach (var applicant in applicants)
            {
                applicantsList.Add(new ApplicantsViewModel
                {
                    Id = applicant.Id,
                    Surname = applicant.Surname,
                    GivenNames = applicant.GivenNames,
                    Nationality = applicant.Nationality,
                    AppliedOn = applicant.ApplicationDate
                });
            }
            var applicantsListView = applicantsList.ToPagedList(pageIndex, pageSize);
            
            return View(applicantsListView);
        }

        public ActionResult VisaApplicantDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.OnlineVisaApplication.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}