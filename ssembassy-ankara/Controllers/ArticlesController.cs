using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string _defaultArticleImageUrl;

        public ArticlesController()
        {
            _db = new ApplicationDbContext();
            _defaultArticleImageUrl = "~/Content/img/ss2.jpg";
        }

        public List<SelectListItem> PopulateArticleCategory()
        {
            var articleCategories = _db.ArticleCategory.ToList();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var item in articleCategories)
            {
                categoryList.Add(
                    new SelectListItem
                    {
                        Text = item.category,
                        Value = item.id.ToString()
                    });
            }

            return categoryList;
        }
        // GET: Articles
        public ActionResult Index()
        {
            return View(_db.Articles.Include(a => a.article_category).ToList());
        }

        // GET: Articles/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            article.imageUrl = string.IsNullOrEmpty(article.imageUrl) ? _defaultArticleImageUrl : article.imageUrl;
            return View(article);
        }

        // GET: Articles/Create
        [Authorize(Roles = "Admin, Content Manager")]
        public ActionResult Create()
        {
            ViewBag.ArticleCategoryList = PopulateArticleCategory();
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "author,title,category_id,ImageFile,contents")] article article)
        {
            if (ModelState.IsValid)
            {
                if (article.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(article.ImageFile.FileName);
                    string extension = Path.GetExtension(article.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    article.imageUrl = "~/Content/img/articles/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/img/articles/"), fileName);
                    article.ImageFile.SaveAs(fileName);
                }
                else
                {
                    article.imageUrl = _defaultArticleImageUrl;
                }

                article.published = DateTime.Now;
                _db.Articles.Add(article);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleCategoryList = PopulateArticleCategory();
            
            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize(Roles = "Admin, Content Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArticleCategoryList = PopulateArticleCategory();
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,published,author,title,category_id,contents")] article article)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(article).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticleCategoryList = PopulateArticleCategory();
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Admin, Content Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            article article = _db.Articles.Find(id);
            _db.Articles.Remove(article);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
