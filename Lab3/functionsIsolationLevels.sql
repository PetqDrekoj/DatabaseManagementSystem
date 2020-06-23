use SupermarketQuery
-- dirty reads
Insert Providers values (181,'OriginalName','Country')

go
alter procedure f1 as begin
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	begin tran
	Select Name from Providers where ProviderId = 181
	WAITFOR DELAY '00:00:03'
	Select Name from Providers where ProviderId = 181
	commit tran
end

go
alter procedure f2 as begin
	begin tran
	Update Providers set Name='changedName' where ProviderId = 181
	print 'name changed'
	WAITFOR DELAY '00:00:03'
	commit tran
end

create procedure f2_undo as begin
	Update Providers set Name='OriginalName' where ProviderId = 181
end


