CREATE TABLE [dbo].[Puzzles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] Varchar(64) not null unique,
	[Description] varchar(1024) not null
)
