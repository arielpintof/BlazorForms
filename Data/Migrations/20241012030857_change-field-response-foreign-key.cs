using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorForms.Migrations
{
    /// <inheritdoc />
    public partial class changefieldresponseforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldResponses_FormResponses_Id",
                table: "FieldResponses");

            migrationBuilder.CreateIndex(
                name: "IX_FieldResponses_FormResponseId",
                table: "FieldResponses",
                column: "FormResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldResponses_FormResponses_FormResponseId",
                table: "FieldResponses",
                column: "FormResponseId",
                principalTable: "FormResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldResponses_FormResponses_FormResponseId",
                table: "FieldResponses");

            migrationBuilder.DropIndex(
                name: "IX_FieldResponses_FormResponseId",
                table: "FieldResponses");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldResponses_FormResponses_Id",
                table: "FieldResponses",
                column: "Id",
                principalTable: "FormResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
