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
    
        public virtual DbSet<article> articles { get; set; }
        public virtual DbSet<article_category> article_category { get; set; }
        public virtual DbSet<embassy_address> embassy_address { get; set; }
        public virtual DbSet<@event> events { get; set; }
        public virtual DbSet<event_category> event_category { get; set; }
        public virtual DbSet<former_personel> former_personel { get; set; }
        public virtual DbSet<message> messages { get; set; }
        public virtual DbSet<positions> positions { get; set; }
        public virtual DbSet<WelcomeMessage> WelcomeMessages { get; set; }
        public virtual DbSet<VisaInfo> VisaInfo { get; set; }
    }
}
