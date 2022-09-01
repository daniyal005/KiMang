using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace KiMang.Services
{
    public class AuditLog
    {
        KI_MANAGEMENT_SYSTEMEntities db = new KI_MANAGEMENT_SYSTEMEntities();
        public virtual void InsertUserActivityLogs(int Userid,string Name, string ModuleName,string Action,string Profession, string Desc)
        {
                AUDIT_LOG auditLog = new AUDIT_LOG();
                auditLog.DEVICE_NAME = "";
                auditLog.Name = Name;
                auditLog.MODULE = ModuleName;
                auditLog.ACTION = Action;
                auditLog.PROFESSION = Profession;
                auditLog.DECRIPTION = Desc;
                auditLog.USERID = Userid;
                auditLog.DateTime = System.DateTime.Now.ToString();
                db.AUDIT_LOG.Add(auditLog);
                db.SaveChanges();
            
        }
    }
}