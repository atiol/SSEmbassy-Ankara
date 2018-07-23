using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ssembassy_ankara.Models
{
    public class AmbassadorViewModel
    {
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Image Url")]
        public string ImgUrl { get; set; }
        [Display(Name = "Position")]
        public string Position { get; set; }
    }

    public class NewsViewModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name = "Image Url")]
        public string ImgUrl { get; set; }
    }

    public class UpcomingEventsViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime When { get; set; }
    }
}