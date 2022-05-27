using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApi.Migrations
{
    public partial class MessageFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Chat_ChatId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ChatId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Message",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatId",
                table: "Message",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Chat_ChatId",
                table: "Message",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id");
        }
    }
}
