﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlanYourTrip_BackEnd.Data;

#nullable disable

namespace PlanYourTrip_BackEnd.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220519140349_TripPlanTable_AddNullable")]
    partial class TripPlanTable_AddNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ContributorsTripPlans", b =>
                {
                    b.Property<int>("ContributorsContributorId")
                        .HasColumnType("int");

                    b.Property<int>("TripPlansTripPlanId")
                        .HasColumnType("int");

                    b.HasKey("ContributorsContributorId", "TripPlansTripPlanId");

                    b.HasIndex("TripPlansTripPlanId");

                    b.ToTable("ContributorsTripPlans");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.Answers", b =>
                {
                    b.Property<int>("IdAnswer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAnswer"), 1L, 1);

                    b.Property<int?>("AutorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Tresc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAnswer");

                    b.HasIndex("AutorId");

                    b.HasIndex("PostId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.Contributors", b =>
                {
                    b.Property<int>("ContributorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContributorId"), 1L, 1);

                    b.Property<int>("UserI_FK")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ContributorId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Contributors");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.Posts", b =>
                {
                    b.Property<int>("IdPost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPost"), 1L, 1);

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("Polubienia")
                        .HasColumnType("int");

                    b.Property<string>("Tresc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tytul")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZdjeciaJSON")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPost");

                    b.HasIndex("AutorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.TripPlans", b =>
                {
                    b.Property<int>("TripPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TripPlanId"), 1L, 1);

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PunktyJSON")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TripPlanId");

                    b.HasIndex("AutorId");

                    b.ToTable("TripPlans");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(4);

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(5);

                    b.Property<int>("IdAvatar")
                        .HasColumnType("int")
                        .HasColumnOrder(7);

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<string>("Nick")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(6);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ContributorsTripPlans", b =>
                {
                    b.HasOne("PlanYourTrip_ClassLibrary.Classes.Contributors", null)
                        .WithMany()
                        .HasForeignKey("ContributorsContributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlanYourTrip_ClassLibrary.Classes.TripPlans", null)
                        .WithMany()
                        .HasForeignKey("TripPlansTripPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.Answers", b =>
                {
                    b.HasOne("PlanYourTrip_ClassLibrary.Classes.Users", "User")
                        .WithMany("Answers")
                        .HasForeignKey("AutorId");

                    b.HasOne("PlanYourTrip_ClassLibrary.Classes.Posts", "Post")
                        .WithMany("Odpowiedzi")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.Contributors", b =>
                {
                    b.HasOne("PlanYourTrip_ClassLibrary.Classes.Users", "User")
                        .WithOne("Contributors")
                        .HasForeignKey("PlanYourTrip_ClassLibrary.Classes.Contributors", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.Posts", b =>
                {
                    b.HasOne("PlanYourTrip_ClassLibrary.Classes.Users", "User")
                        .WithMany("Posts")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.TripPlans", b =>
                {
                    b.HasOne("PlanYourTrip_ClassLibrary.Classes.Users", "Users")
                        .WithMany("TripPlans")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.Posts", b =>
                {
                    b.Navigation("Odpowiedzi");
                });

            modelBuilder.Entity("PlanYourTrip_ClassLibrary.Classes.Users", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Contributors")
                        .IsRequired();

                    b.Navigation("Posts");

                    b.Navigation("TripPlans");
                });
#pragma warning restore 612, 618
        }
    }
}
