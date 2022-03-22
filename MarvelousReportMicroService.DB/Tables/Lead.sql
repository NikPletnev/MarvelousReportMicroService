   
CREATE TABLE [dbo].[Lead]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ExternalId] int not null unique,
	[Name] varchar(20) NOT NULL,
	[LastName] varchar(20) NOT NULL,
	[BirthDay] TINYINT NOT NULL,
	[BirthMonth] TINYINT NOT NULL,
	[BirthYear] SMALLINT NOT NULL,
	[Email] varchar(30) NOT NULL,
	[Phone] varchar(13) NULL,
	[Password] varchar(70) NOT NULL,
	[Role] TINYINT NOT NULL, 
    [IsBanned] BIT NOT NULL, 
    [City] varchar(30) NULL, 
    CONSTRAINT AK_Email UNIQUE(Email),
	CONSTRAINT AK_ExternalId UNIQUE(ExternalId)
)

