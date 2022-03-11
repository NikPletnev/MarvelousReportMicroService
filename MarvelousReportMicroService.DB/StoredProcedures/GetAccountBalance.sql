CREATE PROCEDURE [dbo].[GetAccountBalance]
	@Id int
AS
Begin
	SELECT 
	SUM(Amount)
	From [dbo].[Transaction] where @Id = AccountId 
End
