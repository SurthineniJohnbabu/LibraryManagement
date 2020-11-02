CREATE TABLE [dbo].[LM_UserBookMapping]
(
	[UserBookMappingId] INT IDENTITY(1,1) NOT NULL, 
    [UserId] INT NOT NULL, 
    [BookId] INT NOT NULL, 
    [IsFavorite] BIT NULL, 
    [IsRead] BIT NULL,
    [BookReview] NVARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [CreatedBy] NVARCHAR(200) NOT NULL, 
    [ModifiedBy] NVARCHAR(200) NULL, 
    [ModifiedDate] DATETIME NULL,
	CONSTRAINT [PK_LM_UserBookMapping_Id] PRIMARY KEY CLUSTERED ([UserBookMappingId] ASC),
	CONSTRAINT [FK_LM_UserBookMapping_LM_UserMaster_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[LM_UserMaster] ([UserId]),
	CONSTRAINT [FK_LM_UserBookMapping_LM_BookMaster_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[LM_BookMaster] ([BookId])
)
