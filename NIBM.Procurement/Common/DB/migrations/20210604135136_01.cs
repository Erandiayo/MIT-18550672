using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NIBM.Procurement.common.db.migrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    TelNo_1 = table.Column<string>(nullable: true),
                    TelNo_2 = table.Column<string>(nullable: true),
                    FaxNo_1 = table.Column<string>(nullable: true),
                    FaxNo_2 = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    DesignationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.DesignationID);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentMenuID = table.Column<int>(nullable: true),
                    DisplaySeq = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsHide = table.Column<bool>(nullable: false),
                    Reporttype = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuID);
                    table.ForeignKey(
                        name: "FK_MenuMenu",
                        column: x => x.ParentMenuID,
                        principalTable: "Menus",
                        principalColumn: "MenuID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    ParameterID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterCode = table.Column<string>(nullable: false),
                    ParameterValue = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.ParameterID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "BranchDepartments",
                columns: table => new
                {
                    BranchID = table.Column<int>(nullable: false),
                    DepartmentID = table.Column<int>(nullable: false),
                    TelNo_1 = table.Column<string>(nullable: true),
                    TelNo_2 = table.Column<string>(nullable: true),
                    FaxNo_1 = table.Column<string>(nullable: true),
                    FaxNo_2 = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchDepartments", x => new { x.BranchID, x.DepartmentID });
                    table.ForeignKey(
                        name: "FK_BranchBranchDepartment",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentBranchDepartment",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubDepartments",
                columns: table => new
                {
                    SubDeptID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DeptID = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RoleMenuAccesses",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false),
                    MenuID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenuAccesses", x => new { x.RoleID, x.MenuID });
                    table.ForeignKey(
                        name: "FK_MenuRoleMenuAccess",
                        column: x => x.MenuID,
                        principalTable: "Menus",
                        principalColumn: "MenuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleRoleMenuAccess",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EPFNo = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    NICNo = table.Column<string>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime", nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Initials = table.Column<string>(nullable: false),
                    Address_1 = table.Column<string>(nullable: true),
                    MobileNo_1 = table.Column<string>(nullable: true),
                    TelNo_1 = table.Column<string>(nullable: true),
                    OfficialEmail = table.Column<string>(nullable: true),
                    DesignationID = table.Column<int>(nullable: false),
                    BranchID = table.Column<int>(nullable: false),
                    DepartmentID = table.Column<int>(nullable: false),
                    SubDeptId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    ImmediateSupervisor1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_DesignationEmployee",
                        column: x => x.DesignationID,
                        principalTable: "Designations",
                        principalColumn: "DesignationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEmployee",
                        column: x => x.ImmediateSupervisor1,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubDepartmentEmployee",
                        column: x => x.SubDeptId,
                        principalTable: "SubDepartments",
                        principalColumn: "SubDeptID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchDepartmentEmployee",
                        columns: x => new { x.BranchID, x.DepartmentID },
                        principalTable: "BranchDepartments",
                        principalColumns: new[] { "BranchID", "DepartmentID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcuremenetRequests",
                columns: table => new
                {
                    ProReqID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReqBy = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ReqSubject = table.Column<string>(nullable: false),
                    DivisionHead = table.Column<int>(nullable: false),
                    AttachmentLink = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DivHeadAppORRejDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ProcDeptReceivedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProcessType = table.Column<int>(nullable: true),
                    ItemCategoryId = table.Column<int>(nullable: true),
                    AmountPurchased = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    ItemReceivedByVendorOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    SelectedVendorId = table.Column<int>(nullable: true),
                    TecApprovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TecTeamAssigedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserFeedback = table.Column<string>(nullable: true),
                    SupervisorComment = table.Column<string>(nullable: true),
                    DGAppORRejDate = table.Column<DateTime>(nullable: true),
                    HRAppRecommendORRejDate = table.Column<DateTime>(nullable: true),
                    ProcurementProcessStartedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcuremenetRequests", x => x.ProReqID);
                    table.ForeignKey(
                        name: "FK_DivHeadProcuremenetRequest",
                        column: x => x.DivisionHead,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeProcuremenetRequest",
                        column: x => x.ReqBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    BranchID = table.Column<int>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeUser",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementReqItems",
                columns: table => new
                {
                    ProReqItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProReqId = table.Column<int>(nullable: false),
                    ItemDesc = table.Column<string>(nullable: false),
                    ReqQty = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    ItemCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementReqItems", x => x.ProReqItemID);
                    table.ForeignKey(
                        name: "FK_ProcuremenetRequestProcurementReqItem",
                        column: x => x.ProReqId,
                        principalTable: "ProcuremenetRequests",
                        principalColumn: "ProReqID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleID);
                    table.ForeignKey(
                        name: "FK_RoleUserRole",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserUserRole",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchID", "Address", "CreatedBy", "CreatedDate", "Description", "FaxNo_1", "FaxNo_2", "ModifiedBy", "ModifiedDate", "Status", "TelNo_1", "TelNo_2" },
                values: new object[] { 1, "120/5, Wijerama Mawatha, Colombo 07", "ERANDI_PC\\erandi", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Colombo", "+94 11 2667769", "+94 11 2685808", null, null, 1, "+94 115 321 000 - 30", "+94 11 2693404" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentID", "CreatedBy", "CreatedDate", "Description", "ModifiedBy", "ModifiedDate", "Status" },
                values: new object[] { 1, "ERANDI_PC\\erandi", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MIS", null, null, 1 });

            migrationBuilder.InsertData(
                table: "Designations",
                columns: new[] { "DesignationID", "CreatedBy", "CreatedDate", "Description", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "ERANDI_PC\\erandi", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consultant", null, null },
                    { 2, "ERANDI_PC\\erandi", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director-SOCE", null, null },
                    { 3, "ERANDI_PC\\erandi", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director-HRM", null, null },
                    { 4, "ERANDI_PC\\erandi", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director General", null, null },
                    { 5, "ERANDI_PC\\erandi", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Head-Procurement", null, null },
                    { 6, "ERANDI_PC\\erandi", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Management Assistant", null, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuID", "Action", "Area", "Controller", "Description", "DisplaySeq", "Icon", "IsHide", "ParentMenuID", "Reporttype", "Text", "Type" },
                values: new object[,]
                {
                    { 10, "ProcurementRequestSummaryReport", "Procurement", "Reports", null, 100, null, false, null, 0, "Procurement Request Summary", "I" },
                    { 9, "Index", "Procurement", "ProcurementRequests", null, 90, null, false, null, 0, "Procurement Request", "I" },
                    { 8, "Index", "Admin", "Users", null, 80, null, false, null, 0, "Users", "I" },
                    { 7, "Index", "Admin", "UserRoles", null, 70, null, false, null, 0, "User Roles", "I" },
                    { 6, "Index", "Admin", "Employees", null, 60, null, false, null, 0, "Employees", "I" },
                    { 4, "Index", "Admin", "SubDepartment", null, 40, null, false, null, 0, "Sub Departments", "I" },
                    { 3, "Index", "Admin", "BranchDepartments", null, 30, null, false, null, 0, "Branch Departments", "I" },
                    { 2, "Index", "Admin", "Departments", null, 20, null, false, null, 0, "Departments", "I" },
                    { 1, "Index", "Admin", "Branches", null, 10, null, false, null, 0, "Branches", "I" },
                    { 5, "Index", "Admin", "Designations", null, 50, null, false, null, 0, "Designations", "I" }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "ParameterID", "ParameterCode", "ParameterValue" },
                values: new object[,]
                {
                    { 4, "HRM_MA", "1" },
                    { 1, "DG_EmployeeID", "1" },
                    { 2, "HRM_AO", "1" },
                    { 3, "HRM_DD", "1" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Code", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[] { 1, "Admin", "ERANDI_PC\\erandi", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Administrator" });

            migrationBuilder.InsertData(
                table: "BranchDepartments",
                columns: new[] { "BranchID", "DepartmentID", "CreatedBy", "CreatedDate", "FaxNo_1", "FaxNo_2", "ModifiedBy", "ModifiedDate", "Status", "TelNo_1", "TelNo_2" },
                values: new object[] { 1, 1, "ERANDI_PC\\erandi", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0112667769", "0112667769", null, null, 1, "0112667769", "0112667769" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Address_1", "BranchID", "CreatedBy", "CreatedDate", "DOB", "DepartmentID", "DesignationID", "EPFNo", "FirstName", "FullName", "Gender", "ImmediateSupervisor1", "Initials", "LastName", "MiddleName", "MobileNo_1", "ModifiedBy", "ModifiedDate", "NICNo", "OfficialEmail", "Status", "SubDeptId", "TelNo_1", "Title" },
                values: new object[] { 1, null, 1, "ERANDI_PC\\erandi", new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1988, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 992, "Gangani", "Gangani Wickramasinghe", 1, null, "G C", "Wikramasinghe", "", "0712496994", null, null, "885312322V", "b@gmail.com", 0, null, null, 4 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Address_1", "BranchID", "CreatedBy", "CreatedDate", "DOB", "DepartmentID", "DesignationID", "EPFNo", "FirstName", "FullName", "Gender", "ImmediateSupervisor1", "Initials", "LastName", "MiddleName", "MobileNo_1", "ModifiedBy", "ModifiedDate", "NICNo", "OfficialEmail", "Status", "SubDeptId", "TelNo_1", "Title" },
                values: new object[] { 3, null, 1, "ERANDI_PC\\erandi", new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1988, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 450, "Ananda", "Ananda Kulasooriya", 1, null, "D M A", "Kulasooriya", "", "0712496994", null, null, "885312322V", "b@gmail.com", 0, null, null, 4 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Address_1", "BranchID", "CreatedBy", "CreatedDate", "DOB", "DepartmentID", "DesignationID", "EPFNo", "FirstName", "FullName", "Gender", "ImmediateSupervisor1", "Initials", "LastName", "MiddleName", "MobileNo_1", "ModifiedBy", "ModifiedDate", "NICNo", "OfficialEmail", "Status", "SubDeptId", "TelNo_1", "Title" },
                values: new object[] { 2, null, 1, "ERANDI_PC\\erandi", new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1988, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1584, "Erandi", "Erandi Ayodya Rathnayaka", 1, 1, "R M E A", "Rathnayaka", "Ayodya", "0712496994", null, null, "885312322V", "b@gmail.com", 0, null, null, 4 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "BranchID", "CreatedBy", "CreatedDate", "DepartmentID", "EmployeeID", "ModifiedBy", "ModifiedDate", "Password", "Status", "UserName" },
                values: new object[] { 1, null, "ERANDI_PC\\erandi", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, null, "1", 1, "erandi" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleID", "RoleID", "UserID" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_FK_DepartmentBranchDepartment",
                table: "BranchDepartments",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_FK_DesignationEmployee",
                table: "Employees",
                column: "DesignationID");

            migrationBuilder.CreateIndex(
                name: "UC_EmployeeEPFNo",
                table: "Employees",
                column: "EPFNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FK_EmployeeEmployee",
                table: "Employees",
                column: "ImmediateSupervisor1");

            migrationBuilder.CreateIndex(
                name: "IX_FK_SubDepartmentEmployee",
                table: "Employees",
                column: "SubDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_BranchDepartmentEmployee",
                table: "Employees",
                columns: new[] { "BranchID", "DepartmentID" });

            migrationBuilder.CreateIndex(
                name: "IX_FK_MenuMenu",
                table: "Menus",
                column: "ParentMenuID");

            migrationBuilder.CreateIndex(
                name: "IX_FK_DivHeadProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "DivisionHead");

            migrationBuilder.CreateIndex(
                name: "IX_FK_ItemCategoryProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_EmployeeProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "ReqBy");

            migrationBuilder.CreateIndex(
                name: "IX_FK_VendorProcuremenetRequest",
                table: "ProcuremenetRequests",
                column: "SelectedVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_ItemCategoryProcurementReqItem",
                table: "ProcurementReqItems",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_ProcuremenetRequestProcurementReqItem",
                table: "ProcurementReqItems",
                column: "ProReqId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenuAccesses_MenuID",
                table: "RoleMenuAccesses",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_FK_DepartmentSubDepartment",
                table: "SubDepartments",
                column: "DeptID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleID",
                table: "UserRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserID",
                table: "UserRoles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchID",
                table: "Users",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentID",
                table: "Users",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_FK_EmployeeUser",
                table: "Users",
                column: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "ProcurementReqItems");

            migrationBuilder.DropTable(
                name: "RoleMenuAccesses");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ProcuremenetRequests");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "SubDepartments");

            migrationBuilder.DropTable(
                name: "BranchDepartments");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
