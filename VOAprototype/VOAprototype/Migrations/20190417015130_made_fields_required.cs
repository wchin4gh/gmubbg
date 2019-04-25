using Microsoft.EntityFrameworkCore.Migrations;

namespace VOAprototype.Migrations
{
    public partial class made_fields_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ITTower",
                table: "PurchaseOrder",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ITFunction",
                table: "PurchaseOrder",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "PurchaseOrder",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BusinessFunction",
                table: "PurchaseOrder",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Application",
                table: "PurchaseOrder",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ITTower",
                table: "PurchaseOrder",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ITFunction",
                table: "PurchaseOrder",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "PurchaseOrder",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "BusinessFunction",
                table: "PurchaseOrder",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Application",
                table: "PurchaseOrder",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
