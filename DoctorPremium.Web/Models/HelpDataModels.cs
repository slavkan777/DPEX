using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Web.Models
{
    public class HelpDataModels
    {
        public void FromEntity(Help HelpData)
        {
            URL = HelpData.URL;
            Order = HelpData.Order;
            Description = HelpData.Description;
            Notes = HelpData.Notes;
        }
        public int HelpId { get; set; }
        public int LanguageId { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public bool IsPublish { get; set; }
    
    }
}