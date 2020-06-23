use SupermarketQuery

print @@TranCount
SET TRANSACTION ISOLATION level read uncommitted
begin tran
insert Providers values(200,'name','country')
WAITFOR DELAY '00:00:03'
insert Transporters values(200,'name','country')
commit tran


-- Delete from Providers where ProviderId = 200
-- Delete from Transporters where TransporterId = 200