using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiMang.Controllers.System
{
    public class GroupController : Controller
    {
        // GET: Group
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        public ActionResult List()
        {
            var query = from s in db.GROUPS select s;
            return View("LIST", query);
        }


        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(string id = null)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Group_Id", "Group_Desc");
            GROUP objGroup = db.GROUPS.Find(Convert.ToInt32(id));
            if (objGroup == null)
            {
                return HttpNotFound();
            }
            return View(objGroup);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GROUP objGroup)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid)
            {

                objGroup.GROUP_ID = db.GROUPS.Where(x => x.GROUP_ID == objGroup.GROUP_ID).Select(x => x.GROUP_ID).FirstOrDefault();
                db.Entry(objGroup).State = EntityState.Modified;
                db.GROUPS.Append(objGroup);
                db.SaveChanges();
                ModelState.AddModelError("", "Record has been udpated successfully!");
            }
            var query = from s in db.GROUPS select s;
            return View("LIST", query);
        }

        //[Authorize]
        public ActionResult Delete(string id = null)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            GROUP objGroup = db.GROUPS.Find(Convert.ToInt32(id));
            if (objGroup == null)
            {
                return HttpNotFound();
            }
            return View(objGroup);
        }

        //
        // POST: /Staff/Delete/
        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            GROUP objGroup = db.GROUPS.Find(Convert.ToInt32(id));
            db.GROUPS.Remove(objGroup);
            db.SaveChanges();

            var query = from s in db.GROUPS select s;
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
        public ActionResult Create(GROUP objGroup)
        {
            GROUP findGroup = db.GROUPS.Find(Convert.ToInt32(objGroup.GROUP_ID));
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid && findGroup == null)
            {
                db.GROUPS.Add(objGroup);
                db.SaveChanges();
                var query = from s in db.GROUPS select s;
                return View("LIST", query);
            }
            else
            {
                ModelState.AddModelError("", "This Class is already exist!");
            }
            return View(objGroup);
        }

    }
}