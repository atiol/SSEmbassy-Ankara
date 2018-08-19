using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ssembassy_ankara.Models
{
    public class RSSFeed
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public string DateCreated { get; set; }
        public string Language { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string EncodedContent { get; set; }
    }
}