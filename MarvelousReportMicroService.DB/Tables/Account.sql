CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ExternalId] int not null unique,
    [Name] VARCHAR(20) NOT NULL, 
    [CurrencyType] TINYINT NOT NULL, 
    [LeadId] INT NOT NULL,
    [LockDate] DATETIME NULL,
    [IsBlocked] BIT NOT NULL,
    CONSTRAINT [FK_LeadId_ToLead] FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Lead]([ExternalId])
)