using Microsoft.EntityFrameworkCore.Migrations;

namespace ITFriends.Infrastructure.Data.Write.Migrations.WriteDb
{
    public partial class AddTopicSubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserSubscription",
                columns: table => new
                {
                    SubscribersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriptionsTopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserSubscription", x => new { x.SubscribersId, x.SubscriptionsTopicId });
                    table.ForeignKey(
                        name: "FK_AppUserSubscription_AspNetUsers_SubscribersId",
                        column: x => x.SubscribersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserSubscription_Topics_SubscriptionsTopicId",
                        column: x => x.SubscriptionsTopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSubscription_SubscriptionsTopicId",
                table: "AppUserSubscription",
                column: "SubscriptionsTopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserSubscription");
        }
    }
}
