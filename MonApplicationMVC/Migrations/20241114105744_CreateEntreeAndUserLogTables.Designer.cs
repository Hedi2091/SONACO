﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonApplicationMVC.Data;

#nullable disable

namespace MonApplicationMVC.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241114105744_CreateEntreeAndUserLogTables")]
    partial class CreateEntreeAndUserLogTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MonApplicationMVC.Models.Entree", b =>
                {
                    b.Property<string>("NumEntree")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CodeClient")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CodeFour")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateArr")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Largeur")
                        .HasColumnType("double");

                    b.Property<int>("NbRouleau")
                        .HasColumnType("int");

                    b.HasKey("NumEntree");

                    b.ToTable("Entree", (string)null);
                });

            modelBuilder.Entity("MonApplicationMVC.Models.User_log", b =>
                {
                    b.Property<int>("idUser_log")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("idUser_log"));

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("user")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("idUser_log");

                    b.ToTable("User_log", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
