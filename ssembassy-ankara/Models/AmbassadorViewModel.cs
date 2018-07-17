using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ssembassy_ankara.Models
{
    public class AmbassadorViewModel : ApplicationUser
    {
        public string Bio { get; set; }
        public string Message { get; set; }
    }
}