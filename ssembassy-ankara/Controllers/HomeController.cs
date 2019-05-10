using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
            var articleList = _db.Articles.OrderByDescending(x => x.id).Take(5).ToList();
            var articles = new List<NewsViewModel>();

            foreach (var item in articleList)
            {
                var articleViewModel = new NewsViewModel
                {
                    Id = item.id,
                    Title = item.title,
                    ImgUrl = string.IsNullOrEmpty(item.imageUrl) ? _defaultArticleImageUrl : item.imageUrl
                };
                articles.Add(articleViewModel);
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
            var staffList = _db.Users.OrderBy(x => x.Id).ToList();
            var staffViewList = new List<StaffBasicInfoViewModel>();

            if (staffList.Count > 0)
            {
                foreach (var staff in staffList)
                {
                    var staffViewModel = new StaffBasicInfoViewModel
                    {
                        Id = staff.Id,
                        FullName = staff.FullName,
                        ImgUrl = string.IsNullOrEmpty(staff.ImgUrl) ? _defaultStaffImageUrl: staff.ImgUrl,
                        Position = staff.Positions.position
                    };
                    staffViewList.Add(staffViewModel);
                }
            }

            return View(staffViewList);
        }

        // History Page Details
        public ActionResult History()
        {
            var model = _db.SSHistory.FirstOrDefault();
            return View(model);
        }

        // Embassy Mission Page Details
        public ActionResult EmbassyMission()
        {
            var model = _db.EmbassyMission.FirstOrDefault();
            return View(model);
        }

        // Relationship with Turkey
        public ActionResult TurkeyRelations()
        {
            var model = _db.TurkeyRelations.FirstOrDefault();
            return View(model);
        }

        // Education and Culture
        public ActionResult EducationAndCulture()
        {
            var model = _db.EducationAndCulture.FirstOrDefault();
            return View(model);
        }
    }
}