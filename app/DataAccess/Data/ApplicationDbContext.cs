﻿using System;
using System.IO;
using DataModels.Books;
using DataModels.Carts;
using DataModels.Customers;
using DataModels.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Type = DataModels.Books.Type;

namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Condition> Condition { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Type> Type { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Resale> Resale { get; set; }
        public DbSet<ResaleStatus> ResaleStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateSeedResaleStatus(modelBuilder);
            CreateSeedOrderStatus(modelBuilder);

        }
        public void CreateSeedResaleStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResaleStatus>().HasData(new ResaleStatus
            { ResaleStatus_Id = 1, Status = "Pending Approval" });
            modelBuilder.Entity<ResaleStatus>().HasData(new ResaleStatus
            { ResaleStatus_Id = 2, Status = "Approved/Awaiting Shipment from Customer" });
            modelBuilder.Entity<ResaleStatus>().HasData(new ResaleStatus { ResaleStatus_Id = 3, Status = "Rejected" });
            modelBuilder.Entity<ResaleStatus>().HasData(new ResaleStatus
            { ResaleStatus_Id = 4, Status = "Shipment Receipt Confirmed" });
            modelBuilder.Entity<ResaleStatus>().HasData(new ResaleStatus
            { ResaleStatus_Id = 5, Status = "Payment Completed" });
        }

        public void CreateSeedOrderStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus
            { OrderStatus_Id = 1, Status = "Just Placed", Position = 2 });
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus
            { OrderStatus_Id = 2, Status = "En Route", Position = 3 });
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus
            { OrderStatus_Id = 3, Status = "Pending", Position = 1 });
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus
            { OrderStatus_Id = 4, Status = "Delivered", Position = 4 });
        }
        }

   
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var environmentName =
               Environment.GetEnvironmentVariable(
                   "ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../frontend/appsettings.json")
                .AddJsonFile(Directory.GetCurrentDirectory() + $"/../frontend/appsettings.{environmentName}.json", true)

                .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("BobBookstoreContextConnection");
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DataMigrations"));

            return new ApplicationDbContext(builder.Options);
        }
    }
}