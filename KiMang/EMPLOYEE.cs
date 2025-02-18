//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiMang
{
    using System;
    using System.Collections.Generic;
    
    public partial class EMPLOYEE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEE()
        {
            this.APPARAISAL_TOTAL_POINTS = new HashSet<APPARAISAL_TOTAL_POINTS>();
            this.APPRAISAL_POINTS = new HashSet<APPRAISAL_POINTS>();
            this.FINANCEs = new HashSet<FINANCE>();
            this.HRMS = new HashSet<HRM>();
            this.PAY_SLIP = new HashSet<PAY_SLIP>();
            this.SALARies = new HashSet<SALARY>();
            this.EMPLOYEE1 = new HashSet<EMPLOYEE>();
            this.EMPLOYEEs = new HashSet<EMPLOYEE>();
        }
    
        public string EMPLOYEE_ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> DEPT_ID { get; set; }
        public Nullable<int> DESIGNATION_ID { get; set; }
        public string GRADE { get; set; }
        public string QUALIFICATION { get; set; }
        public string SUBJECT_OF_TEACHING { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<System.DateTime> JOINING_DATE { get; set; }
        public Nullable<decimal> IS_ACTIVE { get; set; }
        public string PWD { get; set; }
        public Nullable<System.DateTime> DATE_OF_BIRTH { get; set; }
        public Nullable<int> LEAVES { get; set; }
        public byte[] PIC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPARAISAL_TOTAL_POINTS> APPARAISAL_TOTAL_POINTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPRAISAL_POINTS> APPRAISAL_POINTS { get; set; }
        public virtual DEPARTMENT DEPARTMENT { get; set; }
        public virtual DESIGNATION DESIGNATION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FINANCE> FINANCEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRM> HRMS { get; set; }
        public virtual HRMS_EMPLOYEE HRMS_EMPLOYEE { get; set; }
        public virtual HRMS_Managed HRMS_Managed { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAY_SLIP> PAY_SLIP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SALARY> SALARies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE> EMPLOYEE1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE> EMPLOYEEs { get; set; }
    }
}
