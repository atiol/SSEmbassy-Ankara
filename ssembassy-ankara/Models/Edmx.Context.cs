﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ssembassy_ankara.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<article> article { get; set; }
        public virtual DbSet<article_category> article_category { get; set; }
        public virtual DbSet<BusinessInvestments> BusinessInvestments { get; set; }
        public virtual DbSet<CitizenRegistration> CitizenRegistration { get; set; }
        public virtual DbSet<EducationAndCulture> EducationAndCulture { get; set; }
        public virtual DbSet<embassy_address> embassy_address { get; set; }
        public virtual DbSet<EmbassyMission> EmbassyMission { get; set; }
        public virtual DbSet<@event> @event { get; set; }
        public virtual DbSet<event_category> event_category { get; set; }
        public virtual DbSet<FormerAmbassadors> FormerAmbassadors { get; set; }
        public virtual DbSet<ImportantNotice> ImportantNotice { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<messages> messages { get; set; }
        public virtual DbSet<ModeOfTransport> ModeOfTransport { get; set; }
        public virtual DbSet<OnlineVisaApplication> OnlineVisaApplication { get; set; }
        public virtual DbSet<PassportType> PassportType { get; set; }
        public virtual DbSet<positions> positions { get; set; }
        public virtual DbSet<PurposeOfVisit> PurposeOfVisit { get; set; }
        public virtual DbSet<Sex> Sex { get; set; }
        public virtual DbSet<SSHistory> SSHistory { get; set; }
        public virtual DbSet<TurkeyRelations> TurkeyRelations { get; set; }
        public virtual DbSet<VisaInfo> VisaInfo { get; set; }
        public virtual DbSet<VisaTypeRequested> VisaTypeRequested { get; set; }
        public virtual DbSet<VisaApproval> VisaApproval { get; set; }
    }
}
