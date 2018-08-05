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
        [Display(Name = "Date of Application")]
        public DateTime DateOfApplication { get; set; }

        [Required]
        [Display(Name = "Have you previously applied for South Sudanese Visa?")]
        public bool AppliedBefore { get; set; }

        [Required]
        [Display(Name = "Previous Visa Number")]
        public string PreviousVisaNumber { get; set; }

        [Required]
        [Display(Name = "Date of Issue")]
        public DateTime DateOfIssue { get; set; }

        [Required]
        [Display(Name = "Place of Issue")]
        public string PlaceOfIssue { get; set; }

        [Required]
        [Display(Name = "Date of Arrival in South Sudan")]
        public DateTime DateOfArrival { get; set; }

        [Required]
        [Display(Name = "Point of Entry")]
        public string PointOfEntry { get; set; }

        [Required]
        [Display(Name = "Point of Exit")]
        public string PointOfExit { get; set; }

    }

    public class PersonalDetails
    {
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Given Names")]
        public string GivenNames { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Place of Birth")]
        public string PlaceOfBirth { get; set; }
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Required]
        [Display(Name = "Nationality / Citizenship (If dual give both)")]
        public string Nationality { get; set; }

        [Display(Name = "TelePhone")]
        public string Phone { get; set; }

        [Display(Name = "Mobile Phone")]
        public string Mobile { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }
    }

    public class PassportDetails
    {
        [Required]
        [Display(Name = "Passport Type")]
        public string PassportType { get; set; }

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

        //[Display(Name = "Employer Name")]
        //public string EmployerName { get; set; }

        //[Display(Name = "Employer Address")]
        //public string EmployerAddress { get; set; }

        //[Display(Name = "Phone")]
        //public string Phone { get; set; }

        //[Display(Name = "E-mail")]
        //public string Email { get; set; }
    }

    public class ApplicantContactDetails : PersonalDetails
    {
        //[Required]
        //[Display(Name = "Present Address")]
        //public string PresentAddress { get; set; }

        [Required]
        [Display(Name = "Permanent Country of Origin Address")]
        public string PermanentCountryOfOriginAddress { get; set; }

        //[Required]
        //[Display(Name = "Phone No")]
        //public string Phone { get; set; }

        //[Required]
        //[Display(Name = "Mobile No")]
        //public string Mobile { get; set; }

        //[Required]
        //[Display(Name = "E-mail Address")]
        //public string Email { get; set; }
    }

    public class SpouseDetails  : PersonalDetails
    {
        //[Display(Name = "Surname")]
        //public string Surname { get; set; }

        //[Display(Name = "Given Names")]
        //public string GivenNames { get; set; }

        //[Display(Name = "Permanent Address")]
        //public string PermanentAddress { get; set; }

        //[Display(Name = "Phone No")]
        //public string Phone { get; set; }

        //[Display(Name = "Mobile No")]
        //public string Mobile { get; set; }

        //[Display(Name = "E-mail Address")]
        //public string Email { get; set; }
    }

    public class NextOfKinDetails : PersonalDetails
    {
        //[Required]
        //[Display(Name = "Surname")]
        //public string Surname { get; set; }

        //[Required]
        //[Display(Name = "Given Names")]
        //public string GivenNames { get; set; }

        //[Required]
        //[Display(Name = "Permanent Address")]
        //public string PermanentAddress { get; set; }

        //[Required]
        //[Display(Name = "Phone No")]
        //public string Phone { get; set; }

        //[Required]
        //[Display(Name = "Mobile No")]
        //public string Mobile { get; set; }

        //[Required]
        //[Display(Name = "E-mail Address")]
        //public string Email { get; set; }
    }

    public class HaveYouEver
    {
        [Required]
        [Display(Name = "Been convicted of a crime or offence in any country?")]
        public bool CrimeConvict { get; set; }

        [Required]
        [Display(Name = "Been deported or removed from South Sudan or any country for overstaying your visa or violating any law or regulation?")]
        public bool DeportedForOverstayOrAnyReason { get; set; }

        [Required]
        [Display(Name = "Been convicted and sentenced for a drug offence in any country in violation of law concerning narcotics, marijuana, opium, stimulants or psychotropic substances?")]
        public bool DrugOffence { get; set; }

        [Required]
        [Display(Name = "Committed trafficking in persons or incited or aided another to commit such an offence?")]
        public bool HumanTrafficking { get; set; }

        [Required]
        [Display(Name = "Are you suffering from tuberculosis, any other infectious or contagious disease?")]
        public bool ContagiousDisease { get; set; }

        [Required]
        [Display(Name = "If you answer yes to any of the questions above, provide explanation below")]
        public string ExplanationIfYes { get; set; }

        [Required]
        [Display(Name = "Address of Place of Stay/Hotel")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Funds Available for My Stay?")]
        public bool Funds { get; set; }
    }

    public class ReferencesInSouthSudan : PersonalDetails
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //[Required]
        //[Display(Name = "Telefon No")]
        //public string Phone { get; set; }

        //[Required]
        //[Display(Name = "Address")]
        //public string Address { get; set; }

        //[Required]
        //[Display(Name = "Date of Birth")]
        //public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Relationship to applicant")]
        public string Relationship { get; set; }

        [Display(Name = "Professional Occupation")]
        public string Occupation { get; set; }

        [Required]
        [Display(Name = "Nationality and Immigration Status")]
        public string NationalityAndImmigrationStatus { get; set; }
    }

    public class Declaration
    {
        [Required]
        [Display(Name = "I declare that the information provided in this form is true and accurate")]
        public bool IdeclareTruthOfInfo { get; set; }
    }
}