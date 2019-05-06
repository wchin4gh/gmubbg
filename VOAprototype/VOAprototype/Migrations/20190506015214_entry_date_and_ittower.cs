using Microsoft.EntityFrameworkCore.Migrations;

namespace VOAprototype.Migrations
{
    public partial class entry_date_and_ittower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ITTower",
                table: "PurchaseOrder",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ITTower",
                table: "PurchaseOrder",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
