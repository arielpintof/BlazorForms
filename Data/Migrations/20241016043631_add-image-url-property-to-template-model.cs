using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorForms.Migrations
{
    /// <inheritdoc />
    public partial class addimageurlpropertytotemplatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Templates",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Templates");
        }
    }
}
