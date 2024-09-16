﻿// <auto-generated />
using System;
using BankApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("BankApp.Models.KundDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Efternamn")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Epost")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Förnamn")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Lösenord")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Personnummer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Postnummer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Postort")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tele")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Kunder");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cd7677b3-0f9b-4a90-95dc-fb2289adfdfb"),
                            Adress = "Knasgatan 1",
                            Efternamn = "Knasare",
                            Epost = "knaspelle.knasare@knas.se",
                            Förnamn = "Knaspelle",
                            IsAdmin = false,
                            Lösenord = "knaspass",
                            Personnummer = "1977-04-25",
                            Postnummer = "123 45",
                            Postort = "Knasby",
                            Tele = "070-123 45 67"
                        },
                        new
                        {
                            Id = new Guid("e942715d-6de8-4289-91c4-fe13f5a7588a"),
                            Adress = "Ankgatan 1",
                            Efternamn = "Ankare",
                            Epost = "ankpelle.ankare@ank.se",
                            Förnamn = "Ankpelle",
                            IsAdmin = false,
                            Lösenord = "ankpass",
                            Personnummer = "2011-09-11",
                            Postnummer = "543 21",
                            Postort = "Ankby",
                            Tele = "070-765 43 21"
                        },
                        new
                        {
                            Id = new Guid("6018c2ba-99b6-4c8f-8caa-7c240b7c6e47"),
                            Adress = "Testgatan 1",
                            Efternamn = "Testare",
                            Epost = "test.testare@testby.se",
                            Förnamn = "Test",
                            IsAdmin = false,
                            Lösenord = "pass",
                            Personnummer = "1111-11-11",
                            Postnummer = "111 11",
                            Postort = "Testby",
                            Tele = "111-111 11 11"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
