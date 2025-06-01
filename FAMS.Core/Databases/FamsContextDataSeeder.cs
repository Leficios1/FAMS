using FAMS.Core.Helpers;
using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;

namespace FAMS.Core.Databases
{
    public static class FamsContextDataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPermission>().HasData(

               new UserPermission
               {
                   PermissionId = "SA",
                   RoleName = "Super Admin",
                   Syllabus = 5,
                   TrainingProgram = 5,
                   Class = 5,
                   LearningMaterial = 5,
                   UserManagement = 5
               },
               new UserPermission
               {
                   PermissionId = "AD",
                   RoleName = "Class Admin",
                   Syllabus = 5,
                   TrainingProgram = 5,
                   Class = 5,
                   LearningMaterial = 5,
                   UserManagement = 1
               },
               new UserPermission
               {
                   PermissionId = "TR",
                   RoleName = "Trainer",
                   Syllabus = 2,
                   TrainingProgram = 2,
                   Class = 2,
                   LearningMaterial = 2,
                   UserManagement = 1
               }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Super Admin",
                    Email = "superadmin@example.com",
                    Phone = "1234567890",
                    Password = "superadmin123",
                    PermissionId = "SA",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 2,
                    Name = "Class Admin",
                    Email = "classadmin@example.com",
                    Phone = "9876543210",
                    Password = "classadmin456",
                    PermissionId = "AD",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 3,
                    Name = "Phan Thanh Nam",
                    Email = "nampt12@fpt.com",
                    Phone = "5551234567",
                    Password = "trainer789",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 4,
                    Name = "Tran Bao Ngoc",
                    Email = "ngoctb30@fpt.com",
                    Phone = "0913248768",
                    Password = "reallystrongpass!",
                    PermissionId = "AD",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Female",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 6,
                    Name = "Trinh Huu Tuan",
                    Email = "tuanhuu3264@fpt.com",
                    Phone = "0905164896",
                    Password = "reallystrongpass!",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 5,
                    Name = "Trinh Son Tung",
                    Email = "sontung@fpt.com",
                    Phone = "0908765123",
                    Password = "reallystrongpass!",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 7,
                    Name = "Trinh Xuan Binh",
                    Email = "xuanbinh@fpt.com",
                    Phone = "0907865123",
                    Password = "reallystrongpass!",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 8,
                    Name = "Trinh Le Khoa",
                    Email = "lekhoa@fpt.com",
                    Phone = "0908765132",
                    Password = "reallystrongpass!",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {

                    Id = 9,
                    Name = "Vo Duy Khoa",
                    Email = "duykhoa@fpt.com",
                    Phone = "0907235423",
                    Password = "reallystrongpass!",
                    PermissionId = "AD",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"

                },
                new User
                {
                    Id = 10,
                    Name = "Vu Hoang Duy Khanh",
                    Email = "duykhanh@fpt.com",
                    Phone = "0907862333",
                    Password = "reallystrongpass!",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 11,
                    Name = "Vo Thi My Tien",
                    Email = "mytien@fpt.com",
                    Phone = "0907232333",
                    Password = "reallystrongpass!",
                    PermissionId = "AD",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Female",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 12,
                    Name = "Mai Thi Minh Thuong",
                    Email = "minhthuong@fpt.com",
                    Phone = "0907832233",
                    Password = "reallystrongpass!",
                    PermissionId = "AD",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Female",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 13,
                    Name = "Tran Truong Van",
                    Email = "truongvan@fpt.com",
                    Phone = "0907843233",
                    Password = "reallystrongpass!",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 14,
                    Name = "Tran Minh Manh",
                    Email = "minhmanh@fpt.com",
                    Phone = "0902312333",
                    Password = "reallystrongpass!",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 15,
                    Name = "Au Phuong Hanh",
                    Email = "auphuonghanh@fpt.com",
                    Phone = "0123456789",
                    Password = "auphuonghanh123",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Female",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 16,
                    Name = "Tiep Phuong Hoa",
                    Email = "tiepphuonghoa@fpt.com",
                    Phone = "0234567890",
                    Password = "tiepphuonghoa456",
                    PermissionId = "AD",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Female",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                 new User
                 {
                     Id = 17,
                     Name = "Bui Quynh Nhu",
                     Email = "buiquynhnhu@fpt.com",
                     Phone = "0345678901",
                     Password = "buiquynhnhu789",
                     PermissionId = "TR",
                     Status = true,
                     DateOfBirth = DateTime.MinValue,
                     Gender = "Female",
                     CreatedBy = "LongV",
                     CreatedDate = DateTimeOffset.Now,
                     AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                 },
                new User
                {
                    Id = 18,
                    Name = "Hinh Thao Nguyen",
                    Email = "hinhthaonguyen@fpt.com",
                    Phone = "0456789012",
                    Password = "hinhthaonguyen123",
                    PermissionId = "AD",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Female",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                 new User
                 {
                     Id = 19,
                     Name = "Bao Thao Van",
                     Email = "baothaovan@fpt.com",
                     Phone = "0567890123",
                     Password = "baothaovan456",
                     PermissionId = "TR",
                     Status = true,
                     DateOfBirth = DateTime.MinValue,
                     Gender = "Female",
                     CreatedBy = "LongV",
                     CreatedDate = DateTimeOffset.Now,
                     AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                 },
                new User
                {
                    Id = 20,
                    Name = "Ông Thiên Thư",
                    Email = "ongthienthu@fpt.com",
                    Phone = "0678901234",
                    Password = "ongthienthu789",
                    PermissionId = "SA",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Female",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                 new User
                 {
                     Id = 21,
                     Name = "Nghiem Thuy Diem",
                     Email = "nghiemthuydiem@fpt.com",
                     Phone = "0789012345",
                     Password = "nghiemthuydiem123",
                     PermissionId = "TR",
                     Status = true,
                     DateOfBirth = DateTime.MinValue,
                     Gender = "Female",
                     CreatedBy = "LongV",
                     CreatedDate = DateTimeOffset.Now,
                     AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                 },
                 new User
                 {
                     Id = 22,
                     Name = "Xung Thuy Vi",
                     Email = "xungthuyvi@fpt.com",
                     Phone = "0890123456",
                     Password = "xungthuyvi456",
                     PermissionId = "AD",
                     Status = true,
                     DateOfBirth = DateTime.MinValue,
                     Gender = "Female",
                     CreatedBy = "LongV",
                     CreatedDate = DateTimeOffset.Now,
                     AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                 },
                 new User
                 {
                     Id = 23,
                     Name = "Ta Truc Chi",
                     Email = "tatrucchi@fpt.com",
                     Phone = "0901234567",
                     Password = "tatrucchi789",
                     PermissionId = "TR",
                     Status = true,
                     DateOfBirth = DateTime.MinValue,
                     Gender = "Female",
                     CreatedBy = "LongV",
                     CreatedDate = DateTimeOffset.Now,
                     AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                 },
                 new User
                 {
                     Id = 24,
                     Name = "Kim Tuyet Chi",
                     Email = "kimtuyetchi@fpt.com",
                     Phone = "0912345678",
                     Password = "kimtuyetchi123",
                     PermissionId = "TR",
                     Status = true,
                     DateOfBirth = DateTime.MinValue,
                     Gender = "Female",
                     CreatedBy = "LongV",
                     CreatedDate = DateTimeOffset.Now,
                     AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                 },

                new User
                {
                    Id = 29,
                    Name = "Do The Phi",
                    Email = "thephi@fpt.com",
                    Phone = "0907452233",
                    Password = "reallystrongpass!",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
                new User
                {
                    Id = 25,
                    Name = "Lo Huu Canh",
                    Email = "lohuucanh@fpt.com",
                    Phone = "0123456789",
                    Password = "lohuucanh123",
                    PermissionId = "TR",
                    Status = true,
                    DateOfBirth = DateTime.MinValue,
                    Gender = "Male",
                    CreatedBy = "LongV",
                    CreatedDate = DateTimeOffset.Now,
                    AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
                },
             new User
             {
                 Id = 26,
                 Name = "Tong An Khang",
                 Email = "tongan khang@fpt.com",
                 Phone = "0123456789",
                 Password = "tongan khang123",
                 PermissionId = "AD",
                 Status = true,
                 DateOfBirth = DateTime.MinValue,
                 Gender = "Male",
                 CreatedBy = "LongV",
                 CreatedDate = DateTimeOffset.Now,
                 AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
             },
             new User
             {
                 Id = 27,
                 Name = "Bui Phu Hai",
                 Email = "buiphuhai@fpt.com",
                 Phone = "0123456789",
                 Password = "buiphuhai123",
                 PermissionId = "TR",
                 Status = true,
                 DateOfBirth = DateTime.MinValue,
                 Gender = "Male",
                 CreatedBy = "LongV",
                 CreatedDate = DateTimeOffset.Now,
                 AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
             },
              new User
              {
                  Id = 28,
                  Name = "Luong Quang Hung",
                  Email = "luongquanghung@fpt.com",
                  Phone = "0123456789",
                  Password = "luongquanghung123",
                  PermissionId = "AD",
                  Status = true,
                  DateOfBirth = DateTime.MinValue,
                  Gender = "Male",
                  CreatedBy = "LongV",
                  CreatedDate = DateTimeOffset.Now,
                  AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg"
              }

            );

            modelBuilder.Entity<LearningObjective>().HasData(
                new LearningObjective
                {
                    ObjectiveCode = "LO01",
                    Description = "Understand the basics of object-oriented programming"
                },
                new LearningObjective
                {
                    ObjectiveCode = "LO02",
                    Description = "Analyze and solve simple programming problems using loops and conditionals"
                },
                new LearningObjective
                {
                    ObjectiveCode = "LO03",
                    Description = "Design and implement algorithms to manipulate data structures",
                },
                new LearningObjective
                {
                    ObjectiveCode = "LO04",
                    Description = "Develop user-friendly graphical user interfaces for applications",
                },
                new LearningObjective
                {
                    ObjectiveCode = "LO05",
                    Description = "Apply software testing techniques to ensure code quality and reliability"
                },
                new LearningObjective
                {
                    ObjectiveCode = "K4SD",
                    Description = "Explore libraries, frameworks, and tools commonly used in software development with a focus on C#, such as ASP.NET Core or Entity Framework."
                },
                new LearningObjective
                {
                    ObjectiveCode = "H4SD",
                    Description = "Understand the basics of functional programming paradigms in C#, including immutability, higher-order functions, and lambda expressions."
                },
                new LearningObjective
                {
                    ObjectiveCode = "K3BS",
                    Description = "Explore advanced topics like concurrency, threading, and parallel programming in C#, and understand how to manage concurrent execution."
                }
            );

            modelBuilder.Entity<DeliveryType>().HasData(
                new DeliveryType
                {
                    Id = 1,
                    TypeName = "Assignment/Lab"
                },
                new DeliveryType
                {
                    Id = 2,
                    TypeName = "Concept/Lecture"
                },
                new DeliveryType
                {
                    Id = 3,
                    TypeName = "Guide/Review"
                },
                new DeliveryType
                {
                    Id = 4,
                    TypeName = "Test/Quiz"
                },
                new DeliveryType
                {
                    Id = 5,
                    TypeName = "Exam"
                },
                new DeliveryType
                {
                    Id = 6,
                    TypeName = "Seminar/Workshop"
                }
            );

            modelBuilder.Entity<Syllabus>().HasData(
                new Syllabus
                {
                    Id = 1,
                    SyllabusCode = "SC01",
                    SyllabusName = "Basic Cross-Platform Application Programming With .NET",
                    Version = "1",
                    AttendeeNumber = 35,
                    Priority = "Intership",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 1,
                    CreatedBy = "Xung Thuy Vi",
                    CourseObjective = "<div>\r\n    <p>This topic is to introduce C# programming language knowledge; adapt trainees with skills, lessons, and practices specifically used in Fsoft projects. In detail, after completing the topic, trainees will:</p>\r\n    <ul>\r\n        <li>Understand basic concepts of high-level programming languages (keyword, statement, operator, control-of-flow)</li>\r\n        <li>Understand and distinguish two concepts: class (Class) and object (Object)</li>\r\n        <li>Understand and apply object-oriented programming knowledge to resolve simple problems (Inheritance, Encapsulation, Abstraction, Polymorphism)</li>\r\n        <li>Work with some of the existing data structures in C# (List, ArrayList, HashTable, Dictionary)</li>\r\n        <li>Know how to control program errors (use try ... catch..finally, throw, throws)</li>\r\n        <li>Be able to work with concurrency and multi-threading in C#</li>\r\n        <li>Be able to work with common classes in ADO.net: SqlConnection, SqlCommand, SqlParameter, SqlDataAdapter, SqlDataReader</li>\r\n        <li>Be able to manipulate SQL data from Window Form Application via 4 basic commands: Add, Update, Delete, Select</li>\r\n        <li>Know how to design UI screen in Window Form Application</li>\r\n        <li>Know how to use appropriate controls for each field/data type: Textbox, Label, Combobox, Radio, DateTimePicker, NumericUpDown, RichTextBox</li>\r\n    </ul>\r\n</div>"
                  ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n    <p>Trainees' PCs need to have the following software installed & run without any issues:</p>\r\n    <ul>\r\n        <li>Microsoft SQL Server 2005 Express</li>\r\n        <li>Microsoft Visual Studio 2017</li>\r\n        <li>Microsoft Office 2007 (Visio, Word, PowerPoint)</li>\r\n    </ul>\r\n</div>"
                  ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>Since there is no specific information provided for Training Principles in the syllabus, here are some generic principles:</p>\r\n    <ul>\r\n        <li>Provide a conducive learning environment that fosters active participation and engagement.</li>\r\n        <li>Encourage collaborative learning through group activities, discussions, and peer interactions.</li>\r\n        <li>Emphasize hands-on practical exercises to reinforce theoretical concepts.</li>\r\n        <li>Ensure clear communication of learning objectives, expectations, and assessment criteria.</li>\r\n        <li>Provide constructive feedback and guidance to facilitate continuous improvement.</li>\r\n        <li>Foster a culture of respect, inclusivity, and professionalism among participants.</li>\r\n        <li>Encourage self-directed learning and exploration to deepen understanding and knowledge retention.</li>\r\n        <li>Adapt teaching methods and materials to cater to diverse learning styles and preferences.</li>\r\n        <li>Continuously evaluate and adjust training strategies based on feedback and performance analysis.</li>\r\n    </ul>"
                },
                new Syllabus
                {
                    Id = 2,
                    SyllabusCode = "SC02",
                    SyllabusName = "Software Testing",
                    Version = "1",
                    AttendeeNumber = 35,
                    Priority = "Fresher",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 1,
                    CreatedBy = "Xung Thuy Vi",
                    CourseObjective = "<div>\r\n    <p>This topic is to introduce C# programming language knowledge; adapt trainees with skills, lessons, and practices specifically used in Fsoft projects. In detail, after completing the topic, trainees will:</p>\r\n    <ul>\r\n        <li>Understand basic concepts of high-level programming languages (keyword, statement, operator, control-of-flow)</li>\r\n        <li>Understand and distinguish two concepts: class (Class) and object (Object)</li>\r\n        <li>Understand and apply object-oriented programming knowledge to resolve simple problems (Inheritance, Encapsulation, Abstraction, Polymorphism)</li>\r\n        <li>Work with some of the existing data structures in C# (List, ArrayList, HashTable, Dictionary)</li>\r\n        <li>Know how to control program errors (use try ... catch..finally, throw, throws)</li>\r\n        <li>Be able to work with concurrency and multi-threading in C#</li>\r\n        <li>Be able to work with common classes in ADO.net: SqlConnection, SqlCommand, SqlParameter, SqlDataAdapter, SqlDataReader</li>\r\n        <li>Be able to manipulate SQL data from Window Form Application via 4 basic commands: Add, Update, Delete, Select</li>\r\n        <li>Know how to design UI screen in Window Form Application</li>\r\n        <li>Know how to use appropriate controls for each field/data type: Textbox, Label, Combobox, Radio, DateTimePicker, NumericUpDown, RichTextBox</li>\r\n    </ul>\r\n</div>"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n    <p>Trainees' PCs need to have the following software installed & run without any issues:</p>\r\n    <ul>\r\n        <li>Microsoft SQL Server 2005 Express</li>\r\n        <li>Microsoft Visual Studio 2017</li>\r\n        <li>Microsoft Office 2007 (Visio, Word, PowerPoint)</li>\r\n    </ul>\r\n</div>"
                  ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>No specific information provided for Training Principles in the syllabus.</p>\r\n</div>"
                },
                new Syllabus
                {
                    Id = 3,
                    SyllabusCode = "SC03",
                    SyllabusName = "Software Requirements",
                    Version = "1.1",
                    AttendeeNumber = 35,
                    Priority = "Fresher",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 1,
                    CreatedBy = "Xung Thuy Vi",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Software Requirements\" syllabus is to provide students with a comprehensive understanding of software requirements engineering principles and practices. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the importance of gathering, analyzing, and documenting software requirements.</li>\r\n        <li>Learn various techniques for eliciting requirements from stakeholders.</li>\r\n        <li>Be able to create clear, concise, and unambiguous software requirement specifications.</li>\r\n        <li>Gain practical experience in identifying, prioritizing, and managing software requirements throughout the development lifecycle.</li>\r\n        <li>Develop skills in validating and verifying software requirements to ensure they meet stakeholders' needs and expectations.</li>\r\n        <li>Understand the role of software requirements in the overall software development process and its impact on project success.</li>\r\n    </ul>\r\n</div>"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                  ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Software Requirements\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Real-World Application: Emphasize practical, hands-on exercises and case studies to reinforce theoretical concepts and demonstrate their application in real-world scenarios.</li>\r\n        <li>Continuous Feedback: Provide regular feedback to students on their progress and performance to facilitate continuous improvement and adjustment of learning strategies.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can work together, share insights, and learn from each other's experiences.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach software requirements engineering as a problem-solving activity, focusing on identifying and addressing stakeholder needs and challenges.</li>\r\n        <li>Ethical Considerations: Highlight the importance of ethical behavior and professional conduct in software requirements engineering, emphasizing integrity, honesty, and respect for stakeholders' interests.</li>\r\n    </ul>\r\n</div>"
                },
                new Syllabus
                {
                    Id = 4,
                    SyllabusCode = "SC04",
                    SyllabusName = "Softwate Development Project",
                    Version = "1.2",
                    AttendeeNumber = 35,
                    Priority = "All Level",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 1,
                    CreatedBy = "Trinh Huu Tuan",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Software Development Project\" syllabus is to equip students with the necessary knowledge and skills to successfully manage and execute software development projects. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the key principles and methodologies of software project management.</li>\r\n        <li>Learn how to effectively plan, execute, and monitor software development projects.</li>\r\n        <li>Gain practical experience in defining project scope, objectives, and deliverables.</li>\r\n        <li>Develop skills in team management, communication, and collaboration within project teams.</li>\r\n        <li>Learn how to identify and mitigate risks associated with software development projects.</li>\r\n        <li>Understand the importance of quality assurance and testing in ensuring project success.</li>\r\n        <li>Learn how to manage project resources, budgets, and timelines effectively.</li>\r\n        <li>Gain insights into the various stages of the software development lifecycle and their impact on project management.</li>\r\n        <li>Be able to apply project management tools and techniques to real-world software development scenarios.</li>\r\n    </ul>\r\n</div>"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                  ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Software Development Project\" syllabus include:</p>\r\n    <ul>\r\n        <li>Emphasize Agile Methodologies: Prioritize Agile principles and practices to adapt to changing project requirements and deliver high-quality software incrementally.</li>\r\n        <li>Effective Communication: Encourage open and transparent communication among project team members, stakeholders, and clients to ensure shared understanding and alignment of project goals.</li>\r\n        <li>Continuous Learning: Foster a culture of continuous learning and improvement within the project team, encouraging individuals to acquire new skills and stay updated with industry trends and best practices.</li>\r\n        <li>Collaborative Problem Solving: Promote collaborative problem-solving approaches where team members work together to identify and address project challenges, leveraging diverse perspectives and expertise.</li>\r\n        <li>Iterative Development: Embrace iterative development processes to rapidly prototype and refine software solutions based on feedback from stakeholders, minimizing the risk of costly errors and delays.</li>\r\n        <li>Quality Focus: Prioritize software quality throughout the development lifecycle, emphasizing the importance of testing, code reviews, and quality assurance practices to deliver robust and reliable software products.</li>\r\n        <li>Adaptability: Encourage adaptability and resilience in the face of changing project requirements, market conditions, and technological advancements, enabling teams to respond effectively to evolving project dynamics.</li>\r\n        <li>Stakeholder Engagement: Engage stakeholders actively throughout the project lifecycle, soliciting their input, addressing their concerns, and ensuring their satisfaction with project deliverables.</li>\r\n        <li>Project Transparency: Maintain transparency in project management processes, providing stakeholders with visibility into project progress, risks, and issues, fostering trust and accountability.</li>\r\n    </ul>\r\n</div>"
                },
                new Syllabus
                {
                    Id = 5,
                    SyllabusCode = "SC04",
                    SyllabusName = "Softwate Development Project",
                    Version = "1.1",
                    AttendeeNumber = 35,
                    Priority = "All Level",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 0,
                    CreatedBy = "",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Software Development Project\" syllabus is to equip participants with the knowledge, skills, and tools necessary to effectively manage software development projects. Upon completion of the course, participants will:</p>\r\n    <ul>\r\n        <li>Understand the key principles and best practices of software project management.</li>\r\n        <li>Learn how to plan, execute, and monitor software development projects from initiation to closure.</li>\r\n        <li>Gain practical experience in defining project scope, objectives, and deliverables.</li>\r\n        <li>Develop skills in team management, communication, and collaboration within project teams.</li>\r\n        <li>Learn how to identify and mitigate risks associated with software development projects.</li>\r\n        <li>Understand the importance of quality assurance and testing in ensuring project success.</li>\r\n        <li>Learn how to manage project resources, budgets, and timelines effectively.</li>\r\n        <li>Gain insights into different project management methodologies and their applicability to various project scenarios.</li>\r\n    </ul>\r\n</div>"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                  ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Software Development Project\" syllabus include:</p>\r\n    <ul>\r\n        <li>Emphasize Agile Methodologies: Prioritize Agile principles and practices to adapt to changing project requirements and deliver high-quality software incrementally.</li>\r\n        <li>Effective Communication: Encourage open and transparent communication among project team members, stakeholders, and clients to ensure shared understanding and alignment of project goals.</li>\r\n        <li>Continuous Learning: Foster a culture of continuous learning and improvement within the project team, encouraging individuals to acquire new skills and stay updated with industry trends and best practices.</li>\r\n        <li>Collaborative Problem Solving: Promote collaborative problem-solving approaches where team members work together to identify and address project challenges, leveraging diverse perspectives and expertise.</li>\r\n        <li>Iterative Development: Embrace iterative development processes to rapidly prototype and refine software solutions based on feedback from stakeholders, minimizing the risk of costly errors and delays.</li>\r\n        <li>Quality Focus: Prioritize software quality throughout the development lifecycle, emphasizing the importance of testing, code reviews, and quality assurance practices to deliver robust and reliable software products.</li>\r\n        <li>Adaptability: Encourage adaptability and resilience in the face of changing project requirements, market conditions, and technological advancements, enabling teams to respond effectively to evolving project dynamics.</li>\r\n        <li>Stakeholder Engagement: Engage stakeholders actively throughout the project lifecycle, soliciting their input, addressing their concerns, and ensuring their satisfaction with project deliverables.</li>\r\n        <li>Project Transparency: Maintain transparency in project management processes, providing stakeholders with visibility into project progress, risks, and issues, fostering trust and accountability.</li>\r\n    </ul>\r\n</div>\r\n",
                },
                new Syllabus
                {
                    Id = 6,
                    SyllabusCode = "CE01",
                    SyllabusName = "Computer Architecture",
                    Version = "1.3",
                    AttendeeNumber = 35,
                    Priority = "All Level",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 0,
                    CreatedBy = "",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Computer Architecture\" syllabus is to provide participants with a comprehensive understanding of computer hardware and system architecture. Upon completion of the course, participants will:</p>\r\n    <ul>\r\n        <li>Gain a solid understanding of fundamental computer architecture principles and concepts.</li>\r\n        <li>Learn about different CPU architectures, instruction sets, and memory hierarchies.</li>\r\n        <li>Understand the role of operating systems in managing hardware resources and executing programs.</li>\r\n        <li>Explore advanced topics in computer architecture, such as parallel processing, pipelining, and cache optimization.</li>\r\n        <li>Develop skills in designing and optimizing computer systems for performance, reliability, and power efficiency.</li>\r\n        <li>Gain insights into emerging trends and technologies in computer architecture, such as cloud computing, IoT, and edge computing.</li>\r\n        <li>Apply theoretical knowledge to practical hands-on exercises and projects to reinforce learning.</li>\r\n    </ul>\r\n</div>"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                  ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Computer Architecture\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Engage participants through interactive lectures, discussions, and hands-on activities to promote active learning and retention of knowledge.</li>\r\n        <li>Real-World Application: Emphasize practical applications of computer architecture principles through lab exercises, case studies, and project work to enhance practical skills and problem-solving abilities.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where participants can share insights, collaborate on projects, and learn from each other's experiences.</li>\r\n        <li>Continuous Feedback: Provide regular feedback to participants on their progress and performance to facilitate continuous improvement and adjustment of learning strategies.</li>\r\n        <li>Flexibility: Adapt the course delivery and content to accommodate participants with diverse backgrounds, learning styles, and skill levels.</li>\r\n        <li>Ethical Considerations: Emphasize ethical considerations in computer architecture design and implementation, including issues related to security, privacy, and intellectual property.</li>\r\n        <li>Lifelong Learning: Encourage participants to continue learning and staying updated with advancements in computer architecture beyond the course duration, fostering a culture of lifelong learning.</li>\r\n    </ul>\r\n</div>",
                },
                new Syllabus
                {
                    Id = 11,
                    SyllabusCode = "CS23",
                    SyllabusName = "Computer Science",
                    Version = "1.1",
                    AttendeeNumber = 35,
                    Priority = "All Level",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 0,
                    CreatedBy = "",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Computer Science\" syllabus is to provide participants with a comprehensive understanding of computer science fundamentals and principles. Upon completion of the course, participants will:</p>\r\n    <ul>\r\n        <li>Gain a solid understanding of core concepts in computer science, including algorithms, data structures, and problem-solving techniques.</li>\r\n        <li>Learn programming languages commonly used in computer science, such as Python, Java, or C++.</li>\r\n        <li>Understand the principles of software development, including software design, testing, and debugging.</li>\r\n        <li>Explore advanced topics in computer science, such as artificial intelligence, machine learning, and data science.</li>\r\n        <li>Develop practical programming skills through hands-on exercises, projects, and coding challenges.</li>\r\n        <li>Apply theoretical knowledge to real-world problems and scenarios, fostering critical thinking and analytical skills.</li>\r\n        <li>Prepare for further studies or careers in various fields of computer science, including software engineering, cybersecurity, and computer systems.</li>\r\n    </ul>\r\n</div>\r\n"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                    ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Computer Science\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Engage participants through interactive lectures, group discussions, and problem-solving activities to promote active learning and participation.</li>\r\n        <li>Hands-On Experience: Provide ample opportunities for participants to apply theoretical concepts through practical coding exercises, projects, and labs.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where participants can work together, share ideas, and learn from each other's experiences.</li>\r\n        <li>Continuous Feedback: Offer regular feedback and assessments to participants to monitor their progress and provide guidance for improvement.</li>\r\n        <li>Adaptability: Tailor the course content and delivery methods to accommodate participants with diverse backgrounds, learning styles, and skill levels.</li>\r\n        <li>Ethical Considerations: Emphasize ethical behavior and responsible use of technology in all aspects of computer science education and practice.</li>\r\n        <li>Lifelong Learning: Encourage participants to cultivate a passion for lifelong learning and stay updated with advancements in the field of computer science.</li>\r\n    </ul>\r\n</div>",
                },

                new Syllabus
                {
                    Id = 12,
                    SyllabusCode = "OS21",
                    SyllabusName = "Operation Service",
                    Version = "1.1",
                    AttendeeNumber = 35,
                    Priority = "All Level",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 0,
                    CreatedBy = "",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Operation Service\" syllabus is to equip participants with the necessary knowledge and skills to efficiently manage and operate various services. Upon completion of the course, participants will:</p>\r\n    <ul>\r\n        <li>Understand the fundamentals of service management and operation.</li>\r\n        <li>Learn best practices for service deployment, configuration, and maintenance.</li>\r\n        <li>Gain proficiency in managing service availability, reliability, and performance.</li>\r\n        <li>Explore techniques for troubleshooting common service-related issues and incidents.</li>\r\n        <li>Develop skills in monitoring and optimizing service performance and resource utilization.</li>\r\n        <li>Understand the importance of security and compliance in service management.</li>\r\n        <li>Apply industry-standard tools and methodologies for effective service management and operation.</li>\r\n    </ul>\r\n</div>"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                  ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Operation Service\" syllabus include:</p>\r\n    <ul>\r\n        <li>Hands-On Learning: Provide hands-on experience with real-world scenarios and practical exercises to reinforce theoretical concepts.</li>\r\n        <li>Collaborative Environment: Foster collaboration and teamwork among participants through group activities and discussions.</li>\r\n        <li>Continuous Improvement: Encourage continuous learning and improvement through feedback, reflection, and iteration.</li>\r\n        <li>Adaptability: Tailor the training approach to accommodate participants with varying skill levels and learning preferences.</li>\r\n        <li>Problem-Solving Skills: Develop participants' problem-solving skills by presenting them with challenging scenarios and encouraging them to find solutions.</li>\r\n        <li>Professional Development: Support participants in enhancing their professional skills and competencies relevant to service management and operation.</li>\r\n        <li>Ethical Considerations: Emphasize ethical practices and responsible decision-making in service management and operation.</li>\r\n    </ul>\r\n</div>",
                },
                new Syllabus
                {
                    Id = 7,
                    SyllabusCode = "EP04",
                    SyllabusName = "Enumarable Programming",
                    Version = "1.1",
                    AttendeeNumber = 35,
                    Priority = "Intern",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 0,
                    CreatedBy = "",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Enumarable Programming\" syllabus is to provide students with a comprehensive understanding of enumerable programming concepts and techniques. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the fundamental concepts of enumerable programming.</li>\r\n        <li>Be proficient in using enumerable methods and LINQ queries to manipulate data collections.</li>\r\n        <li>Learn how to efficiently work with sequences and iterators in C#.</li>\r\n        <li>Gain practical experience in applying enumerable programming techniques to solve real-world problems.</li>\r\n        <li>Develop skills in writing clean, readable, and maintainable code using enumerable programming paradigms.</li>\r\n        <li>Understand the benefits and limitations of enumerable programming compared to traditional imperative programming.</li>\r\n    </ul>\r\n</div>"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                  ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Enumarable Programming\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Hands-on Practice: Provide opportunities for students to practice enumerable programming concepts through coding exercises and projects.</li>\r\n        <li>Feedback and Reflection: Offer timely feedback on students' progress and encourage reflection on their learning experiences to promote continuous improvement.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can work together, share insights, and learn from each other's experiences.</li>\r\n        <li>Real-world Application: Emphasize the practical application of enumerable programming techniques in real-world scenarios to reinforce learning and skill development.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach programming challenges using a problem-solving mindset and apply enumerable programming techniques to find solutions.</li>\r\n    </ul>\r\n</div>\r\n",
                },
                new Syllabus
                {
                    Id = 8,
                    SyllabusCode = "CN01",
                    SyllabusName = "Backend End With CSharp .NET",
                    Version = "1.1",
                    AttendeeNumber = 35,
                    Priority = "Intern",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 0,
                    CreatedBy = "",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Backend End With CSharp .NET\" syllabus is to provide students with a comprehensive understanding of backend development using C# and .NET framework. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the architecture and components of backend systems.</li>\r\n        <li>Be proficient in developing server-side applications using C# programming language.</li>\r\n        <li>Learn how to implement RESTful APIs and handle HTTP requests and responses.</li>\r\n        <li>Gain practical experience in working with databases, including SQL Server, for data storage and retrieval.</li>\r\n        <li>Develop skills in authentication and authorization mechanisms for securing backend services.</li>\r\n        <li>Understand best practices for error handling, logging, and performance optimization in backend development.</li>\r\n    </ul>\r\n</div>"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                  ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Backend End With CSharp .NET\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Hands-on Practice: Provide opportunities for students to work on practical projects and assignments to reinforce theoretical concepts.</li>\r\n        <li>Feedback and Reflection: Offer constructive feedback on students' work and encourage reflection on their learning progress to promote continuous improvement.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can collaborate on projects, share knowledge, and learn from each other's experiences.</li>\r\n        <li>Real-world Application: Emphasize the practical application of backend development principles in real-world scenarios to prepare students for industry requirements.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach programming challenges with a problem-solving mindset and develop creative solutions using backend technologies.</li>\r\n    </ul>\r\n</div>",
                },
                new Syllabus
                {
                    Id = 9,
                    SyllabusCode = "JS01",
                    SyllabusName = "Backend End With Java Spring",
                    Version = "1.1",
                    AttendeeNumber = 35,
                    Priority = "All Level",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 0,
                    CreatedBy = "",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Backend End With Java Spring\" syllabus is to provide students with a comprehensive understanding of backend development using the Java Spring framework. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the architecture and components of backend systems.</li>\r\n        <li>Be proficient in developing server-side applications using Java Spring framework.</li>\r\n        <li>Learn how to implement RESTful APIs and handle HTTP requests and responses.</li>\r\n        <li>Gain practical experience in working with databases, including SQL Server, for data storage and retrieval.</li>\r\n        <li>Develop skills in authentication and authorization mechanisms for securing backend services.</li>\r\n        <li>Understand best practices for error handling, logging, and performance optimization in backend development.</li>\r\n    </ul>\r\n</div>\r\n"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                    ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Backend End With Java Spring\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Hands-on Practice: Provide opportunities for students to work on practical projects and assignments to reinforce theoretical concepts.</li>\r\n        <li>Feedback and Reflection: Offer constructive feedback on students' work and encourage reflection on their learning progress to promote continuous improvement.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can collaborate on projects, share knowledge, and learn from each other's experiences.</li>\r\n        <li>Real-world Application: Emphasize the practical application of backend development principles in real-world scenarios to prepare students for industry requirements.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach programming challenges with a problem-solving mindset and develop creative solutions using backend technologies.</li>\r\n    </ul>\r\n</div>",
                },
                new Syllabus
                {
                    Id = 10,
                    SyllabusCode = "RJ04",
                    SyllabusName = "Front End With React Js",
                    Version = "1.1",
                    AttendeeNumber = 35,
                    Priority = "All Level",
                    CreatedDate = DateTimeOffset.Now,
                    UserId = 1,
                    PublishStatus = 0,
                    CreatedBy = "",
                    CourseObjective = "<div>\r\n    <p>The course objective for the \"Front End With React Js\" syllabus is to provide students with a comprehensive understanding of frontend development using the React.js library. Upon completion of the course, students will:</p>\r\n    <ul>\r\n        <li>Understand the fundamentals of React.js, including components, props, state, and lifecycle methods.</li>\r\n        <li>Be proficient in building dynamic user interfaces and single-page applications (SPAs) using React.js.</li>\r\n        <li>Learn how to manage application state using React's built-in state management and context API.</li>\r\n        <li>Gain practical experience in routing, form handling, and integrating third-party libraries with React.js.</li>\r\n        <li>Develop skills in using React.js in combination with other frontend technologies such as Redux for state management.</li>\r\n        <li>Understand best practices for performance optimization, code organization, and debugging in React.js development.</li>\r\n    </ul>\r\n</div>"
                    ,
                    ModifiedBy = "Xung Thuy Vi",
                    ModifiedDate = DateTimeOffset.Now,
                    TechnicalRequirement = "<div>\r\n        Trainees PCs need to have the following software installed &amp; run\r\n        without any issues:\r\n      </div>\r\n      <div>• Microsoft SQL Server 2005 Express</div>\r\n      <div>• Microsoft Visual Studio 2017</div>\r\n      <div>• Microsoft Office 2007 (Visio, Word, PowerPoint)</div>\r\n    </div>"
                    ,
                    TrainingMaterials = "No Information",
                    TrainingPrinciples = "<div>\r\n    <p>The training principles for the \"Front End With React Js\" syllabus include:</p>\r\n    <ul>\r\n        <li>Active Learning: Encourage active participation, discussion, and engagement among students to enhance learning and retention.</li>\r\n        <li>Hands-on Practice: Provide opportunities for students to work on practical projects and assignments to reinforce theoretical concepts.</li>\r\n        <li>Feedback and Reflection: Offer constructive feedback on students' work and encourage reflection on their learning progress to promote continuous improvement.</li>\r\n        <li>Collaborative Learning: Foster a collaborative learning environment where students can collaborate on projects, share knowledge, and learn from each other's experiences.</li>\r\n        <li>Real-world Application: Emphasize the practical application of frontend development principles in real-world scenarios to prepare students for industry requirements.</li>\r\n        <li>Problem-Solving Approach: Encourage students to approach programming challenges with a problem-solving mindset and develop creative solutions using React.js and associated technologies.</li>\r\n    </ul>\r\n</div>",
                }

            );

            modelBuilder.Entity<TrainingUnit>().HasData(
                 new TrainingUnit
                 {
                     UnitCode = 1,
                     UnitName = "Basic concepts of high-level programming languages",
                     DayNumber = 1,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 2,
                     UnitName = "Introduction to object-oriented programming (Inheritance, Encapsulation, Abstraction, Polymorphism)",
                     DayNumber = 2,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 13,
                     UnitName = "Practice with object-oriented programming",
                     DayNumber = 2,
                     SyllabusId = 1,
                 }
                 ,
                 new TrainingUnit
                 {
                     UnitCode = 3,
                     UnitName = "List, ArrayList, HashTable, and Dictionary",
                     DayNumber = 3,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 4,
                     UnitName = "Controlling program errors using try...catch...finally, throw, throws",
                     DayNumber = 4,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 5,
                     UnitName = "Concurrency and Multi-Threading in C#",
                     DayNumber = 5,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 6,
                     UnitName = "Introduce Common Classes in ADO.NET",
                     DayNumber = 6,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 7,
                     UnitName = "Manipulating SQL Data from Window Form Application",
                     DayNumber = 7,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 8,
                     UnitName = "Using Controls in Windows Form Application",
                     DayNumber = 8,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 9,
                     UnitName = "Hands-On Projects and Exercises",
                     DayNumber = 9,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 10,
                     UnitName = "Introduce the .NET with LINQ",
                     DayNumber = 10,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 11,
                     UnitName = "Using .NET To Do Basic API CRUD",
                     DayNumber = 11,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 100,
                     UnitName = "Authencation And Authorization",
                     DayNumber = 12,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 12,
                     UnitName = "Practice .NET",
                     DayNumber = 12,
                     SyllabusId = 1,
                 },
                 new TrainingUnit
                 {
                     UnitCode = 14,
                     UnitName = "Introduction to Algorithms and Data Structures",
                     DayNumber = 1,
                     SyllabusId = 2
                 },
                new TrainingUnit
                {
                    UnitCode = 15,
                    UnitName = "Advanced Data Structures (Trees, Graphs)",
                    DayNumber = 2,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 16,
                    UnitName = "Introduction to Software Testing Techniques",
                    DayNumber = 3,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 17,
                    UnitName = "Test Planning and Documentation",
                    DayNumber = 4,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 18,
                    UnitName = "Test Case Design",
                    DayNumber = 5,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 19,
                    UnitName = "Test Automation Fundamentals",
                    DayNumber = 6,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 20,
                    UnitName = "Introduction to Test Automation Frameworks",
                    DayNumber = 7,
                    SyllabusId = 2
                },
               new TrainingUnit
               {
                   UnitCode = 21,
                   UnitName = "Load and Performance Testing",
                   DayNumber = 8,
                   SyllabusId = 2
               },
                new TrainingUnit
                {
                    UnitCode = 22,
                    UnitName = "Security Testing",
                    DayNumber = 9,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 23,
                    UnitName = "Exploratory Testing",
                    DayNumber = 10,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 24,
                    UnitName = "Agile Testing Principles and Practices",
                    DayNumber = 11,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 25,
                    UnitName = "Continuous Integration and Continuous Testing",
                    DayNumber = 12,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 26,
                    UnitName = "Behavior-Driven Development (BDD) and Test-Driven Development (TDD)",
                    DayNumber = 13,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 27,
                    UnitName = "Testing in DevOps",
                    DayNumber = 14,
                    SyllabusId = 2
                },
                new TrainingUnit
                {
                    UnitCode = 28,
                    UnitName = "User Acceptance Testing (UAT)",
                    DayNumber = 15,
                    SyllabusId = 2
                },
// Unit 49
new TrainingUnit
{
    UnitCode = 49,
    UnitName = "Performance Evaluation and Optimization",
    DayNumber = 1,
    SyllabusId = 6,
},

// Unit 50
new TrainingUnit
{
    UnitCode = 50,
    UnitName = "Security and Reliability in Computer Systems",
    DayNumber = 2,
    SyllabusId = 6,
},

// Unit 51
new TrainingUnit
{
    UnitCode = 51,
    UnitName = "Fault Tolerance and Error Correction",
    DayNumber = 3,
    SyllabusId = 6,
},

// Unit 52
new TrainingUnit
{
    UnitCode = 52,
    UnitName = "Power Efficiency and Green Computing",
    DayNumber = 4,
    SyllabusId = 6,
},

// Unit 53
new TrainingUnit
{
    UnitCode = 53,
    UnitName = "Embedded Systems Design",
    DayNumber = 5,
    SyllabusId = 6,
},

// Unit 54
new TrainingUnit
{
    UnitCode = 54,
    UnitName = "IoT Architecture and Applications",
    DayNumber = 6,
    SyllabusId = 6,
},

// Unit 55
new TrainingUnit
{
    UnitCode = 55,
    UnitName = "Edge Computing Fundamentals",
    DayNumber = 7,
    SyllabusId = 6,
},

// Unit 56
new TrainingUnit
{
    UnitCode = 56,
    UnitName = "High-Performance Computing (HPC)",
    DayNumber = 8,
    SyllabusId = 6,
},

// Unit 57
new TrainingUnit
{
    UnitCode = 57,
    UnitName = "Quantum Computing Concepts",
    DayNumber = 9,
    SyllabusId = 6,
},

// Unit 58
new TrainingUnit
{
    UnitCode = 58,
    UnitName = "Ethical Considerations in Computer Architecture",
    DayNumber = 10,
    SyllabusId = 6,
},

new TrainingUnit
{
    UnitCode = 29,
    UnitName = "Non-functional Testing (Usability, Compatibility, Accessibility)",
    DayNumber = 16,
    SyllabusId = 2
},
new TrainingUnit
{
    UnitCode = 30,
    UnitName = "Testing Tools and Technologies Overview",
    DayNumber = 17,
    SyllabusId = 2
},
new TrainingUnit
{
    UnitCode = 31,
    UnitName = "Introduction to Test Management Tools",
    DayNumber = 18,
    SyllabusId = 2
},
new TrainingUnit
{
    UnitCode = 32,
    UnitName = "Test Metrics and Reporting",
    DayNumber = 19,
    SyllabusId = 2
},
new TrainingUnit
{
    UnitCode = 33,
    UnitName = "Defect Management and Tracking",
    DayNumber = 20,
    SyllabusId = 2
},
new TrainingUnit
{
    UnitCode = 34,
    UnitName = "Test Process Improvement",
    DayNumber = 21,
    SyllabusId = 2
},
new TrainingUnit
{
    UnitCode = 35,
    UnitName = "Ethical and Legal Aspects of Software Testing",
    DayNumber = 22,
    SyllabusId = 2
},
new TrainingUnit
{
    UnitCode = 36,
    UnitName = "Emerging Trends in Software Testing",
    DayNumber = 23,
    SyllabusId = 2
},
new TrainingUnit
{
    UnitCode = 37,
    UnitName = "Capstone Project: Real-world Testing Scenarios",
    DayNumber = 24,
    SyllabusId = 2
},
                new TrainingUnit
                {
                    UnitCode = 38,
                    UnitName = "Review and Assessment",
                    DayNumber = 25,
                    SyllabusId = 2
                },
                 new TrainingUnit
                 {
                     UnitCode = 39,
                     UnitName = "Software Quality Assurance Techniques",
                     DayNumber = 1,
                     SyllabusId = 3,
                 },

// Unit 40
new TrainingUnit
{
    UnitCode = 40,
    UnitName = "Software Configuration Management",
    DayNumber = 2,
    SyllabusId = 3,
},

// Unit 41
new TrainingUnit
{
    UnitCode = 41,
    UnitName = "Software Project Management",
    DayNumber = 3,
    SyllabusId = 3,
},

// Unit 42
new TrainingUnit
{
    UnitCode = 42,
    UnitName = "Software Maintenance and Evolution",
    DayNumber = 4,
    SyllabusId = 3,
},

// Unit 43
new TrainingUnit
{
    UnitCode = 43,
    UnitName = "Software Testing Strategies",
    DayNumber = 5,
    SyllabusId = 3,
},

// Unit 44
new TrainingUnit
{
    UnitCode = 44,
    UnitName = "Software Validation and Verification",
    DayNumber = 6,
    SyllabusId = 3,
},

// Unit 45
new TrainingUnit
{
    UnitCode = 45,
    UnitName = "Requirements Traceability and Management",
    DayNumber = 7,
    SyllabusId = 3,
},

// Unit 46
new TrainingUnit
{
    UnitCode = 46,
    UnitName = "Software Metrics and Measurement",
    DayNumber = 8,
    SyllabusId = 3,
},

// Unit 47
new TrainingUnit
{
    UnitCode = 47,
    UnitName = "Software Risk Management",
    DayNumber = 9,
    SyllabusId = 3,
},

// Unit 48
new TrainingUnit
{
    UnitCode = 48,
    UnitName = "Software Quality Standards and Models",
    DayNumber = 10,
    SyllabusId = 3,
},
// Unit 59
new TrainingUnit
{
    UnitCode = 59,
    UnitName = "Introduction to Algorithms",
    DayNumber = 1,
    SyllabusId = 11,
},
// Unit 60
new TrainingUnit
{
    UnitCode = 60,
    UnitName = "Data Structures and Algorithms",
    DayNumber = 1,
    SyllabusId = 11,
},
// Unit 61
new TrainingUnit
{
    UnitCode = 61,
    UnitName = "Programming Fundamentals",
    DayNumber = 1,
    SyllabusId = 11,
},
// Unit 62
new TrainingUnit
{
    UnitCode = 62,
    UnitName = "Object-Oriented Programming Principles",
    DayNumber = 1,
    SyllabusId = 11,
},
// Unit 63
new TrainingUnit
{
    UnitCode = 63,
    UnitName = "Software Design and Architecture",
    DayNumber = 1,
    SyllabusId = 11,
},
// Unit 64
new TrainingUnit
{
    UnitCode = 64,
    UnitName = "Software Development Lifecycle",
    DayNumber = 1,
    SyllabusId = 11,
},
// Unit 65
new TrainingUnit
{
    UnitCode = 65,
    UnitName = "Introduction to Artificial Intelligence",
    DayNumber = 1,
    SyllabusId = 11,
},
// Unit 66
new TrainingUnit
{
    UnitCode = 66,
    UnitName = "Machine Learning Fundamentals",
    DayNumber = 1,
    SyllabusId = 11,
},
// Unit 67
new TrainingUnit
{
    UnitCode = 67,
    UnitName = "Data Science Essentials",
    DayNumber = 1,
    SyllabusId = 11,
},
// Unit 68
new TrainingUnit
{
    UnitCode = 68,
    UnitName = "Cybersecurity Basics",
    DayNumber = 1,
    SyllabusId = 11,

},
// Unit 69
new TrainingUnit
{
    UnitCode = 69,
    UnitName = "Fundamentals of Service Management",
    DayNumber = 1,
    SyllabusId = 12,
},
// Unit 70
new TrainingUnit
{
    UnitCode = 70,
    UnitName = "Service Deployment and Maintenance",
    DayNumber = 2,
    SyllabusId = 12,
},
// Unit 71
new TrainingUnit
{
    UnitCode = 71,
    UnitName = "Troubleshooting Service Issues",
    DayNumber = 3,
    SyllabusId = 12,
},
// Unit 72
new TrainingUnit
{
    UnitCode = 72,
    UnitName = "Service Performance Monitoring",
    DayNumber = 4,
    SyllabusId = 12,
},
// Unit 73
new TrainingUnit
{
    UnitCode = 73,
    UnitName = "Security and Compliance",
    DayNumber = 5,
    SyllabusId = 12,
},
// Unit 74
new TrainingUnit
{
    UnitCode = 74,
    UnitName = "Advanced Enumerable Methods",
    DayNumber = 6,
    SyllabusId = 7,
},

// Unit 75
new TrainingUnit
{
    UnitCode = 75,
    UnitName = "Parallel Programming with Enumerable",
    DayNumber = 7,
    SyllabusId = 7,
},

// Unit 76
new TrainingUnit
{
    UnitCode = 76,
    UnitName = "Error Handling in Enumerable",
    DayNumber = 8,
    SyllabusId = 7,
},

// Unit 77
new TrainingUnit
{
    UnitCode = 77,
    UnitName = "Optimizing Enumerable Performance",
    DayNumber = 9,
    SyllabusId = 7,
},

// Unit 78
new TrainingUnit
{
    UnitCode = 78,
    UnitName = "Project: Applying Enumerable Programming Concepts",
    DayNumber = 10,
    SyllabusId = 7,
},
// Unit 79
new TrainingUnit
{
    UnitCode = 79,
    UnitName = "Backend Systems Architecture",
    DayNumber = 1,
    SyllabusId = 8,
},

// Unit 80
new TrainingUnit
{
    UnitCode = 80,
    UnitName = "C# Programming for Backend Development",
    DayNumber = 2,
    SyllabusId = 8,
},

// Unit 81
new TrainingUnit
{
    UnitCode = 81,
    UnitName = "Implementing RESTful APIs with .NET",
    DayNumber = 3,
    SyllabusId = 8,
},

// Unit 82
new TrainingUnit
{
    UnitCode = 82,
    UnitName = "Working with Databases in C#",
    DayNumber = 4,
    SyllabusId = 8,
},

// Unit 83
new TrainingUnit
{
    UnitCode = 83,
    UnitName = "Authentication and Authorization in .NET",
    DayNumber = 5,
    SyllabusId = 8,
},
// Unit 84
new TrainingUnit
{
    UnitCode = 84,
    UnitName = "Understanding Backend Systems Architecture",
    DayNumber = 1,
    SyllabusId = 9,
},

// Unit 85
new TrainingUnit
{
    UnitCode = 85,
    UnitName = "Introduction to Java Spring Framework",
    DayNumber = 2,
    SyllabusId = 9,
},

// Unit 86
new TrainingUnit
{
    UnitCode = 86,
    UnitName = "Building RESTful APIs with Spring Boot",
    DayNumber = 3,
    SyllabusId = 9,
},

// Unit 87
new TrainingUnit
{
    UnitCode = 87,
    UnitName = "Working with Databases in Spring Applications",
    DayNumber = 4,
    SyllabusId = 9,
},

// Unit 88
new TrainingUnit
{
    UnitCode = 88,
    UnitName = "Implementing Security in Spring",
    DayNumber = 5,
    SyllabusId = 9,
},
// Training Units for Syllabus ID 10 (Unit code starts from 89)
// Unit 89
new TrainingUnit
{
    UnitCode = 89,
    UnitName = "Fundamentals of React.js",
    DayNumber = 1,
    SyllabusId = 10,
},
// Unit 90
new TrainingUnit
{
    UnitCode = 90,
    UnitName = "Building Dynamic User Interfaces",
    DayNumber = 2,
    SyllabusId = 10,
},
// Unit 91
new TrainingUnit
{
    UnitCode = 91,
    UnitName = "Managing Application State with React",
    DayNumber = 3,
    SyllabusId = 10,
},
// Unit 92
new TrainingUnit
{
    UnitCode = 92,
    UnitName = "Routing and Form Handling",
    DayNumber = 4,
    SyllabusId = 10,
},
// Unit 93
new TrainingUnit
{
    UnitCode = 93,
    UnitName = "Integration with Redux",
    DayNumber = 5,
    SyllabusId = 10,
}

                );

            var TrainingContent = new List<TrainingContent>() {
                new TrainingContent()
                {
                    Id=1,
                    LearningObjectiveCode = "LO01",
                    UnitCode = 1,
                    ContentName = "Learn Better in Winform",
                    DeliveryType = 1,
                    Duration = 20,
                    TrainingFormat = "Offline"
                },

            new TrainingContent()
            {
                Id=2,
                LearningObjectiveCode = "LO02",
                UnitCode = 1,
                ContentName = "Learn Better in JAVA",
                DeliveryType = 2,
                Duration = 25,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {
                Id=3,
                LearningObjectiveCode = "LO02",
                UnitCode = 1,
                ContentName = "Practice with object-oriented programming",
                DeliveryType = 3,
                Duration = 15,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "LO03",
                UnitCode = 2,
                ContentName = "Learn Better in C#",
                DeliveryType = 4,
                Duration = 30,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "LO04",
                UnitCode = 2,
                ContentName = "Learn Better in C++",
                DeliveryType = 5,
                Duration = 10,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "LO05",
                UnitCode = 3,
                ContentName = "Introduce Common Classes in ADO.NET",
                DeliveryType = 1,
                Duration = 15,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "K4SD",
                UnitCode = 3,
                ContentName = "Manipulating SQL Data from Window Form Application",
                DeliveryType = 2,
                Duration = 20,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "K4SD",
                UnitCode = 4,
                ContentName = "Using Controls in Windows Form Application",
                DeliveryType = 3,
                Duration = 25,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "H4SD",
                UnitCode = 4,
                ContentName = "Hands-On Projects and Exercises",
                DeliveryType = 4,
                Duration = 30,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "K3BS",
                UnitCode = 5,
                ContentName = "Introduce the .NET with LINQ",
                DeliveryType = 5,
                Duration = 35,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "K4SD",
                UnitCode = 5,
                ContentName = "Using .NET To Do Basic API CRUD",
                DeliveryType = 1,
                Duration = 40,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "K3BS",
                UnitCode = 6,
                ContentName = "Authentication And Authorization",
                DeliveryType = 2,
                Duration = 45,
                TrainingFormat = "Offline"
            },

            new TrainingContent()
            {

                LearningObjectiveCode = "LO05",
                UnitCode = 6,
                ContentName = "Practice .NET",
                DeliveryType = 3,
                Duration = 50,
                TrainingFormat = "Offline"
            },
            new TrainingContent
            {

                LearningObjectiveCode = "LO02",
                UnitCode = 14,
                ContentName = "Introduction to Algorithms",
                DeliveryType = 1,
                Duration = 20,
                TrainingFormat = "Offline"
            },
new TrainingContent
{

    LearningObjectiveCode = "LO04",
    UnitCode = 14,
    ContentName = "Basic Data Structures",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 15
new TrainingContent
{

    LearningObjectiveCode = "LO04",
    UnitCode = 15,
    ContentName = "Trees: Binary, AVL, Red-Black",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "K4SD",
    UnitCode = 15,
    ContentName = "Graphs: Directed, Undirected",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 16
new TrainingContent
{

    LearningObjectiveCode = "H4SD",
    UnitCode = 16,
    ContentName = "Black Box Testing",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 16,
    ContentName = "White Box Testing",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 17,
    ContentName = "Introduction to Software Development Life Cycle (SDLC)",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 17,
    ContentName = "Agile Methodologies",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 18
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 18,
    ContentName = "Introduction to Manual Testing",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 18,
    ContentName = "Test Case Development",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 19
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 19,
    ContentName = "Static Testing Techniques",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 19,
    ContentName = "Dynamic Testing Techniques",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 20
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 20,
    ContentName = "Test Automation",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 20,
    ContentName = "Performance Testing",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 21
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 21,
    ContentName = "Security Testing",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 21,
    ContentName = "Usability Testing",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 22
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 22,
    ContentName = "Regression Testing",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 22,
    ContentName = "Exploratory Testing",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 23
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 23,
    ContentName = "Integration Testing",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 23,
    ContentName = "Acceptance Testing",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 24
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 24,
    ContentName = "Testing Tools",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 24,
    ContentName = "Defect Tracking",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 25
new TrainingContent
{

    LearningObjectiveCode = "LO01",
    UnitCode = 25,
    ContentName = "Risk-based Testing",
    DeliveryType = 2,
    Duration = 30,
    Note = "Must be "
},
new TrainingContent
{
    UnitCode = 39,
    LearningObjectiveCode = "K4SD",
    ContentName = "Introduction to Quality Assurance",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},

        new TrainingContent
        {
            UnitCode = 39,
            LearningObjectiveCode = "LO02",
            ContentName = "Quality Planning and Control",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
         new TrainingContent
         {
             UnitCode = 40,
             LearningObjectiveCode = "H4SD",
             ContentName = "Configuration Identification and Version Control",
             DeliveryType = 1,
             Duration = 20,
             TrainingFormat = "Offline"
         },
        new TrainingContent
        {
            UnitCode = 40,
            LearningObjectiveCode = "H4SD",
            ContentName = "Configuration Change Management",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 41,
            LearningObjectiveCode = "H4SD",
            ContentName = "Project Planning and Scheduling",
            DeliveryType = 1,
            Duration = 20,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 41,
            LearningObjectiveCode = "H4SD",
            ContentName = "Project Execution and Monitoring",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 42,
            LearningObjectiveCode = "K4SD",
            ContentName = "Requirements Gathering Techniques",
            DeliveryType = 1,
            Duration = 20,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 42,
            LearningObjectiveCode = "K4SD",
            ContentName = "Stakeholder Analysis",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
         new TrainingContent
         {
             UnitCode = 43,
             LearningObjectiveCode = "K4SD",
             ContentName = "Requirements Modeling",
             DeliveryType = 1,
             Duration = 20,
             TrainingFormat = "Offline"
         },
        new TrainingContent
        {
            UnitCode = 43,
            LearningObjectiveCode = "K4SD",
            ContentName = "Use Case Development",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 44,
            LearningObjectiveCode = "K4SD",
            ContentName = "Requirements Review",
            DeliveryType = 1,
            Duration = 20,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 45,
            LearningObjectiveCode = "K4SD",
            ContentName = "Requirements Testing",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 45,
            LearningObjectiveCode = "K4SD",
            ContentName = "Requirements Traceability",
            DeliveryType = 1,
            Duration = 20,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 46,
            LearningObjectiveCode = "LO04",
            ContentName = "Requirements Change Management",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
         new TrainingContent
         {
             UnitCode = 46,
             LearningObjectiveCode = "LO04",
             ContentName = "Requirements Management Tools",
             DeliveryType = 1,
             Duration = 20,
             TrainingFormat = "Offline"
         },
        new TrainingContent
        {
            UnitCode = 47,
            LearningObjectiveCode = "LO04",
            ContentName = "Requirements Analysis Tools",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
         new TrainingContent
         {
             UnitCode = 47,
             LearningObjectiveCode = "LO04",
             ContentName = "Requirements Engineering Frameworks",
             DeliveryType = 1,
             Duration = 20,
             TrainingFormat = "Offline"
         },
        new TrainingContent
        {
            UnitCode = 48,
            LearningObjectiveCode = "LO04",
            ContentName = "Requirements Quality Attributes",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 48,
            LearningObjectiveCode = "LO04",
            ContentName = "Managing Requirements Complexity",
            DeliveryType = 1,
            Duration = 20,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            UnitCode = 48,
            LearningObjectiveCode = "LO04",
            ContentName = "Dealing with Changing Requirements",
            DeliveryType = 2,
            Duration = 25,
            TrainingFormat = "Offline"
        },
        new TrainingContent
        {
            LearningObjectiveCode = "K4SD",
            UnitCode = 49,
            ContentName = "Introduction to Performance Evaluation",
            DeliveryType = 1,
            Duration = 20,
            TrainingFormat = "Offline"
        },
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 49,
    ContentName = "Performance Optimization Techniques",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 50 Contents
new TrainingContent
{
    LearningObjectiveCode = "K3BS",
    UnitCode = 50,
    ContentName = "Understanding Security in Computer Systems",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO01",
    UnitCode = 50,
    ContentName = "Reliability Engineering Principles",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 51 Contents
new TrainingContent
{
    LearningObjectiveCode = "LO02",
    UnitCode = 51,
    ContentName = "Fault Tolerance Mechanisms",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 51,
    ContentName = "Error Correction Techniques",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 52 Contents
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 52,
    ContentName = "Power Efficiency Metrics",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 52,
    ContentName = "Green Computing Practices",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 53 Contents
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 53,
    ContentName = "Introduction to Embedded Systems",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 53,
    ContentName = "Embedded System Design Considerations",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 54 Contents
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 54,
    ContentName = "Fundamentals of IoT Architecture",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 54,
    ContentName = "Applications of IoT in Industry",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 55 Contents
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 55,
    ContentName = "Understanding Edge Computing",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 55,
    ContentName = "Edge Computing Use Cases",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 56 Contents
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 56,
    ContentName = "Introduction to High-Performance Computing (HPC)",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 56,
    ContentName = "Parallel Processing Techniques",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 57 Contents
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 57,
    ContentName = "Fundamentals of Quantum Computing",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 57,
    ContentName = "Quantum Computing Applications",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Unit 58 Contents
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 58,
    ContentName = "Ethical Considerations in Computer Architecture",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "H4SD",
    UnitCode = 58,
    ContentName = "Security and Privacy Issues",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 59,
    ContentName = "Introduction to Basic Algorithms",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 59,
    ContentName = "Algorithm Analysis and Complexity",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Contents for Unit 60
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 60,
    ContentName = "Arrays and Linked Lists",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 60,
    ContentName = "Stacks and Queues",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Contents for Unit 61
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 61,
    ContentName = "Variables, Data Types, and Operators",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 61,
    ContentName = "Control Flow and Looping",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Contents for Unit 62
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 62,
    ContentName = "Encapsulation and Abstraction",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 62,
    ContentName = "Inheritance and Polymorphism",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Contents for Unit 63
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 63,
    ContentName = "Software Design Principles",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 63,
    ContentName = "Architectural Patterns",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Contents for Unit 64
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 64,
    ContentName = "Overview of SDLC Models",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 64,
    ContentName = "Agile Software Development",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Contents for Unit 65
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 65,
    ContentName = "Introduction to AI Concepts",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 65,
    ContentName = "Applications of AI",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Contents for Unit 66
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 66,
    ContentName = "Basic Concepts of Machine Learning",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 66,
    ContentName = "Supervised and Unsupervised Learning",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},

// Contents for Unit 67
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 67,
    ContentName = "Introduction to Data Science",
    DeliveryType = 1,
    Duration = 20,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 67,
    ContentName = "Data Wrangling and Analysis",
    DeliveryType = 2,
    Duration = 25,
    TrainingFormat = "Offline"
},
// Content for Unit 69
new TrainingContent
{
    LearningObjectiveCode = "LO01",
    UnitCode = 69,
    ContentName = "Introduction to Service Management",
    DeliveryType = 1,
    Duration = 30,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO02",
    UnitCode = 69,
    ContentName = "Basic Concepts of Service Operations",
    DeliveryType = 1,
    Duration = 30,
    TrainingFormat = "Offline"
},

// Content for Unit 70
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 70,
    ContentName = "Service Deployment Strategies",
    DeliveryType = 1,
    Duration = 40,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 70,
    ContentName = "Service Maintenance Practices",
    DeliveryType = 1,
    Duration = 40,
    TrainingFormat = "Offline"
},

// Content for Unit 71
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 71,
    ContentName = "Identifying Service Issues",
    DeliveryType = 1,
    Duration = 40,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 71,
    ContentName = "Troubleshooting Techniques",
    DeliveryType = 1,
    Duration = 40,
    TrainingFormat = "Offline"
},

// Content for Unit 72
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 72,
    ContentName = "Monitoring Service Performance Metrics",
    DeliveryType = 1,
    Duration = 40,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 72,
    ContentName = "Analyzing Performance Data",
    DeliveryType = 1,
    Duration = 40,
    TrainingFormat = "Offline"
},

// Content for Unit 73
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 73,
    ContentName = "Introduction to Service Security",
    DeliveryType = 1,
    Duration = 30,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO05",
    UnitCode = 73,
    ContentName = "Compliance Requirements",
    DeliveryType = 1,
    Duration = 30,
    TrainingFormat = "Offline"
},
// Content for Unit 74
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 74,
    ContentName = "Introduction to Enumerable Programming Concepts",
    DeliveryType = 1,
    Duration = 60,
    TrainingFormat = "Offline"
},

// Content for Unit 75
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 75,
    ContentName = "Working with Enumerable Methods in C#",
    DeliveryType = 1,
    Duration = 90,
    TrainingFormat = "Offline"
},

// Content for Unit 76
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 76,
    ContentName = "LINQ Queries for Data Manipulation",
    DeliveryType = 1,
    Duration = 90,
    TrainingFormat = "Offline"
},

// Content for Unit 77
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 77,
    ContentName = "Real-world Applications of Enumerable Programming",
    DeliveryType = 1,
    Duration = 30,
    TrainingFormat = "Offline"
},
// Unit 79
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 79,
    ContentName = "Understanding Backend Systems Architecture",
    DeliveryType = 1,
    Duration = 60,
    TrainingFormat = "Offline",
},

// Unit 80
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 80,
    ContentName = "Introduction to C# Programming",
    DeliveryType = 1,
    Duration = 90,
    TrainingFormat = "Offline",
},

// Unit 81
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 81,
    ContentName = "Building RESTful APIs with .NET Core",
    DeliveryType = 1,
    Duration = 120,
    TrainingFormat = "Offline",
},

// Unit 82
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 82,
    ContentName = "Working with SQL Server in C# Applications",
    DeliveryType = 1,
    Duration = 90,
    TrainingFormat = "Offline",
},

// Unit 83
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 83,
    ContentName = "Implementing Authentication and Authorization in .NET",
    DeliveryType = 1,
    Duration = 120,
    TrainingFormat = "Offline",
},
// Content for Unit 84
new TrainingContent
{
    LearningObjectiveCode = "K4SD",
    UnitCode = 84,
    ContentName = "Understanding Backend Systems Architecture",
    DeliveryType = 1,
    Duration = 60,
    TrainingFormat = "Offline"
},

// Content for Unit 85
new TrainingContent
{
    LearningObjectiveCode = "K4SD",
    UnitCode = 85,
    ContentName = "Introduction to Java Spring Framework",
    DeliveryType = 1,
    Duration = 90,
    TrainingFormat = "Offline"
},

// Content for Unit 86
new TrainingContent
{
    LearningObjectiveCode = "K4SD",
    UnitCode = 86,
    ContentName = "Building RESTful APIs with Spring Boot",
    DeliveryType = 1,
    Duration = 120,
    TrainingFormat = "Offline"
},

// Content for Unit 87
new TrainingContent
{
    LearningObjectiveCode = "K4SD",
    UnitCode = 87,
    ContentName = "Working with Databases in Spring Applications",
    DeliveryType = 1,
    Duration = 90,
    TrainingFormat = "Offline"
},

// Content for Unit 88
new TrainingContent
{
    LearningObjectiveCode = "K4SD",
    UnitCode = 88,
    ContentName = "Implementing Security in Spring",
    DeliveryType = 1,
    Duration = 90,
    TrainingFormat = "Offline"
},
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 89,
    ContentName = "Introduction to React.js",
    DeliveryType = 1, // Offline
    Duration = 60,
    TrainingFormat = "Workshop",
},
// Unit 90
new TrainingContent
{
    LearningObjectiveCode = "LO01",
    UnitCode = 90,
    ContentName = "Creating Dynamic UI Components",
    DeliveryType = 1, // Offline
    Duration = 90,
    TrainingFormat = "Workshop",
},
// Unit 91
new TrainingContent
{
    LearningObjectiveCode = "LO04",
    UnitCode = 91,
    ContentName = "State Management in React",
    DeliveryType = 1, // Offline
    Duration = 60,
    TrainingFormat = "Workshop",
},
// Unit 92
new TrainingContent
{
    LearningObjectiveCode = "LO03",
    UnitCode = 92,
    ContentName = "Routing and Form Handling in React",
    DeliveryType = 1, // Offline
    Duration = 90,
    TrainingFormat = "Workshop",
},
// Unit 93
new TrainingContent
{
    LearningObjectiveCode = "K4SD",
    UnitCode = 93,
    ContentName = "Integrating Redux with React Applications",
    DeliveryType = 1, // Offline
    Duration = 90,
    TrainingFormat = "Workshop",
}
};
            var i = 1;
            foreach (var content in TrainingContent)
            {
                content.Id = i;
                i++;
                modelBuilder.Entity<TrainingContent>().HasData(content);
            }

            modelBuilder.Entity<SyllabusObjective>().HasData(
                new SyllabusObjective
                {
                    Id=1,
                    SyllabusId = 1,
                    ObjectiveCode = "LO01"
                },
                new SyllabusObjective
                {
                    Id = 2,
                    SyllabusId = 1,
                    ObjectiveCode = "LO02"
                },
                new SyllabusObjective
                {
                    Id = 3,
                    SyllabusId = 1,
                    ObjectiveCode = "LO03"
                },
                new SyllabusObjective
                {
                    Id = 4,
                    SyllabusId = 2,
                    ObjectiveCode = "K4SD"
                },
                new SyllabusObjective
                {
                    Id =5,

                    SyllabusId = 3,
                    ObjectiveCode = "H4SD"
                },
                new SyllabusObjective
                {
                    Id = 6,

                    SyllabusId = 4,
                    ObjectiveCode = "LO02"
                },
                new SyllabusObjective
                {
                    Id = 7,
                    SyllabusId = 4,
                    ObjectiveCode = "LO03"
                },
                new SyllabusObjective
                {
                    Id = 8,
                    SyllabusId = 4,
                    ObjectiveCode = "K4SD"
                },
                new SyllabusObjective
                {
                    Id = 9,
                    SyllabusId = 4,
                    ObjectiveCode = "H4SD"
                },
                new SyllabusObjective
                {
                    Id = 10,
                    SyllabusId = 5,
                    ObjectiveCode = "LO01"
                },
                new SyllabusObjective
                {
                    Id = 11,
                    SyllabusId = 6,
                    ObjectiveCode = "LO04"
                },
                new SyllabusObjective
                {
                    Id = 12,
                    SyllabusId = 7,
                    ObjectiveCode = "LO01"
                },
                new SyllabusObjective
                {
                    Id = 13,
                    SyllabusId = 8,
                    ObjectiveCode = "K4SD"
                },
                new SyllabusObjective
                {
                    Id = 14,
                    SyllabusId = 9,
                    ObjectiveCode = "H4SD"
                },
                new SyllabusObjective
                {
                    Id = 15,
                    SyllabusId = 10,
                    ObjectiveCode = "H4SD"
                }
            );

            modelBuilder.Entity<TrainingProgram>().HasData(
                new TrainingProgram
                {
                    TrainingProgramCode = 1,
                    Name = "Backend Programming C#",
                    UserId = 1,
                    StartTime = DateTime.Now,
                    Duration = 40,
                    Status = 1,
                    CreatedBy = "Trinh Huu Tuan",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Trinh Huu Tuan",
                    TopicCode = ".NET"
                },
                new TrainingProgram
                {
                    TrainingProgramCode = 2,
                    Name = "Enumarable Programming",
                    UserId = 3,
                    StartTime = DateTime.Now,
                    Duration = 40,
                    Status = 1,
                    CreatedBy = "Trinh Son Tung",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Trinh Son Tung",
                    TopicCode = "CENUM"
                },
                new TrainingProgram
                {
                    TrainingProgramCode = 3,
                    Name = "Front End React Js",
                    UserId = 2,
                    StartTime = DateTime.Now,
                    Duration = 20,
                    Status = 1,
                    CreatedBy = "Tran Truong Van",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Tran Truong Van",
                    TopicCode = ".RJS"
                },
                 new TrainingProgram
                 {
                     TrainingProgramCode = 4,
                     Name = "Full Stack",
                     UserId = 2,
                     StartTime = DateTime.Now,
                     Duration = 40,
                     Status = 1,
                     CreatedBy = "Tran Truong Van",
                     CreatedDate = DateTime.Now,
                     ModifiedDate = DateTime.Now,
                     ModifiedBy = "Tran Truong Van",
                     TopicCode = ".FST"
                 },
                 new TrainingProgram
                 {
                     TrainingProgramCode = 5,
                     Name = "DevOps",
                     UserId = 2,
                     StartTime = DateTime.Now,
                     Duration = 40,
                     Status = 1,
                     CreatedBy = "Tran Truong Van",
                     CreatedDate = DateTime.Now,
                     ModifiedDate = DateTime.Now,
                     ModifiedBy = "Tran Truong Van",
                     TopicCode = ".FST"
                 },
                 new TrainingProgram
                 {
                     TrainingProgramCode = 6,
                     Name = "Database Engineer",
                     UserId = 2,
                     StartTime = DateTime.Now,
                     Duration = 40,
                     Status = 1,
                     CreatedBy = "Tran Truong Van",
                     CreatedDate = DateTime.Now,
                     ModifiedDate = DateTime.Now,
                     ModifiedBy = "Tran Truong Van",
                     TopicCode = ".FST"
                 }
            );
            modelBuilder.Entity<Class>().HasData(
                new Class
                {
                    Id = 1,
                    TrainingProgramCode = 1,
                    ClassName = "Backend CSharp 01",
                    ClassCode = "HCM_CPL_NET_01",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-08-08"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart  = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee="Fresher"



                }, new Class
                {
                    Id = 2,
                    TrainingProgramCode = 1,
                    ClassName = "Backend CSharp 02",
                    ClassCode = "HCM_CPL_NET_02",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-08-08"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee = "Fresher"



                },
                new Class
                {
                    Id =3,
                    TrainingProgramCode = 1,
                    ClassName = "Backend CSharp 03",
                    ClassCode = "HCM_CPL_NET_03",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-08-08"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("07:00:00"),
                    ClassTimeEnd = DateTime.Parse("09:00:00"),
                    Attendee = "Fresher"



                },
                new Class
                {
                    Id = 4,
                    TrainingProgramCode = 1,
                    ClassName = "Backend CSharp 04",
                    ClassCode = "HCM_CPL_NET_04",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-05-05"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("07:00:00"),
                    ClassTimeEnd = DateTime.Parse("09:00:00"),
                    Attendee = "Fresher"



                },
                new Class
                {
                    Id = 5,
                    TrainingProgramCode = 1,
                    ClassName = "Backend CSharp 05",
                    ClassCode = "HCM_CPL_NET_05",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-05-05"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("13:00:00"),
                    ClassTimeEnd = DateTime.Parse("15:00:00"),
                    Attendee = "Fresher"



                },
                new Class
                {
                    Id=6,
                    TrainingProgramCode = 2,
                    ClassName = "Enumerable Programming 01",
                    ClassCode = "ENUM_01",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-05-05"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee="Intern"



                },
                new Class
                {

                    Id = 7,
                    TrainingProgramCode = 2,
                    ClassName = "Enumerable Programming 02",
                    ClassCode = "ENUM_02",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-05-05"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee = "Intern"



                },
                new Class
                {
                    Id = 8,
                    TrainingProgramCode = 2,
                    ClassName = "Enumerable Programming 03",
                    ClassCode = "ENUM_03",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-07-07"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("07:00:00"),
                    ClassTimeEnd = DateTime.Parse("09:00:00"),
                    Attendee = "Intern"



                },
                new Class
                {
                    Id = 9,
                    TrainingProgramCode = 2,
                    ClassName = "Enumerable Programming 04",
                    ClassCode = "ENUM_04",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-07-07"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("07:00:00"),
                    ClassTimeEnd = DateTime.Parse("09:00:00"),
                    Attendee = "Intern"



                },
                new Class
                {
                    Id = 10,
                    TrainingProgramCode = 3,
                    ClassName = "Front End React Js 01",
                    ClassCode = "RJS_01",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee = "Intern"



                },
                new Class
                {
                    Id = 11,
                    TrainingProgramCode = 3,
                    ClassName = "Front End React Js 02",
                    ClassCode = "RJS_02",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee= "Online fee-Fresher"




                },
                new Class
                {
                    Id = 12,
                    TrainingProgramCode = 3,
                    ClassName = "Front End React Js 03",
                    ClassCode = "RJS_03",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("07:00:00"),
                    ClassTimeEnd = DateTime.Parse("09:00:00"),
                    Attendee = "Online fee-Fresher"



                },
                new Class
                {
                    Id = 13,
                    TrainingProgramCode = 3,
                    ClassName = "Front End React Js 04",
                    ClassCode = "RJS_04",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("07:00:00"),
                    ClassTimeEnd = DateTime.Parse("09:00:00"),
                    Attendee = "Online fee-Fresher"



                },
                new Class
                {
                    Id = 14,
                    TrainingProgramCode = 4,
                    ClassName = "Full Stack 01",
                    ClassCode = "FST_01",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee = "Online fee-Fresher"



                }, new Class
                {
                    Id = 15,
                    TrainingProgramCode = 4,
                    ClassName = "Full Stack 02",
                    ClassCode = "FST_02",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee = "Online fee-Fresher"


                },

                new Class
                {
                    Id = 16,
                    TrainingProgramCode = 1,
                    ClassName = "Backend CSharp 06",
                    ClassCode = "HCM_CPL_NET_06",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-1-1"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee="Offline fee-Fresher"



                },
                new Class
                {
                    Id= 17,
                    TrainingProgramCode = 4,
                    ClassName = "Full Stack 04",
                    ClassCode = "FST_04",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("07:00:00"),
                    ClassTimeEnd = DateTime.Parse("09:00:00"),
                    Attendee= "Offline fee-Fresher"

                },
                new Class
                {

                    Id = 18,
                    TrainingProgramCode = 5,
                    ClassName = "DevOps 01",
                    ClassCode = "DEVOPS_01",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("13:00:00"),
                    ClassTimeEnd = DateTime.Parse("15:00:00"),
                    Attendee = "Offline fee-Fresher"



                },
                new Class
                {

                    Id = 19,
                    TrainingProgramCode = 5,
                    ClassName = "DevOps 02",
                    ClassCode = "DEVOPS_02",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee = "Offline fee-Fresher"
                },
                new Class
                {
                    Id = 20,
                    TrainingProgramCode = 5,
                    ClassName = "DevOps 03",
                    ClassCode = "DEVOPS_03",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-01-01"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("13:00:00"),
                    ClassTimeEnd = DateTime.Parse("15:00:00"),
                    Attendee = "Offline fee-Fresher"
                },
                new Class
                {
                    Id = 21,
                    TrainingProgramCode = 6,
                    ClassName = "Database Engineer 01",
                    ClassCode = "DBE_01",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-1-1"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee = "Offline fee-Fresher"
                },
                new Class
                {
                    Id = 22,
                    TrainingProgramCode = 6,
                    ClassName = "Database Engineer 02",
                    ClassCode = "DBE_02",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-1-1"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee = "Offline fee-Fresher"

                },

                new Class
                {
                    Id = 23,
                    TrainingProgramCode = 6,
                    ClassName = "Database Engineer 03",
                    ClassCode = "DBE_03",
                    Duration = 90,
                    Status = "Opening",
                    Location = "Ho Chi Minh",
                    FSU = "FHM",
                    StartDate = DateTime.Parse("2024-1-1"),
                    EndDate = new DateTime(2024, 4, 3),
                    CreatedBy = "Super Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Super Admin",
                    ClassTimeStart = DateTime.Parse("09:00:00"),
                    ClassTimeEnd = DateTime.Parse("12:00:00"),
                    Attendee = "Offline fee-Fresher"
                }

             );
            for (int num = 1; num <=12; num++)
            {
                modelBuilder.Entity<AssessmentScheme>().HasData(
                      new AssessmentScheme()
                      {
                          Id=num,
                          SyllabusId =num,
                          Assignment=10,
                          Final=30,
                          FinalPractice=50,
                          FinalTheory=50,
                          Passing=80,
                          Quiz=60
                      }

                    );
            }

            var programSyllabus = new List<TrainingProgramSyllabus>() {
                new TrainingProgramSyllabus()
                {

                    Sequence=1,
                    TrainingProgramCode = 1,
                    SyllabusId = 1,
                },
                new TrainingProgramSyllabus()
                {
                     Sequence=2,
                    TrainingProgramCode = 1,
                    SyllabusId = 11,
                },
                new TrainingProgramSyllabus()
                {
                    Sequence=3,
                    TrainingProgramCode = 1,
                    SyllabusId = 8,
                }
                , new TrainingProgramSyllabus()
                {
                    Sequence=1,
                    TrainingProgramCode = 2,
                    SyllabusId = 1,
                },
                new TrainingProgramSyllabus()
                {
                    Sequence=2,
                    TrainingProgramCode = 2,
                    SyllabusId = 11,
                },
                new TrainingProgramSyllabus()
                {
                    Sequence=3,
                    TrainingProgramCode = 2,
                    SyllabusId = 8 ,
                }
                , new TrainingProgramSyllabus()
                {
                    Sequence = 1,
                    TrainingProgramCode = 3,
                    SyllabusId = 3,
                },
                new TrainingProgramSyllabus()
                {
                    Sequence = 2,
                    TrainingProgramCode = 3,
                    SyllabusId = 10,
                },
                new TrainingProgramSyllabus()
                {
                    Sequence = 3,
                    TrainingProgramCode = 3,
                    SyllabusId = 4,
                }
                , new TrainingProgramSyllabus()
                {
                    Sequence = 1,
                    TrainingProgramCode = 4,
                    SyllabusId = 8,
                },
                new TrainingProgramSyllabus()
                {
                    Sequence = 2,
                    TrainingProgramCode = 4,
                    SyllabusId = 10,
                },
                new TrainingProgramSyllabus()
                {
                    Sequence = 3,
                    TrainingProgramCode = 4,
                    SyllabusId = 4,
                }
                , new TrainingProgramSyllabus()
                {
                    Sequence = 1,
                    TrainingProgramCode = 5,
                    SyllabusId = 6,
                },
                new TrainingProgramSyllabus()
                {
                    Sequence = 2,
                    TrainingProgramCode = 5,
                    SyllabusId = 11,
                },
                new TrainingProgramSyllabus()
                {
                    Sequence = 3,
                    TrainingProgramCode = 5,
                    SyllabusId = 12,
                }
                };
            i = 1;
            foreach (var programsys in programSyllabus)
            {
                programsys.Id = i;
                i++;
                modelBuilder.Entity<TrainingProgramSyllabus>().HasData(programsys);
            }

            for (var A = 1; A<=4; A++)
                modelBuilder.Entity<ClassUser>().HasData(
                    new ClassUser()
                    {

                        ClassId = A,
                        UserId = 1,
                        UserType = 1
                    }
                    );
            for (var a = 5; a<=9; a++) modelBuilder.Entity<ClassUser>().HasData(
                 new ClassUser()
                 {
                     ClassId = a,
                     UserId = 12,
                     UserType = 1
                 }
                 );
            for (var a = 10; a <= 14; a++) modelBuilder.Entity<ClassUser>().HasData(
                 new ClassUser()
                 {
                     ClassId = a,
                     UserId = 14,
                     UserType = 1
                 }
                 );
            for (var ai = 15; ai <= 18; ai++) modelBuilder.Entity<ClassUser>().HasData(
                 new ClassUser()
                 {
                     ClassId = ai,
                     UserId = 20,
                     UserType = 1
                 }
                 );
            for (var ai = 1; ai <= 4; ai++)
                modelBuilder.Entity<ClassUser>().HasData(
                    new ClassUser()
                    {
                        ClassId = ai,
                        UserId = 8,
                        UserType = 1
                    }
                    );
            for (var ai = 5; ai <= 9; ai++) modelBuilder.Entity<ClassUser>().HasData(
                 new ClassUser()
                 {
                     ClassId = ai,
                     UserId = 10,
                     UserType = 1
                 }
                 );
            for (var ai = 10; ai <= 14; ai++) modelBuilder.Entity<ClassUser>().HasData(
                 new ClassUser()
                 {
                     ClassId = ai,
                     UserId = 15,
                     UserType = 1
                 }
                 );
            for (var ai = 15; ai <= 18; ai++) modelBuilder.Entity<ClassUser>().HasData(
                 new ClassUser()
                 {
                     ClassId = ai,
                     UserId = 13,
                     UserType = 1
                 }
                 );

            modelBuilder.Entity<CalendarClass>().HasData(
                new CalendarClass()
                {
                    Id = 1,
                    ClassId = 1,
                    DateAndTimeStudy = DateTime.Parse("2024-04-21")
                },
                new CalendarClass()
                {
                    Id = 2,
                    ClassId = 1,
                    DateAndTimeStudy = DateTime.Parse("2024-04-23")
                },
                new CalendarClass()
                {
                    Id = 3,
                    ClassId = 1,
                    DateAndTimeStudy = DateTime.Parse("2024-04-25")
                },
                new CalendarClass()
                {
                    Id = 4,
                    ClassId = 1,
                    DateAndTimeStudy = DateTime.Parse("2024-04-27")
                }
                );
        }
    }
}
