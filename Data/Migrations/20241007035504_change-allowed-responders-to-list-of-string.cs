using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorForms.Migrations
{
    /// <inheritdoc />
    public partial class changeallowedresponderstolistofstring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Templates_TemplateId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TemplateId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AllowedResponderIds",
                table: "Templates",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedResponderIds",
                table: "Templates");

            migrationBuilder.AddColumn<Guid>(
                name: "TemplateId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TemplateId",
                table: "AspNetUsers",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Templates_TemplateId",
                table: "AspNetUsers",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id");
        }
    }
}
