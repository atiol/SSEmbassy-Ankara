//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ssembassy_ankara.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CitizenRegistration
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public System.DateTime ApplicationDate { get; set; }
        public string PassportNumber { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public string University { get; set; }
        public string NextOfKinInTurkey { get; set; }
        public string RelationshipWithNextOfKin { get; set; }
        public string ImageUrl { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string TurkeyAddress { get; set; }
        public string TurkeyPhone { get; set; }
        public string Email { get; set; }
        public int PurposeOfVisitId { get; set; }
        public int ExpectedDurationOfStay { get; set; }
        public bool IdeclareTruthOfInfo { get; set; }
        public string PassportImage { get; set; }
        public string NextOfKinContact { get; set; }
    
        public virtual PurposeOfVisit PurposeOfVisit { get; set; }
    }
}
