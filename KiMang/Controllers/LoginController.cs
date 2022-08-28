using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KiMang.Controllers
{
    public class LoginController : Controller
    {
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        public ActionResult login()
        {
            return View();
        }
        // GET: Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(EMPLOYEE objEmp)
        {
            if (ModelState.IsValid)
            {
                var userExist = db.EMPLOYEEs.Where(x => x.EMPLOYEE_ID == objEmp.EMPLOYEE_ID && x.NAME == objEmp.NAME && x.PWD == objEmp.PWD).ToArray();
                if (userExist.Length > 0)
                {
                    if (userExist[0].IS_ACTIVE == 1)
                    {
                        FormsAuthentication.SetAuthCookie(objEmp.NAME, false);
                        Session["UserID"] = objEmp.EMPLOYEE_ID.ToString();
                        Session["UserName"] = objEmp.NAME.ToString();
                        Session["UserType"] = "Teacher";
                        return RedirectToAction("Dashboard", "Dashboard", new { area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Not Active");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Credentials");
            }
            return null;

        }
    }
}