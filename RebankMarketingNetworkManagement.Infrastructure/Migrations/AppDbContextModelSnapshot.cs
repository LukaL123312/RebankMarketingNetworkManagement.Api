﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RebankMarketingNetworkManagement.Infrastructure;

#nullable disable

namespace RebankMarketingNetworkManagement.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.Distributor", b =>
                {
                    b.Property<Guid>("DistributorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecommendedCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("RecommenderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("DistributorID");

                    b.HasIndex("RecommenderID");

                    b.ToTable("Distributor", "dbo");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorAddressInformation", b =>
                {
                    b.Property<Guid>("DistributorAddressInformationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("AddressType")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("DistributorID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DistributorAddressInformationID");

                    b.HasIndex("DistributorID")
                        .IsUnique();

                    b.ToTable("DistributorAddressInformation", "dbo");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorBonus", b =>
                {
                    b.Property<Guid>("DistributorBonusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BonusDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<decimal?>("DailyFirstGenRecommendationBonusAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DailyIndividualBonusAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DailySaleAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DailySecondGenRecommendationBonusAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DistributorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DistributorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DistributorSurname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("DistributorBonusID");

                    b.HasIndex("DistributorID");

                    b.ToTable("DistributorBonus", "dbo");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorContactInformation", b =>
                {
                    b.Property<Guid>("DistributorContactInformationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactInformation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ContactType")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("DistributorID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DistributorContactInformationID");

                    b.HasIndex("DistributorID")
                        .IsUnique();

                    b.ToTable("DistributorContactInformation", "dbo");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorPrivateDocumentInformation", b =>
                {
                    b.Property<Guid>("DistributorPrivateDocumentInformationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("DistributorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuerOrganization")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("IssuingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("PrivateNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("DistributorPrivateDocumentInformationID");

                    b.HasIndex("DistributorID")
                        .IsUnique();

                    b.ToTable("DistributorPrivateDocumentInformation", "dbo");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorSale", b =>
                {
                    b.Property<Guid>("DistributorSaleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DistributorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCountedForBonusCalculation")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SumSalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("DistributorSaleID");

                    b.HasIndex("DistributorID");

                    b.HasIndex("ProductID");

                    b.ToTable("DistributorSale", "dbo");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductID");

                    b.ToTable("Product", "dbo");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Id");

                    b.ToTable("User", "dbo");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.Distributor", b =>
                {
                    b.HasOne("RebankMarketingNetworkManagement.Domain.Distributor", "Recommender")
                        .WithMany("RecommendedDistributors")
                        .HasForeignKey("RecommenderID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Recommender");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorAddressInformation", b =>
                {
                    b.HasOne("RebankMarketingNetworkManagement.Domain.Distributor", "Distributor")
                        .WithOne("AddressInformation")
                        .HasForeignKey("RebankMarketingNetworkManagement.Domain.DistributorAddressInformation", "DistributorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorBonus", b =>
                {
                    b.HasOne("RebankMarketingNetworkManagement.Domain.Distributor", "Distributor")
                        .WithMany("DistributorBonus")
                        .HasForeignKey("DistributorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorContactInformation", b =>
                {
                    b.HasOne("RebankMarketingNetworkManagement.Domain.Distributor", "Distributor")
                        .WithOne("ContactInformation")
                        .HasForeignKey("RebankMarketingNetworkManagement.Domain.DistributorContactInformation", "DistributorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorPrivateDocumentInformation", b =>
                {
                    b.HasOne("RebankMarketingNetworkManagement.Domain.Distributor", "Distributor")
                        .WithOne("PrivateDocumentInformation")
                        .HasForeignKey("RebankMarketingNetworkManagement.Domain.DistributorPrivateDocumentInformation", "DistributorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.DistributorSale", b =>
                {
                    b.HasOne("RebankMarketingNetworkManagement.Domain.Distributor", "Distributor")
                        .WithMany("DistributorSales")
                        .HasForeignKey("DistributorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RebankMarketingNetworkManagement.Domain.Product", "Product")
                        .WithMany("DistributorSales")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Distributor");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.Distributor", b =>
                {
                    b.Navigation("AddressInformation")
                        .IsRequired();

                    b.Navigation("ContactInformation")
                        .IsRequired();

                    b.Navigation("DistributorBonus");

                    b.Navigation("DistributorSales");

                    b.Navigation("PrivateDocumentInformation")
                        .IsRequired();

                    b.Navigation("RecommendedDistributors");
                });

            modelBuilder.Entity("RebankMarketingNetworkManagement.Domain.Product", b =>
                {
                    b.Navigation("DistributorSales");
                });
#pragma warning restore 612, 618
        }
    }
}
