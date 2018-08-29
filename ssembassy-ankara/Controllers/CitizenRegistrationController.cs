using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    public class CitizenRegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const string DefaultUserImgUrl = "~/Content/img/muser.png";
 
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
        public ActionResult PersonalDetails()
        {
            return View();
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
                return View("PersonalDetails", abt);
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

                _db.CitizenRegistration.Add(obj);
                _db.SaveChanges();
                RemoveCitizen();
                return View("Success");
            }

            ViewBag.PurposeOfVisit = GetPurposeOfStayListItems();
            return View("ContactInfo");
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