using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VOAprototype.Migrations
{
    public partial class add_po_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TBMITServices",
                table: "PurchaseOrder",
                newName: "TBMName");

            migrationBuilder.RenameColumn(
                name: "SupportingServices",
                table: "PurchaseOrder",
                newName: "TBMITService");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "PurchaseOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Entity",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "PurchaseOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Finance",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ITFunction",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ITTower",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatsPerLicense",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatsUsed",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TBMCategory",
                table: "PurchaseOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "Entity",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "Finance",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "ITFunction",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "ITTower",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "SeatsPerLicense",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "SeatsUsed",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "TBMCategory",
                table: "PurchaseOrder");

            migrationBuilder.RenameColumn(
                name: "TBMName",
                table: "PurchaseOrder",
                newName: "TBMITServices");

            migrationBuilder.RenameColumn(
                name: "TBMITService",
                table: "PurchaseOrder",
                newName: "SupportingServices");
        }
    }
}
