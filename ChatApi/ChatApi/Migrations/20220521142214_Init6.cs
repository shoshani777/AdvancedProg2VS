using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApi.Migrations
{
    public partial class Init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name1",
                table: "Chat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name2",
                table: "Chat",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name1",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Name2",
                table: "Chat");

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
    }
}
