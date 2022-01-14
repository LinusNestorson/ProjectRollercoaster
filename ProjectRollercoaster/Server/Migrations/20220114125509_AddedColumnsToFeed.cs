using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectRollercoaster.Server.Migrations
{
    public partial class AddedColumnsToFeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RssLink",
                table: "Feeds",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Feeds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Feeds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublishDate",
                table: "Feeds",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Feeds");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Feeds",
                newName: "RssLink");
        }
    }
}
