﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250114171146_ChangeSeedUsers")]
    partial class ChangeSeedUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Authentications.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5d4b0be2-bb99-4122-84d4-5cd2a82d02bf"),
                            Name = "Regular"
                        },
                        new
                        {
                            Id = new Guid("d89e88ef-bf41-466a-8120-4be1c51b64fb"),
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Domain.Authentications.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("8f47154f-be3e-4e82-a013-07c7d2cb7f70"),
                            Email = "admin@example.com",
                            Name = "Admin",
                            PasswordHash = "hjN2JDjGD8nhsUZyIGOa7w==:Zi8Amp0D5sZw1zbBMmkZU0ft6z3bWIIOD6AqLPzggFk="
                        },
                        new
                        {
                            Id = new Guid("eeec799f-14c7-4ee4-b625-74da815e2313"),
                            Email = "user@example.com",
                            Name = "Regular",
                            PasswordHash = "ShNjoDzo7v7n6Wzb9z3Rtw==:LPGjWpb5RTq5WMaA7QptYZY3ksQQRWk55zmN5eWrXMA="
                        });
                });

            modelBuilder.Entity("Domain.Urls.Url", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("original_url");

                    b.Property<string>("ShortenedUrl")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("shortened_url");

                    b.HasKey("Id")
                        .HasName("pk_urls");

                    b.HasIndex("CreatedBy")
                        .HasDatabaseName("ix_urls_created_by");

                    b.ToTable("urls", (string)null);
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid")
                        .HasColumnName("roles_id");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid")
                        .HasColumnName("users_id");

                    b.HasKey("RolesId", "UsersId")
                        .HasName("pk_user_roles");

                    b.HasIndex("UsersId")
                        .HasDatabaseName("ix_user_roles_users_id");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            RolesId = new Guid("114b75b9-6a1c-4d3d-942f-0d46146e82af"),
                            UsersId = new Guid("8f47154f-be3e-4e82-a013-07c7d2cb7f70")
                        },
                        new
                        {
                            RolesId = new Guid("5c0db531-0267-4d96-a941-eb8c56b81f86"),
                            UsersId = new Guid("eeec799f-14c7-4ee4-b625-74da815e2313")
                        });
                });

            modelBuilder.Entity("Domain.Urls.Url", b =>
                {
                    b.HasOne("Domain.Authentications.Users.User", null)
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_urls_users_created_by");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Domain.Authentications.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_roles_roles_id");

                    b.HasOne("Domain.Authentications.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_users_users_id");
                });
#pragma warning restore 612, 618
        }
    }
}
