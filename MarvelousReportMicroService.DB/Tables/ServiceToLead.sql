CREATE TABLE [dbo].[ServiceToLead]
(
  [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [Type] TINYINT NOT NULL,
  [Period] int NULL,
  [Price] decimal(10,0) NOT NULL,
  [Status] TINYINT NOT NULL,
  [ServiceId] int NOT NULL,
  [LeadId] int NOT NULL,
  [TransactionId] int NOT NULL, 
    CONSTRAINT [FK_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Service]([Id]), 
    CONSTRAINT [FK_LeadId] FOREIGN KEY ([LeadId]) REFERENCES [Lead]([Id]),
  CONSTRAINT [FK_TransactionId] FOREIGN KEY ([TransactionId]) REFERENCES [Transaction]([Id])
)