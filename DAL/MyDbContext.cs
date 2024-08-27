//using AutomobileServiceCenter_MasterDetailsInAPI.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace AutomobileServiceCenter_MasterDetailsInAPI.DAL
//{
//    public class MyDbContext : DbContext
//    {
//        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
//        {
//        }

//        public DbSet<Service> Services { get; set; }
//        public DbSet<AppointMaster> AppointMasters { get; set; }
//        public DbSet<AppointDetail> AppointDetails { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // Ignoring Identity-related classes
//            modelBuilder.Ignore<IdentityUserLogin<string>>();
//            modelBuilder.Ignore<IdentityUserRole<string>>();
//            modelBuilder.Ignore<IdentityUserToken<string>>();

//            // Configuring relationships
//            modelBuilder.Entity<AppointDetail>()
//                .HasOne(d => d.AppointMaster)
//                .WithMany(o => o.OrderDetail)
//                .HasForeignKey(d => d.AppointId);

//            modelBuilder.Entity<AppointDetail>()
//                .HasOne(d => d.Service)
//                .WithMany(s => s.Details)
//                .HasForeignKey(d => d.ServiceId);

//            // Seeding data for Services
//            modelBuilder.Entity<Service>().HasData(
//                new Service { ServiceId = 1, ServiceName = "Engine Oil Change" },
//    new Service { ServiceId = 2, ServiceName = "Brake Inspection" },
//    new Service { ServiceId = 3, ServiceName = "Tire Rotation" },
//    new Service { ServiceId = 4, ServiceName = "Battery Replacement" },
//    new Service { ServiceId = 5, ServiceName = "Transmission Repair" },
//    new Service { ServiceId = 6, ServiceName = "Wheel Alignment" },
//    new Service { ServiceId = 7, ServiceName = "Air Filter Replacement" },
//    new Service { ServiceId = 8, ServiceName = "Coolant Flush" },
//    new Service { ServiceId = 9, ServiceName = "Exhaust Repair" },
//    new Service { ServiceId = 10, ServiceName = "Suspension Repair" }
//            );

//            // Seeding data for AppointMasters
//            modelBuilder.Entity<AppointMaster>().HasData(
//                new AppointMaster
//                {
//                    AppointId = 1,
//                    CustomerName = "Sharmin Shumi",
//                    AppointDate = DateTime.Now,
//                    IsComplete = true,
//                    ImagePath = "image1.jpg"
//                },
//                new AppointMaster
//                {
//                    AppointId = 2,
//                    CustomerName = "Nazmul Alam",
//                    AppointDate = DateTime.Now.AddDays(-1),
//                    IsComplete = false,
//                    ImagePath = "image2.jpg"
//                }
//            );

//            // Seeding data for AppointDetails
//            modelBuilder.Entity<AppointDetail>().HasData(
//                new AppointDetail
//                {
//                    AppointDetailId = 1,
//                    AppointId = 1,
//                    ServiceId = 1,
//                    Quantity = 1,
//                    Price = 100
//                },
//                new AppointDetail
//                {
//                    AppointDetailId = 2,
//                    AppointId = 1,
//                    ServiceId = 2,
//                    Quantity = 2,
//                    Price = 200
//                },
//                new AppointDetail
//                {
//                    AppointDetailId = 3,
//                    AppointId = 2,
//                    ServiceId = 3,
//                    Quantity = 3,
//                    Price = 300
//                }
//            );
//        }
//    }
//}
using AutomobileServiceCenter_MasterDetailsInAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AutomobileServiceCenter_MasterDetailsInAPI.DAL
{
    public class MyDbContext : IdentityDbContext<IdentityUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        // Define DbSet properties for your application's entities
        public DbSet<Service> Services { get; set; }
        public DbSet<AppointMaster> AppointMasters { get; set; }
        public DbSet<AppointDetail> AppointDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure base configurations are applied

            // Ignoring Identity-related classes
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();

            // Configuring relationships for AppointMaster and AppointDetail
            modelBuilder.Entity<AppointDetail>()
                .HasOne(d => d.AppointMaster)
                .WithMany(o => o.AppointDetail)
                .HasForeignKey(d => d.AppointId);

            modelBuilder.Entity<AppointDetail>()
                .HasOne(d => d.Service)
                .WithMany(s => s.AppointDetail)
                .HasForeignKey(d => d.ServiceId);

            // Seeding data for Services
            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceId = 1, ServiceName = "Engine Oil Change" },
                new Service { ServiceId = 2, ServiceName = "Brake Inspection" },
                new Service { ServiceId = 3, ServiceName = "Tire Rotation" },
                new Service { ServiceId = 4, ServiceName = "Battery Replacement" },
                new Service { ServiceId = 5, ServiceName = "Transmission Repair" },
                new Service { ServiceId = 6, ServiceName = "Wheel Alignment" },
                new Service { ServiceId = 7, ServiceName = "Air Filter Replacement" },
                new Service { ServiceId = 8, ServiceName = "Coolant Flush" },
                new Service { ServiceId = 9, ServiceName = "Exhaust Repair" },
                new Service { ServiceId = 10, ServiceName = "Suspension Repair" }
            );

            // Seeding data for AppointMasters
            modelBuilder.Entity<AppointMaster>().HasData(
                new AppointMaster
                {
                    AppointId = 1,
                    CustomerName = "Sharmin Shumi",
                    AppointDate = DateTime.Now,
                    IsComplete = true,
                    ImagePath = "image1.jpg"
                },
                new AppointMaster
                {
                    AppointId = 2,
                    CustomerName = "Nazmul Alam",
                    AppointDate = DateTime.Now.AddDays(-1),
                    IsComplete = false,
                    ImagePath = "image2.jpg"
                }
            );

            // Seeding data for AppointDetails
            modelBuilder.Entity<AppointDetail>().HasData(
                new AppointDetail
                {
                    AppointDetailId = 1,
                    AppointId = 1,
                    ServiceId = 1,
                    Quantity = 1,
                    Price = 100
                },
                new AppointDetail
                {
                    AppointDetailId = 2,
                    AppointId = 1,
                    ServiceId = 2,
                    Quantity = 2,
                    Price = 200
                },
                new AppointDetail
                {
                    AppointDetailId = 3,
                    AppointId = 2,
                    ServiceId = 3,
                    Quantity = 3,
                    Price = 300
                }
            );
        }
    }
}
