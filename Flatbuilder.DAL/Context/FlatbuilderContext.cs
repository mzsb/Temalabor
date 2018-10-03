﻿using Flatbuilder.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flatbuilder.DAL.Context
{
    //tools-->nuget package manager-->package manager console

    //add-migration <nev> ha valamit valtoztatni kell a strukturan
    //update-database hogy a jelenlegit lehuzzatok a sajat gepetekre
    //View-->Sql Server Object Explorer ebben kell lenni valahol ha sikerult az update
    public class FlatbuilderContext : DbContext
    {
        
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Costumer> Costumers { get; set; }        
        public DbSet<Order> Orders { get; set; }
                
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

            //Tulajdonkeppen egyik sem kene ide feltetlenul csak a constraintek ha vannak, mert felvettuk az Id-ket meg ilyneket es az EFCore 
            //konvencio alapjan is ki tudna talalni de jobb biztosra menni

            //configuring inheritance
            modelBuilder.Entity<Bedroom>().HasBaseType<Room>();
            modelBuilder.Entity<Kitchen>().HasBaseType<Room>();
            modelBuilder.Entity<Shower>().HasBaseType<Room>();

            //configuring Ids
            modelBuilder.Entity<Room>().HasKey(r => r.Id);
            modelBuilder.Entity<Costumer>().HasKey(c => c.Id);
            modelBuilder.Entity<Order>().HasKey(o => o.Id);

            //configuring one-to-one relations
            modelBuilder.Entity<Order>()
                .HasOne<Costumer>(o => o.Costumer)
                .WithOne(c => c.Order)
                .HasForeignKey<Costumer>(c => c.OrderId);

            //configuring one-to-many relations
            modelBuilder.Entity<Room>()
                .HasOne<Order>(r => r.Order)
                .WithMany(ro => ro.Rooms)
                .HasForeignKey(r => r.OrderId);
            
            modelBuilder.Entity<Order>()
                .HasMany<Room>(o => o.Rooms)
                .WithOne();            
        }        
    }
}
