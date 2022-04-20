CREATE PROCEDURE [dbo].[AddInvoicePayment] 
    @AccountId int, 
    @GrossAmount decimal, 
    @PaypalFee decimal, 
    @MarvelousFee decimal,  
    @TransactionAmount decimal, 
    @TransactionId BIGINT 
AS
	insert into [dbo].[InvoicePayment] 
    ([AccountId],
    [GrossAmount],
    [PaypalFee],
    [MarvelousFee],
    [TransactionAmount],
    [TransactionId])
values 
   (@AccountId, 
    @GrossAmount, 
    @PaypalFee, 
    @MarvelousFee,  
    @TransactionAmount, 
    @TransactionId)
