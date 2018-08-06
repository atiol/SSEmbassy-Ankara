using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    public class CitizenRegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CitizenRegistrationController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: CitizenRegistration
        public ActionResult Index()
        {
            return View();
        }

        // GET: Purpose of stay
        public List<SelectListItem> GetPurposeOfStayListItems()
        {
            var purposeList = _context.PurposeOfVisit.ToList();
            List<SelectListItem> visitPurposeListItems = new List<SelectListItem>();
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

        [HttpPost]
        public PartialViewResult AboutCitizen(AboutCitizen aboutCitizen, string prevBtn, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    CitizenRegistration obj = GetCitizen();

                    obj.ApplicationDate = DateTime.Now;
                    obj.FullName = aboutCitizen.FullName;
                    obj.BirthDate = aboutCitizen.BirthDate;
                    obj.PassportNumber = aboutCitizen.PassportNumber;
                    obj.ExpiryDate = aboutCitizen.ExpiryDate;

                    string filename = Path.GetFileNameWithoutExtension(aboutCitizen.ImageFile.FileName);
                    string extension = Path.GetExtension(aboutCitizen.ImageFile.FileName);
                    filename = filename + extension;
                    aboutCitizen.ImageUrl = "~/Content/citizenImages/" + filename;

                    filename = Path.Combine(Server.MapPath("~/Content/citizenImages/"), filename);
                    aboutCitizen.ImageFile.SaveAs(filename);

                    obj.University = aboutCitizen.University;
                    obj.ImageUrl = aboutCitizen.ImageUrl;

                    return PartialView("_CitizenContactInfo");

                }
            }
            return PartialView("_AboutCitizen");
        }

        public PartialViewResult CitizenContactInfoPartial()
        {
            ViewBag.PurposeOfVisit = GetPurposeOfStayListItems();

            return PartialView("_CitizenContactInfo");
        }

        [HttpPost]
        public ActionResult CitizenContactInfoPartial(CitizenContactDetails model, string prevBtn, string nextBtn)
        {
            CitizenRegistration obj = GetCitizen();
            if (prevBtn != null)
            {
                AboutCitizen abt = new AboutCitizen
                {
                    FullName = obj.FullName,
                    ImageUrl = obj.ImageUrl,
                    BirthDate = obj.BirthDate,
                    PassportNumber = obj.PassportNumber,
                    ExpiryDate = obj.ExpiryDate,
                    University = obj.University
                };
                return PartialView("_AboutCitizen", abt);
            }

            if (nextBtn != null)
            {
                obj.TurkeyAddress = model.TurkeyAddress;
                obj.TurkeyPhone = model.TurkeyPhone;
                obj.Email = model.Email;
                obj.NextOfKinInTurkey = model.NextOfKinInTurkey;
                obj.RelationshipWithNextOfKin = model.RelationshipWithNextOfKin;
                obj.PurposeOfVisitId = model.PurposeOfStayId;
                obj.ExpectedDurationOfStay = model.DurationOfStay;
                obj.IdeclareTruthOfInfo = model.IdeclareTruthOfInfo;

                _context.CitizenRegistration.Add(obj);
                _context.SaveChanges();
                RemoveCitizen();

                return View("Success");
            }

            return PartialView("_CitizenContactInfo");
        }
    }
}