rollback
use SupermarketQuery
ALTER DATABASE SupermarketQuery SET ALLOW_SNAPSHOT_ISOLATION ON
go



-- Select * from Providers
SET TRAN ISOLATION LEVEL SNAPSHOT
BEGIN TRAN
SELECT Name FROM Providers where ProviderId = 250
WAITFOR DELAY '00:00:03'
UPDATE Providers SET Name='newname2' where ProviderId = 250
SELECT Name FROM Providers where ProviderId = 250
COMMIT TRAN


ALTER DATABASE SupermarketQuery
SET ALLOW_SNAPSHOT_ISOLATION OFF
ALTER DATABASE SupermarketQuery
SET READ_COMMITTED_SNAPSHOT OFF