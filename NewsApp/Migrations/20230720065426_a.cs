using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageCaption",
                table: "Images",
                newName: "MyImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyImage",
                table: "Images",
                newName: "ImageCaption");
        }
    }
}
