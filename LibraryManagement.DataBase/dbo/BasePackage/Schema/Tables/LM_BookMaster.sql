CREATE TABLE [dbo].[LM_BookMaster]
(
	[BookId] INT IDENTITY(1,1) NOT NULL,
	[BookName] NVARCHAR(200) NOT NULL, 
    [BookAuther] NVARCHAR(200) NULL, 
    [IsActive] BIT NOT NULL, 
    [PageCount] INT NULL, 
    [Category] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_LM_BookMaster_BookId] PRIMARY KEY CLUSTERED ([BookId] ASC)
)
