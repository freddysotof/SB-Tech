using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SB.Persistence.Migrations
{
    public partial class addproductname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Product",
                type: "varchar(101)",
                unicode: false,
                maxLength: 101,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Product");
        }
    }
}
