using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OOADProject.Models;
using System.Diagnostics;

namespace OOADProject.Controllers
{
    public class SearchController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Search
        public ActionResult Index(string c, int? sa, int? rc, string cc, int? lb, int? ub, string q)
        {
            //remove multiple space from keyword
            string keyword = "";
            if (sa == null)
                return RedirectToAction("Index", "Home");
            if (q != null)
                keyword = string.Join(" ", q.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            ViewData["Keyword"] = keyword + "  ";
            string keyword_query = Search(keyword);
            string filter_query = "";
            //if Filter used
            if (!(c == "" && sa == 0 && rc == 0 && cc == "" && lb == null && ub == null))
            {
                filter_query = Filter(c, sa, rc, cc, lb, ub);
            }
            Debug.Write("SELECT * FROM dbo.Dr_CAR " + keyword_query + filter_query);
            var result = db.DR_Car.SqlQuery("SELECT * FROM dbo.Dr_CAR " + keyword_query + filter_query).ToList();
            //"INNER JOIN dbo.Dr_RENTALCOMPANY ON dbo.Dr_CAR.RentalCompanyId = dbo.Dr_RENTALCOMPANY.Id

            //bind dropdownlist from db
            //var CarCompany = from a in db.DR_Car
            //                 group a by new { a.CarCompany } into b
            //                 select b.Key.CarCompany;
            //ViewBag.CarCompany = new SelectList(CarCompany);

            //set filter opened
            //if (c == "" && sa == 0 && rc == 0 && cc == "" && lb == null && ub == null)
            //    ViewBag.flag = "collapse";
            //else
            //    ViewBag.flag = "collapse in";

            return View(result);
        }
        private String Search(string query)
        {
            if (query == "")
                return "";
            string result = "WHERE";
            string[] keyword = query.Split(' ');
            for (int i = 0; i < keyword.Length; i++)
            {
                if (i != 0)
                    result += " AND";
                result += " Type LIKE '%" + keyword[i] + "%'";
            }
            return result;
        }
        private String Filter(string c, int? sa, int? rc, string cc, int? lb, int? ub)
        {
            if (lb == null)
                lb = 0;
            if (ub == null)
                ub = 999999;
            string result = " AND Catalog like N'%" + c + "%'"
                + " AND (SeatAmount = " + sa
                + " OR " + sa + " = 0)"
                + " AND (RentalCompanyId = " + rc
                + " OR " + rc + " = 0)"
                + " AND CarCompany like '%" + cc + "%'"
                + " AND Price >= " + lb
                + " AND Price <= " + ub;
            return result;
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

        public ActionResult Compare(string compare)
        {
            if (compare == "" || compare == null)
                return RedirectToAction("Index", "Home");
            string[] s_array = compare.Split('-');
            string query = "";
            int[] c_id = new int[s_array.Length];
            for (int i = 0; i < s_array.Length; i++)
            {
                query = query + " Id=" + s_array[i] + " OR";
            }
            query= query.Remove(query.Length - 2);
            //這邊一樣使用sql語法
            Debug.Write("SELECT * FROM dbo.Dr_CAR WHERE " + query);
            var result = db.DR_Car.SqlQuery("SELECT * FROM dbo.Dr_CAR WHERE " + query).ToList();

            return View(result);
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
