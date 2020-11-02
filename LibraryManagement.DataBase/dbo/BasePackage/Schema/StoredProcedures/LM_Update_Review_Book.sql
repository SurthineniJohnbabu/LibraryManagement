CREATE PROCEDURE [dbo].[LM_Update_Review_Book]
(
	@UserId INT,
	@BookName NVARCHAR(200),
	@BookReview NVARCHAR(MAX)
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @UserBookMappingId INT,
			@BookId INT,
			@ReviewExists NVARCHAR(MAX) = NULL

	SET @BookId = (SELECT TOP 1 BookId FROM LM_BookMaster WHERE BookName = @BookName AND IsActive = 1)

	IF @BookId = '' OR @BookId = 0 OR @BookId IS NULL
	BEGIN
		PRINT 'Book Id is blank/empty'
		RETURN;
	END

	SET @UserBookMappingId = (SELECT UserBookMappingId FROM [dbo].[LM_UserBookMapping] WHERE UserId = @UserId AND BookId = @BookId)

	IF @UserBookMappingId IS NULL OR @UserBookMappingId = '' OR @UserBookMappingId = 0
	BEGIN
		PRINT 'Book Id and User Id mapping is not available'
		RETURN;		
	END

	SET @ReviewExists = (SELECT BookReview FROM [dbo].[LM_UserBookMapping] WHERE UserId = @UserId AND BookId = @BookId)

	IF(@ReviewExists IS NOT NULL)
	BEGIN
		PRINT 'Review is already exists for this Book'
		RETURN;
	END
	ELSE
	BEGIN	
		UPDATE [dbo].[LM_UserBookMapping]
		SET [BookReview] = @BookReview,
			[ModifiedBy] = @UserId,
			[ModifiedDate] = GETDATE()
		WHERE BookId = @BookId AND UserId = @UserId	
	END
END
