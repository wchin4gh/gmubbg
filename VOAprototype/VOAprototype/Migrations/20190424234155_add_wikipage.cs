using Microsoft.EntityFrameworkCore.Migrations;

namespace VOAprototype.Migrations
{
    public partial class add_wikipage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WikiPage",
                table: "ITFunction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WikiPage",
                table: "ITFunction");
        }
    }
}
