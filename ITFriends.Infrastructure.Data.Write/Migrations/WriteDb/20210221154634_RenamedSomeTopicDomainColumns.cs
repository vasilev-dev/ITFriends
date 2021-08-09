using Microsoft.EntityFrameworkCore.Migrations;

namespace ITFriends.Infrastructure.Data.Write.Migrations.WriteDb
{
    public partial class RenamedSomeTopicDomainColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicMessages_AspNetUsers_CreatorAppUserId",
                table: "TopicMessages");

            migrationBuilder.RenameColumn(
                name: "UpdatedDateTime",
                table: "TopicMessages",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatorAppUserId",
                table: "TopicMessages",
                newName: "AuthorAppUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedDateTime",
                table: "TopicMessages",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_TopicMessages_CreatorAppUserId",
                table: "TopicMessages",
                newName: "IX_TopicMessages_AuthorAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicMessages_AspNetUsers_AuthorAppUserId",
                table: "TopicMessages",
                column: "AuthorAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicMessages_AspNetUsers_AuthorAppUserId",
                table: "TopicMessages");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "TopicMessages",
                newName: "UpdatedDateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "TopicMessages",
                newName: "CreatedDateTime");

            migrationBuilder.RenameColumn(
                name: "AuthorAppUserId",
                table: "TopicMessages",
                newName: "CreatorAppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TopicMessages_AuthorAppUserId",
                table: "TopicMessages",
                newName: "IX_TopicMessages_CreatorAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicMessages_AspNetUsers_CreatorAppUserId",
                table: "TopicMessages",
                column: "CreatorAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
