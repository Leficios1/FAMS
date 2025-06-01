using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAMS.Core.Migrations
{
    public partial class NewDb : Migration
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
                name: "FSU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FSU", x => x.Id);
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
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
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
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    PublishStatus = table.Column<int>(type: "int", nullable: false),
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
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassTimeEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Attendee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassTimeStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FSU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
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
                    TrainingFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "CalendarClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    DateAndTimeStudy = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarClasses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassTrainerUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitCode = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTrainerUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassTrainerUnits_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTrainerUnits_TrainingUnits_UnitCode",
                        column: x => x.UnitCode,
                        principalTable: "TrainingUnits",
                        principalColumn: "UnitCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTrainerUnits_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_TrainingContents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "TrainingContents",
                        principalColumn: "TrainingContentId",
                        onDelete: ReferentialAction.Cascade);
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
                    { "H4SD", "Understand the basics of functional programming paradigms in C#, including immutability, higher-order functions, and lambda expressions." },
                    { "K3BS", "Explore advanced topics like concurrency, threading, and parallel programming in C#, and understand how to manage concurrent execution." },
                    { "K4SD", "Explore libraries, frameworks, and tools commonly used in software development with a focus on C#, such as ASP.NET Core or Entity Framework." },
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
                columns: new[] { "Id", "AvatarUrl", "CreatedBy", "CreatedDate", "DateOfBirth", "Email", "Gender", "ModifiedBy", "ModifiedDate", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { 1, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9284), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@example.com", "Male", null, null, "Super Admin", "superadmin123", "SA", "1234567890", true },
                    { 2, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9306), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "classadmin@example.com", "Male", null, null, "Class Admin", "classadmin456", "AD", "9876543210", true },
                    { 3, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9308), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nampt12@fpt.com", "Male", null, null, "Phan Thanh Nam", "trainer789", "TR", "5551234567", true },
                    { 4, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9312), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngoctb30@fpt.com", "Female", null, null, "Tran Bao Ngoc", "reallystrongpass!", "AD", "0913248768", true },
                    { 5, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9316), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sontung@fpt.com", "Male", null, null, "Trinh Son Tung", "reallystrongpass!", "TR", "0908765123", true },
                    { 6, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9314), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanhuu3264@fpt.com", "Male", null, null, "Trinh Huu Tuan", "reallystrongpass!", "TR", "0905164896", true },
                    { 7, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9319), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xuanbinh@fpt.com", "Male", null, null, "Trinh Xuan Binh", "reallystrongpass!", "TR", "0907865123", true },
                    { 8, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9321), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lekhoa@fpt.com", "Male", null, null, "Trinh Le Khoa", "reallystrongpass!", "TR", "0908765132", true },
                    { 9, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9322), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duykhoa@fpt.com", "Male", null, null, "Vo Duy Khoa", "reallystrongpass!", "AD", "0907235423", true },
                    { 10, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9324), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duykhanh@fpt.com", "Male", null, null, "Vu Hoang Duy Khanh", "reallystrongpass!", "TR", "0907862333", true },
                    { 11, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9339), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mytien@fpt.com", "Female", null, null, "Vo Thi My Tien", "reallystrongpass!", "AD", "0907232333", true },
                    { 12, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9340), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhthuong@fpt.com", "Female", null, null, "Mai Thi Minh Thuong", "reallystrongpass!", "AD", "0907832233", true },
                    { 13, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9342), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "truongvan@fpt.com", "Male", null, null, "Tran Truong Van", "reallystrongpass!", "TR", "0907843233", true },
                    { 14, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9344), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhmanh@fpt.com", "Male", null, null, "Tran Minh Manh", "reallystrongpass!", "TR", "0902312333", true },
                    { 15, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9345), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "auphuonghanh@fpt.com", "Female", null, null, "Au Phuong Hanh", "auphuonghanh123", "TR", "0123456789", true },
                    { 16, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9348), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiepphuonghoa@fpt.com", "Female", null, null, "Tiep Phuong Hoa", "tiepphuonghoa456", "AD", "0234567890", true },
                    { 17, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9349), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "buiquynhnhu@fpt.com", "Female", null, null, "Bui Quynh Nhu", "buiquynhnhu789", "TR", "0345678901", true },
                    { 18, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9351), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hinhthaonguyen@fpt.com", "Female", null, null, "Hinh Thao Nguyen", "hinhthaonguyen123", "AD", "0456789012", true },
                    { 19, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9353), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "baothaovan@fpt.com", "Female", null, null, "Bao Thao Van", "baothaovan456", "TR", "0567890123", true },
                    { 20, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9354), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ongthienthu@fpt.com", "Female", null, null, "Ông Thiên Thư", "ongthienthu789", "SA", "0678901234", true },
                    { 21, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9356), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nghiemthuydiem@fpt.com", "Female", null, null, "Nghiem Thuy Diem", "nghiemthuydiem123", "TR", "0789012345", true },
                    { 22, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9357), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xungthuyvi@fpt.com", "Female", null, null, "Xung Thuy Vi", "xungthuyvi456", "AD", "0890123456", true },
                    { 23, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9359), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tatrucchi@fpt.com", "Female", null, null, "Ta Truc Chi", "tatrucchi789", "TR", "0901234567", true },
                    { 24, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9361), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kimtuyetchi@fpt.com", "Female", null, null, "Kim Tuyet Chi", "kimtuyetchi123", "TR", "0912345678", true },
                    { 25, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9364), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lohuucanh@fpt.com", "Male", null, null, "Lo Huu Canh", "lohuucanh123", "TR", "0123456789", true },
                    { 26, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9366), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tongan khang@fpt.com", "Male", null, null, "Tong An Khang", "tongan khang123", "AD", "0123456789", true },
                    { 27, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9368), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "buiphuhai@fpt.com", "Male", null, null, "Bui Phu Hai", "buiphuhai123", "TR", "0123456789", true },
                    { 28, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9369), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "luongquanghung@fpt.com", "Male", null, null, "Luong Quang Hung", "luongquanghung123", "AD", "0123456789", true },
                    { 29, "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg", "LongV", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9363), new TimeSpan(0, 7, 0, 0, 0)), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "thephi@fpt.com", "Male", null, null, "Do The Phi", "reallystrongpass!", "TR", "0907452233", true }
                });

            migrationBuilder.InsertData(
                table: "Syllabuses",
                columns: new[] { "Id", "AttendeeNumber", "CourseObjective", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Priority", "PublishStatus", "SyllabusCode", "SyllabusName", "TechnicalRequirement", "TrainingMaterials", "TrainingPrinciples", "UserId", "Version" },
                values: new object[,]
                {
                    { 1, 35, "<div>\r\n    <p>This topic is to introduce C# programming language knowledge; adapt trainees with skills, lessons, and practices specifically used in Fsoft projects. In detail, after completing the topic, trainees will:</p>\r\n    <ul>\r\n        <li>Understand basic concepts of high-level programming languages (keyword, statement, operator, control-of-flow)</li>\r\n        <li>Understand and distinguish two concepts: class (Class) and object (Object)</li>\r\n        <li>Understand and apply object-oriented programming knowledge to resolve simple problems (Inheritance, Encapsulation, Abstraction, Polymorphism)</li>\r\n        <li>Work with some of the existing data structures in C# (List, ArrayList, HashTable, Dictionary)</li>\r\n        <li>Know how to control program errors (use try ... catch..finally, throw, throws)</li>\r\n        <li>Be able to work with concurrency and multi-threading in C#</li>\r\n        <li>Be able to work with common classes in ADO.net: SqlConnection, SqlCommand, SqlParameter, SqlDataAdapter, SqlDataReader</li>\r\n        <li>Be able to manipulate SQL data from Window Form Application via 4 basic commands: Add, Update, Delete, Select</li>\r\n        <li>Know how to design UI screen in Window Form Application</li>\r\n        <li>Know how to use appropriate controls for each field/data type: Textbox, Label, Combobox, Radio, DateTimePicker, NumericUpDown, RichTextBox</li>\r\n    </ul>\r\n</div>", "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9477), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9480), new TimeSpan(0, 7, 0, 0, 0)), "Intership", 1, "SC01", "Basic Cross-Platform Application Programming With .NET", "<div>\r\n    <p>Trainees' PCs need to have the following software installed & run without any issues:</p>\r\n    <ul>\r\n        <li>Microsoft SQL Server 2005 Express</li>\r\n        <li>Microsoft Visual Studio 2017</li>\r\n        <li>Microsoft Office 2007 (Visio, Word, PowerPoint)</li>\r\n    </ul>\r\n</div>", "No Information", "<div>\r\n    <p>Since there is no specific information provided for Training Principles in the syllabus, here are some generic principles:</p>\r\n    <ul>\r\n        <li>Provide a conducive learning environment that fosters active participation and engagement.</li>\r\n        <li>Encourage collaborative learning through group activities, discussions, and peer interactions.</li>\r\n        <li>Emphasize hands-on practical exercises to reinforce theoretical concepts.</li>\r\n        <li>Ensure clear communication of learning objectives, expectations, and assessment criteria.</li>\r\n        <li>Provide constructive feedback and guidance to facilitate continuous improvement.</li>\r\n        <li>Foster a culture of respect, inclusivity, and professionalism among participants.</li>\r\n        <li>Encourage self-directed learning and exploration to deepen understanding and knowledge retention.</li>\r\n        <li>Adapt teaching methods and materials to cater to diverse learning styles and preferences.</li>\r\n        <li>Continuously evaluate and adjust training strategies based on feedback and performance analysis.</li>\r\n    </ul>", 1, "1" },
                    { 2, 35, "<div>\r\n    <p>This topic is to introduce C# programming language knowledge; adapt trainees with skills, lessons, and practices specifically used in Fsoft projects. In detail, after completing the topic, trainees will:</p>\r\n    <ul>\r\n        <li>Understand basic concepts of high-level programming languages (keyword, statement, operator, control-of-flow)</li>\r\n        <li>Understand and distinguish two concepts: class (Class) and object (Object)</li>\r\n        <li>Understand and apply object-oriented programming knowledge to resolve simple problems (Inheritance, Encapsulation, Abstraction, Polymorphism)</li>\r\n        <li>Work with some of the existing data structures in C# (List, ArrayList, HashTable, Dictionary)</li>\r\n        <li>Know how to control program errors (use try ... catch..finally, throw, throws)</li>\r\n        <li>Be able to work with concurrency and multi-threading in C#</li>\r\n        <li>Be able to work with common classes in ADO.net: SqlConnection, SqlCommand, SqlParameter, SqlDataAdapter, SqlDataReader</li>\r\n        <li>Be able to manipulate SQL data from Window Form Application via 4 basic commands: Add, Update, Delete, Select</li>\r\n        <li>Know how to design UI screen in Window Form Application</li>\r\n        <li>Know how to use appropriate controls for each field/data type: Textbox, Label, Combobox, Radio, DateTimePicker, NumericUpDown, RichTextBox</li>\r\n    </ul>\r\n</div>", "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9483), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9484), new TimeSpan(0, 7, 0, 0, 0)), "Fresher", 1, "SC02", "Software Testing", "<div>\r\n    <p>Trainees' PCs need to have the following software installed & run without any issues:</p>\r\n    <ul>\r\n        <li>Microsoft SQL Server 2005 Express</li>\r\n        <li>Microsoft Visual Studio 2017</li>\r\n        <li>Microsoft Office 2007 (Visio, Word, PowerPoint)</li>\r\n    </ul>\r\n</div>", "No Information", "<div>\r\n    <p>No specific information provided for Training Principles in the syllabus.</p>\r\n</div>", 1, "1" },
                    { 3, 35, "<div>\r\n    <p>The course objective for the \"Software Requirements\" syllabus is to provide students with a comprehensive understanding of software requirements engineering principles and practices. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the importance of gathering, analyzing, and documenting software requirements.</li>\r\n        <li>Learn various techniques for eliciting requirements from stakeholders.</li>\r\n        <li>Be able to create clear, concise, and unambiguous software requirement specifications.</li>\r\n        <li>Gain practical experience in identifying, prioritizing, and managing software requirements throughout the development lifecycle.</li>\r\n        <li>Develop skills in validating and verifying software requirements to ensure they meet stakeholders' needs and expectations.</li>\r\n        <li>Understand the role of software requirements in the overall software development process and its impact on project success.</li>\r\n    </ul>\r\n</div>", "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9486), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9487), new TimeSpan(0, 7, 0, 0, 0)), "Fresher", 1, "SC03", "Software Requirements", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Software Requirements\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Real-World Application: Emphasize practical, hands-on exercises and case studies to reinforce theoretical concepts and demonstrate their application in real-world scenarios.</li>\r\n        <li>Continuous Feedback: Provide regular feedback to students on their progress and performance to facilitate continuous improvement and adjustment of learning strategies.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can work together, share insights, and learn from each other's experiences.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach software requirements engineering as a problem-solving activity, focusing on identifying and addressing stakeholder needs and challenges.</li>\r\n        <li>Ethical Considerations: Highlight the importance of ethical behavior and professional conduct in software requirements engineering, emphasizing integrity, honesty, and respect for stakeholders' interests.</li>\r\n    </ul>\r\n</div>", 1, "1.1" },
                    { 4, 35, "<div>\r\n    <p>The course objective for the \"Software Development Project\" syllabus is to equip students with the necessary knowledge and skills to successfully manage and execute software development projects. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the key principles and methodologies of software project management.</li>\r\n        <li>Learn how to effectively plan, execute, and monitor software development projects.</li>\r\n        <li>Gain practical experience in defining project scope, objectives, and deliverables.</li>\r\n        <li>Develop skills in team management, communication, and collaboration within project teams.</li>\r\n        <li>Learn how to identify and mitigate risks associated with software development projects.</li>\r\n        <li>Understand the importance of quality assurance and testing in ensuring project success.</li>\r\n        <li>Learn how to manage project resources, budgets, and timelines effectively.</li>\r\n        <li>Gain insights into the various stages of the software development lifecycle and their impact on project management.</li>\r\n        <li>Be able to apply project management tools and techniques to real-world software development scenarios.</li>\r\n    </ul>\r\n</div>", "Trinh Huu Tuan", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9488), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9489), new TimeSpan(0, 7, 0, 0, 0)), "All Level", 1, "SC04", "Softwate Development Project", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Software Development Project\" syllabus include:</p>\r\n    <ul>\r\n        <li>Emphasize Agile Methodologies: Prioritize Agile principles and practices to adapt to changing project requirements and deliver high-quality software incrementally.</li>\r\n        <li>Effective Communication: Encourage open and transparent communication among project team members, stakeholders, and clients to ensure shared understanding and alignment of project goals.</li>\r\n        <li>Continuous Learning: Foster a culture of continuous learning and improvement within the project team, encouraging individuals to acquire new skills and stay updated with industry trends and best practices.</li>\r\n        <li>Collaborative Problem Solving: Promote collaborative problem-solving approaches where team members work together to identify and address project challenges, leveraging diverse perspectives and expertise.</li>\r\n        <li>Iterative Development: Embrace iterative development processes to rapidly prototype and refine software solutions based on feedback from stakeholders, minimizing the risk of costly errors and delays.</li>\r\n        <li>Quality Focus: Prioritize software quality throughout the development lifecycle, emphasizing the importance of testing, code reviews, and quality assurance practices to deliver robust and reliable software products.</li>\r\n        <li>Adaptability: Encourage adaptability and resilience in the face of changing project requirements, market conditions, and technological advancements, enabling teams to respond effectively to evolving project dynamics.</li>\r\n        <li>Stakeholder Engagement: Engage stakeholders actively throughout the project lifecycle, soliciting their input, addressing their concerns, and ensuring their satisfaction with project deliverables.</li>\r\n        <li>Project Transparency: Maintain transparency in project management processes, providing stakeholders with visibility into project progress, risks, and issues, fostering trust and accountability.</li>\r\n    </ul>\r\n</div>", 1, "1.2" },
                    { 5, 35, "<div>\r\n    <p>The course objective for the \"Software Development Project\" syllabus is to equip participants with the knowledge, skills, and tools necessary to effectively manage software development projects. Upon completion of the course, participants will:</p>\r\n    <ul>\r\n        <li>Understand the key principles and best practices of software project management.</li>\r\n        <li>Learn how to plan, execute, and monitor software development projects from initiation to closure.</li>\r\n        <li>Gain practical experience in defining project scope, objectives, and deliverables.</li>\r\n        <li>Develop skills in team management, communication, and collaboration within project teams.</li>\r\n        <li>Learn how to identify and mitigate risks associated with software development projects.</li>\r\n        <li>Understand the importance of quality assurance and testing in ensuring project success.</li>\r\n        <li>Learn how to manage project resources, budgets, and timelines effectively.</li>\r\n        <li>Gain insights into different project management methodologies and their applicability to various project scenarios.</li>\r\n    </ul>\r\n</div>", "", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9491), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9491), new TimeSpan(0, 7, 0, 0, 0)), "All Level", 0, "SC04", "Softwate Development Project", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Software Development Project\" syllabus include:</p>\r\n    <ul>\r\n        <li>Emphasize Agile Methodologies: Prioritize Agile principles and practices to adapt to changing project requirements and deliver high-quality software incrementally.</li>\r\n        <li>Effective Communication: Encourage open and transparent communication among project team members, stakeholders, and clients to ensure shared understanding and alignment of project goals.</li>\r\n        <li>Continuous Learning: Foster a culture of continuous learning and improvement within the project team, encouraging individuals to acquire new skills and stay updated with industry trends and best practices.</li>\r\n        <li>Collaborative Problem Solving: Promote collaborative problem-solving approaches where team members work together to identify and address project challenges, leveraging diverse perspectives and expertise.</li>\r\n        <li>Iterative Development: Embrace iterative development processes to rapidly prototype and refine software solutions based on feedback from stakeholders, minimizing the risk of costly errors and delays.</li>\r\n        <li>Quality Focus: Prioritize software quality throughout the development lifecycle, emphasizing the importance of testing, code reviews, and quality assurance practices to deliver robust and reliable software products.</li>\r\n        <li>Adaptability: Encourage adaptability and resilience in the face of changing project requirements, market conditions, and technological advancements, enabling teams to respond effectively to evolving project dynamics.</li>\r\n        <li>Stakeholder Engagement: Engage stakeholders actively throughout the project lifecycle, soliciting their input, addressing their concerns, and ensuring their satisfaction with project deliverables.</li>\r\n        <li>Project Transparency: Maintain transparency in project management processes, providing stakeholders with visibility into project progress, risks, and issues, fostering trust and accountability.</li>\r\n    </ul>\r\n</div>\r\n", 1, "1.1" },
                    { 6, 35, "<div>\r\n    <p>The course objective for the \"Computer Architecture\" syllabus is to provide participants with a comprehensive understanding of computer hardware and system architecture. Upon completion of the course, participants will:</p>\r\n    <ul>\r\n        <li>Gain a solid understanding of fundamental computer architecture principles and concepts.</li>\r\n        <li>Learn about different CPU architectures, instruction sets, and memory hierarchies.</li>\r\n        <li>Understand the role of operating systems in managing hardware resources and executing programs.</li>\r\n        <li>Explore advanced topics in computer architecture, such as parallel processing, pipelining, and cache optimization.</li>\r\n        <li>Develop skills in designing and optimizing computer systems for performance, reliability, and power efficiency.</li>\r\n        <li>Gain insights into emerging trends and technologies in computer architecture, such as cloud computing, IoT, and edge computing.</li>\r\n        <li>Apply theoretical knowledge to practical hands-on exercises and projects to reinforce learning.</li>\r\n    </ul>\r\n</div>", "", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9494), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9495), new TimeSpan(0, 7, 0, 0, 0)), "All Level", 0, "CE01", "Computer Architecture", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Computer Architecture\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Engage participants through interactive lectures, discussions, and hands-on activities to promote active learning and retention of knowledge.</li>\r\n        <li>Real-World Application: Emphasize practical applications of computer architecture principles through lab exercises, case studies, and project work to enhance practical skills and problem-solving abilities.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where participants can share insights, collaborate on projects, and learn from each other's experiences.</li>\r\n        <li>Continuous Feedback: Provide regular feedback to participants on their progress and performance to facilitate continuous improvement and adjustment of learning strategies.</li>\r\n        <li>Flexibility: Adapt the course delivery and content to accommodate participants with diverse backgrounds, learning styles, and skill levels.</li>\r\n        <li>Ethical Considerations: Emphasize ethical considerations in computer architecture design and implementation, including issues related to security, privacy, and intellectual property.</li>\r\n        <li>Lifelong Learning: Encourage participants to continue learning and staying updated with advancements in computer architecture beyond the course duration, fostering a culture of lifelong learning.</li>\r\n    </ul>\r\n</div>", 1, "1.3" },
                    { 7, 35, "<div>\r\n    <p>The course objective for the \"Enumarable Programming\" syllabus is to provide students with a comprehensive understanding of enumerable programming concepts and techniques. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the fundamental concepts of enumerable programming.</li>\r\n        <li>Be proficient in using enumerable methods and LINQ queries to manipulate data collections.</li>\r\n        <li>Learn how to efficiently work with sequences and iterators in C#.</li>\r\n        <li>Gain practical experience in applying enumerable programming techniques to solve real-world problems.</li>\r\n        <li>Develop skills in writing clean, readable, and maintainable code using enumerable programming paradigms.</li>\r\n        <li>Understand the benefits and limitations of enumerable programming compared to traditional imperative programming.</li>\r\n    </ul>\r\n</div>", "", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9501), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9502), new TimeSpan(0, 7, 0, 0, 0)), "Intern", 0, "EP04", "Enumarable Programming", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Enumarable Programming\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Hands-on Practice: Provide opportunities for students to practice enumerable programming concepts through coding exercises and projects.</li>\r\n        <li>Feedback and Reflection: Offer timely feedback on students' progress and encourage reflection on their learning experiences to promote continuous improvement.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can work together, share insights, and learn from each other's experiences.</li>\r\n        <li>Real-world Application: Emphasize the practical application of enumerable programming techniques in real-world scenarios to reinforce learning and skill development.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach programming challenges using a problem-solving mindset and apply enumerable programming techniques to find solutions.</li>\r\n    </ul>\r\n</div>\r\n", 1, "1.1" },
                    { 8, 35, "<div>\r\n    <p>The course objective for the \"Backend End With CSharp .NET\" syllabus is to provide students with a comprehensive understanding of backend development using C# and .NET framework. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the architecture and components of backend systems.</li>\r\n        <li>Be proficient in developing server-side applications using C# programming language.</li>\r\n        <li>Learn how to implement RESTful APIs and handle HTTP requests and responses.</li>\r\n        <li>Gain practical experience in working with databases, including SQL Server, for data storage and retrieval.</li>\r\n        <li>Develop skills in authentication and authorization mechanisms for securing backend services.</li>\r\n        <li>Understand best practices for error handling, logging, and performance optimization in backend development.</li>\r\n    </ul>\r\n</div>", "", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9503), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9504), new TimeSpan(0, 7, 0, 0, 0)), "Intern", 0, "CN01", "Backend End With CSharp .NET", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Backend End With CSharp .NET\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Hands-on Practice: Provide opportunities for students to work on practical projects and assignments to reinforce theoretical concepts.</li>\r\n        <li>Feedback and Reflection: Offer constructive feedback on students' work and encourage reflection on their learning progress to promote continuous improvement.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can collaborate on projects, share knowledge, and learn from each other's experiences.</li>\r\n        <li>Real-world Application: Emphasize the practical application of backend development principles in real-world scenarios to prepare students for industry requirements.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach programming challenges with a problem-solving mindset and develop creative solutions using backend technologies.</li>\r\n    </ul>\r\n</div>", 1, "1.1" },
                    { 9, 35, "<div>\r\n    <p>The course objective for the \"Backend End With Java Spring\" syllabus is to provide students with a comprehensive understanding of backend development using the Java Spring framework. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the architecture and components of backend systems.</li>\r\n        <li>Be proficient in developing server-side applications using Java Spring framework.</li>\r\n        <li>Learn how to implement RESTful APIs and handle HTTP requests and responses.</li>\r\n        <li>Gain practical experience in working with databases, including SQL Server, for data storage and retrieval.</li>\r\n        <li>Develop skills in authentication and authorization mechanisms for securing backend services.</li>\r\n        <li>Understand best practices for error handling, logging, and performance optimization in backend development.</li>\r\n    </ul>\r\n</div>\r\n", "", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9505), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9506), new TimeSpan(0, 7, 0, 0, 0)), "All Level", 0, "JS01", "Backend End With Java Spring", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Backend End With Java Spring\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Hands-on Practice: Provide opportunities for students to work on practical projects and assignments to reinforce theoretical concepts.</li>\r\n        <li>Feedback and Reflection: Offer constructive feedback on students' work and encourage reflection on their learning progress to promote continuous improvement.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can collaborate on projects, share knowledge, and learn from each other's experiences.</li>\r\n        <li>Real-world Application: Emphasize the practical application of backend development principles in real-world scenarios to prepare students for industry requirements.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach programming challenges with a problem-solving mindset and develop creative solutions using backend technologies.</li>\r\n    </ul>\r\n</div>", 1, "1.1" },
                    { 10, 35, "<div>\r\n    <p>The course objective for the \"Front End With React Js\" syllabus is to provide students with a comprehensive understanding of frontend development using the React.js library. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the fundamentals of React.js, including components, props, state, and lifecycle methods.</li>\r\n        <li>Be proficient in building dynamic user interfaces and single-page applications (SPAs) using React.js.</li>\r\n        <li>Learn how to manage application state using React's built-in state management and context API.</li>\r\n        <li>Gain practical experience in routing, form handling, and integrating third-party libraries with React.js.</li>\r\n        <li>Develop skills in using React.js in combination with other frontend technologies such as Redux for state management.</li>\r\n        <li>Understand best practices for performance optimization, code organization, and debugging in React.js development.</li>\r\n    </ul>\r\n</div>", "", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9508), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9509), new TimeSpan(0, 7, 0, 0, 0)), "All Level", 0, "RJ04", "Front End With React Js", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Front End With React Js\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Hands-on Practice: Provide opportunities for students to work on practical projects and assignments to reinforce theoretical concepts.</li>\r\n        <li>Feedback and Reflection: Offer constructive feedback on students' work and encourage reflection on their learning progress to promote continuous improvement.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can collaborate on projects, share knowledge, and learn from each other's experiences.</li>\r\n        <li>Real-world Application: Emphasize the practical application of frontend development principles in real-world scenarios to prepare students for industry requirements.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach programming challenges with a problem-solving mindset and develop creative solutions using React.js and associated technologies.</li>\r\n    </ul>\r\n</div>", 1, "1.1" },
                    { 11, 35, "<div>\r\n    <p>The course objective for the \"Computer Science\" syllabus is to provide participants with a comprehensive understanding of computer science fundamentals and principles. Upon completion of the course, participants will:</p>\r\n    <ul>\r\n        <li>Gain a solid understanding of core concepts in computer science, including algorithms, data structures, and problem-solving techniques.</li>\r\n        <li>Learn programming languages commonly used in computer science, such as Python, Java, or C++.</li>\r\n        <li>Understand the principles of software development, including software design, testing, and debugging.</li>\r\n        <li>Explore advanced topics in computer science, such as artificial intelligence, machine learning, and data science.</li>\r\n        <li>Develop practical programming skills through hands-on exercises, projects, and coding challenges.</li>\r\n        <li>Apply theoretical knowledge to real-world problems and scenarios, fostering critical thinking and analytical skills.</li>\r\n        <li>Prepare for further studies or careers in various fields of computer science, including software engineering, cybersecurity, and computer systems.</li>\r\n    </ul>\r\n</div>\r\n", "", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9496), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9497), new TimeSpan(0, 7, 0, 0, 0)), "All Level", 0, "CS23", "Computer Science", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Computer Science\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Engage participants through interactive lectures, group discussions, and problem-solving activities to promote active learning and participation.</li>\r\n        <li>Hands-On Experience: Provide ample opportunities for participants to apply theoretical concepts through practical coding exercises, projects, and labs.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where participants can work together, share ideas, and learn from each other's experiences.</li>\r\n        <li>Continuous Feedback: Offer regular feedback and assessments to participants to monitor their progress and provide guidance for improvement.</li>\r\n        <li>Adaptability: Tailor the course content and delivery methods to accommodate participants with diverse backgrounds, learning styles, and skill levels.</li>\r\n        <li>Ethical Considerations: Emphasize ethical behavior and responsible use of technology in all aspects of computer science education and practice.</li>\r\n        <li>Lifelong Learning: Encourage participants to cultivate a passion for lifelong learning and stay updated with advancements in the field of computer science.</li>\r\n    </ul>\r\n</div>", 1, "1.1" },
                    { 12, 35, "<div>\r\n    <p>The course objective for the \"Operation Service\" syllabus is to equip participants with the necessary knowledge and skills to efficiently manage and operate various services. Upon completion of the course, participants will:</p>\r\n    <ul>\r\n        <li>Understand the fundamentals of service management and operation.</li>\r\n        <li>Learn best practices for service deployment, configuration, and maintenance.</li>\r\n        <li>Gain proficiency in managing service availability, reliability, and performance.</li>\r\n        <li>Explore techniques for troubleshooting common service-related issues and incidents.</li>\r\n        <li>Develop skills in monitoring and optimizing service performance and resource utilization.</li>\r\n        <li>Understand the importance of security and compliance in service management.</li>\r\n        <li>Apply industry-standard tools and methodologies for effective service management and operation.</li>\r\n    </ul>\r\n</div>", "", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9498), new TimeSpan(0, 7, 0, 0, 0)), "Xung Thuy Vi", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 618, DateTimeKind.Unspecified).AddTicks(9499), new TimeSpan(0, 7, 0, 0, 0)), "All Level", 0, "OS21", "Operation Service", "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>", "No Information", "<div>\r\n    <p>The training principles for the \"Operation Service\" syllabus include:</p>\r\n    <ul>\r\n        <li>Hands-On Learning: Provide hands-on experience with real-world scenarios and practical exercises to reinforce theoretical concepts.</li>\r\n        <li>Collaborative Environment: Foster collaboration and teamwork among participants through group activities and discussions.</li>\r\n        <li>Continuous Improvement: Encourage continuous learning and improvement through feedback, reflection, and iteration.</li>\r\n        <li>Adaptability: Tailor the training approach to accommodate participants with varying skill levels and learning preferences.</li>\r\n        <li>Problem-Solving Skills: Develop participants' problem-solving skills by presenting them with challenging scenarios and encouraging them to find solutions.</li>\r\n        <li>Professional Development: Support participants in enhancing their professional skills and competencies relevant to service management and operation.</li>\r\n        <li>Ethical Considerations: Emphasize ethical practices and responsible decision-making in service management and operation.</li>\r\n    </ul>\r\n</div>", 1, "1.1" }
                });

            migrationBuilder.InsertData(
                table: "TrainingPrograms",
                columns: new[] { "TrainingProgramCode", "CreatedBy", "CreatedDate", "Duration", "ModifiedBy", "ModifiedDate", "Name", "StartTime", "Status", "TopicCode", "UserId" },
                values: new object[,]
                {
                    { 1, "Trinh Huu Tuan", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(487), new TimeSpan(0, 7, 0, 0, 0)), 40f, "Trinh Huu Tuan", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(488), new TimeSpan(0, 7, 0, 0, 0)), "Backend Programming C#", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(477), new TimeSpan(0, 7, 0, 0, 0)), 1, ".NET", 1 },
                    { 2, "Trinh Son Tung", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(491), new TimeSpan(0, 7, 0, 0, 0)), 40f, "Trinh Son Tung", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(492), new TimeSpan(0, 7, 0, 0, 0)), "Enumarable Programming", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(490), new TimeSpan(0, 7, 0, 0, 0)), 1, "CENUM", 3 },
                    { 3, "Tran Truong Van", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(495), new TimeSpan(0, 7, 0, 0, 0)), 20f, "Tran Truong Van", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(495), new TimeSpan(0, 7, 0, 0, 0)), "Front End React Js", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(494), new TimeSpan(0, 7, 0, 0, 0)), 1, ".RJS", 2 },
                    { 4, "Tran Truong Van", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(499), new TimeSpan(0, 7, 0, 0, 0)), 40f, "Tran Truong Van", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(499), new TimeSpan(0, 7, 0, 0, 0)), "Full Stack", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(497), new TimeSpan(0, 7, 0, 0, 0)), 1, ".FST", 2 },
                    { 5, "Tran Truong Van", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(502), new TimeSpan(0, 7, 0, 0, 0)), 40f, "Tran Truong Van", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(503), new TimeSpan(0, 7, 0, 0, 0)), "DevOps", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(500), new TimeSpan(0, 7, 0, 0, 0)), 1, ".FST", 2 },
                    { 6, "Tran Truong Van", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(505), new TimeSpan(0, 7, 0, 0, 0)), 40f, "Tran Truong Van", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(505), new TimeSpan(0, 7, 0, 0, 0)), "Database Engineer", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(504), new TimeSpan(0, 7, 0, 0, 0)), 1, ".FST", 2 }
                });

            migrationBuilder.InsertData(
                table: "AssessmentSchemes",
                columns: new[] { "AssessmentSchemeId", "Assignment", "Final", "FinalPractice", "FinalTheory", "Passing", "Quiz", "SyllabusId" },
                values: new object[,]
                {
                    { 1, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 1 },
                    { 2, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 2 },
                    { 3, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 3 },
                    { 4, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 4 },
                    { 5, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 5 },
                    { 6, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 6 },
                    { 7, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 7 },
                    { 8, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 8 },
                    { 9, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 9 },
                    { 10, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 10 },
                    { 11, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 11 },
                    { 12, 10.0, 30.0, 50.0, 50.0, 80.0, 60.0, 12 }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Attendee", "ClassCode", "ClassName", "ClassTimeEnd", "ClassTimeStart", "CreatedBy", "CreatedDate", "Duration", "EndDate", "FSU", "Location", "ModifiedBy", "ModifiedDate", "StartDate", "Status", "TrainingProgramCode" },
                values: new object[,]
                {
                    { 1, "Fresher", "HCM_CPL_NET_01", "Backend CSharp 01", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(601), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(602), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 1 },
                    { 2, "Fresher", "HCM_CPL_NET_02", "Backend CSharp 02", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(629), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(630), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 1 },
                    { 3, "Fresher", "HCM_CPL_NET_03", "Backend CSharp 03", new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(642), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(643), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 1 },
                    { 4, "Fresher", "HCM_CPL_NET_04", "Backend CSharp 04", new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(656), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(657), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 1 },
                    { 5, "Fresher", "HCM_CPL_NET_05", "Backend CSharp 05", new DateTime(2024, 4, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(669), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(669), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 1 },
                    { 6, "Intern", "ENUM_01", "Enumerable Programming 01", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(685), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(687), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 2 },
                    { 7, "Intern", "ENUM_02", "Enumerable Programming 02", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(698), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(699), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 2 },
                    { 8, "Intern", "ENUM_03", "Enumerable Programming 03", new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(712), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(712), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 2 },
                    { 9, "Intern", "ENUM_04", "Enumerable Programming 04", new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(723), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(724), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 2 },
                    { 10, "Intern", "RJS_01", "Front End React Js 01", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(735), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(736), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 3 },
                    { 11, "Online fee-Fresher", "RJS_02", "Front End React Js 02", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(747), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(748), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 3 },
                    { 12, "Online fee-Fresher", "RJS_03", "Front End React Js 03", new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(759), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(760), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 3 },
                    { 13, "Online fee-Fresher", "RJS_04", "Front End React Js 04", new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(771), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(772), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 3 },
                    { 14, "Online fee-Fresher", "FST_01", "Full Stack 01", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(788), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(789), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 4 },
                    { 15, "Online fee-Fresher", "FST_02", "Full Stack 02", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(800), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(801), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 4 },
                    { 16, "Offline fee-Fresher", "HCM_CPL_NET_06", "Backend CSharp 06", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(813), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(814), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 1 },
                    { 17, "Offline fee-Fresher", "FST_04", "Full Stack 04", new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(826), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(827), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 4 },
                    { 18, "Offline fee-Fresher", "DEVOPS_01", "DevOps 01", new DateTime(2024, 4, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(839), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(839), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 5 },
                    { 19, "Offline fee-Fresher", "DEVOPS_02", "DevOps 02", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(850), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(851), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 5 },
                    { 20, "Offline fee-Fresher", "DEVOPS_03", "DevOps 03", new DateTime(2024, 4, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(863), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(864), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 5 },
                    { 21, "Offline fee-Fresher", "DBE_01", "Database Engineer 01", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(875), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(876), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 6 },
                    { 22, "Offline fee-Fresher", "DBE_02", "Database Engineer 02", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(887), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(888), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 6 },
                    { 23, "Offline fee-Fresher", "DBE_03", "Database Engineer 03", new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(899), new TimeSpan(0, 7, 0, 0, 0)), 90, new DateTimeOffset(new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "FHM", "Ho Chi Minh", "Super Admin", new DateTimeOffset(new DateTime(2024, 4, 1, 14, 44, 4, 619, DateTimeKind.Unspecified).AddTicks(899), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "Opening", 6 }
                });

            migrationBuilder.InsertData(
                table: "SyllabusObjectives",
                columns: new[] { "SyllabusObjectiveId", "ObjectiveCode", "SyllabusId" },
                values: new object[,]
                {
                    { 1, "LO01", 1 },
                    { 2, "LO02", 1 },
                    { 3, "LO03", 1 },
                    { 4, "K4SD", 2 },
                    { 5, "H4SD", 3 },
                    { 6, "LO02", 4 },
                    { 7, "LO03", 4 }
                });

            migrationBuilder.InsertData(
                table: "SyllabusObjectives",
                columns: new[] { "SyllabusObjectiveId", "ObjectiveCode", "SyllabusId" },
                values: new object[,]
                {
                    { 8, "K4SD", 4 },
                    { 9, "H4SD", 4 },
                    { 10, "LO01", 5 },
                    { 11, "LO04", 6 },
                    { 12, "LO01", 7 },
                    { 13, "K4SD", 8 },
                    { 14, "H4SD", 9 },
                    { 15, "H4SD", 10 }
                });

            migrationBuilder.InsertData(
                table: "TrainingProgramSyllabuses",
                columns: new[] { "Id", "Sequence", "SyllabusId", "TrainingProgramCode" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 11, 1 },
                    { 3, 3, 8, 1 },
                    { 4, 1, 1, 2 },
                    { 5, 2, 11, 2 },
                    { 6, 3, 8, 2 },
                    { 7, 1, 3, 3 },
                    { 8, 2, 10, 3 },
                    { 9, 3, 4, 3 },
                    { 10, 1, 8, 4 },
                    { 11, 2, 10, 4 },
                    { 12, 3, 4, 4 },
                    { 13, 1, 6, 5 },
                    { 14, 2, 11, 5 },
                    { 15, 3, 12, 5 }
                });

            migrationBuilder.InsertData(
                table: "TrainingUnits",
                columns: new[] { "UnitCode", "DayNumber", "SyllabusId", "UnitName" },
                values: new object[,]
                {
                    { 1, 1, 1, "Basic concepts of high-level programming languages" },
                    { 2, 2, 1, "Introduction to object-oriented programming (Inheritance, Encapsulation, Abstraction, Polymorphism)" },
                    { 3, 3, 1, "List, ArrayList, HashTable, and Dictionary" },
                    { 4, 4, 1, "Controlling program errors using try...catch...finally, throw, throws" },
                    { 5, 5, 1, "Concurrency and Multi-Threading in C#" },
                    { 6, 6, 1, "Introduce Common Classes in ADO.NET" },
                    { 7, 7, 1, "Manipulating SQL Data from Window Form Application" },
                    { 8, 8, 1, "Using Controls in Windows Form Application" },
                    { 9, 9, 1, "Hands-On Projects and Exercises" },
                    { 10, 10, 1, "Introduce the .NET with LINQ" },
                    { 11, 11, 1, "Using .NET To Do Basic API CRUD" },
                    { 12, 12, 1, "Practice .NET" },
                    { 13, 2, 1, "Practice with object-oriented programming" },
                    { 14, 1, 2, "Introduction to Algorithms and Data Structures" },
                    { 15, 2, 2, "Advanced Data Structures (Trees, Graphs)" },
                    { 16, 3, 2, "Introduction to Software Testing Techniques" },
                    { 17, 4, 2, "Test Planning and Documentation" },
                    { 18, 5, 2, "Test Case Design" },
                    { 19, 6, 2, "Test Automation Fundamentals" }
                });

            migrationBuilder.InsertData(
                table: "TrainingUnits",
                columns: new[] { "UnitCode", "DayNumber", "SyllabusId", "UnitName" },
                values: new object[,]
                {
                    { 20, 7, 2, "Introduction to Test Automation Frameworks" },
                    { 21, 8, 2, "Load and Performance Testing" },
                    { 22, 9, 2, "Security Testing" },
                    { 23, 10, 2, "Exploratory Testing" },
                    { 24, 11, 2, "Agile Testing Principles and Practices" },
                    { 25, 12, 2, "Continuous Integration and Continuous Testing" },
                    { 26, 13, 2, "Behavior-Driven Development (BDD) and Test-Driven Development (TDD)" },
                    { 27, 14, 2, "Testing in DevOps" },
                    { 28, 15, 2, "User Acceptance Testing (UAT)" },
                    { 29, 16, 2, "Non-functional Testing (Usability, Compatibility, Accessibility)" },
                    { 30, 17, 2, "Testing Tools and Technologies Overview" },
                    { 31, 18, 2, "Introduction to Test Management Tools" },
                    { 32, 19, 2, "Test Metrics and Reporting" },
                    { 33, 20, 2, "Defect Management and Tracking" },
                    { 34, 21, 2, "Test Process Improvement" },
                    { 35, 22, 2, "Ethical and Legal Aspects of Software Testing" },
                    { 36, 23, 2, "Emerging Trends in Software Testing" },
                    { 37, 24, 2, "Capstone Project: Real-world Testing Scenarios" },
                    { 38, 25, 2, "Review and Assessment" },
                    { 39, 1, 3, "Software Quality Assurance Techniques" },
                    { 40, 2, 3, "Software Configuration Management" },
                    { 41, 3, 3, "Software Project Management" },
                    { 42, 4, 3, "Software Maintenance and Evolution" },
                    { 43, 5, 3, "Software Testing Strategies" },
                    { 44, 6, 3, "Software Validation and Verification" },
                    { 45, 7, 3, "Requirements Traceability and Management" },
                    { 46, 8, 3, "Software Metrics and Measurement" },
                    { 47, 9, 3, "Software Risk Management" },
                    { 48, 10, 3, "Software Quality Standards and Models" },
                    { 49, 1, 6, "Performance Evaluation and Optimization" },
                    { 50, 2, 6, "Security and Reliability in Computer Systems" },
                    { 51, 3, 6, "Fault Tolerance and Error Correction" },
                    { 52, 4, 6, "Power Efficiency and Green Computing" },
                    { 53, 5, 6, "Embedded Systems Design" },
                    { 54, 6, 6, "IoT Architecture and Applications" },
                    { 55, 7, 6, "Edge Computing Fundamentals" },
                    { 56, 8, 6, "High-Performance Computing (HPC)" },
                    { 57, 9, 6, "Quantum Computing Concepts" },
                    { 58, 10, 6, "Ethical Considerations in Computer Architecture" },
                    { 59, 1, 11, "Introduction to Algorithms" },
                    { 60, 1, 11, "Data Structures and Algorithms" },
                    { 61, 1, 11, "Programming Fundamentals" }
                });

            migrationBuilder.InsertData(
                table: "TrainingUnits",
                columns: new[] { "UnitCode", "DayNumber", "SyllabusId", "UnitName" },
                values: new object[,]
                {
                    { 62, 1, 11, "Object-Oriented Programming Principles" },
                    { 63, 1, 11, "Software Design and Architecture" },
                    { 64, 1, 11, "Software Development Lifecycle" },
                    { 65, 1, 11, "Introduction to Artificial Intelligence" },
                    { 66, 1, 11, "Machine Learning Fundamentals" },
                    { 67, 1, 11, "Data Science Essentials" },
                    { 68, 1, 11, "Cybersecurity Basics" },
                    { 69, 1, 12, "Fundamentals of Service Management" },
                    { 70, 2, 12, "Service Deployment and Maintenance" },
                    { 71, 3, 12, "Troubleshooting Service Issues" },
                    { 72, 4, 12, "Service Performance Monitoring" },
                    { 73, 5, 12, "Security and Compliance" },
                    { 74, 6, 7, "Advanced Enumerable Methods" },
                    { 75, 7, 7, "Parallel Programming with Enumerable" },
                    { 76, 8, 7, "Error Handling in Enumerable" },
                    { 77, 9, 7, "Optimizing Enumerable Performance" },
                    { 78, 10, 7, "Project: Applying Enumerable Programming Concepts" },
                    { 79, 1, 8, "Backend Systems Architecture" },
                    { 80, 2, 8, "C# Programming for Backend Development" },
                    { 81, 3, 8, "Implementing RESTful APIs with .NET" },
                    { 82, 4, 8, "Working with Databases in C#" },
                    { 83, 5, 8, "Authentication and Authorization in .NET" },
                    { 84, 1, 9, "Understanding Backend Systems Architecture" },
                    { 85, 2, 9, "Introduction to Java Spring Framework" },
                    { 86, 3, 9, "Building RESTful APIs with Spring Boot" },
                    { 87, 4, 9, "Working with Databases in Spring Applications" },
                    { 88, 5, 9, "Implementing Security in Spring" },
                    { 89, 1, 10, "Fundamentals of React.js" },
                    { 90, 2, 10, "Building Dynamic User Interfaces" },
                    { 91, 3, 10, "Managing Application State with React" },
                    { 92, 4, 10, "Routing and Form Handling" },
                    { 93, 5, 10, "Integration with Redux" },
                    { 100, 12, 1, "Authencation And Authorization" }
                });

            migrationBuilder.InsertData(
                table: "CalendarClasses",
                columns: new[] { "Id", "ClassId", "DateAndTimeStudy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(2024, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(2024, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ClassUsers",
                columns: new[] { "ClassId", "UserId", "UserType" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 3, 1, 1 },
                    { 4, 1, 1 },
                    { 1, 8, 1 },
                    { 2, 8, 1 },
                    { 3, 8, 1 },
                    { 4, 8, 1 },
                    { 5, 10, 1 },
                    { 6, 10, 1 },
                    { 7, 10, 1 },
                    { 8, 10, 1 },
                    { 9, 10, 1 },
                    { 5, 12, 1 },
                    { 6, 12, 1 },
                    { 7, 12, 1 },
                    { 8, 12, 1 },
                    { 9, 12, 1 },
                    { 15, 13, 1 },
                    { 16, 13, 1 },
                    { 17, 13, 1 },
                    { 18, 13, 1 },
                    { 10, 14, 1 },
                    { 11, 14, 1 },
                    { 12, 14, 1 },
                    { 13, 14, 1 },
                    { 14, 14, 1 },
                    { 10, 15, 1 },
                    { 11, 15, 1 },
                    { 12, 15, 1 },
                    { 13, 15, 1 },
                    { 14, 15, 1 },
                    { 15, 20, 1 },
                    { 16, 20, 1 },
                    { 17, 20, 1 },
                    { 18, 20, 1 }
                });

            migrationBuilder.InsertData(
                table: "TrainingContents",
                columns: new[] { "TrainingContentId", "ContentName", "DeliveryType", "Duration", "LearningObjectiveCode", "Note", "TrainingFormat", "UnitCode" },
                values: new object[,]
                {
                    { 1, "Learn Better in Winform", 1, 20f, "LO01", null, "Offline", 1 },
                    { 2, "Learn Better in JAVA", 2, 25f, "LO02", null, "Offline", 1 }
                });

            migrationBuilder.InsertData(
                table: "TrainingContents",
                columns: new[] { "TrainingContentId", "ContentName", "DeliveryType", "Duration", "LearningObjectiveCode", "Note", "TrainingFormat", "UnitCode" },
                values: new object[,]
                {
                    { 3, "Practice with object-oriented programming", 3, 15f, "LO02", null, "Offline", 1 },
                    { 4, "Learn Better in C#", 4, 30f, "LO03", null, "Offline", 2 },
                    { 5, "Learn Better in C++", 5, 10f, "LO04", null, "Offline", 2 },
                    { 6, "Introduce Common Classes in ADO.NET", 1, 15f, "LO05", null, "Offline", 3 },
                    { 7, "Manipulating SQL Data from Window Form Application", 2, 20f, "K4SD", null, "Offline", 3 },
                    { 8, "Using Controls in Windows Form Application", 3, 25f, "K4SD", null, "Offline", 4 },
                    { 9, "Hands-On Projects and Exercises", 4, 30f, "H4SD", null, "Offline", 4 },
                    { 10, "Introduce the .NET with LINQ", 5, 35f, "K3BS", null, "Offline", 5 },
                    { 11, "Using .NET To Do Basic API CRUD", 1, 40f, "K4SD", null, "Offline", 5 },
                    { 12, "Authentication And Authorization", 2, 45f, "K3BS", null, "Offline", 6 },
                    { 13, "Practice .NET", 3, 50f, "LO05", null, "Offline", 6 },
                    { 14, "Introduction to Algorithms", 1, 20f, "LO02", null, "Offline", 14 },
                    { 15, "Basic Data Structures", 2, 25f, "LO04", null, "Offline", 14 },
                    { 16, "Trees: Binary, AVL, Red-Black", 1, 20f, "LO04", null, "Offline", 15 },
                    { 17, "Graphs: Directed, Undirected", 2, 25f, "K4SD", null, "Offline", 15 },
                    { 18, "Black Box Testing", 1, 20f, "H4SD", null, "Offline", 16 },
                    { 19, "White Box Testing", 2, 25f, "LO01", null, "Offline", 16 },
                    { 20, "Introduction to Software Development Life Cycle (SDLC)", 1, 20f, "LO01", null, "Offline", 17 },
                    { 21, "Agile Methodologies", 2, 25f, "LO01", null, "Offline", 17 },
                    { 22, "Introduction to Manual Testing", 1, 20f, "LO01", null, "Offline", 18 },
                    { 23, "Test Case Development", 2, 25f, "LO01", null, "Offline", 18 },
                    { 24, "Static Testing Techniques", 1, 20f, "LO01", null, "Offline", 19 },
                    { 25, "Dynamic Testing Techniques", 2, 25f, "LO01", null, "Offline", 19 },
                    { 26, "Test Automation", 1, 20f, "LO01", null, "Offline", 20 },
                    { 27, "Performance Testing", 2, 25f, "LO01", null, "Offline", 20 },
                    { 28, "Security Testing", 1, 20f, "LO01", null, "Offline", 21 },
                    { 29, "Usability Testing", 2, 25f, "LO01", null, "Offline", 21 },
                    { 30, "Regression Testing", 1, 20f, "LO01", null, "Offline", 22 },
                    { 31, "Exploratory Testing", 2, 25f, "LO01", null, "Offline", 22 },
                    { 32, "Integration Testing", 1, 20f, "LO01", null, "Offline", 23 },
                    { 33, "Acceptance Testing", 2, 25f, "LO01", null, "Offline", 23 },
                    { 34, "Testing Tools", 1, 20f, "LO01", null, "Offline", 24 },
                    { 35, "Defect Tracking", 2, 25f, "LO01", null, "Offline", 24 },
                    { 36, "Risk-based Testing", 2, 30f, "LO01", "Must be ", null, 25 },
                    { 37, "Introduction to Quality Assurance", 1, 20f, "K4SD", null, "Offline", 39 },
                    { 38, "Quality Planning and Control", 2, 25f, "LO02", null, "Offline", 39 },
                    { 39, "Configuration Identification and Version Control", 1, 20f, "H4SD", null, "Offline", 40 },
                    { 40, "Configuration Change Management", 2, 25f, "H4SD", null, "Offline", 40 },
                    { 41, "Project Planning and Scheduling", 1, 20f, "H4SD", null, "Offline", 41 },
                    { 42, "Project Execution and Monitoring", 2, 25f, "H4SD", null, "Offline", 41 },
                    { 43, "Requirements Gathering Techniques", 1, 20f, "K4SD", null, "Offline", 42 },
                    { 44, "Stakeholder Analysis", 2, 25f, "K4SD", null, "Offline", 42 }
                });

            migrationBuilder.InsertData(
                table: "TrainingContents",
                columns: new[] { "TrainingContentId", "ContentName", "DeliveryType", "Duration", "LearningObjectiveCode", "Note", "TrainingFormat", "UnitCode" },
                values: new object[,]
                {
                    { 45, "Requirements Modeling", 1, 20f, "K4SD", null, "Offline", 43 },
                    { 46, "Use Case Development", 2, 25f, "K4SD", null, "Offline", 43 },
                    { 47, "Requirements Review", 1, 20f, "K4SD", null, "Offline", 44 },
                    { 48, "Requirements Testing", 2, 25f, "K4SD", null, "Offline", 45 },
                    { 49, "Requirements Traceability", 1, 20f, "K4SD", null, "Offline", 45 },
                    { 50, "Requirements Change Management", 2, 25f, "LO04", null, "Offline", 46 },
                    { 51, "Requirements Management Tools", 1, 20f, "LO04", null, "Offline", 46 },
                    { 52, "Requirements Analysis Tools", 2, 25f, "LO04", null, "Offline", 47 },
                    { 53, "Requirements Engineering Frameworks", 1, 20f, "LO04", null, "Offline", 47 },
                    { 54, "Requirements Quality Attributes", 2, 25f, "LO04", null, "Offline", 48 },
                    { 55, "Managing Requirements Complexity", 1, 20f, "LO04", null, "Offline", 48 },
                    { 56, "Dealing with Changing Requirements", 2, 25f, "LO04", null, "Offline", 48 },
                    { 57, "Introduction to Performance Evaluation", 1, 20f, "K4SD", null, "Offline", 49 },
                    { 58, "Performance Optimization Techniques", 2, 25f, "H4SD", null, "Offline", 49 },
                    { 59, "Understanding Security in Computer Systems", 1, 20f, "K3BS", null, "Offline", 50 },
                    { 60, "Reliability Engineering Principles", 2, 25f, "LO01", null, "Offline", 50 },
                    { 61, "Fault Tolerance Mechanisms", 1, 20f, "LO02", null, "Offline", 51 },
                    { 62, "Error Correction Techniques", 2, 25f, "LO03", null, "Offline", 51 },
                    { 63, "Power Efficiency Metrics", 1, 20f, "LO04", null, "Offline", 52 },
                    { 64, "Green Computing Practices", 2, 25f, "LO05", null, "Offline", 52 },
                    { 65, "Introduction to Embedded Systems", 1, 20f, "H4SD", null, "Offline", 53 },
                    { 66, "Embedded System Design Considerations", 2, 25f, "H4SD", null, "Offline", 53 },
                    { 67, "Fundamentals of IoT Architecture", 1, 20f, "H4SD", null, "Offline", 54 },
                    { 68, "Applications of IoT in Industry", 2, 25f, "H4SD", null, "Offline", 54 },
                    { 69, "Understanding Edge Computing", 1, 20f, "H4SD", null, "Offline", 55 },
                    { 70, "Edge Computing Use Cases", 2, 25f, "H4SD", null, "Offline", 55 },
                    { 71, "Introduction to High-Performance Computing (HPC)", 1, 20f, "H4SD", null, "Offline", 56 },
                    { 72, "Parallel Processing Techniques", 2, 25f, "H4SD", null, "Offline", 56 },
                    { 73, "Fundamentals of Quantum Computing", 1, 20f, "H4SD", null, "Offline", 57 },
                    { 74, "Quantum Computing Applications", 2, 25f, "H4SD", null, "Offline", 57 },
                    { 75, "Ethical Considerations in Computer Architecture", 1, 20f, "H4SD", null, "Offline", 58 },
                    { 76, "Security and Privacy Issues", 2, 25f, "H4SD", null, "Offline", 58 },
                    { 77, "Introduction to Basic Algorithms", 1, 20f, "LO05", null, "Offline", 59 },
                    { 78, "Algorithm Analysis and Complexity", 2, 25f, "LO04", null, "Offline", 59 },
                    { 79, "Arrays and Linked Lists", 1, 20f, "LO04", null, "Offline", 60 },
                    { 80, "Stacks and Queues", 2, 25f, "LO04", null, "Offline", 60 },
                    { 81, "Variables, Data Types, and Operators", 1, 20f, "LO04", null, "Offline", 61 },
                    { 82, "Control Flow and Looping", 2, 25f, "LO04", null, "Offline", 61 },
                    { 83, "Encapsulation and Abstraction", 1, 20f, "LO04", null, "Offline", 62 },
                    { 84, "Inheritance and Polymorphism", 2, 25f, "LO04", null, "Offline", 62 },
                    { 85, "Software Design Principles", 1, 20f, "LO04", null, "Offline", 63 },
                    { 86, "Architectural Patterns", 2, 25f, "LO04", null, "Offline", 63 }
                });

            migrationBuilder.InsertData(
                table: "TrainingContents",
                columns: new[] { "TrainingContentId", "ContentName", "DeliveryType", "Duration", "LearningObjectiveCode", "Note", "TrainingFormat", "UnitCode" },
                values: new object[,]
                {
                    { 87, "Overview of SDLC Models", 1, 20f, "LO04", null, "Offline", 64 },
                    { 88, "Agile Software Development", 2, 25f, "LO04", null, "Offline", 64 },
                    { 89, "Introduction to AI Concepts", 1, 20f, "LO04", null, "Offline", 65 },
                    { 90, "Applications of AI", 2, 25f, "LO04", null, "Offline", 65 },
                    { 91, "Basic Concepts of Machine Learning", 1, 20f, "LO04", null, "Offline", 66 },
                    { 92, "Supervised and Unsupervised Learning", 2, 25f, "LO04", null, "Offline", 66 },
                    { 93, "Introduction to Data Science", 1, 20f, "LO04", null, "Offline", 67 },
                    { 94, "Data Wrangling and Analysis", 2, 25f, "LO04", null, "Offline", 67 },
                    { 95, "Introduction to Service Management", 1, 30f, "LO01", null, "Offline", 69 },
                    { 96, "Basic Concepts of Service Operations", 1, 30f, "LO02", null, "Offline", 69 },
                    { 97, "Service Deployment Strategies", 1, 40f, "LO05", null, "Offline", 70 },
                    { 98, "Service Maintenance Practices", 1, 40f, "LO05", null, "Offline", 70 },
                    { 99, "Identifying Service Issues", 1, 40f, "LO05", null, "Offline", 71 },
                    { 100, "Troubleshooting Techniques", 1, 40f, "LO05", null, "Offline", 71 },
                    { 101, "Monitoring Service Performance Metrics", 1, 40f, "LO05", null, "Offline", 72 },
                    { 102, "Analyzing Performance Data", 1, 40f, "LO05", null, "Offline", 72 },
                    { 103, "Introduction to Service Security", 1, 30f, "LO05", null, "Offline", 73 },
                    { 104, "Compliance Requirements", 1, 30f, "LO05", null, "Offline", 73 },
                    { 105, "Introduction to Enumerable Programming Concepts", 1, 60f, "LO03", null, "Offline", 74 },
                    { 106, "Working with Enumerable Methods in C#", 1, 90f, "LO03", null, "Offline", 75 },
                    { 107, "LINQ Queries for Data Manipulation", 1, 90f, "LO03", null, "Offline", 76 },
                    { 108, "Real-world Applications of Enumerable Programming", 1, 30f, "LO03", null, "Offline", 77 },
                    { 109, "Understanding Backend Systems Architecture", 1, 60f, "LO03", null, "Offline", 79 },
                    { 110, "Introduction to C# Programming", 1, 90f, "LO03", null, "Offline", 80 },
                    { 111, "Building RESTful APIs with .NET Core", 1, 120f, "LO03", null, "Offline", 81 },
                    { 112, "Working with SQL Server in C# Applications", 1, 90f, "LO03", null, "Offline", 82 },
                    { 113, "Implementing Authentication and Authorization in .NET", 1, 120f, "LO03", null, "Offline", 83 },
                    { 114, "Understanding Backend Systems Architecture", 1, 60f, "K4SD", null, "Offline", 84 },
                    { 115, "Introduction to Java Spring Framework", 1, 90f, "K4SD", null, "Offline", 85 },
                    { 116, "Building RESTful APIs with Spring Boot", 1, 120f, "K4SD", null, "Offline", 86 },
                    { 117, "Working with Databases in Spring Applications", 1, 90f, "K4SD", null, "Offline", 87 },
                    { 118, "Implementing Security in Spring", 1, 90f, "K4SD", null, "Offline", 88 },
                    { 119, "Introduction to React.js", 1, 60f, "LO03", null, "Workshop", 89 },
                    { 120, "Creating Dynamic UI Components", 1, 90f, "LO01", null, "Workshop", 90 },
                    { 121, "State Management in React", 1, 60f, "LO04", null, "Workshop", 91 },
                    { 122, "Routing and Form Handling in React", 1, 90f, "LO03", null, "Workshop", 92 },
                    { 123, "Integrating Redux with React Applications", 1, 90f, "K4SD", null, "Workshop", 93 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSchemes_SyllabusId",
                table: "AssessmentSchemes",
                column: "SyllabusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalendarClasses_ClassId",
                table: "CalendarClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassCode",
                table: "Classes",
                column: "ClassCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TrainingProgramCode",
                table: "Classes",
                column: "TrainingProgramCode");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTrainerUnits_ClassId",
                table: "ClassTrainerUnits",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTrainerUnits_TrainerId",
                table: "ClassTrainerUnits",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTrainerUnits_UnitCode",
                table: "ClassTrainerUnits",
                column: "UnitCode");

            migrationBuilder.CreateIndex(
                name: "IX_ClassUsers_ClassId",
                table: "ClassUsers",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ContentId",
                table: "Materials",
                column: "ContentId");

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
                name: "CalendarClasses");

            migrationBuilder.DropTable(
                name: "ClassTrainerUnits");

            migrationBuilder.DropTable(
                name: "ClassUsers");

            migrationBuilder.DropTable(
                name: "FSU");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "SyllabusObjectives");

            migrationBuilder.DropTable(
                name: "TrainingProgramSyllabuses");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "TrainingContents");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropTable(
                name: "LearningObjectives");

            migrationBuilder.DropTable(
                name: "TrainingUnits");

            migrationBuilder.DropTable(
                name: "Syllabuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserPermissions");
        }
    }
}
