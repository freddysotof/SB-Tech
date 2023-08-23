using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SB.Persistence.Migrations
{
    public partial class modifyproductcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Product",
                type: "varchar(101)",
                unicode: false,
                maxLength: 101,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(31)",
                oldUnicode: false,
                oldMaxLength: 31,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Product",
                type: "varchar(31)",
                unicode: false,
                maxLength: 31,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(101)",
                oldUnicode: false,
                oldMaxLength: 101,
                oldNullable: true);
        }
    }
}
