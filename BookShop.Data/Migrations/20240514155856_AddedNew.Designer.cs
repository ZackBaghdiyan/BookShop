﻿// <auto-generated />
using System;
using BookShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookShop.Data.Migrations
{
    [DbContext(typeof(BookShopDbContext))]
    [Migration("20240514155856_AddedNew")]
    partial class AddedNew
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookShop.Data.Entities.CartEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("BookShop.Data.Entities.CartItemEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CartId")
                        .HasColumnType("bigint");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId", "CartId")
                        .IsUnique();

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("BookShop.Data.Entities.ClientEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BookShop.Data.Entities.InvoiceEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("boolean");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long>("PaymentId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("BookShop.Data.Entities.OrderEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProductId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookShop.Data.Entities.PaymentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<long>("PaymentMethodId")
                        .HasColumnType("bigint");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("BookShop.Data.Entities.PaymentMethodEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("BookShop.Data.Entities.ProductEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BookShop.Data.Entities.WishListEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("BookShop.Data.Entities.WishListItemEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("ClientEntityId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long>("WishListId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClientEntityId");

                    b.HasIndex("WishListId");

                    b.HasIndex("ProductId", "WishListId")
                        .IsUnique();

                    b.ToTable("WishListItems");
                });

            modelBuilder.Entity("BookShop.Data.Entities.CartEntity", b =>
                {
                    b.HasOne("BookShop.Data.Entities.ClientEntity", "Client")
                        .WithOne("CartEntity")
                        .HasForeignKey("BookShop.Data.Entities.CartEntity", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BookShop.Data.Entities.CartItemEntity", b =>
                {
                    b.HasOne("BookShop.Data.Entities.CartEntity", "CartEntity")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Entities.ProductEntity", "ProductEntity")
                        .WithMany("CartItemEntity")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartEntity");

                    b.Navigation("ProductEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.InvoiceEntity", b =>
                {
                    b.HasOne("BookShop.Data.Entities.ClientEntity", "ClientEntity")
                        .WithMany("Invoices")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Entities.OrderEntity", "OrderEntity")
                        .WithOne("InvoiceEntity")
                        .HasForeignKey("BookShop.Data.Entities.InvoiceEntity", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Entities.PaymentEntity", "PaymentEntity")
                        .WithOne("InvoiceEntity")
                        .HasForeignKey("BookShop.Data.Entities.InvoiceEntity", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientEntity");

                    b.Navigation("OrderEntity");

                    b.Navigation("PaymentEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.OrderEntity", b =>
                {
                    b.HasOne("BookShop.Data.Entities.ClientEntity", "ClientEntity")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Entities.ProductEntity", "ProductEntity")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientEntity");

                    b.Navigation("ProductEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.PaymentEntity", b =>
                {
                    b.HasOne("BookShop.Data.Entities.PaymentMethodEntity", "PaymentMethodEntity")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethodEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.PaymentMethodEntity", b =>
                {
                    b.HasOne("BookShop.Data.Entities.ClientEntity", "ClientEntity")
                        .WithMany("PaymentMethods")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.WishListEntity", b =>
                {
                    b.HasOne("BookShop.Data.Entities.ClientEntity", "ClientEntity")
                        .WithOne("WishListEntity")
                        .HasForeignKey("BookShop.Data.Entities.WishListEntity", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.WishListItemEntity", b =>
                {
                    b.HasOne("BookShop.Data.Entities.ClientEntity", "ClientEntity")
                        .WithMany()
                        .HasForeignKey("ClientEntityId");

                    b.HasOne("BookShop.Data.Entities.ProductEntity", "ProductEntity")
                        .WithMany("WishListItemEntity")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookShop.Data.Entities.WishListEntity", "WishListEntity")
                        .WithMany("WishListItems")
                        .HasForeignKey("WishListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientEntity");

                    b.Navigation("ProductEntity");

                    b.Navigation("WishListEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.CartEntity", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("BookShop.Data.Entities.ClientEntity", b =>
                {
                    b.Navigation("CartEntity");

                    b.Navigation("Invoices");

                    b.Navigation("Orders");

                    b.Navigation("PaymentMethods");

                    b.Navigation("WishListEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.OrderEntity", b =>
                {
                    b.Navigation("InvoiceEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.PaymentEntity", b =>
                {
                    b.Navigation("InvoiceEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.PaymentMethodEntity", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("BookShop.Data.Entities.ProductEntity", b =>
                {
                    b.Navigation("CartItemEntity");

                    b.Navigation("Orders");

                    b.Navigation("WishListItemEntity");
                });

            modelBuilder.Entity("BookShop.Data.Entities.WishListEntity", b =>
                {
                    b.Navigation("WishListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
