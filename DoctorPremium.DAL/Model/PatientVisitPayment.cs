//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorPremium.DAL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatientVisitPayment
    {
        public int PatientVisitPaymentId { get; set; }
        public int PatientVisitId { get; set; }
        public Nullable<decimal> CostOfServices { get; set; }
        public Nullable<decimal> Paid { get; set; }
        public string Comment { get; set; }
        public System.DateTime CreateDateUtc { get; set; }
        public Nullable<System.DateTime> UpdateDateUtc { get; set; }
    
        public virtual PatientVisit PatientVisit { get; set; }
    }
}