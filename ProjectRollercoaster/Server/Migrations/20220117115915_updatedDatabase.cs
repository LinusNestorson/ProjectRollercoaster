﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectRollercoaster.Server.Migrations
{
    public partial class updatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Users_UserId",
                table: "Feeds");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Feeds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_Users_UserId",
                table: "Feeds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Users_UserId",
                table: "Feeds");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Feeds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_Users_UserId",
                table: "Feeds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
