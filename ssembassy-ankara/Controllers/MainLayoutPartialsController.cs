using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        // Get: Ambassador Info
        public PartialViewResult AmbassadorInfoPartial()
        {
            var model = _context.Users.FirstOrDefault(u => u.Position == "Ambassador");
            if (model != null)
            {
                return PartialView("_AmbassadorInfoPartial", new StaffBasicInfoViewModel
                {
                    Id = model.Id,
                    FullName = model.FullName,
                    Position = model.Position,
                    ImgUrl = model.ImgUrl,
                    Message = model.Message
                });
            }

            return PartialView("_AmbassadorInfoPartial", null);
        }
        
        // GET: Address Info
        public PartialViewResult _EmbassyAddressPartialViewResult()
        {
            var model = _context.EmbassyAddress.First();
            return PartialView(new embassy_address
            {
                address = model.address,
                telephone = model.telephone,
                email = model.email,
                fax = model.fax
            });
        }
    }
}
