using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndpointParametersSolution.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmailIndexForUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_userName",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "userEmail",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_userEmail",
                table: "Users",
                column: "userEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_userEmail",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "userEmail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_userName",
                table: "Users",
                column: "userName",
                unique: true);
        }
    }
}
