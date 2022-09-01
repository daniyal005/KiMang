using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace KiMang.Controllers.Student
{
    public class StudentController : Controller
    {
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        Services.AuditLog auditLog = new Services.AuditLog();
        // GET: Student

        public ActionResult Student()
        {
            var query = from s in db.STUDENTs
                        join sf in db.STUDENT_FEE
                        on s.STUDENT_ID equals sf.STUDENT_ID
                        select s;

            var StdRecords= db.STUDENTs.OrderBy(m => m.STUDENT_ID).ToPagedList(1, 20);
            ViewData["student"] = StdRecords;
            ViewBag.TotalStdCount = StdRecords.TotalItemCount;
            ViewData["std_fees"] = db.STUDENT_FEE.OrderBy(m => m.STUDENT_ID).ToPagedList(1, 20);
            return View();


        }

        #region info
        public ActionResult List()
        {
            var std = db.STUDENTs.ToList();
            return PartialView(std);
        }



        public ActionResult Create()
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            ViewBag.STUDENT_STATUS = new SelectList(db.STUDENT_STATUS.ToList(), "Status_Id", "Status_Desc");
            ViewBag.YEAR = new SelectList(db.YEARs.ToList(), "Year_ID", "Year_Descr");
            ViewBag.CLASS = new SelectList(db.CLASSES.ToList(), "Class_ID", "Class_Desc");
            return View ();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult create(STUDENT obj)
        {
                STUDENT findClass = db.STUDENTs.Find(Convert.ToInt32(obj.STUDENT_ID),obj.YEAR);
                ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
                ViewBag.STUDENT_STATUS = new SelectList(db.STUDENT_STATUS.ToList(), "Status_Id", "Status_Desc");
                ViewBag.YEAR = new SelectList(db.YEARs.ToList(), "Year_ID", "Year_Descr");
                ViewBag.CLASS = new SelectList(db.CLASSES.ToList(), "Class_ID", "Class_Desc");


                if (ModelState.IsValid && findClass==null )
                {
                       db.STUDENTs.Add(obj);
                        auditLog.InsertUserActivityLogs((Convert.ToInt32(Session["UserID"])), Session["UserName"].ToString(), "Student", "Create", Session["UserType"].ToString(), "Create Student of Id"+obj.STUDENT_ID);
                        db.SaveChanges();
                       var query = from s in db.STUDENTs select s;
                       return View();
                
                }
                else
                {
                    ModelState.AddModelError("", "This Student ID is already exist!");
                }
                return View(obj);

         }


        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(string id =null ,string year = null)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            ViewBag.STUDENT_STATUS = new SelectList(db.STUDENT_STATUS.ToList(), "Status_Id", "Status_Desc");
            ViewBag.YEAR = new SelectList(db.YEARs.ToList(), "Year_Descr", "Year_Descr");
            ViewBag.CLASS = new SelectList(db.CLASSES.ToList(), "Class_ID", "Class_Desc");


            STUDENT obj = db.STUDENTs.Find(Convert.ToInt32(id),year);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(STUDENT obj)
        {
            ViewBag.DEPARTMENTs = new SelectList(db.DEPARTMENTs.ToList(), "Dept_Id", "Dept_Desc");
            ViewBag.STUDENT_STATUS = new SelectList(db.STUDENT_STATUS.ToList(), "Status_Id", "Status_Desc");
            ViewBag.YEAR = new SelectList(db.YEARs.ToList(), "Year_Descr", "Year_Descr");
            ViewBag.CLASS = new SelectList(db.CLASSES.ToList(), "Class_ID", "Class_Desc");
            
            if (ModelState.IsValid)
            {

                obj.DEPT_ID = db.DEPARTMENTs.Where(x => x.Dept_ID == obj.DEPT_ID).Select(x => x.Dept_ID).FirstOrDefault();
                obj.STATUS_ID = db.STUDENT_STATUS.Where(x => x.STATUS_ID == obj.STATUS_ID).Select(x => x.STATUS_ID).FirstOrDefault();
                obj.CLASS_ID = db.CLASSES.Where(x => x.Class_ID == obj.CLASS_ID).Select(x => x.Class_ID).FirstOrDefault();
                //obj.YEAR = db.YEARs.Where(x => x.Year_Id == obj.YEAR)).Select(x => x.).FirstOrDefault();

                db.Entry(obj).State = EntityState.Modified;
                db.STUDENTs.Append(obj);
                db.SaveChanges();
                ModelState.AddModelError("", "Record has been udpated successfully!");
            }
            //var query = from s in db.STUDENTs select s;
            return RedirectToAction("Student");
        }



        public async Task<ActionResult> Delete(int id)

           
        {
            if (ModelState.IsValid)
            {
                STUDENT graduates = db.STUDENTs.FirstOrDefault(x => x.STUDENT_ID == id);
                if (graduates != null)
                {
                    db.STUDENTs.Remove(graduates);
                    await db.SaveChangesAsync();
                }
            }

            return RedirectToAction("Student");
        }
        #endregion

        #region fees

        public ActionResult FEE()
        {
            var std = db.STUDENT_FEE.ToList();
            return View(std);
        }

        [AllowAnonymous]

        public ActionResult Fees_Edit()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Fees_Edit(STUDENT_FEE obj)
        {

            //STUDENT_FEE findClass = db.STUDENT_FEE.Find(obj.STUDENT_ID);


            if (ModelState.IsValid )
                {
                db.STUDENT_FEE.Add(obj);
                    db.SaveChanges();
                    var query = from s in db.STUDENT_FEE select s;
                    return View("FEE", query);
                }
                else
                {
                    ModelState.AddModelError("", "This Class is already exist!");
                }
                return View(obj);

        }


        #endregion

        [HttpPost]
        public ActionResult DeleteMultiple(List<STUDENT> items, string submit)
        {
            foreach (var item in items)
            {
                if (item.IS_SELECTED == true)
                {
                }
            }
            return View("Student");
        }
    }
}