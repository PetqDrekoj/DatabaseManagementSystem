use SupermarketQuery

SET TRANSACTION ISOLATION LEVEL serializable
begin tran
Select Count(*) from Providers 
WAITFOR DELAY '00:00:03'
Select Count(*) from Providers
commit tran