use SupermarketQuery
-- create a stored procedure that inserts data in tables that are in a m:n relationship; 
-- if one insert fails, all the operations performed by the procedure must be rolled back
-- Transporter, Transportation, Provider

Select * from Transportations
Select * from Transporters
Select * from Providers

go

ALTER PROCEDURE insert_and_rollback AS BEGIN
	BEGIN TRAN
	BEGIN TRY
		Insert into Transporters values (99,'Name','Country')
		print 'Transporter insertion was ok'
		Insert into Providers values (99,'Name','Country')
		print 'Provider insertion was ok'
		Insert into Transportations(TransportationId,TransporterId,ProviderId) values (99,99,99)
		print 'Transportation insertion was ok'

		COMMIT TRAN
	END TRY

	BEGIN CATCH
		print 'There was an error, rollback required'
		ROLLBACK TRAN
	END CATCH
END

EXEC insert_and_rollback
Select * from Transportations

DELETE FROM Transportations where TransportationId = 99
DELETE FROM Transporters where TransporterId = 99
DELETE FROM Providers where ProviderId = 99