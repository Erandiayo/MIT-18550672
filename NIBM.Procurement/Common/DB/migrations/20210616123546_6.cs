using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NIBM.Procurement.common.db.migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwardedVendorId",
                table: "ProcuremenetRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReqNowithin3Months",
                table: "ProcuremenetRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SpecRequestedOn",
                table: "ProcuremenetRequests",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcuremenetRequest_AwardedVendor",
                table: "ProcuremenetRequests");

            migrationBuilder.DropIndex(
                name: "IX_FK_AwardedVendors",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "AwardedVendorId",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "ReqNowithin3Months",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "SpecRequestedOn",
                table: "ProcuremenetRequests");
        }
    }
}
