using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VOAprototype.Migrations
{
    public partial class purchase_date_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "PurchaseOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "PurchaseOrder");
        }
    }
}
