use SupermarketQuery

SET TRANSACTION ISOLATION LEVEL Repeatable read
begin tran
Select * from Providers where ProviderId = 181
WAITFOR DELAY '00:00:03'
Select * from Providers where ProviderId = 181
commit tran