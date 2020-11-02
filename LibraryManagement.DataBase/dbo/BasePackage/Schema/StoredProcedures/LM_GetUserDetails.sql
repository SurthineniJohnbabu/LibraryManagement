CREATE PROCEDURE [dbo].[LM_GetUserDetails]
(
	@UserId INT NULL
)
AS
BEGIN
	SET NOCOUNT ON;
	
	IF(@UserId IS NULL OR @UserId ='' OR @UserId = 0)
	BEGIN
		SELECT UM.UserId, UM.UserName, UM.FirstName, UM.LastName, RM.RoleName FROM LM_UserMaster UM
		INNER JOIN LM_RoleMaster RM ON RM.RoleId = UM.RoleId
		WHERE UM.IsActive = 1
	END
	ELSE
	BEGIN
		SELECT UM.UserId, UM.UserName, UM.FirstName, UM.LastName, RM.RoleName FROM LM_UserMaster UM
		INNER JOIN LM_RoleMaster RM ON RM.RoleId = UM.RoleId 
		WHERE UM.UserId = @UserId AND UM.IsActive = 1
	END
END
