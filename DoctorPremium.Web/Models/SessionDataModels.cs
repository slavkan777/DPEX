using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;

namespace DoctorPremium.Web.Models
{
    public class SessionDataModels
    {
        public void FromEntity()
        {

        }

        public int Id { get; set; }
        public string SessionId { get; set; }
        public System.DateTime SessionStart { get; set; }
        public Nullable<System.DateTime> SessionEnd { get; set; }
        public int Duration { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
    }

    public class SessionDataCounter
    {
        public int Daily { get; set; }
        public int Weekly { get; set; }
        public int Monthly { get; set; }
    }
}