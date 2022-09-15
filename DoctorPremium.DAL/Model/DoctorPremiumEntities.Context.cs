﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    public partial class DoctorPremiumEntities : DbContext
    {
        public DoctorPremiumEntities(string ConnectionString)
            : base(ConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DentalCard> DentalCards { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<PatientDocument> PatientDocuments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientVisitPayment> PatientVisitPayments { get; set; }
        public virtual DbSet<PatientVisit> PatientVisits { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<ScheduleRecord> ScheduleRecords { get; set; }
        public virtual DbSet<ScheduleRecordType> ScheduleRecordTypes { get; set; }
        public virtual DbSet<TimeZone> TimeZones { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<UserPublicPage> UserPublicPages { get; set; }
        public virtual DbSet<Help> Helps { get; set; }
        public virtual DbSet<SessionData> SessionDatas { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
    
        public virtual ObjectResult<RightMoneyData_Result> RightMoneyData(Nullable<int> dataUserIdForMany)
        {
            var dataUserIdForManyParameter = dataUserIdForMany.HasValue ?
                new ObjectParameter("DataUserIdForMany", dataUserIdForMany) :
                new ObjectParameter("DataUserIdForMany", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RightMoneyData_Result>("RightMoneyData", dataUserIdForManyParameter);
        }
    }
}
