use atmTransaction
begin tran
update customers set name='name2' where cid=1
WAITFOR DELAY '00:00:03'
update banks set Name='name2' where bid=1
commit tran

