using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApi.Migrations
{
    public partial class UserC2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserContact");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "Chat",
                table: "Message");

            migrationBuilder.AddColumn<string>(
                name: "Server",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserName",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_AuthorUserName",
                table: "Message",
                column: "AuthorUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_AuthorUserName",
                table: "Message",
                column: "AuthorUserName",
                principalTable: "User",
                principalColumn: "UserName");
        }
    }
}
