using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApi.Migrations
{
    public partial class chatsAtrributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isClicked",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "unread",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "unreadMark",
                table: "Chat");

            migrationBuilder.AddColumn<bool>(
                name: "isClicked",
                table: "UserContact",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "unread",
                table: "UserContact",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "unreadMark",
                table: "UserContact",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isClicked",
                table: "UserContact");

            migrationBuilder.DropColumn(
                name: "unread",
                table: "UserContact");

            migrationBuilder.DropColumn(
                name: "unreadMark",
                table: "UserContact");

            migrationBuilder.AddColumn<bool>(
                name: "isClicked",
                table: "Chat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "unread",
                table: "Chat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "unreadMark",
                table: "Chat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
