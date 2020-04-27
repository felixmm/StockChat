using Microsoft.EntityFrameworkCore.Migrations;

namespace SockChat.API.Migrations
{
    public partial class MessageUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Messages");
        }
    }
}
