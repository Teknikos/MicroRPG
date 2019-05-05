CREATE TABLE [dbo].[Monsters]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] nvarchar (64) not null,
	[EnvTags] nvarchar (64) not null,
	[Description] nvarchar (128) not null,
	[Speed] int not null,
	[HP] int not null,
	[Reduction] int not null,
	[Damage] nvarchar (16) not null
)
