using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ssembassy_ankara.Models
{
    public class StaffBasicInfoViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Image Url")]
        public string ImgUrl { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }
    }

    public class NewsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Image Url")]
        public string ImgUrl { get; set; }
    }

    public class UpcomingEventsViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime When { get; set; }
    }

    public class LoggedInUser
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
    }
}