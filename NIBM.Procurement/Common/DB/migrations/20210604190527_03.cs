using Microsoft.EntityFrameworkCore.Migrations;

namespace NIBM.Procurement.common.db.migrations
{
    public partial class _03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(table: "Menus", "MenuID", 10, "Action", "Requests");
            migrationBuilder.UpdateData(table: "Menus", "MenuID", 10, "Area", "Procurement");
            migrationBuilder.UpdateData(table: "Menus", "MenuID", 10, "Controller", "ProcurementRequests");
            migrationBuilder.UpdateData(table: "Menus", "MenuID", 10, "Text", "Approve Requests");

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuID", "Action", "Area", "Controller", "Description", "DisplaySeq", "Icon", "IsHide", "ParentMenuID", "Reporttype", "Text", "Type" },
                values: new object[] { 11, "Requests", "Procurement", "ProcurementRequests", null, 110, null, false, null, 0, "Procurement Department Process", "I" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuID", "Action", "Area", "Controller", "Description", "DisplaySeq", "Icon", "IsHide", "ParentMenuID", "Reporttype", "Text", "Type" },
                values: new object[] { 12, "ProcurementRequestSummaryReport", "Procurement", "Reports", null, 120, null, false, null, 0, "Procurement Request Summary", "I" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 12);
        }
    }
}
