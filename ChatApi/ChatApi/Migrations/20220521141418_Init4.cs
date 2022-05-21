using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApi.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Chat_ChatId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ChatId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "User1UserName",
                table: "Chat",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User2UserName",
                table: "Chat",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_User1UserName",
                table: "Chat",
                column: "User1UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_User2UserName",
                table: "Chat",
                column: "User2UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_User_User1UserName",
                table: "Chat",
                column: "User1UserName",
                principalTable: "User",
                principalColumn: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_User_User2UserName",
                table: "Chat",
                column: "User2UserName",
                principalTable: "User",
                principalColumn: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_User_User1UserName",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_User_User2UserName",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_User1UserName",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_User2UserName",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "User1UserName",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "User2UserName",
                table: "Chat");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ChatId",
                table: "User",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Chat_ChatId",
                table: "User",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id");
        }
    }
}
