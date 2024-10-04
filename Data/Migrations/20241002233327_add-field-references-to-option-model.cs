using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorForms.Migrations
{
    /// <inheritdoc />
    public partial class addfieldreferencestooptionmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Option_Fields_FieldId",
                table: "Option");

            migrationBuilder.AlterColumn<Guid>(
                name: "FieldId",
                table: "Option",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Option_Fields_FieldId",
                table: "Option",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Option_Fields_FieldId",
                table: "Option");

            migrationBuilder.AlterColumn<Guid>(
                name: "FieldId",
                table: "Option",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Option_Fields_FieldId",
                table: "Option",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }
    }
}
