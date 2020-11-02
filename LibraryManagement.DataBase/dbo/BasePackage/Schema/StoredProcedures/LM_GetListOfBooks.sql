CREATE PROCEDURE [dbo].[LM_GetListOfBooks]
(
	@BookName NVARCHAR(200) NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @BookId INT;

	SET @BookId = (SELECT TOP 1 BookId FROM LM_BookMaster WHERE BookName = @BookName)

	IF(@BookId IS NULL OR @BookId = '' OR @BookId = 0)
	BEGIN
		SELECT BookId, BookName, BookAuther, IsActive, PageCount, Category 
		FROM LM_BookMaster
	END
	ELSE
	BEGIN
		SELECT BookId, BookName, BookAuther, IsActive, PageCount, Category 
		FROM LM_BookMaster WHERE BookId = @BookId
	END
END
