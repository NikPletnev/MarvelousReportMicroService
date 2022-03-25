CREATE TABLE [dbo].[ServiceToLead]
(
  [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [ExternalId] int not null unique,
  [Price] decimal(10,0) NOT NULL,
  [Status] TINYINT NOT NULL,
  [Period] TINYINT NULL,
  [ServiceId] int NOT NULL,
  [LeadId] int NOT NULL,
	CONSTRAINT [FK_ServiceToLead_Service] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service]([ExternalId]),
	CONSTRAINT [FK_ServiceToLead_Lead] FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Lead]([ExternalId])
)