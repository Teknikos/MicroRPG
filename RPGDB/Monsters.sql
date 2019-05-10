CREATE TABLE [dbo].[Monsters]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[Name] nvarchar (64) not null unique,
	[EnvTags] nvarchar (64) not null,
	[Description] nvarchar (1024) not null,
	[Speed] int not null,
	[HP] int not null,
	[Reduction] int not null,
	[Damage] nvarchar (16) not null
)
