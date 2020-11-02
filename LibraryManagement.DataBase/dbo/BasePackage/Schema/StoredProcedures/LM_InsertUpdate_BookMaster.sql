CREATE PROCEDURE [dbo].[LM_InsertUpdate_BookMaster]
(
	@BookName NVARCHAR(200),
	@BookAuthor NVARCHAR(200) = NULL,
	@IsActive BIT,
	@PageCount INT = NULL,
	@Category NVARCHAR(50) = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @BookId INT
	SET @BookId = (SELECT TOP 1 BookId FROM LM_BookMaster WHERE BookName = @BookName)

	IF(@BookId = '' OR @BookId = 0 OR @BookId IS NULL)
	BEGIN
		INSERT INTO [dbo].[LM_BookMaster]
		(
			[BookName],
			[BookAuther],
			[IsActive],
			[PageCount],
			[Category]
		)
		SELECT @BookName, @BookAuthor, @IsActive, @PageCount, @Category
	END
	ELSE
	BEGIN
		UPDATE [dbo].[LM_BookMaster]
		SET [BookName] = @BookName,
			[BookAuther] = @BookAuthor,
			[IsActive] = @IsActive,
			[PageCount] = @PageCount,
			[Category] = @Category
		WHERE BookId = @BookId
	END
END
