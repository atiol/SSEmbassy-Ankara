using System;
using System.ComponentModel.DataAnnotations;

namespace ssembassy_ankara.Models
{
    public class GeneralInformation
    {
        [Required]
        [Display(Name = "Place of Application")]
        public string PlaceOfApplication { get; set; }

        [Required]
        [Display(Name = "Have you previously applied for South Sudanese Visa?")]
        public bool AppliedBefore { get; set; }

        [Display(Name = "Previous Visa Number")]
        public string PreviousVisaNumber { get; set; }

        [Display(Name = "Date of Issue")]
        public DateTime? DateOfIssue { get; set; }

        [Display(Name = "Place of Issue")]
        public string PlaceOfIssue { get; set; }

        [Display(Name = "Date of Arrival in South Sudan")]
        public DateTime? DateOfArrival { get; set; }

        [Display(Name = "Point of Entry")]
        public string PointOfEntry { get; set; }

        [Display(Name = "Point of Exit")]
        public string PointOfExit { get; set; }

    }

    public class VisaTypeDetails
    {
        [Required]
        [Display(Name = "VisaTypeRequested")]
        public int VisaTypeId { get; set; }

        [Required]
        [Display(Name = "Purpose of Visit")]
        public int PurposeOfVisitId { get; set; }

        [Required]
        [Display(Name = "Duration of Intended Stay")]
        public int DurationOfIntendedStay { get; set; }

        [Required]
        [Display(Name = "Date of Intended Arrival in South Sudan")]
        public DateTime IntendedArrivalDate { get; set; }

        [Required]
        [Display(Name = "Mode of Transport")]
        public int TransportModeId { get; set; }
    }

    public class PersonalDetails
    {
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Given Names")]
        public string GivenNames { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Place of Birth")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Gender")]
        public int GenderId { get; set; }

        [Display(Name = "Country of Birth")]
        public string CountryOfBirth { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Nationality / Citizenship (If dual give both)")]
        public string Nationality { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "TelePhone")]
        public string Phone { get; set; }

        [Display(Name = "Mobile Phone")]
        public string Mobile { get; set; }

        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                           + "@"
                           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }

    public class PassportDetails
    {
        [Required]
        [Display(Name = "Passport Type")]
        public int PassportTypeId { get; set; }

        [Required]
        [Display(Name = "Passport Number")]
        public string PassportNo { get; set; }

        [Required]
        [Display(Name = "Date of Issue")]
        public DateTime IssueDate { get; set; }

        [Required]
        [Display(Name = "Country of Issue")]
        public string IssuingCountry { get; set; }

        [Required]
        [Display(Name = "Date of Expiry")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [Display(Name = "Place of Issue")]
        public string PlaceOfIssue { get; set; }
    }

    public class ProfessionalDetails : PersonalDetails
    {
        [Required]
        [Display(Name = "Present Occupation")]
        public string PresentOccupation { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
    }

    public class ApplicantContactDetails : PersonalDetails
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Permanent Country of Origin Address")]
        public string PermanentCountryOfOriginAddress { get; set; }
    }

    public class SpouseDetails  : PersonalDetails
    {
        
    }

    public class NextOfKinDetails : PersonalDetails
    {
    }

    public class HaveYouEver
    {
        [Required]
        [Display(Name = "a) Been convicted of a crime or offence in any country?")]
        public bool CrimeConvict { get; set; }

        [Required]
        [Display(Name = "b) Been deported or removed from South Sudan or any country for overstaying your visa or violating any law or regulation?")]
        public bool DeportedForOverstayOrAnyReason { get; set; }

        [Required]
        [Display(Name = "c) Been convicted and sentenced for a drug offence in any country in violation of law concerning narcotics, marijuana, opium, stimulants or psychotropic substances?")]
        public bool DrugOffence { get; set; }

        [Required]
        [Display(Name = "d) Committed trafficking in persons or incited or aided another to commit such an offence?")]
        public bool HumanTrafficking { get; set; }

        [Required]
        [Display(Name = "e) Are you suffering from tuberculosis, any other infectious or contagious disease?")]
        public bool ContagiousDisease { get; set; }

        [Display(Name = "If you answer yes to any of the questions above, provide explanation below")]
        [DataType(DataType.MultilineText)]
        public string ExplanationIfYes { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address of Place of Stay/Hotel")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Funds Available for My Stay")]
        public string Funds { get; set; }
    }

    public class ReferencesInSouthSudan : PersonalDetails
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Gender")]
        public int? SexId { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Relationship to applicant")]
        public string Relationship { get; set; }

        [Display(Name = "Professional Occupation")]
        public string Occupation { get; set; }

        [Display(Name = "Nationality and Immigration Status")]
        public string NationalityAndImmigrationStatus { get; set; }
    }

    public class Declaration
    {
        [Display(Name = "I declare that the information provided in this form is true and accurate")]
        public bool IdeclareTruthOfInfo { get; set; }
    }

    public class ApplicantsViewModel
    {
        [Display(Name = "Applied On")]
        public DateTime AppliedOn { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Given Name")]
        public string GivenNames { get; set; }

        public string Nationality { get; set; }

        public int Id { get; set; }
    }

    public class ApprovingAuthority
    {
        [Required]
        [Display(Name = "Officer Name")]
        public string OfficerName { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Entry Type")]
        public string EntryType { get; set; }

        [Required]
        [Display(Name = "Period of Stay")]
        public int PeriodOfStay { get; set; }
        
        [Display(Name = "Date of Approval")]
        public DateTime DateOfApproval { get; set; }

        public string Comments { get; set; }
    }

    public class VisaFees
    {
        [Required]
        [Display(Name = "Amount")]
        public double Amount { get; set; }

        [Required]
        [Display(Name = "Date of Receipt")]
        public DateTime DateOfReceipt { get; set; }

        [Required]
        [Display(Name = "Receipt No")]
        public string ReceiptNo { get; set; }

        [Required]
        [Display(Name = "Designated Officer's Name")]
        public string DesignatedOfficerName { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string OfficerTitle { get; set; }

        [Required]
        [Display(Name = "Visa Number")]
        public string VisaNumber { get; set; }
    }
}