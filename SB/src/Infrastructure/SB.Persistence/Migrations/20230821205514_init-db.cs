using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SB.Persistence.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    CustomerAddressId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    DocDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.OrderHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(31)", unicode: false, maxLength: 31, nullable: true),
                    Description = table.Column<string>(type: "varchar(101)", unicode: false, maxLength: 101, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false),
                    ItemNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_OrderHeader",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeader",
                        principalColumn: "OrderHeaderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderHeaderId",
                table: "OrderDetail",
                column: "OrderHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "OrderHeader");
        }
    }
}
