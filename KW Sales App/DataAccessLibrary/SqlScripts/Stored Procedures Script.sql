USE SQLKWSALESDB;
GO

--If running this script for the first time change all the ALTER statments to CREATE
--Customers CRUD procedures
ALTER PROCEDURE dbo.usp_InsertCustomer
(
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Address NVARCHAR(100),
	@City NVARCHAR(40),
	@State NVARCHAR(30),
	@Zip NVARCHAR(10)
)AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			SET NOCOUNT ON;

			INSERT INTO dbo.Customers (FirstName, LastName, Address, City, State, Zip)
			VALUES (@FirstName, @LastName, @Address, @City, @State, @Zip);

			--return the id created
			SELECT SCOPE_IDENTITY() AS ID;
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRAN
			END;
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
END;
GO