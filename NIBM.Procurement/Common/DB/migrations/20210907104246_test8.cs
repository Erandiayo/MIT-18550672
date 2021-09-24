using Microsoft.EntityFrameworkCore.Migrations;

namespace NIBM.Procurement.common.db.migrations
{
    public partial class test8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tender_ProcuremenetRequest",
                table: "Tenders");

            migrationBuilder.DropIndex(
                name: "IX_Tenders_ProcReqId",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "ProcReqId",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "ReqNowithin3Months",
                table: "ProcuremenetRequests");

            migrationBuilder.AddColumn<int>(
                name: "TenderID",
                table: "ProcuremenetRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FK_TenderProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "TenderID");

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
                name: "FK_TenderProcuremenetRequest",
                table: "ProcuremenetRequests");

            migrationBuilder.DropIndex(
                name: "IX_FK_TenderProcuremenetRequest",
                table: "ProcuremenetRequests");

            migrationBuilder.DropColumn(
                name: "TenderID",
                table: "ProcuremenetRequests");

            migrationBuilder.AddColumn<int>(
                name: "ProcReqId",
                table: "Tenders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReqNowithin3Months",
                table: "ProcuremenetRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenders_ProcReqId",
                table: "Tenders",
                column: "ProcReqId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tender_ProcuremenetRequest",
                table: "Tenders",
                column: "ProcReqId",
                principalTable: "ProcuremenetRequests",
                principalColumn: "ProReqID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
