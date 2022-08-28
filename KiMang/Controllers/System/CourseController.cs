using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiMang.Controllers.System
{
    public class CourseController : Controller
    {
        // GET: Course
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        public ActionResult List()
        {
            var query = from s in db.COURSES select s;
            return View("LIST", query);
        }


        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(string id = null)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
 
            COURS objCourse = db.COURSES.Find(Convert.ToInt32(id));
            if (objCourse == null)
            {
                return HttpNotFound();
            }
            return View(objCourse);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(COURS objCourse)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid)
            {

                objCourse.Dept_ID = db.DEPARTMENTs.Where(x => x.Dept_ID == objCourse.Dept_ID).Select(x => x.Dept_ID).FirstOrDefault();
                db.Entry(objCourse).State = EntityState.Modified;
                db.COURSES.Append(objCourse);
                db.SaveChanges();
                ModelState.AddModelError("", "Record has been udpated successfully!");
            }
            var query = from s in db.COURSES select s;
            return View("LIST", query);                    
        }

        //[Authorize]
        public ActionResult Delete(string id = null)
        {
            //ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            ViewBag.COURSES = new SelectList(db.COURSES.ToList(), "Course_Id", "Course_Desc");
            COURS objCourse = db.COURSES.Find(Convert.ToInt32(id));
            if (objCourse == null)
            {
                return HttpNotFound();
            }
            return View(objCourse);
        }

        //
        // POST: /Staff/Delete/
        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            COURS objCourse = db.COURSES.Find(Convert.ToInt32(id));
            db.COURSES.Remove(objCourse);
            db.SaveChanges();

            var query = from s in db.COURSES select s;
            return View("LIST", query);
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            //ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            ViewBag.COURSES = new SelectList(db.COURSES.ToList(), "Course_Id", "Course_Desc");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(COURS objCourse)
        {
            COURS findCourse = db.COURSES.Find(Convert.ToInt32(objCourse.COURSE_ID));
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            if (ModelState.IsValid && findCourse == null)
            {
                db.COURSES.Add(objCourse);
                db.SaveChanges();
                var query = from s in db.COURSES select s;
                return View("LIST", query);
            }
            else
            {
                ModelState.AddModelError("", "This Course is already exist!");
            }
            return View(objCourse);
        }

    }
}