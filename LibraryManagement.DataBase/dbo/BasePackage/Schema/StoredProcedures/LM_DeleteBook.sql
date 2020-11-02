CREATE PROCEDURE [dbo].[LM_DeleteBook]
(
	@BookName NVARCHAR(200)
)
AS 
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @BookId INT

	BEGIN TRY
		BEGIN TRANSACTION
			SET @BookId = (SELECT BookId FROM LM_BookMaster WHERE BookName = @BookName)

			IF EXISTS 
			(
				SELECT TOP 1 1 FROM LM_UserBookMapping UBM 
				INNER JOIN LM_BookMaster BM ON UBM.BookId = BM.BookId
				WHERE BM.BookId = @BookId
			)
			BEGIN
					DELETE FROM LM_UserBookMapping WHERE BookId = @BookId
					UPDATE LM_BookMaster SET IsActive = 0 WHERE BookId = @BookId
			END
			ELSE 
			BEGIN
				UPDATE LM_BookMaster SET IsActive = 0 WHERE BookId = @BookId
			END		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		PRINT 'Exception Occured'
		RETURN
	END CATCH
END
