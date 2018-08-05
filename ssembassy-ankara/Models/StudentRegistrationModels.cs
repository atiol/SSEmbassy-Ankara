using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ssembassy_ankara.Models
{
    public class StudentRegistrationModels : PersonalDetails
    {
        public DateTime ApplicationDate { get; set; }

        [Required]
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [Required]
        [Display(Name = "Date of Expiry")]
        public string ExpiryDate { get; set; }

        [Required]
        [Display(Name = "Name of University or College (applicable for students)")]
        public string University { get; set; }

        [Required]
        [Display(Name = "Person to be contacted in Turkey incase of emmergency")]
        public string NextOfKinInTurkey { get; set; }

        [Required]
        [Display(Name = "Their relationship with you")]
        public string RelationshipWithNextOfKin { get; set; }
    }
}