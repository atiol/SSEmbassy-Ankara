using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ssembassy_ankara.Models
{
    [MetadataType(typeof(articleAttribs))]
    public partial class article
    {
    }

    public class articleAttribs
    {
        [ForeignKey("article_category")]
        [Display(Name = "Category")]
        public int category_id { get; set; }

        [AllowHtml]
        [Display(Name = "Body")]
        public string contents { get; set; }

        [Display(Name = "Author")]
        public string author { get; set; }

        [Display(Name = "Title")]
        public string title { get; set; }
    }
}