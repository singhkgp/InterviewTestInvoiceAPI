﻿// <auto-generated />
using System;
using InterviewTestInvoiceAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InterviewTestInvoiceAPI.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20250114151245_call-1")]
    partial class call1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InterviewTestInvoiceAPI.Models.InvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("TestInvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TestInvoiceId");

                    b.ToTable("InvoiceItem");
                });

            modelBuilder.Entity("InterviewTestInvoiceAPI.Models.TestInvoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TestInvoice");
                });

            modelBuilder.Entity("InterviewTestInvoiceAPI.Models.InvoiceItem", b =>
                {
                    b.HasOne("InterviewTestInvoiceAPI.Models.TestInvoice", null)
                        .WithMany("Items")
                        .HasForeignKey("TestInvoiceId");
                });

            modelBuilder.Entity("InterviewTestInvoiceAPI.Models.TestInvoice", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
