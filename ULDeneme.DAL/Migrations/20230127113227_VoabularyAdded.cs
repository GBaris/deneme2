using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ULDeneme.DAL.Migrations
{
    /// <inheritdoc />
    public partial class VoabularyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vocabularies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnKVoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KVoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SozlukID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vocabularies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vocabularies_Sozluks_SozlukID",
                        column: x => x.SozlukID,
                        principalTable: "Sozluks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vocabularies_SozlukID",
                table: "Vocabularies",
                column: "SozlukID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vocabularies");
        }
    }
}
