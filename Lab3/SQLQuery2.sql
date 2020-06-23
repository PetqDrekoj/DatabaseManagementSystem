use SupermarketQuery
-- create a stored procedure that inserts data in tables that are in a m:n relationship; if an insert fails, 
-- try to recover as much as possible from the entire operation: for example if the user wants to add a book and its authors,
-- succeeds creating the authors, but fails with the book, the authors should remain in the database


-- Transporter, Transportation, Provider

go
Alter PROCEDURE insert_and_rollback_partial AS BEGIN
	Declare @transaction_has_savepoint Int = 0
	
	BEGIN TRY
		BEGIN TRAN
		Insert into Transporters values (102,'Name','Country')
		print 'Transporter insertion was ok'
		SAVE TRANSACTION my_checkpoint
		SET @transaction_has_savepoint = 1
		Insert into Providers values (102,'Name','Country')
		print 'Provider insertion was ok'
		SAVE TRANSACTION my_checkpoint
		Insert into Transportations(TransportationId,TransporterId,ProviderId) values (102,102,101)
		print 'Transportation insertion was ok'
		SAVE TRANSACTION my_checkpoint

		COMMIT TRAN
	END TRY

	BEGIN CATCH
		print 'There was an error, saves until the last checkpoint'
		commit tran my_checkpoint
	END CATCH
END

EXEC insert_and_rollback_partial
Select * from Transportations
Select * from Transporters
Select * from Providers


DELETE FROM Transportations where TransportationId = 102
DELETE FROM Transporters where TransporterId = 102
DELETE FROM Providers where ProviderId = 102