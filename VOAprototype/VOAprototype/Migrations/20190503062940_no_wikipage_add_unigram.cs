using Microsoft.EntityFrameworkCore.Migrations;

namespace VOAprototype.Migrations
{
    public partial class no_wikipage_add_unigram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WikiPage",
                table: "ITFunction",
                newName: "Unigram");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ITFunction",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unigram",
                table: "ITFunction",
                newName: "WikiPage");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ITFunction",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
