using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostsApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInitialsFromUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Initials",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Initials",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
