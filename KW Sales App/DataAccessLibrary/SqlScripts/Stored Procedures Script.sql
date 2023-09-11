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

ALTER PROCEDURE dbo.usp_GetAllCustomers AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;

		SELECT CustomerID, FirstName, LastName, Address, City, State, Zip
		FROM dbo.Customers;
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
END;
GO

--**************Product Stored Procedures
ALTER PROCEDURE dbo.usp_GetAllProducts AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;

		SELECT ProductID, Description, Price, NumberSold
		FROM dbo.Products;
	END TRY

	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Message;
	END CATCH;
END;
GO