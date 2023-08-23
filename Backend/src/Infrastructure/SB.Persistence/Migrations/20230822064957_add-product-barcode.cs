using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SB.Persistence.Migrations
{
    public partial class addproductbarcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Product");
        }
    }
}
