use SupermarketQuery


SET TRANSACTION ISOLATION LEVEL read committed
begin tran
Select Name from Providers where ProviderId = 181
WAITFOR DELAY '00:00:03'
Select Name from Providers where ProviderId = 181
commit tran