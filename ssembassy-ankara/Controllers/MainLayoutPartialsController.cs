using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    public class MainLayoutPartialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainLayoutPartialsController()
        {
            // Initialize db context
            _context = new ApplicationDbContext();
        }

        // GET: Welcome message
        public PartialViewResult _WelcomeMessagePartialViewResult()
        {
            var model = _context.WelcomeMessage.FirstOrDefault();
            
            return PartialView( new WelcomeMessage()
            {
                Message = model.Message
            });
        }

        // Get: Ambassador Info

        
        // GET: Address Info
        public PartialViewResult _EmbassyAddressPartialViewResult()
        {
            var model = _context.EmbassyAddress.FirstOrDefault();
            return PartialView(new embassy_address()
            {
                address = model.address,
                telephone = model.telephone,
                email = model.email,
                fax = model.fax
            });
        }
    }
}
