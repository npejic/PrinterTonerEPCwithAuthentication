USE [DefaultConnection]
GO

INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'fcda3264-d9a4-4874-9298-5eb61feb7718', N'Admin')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Nick], [CellPhone], [Remark], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'135a76c8-60d9-4010-ab19-f047c0c09f5b', N'Jevtić Dragan', N'Bucko', N'069-654-026', NULL, N'jevticd@epc.rs', 0, N'ABjvENIfEw1QjiefId0nMp2mYZCSOAnK3fdYtKUAsGq3sEqFm9/u14bwBmOH0aV2Mg==', N'fe6b896e-fba1-4991-a0f6-50cb4994df6a', NULL, 0, 0, NULL, 1, 0, N'jevticd')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Nick], [CellPhone], [Remark], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1d3ef61c-6eb5-4d0e-b208-34d4bb577855', N'Tančik Dragica', N'Daca', N'062-508-272', NULL, N'tancikd@epc.rs', 0, N'AKLCfixpjSrcZ8YhwZhciw4UjQxiBfFAZMWr1ixTgBEB1Yr9Y4WrJ8freJ8vAR6Lbw==', N'bda2362f-4fd0-4795-b092-d24956d32806', NULL, 0, 0, NULL, 1, 0, N'tancikd')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Nick], [CellPhone], [Remark], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7f29ef88-ceb8-4a07-87a1-f24a594ae194', N'Pejić Nikola', N'Nik', N'213421421', NULL, N'npejic@gmail.com', 0, N'ABq63gRvK8ji0nEkpkyXU2GhbOwE3v7GCRkctuonKREj0Q2nnidFfIP85wZhjNmiUw==', N'e12e00c4-8ed9-4638-a349-1d85765eca8a', NULL, 0, 0, NULL, 1, 0, N'npejic')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Nick], [CellPhone], [Remark], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'858cfcb7-1da5-4da4-b9a2-9713f4a70b57', N'Ađanski Goran', N'Goran', N'069-654-988', NULL, N'adjanskig@epc.rs', 0, N'AOZepRZ1A99mi+3uwY6keJuJ10qmhZcjsENarSEH+UluO8bOmo4aQIrAvR9x8sRRjQ==', N'6a9745e5-d13e-4b34-a467-d06af496c5b7', NULL, 0, 0, NULL, 1, 0, N'adjanskig')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Nick], [CellPhone], [Remark], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'88f0eb37-6b20-4952-84aa-01c9cc409260', N'Šimić Ognjen', N'Ognjen', N'069-654-990', NULL, N'simico@epc.rs', 0, N'AIInVJYIN8fjAAE6u4R2nvXcrfh59FxWYijU2ua7+LEnai7Q0CXh7v92JDqpzbVUaQ==', N'9de85da3-6c4e-44f6-aa14-f2471e1df3f7', NULL, 0, 0, NULL, 1, 0, N'simico')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Nick], [CellPhone], [Remark], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a4ba3e62-7f4b-4a3b-8fa6-775281339ff6', N'Tančik Nemanja', N'Tanči', N'069-654-991', NULL, N'tancikn@epc.rs', 0, N'AAnjrscZQrOD9mSR9reyf6Q3sq0hn48ENdOP42Uqew9wGAOUChIRcGcX5cXW4UPSpA==', N'492bf1a9-a665-4852-b092-16dfc252701b', NULL, 0, 0, NULL, 1, 0, N'tancikn')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Nick], [CellPhone], [Remark], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd29b3eec-0f99-4d96-b607-8b294bbb0cae', N'Šimić Marko', N'Marko', N'069-527-3777', NULL, N'simicm@epc.rs', 0, N'ANa+eyOY5oU3T8GF2ro770M+A6iUdl0dC1EqubN0W4Fb+E/qD8Mlih30Y18BccnsYg==', N'dc289852-ad06-48c8-94b1-30aeee7eda9a', NULL, 0, 0, NULL, 1, 0, N'simicm')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88f0eb37-6b20-4952-84aa-01c9cc409260', N'fcda3264-d9a4-4874-9298-5eb61feb7718')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a4ba3e62-7f4b-4a3b-8fa6-775281339ff6', N'fcda3264-d9a4-4874-9298-5eb61feb7718')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd29b3eec-0f99-4d96-b607-8b294bbb0cae', N'fcda3264-d9a4-4874-9298-5eb61feb7718')
GO

use [DefaultConnection]
CREATE TABLE [dbo].[ToDoes1](
	[ToDoID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Closed] [datetime] NULL,
	[ApplicationUserID] [nvarchar](max) NOT NULL,
	[IsReady] [bit] NOT NULL,
	[Remark] [nvarchar](max) NULL,
	[Created] [datetime] NOT NULL,
	)

	--select * from dbo.ToDoes1 where ApplicationUserID = '6';

use [DefaultConnection]
update dbo.ToDoes1 set ApplicationUserID = '858cfcb7-1da5-4da4-b9a2-9713f4a70b57' where ApplicationUserID = '7';
update dbo.ToDoes1 set ApplicationUserID = 'd29b3eec-0f99-4d96-b607-8b294bbb0cae' where ApplicationUserID = '6';
update dbo.ToDoes1 set ApplicationUserID = '135a76c8-60d9-4010-ab19-f047c0c09f5b' where ApplicationUserID = '5';
update dbo.ToDoes1 set ApplicationUserID = '1d3ef61c-6eb5-4d0e-b208-34d4bb577855' where ApplicationUserID = '4';
update dbo.ToDoes1 set ApplicationUserID = 'a4ba3e62-7f4b-4a3b-8fa6-775281339ff6' where ApplicationUserID = '3';
update dbo.ToDoes1 set ApplicationUserID = 'dc93e518-c63a-4588-bbb5-89abb96125b1' where ApplicationUserID = '1';
