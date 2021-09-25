using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NIBM.Procurement.common.db.migrations
{
    public partial class Test_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubDepartmentEmployee",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "SubDepartments");

            migrationBuilder.DropIndex(
                name: "IX_FK_SubDepartmentEmployee",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Address_1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MobileNo_1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NICNo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OfficialEmail",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SubDeptId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TelNo_1",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_1",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Employees",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MobileNo_1",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NICNo",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OfficialEmail",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubDeptId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelNo_1",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubDepartments",
                columns: table => new
                {
                    SubDeptID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeptID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDepartments", x => x.SubDeptID);
                    table.ForeignKey(
                        name: "FK_DepartmentSubDepartment",
                        column: x => x.DeptID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1,
                columns: new[] { "DOB", "MobileNo_1", "NICNo", "OfficialEmail" },
                values: new object[] { new DateTime(1988, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "0712496994", "885312322V", "b@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2,
                columns: new[] { "DOB", "MobileNo_1", "NICNo", "OfficialEmail" },
                values: new object[] { new DateTime(1988, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "0712496994", "885312322V", "b@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3,
                columns: new[] { "DOB", "MobileNo_1", "NICNo", "OfficialEmail" },
                values: new object[] { new DateTime(1988, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "0712496994", "885312322V", "b@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_FK_SubDepartmentEmployee",
                table: "Employees",
                column: "SubDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_DepartmentSubDepartment",
                table: "SubDepartments",
                column: "DeptID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubDepartmentEmployee",
                table: "Employees",
                column: "SubDeptId",
                principalTable: "SubDepartments",
                principalColumn: "SubDeptID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
