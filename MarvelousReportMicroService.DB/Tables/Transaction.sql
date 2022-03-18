CREATE TABLE [dbo].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExternalId] [int] not null unique,
	[Amount] [decimal](18, 4) NOT NULL,
	[Type] TINYINT NOT NULL,
	[AccountId] [int] NOT NULL,
	[Date] [datetime2](2) NOT NULL,
	[Currency] TINYINT NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
), 
    CONSTRAINT [FK_Transaction_ToAccaunts] FOREIGN KEY ([AccountId]) REFERENCES [Account]([Id]))