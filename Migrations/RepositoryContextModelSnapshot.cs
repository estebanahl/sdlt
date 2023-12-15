﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using sdlt.Repository;

#nullable disable

namespace backEnd.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("sdlt.Entities.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("category");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            Description = "",
                            Name = "Ensaldas"
                        },
                        new
                        {
                            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            Description = "",
                            Name = "Carnes"
                        });
                });

            modelBuilder.Entity("sdlt.Entities.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.ToTable("product");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            Active = true,
                            CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            Description = "Ensalada rusa con mayonesa natura y un poco de cebolla",
                            Name = "Ensalada Rusa"
                        },
                        new
                        {
                            Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                            Active = true,
                            CategoryId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            Description = "Ensalada de repollo con vinagre abadía",
                            Name = "Ensalada de repollo"
                        },
                        new
                        {
                            Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                            Active = true,
                            CategoryId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            Description = "Pernil de cerdo fileteado curado por 2 años",
                            Name = "Pernil de cerdo fileteado"
                        });
                });

            modelBuilder.Entity("sdlt.Entities.Models.Product", b =>
                {
                    b.HasOne("sdlt.Entities.Models.Category", "Category")
                        .WithOne("Products")
                        .HasForeignKey("sdlt.Entities.Models.Product", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("sdlt.Entities.Models.Category", b =>
                {
                    b.Navigation("Products")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
