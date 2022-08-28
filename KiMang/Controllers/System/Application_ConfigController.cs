using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiMang.Controllers.System
{
    public class Application_ConfigController : Controller
    {
        // GET: Application_Config
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        public ActionResult List()
        {
            var query = from s in db.APPLICATION_CONFIGURATION select s;
            return View("LIST", query);
        }


        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(string id = null)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");

            APPLICATION_CONFIGURATION objApp_Config = db.APPLICATION_CONFIGURATION.Find(Convert.ToInt32(id));
            if (objApp_Config == null)
            {
                return HttpNotFound();
            }
            return View(objApp_Config);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(APPLICATION_CONFIGURATION objApp_Config)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid)
            {

                objApp_Config.CONFIG_ID = db.APPLICATION_CONFIGURATION.Where(x => x.CONFIG_ID == objApp_Config.CONFIG_ID).Select(x => x.CONFIG_ID).FirstOrDefault();
                db.Entry(objApp_Config).State = EntityState.Modified;
                db.APPLICATION_CONFIGURATION.Append(objApp_Config);
                db.SaveChanges();
                ModelState.AddModelError("", "Record has been udpated successfully!");
            }
            var query = from s in db.APPLICATION_CONFIGURATION select s;
            return View("LIST", query);
        }

        //[Authorize]
        public ActionResult Delete(string id = null)
        {
            //ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            ViewBag.APPLICATION_CONFIGURATION = new SelectList(db.APPLICATION_CONFIGURATION.ToList(), "Course_Id", "Course_Desc");
            APPLICATION_CONFIGURATION objApp_Config = db.APPLICATION_CONFIGURATION.Find(Convert.ToInt32(id));
            if (objApp_Config == null)
            {
                return HttpNotFound();
            }
            return View(objApp_Config);
        }

        //
        // POST: /Staff/Delete/
        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            APPLICATION_CONFIGURATION objApp_Config = db.APPLICATION_CONFIGURATION.Find(Convert.ToInt32(id));
            db.APPLICATION_CONFIGURATION.Remove(objApp_Config);
            db.SaveChanges();

            var query = from s in db.APPLICATION_CONFIGURATION select s;
            return View("LIST", query);
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            //ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            ViewBag.APPLICATION_CONFIGURATION = new SelectList(db.APPLICATION_CONFIGURATION.ToList(), "Course_Id", "Course_Desc");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(APPLICATION_CONFIGURATION objApp_Config)
        {
            APPLICATION_CONFIGURATION findobjApp_Config = db.APPLICATION_CONFIGURATION.Find(Convert.ToInt32(objApp_Config.CONFIG_ID));
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid && findobjApp_Config == null)
            {
                db.APPLICATION_CONFIGURATION.Add(objApp_Config);
                db.SaveChanges();
                var query = from s in db.APPLICATION_CONFIGURATION select s;
                return View("LIST", query);
            }
            else
            {
                ModelState.AddModelError("", "This Course is already exist!");
            }
            return View(objApp_Config);
        }

    }
}