﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ssembassy_ankara.Models
{
    [MetadataType(typeof(ArticleAttribs))]
    public partial class article
    {
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class ArticleAttribs
    {
        [Required]
        [ForeignKey("article_category")]
        [Display(Name = "Category")]
        public int category_id { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Body")]
        public string contents { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string author { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Display(Name = "Upload Image")]
        public string imageUrl { get; set; }
    }

    // Visa info
    [MetadataType(typeof(VisaInfoAttribs))]
    public partial class VisaInfo
    {

    }

    public class VisaInfoAttribs
    {
        [AllowHtml]
        [Display(Name = "English Translation")]
        public string InfoEn { get; set; }

        [AllowHtml]
        [Display(Name = "Turkish Translation")]
        public string InfoTr { get; set; }
    }

    // Important Information
    [MetadataType(typeof(ImportantNoticeAttribs))]
    public partial class ImportantNotice
    {

    }

    public class ImportantNoticeAttribs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        public string MessageEn { get; set; }

        [AllowHtml]
        public string MessageTr { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Should Show ?")]
        public bool Status { get; set; }
    }

    // Business Investments Model
    [MetadataType(typeof(BusinessInvestmentsAttrib))]
    public partial class BusinessInvestments
    {

    }

    public class BusinessInvestmentsAttrib
    {
        [AllowHtml]
        [Display(Name = "Content")]
        public string body { get; set; }
    }

    // Citizen Registration Model
    [MetadataType(typeof(CitizenRegistrationAttribs))]
    public partial class CitizenRegistration
    {

    }

    public class CitizenRegistrationAttribs
    {
        [Display(Name = "Applied On")]
        public DateTime ApplicationDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Birthday")]
        public DateTime BirthDate { get; set; }
    }

    [MetadataType(typeof(OnlineVisaApplicationAttribs))]
    public partial class OnlineVisaApplication
    {

    }

    public class OnlineVisaApplicationAttribs
    {
        [ForeignKey("VisaTypeRequested")]
        public int VisaTypeId { get; set; }

        [ForeignKey("PurposeOfVisit")]
        public int PurposeOfVisitId { get; set; }

        //[ForeignKey("Sex")]
        //public int GenderId { get; set; }

        //[ForeignKey("Sex2")]
        //public Nullable<int> ReferenceGenderId { get; set; }

        [ForeignKey("PassportType")]
        public int PassportTypeId { get; set; }

        [ForeignKey("ModeOfTransport")]
        public int TransportModeId { get; set; }
    }

    [MetadataType(typeof(VisaApprovalAttribs))]
    public partial class VisaApproval
    {

    }

    public class VisaApprovalAttribs
    {
        [Key]
        public int OfficerId { get; set; }
    }
}