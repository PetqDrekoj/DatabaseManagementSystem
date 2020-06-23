use SupermarketQuery
begin tran
WAITFOR DELAY '00:00:03'
insert Providers values(181,'OriginalName','Country')
commit tran

--Delete from Providers where ProviderId = 181
