﻿// <auto-generated />
using System;
using AgaSoft.Client.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgaSoft.Client.Model.Migrations
{
    [DbContext(typeof(AgaSoftRepositoryContext))]
    [Migration("20201018205700_AddRolesTale")]
    partial class AddRolesTale
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AgaSoft.Client.Model.Entities.Roles", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("UserRolesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserRolesId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AgaSoft.Client.Model.Entities.UserRoles", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("AgaSoft.Client.Model.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int?>("RolesId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RolesId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AgaSoft.Client.Model.Entities.Roles", b =>
                {
                    b.HasOne("AgaSoft.Client.Model.Entities.UserRoles", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserRolesId");
                });

            modelBuilder.Entity("AgaSoft.Client.Model.Entities.Users", b =>
                {
                    b.HasOne("AgaSoft.Client.Model.Entities.UserRoles", "Roles")
                        .WithMany("Users")
                        .HasForeignKey("RolesId");
                });
#pragma warning restore 612, 618
        }
    }
}