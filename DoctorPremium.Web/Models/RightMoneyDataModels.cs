using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;

namespace DoctorPremium.Web.Models
{
    public class RightMoneyDataModels
    {
        public void FromEntity(RightMoneyData_Result MoneyData)
        {
            one1 = MoneyData.one1 ?? 0;
            one2 = MoneyData.one2 ?? 0;
            seven1 = MoneyData.seven1 ?? 0;
            seven2 = MoneyData.seven2 ?? 0;
            thirty1 = MoneyData.thirty1 ?? 0;
            thirty2 = MoneyData.thirty2 ?? 0;
            sixty1 = MoneyData.sixty1 ?? 0;
            sixty2 = MoneyData.sixty2 ?? 0;
            ninety1 = MoneyData.ninety1 ?? 0;
            ninety2 = MoneyData.ninety2 ?? 0;
            oneHundredTwenty1 = MoneyData.oneHundredTwenty1 ?? 0;
            oneHundredTwenty2 = MoneyData.oneHundredTwenty2 ?? 0;
            oneHundredEighty1 = MoneyData.oneHundredEighty1 ?? 0;
            oneHundredEighty2 = MoneyData.oneHundredEighty2 ?? 0;
        }
      
        public decimal one1 { get; set; }
   
        public decimal one2 { get; set; }
        public decimal seven1 { get; set; }
        public decimal seven2 { get; set; }
        public decimal thirty1 { get; set; }
        public decimal thirty2 { get; set; }
        public decimal sixty1 { get; set; }
        public decimal sixty2 { get; set; }
        public decimal ninety1 { get; set; }
        public decimal ninety2 { get; set; } 
        public decimal oneHundredTwenty1 { get; set; }
        public decimal oneHundredTwenty2 { get; set; }
        public decimal oneHundredEighty1 { get; set; }
        public decimal oneHundredEighty2 { get; set; }
    }
}