using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string _defaultArticleImageUrl;
        private readonly string _defaultStaffImageUrl;

        public HomeController()
        {
            _defaultArticleImageUrl = "~/Content/img/peace.jpg";
            _defaultStaffImageUrl = "~/Content/img/muser.png";
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Latest 5 articles
        public PartialViewResult GetLatest5Articles()
        {
            var articleList = _db.Articles.OrderByDescending(x => x.published).Take(5).ToList();
            var articles = new List<NewsViewModel>();
            var articleView = new NewsViewModel();

            foreach (var item in articleList)
            {
                articleView.Id = item.id;
                articleView.Title = item.title;
                articleView.ImgUrl = string.IsNullOrEmpty(item.imageUrl) ? _defaultArticleImageUrl : item.imageUrl;

                articles.Add(articleView);
            }

            return PartialView("_ArticlesCarouselPartial", articles);
        }

        // GET: Notice (for main site)
        public ActionResult Notice()
        {
            var notices = _db.ImportantNotice.Where(x => x.Status).ToList();
            return View(notices);
        }

        public ActionResult Staff()
        {
            var staffList = _db.Users.ToList();

            var staffViewList = new List<StaffBasicInfoViewModel>();
            if (staffList.Count > 0)
            {
                foreach (var staff in staffList)
                {
                    staffViewList.Add(new StaffBasicInfoViewModel
                    {
                        Id = staff.Id,
                        FullName = staff.FullName,
                        ImgUrl = staff.ImgUrl,
                        Position = staff.Position
                    });
                }
            }

            return View(staffViewList);
        }
    }
}