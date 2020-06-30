using Microsoft.EntityFrameworkCore.Migrations;

namespace JustDoItBot.Migrations
{
    public partial class AddChatStateToUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatState_UserId",
                table: "ChatState");

            migrationBuilder.CreateIndex(
                name: "IX_ChatState_UserId",
                table: "ChatState",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatState_UserId",
                table: "ChatState");

            migrationBuilder.CreateIndex(
                name: "IX_ChatState_UserId",
                table: "ChatState",
                column: "UserId");
        }
    }
}
