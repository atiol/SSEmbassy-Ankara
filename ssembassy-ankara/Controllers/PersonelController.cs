using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    public class PersonelController : Controller
    {
        private embassy_dbEntities db = new embassy_dbEntities();

        // GET: Personel
        public ActionResult Index()
        {
            var personel = db.personel.Include(p => p.positions);
            return View(personel.ToList());
        }

        // GET: Personel/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            personel personel = db.personel.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }

            return View(personel);
        }

        // GET: Personel/Create
        public ActionResult Create()
        {
            ViewBag.position_id = new SelectList(db.positions, "id", "position");
            return View();
        }

        // POST: Personel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
                "personel_id,position_id,name,surname,telefon,fax,email,contract_start,contract_end,position_id")]
            personel personel)
        {
            if (ModelState.IsValid)
            {
                db.personel.Add(personel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.position_id = new SelectList(db.positions, "id", "position", personel.position_id);
            return View(personel);
        }

        // GET: Personel/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            personel personel = db.personel.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }

            ViewBag.position_id = new SelectList(db.positions, "id", "position", personel.position_id);
            return View(personel);
        }

        // POST: Personel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
                "personel_id,position_id,name,surname,telefon,fax,email,contract_start,contract_end,position_id")]
            personel personel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.position_id = new SelectList(db.positions, "id", "position", personel.position_id);
            return View(personel);
        }

        // GET: Personel/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            personel personel = db.personel.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }

            return View(personel);
        }

        // POST: Personel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            personel personel = db.personel.Find(id);
            db.personel.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}