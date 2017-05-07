using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OOADProject.Controllers
{
    public class SearchController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Search
        public ActionResult Index(string Keyword)
        {
            // Get a typed table to run queries
            if (Keyword != "")
                ViewData["Keyword"] = "' " + Keyword + " '";
            var dR_Car = db.DR_Car.Include(d => d.DR_RentalCompany).Include(d => d.DR_CarStation);
            var test = from p in db.DR_Car where p.Type.Contains(Keyword) orderby p.Id descending select p;

            //return View(dR_Car.ToList());
            return View(test.ToList());
        }

        // GET: Search/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DR_Car dR_Car = db.DR_Car.Find(id);
            if (dR_Car == null)
            {
                return HttpNotFound();
            }
            return View(dR_Car);
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            ViewBag.RentalCompanyId = new SelectList(db.DR_RentalCompany, "Id", "Name");
            ViewBag.CarStationId = new SelectList(db.DR_CarStation, "Id", "Address");
            return View();
        }

        // POST: Search/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Catalog,ManufacturedYear,Photo,SeatAmount,Mileage,CarCompany,RentalCompanyId,CarStationId,Price,Feature,Equipment")] DR_Car dR_Car)
        {
            if (ModelState.IsValid)
            {
                db.DR_Car.Add(dR_Car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RentalCompanyId = new SelectList(db.DR_RentalCompany, "Id", "Name", dR_Car.RentalCompanyId);
            ViewBag.CarStationId = new SelectList(db.DR_CarStation, "Id", "Address", dR_Car.CarStationId);
            return View(dR_Car);
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DR_Car dR_Car = db.DR_Car.Find(id);
            if (dR_Car == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentalCompanyId = new SelectList(db.DR_RentalCompany, "Id", "Name", dR_Car.RentalCompanyId);
            ViewBag.CarStationId = new SelectList(db.DR_CarStation, "Id", "Address", dR_Car.CarStationId);
            return View(dR_Car);
        }

        // POST: Search/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Catalog,ManufacturedYear,Photo,SeatAmount,Mileage,CarCompany,RentalCompanyId,CarStationId,Price,Feature,Equipment")] DR_Car dR_Car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dR_Car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentalCompanyId = new SelectList(db.DR_RentalCompany, "Id", "Name", dR_Car.RentalCompanyId);
            ViewBag.CarStationId = new SelectList(db.DR_CarStation, "Id", "Address", dR_Car.CarStationId);
            return View(dR_Car);
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DR_Car dR_Car = db.DR_Car.Find(id);
            if (dR_Car == null)
            {
                return HttpNotFound();
            }
            return View(dR_Car);
        }

        // POST: Search/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DR_Car dR_Car = db.DR_Car.Find(id);
            db.DR_Car.Remove(dR_Car);
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
