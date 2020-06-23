use SupermarketQuery
begin tran
WAITFOR DELAY '00:00:03'
Update Providers set Name='changedName' where ProviderId = 181
commit tran

select * from Providers
Update Providers set Name='OriginalName' where ProviderId = 181
