using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiMang.Controllers.System
{
    public class YearController : Controller
    {
        // GET: Year
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        public ActionResult List()
        {
            var query = from s in db.YEARs select s;
            //query = query.OrderByDescending(s => s.Dept_ID);
            return View("LIST", query);
        }


        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(string id = null)
        {
            //ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            YEAR objYear = db.YEARs.Find(Convert.ToInt32(id));
            if (objYear == null)
            {
                return HttpNotFound();
            }
            return View(objYear);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(YEAR objYear)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid)
            {

                objYear.Year_Id = db.YEARs.Where(x => x.Year_Id == objYear.Year_Id).Select(x => x.Year_Id).FirstOrDefault();
                db.Entry(objYear).State = EntityState.Modified;
                db.YEARs.Append(objYear);
                db.SaveChanges();
                ModelState.AddModelError("", "Record has been udpated successfully!");
            }
            var query = from s in db.YEARs select s;
            return View("LIST", query);
        }

        //[Authorize]
        public ActionResult Delete(string id = null)
        {
            // ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            YEAR objYear = db.YEARs.Find(Convert.ToInt32(id));
            if (objYear == null)
            {
                return HttpNotFound();
            }
            return View(objYear);
        }

        //
        // POST: /Staff/Delete/
        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            YEAR objYear = db.YEARs.Find(Convert.ToInt32(id));
            db.YEARs.Remove(objYear);
            db.SaveChanges();

            var query = from s in db.YEARs select s;
            return View("LIST", query);
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.YEARs = new SelectList(db.YEARs.ToList(), "Year_Id", "Year_Desc");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(YEAR objYear)
        {
            YEAR findCourse = db.YEARs.Find(Convert.ToInt32(objYear.Year_Id));
            ViewBag.YEARs = new SelectList(db.YEARs.ToList(), "Year_Id", "Year_Desc");
            if (ModelState.IsValid)
            {
                db.YEARs.Add(objYear);
                db.SaveChanges();
                var query = from s in db.YEARs select s;
                return View("LIST", query);
            }
            else
            {
                ModelState.AddModelError("", "This Year is already exist!");
            }
            return View(objYear);

        }
    }
}