﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Report.API.Models.Context;

#nullable disable

namespace Report.API.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    [Migration("20220821134222_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Report.API.Entities.Report", b =>
                {
                    b.Property<Guid>("uuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("reportStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("requestDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("uuId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Report.API.Models.Entity.ReportDetail", b =>
                {
                    b.Property<Guid>("uuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("personCount")
                        .HasColumnType("integer");

                    b.Property<int>("phoneNumberCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("reportUuId")
                        .HasColumnType("uuid");

                    b.HasKey("uuId");

                    b.HasIndex("reportUuId");

                    b.ToTable("ReportDetails");
                });

            modelBuilder.Entity("Report.API.Models.Entity.ReportDetail", b =>
                {
                    b.HasOne("Report.API.Entities.Report", null)
                        .WithMany("reportDetails")
                        .HasForeignKey("reportUuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Report.API.Entities.Report", b =>
                {
                    b.Navigation("reportDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
