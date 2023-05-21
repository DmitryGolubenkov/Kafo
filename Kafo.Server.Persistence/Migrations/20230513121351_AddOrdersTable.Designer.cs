﻿// <auto-generated />
using System;
using Kafo.Server.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kafo.Server.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230513121351_AddOrdersTable")]
    partial class AddOrdersTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kafo.Domain.Models.CoffeeMachineModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ManufacturerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CoffeeMachineModel");
                });

            modelBuilder.Entity("Kafo.Domain.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AcceptanceDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Appearance")
                        .HasColumnType("integer");

                    b.Property<bool>("CappuccinatorHose")
                        .HasColumnType("boolean");

                    b.Property<bool>("CappuccinatorNozzle")
                        .HasColumnType("boolean");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ClientPhonePrimary")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ClientPhoneSecondary")
                        .HasColumnType("text");

                    b.Property<bool>("CoffeeLid")
                        .HasColumnType("boolean");

                    b.Property<Guid>("CoffeeMachineId")
                        .HasColumnType("uuid");

                    b.Property<string>("EmployeePhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Filter")
                        .HasColumnType("boolean");

                    b.Property<bool>("HotWaterNozzle")
                        .HasColumnType("boolean");

                    b.Property<string>("Malfunction")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("MilkKettle")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OrderFinishDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("OrderNumber")
                        .HasColumnType("integer");

                    b.Property<string>("OtherText")
                        .HasColumnType("text");

                    b.Property<bool>("Pallet")
                        .HasColumnType("boolean");

                    b.Property<bool>("PowerCord")
                        .HasColumnType("boolean");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("WarrantyBefore")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("WasteTray")
                        .HasColumnType("boolean");

                    b.Property<bool>("WaterTank")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CoffeeMachineId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Kafo.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("Token")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Kafo.Domain.Models.Order", b =>
                {
                    b.HasOne("Kafo.Domain.Models.CoffeeMachineModel", "CoffeeMachine")
                        .WithMany()
                        .HasForeignKey("CoffeeMachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoffeeMachine");
                });
#pragma warning restore 612, 618
        }
    }
}
