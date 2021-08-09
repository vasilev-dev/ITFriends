using Microsoft.EntityFrameworkCore.Migrations;

namespace ITFriends.Infrastructure.Data.Write.Migrations.WriteDb
{
    public partial class AddLanguageCodeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "TelegramBindings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "en");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "TelegramBindings");
        }
    }
}
