﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOP2.HR.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HrDbContext : DbContext
    {
        public HrDbContext()
            : base("name=HrDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Department> Departments { get; set; }
        public DbSet<EMP> EMPs { get; set; }
        public DbSet<EMPDB> EMPDBs { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<USERINFO> USERINFOes { get; set; }
    }
}
