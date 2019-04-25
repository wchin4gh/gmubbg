﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VOAprototype.Models;

namespace VOAprototype.Migrations
{
    [DbContext(typeof(VOAprototypeContext))]
    [Migration("20190417015130_made_fields_required")]
    partial class made_fields_required
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VOAprototype.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Application")
                        .IsRequired();

                    b.Property<DateTime>("ApprovalDate");

                    b.Property<string>("BusinessFunction")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("EgovBRM");

                    b.Property<string>("Entity")
                        .IsRequired();

                    b.Property<DateTime>("EntryDate");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<string>("Finance");

                    b.Property<string>("ITFunction")
                        .IsRequired();

                    b.Property<string>("ITTower")
                        .IsRequired();

                    b.Property<int?>("SeatsPerLicense");

                    b.Property<int?>("SeatsUsed");

                    b.Property<string>("TBMCategory");

                    b.Property<string>("TBMITService");

                    b.Property<string>("TBMName");

                    b.Property<decimal>("UnitPrice");

                    b.Property<int>("Units");

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrder");
                });
#pragma warning restore 612, 618
        }
    }
}
