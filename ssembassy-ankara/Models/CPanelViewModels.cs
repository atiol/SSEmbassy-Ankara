using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ssembassy_ankara.Models
{
    [NotMapped]
    public class ArticleViewModel : article
    {
        [Display(Name = "Created On")]
        public string PublishedDate { get; set; }
    }
}