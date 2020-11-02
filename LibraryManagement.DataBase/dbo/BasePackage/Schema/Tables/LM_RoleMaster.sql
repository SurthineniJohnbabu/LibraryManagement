CREATE TABLE [dbo].[LM_RoleMaster]
(
	[RoleId] INT IDENTITY(1,1) NOT NULL, 
    [RoleName] NVARCHAR(50) NOT NULL, 
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NULL, 
    [ModifiedDate] DATETIME NULL, 
    CONSTRAINT [PK_LM_RoleMaster_RoleId] PRIMARY KEY CLUSTERED ([RoleId] ASC)
)
