using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_JWT.Migrations
{
    public partial class updateproductstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
