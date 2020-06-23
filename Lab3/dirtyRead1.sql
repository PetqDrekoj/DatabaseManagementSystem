use SupermarketQuery
begin tran
Update Providers set Name='changedName' where ProviderId = 181
WAITFOR DELAY '00:00:03'
rollback tran
