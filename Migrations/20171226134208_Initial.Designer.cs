﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using toolservice.Data;

namespace toolservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171226134208_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("toolservice.Model.Tool", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("currentLife");

                    b.Property<string>("description")
                        .HasMaxLength(100);

                    b.Property<double>("lifeCycle");

                    b.Property<string>("name")
                        .HasMaxLength(50);

                    b.Property<string>("status")
                        .IsRequired();

                    b.Property<int>("typeid");

                    b.Property<string>("unitOfMeasurement")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("typeid");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("toolservice.Model.ToolType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description")
                        .HasMaxLength(100);

                    b.Property<string>("name")
                        .HasMaxLength(50);

                    b.Property<string>("status")
                        .IsRequired();

                    b.Property<int[]>("thingGroupIds")
                        .HasColumnName("thingGroupIds")
                        .HasColumnType("integer[]");

                    b.HasKey("id");

                    b.ToTable("ToolTypes");
                });

            modelBuilder.Entity("toolservice.Model.Tool", b =>
                {
                    b.HasOne("toolservice.Model.ToolType", "type")
                        .WithMany()
                        .HasForeignKey("typeid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
