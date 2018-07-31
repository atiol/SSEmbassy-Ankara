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

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Notice (for main site)
        public ActionResult Notice()
        {
            var notices = _db.ImportantNotice.Where(x => x.Status).ToList();
            return View(notices);
        }

    }
}