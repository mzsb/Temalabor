﻿// <auto-generated />
using System;
using Flatbuilder.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Flatbuilder.DAL.Migrations
{
    [DbContext(typeof(FlatbuilderContext))]
    [Migration("20181003192847_FlatbuilderMigration1.2")]
    partial class FlatbuilderMigration12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Flatbuilder.DAL.Entities.Costumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.ToTable("Costumers");
                });

            modelBuilder.Entity("Flatbuilder.DAL.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CostumerId");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Location");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CostumerId")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Flatbuilder.DAL.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("OrderId");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Rooms");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Room");
                });

            modelBuilder.Entity("Flatbuilder.DAL.Entities.Bedroom", b =>
                {
                    b.HasBaseType("Flatbuilder.DAL.Entities.Room");


                    b.ToTable("Bedroom");

                    b.HasDiscriminator().HasValue("Bedroom");
                });

            modelBuilder.Entity("Flatbuilder.DAL.Entities.Kitchen", b =>
                {
                    b.HasBaseType("Flatbuilder.DAL.Entities.Room");


                    b.ToTable("Kitchen");

                    b.HasDiscriminator().HasValue("Kitchen");
                });

            modelBuilder.Entity("Flatbuilder.DAL.Entities.Shower", b =>
                {
                    b.HasBaseType("Flatbuilder.DAL.Entities.Room");


                    b.ToTable("Shower");

                    b.HasDiscriminator().HasValue("Shower");
                });

            modelBuilder.Entity("Flatbuilder.DAL.Entities.Order", b =>
                {
                    b.HasOne("Flatbuilder.DAL.Entities.Costumer", "Costumer")
                        .WithOne("Order")
                        .HasForeignKey("Flatbuilder.DAL.Entities.Order", "CostumerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Flatbuilder.DAL.Entities.Room", b =>
                {
                    b.HasOne("Flatbuilder.DAL.Entities.Order", "Order")
                        .WithMany("Rooms")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}