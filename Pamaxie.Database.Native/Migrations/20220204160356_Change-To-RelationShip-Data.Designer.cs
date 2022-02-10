﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pamaxie.Database.Native;

#nullable disable

namespace Pamaxie.Database.Native.Migrations
{
    [DbContext(typeof(PgSqlContext))]
    [Migration("20220204160356_Change-To-RelationShip-Data")]
    partial class ChangeToRelationShipData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.ApiKey", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("id");

                    b.Property<string>("CredentialHash")
                        .HasColumnType("text")
                        .HasColumnName("credential_hash");

                    b.Property<decimal?>("ProjectId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("project_id");

                    b.Property<DateTime?>("TTL")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ttl");

                    b.HasKey("Id")
                        .HasName("pk_api_keys");

                    b.HasIndex("ProjectId")
                        .HasDatabaseName("ix_api_keys_project_id");

                    b.ToTable("api_keys", (string)null);
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.KnownUserIp", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("id");

                    b.Property<string>("IpAddress")
                        .HasColumnType("text")
                        .HasColumnName("ip_address");

                    b.Property<DateTime?>("TTL")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ttl");

                    b.Property<decimal?>("UserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_known_user_ips");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_known_user_ips_user_id");

                    b.ToTable("known_user_ips", (string)null);
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.Project", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<int>("Flags")
                        .HasColumnType("integer")
                        .HasColumnName("flags");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified");

                    b.Property<decimal>("LastModifiedUserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("last_modified_user_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("OwnerId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("owner_id");

                    b.Property<DateTime?>("TTL")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ttl");

                    b.HasKey("Id")
                        .HasName("pk_projects");

                    b.HasIndex("TTL")
                        .HasDatabaseName("ix_projects_ttl");

                    b.ToTable("projects", (string)null);
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.ProjectUser", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("id");

                    b.Property<int>("Permissions")
                        .HasColumnType("integer")
                        .HasColumnName("permissions");

                    b.Property<decimal?>("ProjectId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("project_id");

                    b.Property<DateTime?>("TTL")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ttl");

                    b.Property<decimal?>("UserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_project_users");

                    b.HasIndex("ProjectId")
                        .HasDatabaseName("ix_project_users_project_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_project_users_user_id");

                    b.ToTable("project_users", (string)null);
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.TwoFactorUser", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("id");

                    b.Property<string>("PublicKey")
                        .HasColumnType("text")
                        .HasColumnName("public_key");

                    b.Property<DateTime?>("TTL")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ttl");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<decimal?>("UserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_two_factor_users");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_two_factor_users_user_id");

                    b.ToTable("two_factor_users", (string)null);
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.User", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<int>("Flags")
                        .HasColumnType("integer")
                        .HasColumnName("flags");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<DateTime?>("TTL")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ttl");

                    b.Property<string>("Username")
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("TTL")
                        .HasDatabaseName("ix_users_ttl");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.ApiKey", b =>
                {
                    b.HasOne("Pamaxie.Database.Native.Sql.Project", "Project")
                        .WithMany("ApiKeys")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("fk_api_keys_projects_project_id");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.KnownUserIp", b =>
                {
                    b.HasOne("Pamaxie.Database.Native.Sql.User", "User")
                        .WithMany("KnownIps")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_known_user_ips_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.ProjectUser", b =>
                {
                    b.HasOne("Pamaxie.Database.Native.Sql.Project", "Project")
                        .WithMany("Users")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("fk_project_users_projects_project_id");

                    b.HasOne("Pamaxie.Database.Native.Sql.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_project_users_users_user_id");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.TwoFactorUser", b =>
                {
                    b.HasOne("Pamaxie.Database.Native.Sql.User", "User")
                        .WithMany("TwoFactorAuths")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_two_factor_users_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.Project", b =>
                {
                    b.Navigation("ApiKeys");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Pamaxie.Database.Native.Sql.User", b =>
                {
                    b.Navigation("KnownIps");

                    b.Navigation("Projects");

                    b.Navigation("TwoFactorAuths");
                });
#pragma warning restore 612, 618
        }
    }
}
