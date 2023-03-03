using Microsoft.EntityFrameworkCore.Migrations;
using System.Runtime.CompilerServices;

#nullable disable

namespace ULDeneme.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SozlukTrasnlationTypeRelationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sozluk_Users_UserID",
                table: "Sozluk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sozluk",
                table: "Sozluk");

            migrationBuilder.RenameTable(
                name: "Sozluk",
                newName: "Sozluks");

            migrationBuilder.RenameIndex(
                name: "IX_Sozluk_UserID",
                table: "Sozluks",
                newName: "IX_Sozluks_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sozluks",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Sozluks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TranslationTypeID",
                table: "Sozluks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sozluks",
                table: "Sozluks",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Sozluks_TranslationTypeID",
                table: "Sozluks",
                column: "TranslationTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sozluks_Translations_TranslationTypeID",
                table: "Sozluks",
                column: "TranslationTypeID",
                principalTable: "Translations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sozluks_Users_UserID",
                table: "Sozluks",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sozluks_Translations_TranslationTypeID",
                table: "Sozluks");

            migrationBuilder.DropForeignKey(
                name: "FK_Sozluks_Users_UserID",
                table: "Sozluks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sozluks",
                table: "Sozluks");

            migrationBuilder.DropIndex(
                name: "IX_Sozluks_TranslationTypeID",
                table: "Sozluks");

            migrationBuilder.DropColumn(
                name: "TranslationTypeID",
                table: "Sozluks");

            migrationBuilder.RenameTable(
                name: "Sozluks",
                newName: "Sozluk");

            migrationBuilder.RenameIndex(
                name: "IX_Sozluks_UserID",
                table: "Sozluk",
                newName: "IX_Sozluk_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sozluk",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Sozluk",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sozluk",
                table: "Sozluk",
                column: "ID");

            migrationBuilder.AddForeignKey(
                            name: "FK_Sozluk_Users_UserID",
                            table: "Sozluk",
                            column: "UserID",
                            principalTable: "Users",
                            principalColumn: "ID",
                            onDelete: ReferentialAction.NoAction,
                            onUpdate: ReferentialAction.NoAction
                            );
        }
    }
}
