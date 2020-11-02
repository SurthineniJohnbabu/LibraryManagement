CREATE PROCEDURE [dbo].[LM_InsertUpdate_UserBookMapping]
(
	@UserId INT,
	@BookName NVARCHAR(200),
	@IsFavorite BIT = 0,
	@IsRead BIT = 0,
	@BookReview NVARCHAR(MAX) = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @BookId INT,
			@UserBookMappingId INT
	SET @BookId = (SELECT TOP 1 BookId FROM LM_BookMaster WHERE BookName = @BookName AND IsActive = 1)

	IF @BookId = '' OR @BookId = 0 OR @BookId IS NULL
	BEGIN
		PRINT 'Book Id is blank/empty'
		RETURN;
	END

	SET @UserBookMappingId = (SELECT UserBookMappingId FROM [dbo].[LM_UserBookMapping] WHERE UserId = @UserId AND BookId = @BookId)

	IF(@UserBookMappingId IS NULL)
	BEGIN
		INSERT INTO [dbo].[LM_UserBookMapping]
		(
			[UserId],
			[BookId],
			[IsFavorite],
			[IsRead],
			[BookReview],
			[CreatedDate],
			[CreatedBy],
			[ModifiedBy],
			[ModifiedDate]
		)
		SELECT @UserId, @BookId, @IsFavorite, @IsRead, @BookReview, GETDATE(), 'Admin', NULL, NULL
	END
	ELSE
	BEGIN
		UPDATE [dbo].[LM_UserBookMapping]
		SET [IsFavorite] = @IsFavorite,
			[IsRead] = @IsRead,
			[ModifiedBy] = @UserId,
			[ModifiedDate] = GETDATE()
		WHERE BookId = @BookId AND UserId = @UserId
	END
END
