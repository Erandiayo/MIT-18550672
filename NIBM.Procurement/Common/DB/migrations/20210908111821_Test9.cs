using Microsoft.EntityFrameworkCore.Migrations;

namespace NIBM.Procurement.common.db.migrations
{
    public partial class Test9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcuremenetRequest_AwardedVendor",
                table: "ProcuremenetRequests");

            migrationBuilder.DropIndex(
                name: "IX_FK_AwardedVendors",
                table: "ProcuremenetRequests");

            migrationBuilder.CreateIndex(
                name: "IX_FK_AwardedVendors",
                table: "ProcuremenetRequests",
                column: "AwardedVendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "AwardedVendorId",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorProcuremenetRequest",
                table: "ProcuremenetRequests");

            migrationBuilder.DropIndex(
                name: "IX_FK_AwardedVendors",
                table: "ProcuremenetRequests");

            migrationBuilder.CreateIndex(
                name: "IX_FK_AwardedVendors",
                table: "ProcuremenetRequests",
                column: "AwardedVendorId",
                unique: true,
                filter: "[AwardedVendorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcuremenetRequest_AwardedVendor",
                table: "ProcuremenetRequests",
                column: "AwardedVendorId",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
