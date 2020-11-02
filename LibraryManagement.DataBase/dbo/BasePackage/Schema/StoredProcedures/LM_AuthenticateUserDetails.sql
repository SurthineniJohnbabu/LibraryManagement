CREATE PROCEDURE [dbo].[LM_AuthenticateUserDetails]
(
	@UserName NVARCHAR(200),
	@Password NVARCHAR(200)
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @UserId INT;

	SET @UserId = (SELECT TOP 1 UserId FROM LM_UserMaster WHERE UserName = @UserName AND Password = @Password AND IsActive = 1)

	IF(@UserId IS NULL OR @UserId = '' OR @UserId = 0)
	BEGIN
		PRINT 'User is not available. Please register yourself to proceed.'
		RETURN;
	END
	ELSE
	BEGIN
		SELECT UM.UserId, UM.UserName, UM.FirstName, UM.LastName, RM.RoleName FROM LM_UserMaster UM
		INNER JOIN LM_RoleMaster RM ON RM.RoleId = UM.RoleId 
		WHERE UM.UserId = @UserId
	END
END
