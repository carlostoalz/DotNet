USE master
GO

IF DB_ID('MillionAndUp') IS NULL
BEGIN
	CREATE DATABASE MillionAndUp
END
GO

USE MillionAndUp
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Owner')
BEGIN
	CREATE TABLE [dbo].[Owner]
	(
		IdOwner INT PRIMARY KEY NOT NULL IDENTITY,
		Name NVARCHAR(300) NOT NULL,
		Adress NVARCHAR(300) NOT NULL,
		Photo NVARCHAR(MAX) NOT NULL,
		Birthday DATE NOT NULL,
		Email NVARCHAR(200) NOT NULL UNIQUE,
		Passord NVARCHAR(200) NOT NULL
	)
END
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Property')
BEGIN
	CREATE TABLE [dbo].[Property]
	(
		IdProperty INT PRIMARY KEY NOT NULL IDENTITY,
		Name NVARCHAR(300) NOT NULL,
		Adress NVARCHAR(300) NOT NULL,
		Price MONEY NOT NULL,
		CodeInternal NVARCHAR(50) NOT NULL,
		Year INT NOT NULL,
		IdOwner INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Owner](IdOwner)
	)
END
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PropertyImage')
BEGIN
	CREATE TABLE [dbo].[PropertyImage]
	(
		IdPropertyImage INT PRIMARY KEY NOT NULL IDENTITY,
		IdProperty INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Property](IdProperty),
		[File] NVARCHAR(MAX) NOT NULL,
		Enabled BIT NOT NULL DEFAULT 1
	)
END
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PropertyTrace')
BEGIN
	CREATE TABLE [dbo].[PropertyTrace]
	(
		IdPropertyTrace INT PRIMARY KEY NOT NULL IDENTITY,
		DateSale DATETIME NOT NULL,
		Name NVARCHAR(300) NOT NULL,
		Value MONEY NOT NULL,
		Tax MONEY NOT NULL,
		IdProperty INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Property](IdProperty)
	)
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[PROD_Insert_Owner]
(
	 @Name	nvarchar(600)
	,@Adress	nvarchar(600)
	,@Photo	nvarchar(MAX)
	,@Birthday	date
	,@Email	nvarchar(400)
	,@Passord	nvarchar(400)
)
AS
BEGIN
	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	SET NOCOUNT ON;

    DECLARE @strMsg VARCHAR(2000)

	BEGIN TRY
		BEGIN TRAN
			
			INSERT INTO [dbo].[Owner]
			(Name, Adress, Photo, Birthday, Email, Passord)
			VALUES
			(@Name, @Adress, @Photo, @Birthday, @Email, @Passord)

		COMMIT TRAN

		SELECT SCOPE_IDENTITY()
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: [dbo].[PROD_Insert_Owner] ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE [dbo].[PROD_Get_Owner]
(
	@Email	nvarchar(400)
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	SET NOCOUNT ON;

	SELECT   IdOwner
			,Name
			,Adress
			,Photo
			,Birthday
			,Email
			,Passord
	FROM [dbo].[Owner]
END
GO

CREATE OR ALTER PROCEDURE [dbo].[PROD_Insert_Property]
(
	 @Name	nvarchar(600)
	,@Adress	nvarchar(600)
	,@Value	money
	,@Tax	money
	,@Year	int
	,@IdOwner	int
	,@PropertyImages nvarchar(MAX)
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	SET NOCOUNT ON;

	DECLARE @strMsg VARCHAR(2000),
			@IdProperty int,
			@Price money

	BEGIN TRY
		BEGIN TRAN
			SET @Price = @Value * @Tax + @Value

			INSERT INTO [dbo].[Property]
			(Name,Adress,Price,CodeInternal,Year,IdOwner)
			VALUES
			(@Name,@Adress,@Price,NEWID(),@Year,@IdOwner)

			SET @IdProperty = SCOPE_IDENTITY()


			IF LEN(@PropertyImages) > 0 
			BEGIN
				INSERT INTO [dbo].[PropertyImage] (IdProperty,[File],Enabled)
				SELECT @IdProperty, [File], Enabled
				FROM OPENJSON(@PropertyImages)
				WITH
				(
					[File] NVARCHAR(MAX) '$.File',
					Enabled BIT '$.Enabled'
				)
			END

			INSERT INTO [dbo].[PropertyTrace] 
			(IdProperty, DateSale, Name, Value, Tax)
			VALUES
			(@IdProperty, GETDATE(), @Name, @Value, @Tax)
		COMMIT TRAN

		SELECT IdProperty,Name,Adress,Price,CodeInternal,Year,IdOwner
		FROM [dbo].[Property]
		WHERE IdProperty = @IdProperty

		SELECT IdPropertyImage,IdProperty,[File],Enabled
		FROM [dbo].[PropertyImage]
		WHERE IdProperty = @IdProperty

		SELECT IdPropertyTrace,DateSale,Name,Value,Tax,IdProperty
		FROM [dbo].[PropertyTrace]
		WHERE IdProperty = @IdProperty
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: [dbo].[PROD_Insert_Property] ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)	
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE [dbo].[PROD_Get_Property]
(
	 @IdProperty int
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	SET NOCOUNT ON;

	SELECT IdProperty,Name,Adress,Price,CodeInternal,Year,IdOwner
	FROM [dbo].[Property]
	WHERE IdProperty = @IdProperty

	SELECT IdPropertyImage,IdProperty,[File],Enabled
	FROM [dbo].[PropertyImage]
	WHERE IdProperty = @IdProperty

	SELECT IdPropertyTrace,DateSale,Name,Value,Tax,IdProperty
	FROM [dbo].[PropertyTrace]
	WHERE IdProperty = @IdProperty
END
GO

CREATE OR ALTER PROCEDURE [dbo].[PROD_Get_Properties]
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	SET NOCOUNT ON;

	SELECT IdProperty,Name,Adress,Price,CodeInternal,Year,IdOwner
	FROM [dbo].[Property]

	SELECT IdPropertyImage,IdProperty,[File],Enabled
	FROM [dbo].[PropertyImage]

	SELECT IdPropertyTrace,DateSale,Name,Value,Tax,IdProperty
	FROM [dbo].[PropertyTrace]
END
GO