﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPICore.Model;

namespace WebAPICore.Migrations
{
    [DbContext(typeof(EduModel))]
    [Migration("20190424132046_core")]
    partial class core
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Models.Instructor", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("Salary");

                    b.HasKey("Id");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("WebAPI.Models.Login", b =>
                {
                    b.Property<string>("UserName")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.HasKey("UserName");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("WebAPI.Models.Student", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("Age");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("WebAPI.Models.StudentSubject", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("Subj_Id");

                    b.HasKey("Id", "Subj_Id");

                    b.HasIndex("Subj_Id");

                    b.ToTable("StudentsSubjects");
                });

            modelBuilder.Entity("WebAPI.Models.Subject", b =>
                {
                    b.Property<int>("Subj_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Subj_Id");

                    b.HasIndex("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WebAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebAPI.Models.Instructor", b =>
                {
                    b.HasOne("WebAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Student", b =>
                {
                    b.HasOne("WebAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.StudentSubject", b =>
                {
                    b.HasOne("WebAPI.Models.Student", "Student")
                        .WithMany("StudentsSubjects")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.Subject", "Subject")
                        .WithMany("StudentsSubjects")
                        .HasForeignKey("Subj_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Subject", b =>
                {
                    b.HasOne("WebAPI.Models.Instructor", "Instructor")
                        .WithMany("Subjects")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
