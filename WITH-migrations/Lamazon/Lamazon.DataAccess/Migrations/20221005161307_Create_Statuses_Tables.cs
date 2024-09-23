using Microsoft.EntityFrameworkCore.Migrations;

namespace Lamazon.DataAccess.Migrations
{
    public partial class Create_Statuses_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductStatusId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryStatusId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "ProductCategoryStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategoryStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProductCategoryStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 255, "Deleted" }
                });

            migrationBuilder.CreateTable(
                name: "ProductStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProductStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 255, "Deleted" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductStatusId",
                table: "Products",
                column: "ProductStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductCategoryStatusId",
                table: "ProductCategories",
                column: "ProductCategoryStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_ProductCategoryStatus",
                table: "ProductCategories",
                column: "ProductCategoryStatusId",
                principalTable: "ProductCategoryStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductStatus",
                table: "Products",
                column: "ProductStatusId",
                principalTable: "ProductStatuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_ProductCategoryStatus",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductStatus",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategoryStatuses");

            migrationBuilder.DropTable(
                name: "ProductStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductStatusId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductCategoryStatusId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductStatusId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategoryStatusId",
                table: "ProductCategories");
        }
    }
}
