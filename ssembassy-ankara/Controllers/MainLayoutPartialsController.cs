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
        public PartialViewResult AmbassadorInfoPartial()
        {
            var model = _context.Users.FirstOrDefault(u => u.Position == "Ambassador");
            if (model != null)
            {
                return PartialView("_AmbassadorInfoPartial", new AmbassadorViewModel
                {
                    FullName = model.FullName,
                    Position = model.Position,
                    ImgUrl = model.ImgUrl
                });
            }

            return PartialView("_AmbassadorInfoPartial", null);
        }
        
        // GET: Address Info
        public PartialViewResult _EmbassyAddressPartialViewResult()
        {
            var model = _context.EmbassyAddress.FirstOrDefault();
            return PartialView(new embassy_address
            {
                address = model.address,
                telephone = model.telephone,
                email = model.email,
                fax = model.fax
            });
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
            return PartialView("_UserPanelPartial", userPanel);
        }

        public LoggedInUser GetLoggedInUser()
        {
            var userId = User.Identity.GetUserId();
            var loggedInUser = _context.Users.First(x => x.Id == userId);

            var userViewModal = new LoggedInUser
            {
                FullName = loggedInUser.FullName,
                Position = loggedInUser.Position,
                ImageUrl = loggedInUser.ImgUrl
            };

            return userViewModal;
        }
    }
}
