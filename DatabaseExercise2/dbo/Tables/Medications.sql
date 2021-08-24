CREATE TABLE [dbo].[Medications] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_Medications_Id] DEFAULT (newid()) NOT NULL,
    [Medication] VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_Medications] PRIMARY KEY CLUSTERED ([Id] ASC)
);

