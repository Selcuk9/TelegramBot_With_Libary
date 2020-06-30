using Microsoft.EntityFrameworkCore.Migrations;

namespace JustDoItBot.Migrations
{
    public partial class AddForeignKeyUserIdToChatState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ChatState",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatState_UserId",
                table: "ChatState",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatState_Users_UserId",
                table: "ChatState",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatState_Users_UserId",
                table: "ChatState");

            migrationBuilder.DropIndex(
                name: "IX_ChatState_UserId",
                table: "ChatState");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatState");
        }
    }
}
