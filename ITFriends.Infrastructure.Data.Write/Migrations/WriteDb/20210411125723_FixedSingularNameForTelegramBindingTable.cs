using Microsoft.EntityFrameworkCore.Migrations;

namespace ITFriends.Infrastructure.Data.Write.Migrations.WriteDb
{
    public partial class FixedSingularNameForTelegramBindingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelegramBinding_AspNetUsers_AppUserId",
                table: "TelegramBinding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TelegramBinding",
                table: "TelegramBinding");

            migrationBuilder.RenameTable(
                name: "TelegramBinding",
                newName: "TelegramBindings");

            migrationBuilder.RenameIndex(
                name: "IX_TelegramBinding_AppUserId",
                table: "TelegramBindings",
                newName: "IX_TelegramBindings_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TelegramBindings",
                table: "TelegramBindings",
                columns: new[] { "ChatId", "AppUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramBindings_AspNetUsers_AppUserId",
                table: "TelegramBindings",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelegramBindings_AspNetUsers_AppUserId",
                table: "TelegramBindings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TelegramBindings",
                table: "TelegramBindings");

            migrationBuilder.RenameTable(
                name: "TelegramBindings",
                newName: "TelegramBinding");

            migrationBuilder.RenameIndex(
                name: "IX_TelegramBindings_AppUserId",
                table: "TelegramBinding",
                newName: "IX_TelegramBinding_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TelegramBinding",
                table: "TelegramBinding",
                columns: new[] { "ChatId", "AppUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramBinding_AspNetUsers_AppUserId",
                table: "TelegramBinding",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
