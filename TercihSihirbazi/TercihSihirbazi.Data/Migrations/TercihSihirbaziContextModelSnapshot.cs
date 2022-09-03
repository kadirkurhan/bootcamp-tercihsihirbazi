﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Context;

#nullable disable

namespace TercihSihirbazi.Data.Migrations
{
    [DbContext(typeof(TercihSihirbaziContext))]
    partial class TercihSihirbaziContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AppUserDetailObject", b =>
                {
                    b.Property<int>("AppUserFavoritesId")
                        .HasColumnType("integer");

                    b.Property<int>("FavoritedAppUsersId")
                        .HasColumnType("integer");

                    b.HasKey("AppUserFavoritesId", "FavoritedAppUsersId");

                    b.HasIndex("FavoritedAppUsersId");

                    b.ToTable("AppUserDetailObject");
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.AppUserFavorites", b =>
                {
                    b.Property<int>("AppUserId")
                        .HasColumnType("integer");

                    b.Property<int>("DetailObjectId")
                        .HasColumnType("integer");

                    b.HasKey("AppUserId", "DetailObjectId");

                    b.HasIndex("DetailObjectId");

                    b.ToTable("AppUserFavorites");
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.AppUserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppRoleId")
                        .HasColumnType("integer");

                    b.Property<int>("AppUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppRoleId");

                    b.HasIndex("AppUserId", "AppRoleId")
                        .IsUnique();

                    b.ToTable("AppUserRoles");
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.DetailObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FakulteAdi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProgramAdi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProgramKodu")
                        .HasColumnType("integer");

                    b.Property<string>("PuanTuru")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UniversiteAdi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UniversiteTuru")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Year2018")
                        .HasColumnType("text");

                    b.Property<string>("Year2019")
                        .HasColumnType("text");

                    b.Property<string>("Year2020")
                        .HasColumnType("text");

                    b.Property<string>("Year2021")
                        .HasColumnType("text");

                    b.Property<string>("Year2022")
                        .HasColumnType("text");

                    b.Property<string>("Year2023")
                        .HasColumnType("text");

                    b.Property<int>("Yerlesen")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ExcelData");
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("AppUserDetailObject", b =>
                {
                    b.HasOne("TercihSihirbazi.Entities.Concrete.DetailObject", null)
                        .WithMany()
                        .HasForeignKey("AppUserFavoritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TercihSihirbazi.Entities.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("FavoritedAppUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.AppUserFavorites", b =>
                {
                    b.HasOne("TercihSihirbazi.Entities.Concrete.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TercihSihirbazi.Entities.Concrete.DetailObject", "Favorited")
                        .WithMany()
                        .HasForeignKey("DetailObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Favorited");
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.AppUserRole", b =>
                {
                    b.HasOne("TercihSihirbazi.Entities.Concrete.AppRole", "AppRole")
                        .WithMany("AppUserRoles")
                        .HasForeignKey("AppRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TercihSihirbazi.Entities.Concrete.AppUser", "AppUser")
                        .WithMany("AppUserRoles")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppRole");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.AppRole", b =>
                {
                    b.Navigation("AppUserRoles");
                });

            modelBuilder.Entity("TercihSihirbazi.Entities.Concrete.AppUser", b =>
                {
                    b.Navigation("AppUserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
