CREATE PROCEDURE [dbo].[LM_GetAllUserReviews]
(
	@BookName NVARCHAR(200) NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @BookId INT
	SET @BookId = (SELECT TOP 1 BookId FROM LM_BookMaster WHERE BookName = @BookName AND IsActive = 1)

	IF(@BookId IS NULL)
	BEGIN
		SELECT UM.UserName, BM.BookName, UBM.BookReview FROM LM_UserBookMapping UBM 
		INNER JOIN LM_BookMaster BM ON UBM.BookId = BM.BookId
		INNER JOIN LM_UserMaster UM ON UBM.UserId = UM.UserId
		WHERE BM.IsActive = 1 AND UM.IsActive = 1
	END
	ELSE
	BEGIN		
		SELECT UM.UserName, BM.BookName, UBM.BookReview FROM LM_UserBookMapping UBM 
		INNER JOIN LM_BookMaster BM ON UBM.BookId = BM.BookId
		INNER JOIN LM_UserMaster UM ON UBM.UserId = UM.UserId
		WHERE UBM.BookId = @BookId AND BM.IsActive = 1 AND UM.IsActive = 1
	END
END
