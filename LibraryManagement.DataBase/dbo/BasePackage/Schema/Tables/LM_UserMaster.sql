CREATE TABLE [dbo].[LM_UserMaster]
(
	[UserId] INT IDENTITY(1,1) NOT NULL, 
    [UserName] NVARCHAR(100) NOT NULL, 
    [Password] NVARCHAR(100) NOT NULL, 
    [FirstName] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(100) NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [RoleId] INT NOT NULL, 
    [Email] NVARCHAR(50) NULL, 
    [CreatedBy] NVARCHAR(MAX) NOT NULL DEFAULT('Admin'), 
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedBy] NVARCHAR(MAX) NULL, 
    [ModifiedDate] DATETIME NULL,
	CONSTRAINT [PK_LM_UserMaster_UserId] PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [FK_LM_UserMaster_LM_RoleMaster_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[LM_RoleMaster] ([RoleId])
)
