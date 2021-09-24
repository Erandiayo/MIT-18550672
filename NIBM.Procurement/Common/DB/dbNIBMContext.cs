using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NIBM.Procurement.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;

namespace NIBM.Procurement.DB
{
    public partial class dbNIBMContext : DbContext
    {
        public static string ConnectionString { get; set; }
        private bool EnableLazyLoading = true;

        public dbNIBMContext()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["dbProc"].ConnectionString;
        }

        public dbNIBMContext(bool enableLazyLoading)
        {
            EnableLazyLoading = enableLazyLoading;
        }

        public dbNIBMContext(DbContextOptions<dbNIBMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BranchDepartment> BranchDepartments { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<ProcuremenetRequest> ProcuremenetRequests { get; set; }
        public virtual DbSet<ProcurementReqItem> ProcurementReqItems { get; set; }
        public virtual DbSet<RoleMenuAccess> RoleMenuAccesses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SubDepartment> SubDepartments { get; set; }
        public virtual DbSet<Tender> Tenders { get; set; }
        public virtual DbSet<TenderVendor> TenderVendors { get; set; }
        public virtual DbSet<TECMember> TECMembers { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
                if (EnableLazyLoading)
                    optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchDepartment>(entity =>
            {
                entity.HasKey(e => new { e.BranchID, e.DepartmentID });

                entity.HasIndex(e => e.DepartmentID)
                    .HasName("IX_FK_DepartmentBranchDepartment");

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchDepartments)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchBranchDepartment");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.BranchDepartments)
                    .HasForeignKey(d => d.DepartmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentBranchDepartment");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.BranchID);

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentID);

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasKey(e => e.DesignationID);

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeID);

                entity.HasIndex(e => e.DesignationID)
                    .HasName("IX_FK_DesignationEmployee");

                entity.HasIndex(e => e.EPFNo)
                    .HasName("UC_EmployeeEPFNo")
                    .IsUnique();

                entity.HasIndex(e => e.ImmediateSupervisor1)
                    .HasName("IX_FK_EmployeeEmployee");

                entity.HasIndex(e => e.SubDeptId)
                    .HasName("IX_FK_SubDepartmentEmployee");

                entity.HasIndex(e => new { e.BranchID, e.DepartmentID })
                    .HasName("IX_FK_BranchDepartmentEmployee");

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DOB).HasColumnType("datetime");

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.Initials).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NICNo).IsRequired();


                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DesignationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DesignationEmployee");

                entity.HasOne(d => d.Supervisor1)
                    .WithMany(p => p.InverseImmediateSupervisor1Navigation)
                    .HasForeignKey(d => d.ImmediateSupervisor1)
                    .HasConstraintName("FK_EmployeeEmployee");

                entity.HasOne(d => d.SubDepartment)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.SubDeptId)
                    .HasConstraintName("FK_SubDepartmentEmployee");

                entity.HasOne(d => d.BranchDepartment)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => new { d.BranchID, d.DepartmentID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchDepartmentEmployee");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.MenuID);

                entity.HasIndex(e => e.ParentMenuID)
                    .HasName("IX_FK_MenuMenu");

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.Type).IsRequired();

                entity.HasOne(d => d.ParentMenu)
                    .WithMany(p => p.InverseParentMenu)
                    .HasForeignKey(d => d.ParentMenuID)
                    .HasConstraintName("FK_MenuMenu");
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.HasKey(e => e.ParameterID);

                entity.Property(e => e.ParameterCode).IsRequired();

                entity.Property(e => e.ParameterValue).IsRequired();
            });

            modelBuilder.Entity<ProcuremenetRequest>(entity =>
            {
                entity.HasKey(e => e.ProReqID);

                entity.HasIndex(e => e.DivisionHead)
                    .HasName("IX_FK_DivHeadProcuremenetRequest");

                entity.HasIndex(e => e.ReqBy)
                    .HasName("IX_FK_EmployeeProcuremenetRequest");

                entity.HasIndex(e => e.AwardedVendorId)
                   .HasName("IX_FK_AwardedVendors");

                entity.HasIndex(e => e.TenderID)
                    .HasName("IX_FK_TenderProcuremenetRequest");

                entity.Property(e => e.AmountPurchased).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CompletedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DivHeadAppORRejDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProcDeptReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.ReqDate).HasColumnType("datetime");

                entity.Property(e => e.ReqSubject).IsRequired();

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.DivHead)
                    .WithMany(p => p.ProcuremenetRequestsDivisionHeadNavigation)
                    .HasForeignKey(d => d.DivisionHead)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DivHeadProcuremenetRequest");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProcuremenetRequestsReqByNavigation)
                    .HasForeignKey(d => d.ReqBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeProcuremenetRequest");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.ProcuremenetRequests)
                    .HasForeignKey(d => d.AwardedVendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VendorProcuremenetRequest");

                entity.HasOne(d => d.Tender)
                    .WithMany(p => p.ProcuremenetRequests)
                    .HasForeignKey(d => d.TenderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenderProcuremenetRequest");
            });

            modelBuilder.Entity<ProcurementReqItem>(entity =>
            {
                entity.HasKey(e => e.ProReqItemID);

                entity.HasIndex(e => e.ItemCategoryId)
                    .HasName("IX_FK_ItemCategoryProcurementReqItem");

                entity.HasIndex(e => e.ProReqId)
                    .HasName("IX_FK_ProcuremenetRequestProcurementReqItem");

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ItemDesc).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ReqQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.ProReq)
                    .WithMany(p => p.ProcurementReqItems)
                    .HasForeignKey(d => d.ProReqId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcuremenetRequestProcurementReqItem");
            });

            modelBuilder.Entity<RoleMenuAccess>(entity =>
            {
                entity.HasKey(e => new { e.RoleID, e.MenuID });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleMenuAccesses)
                    .HasForeignKey(d => d.RoleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleRoleMenuAccess");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.RoleMenuAccesses)
                    .HasForeignKey(d => d.MenuID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuRoleMenuAccess");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleID);

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<TECMember>(entity =>
            {
                entity.HasKey(e => e.TECMemberID);

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IX_FK_TecMemberEmployee");

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TECMembers)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Employee_TECMembers");
            });

            modelBuilder.Entity<Tender>(entity =>
            {
                entity.HasKey(e => e.TenderID);

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TenderOpenedDate).IsRequired();

                entity.Property(e => e.TenderClosedDate).IsRequired();

                entity.Property(e => e.TenderOpenedDate).HasColumnType("datetime");
                entity.Property(e => e.TenderClosedDate).HasColumnType("datetime");
                entity.Property(e => e.TecApprovedDate).HasColumnType("datetime");
                entity.Property(e => e.TenderBoardApprovedDate).HasColumnType("datetime");
                entity.Property(e => e.ItemReceivedByVendorOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<TenderVendor>(entity =>
            {
                entity.HasKey(e => e.TenderVendorID);

                entity.HasIndex(e => e.TenderID)
                    .HasName("IX_FK_TenderVendorTenders");

                entity.Property(e => e.CreatedBy).IsRequired();
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Tender)
                   .WithMany(p => p.TenderVendors)
                   .HasForeignKey(d => d.TenderID)
                   .HasConstraintName("FK_TenderVendorTenders");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.TenderVendors)
                    .HasForeignKey(d => d.VendorID)
                    .HasConstraintName("FK_TenderVendorVendors");
            });

            modelBuilder.Entity<SubDepartment>(entity =>
            {
                entity.HasKey(e => e.SubDeptID);

                entity.HasIndex(e => e.DeptID)
                    .HasName("IX_FK_DepartmentSubDepartment");

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SubDepartments)
                    .HasForeignKey(d => d.DeptID)
                    .HasConstraintName("FK_DepartmentSubDepartment");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleID);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleUserRole");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserUserRole");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID);

                entity.HasIndex(e => e.EmployeeID)
                    .HasName("IX_FK_EmployeeUser");

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UserName).IsRequired();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeUser");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.VendorID);

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parameter>().HasData(
               new Parameter { ParameterID = 1, ParameterCode = "DG_EmployeeID", ParameterValue = "1" },
               new Parameter { ParameterID = 2, ParameterCode = "HRM_AO", ParameterValue = "1" },
               new Parameter { ParameterID = 3, ParameterCode = "HRM_DD", ParameterValue = "1" },
               new Parameter { ParameterID = 4, ParameterCode = "HRM_MA", ParameterValue = "1" });

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, Code = "Admin", Name = "Administrator", CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2015, 1, 1) });

            modelBuilder.Entity<Branch>().HasData(
               new Branch { BranchID = 1, Description = "Colombo", Address = "120/5, Wijerama Mawatha, Colombo 07", TelNo_1 = "+94 115 321 000 - 30", TelNo_2 = "+94 11 2693404", FaxNo_1 = "+94 11 2667769", FaxNo_2 = "+94 11 2685808", Status = ActiveState.Active, CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2015, 1, 1) });

            modelBuilder.Entity<Department>().HasData(
               new Department { DepartmentID = 1, Description = "MIS", Status = ActiveState.Active, CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2015, 1, 1) });

            modelBuilder.Entity<BranchDepartment>().HasData(
              new BranchDepartment { BranchID = 1, DepartmentID = 1, TelNo_1 = "0112667769", TelNo_2 = "0112667769", FaxNo_1 = "0112667769", FaxNo_2 = "0112667769", Status = ActiveState.Active, CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2015, 1, 1) });

            modelBuilder.Entity<Designation>().HasData(
               new Designation { DesignationID = 1, Description = "Consultant", CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2021, 5, 15) },
               new Designation { DesignationID = 2, Description = "Director-SOCE", CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2021, 5, 15) },
               new Designation { DesignationID = 3, Description = "Director-HRM", CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2021, 5, 15) },
               new Designation { DesignationID = 4, Description = "Director General", CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2021, 5, 15) },
               new Designation { DesignationID = 5, Description = "Head-Procurement", CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2021, 5, 15) },
               new Designation { DesignationID = 6, Description = "Management Assistant", CreatedBy = "ERANDI_PC\\erandi", CreatedDate = new DateTime(2021, 5, 15) });

            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   EmployeeID = 1,
                   EPFNo = 992,
                   FirstName = "Gangani",
                   MiddleName = "",
                   LastName = "Wikramasinghe",
                   FullName = "Gangani Wickramasinghe",
                   MobileNo_1 = "0712496994",
                   OfficialEmail = "b@gmail.com",
                   DesignationID = 2,
                   BranchID = 1,
                   DepartmentID = 1,
                   NICNo = "885312322V",
                   Title = Title.Ms,
                   DOB = new DateTime(1988, 1, 31),
                   Gender = Gender.Female,
                   Initials = "G C",
                   Status = ActiveState.Active,
                   CreatedBy = "ERANDI_PC\\erandi",
                   CreatedDate = new DateTime(2021, 5, 25)
               },
               new Employee
               {
                   EmployeeID = 2,
                   EPFNo = 1584,
                   FirstName = "Erandi",
                   MiddleName = "Ayodya",
                   LastName = "Rathnayaka",
                   FullName = "Erandi Ayodya Rathnayaka",
                   MobileNo_1 = "0712496994",
                   OfficialEmail = "b@gmail.com",
                   DesignationID = 1,
                   BranchID = 1,
                   DepartmentID = 1,
                   NICNo = "885312322V",
                   Title = Title.Ms,
                   DOB = new DateTime(1988, 1, 31),
                   Gender = Gender.Female,
                   Initials = "R M E A",
                   Status = ActiveState.Active,
                   CreatedBy = "ERANDI_PC\\erandi",
                   CreatedDate = new DateTime(2021, 5, 25),
                   ImmediateSupervisor1 = 1
               },
               new Employee
               {
                   EmployeeID = 3,
                   EPFNo = 450,
                   FirstName = "Ananda",
                   MiddleName = "",
                   LastName = "Kulasooriya",
                   FullName = "Ananda Kulasooriya",
                   MobileNo_1 = "0712496994",
                   OfficialEmail = "b@gmail.com",
                   DesignationID = 4,
                   BranchID = 1,
                   DepartmentID = 1,
                   NICNo = "885312322V",
                   Title = Title.Ms,
                   DOB = new DateTime(1988, 1, 31),
                   Gender = Gender.Female,
                   Initials = "D M A",
                   Status = ActiveState.Active,
                   CreatedBy = "ERANDI_PC\\erandi",
                   CreatedDate = new DateTime(2021, 5, 25)
               });

            modelBuilder.Entity<User>().HasData(new User
            {
                UserID = 1,
                UserName = "erandi",
                Password = "1",
                EmployeeID = 3,
                Status = ActiveState.Active,
                CreatedBy = "ERANDI_PC\\erandi",
                CreatedDate = new DateTime(2015, 1, 1)
            });

            modelBuilder.Entity<UserRole>().HasData(new UserRole { UserRoleID = 1, UserID = 1, RoleID = 1 });

            modelBuilder.Entity<Menu>().HasData(
                new Menu { MenuID = 1, ParentMenuID = 14, DisplaySeq = 10, Text = "Branches", Type = "I", Area = "Admin", Controller = "Branches", Action = "Index", Icon = "fas fa-map-marked-alt", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 2, ParentMenuID = 14, DisplaySeq = 20, Text = "Departments", Type = "I", Area = "Admin", Controller = "Departments", Action = "Index", Icon = "fas fa-vihara", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 3, ParentMenuID = 14, DisplaySeq = 30, Text = "Branch Departments", Type = "I", Area = "Admin", Controller = "BranchDepartments", Action = "Index", Icon = "fas fa-warehouse", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 4, ParentMenuID = 14, DisplaySeq = 50, Text = "Designations", Type = "I", Area = "Admin", Controller = "Designations", Action = "Index", Icon = "fas fa-people-arrows", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 5, ParentMenuID = 14, DisplaySeq = 60, Text = "Employees", Type = "I", Area = "Admin", Controller = "Employees", Action = "Index", Icon = "fas fa-user-tie", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 6, ParentMenuID = 13, DisplaySeq = 10, Text = "User Roles", Type = "I", Area = "Admin", Controller = "UserRoles", Action = "Index", Icon = "fas fa-address-card", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 7, ParentMenuID = 13, DisplaySeq = 20, Text = "Users", Type = "I", Area = "Admin", Controller = "Users", Action = "Index", Icon = "fa-solid fa-users", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 8, ParentMenuID = null, DisplaySeq = 90, Text = "New Request", Type = "I", Area = "Procurement", Controller = "ProcurementRequests", Action = "Index", Icon = "fas fa-envelope-open-text", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 9, ParentMenuID = null, DisplaySeq = 100, Text = "Pending Approval", Type = "I", Area = "Procurement", Controller = "ApproveRequest", Action = "Requests", Icon = "fas fa-check-square", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 10, ParentMenuID = null, DisplaySeq = 110, Text = "Procurement Department Process", Type = "I", Area = "Procurement", Controller = "ProcurementProcess", Action = "ProcurementProcessIndex", Icon = "fas fa-exchange-alt", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 11, ParentMenuID = 12, DisplaySeq = 20, Text = "Procurement Request Summary", Type = "I", Area = "Procurement", Controller = "Reports", Action = "ProcurementRequestSummaryReport", Icon = "null", Description = "Requests within a selected duration(Filter: Branch / Department).", IsHide = true, Reporttype = ReportType.PDF },
                new Menu { MenuID = 12, ParentMenuID = null, DisplaySeq = 140, Text = "Reports", Type = "M", Area = null, Controller = null, Action = null, Icon = "fas fa-file-alt", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 13, ParentMenuID = null, DisplaySeq = 10, Text = "Admin", Type = "M", Area = null, Controller = null, Action = null, Icon = "fas fa-user-tie", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 14, ParentMenuID = null, DisplaySeq = 20, Text = "Organization", Type = "M", Area = null, Controller = null, Action = null, Icon = "fas fa-building", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 15, ParentMenuID = null, DisplaySeq = 120, Text = "Tenders", Type = "I", Area = "Procurement", Controller = "TenderOpen", Action = "Index", Icon = "fas fa-dolly-flatbed", Description = "null", IsHide = false, Reporttype = ReportType.Both },
                new Menu { MenuID = 16, ParentMenuID = 12, DisplaySeq = 20, Text = "Monthly Progress Report", Type = "I", Area = "Procurement", Controller = "Reports", Action = "MonthlyProgressReport", Icon = "null", Description = "null", IsHide = false, Reporttype = ReportType.PDF },
                new Menu { MenuID = 17, ParentMenuID = 12, DisplaySeq = 30, Text = "Tender Details Of Tender", Type = "I", Area = "Procurement", Controller = "Reports", Action = "TenderDetailsOfTenderReport", Icon = "null", Description = "null", IsHide = false, Reporttype = ReportType.PDF },
                new Menu { MenuID = 18, ParentMenuID = 12, DisplaySeq = 40, Text = "Procurement Request Detail Report", Type = "I", Area = "Procurement", Controller = "Reports", Action = "ProcurementRequestDetailReport", Icon = "null", Description = "null", IsHide = false, Reporttype = ReportType.PDF });
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;

            var validationResults = new List<ValidationResult>();

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
                Validator.TryValidateObject(entity, validationContext, validationResults, true);
            }

            if (validationResults.Count > 0)
            {
                var entityValidationResult = validationResults.Select(x => new DbEntityValidationResult(new DbEntityEntry(), x.MemberNames.Select(y => new DbValidationError(y, x.ErrorMessage))));

                var excp = new DbEntityValidationException("Validation failed for one or more entities.", entityValidationResult);
                throw excp;
            }

            return base.SaveChanges();
        }

        public void Detach(object entity)
        {
            Entry(entity).State = EntityState.Detached;
        }

        public void UndoChanges()
        {
            // Undo the changes of the all entries. 
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    // Under the covers, changing the state of an entity from  
                    // Modified to Unchanged first sets the values of all  
                    // properties to the original values that were read from  
                    // the database when it was queried, and then marks the  
                    // entity as Unchanged. This will also reject changes to  
                    // FK relationships since the original value of the FK  
                    // will be restored. 
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // If the EntityState is the Deleted, reload the date from the database.   
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }
    }
}
