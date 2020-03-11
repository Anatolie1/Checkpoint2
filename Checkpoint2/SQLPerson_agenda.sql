DROP DATABASE IF EXISTS Checkpoint2
GO

CREATE DATABASE Checkpoint2
GO

USE Checkpoint2
GO

CREATE TABLE [Cursus] ([id_cursus] INT PRIMARY KEY IDENTITY(1,1),
					   [name] VARCHAR(50) NOT NULL,
					   [startdate] DATE NOT NULL,
					   [enddate] DATE NOT NULL,);

CREATE TABLE [Quest] ([id_quest] INT PRIMARY KEY IDENTITY(1,1),
					  [name] VARCHAR(50) NOT NULL,
					  [date] DATE NOT NULL,
					  [description] TEXT,
					  [FK_id_cursus] INT FOREIGN KEY REFERENCES [Cursus](id_cursus)
					  ON UPDATE CASCADE ON DELETE CASCADE)

CREATE TABLE [Person] ([id_person] INT PRIMARY KEY IDENTITY(1,1),
					   [name] VARCHAR(50) NOT NULL,
					   [superviser] INT FOREIGN KEY REFERENCES [Person](id_person),
					   [FK_id_cursus] INT FOREIGN KEY REFERENCES [Cursus](id_cursus)
						ON UPDATE CASCADE ON DELETE CASCADE)
GO

DROP PROCEDURE IF EXISTS sp_fill_tables
GO

CREATE PROCEDURE sp_fill_tables
AS
	BEGIN		
		INSERT INTO [Cursus] ([name], [startdate],[enddate] )
					VALUES  ('PHP','2020-01-01', '2020-05-30'),
							('JAVA', '2020-03-01', '2020-09-30'),
							('HTML/CSS', '2020-05-01', '2020-06-30')
-- Fill table Quest and Person
		DECLARE @Teacher VARCHAR(50) = 'Teacher'
		DECLARE @Student VARCHAR(50) = 'Student'
		DECLARE @Number INT = 1
		DECLARE @Cursus_id INT
		DECLARE @Cursus_name VARCHAR(60)
		DECLARE @Startdate DATE
		DECLARE @Enddate DATE
		DECLARE Cursus_Cursor CURSOR SCROLL FOR
			SELECT [id_cursus], [name], [startdate], [enddate]   FROM [Cursus]
		OPEN Cursus_Cursor
		FETCH FIRST FROM Cursus_Cursor INTO @Cursus_id, @Cursus_name, @Startdate, @Enddate 
			WHILE @@FETCH_STATUS = 0
				BEGIN
					DECLARE @Count INT  = 1
					WHILE @Startdate < @Enddate
						BEGIN
							INSERT INTO [Quest] ([name], [date], [FK_id_cursus])
										VALUES (@Cursus_name + CONVERT(VARCHAR, @Count), @Startdate, @Cursus_id)
							SET @Startdate = DATEADD(d, 2, @Startdate)
							SET @Count = @Count + 1
						END

					SET @Teacher = @Teacher + @Cursus_name
					INSERT INTO [Person] ([name], [superviser], [FK_id_cursus])
										VALUES (@Teacher, 1 , @Cursus_id)

					WHILE (SELECT COUNT(*) FROM [Person] WHERE [FK_id_cursus] = @Cursus_id  ) < 11
						BEGIN
						DECLARE @Teacher_id INT = (SELECT id_person FROM [Person] WHERE [name] = @Teacher)
							INSERT INTO [Person] ([name], [superviser], [FK_id_cursus])
										VALUES (@Student + CONVERT(VARCHAR, @Number), @Teacher_id , @Cursus_id)
							SET @Number = @Number + 1
						END
						FETCH NEXT FROM Cursus_Cursor INTO @Cursus_id, @Cursus_name, @Startdate, @Enddate
				END
		CLOSE Cursus_Cursor
		DEALLOCATE Cursus_Cursor
	END
GO

EXECUTE sp_fill_tables
GO

DROP PROCEDURE IF EXISTS sp_Event
GO

CREATE PROCEDURE sp_Event
@personId INT,
@startperiod DATE = '2019',
@endperiod DATE = '2021'
AS
	BEGIN
		SELECT  [Quest].name, [Quest].date FROM [Cursus]
		INNER JOIN [Person] ON [Cursus].id_cursus = [Person].FK_id_cursus
		INNER JOIN [Quest] ON [Cursus].id_cursus = [Quest].FK_id_cursus
		WHERE [Person].id_person = @personId AND [Quest].[date] BETWEEN @startperiod AND @endperiod
	END
GO

EXECUTE sp_Event 3, '2020-02-14', '2020-05-01'
GO

DROP PROCEDURE IF EXISTS sp_Calendar_Cursus
GO

CREATE PROCEDURE sp_Calendar_Cursus
@startperiod DATE = '2019',
@endperiod DATE = '2021'
AS
	BEGIN
		SELECT  [Cursus].[name],[Quest].[name], [Quest].[date] FROM [Cursus]
		INNER JOIN [Quest] ON [Cursus].id_cursus = [Quest].FK_id_cursus
		WHERE [Quest].[date] BETWEEN @startperiod AND @endperiod
		ORDER BY [Quest].[date]
	END
GO

EXECUTE sp_Calendar_Cursus '2020-04-14', '2020-05-30'
GO

DROP PROCEDURE IF EXISTS sp_Cursus_Students
GO

CREATE PROCEDURE sp_Cursus_Students
@CursusName VARCHAR(50)
AS
	BEGIN
		SELECT  [Person].[name] FROM [Cursus]
		INNER JOIN [Person] ON [Cursus].id_cursus = [Person].FK_id_cursus
		WHERE [Cursus].[name] = @CursusName AND [Person].[name] LIKE 'S%'
	END
GO

EXECUTE sp_Cursus_Students 'JAVA'
GO

DROP PROCEDURE IF EXISTS sp_Teacher_Students
GO

CREATE PROCEDURE sp_Teacher_Students
@TeacherName VARCHAR(50)
AS
	BEGIN
		SELECT  [pers].[name] FROM [Person]
		INNER JOIN [Person] AS [pers] ON [Person].id_person = [pers].[superviser]
		WHERE [Person].[name] = @TeacherName AND [pers].[name] LIKE 'S%'
	END
GO

EXECUTE sp_Teacher_Students 'TeacherPHPJAVA'
GO

DROP PROCEDURE IF EXISTS sp_Quest_Expedition
GO

CREATE PROCEDURE sp_Quest_Expedition
@CursusName VARCHAR(50),
@startperiod DATE = '2019',
@endperiod DATE = '2021'
AS
	BEGIN
		SELECT  [Quest].[name], [Quest].[date] FROM [Cursus]
		INNER JOIN [Quest] ON [Cursus].id_cursus = [Quest].FK_id_cursus
		WHERE [Cursus].[name] = @CursusName AND ([Quest].[date] BETWEEN @startperiod AND @endperiod)
	END
GO

EXECUTE sp_Quest_Expedition 'JAVA', '2020-05-23', '2020-05-30'
GO



SELECT * FROM [Cursus]
SELECT * FROM [Quest]
SELECT * FROM [Person]