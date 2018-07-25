using System;
using System.Collections.Generic;
using System.Linq;
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
            var systemsEngineer = GetApplicationUser();

            ViewBag.Users = users;
            ViewBag.SysEngineer = systemsEngineer;
            return View();
        }

        public ApplicationUser GetApplicationUser()
        {
            return _db.Users.First(m => m.Position == "Systems Engineer");
        }
    }
}