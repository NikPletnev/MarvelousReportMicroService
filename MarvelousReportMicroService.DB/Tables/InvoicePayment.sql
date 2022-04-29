CREATE TABLE [dbo].[InvoicePayment]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AccountId] INT NULL, 
    [GrossAmount] DECIMAL(9, 2) NULL, 
    [PaypalFee] DECIMAL(9, 2) NULL, 
    [MarvelousFee] DECIMAL(9, 2) NULL, 
    [TransactionAmount] DECIMAL(9, 2) NULL, 
    [TransactionId] BIGINT NULL
)

GO
