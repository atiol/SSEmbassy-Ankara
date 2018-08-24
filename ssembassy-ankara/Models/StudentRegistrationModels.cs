using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ssembassy_ankara.Models
{
    public class AboutCitizen
    {
        [Required]
        [Display(Name = "Full name as it appears on passport")]
        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Upload a recent passport size photo of yourself")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }

        [Required]
        [RegularExpression("^R+\\d*")]
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Expiry")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Name of College or University (applicable for students)")]
        public string University { get; set; }
    }

    public class CitizenContactDetails
    {
        [Required]
        [Display(Name = "Address in Turkey")]
        public string TurkeyAddress { get; set; }

        [Required]
        [Display(Name = "Turkey phone number")]
        public string TurkeyPhone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Name Surname of who to contact incase of emmergency")]
        public string NextOfKinInTurkey { get; set; }

        [Required]
        [Display(Name = "Their relationship with you")]
        public string RelationshipWithNextOfKin { get; set; }

        [Display(Name = "Next of kin phone")]
        public string NextOfKinContact { get; set; }

        [Required]
        [Display(Name = "Purpose of Stay in Turkey")]
        public int PurposeOfStayId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Expected duration of Stay in Turkey in months")]
        public int DurationOfStay { get; set; }

        [Required]
        [Display(Name = "I declare that the information provided above is true and accurate.")]
        public bool IdeclareTruthOfInfo { get; set; }
    }

    [NotMapped]
    public class CitizenViewModel : CitizenRegistration
    {
        public string ApplicationDateForDisplay { get; set; }
        public string ExpiryDateForDisplay { get; set; }
        public string BirthDateForDisplay { get; set; }
    }
}