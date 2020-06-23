use atmTransaction
begin tran
update banks set Name='name1' where bid=1
WAITFOR DELAY '00:00:03'
update customers set name='name1' where cid=1
commit tran

