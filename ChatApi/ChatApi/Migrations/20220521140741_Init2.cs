using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApi.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatUser");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    ChatsId = table.Column<int>(type: "int", nullable: false),
                    UsersUserName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => new { x.ChatsId, x.UsersUserName });
                    table.ForeignKey(
                        name: "FK_ChatUser_Chat_ChatsId",
                        column: x => x.ChatsId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_User_UsersUserName",
                        column: x => x.UsersUserName,
                        principalTable: "User",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_UsersUserName",
                table: "ChatUser",
                column: "UsersUserName");
        }
    }
}
