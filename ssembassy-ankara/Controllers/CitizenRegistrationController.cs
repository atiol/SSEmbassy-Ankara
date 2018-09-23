using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using PagedList;
using ssembassy_ankara.Models;
using ssembassy_ankara.Services;

namespace ssembassy_ankara.Controllers
{
    public class CitizenRegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const string DefaultUserImgUrl = "~/Content/img/muser.png";
        private static readonly string SenderMail = System.Configuration.ConfigurationManager.AppSettings["senderMail"];
        private static readonly string Password = System.Configuration.ConfigurationManager.AppSettings["mailPass"];
        
        public CitizenRegistrationController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: Purpose of stay
        public List<SelectListItem> GetPurposeOfStayListItems()
        {
            var purposeList = _db.PurposeOfVisit.ToList();
            var visitPurposeListItems = new List<SelectListItem>();
            foreach (var item in purposeList)
            {
                visitPurposeListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Purpose,
                        Value = item.Id.ToString()
                    });
            }
            return visitPurposeListItems;
        }

        private CitizenRegistration GetCitizen()
        {
            if (Session["citizen"] == null)
            {
                Session["citizen"] = new CitizenRegistration();
            }

            return (CitizenRegistration) Session["citizen"];
        }

        private void RemoveCitizen()
        {
            Session.Remove("citizen");
        }

        // GET: Index page
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: CitizenRegistration
        [HttpGet]
        public ActionResult PersonalDetails()
        {
            var obj = TempData["about"];

            return obj != null ? View(obj):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalDetails(AboutCitizen aboutCitizen, string prevBtn, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (!ModelState.IsValid) return View();

                var obj = GetCitizen();

                obj.ApplicationDate = DateTime.Now;
                obj.FullName = aboutCitizen.FullName;
                obj.BirthDate = aboutCitizen.BirthDate;
                obj.PassportNumber = aboutCitizen.PassportNumber;
                obj.ExpiryDate = aboutCitizen.ExpiryDate;
                obj.University = aboutCitizen.University;

                var filename = Path.GetFileNameWithoutExtension(aboutCitizen.ImageFile.FileName);
                var extension = Path.GetExtension(aboutCitizen.ImageFile.FileName);
                filename = filename + extension;
                aboutCitizen.ImageUrl = "~/Content/citizenImages/" + filename;
                obj.ImageUrl = aboutCitizen.ImageUrl;
                filename = Path.Combine(Server.MapPath("~/Content/citizenImages/"), filename);
                aboutCitizen.ImageFile.SaveAs(filename);

                var fn = Path.GetFileNameWithoutExtension(aboutCitizen.PassportImageFile.FileName);
                var ext = Path.GetExtension(aboutCitizen.PassportImageFile.FileName);
                fn = fn + ext;
                aboutCitizen.PassportImage = "~/Content/citizenImages/" + fn;
                obj.PassportImage = aboutCitizen.PassportImage;
                fn = Path.Combine(Server.MapPath("~/Content/citizenImages/"), fn);
                aboutCitizen.PassportImageFile.SaveAs(fn);

                return RedirectToAction("ContactInfo");
            }
            return View();
        }

        public ActionResult ContactInfo()
        {
            ViewBag.PurposeOfVisit = GetPurposeOfStayListItems();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactInfo(CitizenContactDetails model, string prevBtn, string nextBtn)
        {
            var obj = GetCitizen();
            if (prevBtn != null)
            {
                var abt = new AboutCitizen
                {
                    FullName = obj.FullName,
                    BirthDate = obj.BirthDate,
                    PassportNumber = obj.PassportNumber,
                    ExpiryDate = obj.ExpiryDate,
                    University = obj.University
                };
                TempData["about"] = abt;
                return View("PersonalDetails");
            }
            if (nextBtn != null)
            {
                obj.TurkeyAddress = model.TurkeyAddress;
                obj.TurkeyPhone = model.TurkeyPhone;
                obj.Email = model.Email;
                obj.NextOfKinInTurkey = model.NextOfKinInTurkey;
                obj.RelationshipWithNextOfKin = model.RelationshipWithNextOfKin;
                obj.NextOfKinContact = model.NextOfKinContact;
                obj.PurposeOfVisitId = model.PurposeOfStayId;
                obj.ExpectedDurationOfStay = model.DurationOfStay;

                try
                {
                    _db.CitizenRegistration.Add(obj);
                    _db.SaveChanges();

                    // prepare email notification parameters
                    var mailBody =
                        $"<p>A new user has registered. Please click the link below to view and verify user information</p><a href=\"https://localhost:44340/CitizenRegistration/Details/{obj.Id}\" target=\"_blank\">Click here to view user information</a><br/>";
                    const string toMail = "embassy.southsudan.ankara@gmail.com";
                    const string subject = "Newly registered citizen!";
                    
                    // SendMail(toMail, subject, mailBody);
                    var notifier = new NotifyByEmail();
                    var result = notifier.SendEmailNotification(toMail, subject, mailBody); // send a notification email to secretary about new member
                    if (!result)
                    {
                        // email wasn't sent to secretary
                    }

                    RemoveCitizen();
                    return View("Success");
                }
                catch (Exception)
                {
                    // ignore
                }
            }

            ViewBag.PurposeOfVisit = GetPurposeOfStayListItems();
            return View("ContactInfo");
        }

        public static bool SendMail(string toEmail, string subject, string body)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Timeout = 100000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(SenderMail, Password)
                };

                // create mail message and send
                var mailMessage = new MailMessage(SenderMail, toEmail, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                client.Send(mailMessage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // GET: List all registered nationals
        [Authorize(Roles = "Admin,Content Manager")]
        public ActionResult Registered(int? page, string searchTerm)
        {
            const int pageSize = 12;
            var pageIndex = 1;
            ViewBag.SearchResult = "";

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var users = _db.CitizenRegistration.Include(p => p.PurposeOfVisit).OrderByDescending(x => x.Id).Where(x => x.FullName.Contains(searchTerm) || searchTerm == null).ToList();
            ViewBag.CitizenCount = users.Count;
            if (ViewBag.CitizenCount < 1)
            {
                ViewBag.SearchResult = "No results found!";
            }
            var usersListView = new List<CitizenViewModel>();
            foreach (var citizen in users)
            {
                usersListView.Add( new CitizenViewModel
                {
                    Id = citizen.Id,
                    FullName = citizen.FullName,
                    ImageUrl = citizen.ImageUrl,
                    Email = citizen.Email,
                    ApplicationDateForDisplay = citizen.ApplicationDate.ToString("d"),
                    BirthDateForDisplay = citizen.BirthDate.ToShortDateString(),
                    PassportNumber = citizen.PassportNumber,
                    PassportImage = citizen.PassportImage,
                    ExpiryDateForDisplay = citizen.ExpiryDate.ToShortDateString(),
                    ExpectedDurationOfStay = citizen.ExpectedDurationOfStay,
                    NextOfKinInTurkey = citizen.NextOfKinInTurkey,
                    NextOfKinContact = citizen.NextOfKinContact,
                    RelationshipWithNextOfKin = citizen.RelationshipWithNextOfKin,
                    PurposeOfVisit = citizen.PurposeOfVisit,
                    TurkeyAddress = citizen.TurkeyAddress,
                    TurkeyPhone = citizen.TurkeyPhone,
                    University = citizen.University
                });
            }
            var usersList = usersListView.ToPagedList(pageIndex, pageSize);

            return View(usersList);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.CitizenRegistration.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            _db.CitizenRegistration.Remove(model);
            _db.SaveChanges();

            return RedirectToAction("Registered");
        }

        public ActionResult Details(int? id)
        {
            if(id == null) {return new HttpStatusCodeResult(HttpStatusCode.BadGateway); }

            var model = _db.CitizenRegistration.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}