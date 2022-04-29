   
CREATE TABLE [dbo].[Lead]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ExternalId] int not null unique,
	[Name] varchar(20) NOT NULL,
	[LastName] varchar(20) NOT NULL,
	[BirthDay] TINYINT NOT NULL,
	[BirthMonth] TINYINT NOT NULL,
	[BirthDate] DATE NOT NULL,
	[Email] varchar(30) NOT NULL,
	[Phone] varchar(13) NULL,
	[Password] varchar(70) NOT NULL,
	[Role] TINYINT NOT NULL, 
    [IsBanned] BIT NOT NULL, 
    [City] varchar(30) NULL,
	CONSTRAINT AK_ExternalId UNIQUE(ExternalId)
)


GO

CREATE INDEX [IX_Lead_ExternalId] ON [dbo].[Lead] ([ExternalId])
GO

CREATE INDEX [IX_Lead_Name] ON [dbo].[Lead] ([Name])
GO

CREATE INDEX [IX_Lead_LastName] ON [dbo].[Lead] ([LastName])
GO

CREATE INDEX [IX_Lead_BirthDate] ON [dbo].[Lead] ([BirthDate])
GO

CREATE INDEX [IX_Lead_Email] ON [dbo].[Lead] ([Email])
GO

CREATE INDEX [IX_Lead_Phone] ON [dbo].[Lead] ([Phone])
GO

CREATE INDEX [IX_Lead_City] ON [dbo].[Lead] ([City])






