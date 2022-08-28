using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace KiMang.Controllers.System
{
    public class DepartmentController : Controller
    {
        // GET: Department
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        public ActionResult List()
        {
            var query = from s in db.DEPARTMENTs select s;
            //query = query.OrderByDescending(s => s.Dept_ID);
            return View("LIST", query);
        }


        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(string id = null)
        {
            //ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            DEPARTMENT objDept = db.DEPARTMENTs.Find(Convert.ToInt32(id));
            if (objDept == null)
            {
                return HttpNotFound();
            }
            return View(objDept);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DEPARTMENT objDept)
        {
            
            if (ModelState.IsValid)
            {

                objDept.Dept_ID = db.DEPARTMENTs.Where(x => x.Dept_ID == objDept.Dept_ID).Select(x => x.Dept_ID).FirstOrDefault();
                db.Entry(objDept).State = EntityState.Modified;
                db.DEPARTMENTs.Append(objDept);
                db.SaveChanges();
                ModelState.AddModelError("", "Record has been udpated successfully!");
            }
            var query = from s in db.DEPARTMENTs select s;
            return View("LIST", query);
        }

        //[Authorize]
        public ActionResult Delete(string id = null)
        {
           // ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            DEPARTMENT objDept = db.DEPARTMENTs.Find(Convert.ToInt32(id));
            if (objDept == null)
            {
                return HttpNotFound();
            }
            return View(objDept);
        }

        //
        // POST: /Staff/Delete/
        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            DEPARTMENT objDept = db.DEPARTMENTs.Find(Convert.ToInt32(id));
            db.DEPARTMENTs.Remove(objDept);
            db.SaveChanges();

            var query = from s in db.DEPARTMENTs select s;
            return View("LIST", query);
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DEPARTMENT objDept)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid)
            {
                db.DEPARTMENTs.Add(objDept);
                db.SaveChanges();
            }
            var query = from s in db.DEPARTMENTs select s;
           // query = query.OrderByDescending(s => s.Dept_ID);
            return View("LIST", query);

        }
    }
}