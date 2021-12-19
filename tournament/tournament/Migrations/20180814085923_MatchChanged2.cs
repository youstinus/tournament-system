using Microsoft.EntityFrameworkCore.Migrations;

namespace tournament.Migrations
{
    public partial class MatchChanged2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScoreTeam1",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoreTeam2",
                table: "Matches",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreTeam1",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ScoreTeam2",
                table: "Matches");
        }
    }
}
