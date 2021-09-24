USE [dbNIBM];
GO

SET IDENTITY_INSERT [dbo].[Parameters] ON
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (1, N'DG_EmployeeID', N'1')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (2, N'HRM_AO', N'1')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (3, N'HRM_DD', N'1')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (4, N'HRM_MA', N'1')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (5, N'Intake_Start_Date', N'2016-01-01')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (6, N'Intake_End_Date', N'2016-12-31')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (7, N'Lec_Subsistence_Allow', N'')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (8, N'Exec_Subsistence_Allow', N'')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (9, N'Otr_Subsistence_Allow', N'')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (10, N'Lec_Transport_Col_KaMa', N'')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (11, N'Lec_Transport_Col_KuGl', N'')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (12, N'Lec_Transport_Gl_Ma', N'')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (13, N'Lec_Transport_Ka_Ku', N'')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (14, N'ExecOtr_Transport_Col_KaMa', N'')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (15, N'ExecOtr_Transport_Col_KuGl', N'')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (16, N'Last_Leave_Tarnsfered_Year', N'2015')
INSERT INTO [dbo].[Parameters] ([ParameterID], [ParameterCode], [ParameterValue]) VALUES (17, N'Chairman_EmployeeID', N'211')
SET IDENTITY_INSERT [dbo].[Parameters] OFF

SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT INTO [dbo].[Roles] ([RoleID], [Code], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Admin', N'Administrator', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Roles] ([RoleID], [Code], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'HrUser', N'HR Department User', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Roles] ([RoleID], [Code], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'ProgOfficeUser', N'Programe Office User', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Roles] ([RoleID], [Code], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'LecturerUser', N'Lecturer User', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Roles] ([RoleID], [Code], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'AdminUser', N'Admin Department User', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Roles] ([RoleID], [Code], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (6, N'FinanceUser', N'Finance Department User', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Roles] ([RoleID], [Code], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (7, N'ExamUser ', N'Examination Department User ', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Roles] OFF

SET IDENTITY_INSERT [dbo].[Menus] ON
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (1, NULL, 10, N'Admin', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (2, NULL, 20, N'Organization', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (3, NULL, 30, N'Student', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (4, NULL, 40, N'Sponsor', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (5, NULL, 50, N'Program', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (6, NULL, 60, N'Examination', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (7, NULL, 70, N'HR', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (8, NULL, 80, N'Cashier', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (9, NULL, 90, N'Finance', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (10, NULL, 110, N'Pending Approval', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (11, 1, 10, N'User Information', N'I', N'Admin', N'Users', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (12, 1, 20, N'Branch Information', N'I', N'Admin', N'Branches', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (13, 1, 30, N'Department Information', N'I', N'Admin', N'Departments', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (14, 1, 40, N'Branch Department', N'I', N'Admin', N'BranchDepartments', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (15, 1, 50, N'Salary Category Information', N'I', N'Admin', N'SalaryCatagories', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (16, 1, 60, N'Designation Information', N'I', N'Admin', N'Designations', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (17, 2, 10, N'Employee Information', N'I', N'Admin', N'Employees', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (18, 2, 20, N'Subject Information', N'I', N'Organization', N'Subjects', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (19, 2, 30, N'Hall Information', N'I', N'Organization', N'Halls', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (20, 2, 40, N'Expense Types', N'I', N'Organization', N'ExpenseTypes', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (21, 3, 10, N'Enrollment', N'I', N'Student', N'Enrollments', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (22, 3, 20, N'Course Registration', N'I', N'Student', N'CourseRegistrations', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (23, 3, 30, N'Other Invoice', N'I', N'Student', N'OtherInvoices', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (24, 3, 40, N'Transfer', N'I', N'Student', N'Transfers', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (25, 3, 50, N'Dropout', N'I', N'Student', N'Dropouts', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (26, 3, 80, N'Print Envelope', N'I', N'Student', NULL, N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (27, 3, 100, N'Print Certificate', N'I', N'Student', NULL, N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (28, 4, 10, N'Registration', N'I', N'Sponsor', N'SponsorRegistrations', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (29, 4, 20, N'Course Invoice', N'I', N'Sponsor', N'SponsorPayments', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (30, 5, 10, N'Add Visiting Lecturer', N'I', N'Program', N'VisitingLecturers', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (31, 5, 20, N'Course Information', N'I', N'Program', N'Courses', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (32, 5, 30, N'Batch', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (33, 32, 10, N'Batch Information', N'I', N'Program', N'Batches', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (34, 32, 20, N'Budget Information', N'I', N'Program', N'Budgets', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (35, 5, 40, N'Approve Budget', N'I', N'Program', N'Budgets', N'ApproveIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (36, 5, 50, N'Amend Budget', N'I', N'Program', N'Budgets', N'AmendIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (37, 32, 30, N'Batch Subjects', N'I', N'Program', N'BatchSubjects', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (38, 32, 40, N'Batch Finalize', N'I', N'Program', N'Batches', N'Finalize')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (39, 32, 50, N'Batch Lectures', N'I', N'Program', N'BatchLectures', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (40, 5, 80, N'Reports', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (41, 6, 10, N'Manage Exam', N'I', N'Examination',N'ExamManagements', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (42, 6, 20, N'Transcript', N'I', N'Examination', NULL, N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (43, 6, 30, N'Marks', N'I', N'Examination', N'ExamMarks', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (44, 6, 40, N'Finalize To Award Ceremony', N'I', N'Examination', NULL, N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (45, 7, 10, N'OPD Claim', N'I', N'HR', N'OpdClaims', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (46, 8, 10, N'Payment Receipt', N'I', N'Cashier', N'PaymentReceipts', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (47, 9, 10, N'Batch Post', N'I', N'Finance', N'BatchPostings', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (48, 40, 10, N'Signing Sheet', N'I', N'Program', N'Reports', N'SigningSheet')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (49, 40, 20, N'Participant List - For Accounts', N'I', N'Program', N'Reports', N'ParticipantList')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (50, 40, 30, N'Cost Estimation Report', N'I', N'Program', N'Reports', N'CostEstimation')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (51, 32, 60, N'Actual Cost - Batch', N'I', N'Program', N'BatchActualCost', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (52, 2, 50, N'General Codes', N'I', N'Organization', N'GeneralCodes', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (53, 40, 40, N'Dropout Students', N'I', N'Program', N'Reports', N'DropoutStudentsReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (54, 2, 60, N'Expenses - For General Codes', N'I', N'Organization', N'GeneralExpenses', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (55, 40, 50, N'Batch Progress Report', N'I', N'Program', N'Reports', N'BatchProgress')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (56, 7, 60, N'Reports', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (57, 56, 10, N'OPD Claim Letter', N'I', N'HR', N'Reports', N'OpdClaims')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (58, 56, 20, N'OPD Claim Settlement Report', N'I', N'HR', N'Reports', N'OpdClaimSettle')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (59, 1, 70, N'Employee', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (60, 59, 10, N'Employee Promotion', N'I', N'Admin', N'EmployeePromotion', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (61, 59, 20, N'Employee Transfer', N'I', N'Admin', N'EmployeeTransfers', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (62, 40, 60, N'Student Data Report - By Batch', N'I', N'Program', N'Reports', N'StudentDataByBatch')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (63, 7, 20, N'Communication Bill Tracker', N'I', N'HR', N'CommunicationBillTracker', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (64, 40, 70, N'Lecture Report - By Lecturer', N'I', N'Program', N'Reports', N'LectureReportByLecturer')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (65, 40, 80, N'Lecture Hours Summary Report', N'I', N'Program', N'Reports', N'LectureHoursSummary')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (66, 1, 80, N'Vehicle', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (67, 66,10, N'Vehicle Information', N'I', N'Admin', N'Vehicles', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (68, 32, 70, N'Batch Cancellation - Student Refund', N'I', N'Program', N'BatchCancellation', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (70, 5, 60, N'Lecturer Extra Hours', N'I', N'Program', N'LecturerExtraHrs', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (71, 59, 30, N'Duty Leave - Completion', N'I', N'Admin', N'EmployeeDutyLeaveCompletions', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (72, 1, 110, N'Reports', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (73, 72, 10, N'Employee History Report', N'I', N'Admin', N'Reports', N'EmployeeHistory')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (74, 72, 20, N'Vehicle Maintenance Report', N'I', N'Admin', N'Reports', N'VehicleMaintenance')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (75, 56, 30, N'Employee Monthly Communication Bill Report', N'I', N'HR', N'Reports', N'EmployeeMonthlyCommunicationBillClaim')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (76, 1, 90, N'Vendor Information', N'I', N'Admin', N'Vendors', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (77, 66,20, N'Vehicle Maintenance', N'I', N'Admin', N'VehicleMaintenances', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (78, 56, 40, N'Monthly Communication Bill Claim Report', N'I', N'HR', N'Reports', N'MonthlyCommunicationBillClaim')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (79, 9, 20, N'Confirm Dropout', N'I', N'Student', N'Dropouts', N'DeductionIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (80, 66,30, N'Vehicle Request', N'I', N'Admin', N'VehicleRequests', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (81, 10,10, N'Approve Lecture Schedule - Lecturer Wise', N'I', N'Program', N'BatchLectures', N'ApproveIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (82, 9, 30, N'Reports', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (83, 82, 10, N'Lecture Hour Statement Report', N'I', N'Finance', N'Reports', N'LectureHourStatement')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (84, 82, 20, N'Batch List - for Incentive', N'I', N'Finance', N'Reports', N'BatchList')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (85, 10, 20, N'Duty Leave - Approve', N'I', N'HR', N'EmployeeDutyLeaves', N'ApproveIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (86, 82, 30, N'Day Time Lecturer Hour Summary Report', N'I', N'Finance', N'Reports', N'DayTimeLecturerHourSummary')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (87, 5, 70, N'Exam Management', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (88, 87, 10, N'Exam Schedule', N'I',  N'Program', N'ExamSchedules', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (89, 87, 20, N'Exam Candidates', N'I',  N'Program', N'ExamCandidates', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (90, 82, 40, N'Batch Actual Cost Report', N'I',  N'Finance', N'Reports', N'BatchActualCost')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (91, 6, 50, N'Reports', N'M', N'Examination', NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (92, 91, 10, N'Exam Shedule Report', N'I', N'Examination', N'Reports', N'ExamSchedule')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (93, 56, 60, N'Employee Duty Leave Report', N'I', N'HR', N'Reports', N'EmployeeDutyLeave')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (94, 56, 70, N'All Employee Duty Leave Report', N'I', N'HR', N'Reports', N'EmployeeDutyLeaveAll')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (95, 82, 50, N'Posted Batch Details Report', N'I',  N'Finance', N'Reports', N'PostedBatchDetails')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (96, 91, 20, N'Exam Signing Sheet', N'I',  N'Examination', N'Reports', N'ExamSigningSheet')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (97, 9, 20, N'Confirm Student Transfer', N'I', N'Finance', N'TransferConfirmations', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (98, 56, 50, N'Monthly Communication Bill Summary Report', N'I', N'HR', N'Reports', N'AllEmployeeBillSummaryReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (99, 3, 110, N'Enrollment - Existing EMS Student', N'I', N'Student', N'EnrollmentOldStudent', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (100, 72, 30, N'Vehicle Request Report', N'I', N'Admin', N'Reports', N'VehicleRequest')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (101, 7, 20, N'Communication Bill Tracker Overview', N'I', N'HR', N'CommunicationBillTracker', N'Overview')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (102, 91, 30, N'Exam Result Sheet ', N'I', N'Examination', N'Reports', N'ExamResultSheet')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (104, 10, 30, N'Short Leave - Approve', N'I', N'HR', N'EmployeeShortLeave', N'ApproveIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (105, 66, 40, N'Particular', N'I', N'Admin', N'Particular', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (106, 66, 50, N'Particular Rate', N'I', N'Admin', N'ParticularRate', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (107, 1, 100, N'Fuel Consumption', N'I', N'Admin', N'FuelConsumption', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (108, 72, 40, N'Fuel Consumption Report', N'I', N'Admin', N'Reports', N'FuelConsumption')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (109, 7, 50, N'Employee Extra Work', N'I', N'HR', N'EmployeeExtraWorks', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (110, 2, 70, N'Invoice - For Employee', N'I', N'Organization', N'EmployeeInvoices', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (111, 40, 90, N'Debtor Report', N'I', N'Program', N'Reports', N'DebtorReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (112, 156, 10, N'Annual Intake Batch Report', N'I', N'Program', N'Reports', N'AnnualIntakeBatchReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (113, 40, 110, N'On Going Batch Summary Report', N'I', N'Program', N'Reports', N'OnGoingBatchReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (115, 7, 30, N'Employee Leave', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (116, 10, 40, N'Lieu Leave - Approve', N'I', N'HR', N'EmployeeLieuLeave', N'ApproveIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (117, 82, 60, N'Paid Library Deposit Summary Report', N'I',  N'Finance', N'Reports', N'PaidLibraryDepositSummaryReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (118, 40, 120, N'Intake Batch Progress Report', N'I', N'Program', N'Reports', N'IntakeBatchProgress')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (119, 7, 70, N'Health Care', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (120, 119, 10, N'Employee Allergy Information',  N'I', N'HR', N'EmployeeAllergy', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (121, 119, 20, N'Employee Long Term Treatment Information',  N'I', N'HR', N'EmployeeLongTermTreatments', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (122, 119, 60, N'Diagnosis Information',  N'I', N'HR', N'Diagnosis', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (123, 40, 130, N'Library Deposit Paid List', N'I', N'Program', N'Reports', N'LibraryDepositPaidList')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (124, 32, 80, N'Intake Visits / Calls', N'I', N'Program', N'Batches', N'IntakeCallsIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (125, 82, 70, N'Lecture Report - By Lecturer', N'I', N'Program', N'Reports', N'LectureReportByLecturer')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (127, 40, 140, N'Transfer Student Report', N'I', N'Program', N'Reports', N'TransferStudentReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (128, 2, 80, N'Reports', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (129,128, 10, N'Costing Report - For General Codes', N'I', N'Organization', N'Reports', N'CostingGenaralCodeReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (130, 40, 150, N'Exam Eligibility - Attendance', N'I',  N'Program', N'Reports', N'ExamEligibitityReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (131, 87, 30, N'Student Attendance', N'I', N'Program', N'StudentAttendance', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (132, 119, 40, N'Student Long Term Treatment Information', N'I', N'HR', N'StudentLTTreatment', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (133, 119, 30, N'Student Allergy Information', N'I', N'HR', N'StudentAllergy', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (134, 56, 80, N'Employee Short Leave Report', N'I', N'HR', N'Reports', N'EmployeeShortLeave')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (135, 56, 90, N'All Employee Short Leave Report', N'I', N'HR', N'Reports', N'EmployeeShortLeaveAll')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (136, 6, 60, N'Repeat Transfers', N'I', N'Examination', N'RepeatTransfers', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (137, 1, 120, N'Daily Payment', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (138, 137, 10, N'Daily Payment Type', N'I', N'Admin', N'DailyPaymentType', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (139, 137, 20, N'Daily Payment Student', N'I', N'Admin', N'DailyPaymentStudents', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (140, NULL, 100, N'Library', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (141, 140, 10, N'Library Refund', N'I', N'Library', N'RefundedLibDeposits', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (142, 9, 30, N'Confirm Library Deposit', N'I', N'Finance', N'LibrarayRefunds', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (69, 115, 10, N'Employee Duty Leave', N'I', N'HR', N'EmployeeDutyLeaves', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (103, 115, 20, N'Employee Short Leave', N'I', N'HR', N'EmployeeShortLeave', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (114, 115, 30, N'Employee Lieu Leave', N'I', N'HR', N'EmployeeLieuLeave', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (143, 1, 15, N'User Roles', N'I', N'Admin', N'UserRoles', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (144, 82, 80, N'Refunded Library Deposit Summary Report', N'I', N'Library', N'Reports', N'RefundedLibraryDeposit')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (145, 40, 160, N'Batch Summary Report', N'I', N'Program', N'Reports', N'BatchSummary')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (146, 137, 30, N'Daily Payment Attendance', N'I', N'Admin', N'DailyPaymentAttendance', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (147, 40, 170, N'OnGoing Progress Report with Revenue', N'I', N'Program', N'Reports', N'OnGoingProgressReportwithRevenue')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (148, 91, 40, N'Admission Card', N'I', N'Examination', N'Reports', N'AdmissionCard')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (149, 56, 100, N'All Employee Lieu Leave Report', N'I', N'HR', N'Reports', N'EmployeeLieuLeaveAll')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (150, 56, 110, N'Employee Lieu Leave Report', N'I', N'HR', N'Reports', N'EmployeeLieuLeave')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (151, 5, 90, N'Industrial Traning', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (152, 151, 10, N'Stusent Registration', N'I', N'Program', N'IndustrialTraningRegistration', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (153, 115, 20, N'Employee Leave - Permanent Staff', N'I', N'HR', N'EmployeeLeaves', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (154, 10, 30, N'Employee Leave - Approve', N'I', N'HR', N'EmployeeLeaves', N'ApproveIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (155, NULL, 111, N'Data Analysis', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (156,155, 10, N'Reports', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (159, 156, 80, N'Progress Comparission Report - PDF', N'I', N'Program', N'Reports', N'ProgressComparissionReportPDF')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (160, 115, 50, N'Check Leave Balance', N'I', N'HR', N'CheckLeaveBalance', N'LeaveBalance')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (161, 10, 30, N'Employee Leave - Approve', N'I', N'HR', N'EmployeeLeaves', N'ApproveIndex')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (162, 40, 160, N'Lecture Hours Approval Status Report', N'I',  N'Program', N'Reports', N'LecHrsApprovalStatusReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (163, 56, 110, N'Employee Extra Work Report', N'I', N'HR', N'Reports', N'EmployeeExtrawork')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (164, 56, 120, N'EmployeeLeave Report', N'I', N'HR', N'Reports', N'EmployeeLeave')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (165, 1, 120, N'Employee Leave Setup', N'I', N'HR', N'EmployeeLeaves', N'SetEmployeeLeave')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (166, 140, 20, N'Issue Card', N'I', N'Library', N'LibraryIssueCard', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (167, NULL, 120, N'Call Center', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (168, 167, 10, N'Inquiry Call', N'I', N'CallCenter', N'InquiryCall', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (169, 119, 70, N'Diagnosis Report', N'I', N'HR', N'Reports', N'Diagnosis')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (170, 140, 30, N'Report', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (171, 170, 10, N'Library Issue Card Report', N'I', N'Library', N'Reports', N'LibraryIssueCard')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (172, 167, 20, N'Inquary Call Registration', N'I', N'CallCenter', N'InquaryCallRegistration', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (173, 167, 30, N'Inquary Call to visit', N'I', N'CallCenter', N'InquaryCallToVisit', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (174, 167, 40, N'Inquary follow up', N'I', N'CallCenter', N'InquaryFollowUp', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (175, 167, 50, N'Intake Inquary Report', N'I', N'CallCenter', N'IntakeInquaryReport', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (176, 72, 50, N'All Employee Details', N'I', N'Admin', N'Reports', N'AllEmployeeReport')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (179, 40, 190, N'Batch List - for Incentive', N'I', N'Finance', N'Reports', N'BatchList')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (180, 137, 40, N'Daily Payment Report - For All', N'I', N'Admin', N'DailyPaymentAttendance', N'AllDailyPaymentStudentSalary')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (181, 6, 70, N'Exam Marks Setup', N'I', N'Examination', N'ExamMarksSetup', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (182, 6, 80, N'Mid/Presentation/Prac/Assign Marks', N'I', N'Examination', N'OtherExamMarks', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (184, 115, 30, N'Employee Leave - Contract Staff', N'I', N'HR', N'ContractEmpLeaves', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (185, 140, 40, N'Library Subject Information', N'I', N'Library', N'LibrarySubjects', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (186, 140, 50, N'Publisher Information', N'I', N'Library', N'LibraryPublisher', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (187, 140, 60, N'Subject Category Information', N'I', N'Library', N'LibraryCategory', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (188, 1, 130, N'KPI Indicator Setup', N'I', N'Admin', N'KPIIndicatorSetup', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (189, 2, 90, N'KPI', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (190, 189, 10, N'Employee Marks', N'I', N'Organization', N'KPIEmpMarks', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (191, 140, 70, N'Books', N'M', NULL, NULL, NULL)
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (192, 191, 10, N'New Book', N'I', N'Library', N'Books', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (193, 189, 20, N'Finalize Employee KPI Marks', N'I', N'Organization', N'KPIEmpMarks', N'Finalize')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (194, 151, 20, N'Student GPA', N'I', N'Program', N'StudentGPA', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (195, 189, 30, N'Employee Attendance', N'I', N'Organization', N'KPIEmployeeAttendance', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (196, 189, 40, N'Confirm KPI Marks', N'I', N'Organization', N'ConfirmKPIMarks', N'KPIMarks')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (197, 56, 130, N'KPI Evaluation Summary Report', N'I', N'HR', N'Reports', N'KPIEvaluateSummary')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (198, 151, 30, N'Daily Diary Issue', N'I', N'Program', N'DailyDiary', N'Index')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (199, 32, 90, N'Lecture Shedule', N'I', N'Program', N'Index', N'LectureSheduling')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (200, 56, 140, N'Employee KPI Performance Report', N'I', N'HR', N'Reports', N'KPIEmployeeIncentive')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (201, 91, 50, N'Exam Batch Schedule Report', N'I', N'Examination', N'Reports', N'ExamBatchSchedule')
INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (205, 156, 120, N'Student Age Analysis Report', N'I', N'Program', N'Reports', N'StudentAgeAnalysis')

SET IDENTITY_INSERT [dbo].[Menus] OFF

INSERT INTO [dbo].[RoleMenuAccesses] SELECT 1, [MenuID] FROM [dbo].[Menus] WHERE [Type] = 'M'

SET IDENTITY_INSERT [dbo].[Branches] ON
INSERT INTO [dbo].[Branches] ([BranchID], [Description], [Address], [TelNo_1], [TelNo_2], [FaxNo_1], [FaxNo_2], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Colombo', N'120/5, Wijerama Mawatha, Colombo 07', N'+94 115 321 000 - 30', N'+94 11 2693404', N'+94 11 2667769', N'+94 11 2685808', 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Branches] ([BranchID], [Description], [Address], [TelNo_1], [TelNo_2], [FaxNo_1], [FaxNo_2], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Kandy', N'No. 02, Asgiriya Road, Kandy', N'+94 81 5621604', N'+94 81 5621604', N'+94 37 2236651', NULL, 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Branches] ([BranchID], [Description], [Address], [TelNo_1], [TelNo_2], [FaxNo_1], [FaxNo_2], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'Kurunegala', N'No. 47, Bauddhaloka Mw, Kurunegala.', N'+94 37 5620298', N'+94 37 5620298', N'+94 37 2222778', NULL, 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Branches] ([BranchID], [Description], [Address], [TelNo_1], [TelNo_2], [FaxNo_1], [FaxNo_2], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'Galle', N'No. 132, Pettigalawatta, Galle.', N'+94 91 5636363', N'+94 91 5636363', N'+94 91 2241042', NULL, 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Branches] ([BranchID], [Description], [Address], [TelNo_1], [TelNo_2], [FaxNo_1], [FaxNo_2], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'Matara', N'No. 26, NSB Building (2nd Floor), Anagarika Dharmapala Mw, Matara.', N'+94 41 2237544', N'+94 41 2237545', N'+94 41 2237546', NULL, 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Branches] OFF

SET IDENTITY_INSERT [dbo].[Departments] ON
INSERT INTO [dbo].[Departments] ([DepartmentID], [Description], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'MIS', 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Departments] ([DepartmentID], [Description], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Management', 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Departments] ([DepartmentID], [Description], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'Administration', 0, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Departments] ([DepartmentID], [Description], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'Finance', 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Departments] ([DepartmentID], [Description], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'Language', 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Departments] OFF

INSERT INTO [dbo].[BranchDepartments] ([BranchID], [DepartmentID], [NavCostProfitCenter], [TelNo_1], [TelNo_2], [FaxNo_1], [FaxNo_2], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 1, N'03', N'0112667769', N'0112667769', N'0112667769', N'0112667769', 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[BranchDepartments] ([BranchID], [DepartmentID], [NavCostProfitCenter], [TelNo_1], [TelNo_2], [FaxNo_1], [FaxNo_2], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 2, N'01', N'0112667769', N'0112667769', N'0112667769', N'0112667769', 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[BranchDepartments] ([BranchID], [DepartmentID], [NavCostProfitCenter], [TelNo_1], [TelNo_2], [FaxNo_1], [FaxNo_2], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 1, N'13', N'0112667769', N'0112667769', N'0112667769', N'0112667769', 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', N'NIBM\erandi', N'2016-01-13 19:28:03')
INSERT INTO [dbo].[BranchDepartments] ([BranchID], [DepartmentID], [NavCostProfitCenter], [TelNo_1], [TelNo_2], [FaxNo_1], [FaxNo_2], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 2, N'11', N'0112667769', N'0112667769', N'0112667769', N'0112667769', 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', N'NIBM\erandi', N'2016-01-13 19:27:50')

SET IDENTITY_INSERT [dbo].[SalaryCatagories] ON
INSERT INTO [dbo].[SalaryCatagories] ([SalCategoryID], [Code], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'HM 2-1', N'Higher Management Level 2-1', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[SalaryCatagories] ([SalCategoryID], [Code], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'HM 1-3', N'Higher Management Level 1-3', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[SalaryCatagories] ([SalCategoryID], [Code], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'HM 1-1', N'Higher Management Level 1-1', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[SalaryCatagories] ([SalCategoryID], [Code], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'MM 1-1', N'Middle Management Level 1-1', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[SalaryCatagories] ([SalCategoryID], [Code], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'AR 2', N'No desc', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[SalaryCatagories] ([SalCategoryID], [Code], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (6, N'AR 1', N'No desc', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[SalaryCatagories] ([SalCategoryID], [Code], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (7, N'MA 4', N'Management Assistant Level 4', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[SalaryCatagories] ([SalCategoryID], [Code], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (8, N'JM 1-1', N'Junior Manager Level 1-1', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
SET IDENTITY_INSERT [dbo].[SalaryCatagories] OFF

SET IDENTITY_INSERT [dbo].[Designations] ON
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Director General', 0, 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Director (IT)', 1, 2, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'Director (PMD)', 1, 2, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'Head (Mgt Development)', 1, 3, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'Head (Consultancy)', 1, 3, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (6, N'Head (Productivity)', 1, 3, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (7, N'Head (Maths & Eng.)', 1, 3, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (8, N'Head (Computer Science)', 1, 3, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (9, N'Head (Regional Center)', 1, 3, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (10, N'Deputy Director (PMD)', 1, 4, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (11, N'Senior Consultant', 1, 5, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (12, N'Consultant', 1, 6, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (13, N'Director (HRM & Administration)', 2, 2, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (14, N'Director (Finance)', 2, 2, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (15, N'Additional Director (HR & Adminstration)', 2, 3, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (16, N'Additional Director (Procument)', 2, 3, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (17, N'Software Engineer', 2, 4, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (18, N'Network Administrator', 2, 8, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (19, N'Staff Assistant', 3, 7, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Designations] ([DesignationID], [Description], [EmployeeCatagory], [SalCategoryID], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (20, N'Program Coordinator', 2, 8, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Designations] OFF

SET IDENTITY_INSERT [dbo].[Employees] ON
INSERT INTO [dbo].[Employees] ([EmployeeID], [NavEmployeeID], [EPFNo], [FirstName], [MiddleName], [LastName], [FullName], [Address_1], [Address_2], [MobileNo_1], [MobileNo_2], [DongleNo], [TelNo_1], [TelNo_2], [ImmeContactName], [ImmeContactAddress], [ImmeContactTelNo], [Relationship], [OfficialEmail], [PersonalEmail], [DesignationID], [BranchID], [DepartmentID], [NICNo], [PassportNo], [Title], [DOB], [Gender], [Initials], [IsConsultant], [EmployeeType], [Status], [JoinedDate], [AppointmentDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'EM/1', 123, N'E A', NULL, N'Weerasinghe', N'E A Weerasinghe', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Wife', N'a@gmail.com', N'a@gmail.com', 12, 1, 2, N'673412345V', N'673412345NS34', 3, N'2015-10-16 00:00:00', 1, N'K D V M', 1, 0, 0, N'2015-10-16 00:00:00', N'2015-10-16 00:00:00', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Employees] ([EmployeeID], [NavEmployeeID], [EPFNo], [FirstName], [MiddleName], [LastName], [FullName], [Address_1], [Address_2], [MobileNo_1], [MobileNo_2], [DongleNo], [TelNo_1], [TelNo_2], [ImmeContactName], [ImmeContactAddress], [ImmeContactTelNo], [Relationship], [OfficialEmail], [PersonalEmail], [DesignationID], [BranchID], [DepartmentID], [NICNo], [PassportNo], [Title], [DOB], [Gender], [Initials], [IsConsultant], [EmployeeType], [Status], [JoinedDate], [AppointmentDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'EM/2', 1386, N'K D V M', NULL, N'Edirisinghe', N'K D V M Edirisinghe', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Wife', N'a@gmail.com', N'a@gmail.com', 12, 1, 2, N'673412345V', N'673412345ND89', 3, N'2015-10-16 00:00:00', 1, N'K D V M', 1, 0, 0, N'2015-10-16 00:00:00', N'2015-10-16 00:00:00', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Employees] ([EmployeeID], [NavEmployeeID], [EPFNo], [FirstName], [MiddleName], [LastName], [FullName], [Address_1], [Address_2], [MobileNo_1], [MobileNo_2], [DongleNo], [TelNo_1], [TelNo_2], [ImmeContactName], [ImmeContactAddress], [ImmeContactTelNo], [Relationship], [OfficialEmail], [PersonalEmail], [DesignationID], [BranchID], [DepartmentID], [NICNo], [PassportNo], [Title], [DOB], [Gender], [Initials], [IsConsultant], [EmployeeType], [Status], [JoinedDate], [AppointmentDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'EM/3', 1584, N'Erandi', N'Ayodya', N'Rathnayaka', N'Erandi Ayodya Rathnayaka', NULL, NULL, N'0712496994', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Wife', N'b@gmail.com', N'a@gmail.com', 17, 1, 1, N'885312322V', N'673412345V', 4, N'2015-10-16 00:00:00', 1, N'R M E A', 1, 1, 0, N'2015-10-16 00:00:00', N'2015-10-16 00:00:00', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Employees] ([EmployeeID], [NavEmployeeID], [EPFNo], [FirstName], [MiddleName], [LastName], [FullName], [Address_1], [Address_2], [MobileNo_1], [MobileNo_2], [DongleNo], [TelNo_1], [TelNo_2], [ImmeContactName], [ImmeContactAddress], [ImmeContactTelNo], [Relationship], [OfficialEmail], [PersonalEmail], [DesignationID], [BranchID], [DepartmentID], [NICNo], [PassportNo], [Title], [DOB], [Gender], [Initials], [IsConsultant], [EmployeeType], [Status], [JoinedDate], [AppointmentDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'EM/4', 581, N'Gangani', NULL, N'Wickramasinghe', N'Gangani Wickramasinhe', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Wife', N'c@gmail.com', N'a@gmail.com', 2, 1, 1, N'45123344V', N'673412345V', 4, N'2015-10-16 00:00:00', 1, N'G C', 1, 0, 0, N'2015-10-16 00:00:00', N'2015-10-16 00:00:00', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Employees] ([EmployeeID], [NavEmployeeID], [EPFNo], [FirstName], [MiddleName], [LastName], [FullName], [Address_1], [Address_2], [MobileNo_1], [MobileNo_2], [DongleNo], [TelNo_1], [TelNo_2], [ImmeContactName], [ImmeContactAddress], [ImmeContactTelNo], [Relationship], [OfficialEmail], [PersonalEmail], [DesignationID], [BranchID], [DepartmentID], [NICNo], [PassportNo], [Title], [DOB], [Gender], [Initials], [IsConsultant], [EmployeeType], [Status], [JoinedDate], [AppointmentDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'EM/5', 100, N'Kumari', NULL, N'De Mel', N'Kumari De Mel', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Wife', N'd@gmail.com', N'a@gmail.com', 20, 1, 1, N'45258355V', N'673412345V', 4, N'2015-10-16 00:00:00', 0, N'V K', 0, 0, 1, N'2015-10-16 00:00:00', N'2015-10-16 00:00:00', N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Employees] OFF

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([UserID], [UserName], [Password], [EmployeeID], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'erandi', N'1', 3, 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Users] ([UserID], [UserName], [Password], [EmployeeID], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'rananga', N'123', NULL, 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Users] ([UserID], [UserName], [Password], [EmployeeID], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'gangani', N'1', 4, 1, N'NIBM\rananga', N'2015-12-29 13:40:10', NULL, NULL)
INSERT INTO [dbo].[Users] ([UserID], [UserName], [Password], [EmployeeID], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'kumari', N'1', 5, 1, N'NIBM\rananga', N'2015-12-29 15:28:55', NULL, NULL)
INSERT INTO [dbo].[Users] ([UserID], [UserName], [Password], [EmployeeID], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'dg', N'1', 1, 1, N'NIBM\rananga', N'2015-12-29 15:29:07', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF

SET IDENTITY_INSERT [dbo].[UserRoles] ON
INSERT INTO [dbo].[UserRoles] ([UserRoleID], [UserID], [RoleID]) VALUES (1, 1, 1)
INSERT INTO [dbo].[UserRoles] ([UserRoleID], [UserID], [RoleID]) VALUES (2, 2, 1)
INSERT INTO [dbo].[UserRoles] ([UserRoleID], [UserID], [RoleID]) VALUES (3, 2, 2)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF

SET IDENTITY_INSERT [dbo].[Courses] ON
INSERT INTO [dbo].[Courses] ([CourseID], [DepartmentID], [CourseCode], [CourseName], [CourseLevel], [CourseType], [Medium], [DurationType], [Duration], [LecStartTime], [LecEndTime], [PaymentBasis], [TotalLecHours], [ParticipantCount], [CourseFee], [RegistrationFee], [LibraryDeposit], [NoOfInstallments], [InstallmentAmt], [ExpectedTotAmt], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 1, 4201, N'Certificate course in computer science', 0, 0, 2, 1, 4, N'08:30:00', N'12:30:00', 1, 312, 100, CAST(30000 AS Decimal(18, 0)), CAST(1000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 2, CAST(15000 AS Decimal(18, 0)), CAST(3000000 AS Decimal(18, 0)), 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Courses] ([CourseID], [DepartmentID], [CourseCode], [CourseName], [CourseLevel], [CourseType], [Medium], [DurationType], [Duration], [LecStartTime], [LecEndTime], [PaymentBasis], [TotalLecHours], [ParticipantCount], [CourseFee], [RegistrationFee], [LibraryDeposit], [NoOfInstallments], [InstallmentAmt], [ExpectedTotAmt], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 1, 4101, N'Diploma in computer system design', 1, 1, 3, 1, 1, N'08:00:00', N'16:00:00', 0, 1000, 100, CAST(160000 AS Decimal(18, 0)), CAST(2000 AS Decimal(18, 0)), CAST(3000 AS Decimal(18, 0)), 2, CAST(80000 AS Decimal(18, 0)), CAST(16000000 AS Decimal(18, 0)), 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[Courses] ([CourseID], [DepartmentID], [CourseCode], [CourseName], [CourseLevel], [CourseType], [Medium], [DurationType], [Duration], [LecStartTime], [LecEndTime], [PaymentBasis], [TotalLecHours], [ParticipantCount], [CourseFee], [RegistrationFee], [LibraryDeposit], [NoOfInstallments], [InstallmentAmt], [ExpectedTotAmt], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, 5, 4003, N'Advanced certificate in English', 0, 0, 2, 1, 4, N'01:30:00', N'16:30:00', 1, 500, 25, CAST(40000 AS Decimal(18, 0)), CAST(1000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 1, CAST(40000 AS Decimal(18, 0)), CAST(1000000 AS Decimal(18, 0)), 1, N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Courses] OFF


SET IDENTITY_INSERT [dbo].[ExpenseTypes] ON
INSERT INTO [dbo].[ExpenseTypes] ([ExpenseTypeID], [Code], [Description], [Rate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'01', N'Photocopy charges', CAST(5 AS Decimal(18, 0)), N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[ExpenseTypes] ([ExpenseTypeID], [Code], [Description], [Rate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'02', N'Tea Supply', CAST(15 AS Decimal(18, 0)), N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
INSERT INTO [dbo].[ExpenseTypes] ([ExpenseTypeID], [Code], [Description], [Rate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'03', N'Printing Papers', CAST(20 AS Decimal(18, 0)), N'ERANDI_PC\erandi', N'2015-01-01 00:00:00', NULL, NULL)
SET IDENTITY_INSERT [dbo].[ExpenseTypes] OFF

SET IDENTITY_INSERT [dbo].[Batches] ON
INSERT INTO [dbo].[Batches] ([BatchID], [BranchID], [DepartmentID], [CourseID], [BatchCode], [BatchName], [StartDate], [EndDate], [CourseLevel], [CourseType], [DurationType], [Duration], [LecStartTime], [LecEndTime], [PaymentBasis], [TotalLecHours], [ParticipantCount], [CourseFee], [RegistrationFee], [LibraryDeposit], [NoOfInstallments], [InstallmentAmt], [Secretary], [Director1], [Director2], [DivisionHead], [BatchStatus], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsIntakeBatch]) VALUES (1, 1, 1, 2, N'1-1-4002-152-F', N'15.2', N'2016-01-01 00:00:00', N'2016-12-31 00:00:00', 1, 1, 3, 1, N'08:00:00', N'16:00:00', 0, 1000, 100, CAST(160000 AS Decimal(18, 0)), CAST(2000 AS Decimal(18, 0)), CAST(3000 AS Decimal(18, 0)), 2, CAST(80000 AS Decimal(18, 0)), 3, 3, NULL, 4, 0, N'Rananga-NB\erandi', N'2015-12-07 21:14:55', N'Rananga-NB\erandi', N'2015-12-07 21:22:39', 'true')
SET IDENTITY_INSERT [dbo].[Batches] OFF

SET IDENTITY_INSERT [dbo].[Budgets] ON
INSERT INTO [dbo].[Budgets] ([BudgetID], [BatchID], [Revenue], [OrgTotalCost], [TotalCost], [OrgGrossContribution], [GrossContribution], [OrgContributionPercentage], [ContributionPercentage], [Comments], [BudgetStatus], [RejectedTimes], [LastRejectedBy], [LastRejectedDate], [ApprovedBy], [ApprovedDate], [IsAmmended], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 1, CAST(16000000.000 AS Decimal(12, 3)), CAST(11500.000 AS Decimal(12, 3)), CAST(0.000 AS Decimal(12, 3)), CAST(15988500.000 AS Decimal(12, 3)), CAST(16000000.000 AS Decimal(12, 3)), CAST(99.928 AS Decimal(12, 3)), CAST(100.000 AS Decimal(12, 3)), NULL, 2, 0, 3, N'2015-12-07 21:24:23', 3, N'2015-12-07 21:25:48', 0, N'Rananga-NB\erandi', N'2015-12-07 21:17:55', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Budgets] OFF

SET IDENTITY_INSERT [dbo].[BudgetDetails] ON
INSERT INTO [dbo].[BudgetDetails] ([DetailID], [BudgetID], [ExpenseTypeID], [OrgUnitPrice], [UnitPrice], [OrgQty], [Qty], [OrgAmount], [Amount], [IsAmmended], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 1, 3, CAST(20.000 AS Decimal(12, 3)), CAST(20.000 AS Decimal(12, 3)), 500, 500, CAST(10000.000 AS Decimal(12, 3)), CAST(10000.000 AS Decimal(12, 3)), 0, N'Rananga-NB\erandi', N'2015-12-07 21:17:55', NULL, NULL)
INSERT INTO [dbo].[BudgetDetails] ([DetailID], [BudgetID], [ExpenseTypeID], [OrgUnitPrice], [UnitPrice], [OrgQty], [Qty], [OrgAmount], [Amount], [IsAmmended], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 1, 2, CAST(10.000 AS Decimal(12, 3)), CAST(10.000 AS Decimal(12, 3)), 100, 100, CAST(1000.000 AS Decimal(12, 3)), CAST(1000.000 AS Decimal(12, 3)), 0, N'Rananga-NB\erandi', N'2015-12-07 21:17:55', NULL, NULL)
INSERT INTO [dbo].[BudgetDetails] ([DetailID], [BudgetID], [ExpenseTypeID], [OrgUnitPrice], [UnitPrice], [OrgQty], [Qty], [OrgAmount], [Amount], [IsAmmended], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, 1, 1, CAST(5.000 AS Decimal(12, 3)), CAST(5.000 AS Decimal(12, 3)), 100, 100, CAST(500.000 AS Decimal(12, 3)), CAST(500.000 AS Decimal(12, 3)), 0, N'Rananga-NB\erandi', N'2015-12-07 21:17:55', NULL, NULL)
SET IDENTITY_INSERT [dbo].[BudgetDetails] OFF

SET IDENTITY_INSERT [dbo].[Enrollments] ON
INSERT INTO [dbo].[Enrollments] ([EnrollmentID], [NavStudentID], [NICNo], [Title], [Gender], [FullName], [Initials], [LName], [Age], [PermenantAddress], [CurrentAddress], [TelNo], [MobileNo], [GuardianName], [GuardianAddress], [GuardianTelNo], [AlStaffConcession], [DipConcession], [HdConcession], [IsBlacklisted], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'860272580V', N'860272580V', 3, 0, N'Suraweera Arachchige Don Rananga Lakshitha', N'S A D R ', N'Lakshitha', 29, N'13/1, Udyana Avenue, Pepiliyana Road, Nugegoda', N'45C, School Avenue, Raththanapitiya Boralesgamuwa', 12809884, 713522384, N'H B G Amitha', N'13/1, Udyana Avenue, Pepiliyana Road, Nugegoda', 773411392, 0, 0, 0, N'False', N'Rananga-NB\erandi', N'2015-12-26 14:36:37', NULL, NULL)
INSERT INTO [dbo].[Enrollments] ([EnrollmentID], [NavStudentID], [NICNo], [Title], [Gender], [FullName], [Initials], [LName], [Age], [PermenantAddress], [CurrentAddress], [TelNo], [MobileNo], [GuardianName], [GuardianAddress], [GuardianTelNo], [AlStaffConcession], [DipConcession], [HdConcession], [IsBlacklisted], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'93458956V', N'93458956V', 4, 1, N'Pavithri Manike Bandara', N'P M ', N'Bandara', 20, N'Colombo', N'Colombo', 0, 0, N'M D Bandara', N'Colombo', 0, 0, 0, 0, N'False', N'Rananga-NB\erandi', N'2015-12-29 23:30:46', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Enrollments] OFF


SET IDENTITY_INSERT [dbo].[SponsorRegistrations] ON
INSERT INTO [dbo].[SponsorRegistrations] ([SponsorID], [NavSponsorID], [Name], [ContactPersonName], [Address], [TelNo1], [TelNo2], [MobileNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'SP/1', N'Bileeta', N'Sanka-Sanji', N'Epsi', N'12345678', N'87654321', N'18273645', N'Rananga-NB\erandi', N'2015-12-26 14:37:07', NULL, NULL)
SET IDENTITY_INSERT [dbo].[SponsorRegistrations] OFF

SET IDENTITY_INSERT [dbo].[BatchJournals] ON
INSERT INTO [dbo].[BatchJournals] ([BatchJournalID], [JournalName], [JournalType], [BalAccountNo]) VALUES (1, N'CASH - COLOMBO', N'ONLINE-COL', N'10205020100')
INSERT INTO [dbo].[BatchJournals] ([BatchJournalID], [JournalName], [JournalType], [BalAccountNo]) VALUES (3, N'CASH - KURUNEGALA', N'ONLINE-KUR', N'10205020200')
INSERT INTO [dbo].[BatchJournals] ([BatchJournalID], [JournalName], [JournalType], [BalAccountNo]) VALUES (5, N'CASH - KANDY', N'ONLINE-KAN', N'10205020300')
INSERT INTO [dbo].[BatchJournals] ([BatchJournalID], [JournalName], [JournalType], [BalAccountNo]) VALUES (6, N'CASH - GALLE', N'ONLINE-GAL', N'10205020400')
INSERT INTO [dbo].[BatchJournals] ([BatchJournalID], [JournalName], [JournalType], [BalAccountNo]) VALUES (7, N'CASH - MATARA', N'ONLINE-MAT', N'10205020500')
INSERT INTO [dbo].[BatchJournals] ([BatchJournalID], [JournalName], [JournalType], [BalAccountNo]) VALUES (8, N'CASH - RATHNAPURA', N'ONLINE_RAT', N'10205020600')
SET IDENTITY_INSERT [dbo].[BatchJournals] OFF

--="INSERT INTO [dbo].[Menus] ([MenuID], [ParentMenuID], [DisplaySeq], [Text], [Type], [Area], [Controller], [Action]) VALUES (" & A2 & ", " & IF(ISBLANK(B2),"NULL",B2) & ", " & C2 & ", N'" & D2 & "', N'" & E2 & "', " & IF(ISBLANK(F2),"NULL","N'" & F2 & "'") & ", " & IF(ISBLANK(G2),"NULL","N'" & G2 & "'") & ", " & IF(ISBLANK(H2),"NULL","N'" & H2 & "'") & ")"

--MenuID	ParentMenuID	DisplaySeq	Text	Type	Area	Controller	Action
--1		10	Admin	M			
--2		20	Organization	M			
--3		30	Student	M			
--4		40	Sponsor	M			
--5		50	Program	M			
--6		60	Examination	M			
--7	1	10	User Information	I	Admin	Users	Index
--8	1	20	Branch Information	I	Admin	Branches	Index
--9	1	30	Department Information	I	Admin	Departments	Index
--10	1	40	Branch Department	I	Admin	BranchDepartments	Index
--11	1	50	Salary Category Information	I	Admin	SalaryCatagories	Index
--12	1	60	Designation Information	I	Admin	Designations	Index
--13	2	10	Employee Information	I	Admin	Employees	Index
--14	2	20	Subject Information	I	Organization	Subjects	Index
--15	2	30	Hall Information	I	Organization	Halls	Index
--16	2	40	Expense Types		Organization	ExpenseTypes	Index
--17	3	10	Enrollment	I	Student		Index
--18	3	20	Course Registration	I	Student		Index
--19	3	30	Other Invoice	I	Student		Index
--20	3	40	Transfer	I	Student		Index
--21	3	50	Dropout	I	Student		Index
--22	3	60	Signing Sheet	I	Student		Index
--23	3	70	Add To Signing Sheet	I	Student		Index
--24	3	80	Print Envelope	I	Student		Index
--25	3	90	List of Participants	I	Student		Index
--26	3	100	Print Certificate	I	Student		Index
--27	4	10	Registration	I	Sponsor		Index
--28	4	20	Course Invoice	I	Sponsor		Index
--29	5	10	Course Information	I	Program	Courses	Index
--30	5	20	Batch Information	I	Program	Batches	Index
--31	5	30	Budget Information	I	Program		Index
--32	5	40	Amend Subjects	I	Program		Index
--33	5	50	Amend Budget	I	Program		Index
--34	5	60	Batch Finalize	I	Program		Index
--35	5	70	Add Lectures	I	Program		Index
--36	5	80	Inhouse / Workshop Course	I	Program		Index
--37	5	90	Reports	M			
--38	6	10	Manage Exam	I	Examination		Index
--39	6	20	Transcript	I	Examination		Index
--40	6	30	Marks	I	Examination		Index
--41	6	40	Add Mark sheet	I	Examination		Index