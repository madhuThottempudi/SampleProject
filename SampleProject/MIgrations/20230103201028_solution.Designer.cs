﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject;

namespace SampleProject.Migrations
{
    [DbContext(typeof(SolutionContext))]
    [Migration("20230103201028_solution")]
    partial class solution
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("SampleProject.Models.Answer", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("AnswerValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OptionsId")
                        .HasColumnType("int");

                    b.HasKey("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("SampleProject.Models.Option", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("OptionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionRefId")
                        .HasColumnType("int");

                    b.Property<string>("QuestionType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuestionRefId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("SampleProject.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionRefId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionRefId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SampleProject.Models.Option", b =>
                {
                    b.HasOne("SampleProject.Models.Answer", "Answer")
                        .WithMany()
                        .HasForeignKey("QuestionRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Answer");
                });

            modelBuilder.Entity("SampleProject.Models.Question", b =>
                {
                    b.HasOne("SampleProject.Models.Option", "Option")
                        .WithMany()
                        .HasForeignKey("QuestionRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Option");
                });
#pragma warning restore 612, 618
        }
    }
}
