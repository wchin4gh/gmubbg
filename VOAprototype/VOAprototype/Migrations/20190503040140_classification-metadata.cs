using Microsoft.EntityFrameworkCore.Migrations;

namespace VOAprototype.Migrations
{
    public partial class classificationmetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstClassification",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondClassification",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdClassification",
                table: "PurchaseOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstClassification",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "SecondClassification",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "ThirdClassification",
                table: "PurchaseOrder");
        }
    }
}
