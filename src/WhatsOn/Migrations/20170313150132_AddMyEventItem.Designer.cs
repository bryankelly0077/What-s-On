﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WhatsOn.Models;

namespace WhatsOn.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170313150132_AddMyEventItem")]
    partial class AddMyEventItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WhatsOn.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.Property<string>("Description");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WhatsOn.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("EndDateTime");

                    b.Property<string>("EventDescription");

                    b.Property<string>("ImageThumbnailUrl");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsEventOfTheWeek");

                    b.Property<string>("StartDateTime");

                    b.Property<string>("Title");

                    b.HasKey("EventId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("WhatsOn.Models.MyEventItem", b =>
                {
                    b.Property<int>("MyEventItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int?>("EventId");

                    b.Property<string>("MyEventId");

                    b.HasKey("MyEventItemId");

                    b.HasIndex("EventId");

                    b.ToTable("MyEventItems");
                });

            modelBuilder.Entity("WhatsOn.Models.Event", b =>
                {
                    b.HasOne("WhatsOn.Models.Category", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WhatsOn.Models.MyEventItem", b =>
                {
                    b.HasOne("WhatsOn.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");
                });
        }
    }
}
