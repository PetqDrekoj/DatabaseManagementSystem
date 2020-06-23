use SupermarketQuery
-- Insert into Providers values(250,'name','country')
-- Select * from Providers
BEGIN TRAN
UPDATE Providers SET Name='newname1' where ProviderId = 250
SELECT Name FROM Providers where ProviderId = 250
COMMIT TRAN

-- UPDATE Providers SET name='name' where ProviderId = 250
