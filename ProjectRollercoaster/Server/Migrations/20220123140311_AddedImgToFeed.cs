using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectRollercoaster.Server.Migrations
{
    public partial class AddedImgToFeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Feeds",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Feeds");
        }
    }
}
