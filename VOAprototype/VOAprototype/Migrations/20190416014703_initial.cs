using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VOAprototype.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TBMITServices = table.Column<string>(nullable: true),
                    BusinessFunction = table.Column<string>(nullable: true),
                    SupportingServices = table.Column<string>(nullable: true),
                    EgovBRM = table.Column<string>(nullable: true),
                    Application = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Units = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todoes");
            migrationBuilder.DropTable(
                name: "PurchaseOrder");
        }
    }
}
