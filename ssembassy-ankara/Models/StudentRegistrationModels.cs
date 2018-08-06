using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [Required]
        [Display(Name = "Date of Expiry")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Name of University or College (applicable for students)")]
        public string University { get; set; }

        [Required]
        public HttpPostedFileBase ImageFile { get; set; }
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
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Person to be contacted in Turkey incase of emmergency")]
        public string NextOfKinInTurkey { get; set; }

        [Required]
        [Display(Name = "Their relationship with you")]
        public string RelationshipWithNextOfKin { get; set; }

        [Required]
        [Display(Name = "Purpose of Stay in Turkey")]
        public int PurposeOfStayId { get; set; }

        [Required]
        [Display(Name = "Expected duration of Stay in Turkey")]
        public int DurationOfStay { get; set; }

        [Required]
        [Display(Name = "I declare that the information provided in this form is true and accurate")]
        public bool IdeclareTruthOfInfo { get; set; }
    }
}