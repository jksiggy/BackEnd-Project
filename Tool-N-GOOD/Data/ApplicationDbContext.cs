using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tool_N_GOOD.Models;

namespace Tool_N_GOOD.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BrandType> BrandTypes { get; set; }
        public DbSet<MeasurementType> MeasurementTypes { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<ToolType> ToolTypes { get; set; }
        public DbSet<UsageHistory> UsageHistoryies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.UsageHistories)
                .WithOne(uh => uh.User)
                .HasForeignKey(uh => uh.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsageHistory>()
                .HasOne(uh => uh.User)
                .WithMany(au => au.UsageHistories)
                .HasForeignKey(uh => uh.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                

            modelBuilder.Entity<Tool>()
                .HasMany(t => t.UsageHistories)
                .WithOne(uh => uh.Tool)
                .HasForeignKey(uh => uh.ToolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsageHistory>()
                .HasOne(uh => uh.Tool)
                .WithMany(t => t.UsageHistories)
                .HasForeignKey(uh => uh.ToolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsageHistory>()
                .Property(b => b.CheckoutTime)
                .HasDefaultValueSql("getdate()");

           



            modelBuilder.Entity<ToolType>()
                .HasMany(tt => tt.Tools)
                .WithOne(t => t.ToolType)
                .HasForeignKey(t => t.ToolTypeId)
                .OnDelete(DeleteBehavior.SetNull);



            modelBuilder.Entity<Tool>()
                .HasOne(t => t.ToolType)
                .WithMany(tt => tt.Tools)
                .HasForeignKey(t => t.ToolTypeId)
                .OnDelete(DeleteBehavior.SetNull);




            modelBuilder.Entity<BrandType>()
                .HasMany(bt => bt.Tools)
                .WithOne(t => t.BrandType)
                .HasForeignKey(t => t.BrandTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Tool>()
                .HasOne(t => t.BrandType)
                .WithMany(bt => bt.Tools)
                .HasForeignKey(t => t.BrandTypeId)
                .OnDelete(DeleteBehavior.SetNull);



            modelBuilder.Entity<MeasurementType>()
                .HasMany(mt => mt.Tools)
                .WithOne(t => t.MeasurementType)
                .HasForeignKey(t => t.MeasurementTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Tool>()
                .HasOne(t => t.MeasurementType)
                .WithMany(mt => mt.Tools)
                .HasForeignKey(t => t.MeasurementTypeId)
                .OnDelete(DeleteBehavior.SetNull);

                

            ApplicationUser user = new ApplicationUser

            {
                FirstName = "warren",
                LastName = "delenger",
                Phone = "615 473 434",
                Occupation = "Mechanic 5",
                UserName = "warren@homedepot.com",
                NormalizedUserName = "WARREN@HOMEDEPOT.COM",
                Email = "warren@homedepot.com",
                NormalizedEmail = "WARREN@HOMEDEPOT.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"

            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Warren01!");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<BrandType>().HasData(
            new BrandType()
            {
                BrandTypeId = 1,
                Name = "DeWalt"
            },
            new BrandType()
            {
                BrandTypeId = 2,
                Name = "STANLEY"
            },
             new BrandType()
             {
                 BrandTypeId = 3,
                 Name = "STIHL"
             },
            new BrandType()
            {
                BrandTypeId = 4,
                Name = "Milwaukee"
            },
             new BrandType()
             {
                 BrandTypeId = 5,
                 Name = "RIGID"
             }
          );

            modelBuilder.Entity<MeasurementType>().HasData(
           new MeasurementType()
           {
               MeasurementTypeId = 1,
               Type = "Standard"

           },
           new MeasurementType()
           {
               MeasurementTypeId = 2,
               Type = "Metrix"

           }
           );

            modelBuilder.Entity<UsageHistory>().HasData(
            new UsageHistory()
            {
                UsageHistoryId = 1,
                UserId = user.Id,
                ToolId = 4,
                TaskFor = "Fix the Gate",
                Inspection = true,
                Serviceable = true,
                ExpectedReturn = new DateTime(2019, 11, 11),
                PromiseReturn = new DateTime(2019, 12, 12),


            }
            );

            modelBuilder.Entity<Tool>().HasData(
            new Tool()
            {
                ToolId = 1,
                Name = "Screw Driver",
                Description = "Red Handle",
                Measurement = "14 inches",
                Serviceable = true,
                BrandTypeId = 3,
                MeasurementTypeId = 1,
                ToolTypeId = 2,
                UserId = user.Id


            },
             new Tool()
             {
                 ToolId = 2,
                 Name = "Hammer",
                 Description = "Sledge sides",
                 Measurement = "1/2 lbs",
                 Serviceable = false,
                 BrandTypeId = 1,
                 MeasurementTypeId = 2,
                 ToolTypeId = 2,
                 UserId = user.Id


             },
              new Tool()
              {
                  ToolId = 3,
                  Name = "Pipe Wrench",
                  Description = "Silver Tip",
                  Measurement = "4 inches ",
                  Serviceable = true,
                  MeasurementTypeId = 1,
                  BrandTypeId = 2,
                  ToolTypeId = 2,
                  UserId = user.Id


              },
               new Tool()
               {
                   ToolId = 4,
                   Name = "Pliers",
                   Description = "Rubber handle",
                   Measurement = "3/4",
                   Serviceable = false,
                   MeasurementTypeId = 2,
                   BrandTypeId = 5,
                   ToolTypeId = 1,
                   UserId = user.Id


               }

        );
            modelBuilder.Entity<ToolType>().HasData(
           new ToolType()
           {
               ToolTypeId = 1,
               Name = "Mechanic",

           },
           new ToolType()
           {
               ToolTypeId = 2,
               Name = "Farming",

           },
           new ToolType()
           {
               ToolTypeId = 3,
               Name = "Building Utility",

           },
           new ToolType()
           {
               ToolTypeId = 4,
               Name = "Appliances repair",

           }
          );



        }
    }
}
