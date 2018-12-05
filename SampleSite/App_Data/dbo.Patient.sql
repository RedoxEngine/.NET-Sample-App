CREATE TABLE [dbo].[Patient]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [MRN] NCHAR(10) NOT NULL, 
    [First] VARCHAR(50) NOT NULL, 
    [Last] VARCHAR(50) NOT NULL, 
    [BirthDate] DATE NOT NULL, 
    [Gender] NCHAR(1) NULL, 
    [Email] VARCHAR(MAX) NOT NULL, 
    [HomePhone] NUMERIC(10) NULL, 
    [CellPhone] NUMERIC(10) NULL, 
    [AddressLine1] VARCHAR(50) NULL,  
    [AddressLine2] VARCHAR(50) NULL, 
    [City] NCHAR(10) NULL, 
    [State] NCHAR(10) NULL, 
    [ZipCode] NCHAR(10) NULL, 
    [SocialSecurityNumber] NUMERIC(9) NOT NULL
)
