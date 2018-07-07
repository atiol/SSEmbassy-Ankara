using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ssembassy_ankara.Controllers
{
    [Authorize(Roles = "Admin, Content Manager")]
    [RequireHttps]
    public class CPanelController : Controller
    {
        // GET: CPanel
        public ActionResult Index()
        {
            return View();
        }
    }
}