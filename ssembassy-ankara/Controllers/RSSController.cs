using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using ssembassy_ankara.Models;
using ssembassy_ankara.RSSHelper;

namespace ssembassy_ankara.Controllers
{
    public class RSSController : Controller
    {
        // GET: RSS
        public PartialViewResult Index(int? page)
        {
            int pageSize = 3;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            const string url = "http://www.sudantribune.com/spip.php?page=backend";
            var listItems = RSSHelper.RSSHelper.Read(url);

            if (listItems == null) return PartialView("_RSSPartial", null);
            var articlesList = new List<RSSFeed>();
            foreach (var item in listItems)
            {
                if (item.Subject == null || !item.Subject.Contains("South Sudan"))
                {
                    Console.WriteLine("Subject : {0}", item.Subject);
                    continue;
                }
                articlesList.Add(item);
            }
            var items = articlesList.ToPagedList(pageIndex, pageSize);
            return PartialView("_RSSPartial", items);
        }
    }
}