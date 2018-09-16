using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ssembassy_ankara.Models;
using PagedList;

namespace ssembassy_ankara.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string _defaultArticleImageUrl;

        public ArticlesController()
        {
            _db = new ApplicationDbContext();
            _defaultArticleImageUrl = "~/Content/img/articles/no-image.jpg";
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
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var articles = _db.Articles.Include(a => a.article_category).OrderByDescending(x => x.id).ToList();            
            var articleViewModelList = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                articleViewModelList.Add( new ArticleViewModel
                {
                    id = article.id,
                    title = article.title,
                    article_category = article.article_category,
                    author = article.author,
                    PublishedDate = article.published.ToString("dd/MM/yyyy")
                });
            }
            var articlesList = articleViewModelList.ToPagedList(pageIndex, pageSize);
            return View(articlesList);
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
                    if (article.ImageFile.IsImage())
                    {
                        var fileName = Path.GetFileNameWithoutExtension(article.ImageFile.FileName);
                        var extension = Path.GetExtension(article.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        article.imageUrl = "~/Content/img/articles/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Content/img/articles/"), fileName);
                        article.ImageFile.SaveAs(fileName);
                    }
                    else
                    {
                        ModelState.AddModelError("ImageError", "Invalid image type. Please upload a valid image");
                        ViewBag.ArticleCategoryList = PopulateArticleCategory();
                        return View(article);
                    }
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

        // Details
        public ActionResult Details(int? id)
        {
            if(id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var model = _db.Articles.Find(id);
            if (model == null) return HttpNotFound();
            ViewBag.PublishedDate = model.published.ToString("dd/MM/yyyy");
            model.imageUrl = string.IsNullOrEmpty(model.imageUrl) ? _defaultArticleImageUrl : model.imageUrl;
            return View(model);
        }

        public FileResult ReturnFile(string filePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(filePath);
            return File(imageBytes, System.Net.Mime.MediaTypeNames.Image.Jpeg, fileName);
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
            if (article.ImageFile == null)
            {
                var thisArticle = _db.Articles.FirstOrDefault(a => a.id == article.id);
                article.imageUrl = thisArticle == null ? "":thisArticle.imageUrl;
            }
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
