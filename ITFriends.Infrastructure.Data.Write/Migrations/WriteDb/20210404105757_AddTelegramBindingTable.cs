using Microsoft.EntityFrameworkCore.Migrations;

namespace ITFriends.Infrastructure.Data.Write.Migrations.WriteDb
{
    public partial class AddTelegramBindingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TelegramBinding",
                columns: table => new
                {
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBinding", x => new { x.ChatId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_TelegramBinding_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBinding_AppUserId",
                table: "TelegramBinding",
                column: "AppUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelegramBinding");
        }
    }
}
