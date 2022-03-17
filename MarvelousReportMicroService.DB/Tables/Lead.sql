﻿   
CREATE TABLE [dbo].[Lead]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ExternalId] int not null,
	[Name] varchar(30) NOT NULL,
	[LastName] varchar(30) NOT NULL,
	[BirthDate] DATETIME NOT NULL,
	[Email] varchar(30) NOT NULL,
	[Phone] varchar(10) NULL,
	[Password] varchar(100) NOT NULL,
	[Role] TINYINT NOT NULL, 
    [IsBanned] BIT NOT NULL, 
    [City] varchar(30) NULL, 
    CONSTRAINT AK_Email UNIQUE(Email),
)

