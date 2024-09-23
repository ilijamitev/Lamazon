using Microsoft.EntityFrameworkCore.Migrations;

namespace Lamazon.DataAccess.Migrations
{
    public partial class Alter_Order_IpAddr_CountryCode_FlagUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Orders",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryFlagUrl",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CountryFlagUrl",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Orders");
        }
    }
}
