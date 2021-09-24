using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NIBM.Procurement.common.db.migrations
{
    public partial class test7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tender_Vendors",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_TenderID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_FK_TenderVendors",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "TenderID",
                table: "Vendors");

            migrationBuilder.CreateTable(
                name: "TenderVendors",
                columns: table => new
                {
                    TenderVendorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenderID = table.Column<int>(nullable: false),
                    VendorID = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderVendors", x => x.TenderVendorID);
                    table.ForeignKey(
                        name: "FK_TenderVendorTenders",
                        column: x => x.TenderID,
                        principalTable: "Tenders",
                        principalColumn: "TenderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenderVendorVendors",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FK_TenderVendorTenders",
                table: "TenderVendors",
                column: "TenderID");

            migrationBuilder.CreateIndex(
                name: "IX_TenderVendors_VendorID",
                table: "TenderVendors",
                column: "VendorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenderVendors");

            migrationBuilder.AddColumn<int>(
                name: "TenderID",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_TenderID",
                table: "Vendors",
                column: "TenderID");

            migrationBuilder.CreateIndex(
                name: "IX_FK_TenderVendors",
                table: "Tenders",
                column: "SelectedVendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tender_Vendors",
                table: "Vendors",
                column: "TenderID",
                principalTable: "Tenders",
                principalColumn: "TenderID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
