USE [master]
GO


-- Create the "rescuepet" database
CREATE DATABASE [PetDB]
GO

USE [PetDB]
GO


-- Create the "Pet" table
CREATE TABLE [dbo].[Location] (
    [LocationID] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [LocationName] VARCHAR(50)
)

CREATE TABLE [dbo].[Type] (
    [TypeID] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [TypeName] VARCHAR(50)
)
CREATE TABLE [dbo].[Breed] (
    [BreedID] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	 [TypeID] INT FOREIGN KEY REFERENCES [Type]([TypeID]),
    [BreedName] VARCHAR(50)
)

CREATE TABLE [dbo].[Pet] (
    [petID] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,

    
    [adoptionStatus] VARCHAR(50),
    [User] VARCHAR(50),
    [Name] VARCHAR(100),
    [Story] TEXT,
    [PostedBy] VARCHAR(100),
    [LocationID] INT FOREIGN KEY REFERENCES [Location]([LocationID]),
     [BreedID] INT FOREIGN KEY REFERENCES [Breed]([BreedID]),
    [Type] VARCHAR(50),
    [Weight] DECIMAL(10, 2),
    [Age] INT,
    [Img] VARCHAR(MAX),
    [Gender] VARCHAR(10),
	[AdoptionDate] VarChar(30)
)
-- Create the "User" table
CREATE TABLE [dbo].[User] (
    [UserID] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [FirstName] VARCHAR(50),
    [LastName] VARCHAR(50),
    [PhoneNumber] VARCHAR(20)
)
CREATE TABLE [dbo].[Adoption] (
    [AdoptionID] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [UserID] INT FOREIGN KEY REFERENCES [User]([UserID]),
    [PetID] INT FOREIGN KEY REFERENCES [Pet]([petID]),
    [adoptionDate] DATE
)

-- Create the "Donation" table
CREATE TABLE [dbo].[Donation] (
    [DonationID] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [UserID] INT FOREIGN KEY REFERENCES [User]([UserID]),
    [DonationAmount] DECIMAL(10, 2),
    [DonationDate] DATE
)



-- Insert records into the "Location" table
INSERT INTO [dbo].[Location] ([LocationName])
VALUES
    ('Soweto'),
    ('Glenwood'),
    ('Kimberly');
-- Insert records into the "Type" table

INSERT INTO [dbo].[Type] ([TypeName])
VALUES
    ('Snakes'),
    ('Cat'),
	 ('Farm Animals'),
	 ('Horse');


-- Insert records into the "Breed" table
INSERT INTO [dbo].[Breed] ([TypeID], [BreedName])
VALUES
    (1, 'Python'),
    (1, 'Black Mamba'),
	(1, 'Puff Adder'),
    (2, 'Russian Cat'),
    (2, 'Siberian Cat'),
	(2, 'Burmese Cat'),
    (3, 'Cow'),
	(3, 'Goat'),
    (3, 'Pig'),
	(4, 'Pony'),
	(4, 'Trojan'),
    (4, 'Morgan');

-- Insert records into the "Pet" table

INSERT INTO [dbo].[Pet] ([adoptionStatus], [User], [Name], [Story], [PostedBy], [LocationID], [BreedID], [Type], [Weight], [Age], [Img], [Gender],[AdoptionDate])
VALUES
      
    ('Available', 'Linda Brown', 'Bluey', 'A colorful cat that loves to play.', 'Michael Johnson', 2, 4, 'Cat', 0.6, 2, 'bluey.jpeg', 'Female', NULL),
    ('Available', 'Andrew Jones', 'Nemo', 'A playful Cat lost in the ocean.', 'John Doe', 3, 5, 'Cat', 0.4, 1, 'cat.jpeg', 'Female', NULL);
    
INSERT INTO [dbo].[User] ([FirstName], [LastName], [PhoneNumber])
VALUES
    ('Sizwe', 'Williams', '+27 11 123 4567'),
    ('Tony', 'Brown', '+27 21 987 2243'),
    ('Thami', 'Jones', '+27 12 335 5555'),
    ('Michelle', 'Davis', '+27 31 399 3333'),
    ('David', 'Miller', '+27 41 444 4444'),
    ('Wade', 'Wilson', '+27 51 555 5555'),
    ('Brian', 'Harris', '+27 32 666 6666'),
    ('Liam', 'Clark', '+27 21 777 5577');

-- Insert records into the "Adoption" table


-- Insert records into the "Donation" table (assuming you have some sample donation data)
INSERT INTO [dbo].[Donation] ([UserID], [DonationAmount], [DonationDate])
VALUES
    (1, 50.00, '2023-08-10'),
    (2, 75.00, '2023-07-25'),
    (3, 100.00, '2023-09-01');


