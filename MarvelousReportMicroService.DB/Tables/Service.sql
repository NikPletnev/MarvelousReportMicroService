﻿CREATE TABLE [dbo].[Service]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[ExternalId] int not null unique,
	[Name] VARCHAR(50) UNIQUE NOT NULL,
	[Type] INT NOT NULL,
	[Description] VARCHAR(1000) NOT NULL,
	[Price] decimal(10,0) NOT NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0
)