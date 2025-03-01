USE master 
GO 

CREATE DATABASE LibraryManagementDB
GO 

USE LibraryManagementDB

GO 

CREATE TABLE Users(
   [Id]                   INT             NOT NULL PRIMARY KEY IDENTITY(1,1),
   [Name]                 NVARCHAR(50)    NOT NULL,
   [Username]             NVARCHAR(50)    NOT NULL,
   [Fathername]           NVARCHAR(50)    NOT NULL,
   [Email]                NVARCHAR(100)   NOT NULL,
   [PasswordHash]         NVARCHAR(MAX)   NOT NULL,
   [Address]              NVARCHAR(MAX)   NOT NULL,
   [MobilePhone]          NVARCHAR(50)    NOT NULL,
   [CardNumber]           NVARCHAR(25)    NOT NULL,
   [BirthDay]             DATETIME2       NOT NULL,
   [DateOfEmployment]     DATETIME2       NOT NULL,
   [DateOfDismissal]      DATETIME2       NULL,
   [Note]                 NVARCHAR(MAX)   NOT NULL,
   [Gender]               SMALLINT        NOT NULL DEFAULT 1,
   [UserType]             SMALLINT        NOT NULL DEFAULT 1,
   [CreatedBy]            INT             NOT NULL,
   [IsDeleted]            BIT             NOT NULL DEFAULT 0,
   [UpdatedBy]            INT             NOT NULL,
   [DeletedBy]            INT             NULL,
   [CreatedDate]          DATETIME2       NOT NULL DEFAULT GETDATE(),
   [UpdatedDate]          DATETIME2       NULL,
   [DeletedDate]          DATETIME2       NULL
)    

GO 

CREATE TABLE Images(          
   [Id]                   INT             NOT NULL PRIMARY KEY IDENTITY(1,1),     
   [PhotoId]              NVARCHAR(255)   NOT NULL UNIQUE, 
   [ImagePath]            NVARCHAR(MAX)   NOT NULL
)

GO 

CREATE TABLE Books(
   [Id]                   INT             NOT NULL PRIMARY KEY IDENTITY(1,1),
   [Name]                 NVARCHAR(100)   NOT NULL,
   [Author]               NVARCHAR(100)   NOT NULL,
   [Price]                MONEY           NOT NULL CHECK([Price] > 0),
   [Image]                NVARCHAR(MAX)   NOT NULL,
   [Language]             SMALLINT        NOT NULL,
   [CreatedBy]            INT             NOT NULL,
   [IsDeleted]            BIT             NOT NULL DEFAULT 0,
   [UpdatedBy]            INT             NOT NULL,
   [DeletedBy]            INT             NULL,
   [ShowOnFirstScreen]    BIT             NOT NULL DEFAULT 1,
   [CreatedDate]          DATETIME2       NOT NULL DEFAULT GETDATE(),
   [UpdatedDate]          DATETIME2       NULL,
   [DeletedDate]          DATETIME2       NULL,
   [UserId]               INT             NULL FOREIGN KEY REFERENCES Users(Id),
   [CoverPhoto]           NVARCHAR(255)   NOT NULL,  
   CONSTRAINT FK_Books_CoverPhoto FOREIGN KEY (CoverPhoto) REFERENCES Images(PhotoId)
) 