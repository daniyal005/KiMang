using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KiMang.Controllers.Teacher
{
    public class TeacherController : Controller
    {
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        // GET: Teacher
    
        #region info
        public ActionResult List()
        {
            var Teach = db.EMPLOYEEs.ToList();
            return View(Teach);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EMPLOYEE obj)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.EMPLOYEEs.Add(obj);
                    db.SaveChanges();

                }
                return View();
            }
            catch (Exception ex)
            {

                return RedirectToAction("List");
            }

        }

        //[HttpPost]

        //public async Task<ActionResult> Delete(string id)


        //{
        //    if (ModelState.IsValid)
        //    {
        //        STUDENT graduates = db.EMPLOYEEs.Find(x => x.EMPLOYEE_ID == id);
        //        if (graduates != null)
        //        {
        //            db.EMPLOYEEs.Remove(graduates);
        //            await db.SaveChangesAsync();
        //        }
        //    }

        //    return RedirectToAction("Student");
        //}

        #endregion
    }
}