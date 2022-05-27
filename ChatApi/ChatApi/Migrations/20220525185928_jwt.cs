using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApi.Migrations
{
    public partial class jwt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactOf",
                table: "UserContact",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "UserContact",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactOf",
                table: "UserContact");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "UserContact");
        }
    }
}
