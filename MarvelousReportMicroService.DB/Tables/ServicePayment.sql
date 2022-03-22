CREATE TABLE [dbo].[ServicePayment]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ExternalId] int not null unique,
	[ServiceToLeadId] INT NOT NULL,
	[TransactionId] int NOT NULL
	CONSTRAINT [FK_Transaction_ServiceToLead] FOREIGN KEY ([ServiceToLeadId]) REFERENCES [dbo].[ServiceToLead]([ExternalId])
)