﻿// <auto-generated />
using System;
using Contact.API.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Contact.API.Migrations
{
    [DbContext(typeof(ContactDbContext))]
    [Migration("20220821142216_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Contact.API.Models.Entity.ContactInformation", b =>
                {
                    b.Property<Guid>("uuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("informationContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("informationType")
                        .HasColumnType("integer");

                    b.Property<Guid>("personUuId")
                        .HasColumnType("uuid");

                    b.HasKey("uuId");

                    b.HasIndex("personUuId");

                    b.ToTable("ContactInformations");
                });

            modelBuilder.Entity("Contact.API.Models.Entity.Person", b =>
                {
                    b.Property<Guid>("uuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("company")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("uuId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Contact.API.Models.Entity.ContactInformation", b =>
                {
                    b.HasOne("Contact.API.Models.Entity.Person", null)
                        .WithMany("contactInformation")
                        .HasForeignKey("personUuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact.API.Models.Entity.Person", b =>
                {
                    b.Navigation("contactInformation");
                });
#pragma warning restore 612, 618
        }
    }
}