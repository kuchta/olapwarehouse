﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OlapWarehouseApi
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OlapWarehouse : DbContext
    {
        public OlapWarehouse()
            : base("name=OlapWarehouse")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Element> Elements { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Cube> Cubes { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Fact> Facts { get; set; }
    }
}
