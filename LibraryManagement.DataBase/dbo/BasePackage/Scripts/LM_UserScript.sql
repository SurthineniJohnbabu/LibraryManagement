DECLARE @UserName NVARCHAR(100),
		@Password NVARCHAR(100),
		@FirstName NVARCHAR(100),
		@LastName NVARCHAR(100),
		@IsActive BIT,
		@RoleId INT,
		@Email NVARCHAR(50)

/*------------------------Admin script--------------------------*/
SET @UserName = 'admin@gmail.com'
SET @Password = 'Password@1234'
SET @FirstName = 'Admin'
SET @LastName = 'Admin'
SET @IsActive = 1
SET @Email = 'admin@gmail.com'
SET @RoleId = (SELECT RoleId FROM LM_RoleMaster WHERE RoleName = 'Admin')

IF NOT EXISTS(SELECT TOP 1 1 FROM LM_UserMaster WHERE UserName = @UserName)
BEGIN
	INSERT INTO LM_UserMaster ([UserName], [Password], [FirstName], [LastName], [IsActive], [RoleId], [Email], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate])
	VALUES (@UserName, @Password, @FirstName, @LastName, @IsActive, @RoleId, @Email, 'Admin', GETDATE(), NULL, NULL)
END

/*------------------------User Script---------------------------*/

SET @UserName = 'John@gmail.com'
SET @Password = 'Password@1234'
SET @FirstName = 'John'
SET @LastName = 'Babu'
SET @IsActive = 1
SET @Email = 'John@gmail.com'
SET @RoleId = (SELECT RoleId FROM LM_RoleMaster WHERE RoleName = 'User')

IF NOT EXISTS(SELECT TOP 1 1 FROM LM_UserMaster WHERE UserName = @UserName)
BEGIN
	INSERT INTO LM_UserMaster ([UserName], [Password], [FirstName], [LastName], [IsActive], [RoleId], [Email], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate])
	VALUES (@UserName, @Password, @FirstName, @LastName, @IsActive, @RoleId, @Email, 'Admin', GETDATE(), NULL, NULL)
END

SET @UserName = 'Catherine@gmail.com'
SET @Password = 'Password@1234'
SET @FirstName = 'Catherine'
SET @LastName = 'Cath'
SET @IsActive = 1
SET @Email = 'Catherine@gmail.com'
SET @RoleId = (SELECT RoleId FROM LM_RoleMaster WHERE RoleName = 'User')

IF NOT EXISTS(SELECT TOP 1 1 FROM LM_UserMaster WHERE UserName = @UserName)
BEGIN
	INSERT INTO LM_UserMaster ([UserName], [Password], [FirstName], [LastName], [IsActive], [RoleId], [Email], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate])
	VALUES (@UserName, @Password, @FirstName, @LastName, @IsActive, @RoleId, @Email, 'Admin', GETDATE(), NULL, NULL)
END

SET @UserName = 'Betsy@gmail.com'
SET @Password = 'Password@1234'
SET @FirstName = 'Betsy'
SET @LastName = 'Bet'
SET @IsActive = 1
SET @Email = 'Betsy@gmail.com'
SET @RoleId = (SELECT RoleId FROM LM_RoleMaster WHERE RoleName = 'User')

IF NOT EXISTS(SELECT TOP 1 1 FROM LM_UserMaster WHERE UserName = @UserName)
BEGIN
	INSERT INTO LM_UserMaster ([UserName], [Password], [FirstName], [LastName], [IsActive], [RoleId], [Email], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate])
	VALUES (@UserName, @Password, @FirstName, @LastName, @IsActive, @RoleId, @Email, 'Admin', GETDATE(), NULL, NULL)
END

GO
