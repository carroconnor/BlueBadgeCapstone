CREATE TABLE [dbo].[Conversation](
	[ConversationId] INT                IDENTITY (1, 1) NOT NULL,
	[RecipientId] INT                (1, 1) NOT NULL,
	[SenderId] INT                (1, 1) NOT NULL,
	[Title]       NVARCHAR (MAX)     NOT NULL,
    [Content]     NVARCHAR (MAX)     NOT NULL,
	[ModifiedUtc] DATETIMEOFFSET (7) NOT NULL,
	CONSTRAINT [PK_dbo.Conversation] PRIMARY KEY CLUSTERED ([ConversationId] ASC),
	CONSTRAINT [FK_dbo.Conversation_dbo.Message_SenderID] FOREIGN KEY ([SenderID]) REFERENCES [dbo].[Message] ([SenderID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Conversation_dbo.Message_RecipientID] FOREIGN KEY ([RecipientID]) REFERENCES [dbo].[Message] ([RecipientID]) ON DELETE CASCADE
);