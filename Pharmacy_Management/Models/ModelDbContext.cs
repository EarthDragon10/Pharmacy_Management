using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pharmacy_Management.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Drawers> Drawers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Medicines> Medicines { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pescritions> Pescritions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SupplierCompanies> SupplierCompanies { get; set; }
        public virtual DbSet<TypeMedicine> TypeMedicine { get; set; }
        public virtual DbSet<TypeProduct> TypeProduct { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customers>()
                .HasMany(e => e.Pescritions)
                .WithRequired(e => e.Customers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Drawers>()
                .HasMany(e => e.Medicines)
                .WithRequired(e => e.Drawers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medicines>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Medicines)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierCompanies>()
                .HasMany(e => e.Medicines)
                .WithRequired(e => e.SupplierCompanies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeProduct>()
                .HasMany(e => e.Medicines)
                .WithRequired(e => e.TypeProduct)
                .WillCascadeOnDelete(false);
        }
    }
}
