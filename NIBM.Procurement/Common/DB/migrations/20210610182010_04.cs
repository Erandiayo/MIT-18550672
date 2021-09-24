using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NIBM.Procurement.common.db.migrations
{
    public partial class _04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FK_ItemCategoryProcuremenetRequest",
                table: "ProcuremenetRequests");

            migrationBuilder.DropIndex(
                name: "IX_FK_VendorProcuremenetRequest",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "ItemCategoryId",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "ItemReceivedByVendorOn",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "SelectedVendorId",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "TecApprovedDate",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "TecTeamAssigedOn",
                table: "ProcuremenetRequests");

            migrationBuilder.AddColumn<string>(
                name: "RecommenedSpecification",
                table: "ProcuremenetRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReqItemCateory",
                table: "ProcuremenetRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpecRecomnedBy",
                table: "ProcuremenetRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SpecRecomnedOn",
                table: "ProcuremenetRequests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    TenderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcReqId = table.Column<int>(nullable: false),
                    TenderOpenedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TenderClosedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TecTeamAssigedOn = table.Column<DateTime>(nullable: true),
                    TecApprovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TECReportAttachment = table.Column<string>(nullable: true),
                    TenderBoardApprovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SelectedVendorId = table.Column<int>(nullable: true),
                    ItemReceivedByVendorOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.TenderID);
                    table.ForeignKey(
                        name: "FK_Tender_ProcuremenetRequest",
                        column: x => x.ProcReqId,
                        principalTable: "ProcuremenetRequests",
                        principalColumn: "ProReqID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TECMembers",
                columns: table => new
                {
                    TECMemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcReqId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    TenderID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TECMembers", x => x.TECMemberID);
                    table.ForeignKey(
                        name: "FK_Employee_TECMembers",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TECMembers_Tenders_TenderID",
                        column: x => x.TenderID,
                        principalTable: "Tenders",
                        principalColumn: "TenderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NavVendorID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    TelNo = table.Column<string>(nullable: true),
                    FaxNo = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    TenderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorID);
                    table.ForeignKey(
                        name: "FK_Tender_Vendors",
                        column: x => x.TenderID,
                        principalTable: "Tenders",
                        principalColumn: "TenderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FK_TecMemberEmployee",
                table: "TECMembers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TECMembers_TenderID",
                table: "TECMembers",
                column: "TenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Tenders_ProcReqId",
                table: "Tenders",
                column: "ProcReqId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FK_TenderVendors",
                table: "Tenders",
                column: "SelectedVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_TenderID",
                table: "Vendors",
                column: "TenderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TECMembers");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Tenders");

            migrationBuilder.DropColumn(
                name: "RecommenedSpecification",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "ReqItemCateory",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "SpecRecomnedBy",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "SpecRecomnedOn",
                table: "ProcuremenetRequests");

            migrationBuilder.AddColumn<int>(
                name: "ItemCategoryId",
                table: "ProcuremenetRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ItemReceivedByVendorOn",
                table: "ProcuremenetRequests",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedVendorId",
                table: "ProcuremenetRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TecApprovedDate",
                table: "ProcuremenetRequests",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TecTeamAssigedOn",
                table: "ProcuremenetRequests",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FK_ItemCategoryProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_VendorProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "SelectedVendorId");
        }
    }
}
