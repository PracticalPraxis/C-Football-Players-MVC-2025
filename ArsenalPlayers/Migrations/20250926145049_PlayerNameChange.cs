using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsenalPlayers.Migrations
{
    public partial class PlayerNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Player",
                newName: "PlayerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerName",
                table: "Player",
                newName: "Name");
        }
    }
}
