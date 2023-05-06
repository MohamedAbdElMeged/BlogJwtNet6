using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogJwtNet6.Migrations
{
    public partial class RenameCreatedAtColumnInPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Posts",
                newName: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Posts",
                newName: "DateTime");
        }
    }
}
