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
    
    public partial class Help
    {
        public int HelpId { get; set; }
        public int LanguageId { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public System.DateTime CreateDateUtc { get; set; }
        public Nullable<System.DateTime> UpdatedateUtc { get; set; }
        public bool IsPublish { get; set; }
    
        public virtual Language Language { get; set; }
    }
}
