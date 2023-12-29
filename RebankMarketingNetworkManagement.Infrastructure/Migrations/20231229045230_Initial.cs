using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RebankMarketingNetworkManagement.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Distributor",
                schema: "dbo",
                columns: table => new
                {
                    DistributorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommenderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecommendedCount = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributor", x => x.DistributorID);
                    table.ForeignKey(
                        name: "FK_Distributor_Distributor_RecommenderID",
                        column: x => x.RecommenderID,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "DistributorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributorAddressInformation",
                schema: "dbo",
                columns: table => new
                {
                    DistributorAddressInformationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistributorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorAddressInformation", x => x.DistributorAddressInformationID);
                    table.ForeignKey(
                        name: "FK_DistributorAddressInformation_Distributor_DistributorID",
                        column: x => x.DistributorID,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "DistributorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributorBonus",
                schema: "dbo",
                columns: table => new
                {
                    DistributorBonusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DailySaleAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DailyIndividualBonusAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DailyFirstGenRecommendationBonusAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DailySecondGenRecommendationBonusAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BonusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DistributorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistributorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DistributorSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorBonus", x => x.DistributorBonusID);
                    table.ForeignKey(
                        name: "FK_DistributorBonus_Distributor_DistributorID",
                        column: x => x.DistributorID,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "DistributorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistributorContactInformation",
                schema: "dbo",
                columns: table => new
                {
                    DistributorContactInformationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistributorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    ContactInformation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorContactInformation", x => x.DistributorContactInformationID);
                    table.ForeignKey(
                        name: "FK_DistributorContactInformation_Distributor_DistributorID",
                        column: x => x.DistributorID,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "DistributorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributorPrivateDocumentInformation",
                schema: "dbo",
                columns: table => new
                {
                    DistributorPrivateDocumentInformationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistributorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IssuingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrivateNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuerOrganization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorPrivateDocumentInformation", x => x.DistributorPrivateDocumentInformationID);
                    table.ForeignKey(
                        name: "FK_DistributorPrivateDocumentInformation_Distributor_DistributorID",
                        column: x => x.DistributorID,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "DistributorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributorSale",
                schema: "dbo",
                columns: table => new
                {
                    DistributorSaleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistributorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SumSalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCountedForBonusCalculation = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorSale", x => x.DistributorSaleID);
                    table.ForeignKey(
                        name: "FK_DistributorSale_Distributor_DistributorID",
                        column: x => x.DistributorID,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "DistributorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributorSale_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Distributor_RecommenderID",
                schema: "dbo",
                table: "Distributor",
                column: "RecommenderID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorAddressInformation_DistributorID",
                schema: "dbo",
                table: "DistributorAddressInformation",
                column: "DistributorID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributorBonus_DistributorID",
                schema: "dbo",
                table: "DistributorBonus",
                column: "DistributorID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorContactInformation_DistributorID",
                schema: "dbo",
                table: "DistributorContactInformation",
                column: "DistributorID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributorPrivateDocumentInformation_DistributorID",
                schema: "dbo",
                table: "DistributorPrivateDocumentInformation",
                column: "DistributorID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributorSale_DistributorID",
                schema: "dbo",
                table: "DistributorSale",
                column: "DistributorID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorSale_ProductID",
                schema: "dbo",
                table: "DistributorSale",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributorAddressInformation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DistributorBonus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DistributorContactInformation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DistributorPrivateDocumentInformation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DistributorSale",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Distributor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");
        }
    }
}
