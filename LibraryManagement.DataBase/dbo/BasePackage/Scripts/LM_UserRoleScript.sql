DECLARE @RoleName NVARCHAR(50)

/*------------Admin script -----------------*/
SET @RoleName = 'Admin'

IF NOT EXISTS(SELECT TOP 1 1 FROM LM_RoleMaster WHERE RoleName = @RoleName)
BEGIN
	INSERT INTO LM_RoleMaster ([RoleName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate])
	VALUES (@RoleName, 'Admin', GETDATE(), NULL, NULL)
END

/*------------User script -----------------*/
SET @RoleName = 'User'

IF NOT EXISTS(SELECT TOP 1 1 FROM LM_RoleMaster WHERE RoleName = @RoleName)
BEGIN
	INSERT INTO LM_RoleMaster ([RoleName], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate])
	VALUES (@RoleName, 'Admin', GETDATE(), NULL, NULL)
END

GO
