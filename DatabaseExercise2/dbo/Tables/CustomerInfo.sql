CREATE TABLE [dbo].[CustomerInfo] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_CustomerInfo_Id] DEFAULT (newid()) NOT NULL,
    [Firstname]    NVARCHAR (50)    NULL,
    [LastName]     NVARCHAR (50)    NULL,
    [Address]      NVARCHAR (255)   NULL,
    [PostalCode]   NVARCHAR (10)    NULL,
    [Phone]        NVARCHAR (50)    NULL,
    [City]         NVARCHAR (50)    NULL,
    [Province]     NVARCHAR (50)    NULL,
    [Country]      NVARCHAR (50)    NULL,
    [Email]        VARCHAR (256)    NULL,
    [MedicationId] UNIQUEIDENTIFIER NULL,
    [DeleteFlag]   BIT              NULL,
    CONSTRAINT [PK_CustomerInfo] PRIMARY KEY CLUSTERED ([Id] ASC)
);

