using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "285c4b9d-352b-4697-a23e-58d5fcb125a2", "User", "USER" },
                    { "2", "4f65981a-2836-43f6-abdd-a749fcb84b17", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "c4bcf356-6dc8-4e47-87a1-48969e9e1f3e", "user@mail.com", false, "user", "user", false, null, "USER@MAIL.COM", "USER@MAIL.COM", "AQAAAAEAACcQAAAAEKXR12PxJPDM9RuR6K4W8E+ZG3ILPeDuRDgZ0SWyqkrv8OkC6dyNy+3B3F+cFxoZwQ==", null, false, "a405b8f9-8dc1-439d-a5f1-5b5cec4d1a10", false, "user@mail.com" },
                    { "2", 0, "61856049-d562-41ff-8875-93bb8d27baf5", "admin@mail.com", false, "admin", "admin", false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEEOguwTxtklYJiMMdeoGk+An9YRD0Gc0VJVo67xC1YDgaFl1R8HSpXmH6jNI22B24w==", null, false, "5f260e2d-5f31-400f-92ec-d9d99b2b8c06", false, "admin@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
