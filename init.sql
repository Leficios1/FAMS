CREATE DATABASE [FamsDb]
GO
USE [FamsDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssessmentSchemes]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssessmentSchemes](
	[AssessmentSchemeId] [int] IDENTITY(1,1) NOT NULL,
	[SyllabusId] [int] NOT NULL,
	[Quiz] [float] NOT NULL,
	[Assignment] [float] NOT NULL,
	[Final] [float] NOT NULL,
	[FinalTheory] [float] NOT NULL,
	[FinalPractice] [float] NOT NULL,
	[Passing] [float] NOT NULL,
 CONSTRAINT [PK_AssessmentSchemes] PRIMARY KEY CLUSTERED 
(
	[AssessmentSchemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrainingProgramCode] [int] NOT NULL,
	[ClassName] [nvarchar](100) NOT NULL,
	[ClassCode] [nvarchar](30) NULL,
	[Duration] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Location] [nvarchar](max) NULL,
	[FSU] [nvarchar](10) NULL,
	[StartDate] [datetimeoffset](7) NOT NULL,
	[EndDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassUsers]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassUsers](
	[UserId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
	[UserType] [int] NOT NULL,
 CONSTRAINT [PK_ClassUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryTypes]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](125) NULL,
 CONSTRAINT [PK_DeliveryTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LearningObjectives]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LearningObjectives](
	[ObjectiveCode] [nvarchar](4) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_LearningObjectives] PRIMARY KEY CLUSTERED 
(
	[ObjectiveCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Syllabuses]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Syllabuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SyllabusCode] [nvarchar](max) NOT NULL,
	[SyllabusName] [nvarchar](150) NOT NULL,
	[TechnicalRequirement] [nvarchar](max) NULL,
	[Version] [nvarchar](max) NULL,
	[AttendeeNumber] [int] NOT NULL,
	[CourseObjective] [nvarchar](max) NULL,
	[TrainingMaterials] [nvarchar](max) NULL,
	[TrainingPrinciples] [nvarchar](max) NULL,
	[Priority] [nvarchar](max) NULL,
	[PublishStatus] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Syllabuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SyllabusObjectives]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyllabusObjectives](
	[SyllabusObjectiveId] [int] IDENTITY(1,1) NOT NULL,
	[SyllabusId] [int] NOT NULL,
	[ObjectiveCode] [nvarchar](4) NOT NULL,
 CONSTRAINT [PK_SyllabusObjectives] PRIMARY KEY CLUSTERED 
(
	[SyllabusObjectiveId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingContents]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingContents](
	[TrainingContentId] [int] IDENTITY(1,1) NOT NULL,
	[LearningObjectiveCode] [nvarchar](4) NOT NULL,
	[UnitCode] [int] NOT NULL,
	[ContentName] [nvarchar](max) NOT NULL,
	[DeliveryType] [int] NOT NULL,
	[Duration] [real] NULL,
	[TrainingFormat] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_TrainingContents] PRIMARY KEY CLUSTERED 
(
	[TrainingContentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingPrograms]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingPrograms](
	[TrainingProgramCode] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[StartTime] [datetimeoffset](7) NOT NULL,
	[Duration] [real] NOT NULL,
	[TopicCode] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_TrainingPrograms] PRIMARY KEY CLUSTERED 
(
	[TrainingProgramCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingProgramSyllabuses]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingProgramSyllabuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[SyllabusId] [int] NOT NULL,
	[TrainingProgramCode] [int] NOT NULL,
 CONSTRAINT [PK_TrainingProgramSyllabuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingUnits]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingUnits](
	[UnitCode] [int] IDENTITY(1,1) NOT NULL,
	[UnitName] [nvarchar](150) NOT NULL,
	[DayNumber] [int] NOT NULL,
	[SyllabusId] [int] NOT NULL,
 CONSTRAINT [PK_TrainingUnits] PRIMARY KEY CLUSTERED 
(
	[UnitCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermissions](
	[PermissionId] [char](2) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Syllabus] [tinyint] NOT NULL,
	[TrainingProgram] [tinyint] NOT NULL,
	[Class] [tinyint] NOT NULL,
	[LearningMaterial] [tinyint] NOT NULL,
	[UserManagement] [tinyint] NOT NULL,
 CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/7/2024 6:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Phone] [nvarchar](12) NULL,
	[Password] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[Gender] [nvarchar](10) NULL,
	[PermissionId] [char](2) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AssessmentSchemes]  WITH CHECK ADD  CONSTRAINT [FK_AssessmentSchemes_Syllabuses_SyllabusId] FOREIGN KEY([SyllabusId])
REFERENCES [dbo].[Syllabuses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AssessmentSchemes] CHECK CONSTRAINT [FK_AssessmentSchemes_Syllabuses_SyllabusId]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_TrainingPrograms_TrainingProgramCode] FOREIGN KEY([TrainingProgramCode])
REFERENCES [dbo].[TrainingPrograms] ([TrainingProgramCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_TrainingPrograms_TrainingProgramCode]
GO
ALTER TABLE [dbo].[ClassUsers]  WITH CHECK ADD  CONSTRAINT [FK_ClassUsers_Classes_ClassId] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Classes] ([Id])
GO
ALTER TABLE [dbo].[ClassUsers] CHECK CONSTRAINT [FK_ClassUsers_Classes_ClassId]
GO
ALTER TABLE [dbo].[ClassUsers]  WITH CHECK ADD  CONSTRAINT [FK_ClassUsers_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ClassUsers] CHECK CONSTRAINT [FK_ClassUsers_Users_UserId]
GO
ALTER TABLE [dbo].[Syllabuses]  WITH CHECK ADD  CONSTRAINT [FK_Syllabuses_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Syllabuses] CHECK CONSTRAINT [FK_Syllabuses_Users_UserId]
GO
ALTER TABLE [dbo].[SyllabusObjectives]  WITH CHECK ADD  CONSTRAINT [FK_SyllabusObjectives_LearningObjectives_ObjectiveCode] FOREIGN KEY([ObjectiveCode])
REFERENCES [dbo].[LearningObjectives] ([ObjectiveCode])
GO
ALTER TABLE [dbo].[SyllabusObjectives] CHECK CONSTRAINT [FK_SyllabusObjectives_LearningObjectives_ObjectiveCode]
GO
ALTER TABLE [dbo].[SyllabusObjectives]  WITH CHECK ADD  CONSTRAINT [FK_SyllabusObjectives_Syllabuses_SyllabusId] FOREIGN KEY([SyllabusId])
REFERENCES [dbo].[Syllabuses] ([Id])
GO
ALTER TABLE [dbo].[SyllabusObjectives] CHECK CONSTRAINT [FK_SyllabusObjectives_Syllabuses_SyllabusId]
GO
ALTER TABLE [dbo].[TrainingContents]  WITH CHECK ADD  CONSTRAINT [FK_TrainingContents_DeliveryTypes_DeliveryType] FOREIGN KEY([DeliveryType])
REFERENCES [dbo].[DeliveryTypes] ([Id])
GO
ALTER TABLE [dbo].[TrainingContents] CHECK CONSTRAINT [FK_TrainingContents_DeliveryTypes_DeliveryType]
GO
ALTER TABLE [dbo].[TrainingContents]  WITH CHECK ADD  CONSTRAINT [FK_TrainingContents_LearningObjectives_LearningObjectiveCode] FOREIGN KEY([LearningObjectiveCode])
REFERENCES [dbo].[LearningObjectives] ([ObjectiveCode])
GO
ALTER TABLE [dbo].[TrainingContents] CHECK CONSTRAINT [FK_TrainingContents_LearningObjectives_LearningObjectiveCode]
GO
ALTER TABLE [dbo].[TrainingContents]  WITH CHECK ADD  CONSTRAINT [FK_TrainingContents_TrainingUnits_UnitCode] FOREIGN KEY([UnitCode])
REFERENCES [dbo].[TrainingUnits] ([UnitCode])
GO
ALTER TABLE [dbo].[TrainingContents] CHECK CONSTRAINT [FK_TrainingContents_TrainingUnits_UnitCode]
GO
ALTER TABLE [dbo].[TrainingPrograms]  WITH CHECK ADD  CONSTRAINT [FK_TrainingPrograms_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[TrainingPrograms] CHECK CONSTRAINT [FK_TrainingPrograms_Users_UserId]
GO
ALTER TABLE [dbo].[TrainingProgramSyllabuses]  WITH CHECK ADD  CONSTRAINT [FK_TrainingProgramSyllabuses_Syllabuses_SyllabusId] FOREIGN KEY([SyllabusId])
REFERENCES [dbo].[Syllabuses] ([Id])
GO
ALTER TABLE [dbo].[TrainingProgramSyllabuses] CHECK CONSTRAINT [FK_TrainingProgramSyllabuses_Syllabuses_SyllabusId]
GO
ALTER TABLE [dbo].[TrainingProgramSyllabuses]  WITH CHECK ADD  CONSTRAINT [FK_TrainingProgramSyllabuses_TrainingPrograms_TrainingProgramCode] FOREIGN KEY([TrainingProgramCode])
REFERENCES [dbo].[TrainingPrograms] ([TrainingProgramCode])
GO
ALTER TABLE [dbo].[TrainingProgramSyllabuses] CHECK CONSTRAINT [FK_TrainingProgramSyllabuses_TrainingPrograms_TrainingProgramCode]
GO
ALTER TABLE [dbo].[TrainingUnits]  WITH CHECK ADD  CONSTRAINT [FK_TrainingUnits_Syllabuses_SyllabusId] FOREIGN KEY([SyllabusId])
REFERENCES [dbo].[Syllabuses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingUnits] CHECK CONSTRAINT [FK_TrainingUnits_Syllabuses_SyllabusId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserPermissions_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[UserPermissions] ([PermissionId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserPermissions_PermissionId]
GO
