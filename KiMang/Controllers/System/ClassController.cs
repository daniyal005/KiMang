using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace KiMang.Controllers.System
{
    public class ClassController : Controller
    {
        // GET: Class
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        Services.AuditLog auditLog = new Services.AuditLog();
        [Authorize]
        public ActionResult List()
        {
            IPagedList<CLASS> cls = null;
            cls = db.CLASSES.OrderBy(m => m.Class_ID).ToPagedList(1, 20);
            ViewBag.TotalCount = cls.TotalItemCount;
            return View("LIST", cls);
        }

        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(string id = null)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            CLASS objclass = db.CLASSES.Find(Convert.ToInt32( id));
            if (objclass == null)
            {
                return HttpNotFound();
            }
            return View(objclass);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CLASS objclass)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid)
            {

                objclass.Dept_ID = db.DEPARTMENTs.Where(x => x.Dept_ID == objclass.Dept_ID).Select(x=>x.Dept_ID).FirstOrDefault();           
                db.Entry(objclass).State = EntityState.Modified;
                db.CLASSES.Append(objclass);
                auditLog.InsertUserActivityLogs(1,"Test",objclass.Class_Desc,"Edit","Teacher","");
                db.SaveChanges();
                ModelState.AddModelError("", "Record has been udpated successfully!");
            }
            var query = from s in db.CLASSES select s;
            return View("LIST", query);
        }

        [Authorize]
        public ActionResult Delete(string id = null)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            CLASS objclass = db.CLASSES.Find(Convert.ToInt32(id));
            if (objclass == null)
            {
                return HttpNotFound();
            }
            return View(objclass);
        }

        //
        // POST: /Staff/Delete/
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            CLASS objclass = db.CLASSES.Find(Convert.ToInt32(id));
            db.CLASSES.Remove(objclass); 
            db.SaveChanges();

            var query = from s in db.CLASSES select s;
            return View("LIST", query);
        }
        [AllowAnonymous]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");            
            return View();
        }
        [Authorize]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CLASS objclass)
        {
            CLASS findClass = db.CLASSES.Find(Convert.ToInt32(objclass.Class_ID));
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid && findClass==null)
            {
                db.CLASSES.Add(objclass);
                db.SaveChanges();
                var query = from s in db.CLASSES select s;
                return View("LIST", query);
            }
            else
            {
                ModelState.AddModelError("", "This Class is already exist!");
            }
            return View(objclass);
        }

    }
}