CREATE PROCEDURE [dbo].[GetProfit]
	@StartDate Date
AS
	select sum(tf.AmountComission) as Amount, year(t.Date) as [Year], month(t.Date) as [Month] from [dbo].[TransactionFee] as tf
	left join [dbo].[Transaction] as t
	on t.ExternalId = tf.TransactionId
	where t.Date > @StartDate
	group by year(t.Date), month(t.date)
	order by year(t.Date), month(t.date)