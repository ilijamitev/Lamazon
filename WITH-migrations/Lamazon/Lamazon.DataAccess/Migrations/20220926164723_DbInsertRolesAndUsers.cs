using Microsoft.EntityFrameworkCore.Migrations;

namespace Lamazon.DataAccess.Migrations
{
    public partial class DbInsertRolesAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Key", "Name" },
                values: new object[,]
                {
                    { "user", "User" },
                    { "admin", "Administrator" }
                });

            migrationBuilder.InsertData(
              table: "Users",
              columns: new[] { "FullName", "RoleKey", "Email", "PasswordHash" },
              values: new object[,]
              {
                    { "Administrator User", "admin", "admin@outlook.com", "AQAAAAEAACcQAAAAEFdijDmZ0+YKrncF+mdkYhzxBCz/IGJxtHc+/dfnhMT7d3DuquPtLUfhKoy8viPz9A==" }, // Admin123!
                    { "Normal User", "user", "user@outlook.com", "AQAAAAEAACcQAAAAEFdijDmZ0+YKrncF+mdkYhzxBCz/IGJxtHc+/dfnhMT7d3DuquPtLUfhKoy8viPz9A==" }
              });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Email",
                keyValues: new object[] { "admin@outlook.com", "user@outlook.com" }
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Key",
                keyValues: new object[] { "user", "admin" }
            );
        }
    }
}
