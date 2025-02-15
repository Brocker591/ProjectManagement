﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TodoApi.Repositories;

#nullable disable

namespace TodoApi.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("20250213121952_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TodoApi.Models.Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Desciption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EditorUsers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResponsibleUser")
                        .HasColumnType("uuid");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Tenant")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TodoApi.Models.TodoStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TodoStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "open"
                        },
                        new
                        {
                            Id = 2,
                            Name = "doing"
                        },
                        new
                        {
                            Id = 3,
                            Name = "done"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
