using Flatbuilder.DAL.Entities;
using Flatbuilder.DAL.Entities.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flatbuilder.DAL.Context
{
    public class FlatbuilderContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Costumer> Costumers { get; set; }
        public DbSet<Order> Orders { get; set; }

        //many-to-many
        public DbSet<OrderRoom> OrderRooms { get; set; }

        public FlatbuilderContext(DbContextOptions<FlatbuilderContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(c => c.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //configuring inheritance
            modelBuilder.Entity<Bedroom>().HasBaseType<Room>();
            modelBuilder.Entity<Kitchen>().HasBaseType<Room>();
            modelBuilder.Entity<Shower>().HasBaseType<Room>();
            modelBuilder.Entity<Room>().HasDiscriminator<string>("RoomType");

            //configuring Ids
            modelBuilder.Entity<Room>().HasKey(r => r.Id);
            modelBuilder.Entity<Costumer>().HasKey(c => c.Id);
            modelBuilder.Entity<Order>().HasKey(o => o.Id);

            //configuring one-to-one relations
            modelBuilder.Entity<Costumer>()
                .HasMany<Order>(c => c.Orders)
                .WithOne(o => o.Costumer)
                .HasForeignKey(o => o.CostumerId);

            //configuring one-to-many relations
            modelBuilder.Entity<Room>()
                .HasMany<OrderRoom>(r => r.OrderRooms)
                .WithOne(o => o.Room);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderRooms)
                .WithOne(r => r.Order);

            modelBuilder.Entity<OrderRoom>()
                .HasOne<Order>(or => or.Order)
                .WithMany(o => o.OrderRooms);

            modelBuilder.Entity<OrderRoom>()
                .HasOne<Room>(or => or.Room)
                .WithMany(r => r.OrderRooms);
        }
    }
}
