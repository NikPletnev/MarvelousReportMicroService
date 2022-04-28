CREATE TABLE [dbo].[TransactionFee]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TransactionId] INT NOT NULL, 
    [AmountComission] DECIMAL(9, 4) NOT NULL
)
