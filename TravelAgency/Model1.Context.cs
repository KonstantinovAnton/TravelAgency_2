﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelAgency
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities3 : DbContext
    {
        public Entities3()
            : base("name=Entities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Nutrition> Nutrition { get; set; }
        public virtual DbSet<Payment_Type> Payment_Type { get; set; }
        public virtual DbSet<PhotoUser> PhotoUser { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<Tour_Type> Tour_Type { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
