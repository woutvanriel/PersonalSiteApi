﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalSiteApi.EntityFramework;

#nullable disable

namespace PersonalSiteApi.Migrations
{
    [DbContext(typeof(PersonalSiteContext))]
    [Migration("20231016203152_Ordering")]
    partial class Ordering
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.LanguageDB", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Flag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.PageContentDB", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProjectDetailsDBId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DetailsId");

                    b.HasIndex("ProjectDetailsDBId");

                    b.ToTable("PageContent");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.PageDB", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.PageDetailDB", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PageId");

                    b.ToTable("PageDetails");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.ProjectContentDB", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProjectDBId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DetailsId");

                    b.HasIndex("ProjectDBId");

                    b.ToTable("ProjectContent");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.ProjectDB", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.ProjectDetailsDB", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectDetailsDB");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.PageContentDB", b =>
                {
                    b.HasOne("PersonalSiteApi.EntityFramework.Classes.PageDetailDB", "Details")
                        .WithMany("Content")
                        .HasForeignKey("DetailsId");

                    b.HasOne("PersonalSiteApi.EntityFramework.Classes.ProjectDetailsDB", null)
                        .WithMany("Content")
                        .HasForeignKey("ProjectDetailsDBId");

                    b.Navigation("Details");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.PageDetailDB", b =>
                {
                    b.HasOne("PersonalSiteApi.EntityFramework.Classes.LanguageDB", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("PersonalSiteApi.EntityFramework.Classes.PageDB", "Page")
                        .WithMany("Details")
                        .HasForeignKey("PageId");

                    b.Navigation("Language");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.ProjectContentDB", b =>
                {
                    b.HasOne("PersonalSiteApi.EntityFramework.Classes.ProjectDetailsDB", "Details")
                        .WithMany()
                        .HasForeignKey("DetailsId");

                    b.HasOne("PersonalSiteApi.EntityFramework.Classes.ProjectDB", null)
                        .WithMany("Content")
                        .HasForeignKey("ProjectDBId");

                    b.Navigation("Details");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.ProjectDetailsDB", b =>
                {
                    b.HasOne("PersonalSiteApi.EntityFramework.Classes.LanguageDB", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("PersonalSiteApi.EntityFramework.Classes.ProjectDB", "Project")
                        .WithMany("Details")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Language");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.PageDB", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.PageDetailDB", b =>
                {
                    b.Navigation("Content");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.ProjectDB", b =>
                {
                    b.Navigation("Content");

                    b.Navigation("Details");
                });

            modelBuilder.Entity("PersonalSiteApi.EntityFramework.Classes.ProjectDetailsDB", b =>
                {
                    b.Navigation("Content");
                });
#pragma warning restore 612, 618
        }
    }
}
