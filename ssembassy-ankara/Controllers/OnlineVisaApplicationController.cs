using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ssembassy_ankara.Models;
using ssembassy_ankara.Services;

namespace ssembassy_ankara.Controllers
{
    public class OnlineVisaApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OnlineVisaApplicationController()
        {
            _db = new ApplicationDbContext();
        }

        private OnlineVisaApplication GetApplicant()
        {
            if (Session["applicant"] == null)
            {
                Session["applicant"] = new OnlineVisaApplication();
            }

            return (OnlineVisaApplication) Session["applicant"];
        }

        private void RemoveApplicant()
        {
            Session.Remove("applicant");
        }

        private List<SelectListItem> GetGender()
        {
            var genderListDb = _db.Sex.ToList();
            var sexList = new List<SelectListItem>();
            foreach (var sex in genderListDb)
            {
                sexList.Add( new SelectListItem
                {
                    Text = sex.Gender,
                    Value = sex.Id.ToString()
                });
            }

            return sexList;
        }

        private List<SelectListItem> GetMaritalStatus()
        {
            var maritalStatusListDb = _db.MaritalStatus.ToList();
            var statusList = new List<SelectListItem>();
            foreach (var status in maritalStatusListDb)
            {
                statusList.Add( new SelectListItem
                {
                    Text = status.Status,
                    Value = status.Id.ToString()
                });
            }

            return statusList;
        }

        private List<SelectListItem> GetTransportMode()
        {
            var transportListDb = _db.ModeOfTransport.ToList();
            var transportList = new List<SelectListItem>();
            foreach (var transport in transportListDb)
            {
                transportList.Add( new SelectListItem
                {
                    Text = transport.Transport,
                    Value = transport.Id.ToString()
                });
            }

            return transportList;
        }

        private List<SelectListItem> GetVisaType()
        {
            var visaTypeListDb = _db.VisaTypeRequested.ToList();
            var visaTypeList = new List<SelectListItem>();
            foreach (var visa in visaTypeListDb)
            {
                visaTypeList.Add( new SelectListItem
                {
                    Text = visa.VisaType,
                    Value = visa.Id.ToString()
                });
            }

            return visaTypeList;
        }

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

        public List<SelectListItem> GetPassportType()
        {
            var passportTypeListDb = _db.PassportType.ToList();
            var passportTypeList = new List<SelectListItem>();
            foreach (var item in passportTypeListDb)
            {
                passportTypeList.Add(
                    new SelectListItem
                    {
                        Text = item.Type,
                        Value = item.Id.ToString()
                    });
            }
            return passportTypeList;
        }

        // GET: OnlineVisaApplication
        [HttpGet]
        public ActionResult Index()
        {
            var obj = TempData["GeneralInfo"];
            return obj != null ? View(obj):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(GeneralInformation model, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (!ModelState.IsValid) return View(model);

                var applicant = GetApplicant();
                applicant.ApplicationDate = DateTime.Now;
                applicant.ApplicationPlace = model.PlaceOfApplication;
                applicant.PreviouslyApplied = model.AppliedBefore;
                applicant.PreviousVisaNo = model.PreviousVisaNumber;
                applicant.PrevIssueDate = model.DateOfIssue;
                applicant.PassportPlaceOfIssue = model.PlaceOfIssue;
                applicant.PrevArrivalDate = model.DateOfArrival;
                applicant.PrevEntryPoint = model.PointOfEntry;
                applicant.PrevExitPoint = model.PointOfExit;

                return RedirectToAction("VisaType");
            }

            return View();
        }

        [HttpGet]
        public ActionResult VisaType()
        {
            ViewBag.VisaType = GetVisaType();
            ViewBag.Purpose = GetPurposeOfStayListItems();
            ViewBag.TransportType = GetTransportMode();
            var obj = TempData["VisaTypeDetails"];

            return obj != null ? View(obj):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VisaType(VisaTypeDetails model, string prevBtn, string nextBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var generalInfo = new GeneralInformation
                {
                    PlaceOfApplication = applicant.ApplicationPlace,
                    AppliedBefore = applicant.PreviouslyApplied,
                    PreviousVisaNumber = applicant.PreviousVisaNo,
                    PlaceOfIssue = applicant.PrevIssuePlace,
                    DateOfIssue = applicant.PrevIssueDate
                };
                TempData["GeneralInfo"] = generalInfo;
                return View("Index");
            }

            if (nextBtn != null)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.VisaType = GetVisaType();
                    ViewBag.Purpose = GetPurposeOfStayListItems();
                    ViewBag.TransportType = GetTransportMode();
                    return View(model);
                }

                applicant.VisaTypeId = model.VisaTypeId;
                applicant.PurposeOfVisitId = model.PurposeOfVisitId;
                applicant.DurationOfStay = model.DurationOfIntendedStay;
                applicant.IntendedArrivalDate = model.IntendedArrivalDate;
                applicant.TransportModeId = model.TransportModeId;

                return RedirectToAction("PersonalDetails");
            }
            return View();
        }

        [HttpGet]
        public ActionResult PersonalDetails()
        {
            ViewBag.Gender = GetGender();
            ViewBag.Status = GetMaritalStatus();
            var obj = TempData["PersonalDetails"];
            return obj != null ? View(obj):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalDetails(PersonalDetails model, string prevBtn, string nextBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var visaType = new VisaTypeDetails
                {
                    VisaTypeId = applicant.VisaTypeId,
                    PurposeOfVisitId = applicant.PurposeOfVisitId,
                    DurationOfIntendedStay = applicant.DurationOfStay,
                    IntendedArrivalDate = applicant.IntendedArrivalDate
                };
                TempData["VisaTypeDetails"] = visaType;
                return RedirectToAction("VisaType");
            }

            if (nextBtn != null)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Gender = GetGender();
                    ViewBag.Status = GetMaritalStatus();
                    return View(model);
                }

                applicant.Surname = model.Surname;
                applicant.GivenNames = model.GivenNames;
                applicant.BirthDate = model.DateOfBirth;
                applicant.BirthPlace = model.PlaceOfBirth;
                applicant.CountryOfBirth = model.CountryOfBirth;
                applicant.GenderId = model.GenderId;
                applicant.MaritalStatus = model.MaritalStatus;
                applicant.Nationality = model.Nationality;

                return RedirectToAction("PassportDetails");
            }
            return View();
        }

        [HttpGet]
        public ActionResult PassportDetails()
        {
            ViewBag.PassportType = GetPassportType();
            var obj = TempData["PassportDetails"];

            return obj != null ? View(obj):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PassportDetails(PassportDetails model, string prevBtn, string nextBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var personalInfo = new PersonalDetails
                {
                    GivenNames = applicant.GivenNames,
                    Surname = applicant.Surname,
                    DateOfBirth = applicant.BirthDate,
                    PlaceOfBirth = applicant.BirthPlace,
                    CountryOfBirth = applicant.CountryOfBirth,
                    GenderId = applicant.GenderId,
                    MaritalStatus = applicant.MaritalStatus,
                    Nationality = applicant.Nationality
                };
                TempData["PersonalDetails"] = personalInfo;
                return RedirectToAction("PersonalDetails");
            }

            if (nextBtn != null)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.PassportType = GetPassportType();
                    return View(model);
                }

                applicant.PassportTypeId = model.PassportTypeId;
                applicant.PassportNumber = model.PassportNo;
                applicant.PassportDateOfIssue = model.IssueDate;
                applicant.PassportCountryOfIssue = model.IssuingCountry;
                applicant.PassportExpiryDate = model.ExpiryDate;
                applicant.PassportPlaceOfIssue = model.PlaceOfIssue;

                return RedirectToAction("ProfessionalDetails");
            }
            ViewBag.PassportType = GetPassportType();
            return View();
        }

        [HttpGet]
        public ActionResult ProfessionalDetails()
        {
            var obj = TempData["ProfessionalDetails"];
            return obj != null ? View(obj):View();
        }

        // POST: Professional Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfessionalDetails(ProfessionalDetails model, string prevBtn, string nextBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var passport = new PassportDetails
                {
                    PlaceOfIssue = applicant.PassportPlaceOfIssue,
                    ExpiryDate = applicant.PassportExpiryDate,
                    PassportNo = applicant.PassportNumber,
                    PassportTypeId = applicant.PassportTypeId,
                    IssueDate = applicant.PassportDateOfIssue,
                    IssuingCountry = applicant.PassportCountryOfIssue
                };
                TempData["PassportDetails"] = passport;
                return RedirectToAction("PassportDetails");
            }

            if (nextBtn != null)
            {
                if (!ModelState.IsValid) return View(model);

                applicant.PresentOccupation = model.PresentOccupation;
                applicant.OccupationTitle = string.IsNullOrEmpty(model.Title) ? "":model.Title;
                applicant.EmployerName = model.GivenNames;
                applicant.EmployerAddress = model.Address;
                applicant.EmployerPhone = model.Phone;
                applicant.EmployerEmail = model.Email;

                return RedirectToAction("ContactDetails");
            }

            return View();
        }

        [HttpGet]
        public ActionResult ContactDetails()
        {
            var obj = TempData["ContactDetails"];
            return obj != null ? View(obj):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactDetails(ApplicantContactDetails model, string prevBtn, string nextBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var profession = new ProfessionalDetails
                {
                    Title = applicant.OccupationTitle,
                    Email = applicant.ApplicantEmail,
                    PresentOccupation = applicant.PresentOccupation,
                    GivenNames = applicant.EmployerName,
                    Address = applicant.EmployerAddress,
                    Phone = applicant.ApplicantPhone
                };
                TempData["ProfessionalDetails"] = profession;
                return View("ProfessionalDetails");
            }

            if (nextBtn != null)
            {
                if (!ModelState.IsValid) return View(model);
                applicant.ApplicantPresentAddress = model.Address;
                applicant.ApplicantPermanentCountryAddress = model.PermanentCountryOfOriginAddress;
                applicant.ApplicantPhone = model.Phone;
                applicant.ApplicantMobile = model.Mobile;
                applicant.ApplicantEmail = model.Email;

                return RedirectToAction("SpouseDetails");
            }
            return View();
        }

        public ActionResult SpouseDetails()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SpouseDetails(SpouseDetails model, string prevBtn, string nextBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var contact = new ApplicantContactDetails
                {
                    Address = applicant.ApplicantPresentAddress,
                    PermanentCountryOfOriginAddress = applicant.ApplicantPermanentCountryAddress,
                    Phone = applicant.ApplicantPhone,
                    Mobile = applicant.ApplicantMobile,
                    Email = applicant.ApplicantEmail
                };
                TempData["ContactDetails"] = contact;
                return View("ContactDetails");
            }

            if (nextBtn != null)
            {
                if (!ModelState.IsValid) return View(model);

                applicant.SpouseSurname = model.Surname;
                applicant.SpouseGivenNames = model.GivenNames;
                applicant.SpousePermanentAddress = model.Address;
                applicant.SpousePhone = model.Phone;
                applicant.SpouseMobile = model.Mobile;
                applicant.SpouseEmail = model.Email;

                return RedirectToAction("NextOfKinDetails");
            }
            return View();
        }

        [HttpGet]
        public ActionResult NextOfKinDetails()
        {
            var obj = TempData["Kin"];
            return obj != null ? View(obj):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NextOfKinDetails(NextOfKinDetails model, string prevBtn, string nextBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var spouse = new SpouseDetails
                {
                    Surname = applicant.SpouseSurname,
                    GivenNames = applicant.SpouseGivenNames,
                    Address = applicant.SpousePermanentAddress,
                    Phone = applicant.SpousePhone,
                    Mobile = applicant.SpouseMobile,
                    Email = applicant.SpouseEmail
                };

                return View("SpouseDetails", spouse);
            }

            if (nextBtn != null)
            {
                if (!ModelState.IsValid) return View(model);

                applicant.KinSurname = model.Surname;
                applicant.KinGivenNames = model.GivenNames;
                applicant.KinPermanentAddress = model.Address;
                applicant.KinPhone = model.Phone;
                applicant.KinMobile = model.Mobile;
                applicant.KinEmail = model.Email;

                return RedirectToAction("ExtraQuestions");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ExtraQuestions()
        {
            var obj = TempData["ExtraQAs"];
            return obj != null ? View(obj):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExtraQuestions(HaveYouEver model, string prevBtn, string nextBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var kin = new NextOfKinDetails
                {
                    Surname = applicant.KinSurname,
                    GivenNames = applicant.KinGivenNames,
                    Address = applicant.KinPermanentAddress,
                    Phone = applicant.KinPhone,
                    Mobile = applicant.KinMobile,
                    Email = applicant.KinEmail
                };
                TempData["Kin"] = kin;
                return View("NextOfKinDetails");
            }

            if (nextBtn != null)
            {
                if (!ModelState.IsValid) return View(model);

                applicant.BeenConvicted = model.CrimeConvict;
                applicant.BeenDeportedForViolationOfLaw = model.DeportedForOverstayOrAnyReason;
                applicant.DrugOffence = model.DrugOffence;
                applicant.CommittedTrafficking = model.HumanTrafficking;
                applicant.InfectiousOrContagiousDisease = model.ContagiousDisease;
                applicant.ExplanationIfYes = model.ExplanationIfYes;
                applicant.PlaceOfStayAddress = model.Address;
                applicant.FundsAvailableForStay = model.Funds;

                return RedirectToAction("ReferencesDetails");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ReferencesDetails()
        {
            ViewBag.Gender = GetGender();
            var obj = TempData["reference"];

            return obj != null ? View(obj) : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReferencesDetails(ReferencesInSouthSudan model, string prevBtn, string nextBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var extra = new HaveYouEver
                {
                    Address = applicant.PlaceOfStayAddress,
                    ContagiousDisease = applicant.InfectiousOrContagiousDisease,
                    HumanTrafficking = applicant.CommittedTrafficking,
                    DeportedForOverstayOrAnyReason = applicant.BeenDeportedForViolationOfLaw,
                    Funds = applicant.FundsAvailableForStay,
                    ExplanationIfYes = applicant.ExplanationIfYes,
                    DrugOffence = applicant.DrugOffence,
                    CrimeConvict = applicant.BeenConvicted
                };
                TempData["ExtraQAs"] = extra;
                return View("ExtraQuestions");
            }

            if (nextBtn != null)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Gender = GetGender();
                    return View(model);
                }

                applicant.ReferenceName = model.Name;
                applicant.ReferencePhone = model.Phone;
                applicant.ReferenceAddress = model.Address;
                applicant.ReferenceBirthDate = model.DateOfBirth;
                applicant.ReferenceGenderId = model.SexId;
                applicant.ReferenceRelationshipToApplicant = model.Relationship;
                applicant.ReferenceProfession = model.Occupation;
                applicant.ReferenceNationality = model.NationalityAndImmigrationStatus;

                return RedirectToAction("Declaration");
            }
            ViewBag.Gender = GetGender();
            return View();
        }

        [HttpGet]
        public ActionResult Declaration()
        {
            var applicant = GetApplicant();
            return View(applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Declaration(FormCollection collection, string prevBtn, string submitBtn)
        {
            var applicant = GetApplicant();
            if (prevBtn != null)
            {
                var reference = new ReferencesInSouthSudan
                {
                    Name = applicant.ReferenceName,
                    Phone = applicant.ReferencePhone,
                    Address = applicant.ReferenceAddress,
                    BirthDate = applicant.ReferenceBirthDate,
                    SexId = applicant.ReferenceGenderId,
                    Relationship = applicant.ReferenceRelationshipToApplicant,
                    Occupation = applicant.ReferenceProfession,
                    NationalityAndImmigrationStatus = applicant.ReferenceNationality
                };
                TempData["reference"] = reference;

                return RedirectToAction("ReferencesDetails");
            }

            if (submitBtn != null)
            {
                ValidateModel(applicant);
                var status = Convert.ToBoolean(collection["declaration"].Split(',')[0]);
                if ( status )
                {
                    try
                    {
                        _db.OnlineVisaApplication.Add(applicant);
                        _db.SaveChanges();
                    
                        // send a notification email to secretary
                        const string secretaryMail = "embassy.southsudan.ankara@gmail.com";
                        const string subject = "New Visa Application!";
                        var body = $"<p>A new user has applied for South Sudanese Visa. Please click the link below to view user information</p><a href=\"https://localhost:44340/CPanel/VisaApplicantDetails/{applicant.Id}\" target=\"_blank\">Click here to view user information</a><br/>";

                        var notifier = new NotifyByEmail();
                        var result = notifier.SendEmailNotification(secretaryMail, subject, body);

                        if (!result)
                        {
                            // email not sent.
                        }

                        RemoveApplicant();
                        return RedirectToAction("Success");
                    }
                    catch (Exception)
                    {
                        return View(applicant);
                    }
                }

                return View(applicant);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Success()
        {
            return View();
        }
    }
}
