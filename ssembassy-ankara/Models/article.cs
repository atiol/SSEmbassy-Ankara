//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ssembassy_ankara.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class article
    {
        public int id { get; set; }
        public string author { get; set; }
        [ForeignKey("article_category")]
        public int category_id { get; set; }
        [AllowHtml]
        public string contents { get; set; }
        public DateTime published { get; set; }
        public string title { get; set; }
    
        public virtual article_category article_category { get; set; }
    }
}
