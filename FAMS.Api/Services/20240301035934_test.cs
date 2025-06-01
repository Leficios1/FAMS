using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAMS.Core.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearningObjectives",
                columns: table => new
                {
                    ObjectiveCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningObjectives", x => x.ObjectiveCode);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<string>(type: "char(2)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Syllabus = table.Column<byte>(type: "tinyint", nullable: false),
                    TrainingProgram = table.Column<byte>(type: "tinyint", nullable: false),
                    Class = table.Column<byte>(type: "tinyint", nullable: false),
                    LearningMaterial = table.Column<byte>(type: "tinyint", nullable: false),
                    UserManagement = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PermissionId = table.Column<string>(type: "char(2)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "UserPermissions",
                        principalColumn: "PermissionId");
                });

            migrationBuilder.CreateTable(
                name: "Syllabuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyllabusCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SyllabusName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TechnicalRequirement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttendeeNumber = table.Column<int>(type: "int", nullable: false),
                    CourseObjective = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingMaterials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingPrinciples = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Syllabuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Syllabuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    TrainingProgramCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    TopicCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms", x => x.TrainingProgramCode);
                    table.ForeignKey(
                        name: "FK_TrainingPrograms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssessmentSchemes",
                columns: table => new
                {
                    AssessmentSchemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyllabusId = table.Column<int>(type: "int", nullable: false),
                    Quiz = table.Column<double>(type: "float", nullable: false),
                    Assignment = table.Column<double>(type: "float", nullable: false),
                    Final = table.Column<double>(type: "float", nullable: false),
                    FinalTheory = table.Column<double>(type: "float", nullable: false),
                    FinalPractice = table.Column<double>(type: "float", nullable: false),
                    Passing = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSchemes", x => x.AssessmentSchemeId);
                    table.ForeignKey(
                        name: "FK_AssessmentSchemes_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyllabusObjectives",
                columns: table => new
                {
                    SyllabusObjectiveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyllabusId = table.Column<int>(type: "int", nullable: false),
                    ObjectiveCode = table.Column<string>(type: "nvarchar(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyllabusObjectives", x => x.SyllabusObjectiveId);
                    table.ForeignKey(
                        name: "FK_SyllabusObjectives_LearningObjectives_ObjectiveCode",
                        column: x => x.ObjectiveCode,
                        principalTable: "LearningObjectives",
                        principalColumn: "ObjectiveCode");
                    table.ForeignKey(
                        name: "FK_SyllabusObjectives_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainingUnits",
                columns: table => new
                {
                    UnitCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    SyllabusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingUnits", x => x.UnitCode);
                    table.ForeignKey(
                        name: "FK_TrainingUnits_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingProgramCode = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClassCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FSU = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_TrainingPrograms_TrainingProgramCode",
                        column: x => x.TrainingProgramCode,
                        principalTable: "TrainingPrograms",
                        principalColumn: "TrainingProgramCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramSyllabuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    SyllabusId = table.Column<int>(type: "int", nullable: false),
                    TrainingProgramCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramSyllabuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingProgramSyllabuses_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainingProgramSyllabuses_TrainingPrograms_TrainingProgramCode",
                        column: x => x.TrainingProgramCode,
                        principalTable: "TrainingPrograms",
                        principalColumn: "TrainingProgramCode");
                });

            migrationBuilder.CreateTable(
                name: "TrainingContents",
                columns: table => new
                {
                    TrainingContentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearningObjectiveCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    UnitCode = table.Column<int>(type: "int", nullable: false),
                    ContentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryType = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: true),
                    TrainingFormat = table.Column<byte>(type: "tinyint", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingContents", x => x.TrainingContentId);
                    table.ForeignKey(
                        name: "FK_TrainingContents_DeliveryTypes_DeliveryType",
                        column: x => x.DeliveryType,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainingContents_LearningObjectives_LearningObjectiveCode",
                        column: x => x.LearningObjectiveCode,
                        principalTable: "LearningObjectives",
                        principalColumn: "ObjectiveCode");
                    table.ForeignKey(
                        name: "FK_TrainingContents_TrainingUnits_UnitCode",
                        column: x => x.UnitCode,
                        principalTable: "TrainingUnits",
                        principalColumn: "UnitCode");
                });

            migrationBuilder.CreateTable(
                name: "ClassUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassUsers", x => new { x.UserId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_ClassUsers_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DeliveryTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Assignment/Lab" },
                    { 2, "Concept/Lecture" },
                    { 3, "Guide/Review" },
                    { 4, "Test/Quiz" },
                    { 5, "Exam" },
                    { 6, "Seminar/Workshop" }
                });

            migrationBuilder.InsertData(
                table: "LearningObjectives",
                columns: new[] { "ObjectiveCode", "Description" },
                values: new object[,]
                {
                    { "749C", "Explore libraries, frameworks, and tools commonly used in software development with a focus on C#, such as ASP.NET Core or Entity Framework." },
                    { "B73B", "Understand the basics of functional programming paradigms in C#, including immutability, higher-order functions, and lambda expressions." },
                    { "EC56", "Explore advanced topics like concurrency, threading, and parallel programming in C#, and understand how to manage concurrent execution." },
                    { "LO01", "Understand the basics of object-oriented programming" },
                    { "LO02", "Analyze and solve simple programming problems using loops and conditionals" },
                    { "LO03", "Design and implement algorithms to manipulate data structures" },
                    { "LO04", "Develop user-friendly graphical user interfaces for applications" },
                    { "LO05", "Apply software testing techniques to ensure code quality and reliability" }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "Class", "LearningMaterial", "RoleName", "Syllabus", "TrainingProgram", "UserManagement" },
                values: new object[,]
                {
                    { "AD", (byte)5, (byte)5, "Class Admin", (byte)5, (byte)5, (byte)1 },
                    { "SA", (byte)5, (byte)5, "Super Admin", (byte)5, (byte)5, (byte)5 },
                    { "TR", (byte)2, (byte)2, "Trainer", (byte)2, (byte)2, (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DateOfBirth", "Email", "Gender", "ModifiedBy", "ModifiedDate", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { 1, "LongV", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4344), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@example.com", "Male", null, null, "Super Admin", "superadmin123", "SA", "123-456-7890", true },
                    { 2, "LongV", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4362), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "classadmin@example.com", "Male", null, null, "Class Admin", "classadmin456", "AD", "987-654-3210", true },
                    { 3, "LongV", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4365), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nampt12@fpt.com", "Male", null, null, "Phan Thanh Nam", "trainer789", "TR", "555-123-4567", true },
                    { 4, "LongV", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4367), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngoctb30@fpt.com", "Female", null, null, "Tran Bao Ngoc", "reallystrongpass!", "AD", "091-3248-768", true }
                });

            migrationBuilder.InsertData(
                table: "Syllabuses",
                columns: new[] { "Id", "AttendeeNumber", "CourseObjective", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Priority", "PublishStatus", "SyllabusCode", "SyllabusName", "TechnicalRequirement", "TrainingMaterials", "TrainingPrinciples", "UserId", "Version" },
                values: new object[,]
                {
                    { 1, 35, null, "TungTS", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4583), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Active", (byte)1, "SC01", "Basic Cross-Platform Application Programming With .NET", null, null, null, 1, "1" },
                    { 2, 35, null, "TungTS", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4589), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Inactive", (byte)1, "SC02", "Software Testing", null, null, null, 1, "1" },
                    { 3, 35, null, "TungTS", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4590), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Active", (byte)1, "SC03", "Software Requirements", null, null, null, 1, "1" }
                });

            migrationBuilder.InsertData(
                table: "TrainingPrograms",
                columns: new[] { "TrainingProgramCode", "CreatedBy", "CreatedDate", "Duration", "ModifiedBy", "ModifiedDate", "Name", "StartTime", "Status", "TopicCode", "UserId" },
                values: new object[,]
                {
                    { 1, "TungTs", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4658), new TimeSpan(0, 7, 0, 0, 0)), 10f, null, new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4660), new TimeSpan(0, 7, 0, 0, 0)), "Training Program in C#", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4649), new TimeSpan(0, 7, 0, 0, 0)), (byte)1, null, 2 },
                    { 2, "TungTs", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4663), new TimeSpan(0, 7, 0, 0, 0)), 30f, null, new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4664), new TimeSpan(0, 7, 0, 0, 0)), "Beginner with C/C++", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4662), new TimeSpan(0, 7, 0, 0, 0)), (byte)1, null, 2 },
                    { 3, "TungTs", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4666), new TimeSpan(0, 7, 0, 0, 0)), 20f, null, new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4667), new TimeSpan(0, 7, 0, 0, 0)), "Learn how to make simple web in Java", new DateTimeOffset(new DateTime(2024, 3, 1, 10, 59, 34, 166, DateTimeKind.Unspecified).AddTicks(4666), new TimeSpan(0, 7, 0, 0, 0)), (byte)1, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "SyllabusObjectives",
                columns: new[] { "SyllabusObjectiveId", "ObjectiveCode", "SyllabusId" },
                values: new object[,]
                {
                    { 1, "LO01", 1 },
                    { 2, "LO02", 1 },
                    { 3, "LO03", 2 }
                });

            migrationBuilder.InsertData(
                table: "TrainingUnits",
                columns: new[] { "UnitCode", "DayNumber", "SyllabusId", "UnitName" },
                values: new object[,]
                {
                    { 1, 100, 1, "FPT University" },
                    { 2, 100, 1, "FPT Software" }
                });

            migrationBuilder.InsertData(
                table: "TrainingContents",
                columns: new[] { "TrainingContentId", "ContentName", "DeliveryType", "Duration", "LearningObjectiveCode", "Note", "TrainingFormat", "UnitCode" },
                values: new object[,]
                {
                    { 1, "Learn Better in Winform", 1, 20f, "LO01", null, (byte)1, 1 },
                    { 2, "Learn Better in JAVA", 2, 25f, "LO02", null, (byte)1, 1 },
                    { 3, "Learn Better in C#", 3, 15f, "LO03", null, (byte)1, 1 },
                    { 4, "Learn Better in C++", 4, 10f, "LO04", null, (byte)1, 2 },
                    { 5, "Working in Project", 5, 30f, "LO05", null, (byte)1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSchemes_SyllabusId",
                table: "AssessmentSchemes",
                column: "SyllabusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassCode",
                table: "Classes",
                column: "ClassCode",
                unique: true,
                filter: "[ClassCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TrainingProgramCode",
                table: "Classes",
                column: "TrainingProgramCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassUsers_ClassId",
                table: "ClassUsers",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_UserId",
                table: "Syllabuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusObjectives_ObjectiveCode",
                table: "SyllabusObjectives",
                column: "ObjectiveCode");

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusObjectives_SyllabusId",
                table: "SyllabusObjectives",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingContents_DeliveryType",
                table: "TrainingContents",
                column: "DeliveryType");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingContents_LearningObjectiveCode",
                table: "TrainingContents",
                column: "LearningObjectiveCode");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingContents_UnitCode",
                table: "TrainingContents",
                column: "UnitCode");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_UserId",
                table: "TrainingPrograms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramSyllabuses_SyllabusId",
                table: "TrainingProgramSyllabuses",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramSyllabuses_TrainingProgramCode",
                table: "TrainingProgramSyllabuses",
                column: "TrainingProgramCode");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingUnits_SyllabusId",
                table: "TrainingUnits",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PermissionId",
                table: "Users",
                column: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentSchemes");

            migrationBuilder.DropTable(
                name: "ClassUsers");

            migrationBuilder.DropTable(
                name: "SyllabusObjectives");

            migrationBuilder.DropTable(
                name: "TrainingContents");

            migrationBuilder.DropTable(
                name: "TrainingProgramSyllabuses");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropTable(
                name: "LearningObjectives");

            migrationBuilder.DropTable(
                name: "TrainingUnits");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");

            migrationBuilder.DropTable(
                name: "Syllabuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserPermissions");
        }
    }
}
