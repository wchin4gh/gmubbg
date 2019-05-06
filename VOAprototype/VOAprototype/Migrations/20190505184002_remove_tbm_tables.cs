using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VOAprototype.Migrations
{
    public partial class remove_tbm_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBMItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBMItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TBMITTowerId = table.Column<int>(nullable: true),
                    TBMServiceId = table.Column<int>(nullable: true),
                    TBMServiceCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBMItem_TBMItem_TBMITTowerId",
                        column: x => x.TBMITTowerId,
                        principalTable: "TBMItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBMItem_TBMItem_TBMServiceId",
                        column: x => x.TBMServiceId,
                        principalTable: "TBMItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBMItem_TBMItem_TBMServiceCategoryId",
                        column: x => x.TBMServiceCategoryId,
                        principalTable: "TBMItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBMItem_TBMITTowerId",
                table: "TBMItem",
                column: "TBMITTowerId");

            migrationBuilder.CreateIndex(
                name: "IX_TBMItem_TBMServiceId",
                table: "TBMItem",
                column: "TBMServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TBMItem_TBMServiceCategoryId",
                table: "TBMItem",
                column: "TBMServiceCategoryId");
        }
    }
}
