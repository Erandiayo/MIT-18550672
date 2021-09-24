using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NIBM.Procurement.common.db.migrations
{
    public partial class _05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tender_ProcuremenetRequest",
                table: "Tenders");

            migrationBuilder.DropForeignKey(
                name: "FK_Tender_Vendors",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_TenderID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Tenders_ProcReqId",
                table: "Tenders");

            migrationBuilder.DropIndex(
                name: "IX_FK_TenderVendors",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "TenderID",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "ProcReqId",
                table: "Tenders");

            migrationBuilder.AddColumn<int>(
                name: "TenderStatus",
                table: "Tenders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AwardedVendorId",
                table: "ProcuremenetRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SpecRequestedOn",
                table: "ProcuremenetRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenderID",
                table: "ProcuremenetRequests",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 8,
                columns: new[] { "Area", "Controller", "Description", "DisplaySeq", "Icon", "Text" },
                values: new object[] { "Procurement", "ProcurementRequests", "null", 90, "fas fa-envelope-open-text", "New Request" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 9,
                columns: new[] { "Action", "Controller", "Description", "DisplaySeq", "Icon", "Text" },
                values: new object[] { "Requests", "ApproveRequest", "null", 100, "fas fa-check-square", "Pending Approval" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 10,
                columns: new[] { "Action", "Controller", "Description", "DisplaySeq", "Icon", "Text" },
                values: new object[] { "ProcurementProcessIndex", "ProcurementProcess", "null", 110, "fas fa-exchange-alt", "Procurement Department Process" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 11,
                columns: new[] { "Action", "Controller", "Description", "DisplaySeq", "Icon", "IsHide", "ParentMenuID", "Reporttype", "Text" },
                values: new object[] { "ProcurementRequestSummaryReport", "Reports", "Requests within a selected duration(Filter: Branch / Department).", 20, "null", true, 12, 1, "Procurement Request Summary" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 12,
                columns: new[] { "Action", "Area", "Controller", "Description", "DisplaySeq", "Icon", "Text", "Type" },
                values: new object[] { null, null, null, "null", 140, "fas fa-file-alt", "Reports", "M" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuID", "Action", "Area", "Controller", "Description", "DisplaySeq", "Icon", "IsHide", "ParentMenuID", "Reporttype", "Text", "Type" },
                values: new object[,]
                {
                    { 13, null, null, null, "null", 10, "fas fa-user-tie", false, null, 0, "Admin", "M" },
                    { 14, null, null, null, "null", 20, "fas fa-building", false, null, 0, "Organization", "M" },
                    { 15, "Index", "Procurement", "TenderOpen", "null", 120, "fas fa-dolly-flatbed", false, null, 0, "Tenders", "I" },
                    { 16, "MonthlyProgressReport", "Procurement", "Reports", "null", 20, "null", false, 12, 1, "Monthly Progress Report", "I" },
                    { 17, "TenderDetailsOfTenderReport", "Procurement", "Reports", "null", 30, "null", false, 12, 1, "Tender Details Of Tender", "I" },
                    { 18, "ProcurementRequestDetailReport", "Procurement", "Reports", "null", 40, "null", false, 12, 1, "Procurement Request Detail Report", "I" }
                });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 1,
                columns: new[] { "Description", "Icon", "ParentMenuID" },
                values: new object[] { "null", "fas fa-map-marked-alt", 14 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 2,
                columns: new[] { "Description", "Icon", "ParentMenuID" },
                values: new object[] { "null", "fas fa-vihara", 14 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 3,
                columns: new[] { "Description", "Icon", "ParentMenuID" },
                values: new object[] { "null", "fas fa-warehouse", 14 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 4,
                columns: new[] { "Controller", "Description", "DisplaySeq", "Icon", "ParentMenuID", "Text" },
                values: new object[] { "Designations", "null", 50, "fas fa-people-arrows", 14, "Designations" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 5,
                columns: new[] { "Controller", "Description", "DisplaySeq", "Icon", "ParentMenuID", "Text" },
                values: new object[] { "Employees", "null", 60, "fas fa-user-tie", 14, "Employees" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 6,
                columns: new[] { "Controller", "Description", "DisplaySeq", "Icon", "ParentMenuID", "Text" },
                values: new object[] { "UserRoles", "null", 10, "fas fa-address-card", 13, "User Roles" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 7,
                columns: new[] { "Controller", "Description", "DisplaySeq", "Icon", "ParentMenuID", "Text" },
                values: new object[] { "Users", "null", 20, "fa-solid fa-users", 13, "Users" });

            migrationBuilder.CreateIndex(
                name: "IX_FK_AwardedVendors",
                table: "ProcuremenetRequests",
                column: "AwardedVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_TenderProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "TenderID");

            migrationBuilder.CreateIndex(
                name: "IX_FK_TenderVendorTenders",
                table: "TenderVendors",
                column: "TenderID");

            migrationBuilder.CreateIndex(
                name: "IX_TenderVendors_VendorID",
                table: "TenderVendors",
                column: "VendorID");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "AwardedVendorId",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TenderProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "TenderID",
                principalTable: "Tenders",
                principalColumn: "TenderID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorProcuremenetRequest",
                table: "ProcuremenetRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderProcuremenetRequest",
                table: "ProcuremenetRequests");

            migrationBuilder.DropTable(
                name: "TenderVendors");

            migrationBuilder.DropIndex(
                name: "IX_FK_AwardedVendors",
                table: "ProcuremenetRequests");

            migrationBuilder.DropIndex(
                name: "IX_FK_TenderProcuremenetRequest",
                table: "ProcuremenetRequests");

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 14);

            migrationBuilder.DropColumn(
                name: "TenderStatus",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "AwardedVendorId",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "SpecRequestedOn",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "TenderID",
                table: "ProcuremenetRequests");

            migrationBuilder.AddColumn<int>(
                name: "TenderID",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcReqId",
                table: "Tenders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 1,
                column: "ParentMenuID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 2,
                column: "ParentMenuID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 3,
                column: "ParentMenuID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 4,
                column: "ParentMenuID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 5,
                column: "ParentMenuID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 6,
                column: "ParentMenuID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 7,
                column: "ParentMenuID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 1,
                columns: new[] { "Description", "Icon", "ParentMenuID" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 2,
                columns: new[] { "Description", "Icon", "ParentMenuID" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 3,
                columns: new[] { "Description", "Icon", "ParentMenuID" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 4,
                columns: new[] { "Controller", "Description", "DisplaySeq", "Icon", "ParentMenuID", "Text" },
                values: new object[] { "SubDepartment", null, 40, null, null, "Sub Departments" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 5,
                columns: new[] { "Controller", "Description", "DisplaySeq", "Icon", "ParentMenuID", "Text" },
                values: new object[] { "Designations", null, 50, null, null, "Designations" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 6,
                columns: new[] { "Controller", "Description", "DisplaySeq", "Icon", "ParentMenuID", "Text" },
                values: new object[] { "Employees", null, 60, null, null, "Employees" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 7,
                columns: new[] { "Controller", "Description", "DisplaySeq", "Icon", "ParentMenuID", "Text" },
                values: new object[] { "UserRoles", null, 70, null, null, "User Roles" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 8,
                columns: new[] { "Area", "Controller", "Description", "DisplaySeq", "Icon", "Text" },
                values: new object[] { "Admin", "Users", null, 80, null, "Users" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 9,
                columns: new[] { "Action", "Controller", "Description", "DisplaySeq", "Icon", "Text" },
                values: new object[] { "Index", "ProcurementRequests", null, 90, null, "Procurement Request" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 10,
                columns: new[] { "Action", "Controller", "Description", "DisplaySeq", "Icon", "Text" },
                values: new object[] { "Requests", "ProcurementRequests", null, 100, null, "Approve Requests" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 11,
                columns: new[] { "Action", "Controller", "Description", "DisplaySeq", "Icon", "IsHide", "ParentMenuID", "Reporttype", "Text" },
                values: new object[] { "Requests", "ProcurementRequests", null, 110, null, false, null, 0, "Procurement Department Process" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 12,
                columns: new[] { "Action", "Area", "Controller", "Description", "DisplaySeq", "Icon", "Text", "Type" },
                values: new object[] { "ProcurementRequestSummaryReport", "Procurement", "Reports", null, 120, null, "Procurement Request Summary", "I" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_TenderID",
                table: "Vendors",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Tender_ProcuremenetRequest",
                table: "Tenders",
                column: "ProcReqId",
                principalTable: "ProcuremenetRequests",
                principalColumn: "ProReqID",
                onDelete: ReferentialAction.Restrict);

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
