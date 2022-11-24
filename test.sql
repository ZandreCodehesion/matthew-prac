CREATE TABLE [Accounts] (
    [UserId] uniqueidentifier NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([UserId])
);
GO


CREATE TABLE [Authors] (
    [AuthorId] uniqueidentifier NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [AuthorName] nvarchar(max) NOT NULL,
    [ActiveFrom] datetime2 NOT NULL,
    [ActiveTo] datetime2 NOT NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([AuthorId])
);
GO


