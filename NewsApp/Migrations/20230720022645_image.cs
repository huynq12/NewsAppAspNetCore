using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.Migrations
{
    /// <inheritdoc />
    public partial class image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Upload",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Images",
                newName: "ImageTitle");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Images",
                newName: "ImageDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageTitle",
                table: "Images",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "ImageDescription",
                table: "Images",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<DateTime>(
                name: "Upload",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
