CREATE DATABASE GuestBook
GO

USE GuestBook

CREATE TABLE Records
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Text] NVARCHAR(300) NOT NULL,
	[Author] NVARCHAR(30) NOT NULL,
	[CreationDate] DATETIME NOT NULL,
	[UpdationDate] DATETIME NOT NULL
);
GO

INSERT INTO Records ([Text], [Author], [CreationDate], [UpdationDate])
VALUES ('My first record in Guest Book', 'Anonymus', GETDATE(), GETDATE()),
('My second record in Guest Book', 'Anonymus', SWITCHOFFSET(CONVERT(datetimeoffset, GETDATE()), '+04:00'), SWITCHOFFSET(CONVERT(datetimeoffset, GETDATE()), '+04:00'));

USE GuestBook
UPDATE Records SET [Text] = 'Edit from SQL', [UpdationDate] = GETDATE() WHERE Id = 1
