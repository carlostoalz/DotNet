USE master

--CREATE DATABASE PruebaOneLink

USE PruebaOneLink
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND TYPE IN (N'U'))
BEGIN
	CREATE TABLE Usuarios (
		IdUsaurio NUMERIC NOT NULL PRIMARY KEY IDENTITY,
		NombreUsuario NVARCHAR(100) NOT NULL,
		Contrasena NVARCHAR(100) NOT NULL,
		Nombre NVARCHAR(500) NOT NULL,
		Email NVARCHAR(500) NOT NULL,
		Estado BIT NOT NULL,
		FechaCreacion DATETIME NOT NULL,
		FechaModificacion DATETIME NULL
	)
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposDocumento]') AND TYPE IN (N'U'))
BEGIN
	CREATE TABLE TiposDocumento 
	(
		IdTipoDocumento NUMERIC PRIMARY KEY NOT NULL IDENTITY,
		NombreTipoDocumento NVARCHAR(100) NOT NULL,
		UsuarioCreacion NUMERIC NOT NULL,
		FechaCreacion DATETIME NOT NULL,
		UsuarioModificacion NUMERIC NULL,
		FechaModificacion DATETIME NULL
	)

	ALTER TABLE TiposDocumento ADD FOREIGN KEY (UsuarioCreacion) REFERENCES Usuarios(IdUsaurio)
	ALTER TABLE TiposDocumento ADD FOREIGN KEY (UsuarioModificacion) REFERENCES Usuarios(IdUsaurio)
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Areas]') AND TYPE IN (N'U'))
BEGIN
	CREATE TABLE Areas (
		IdArea NUMERIC PRIMARY KEY NOT NULL IDENTITY,
		NombreArea NVARCHAR(200) NOT NULL,
		UsuarioCreacion NUMERIC NOT NULL,
		FechaCreacion DATETIME NOT NULL,
		UsuarioModificacion NUMERIC NULL,
		FechaModificacion DATETIME NULL
	)

	ALTER TABLE Areas ADD FOREIGN KEY (UsuarioCreacion) REFERENCES Usuarios(IdUsaurio)
	ALTER TABLE Areas ADD FOREIGN KEY (UsuarioModificacion) REFERENCES Usuarios(IdUsaurio)
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SubAreas]') AND TYPE IN (N'U'))
BEGIN
	CREATE TABLE SubAreas (
		IdSubArea NUMERIC PRIMARY KEY NOT NULL IDENTITY,
		NombreSubArea NVARCHAR(200) NOT NULL,
		IdArea NUMERIC NOT NULL,
		UsuarioCreacion NUMERIC NOT NULL,
		FechaCreacion DATETIME NOT NULL,
		UsuarioModificacion NUMERIC NULL,
		FechaModificacion DATETIME NULL
	)

	ALTER TABLE SubAreas ADD FOREIGN KEY (IdArea) REFERENCES Areas(IdArea)
	ALTER TABLE SubAreas ADD FOREIGN KEY (UsuarioCreacion) REFERENCES Usuarios(IdUsaurio)
	ALTER TABLE SubAreas ADD FOREIGN KEY (UsuarioModificacion) REFERENCES Usuarios(IdUsaurio)
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Empleados]') AND TYPE IN (N'U'))
BEGIN
	CREATE TABLE Empleados (
		IdEmmpleado NUMERIC PRIMARY KEY NOT NULL IDENTITY,
		IdTipoDocumento NUMERIC NOT NULL,
		NumeroDocumento NVARCHAR(50) NOT NULL,
		Nombres NVARCHAR(500) NOT NULL,
		Apellidos NVARCHAR(500) NOT NULL,
		IdArea NUMERIC NOT NULL,
		IdSubArea NUMERIC NOT NULL,
		UsuarioCreacion NUMERIC NOT NULL,
		FechaCreacion DATETIME NOT NULL,
		UsuarioModificacion NUMERIC NULL,
		FechaModificacion DATETIME NULL
	)

	ALTER TABLE Empleados ADD FOREIGN KEY (IdTipoDocumento) REFERENCES TiposDocumento(IdTipoDocumento)
	ALTER TABLE Empleados ADD FOREIGN KEY (IdArea) REFERENCES Areas(IdArea)
	ALTER TABLE Empleados ADD FOREIGN KEY (IdSubArea) REFERENCES SubAreas(IdSubArea)
	ALTER TABLE Empleados ADD FOREIGN KEY (UsuarioCreacion) REFERENCES Usuarios(IdUsaurio)
	ALTER TABLE Empleados ADD FOREIGN KEY (UsuarioModificacion) REFERENCES Usuarios(IdUsaurio)
END
GO


--INSERT INTO Usuarios
--(
--	NombreUsuario,
--	Contrasena,
--	Nombre,
--	Email,
--	Estado,
--	FechaCreacion,
--	FechaModificacion
--)
--VALUES
--(
--	'carlostoalz',
--	'contigente3del9',
--	'Carlos Andres Tobon Alzate',
--	'carlostoalz@hotmail.com',
--	1,
--	GETDATE(),
--	NULL
--)

--Procedimientos

--dbo.Usuarios
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Usuarios_Login]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Usuarios_Login] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_Usuarios_Login]
	@NombreUsuario nvarchar(200),
	@Contrasena nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	DECLARE @strMessage NVARCHAR(2000) = ''

	IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.Usuarios WHERE NombreUsuario = @NombreUsuario)
	BEGIN
		SET @strMessage += 'Usuario no existe \n'
		RAISERROR(@strMessage, 16, 1)
	END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.Usuarios WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contrasena)
		BEGIN
			SET @strMessage += 'Contraseña errada \n'
			RAISERROR(@strMessage, 16, 1)
		END
	END

	SELECT CONVERT(BIT,1)

	SET NOCOUNT OFF;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Usuarios_Actualizar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Usuarios_Actualizar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_Usuarios_Actualizar]
	@NombreUsuario nvarchar(200),
	@Contrasena nvarchar(200),
	@Nombre nvarchar(1000),
	@Email nvarchar(1000)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	DECLARE @strMsg VARCHAR(2000),
			@IdUsaurio NUMERIC

	BEGIN TRY 
		BEGIN TRAN
			
			INSERT INTO dbo.Usuarios 
			(NombreUsuario, Contrasena, Nombre, Email, Estado, FechaCreacion)
			VALUES	
			(@NombreUsuario, @Contrasena, @Nombre, @Email, 1, GETDATE())

			SET @IdUsaurio = SCOPE_IDENTITY()

		COMMIT TRAN

		SELECT @IdUsaurio

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: dbo.usp_Usuarios_Actualizar ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF;
END
GO

--dbo.TiposDocumento
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_TiposDocumento_Obtener]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_TiposDocumento_Obtener] AS')
END
GO

ALTER PROCEDURE usp_TiposDocumento_Obtener
AS
BEGIN	

	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT 
		 IdTipoDocumento
		,NombreTipoDocumento
		,UsuarioCreacion
		,FechaCreacion
		,UsuarioModificacion
		,FechaModificacion
	FROM TiposDocumento

	SET NOCOUNT OFF;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_TiposDocumento_Seleccionar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_TiposDocumento_Seleccionar] AS')
END
GO

ALTER PROCEDURE usp_TiposDocumento_Seleccionar
	@IdTipoDocumento numeric
AS
BEGIN	

	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT 
		 IdTipoDocumento
		,NombreTipoDocumento
		,UsuarioCreacion
		,FechaCreacion
		,UsuarioModificacion
		,FechaModificacion
	FROM TiposDocumento
	WHERE IdTipoDocumento = @IdTipoDocumento	

	SET NOCOUNT OFF;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_TiposDocumento_Actualizar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_TiposDocumento_Actualizar] AS')
END
GO

ALTER PROCEDURE usp_TiposDocumento_Actualizar
	@IdTipoDocumento numeric,
	@NombreTipoDocumento nvarchar(200),
	@Usuario numeric
AS
BEGIN
	
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	DECLARE @strMsg VARCHAR(2000)

	BEGIN TRY 
		BEGIN TRAN
			
			IF @IdTipoDocumento <= 0
			BEGIN 
				INSERT INTO dbo.TiposDocumento 
				(NombreTipoDocumento,UsuarioCreacion,FechaCreacion)
				VALUES	
				(@NombreTipoDocumento, @Usuario, GETDATE())

				SET @IdTipoDocumento = SCOPE_IDENTITY()
			END
			ELSE
			BEGIN
				UPDATE dbo.TiposDocumento
					SET NombreTipoDocumento = @NombreTipoDocumento,
						UsuarioModificacion = @Usuario,
						FechaModificacion = GETDATE()
				WHERE IdTipoDocumento = @IdTipoDocumento
			END

		COMMIT TRAN

		SELECT @IdTipoDocumento

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: dbo.usp_TiposDocumento_Actualizar ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_TiposDocumento_Eliminar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_TiposDocumento_Eliminar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_TiposDocumento_Eliminar]
	@IdTipoDocumento numeric
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  

	DELETE FROM dbo.TiposDocumento
	WHERE IdTipoDocumento = @IdTipoDocumento
END
GO

--[dbo].[Areas]
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Areas_Obtener]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Areas_Obtener] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_Areas_Obtener]
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT 
		 IdArea
		,NombreArea
		,UsuarioCreacion
		,FechaCreacion
		,UsuarioModificacion
		,FechaModificacion
	FROM [dbo].[Areas]

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Areas_Seleccionar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Areas_Seleccionar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_Areas_Seleccionar]
	@IdArea NUMERIC
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT 
		 IdArea
		,NombreArea
		,UsuarioCreacion
		,FechaCreacion
		,UsuarioModificacion
		,FechaModificacion
	FROM [dbo].[Areas]
	WHERE IdArea = @IdArea

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Areas_Actualizar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Areas_Actualizar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_Areas_Actualizar] 
	@IdArea	numeric,
	@NombreArea	nvarchar(400),
	@Usuario	numeric
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	DECLARE @strMsg VARCHAR(2000)

	BEGIN TRY
		BEGIN TRAN
			
			IF @IdArea <= 0
			BEGIN
				INSERT INTO [dbo].[Areas]
				(NombreArea,UsuarioCreacion,FechaCreacion)
				VALUES
				(@NombreArea, @Usuario, GETDATE())

				SET @IdArea = SCOPE_IDENTITY()
			END
			ELSE
			BEGIN
				UPDATE [dbo].[Areas]
					SET NombreArea = @NombreArea,
						UsuarioModificacion = @Usuario,
						FechaModificacion = GETDATE()
				WHERE IdArea = @IdArea
			END

		COMMIT TRAN

		SELECT @IdArea
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: dbo.usp_Areas_Actualizar ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Areas_Eliminar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Areas_Eliminar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_Areas_Eliminar]
	@IdArea NUMERIC
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  

	IF EXISTS (SELECT TOP 1 1 FROM [dbo].[SubAreas] WHERE IdArea = @IdArea)
	BEGIN
		RAISERROR('El area que está intentado eliminar tiene subareas aignadas, por lo tanto dirijase a la pantalla de subareas, elimine las subareas asignadas al area que desea eliminar, e intente de nuevo.',16,1)
	END
	ELSE
	BEGIN
		DELETE FROM [dbo].[Areas]
		WHERE IdArea = @IdArea
	END
END
GO

--[dbo].[SubAreas]
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SubAreas_Obtener]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_SubAreas_Obtener] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_SubAreas_Obtener]
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT 
		 IdSubArea
		,NombreSubArea
		,IdArea
		,UsuarioCreacion
		,FechaCreacion
		,UsuarioModificacion
		,FechaModificacion
	FROM [dbo].[SubAreas]

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SubAreas_Seleccionar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_SubAreas_Seleccionar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_SubAreas_Seleccionar]
	@IdSubArea NUMERIC
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT 
		 IdSubArea
		,NombreSubArea
		,IdArea
		,UsuarioCreacion
		,FechaCreacion
		,UsuarioModificacion
		,FechaModificacion
	FROM [dbo].[SubAreas]
	WHERE IdSubArea = @IdSubArea

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SubAreas_Buscar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_SubAreas_Buscar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_SubAreas_Buscar]
	@IdArea NUMERIC
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT 
		 IdSubArea
		,NombreSubArea
		,IdArea
		,UsuarioCreacion
		,FechaCreacion
		,UsuarioModificacion
		,FechaModificacion
	FROM [dbo].[SubAreas]
	WHERE IdArea = @IdArea

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SubAreas_Actualizar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_SubAreas_Actualizar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_SubAreas_Actualizar]
	@IdSubArea	numeric,
	@NombreSubArea	nvarchar(400),
	@IdArea	numeric,
	@Usuario	numeric
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	DECLARE @strMsg VARCHAR(2000)

	BEGIN TRY
		BEGIN TRAN
			IF @IdSubArea <= 0
			BEGIN
				INSERT INTO [dbo].[SubAreas]
				(NombreSubArea,IdArea,UsuarioCreacion,FechaCreacion)
				VALUES
				(@NombreSubArea,@IdArea,@Usuario,GETDATE())

				SET @IdSubArea = SCOPE_IDENTITY()
			END
			ELSE
			BEGIN
				UPDATE [dbo].[SubAreas]
					SET NombreSubArea = @NombreSubArea,
						IdArea = @IdArea,
						UsuarioModificacion = @Usuario,
						FechaModificacion = GETDATE()
				WHERE IdSubArea = @IdSubArea
			END
		COMMIT TRAN

		SELECT @IdSubArea
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: dbo.usp_SubAreas_Actualizar ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SubAreas_Eliminar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_SubAreas_Eliminar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_SubAreas_Eliminar] 
	@IdSubArea NUMERIC
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  

	DELETE FROM [dbo].[SubAreas]
	WHERE IdSubArea = @IdSubArea
END
GO

--[dbo].[Empleados]
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Empleados_Obtener]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Empleados_Obtener] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_Empleados_Obtener]
	@PaginaActual int = 1,
	@TamanoPagina int = 10,
	@termino nvarchar(1000) = NULL,
	@cantPaginas int output
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	DECLARE @cantRegistros int

	DECLARE @tmpEmpleados TABLE 
	(
		 IdEmmpleado NUMERIC
		,IdTipoDocumento NUMERIC
		,NombreTipoDocumento NVARCHAR(200)
		,NumeroDocumento NVARCHAR(100)
		,Nombres NVARCHAR(1000)
		,Apellidos NVARCHAR(1000)
		,IdArea NUMERIC
		,NombreArea NVARCHAR(400)
		,IdSubArea NUMERIC
		,NombreSubArea NVARCHAR(1000)
		,UsuarioCreacion NUMERIC
		,FechaCreacion DATETIME
		,UsuarioModificacion NUMERIC
		,FechaModificacion DATETIME
		,[row] INT
	)

	INSERT INTO @tmpEmpleados(
	 IdEmmpleado
	,IdTipoDocumento
	,NombreTipoDocumento
	,NumeroDocumento
	,Nombres
	,Apellidos
	,IdArea
	,NombreArea
	,IdSubArea
	,NombreSubArea
	,UsuarioCreacion
	,FechaCreacion
	,UsuarioModificacion
	,FechaModificacion
	,[row]
	)
	SELECT 
			 TMP.IdEmmpleado
			,TMP.IdTipoDocumento
			,TMP.NombreTipoDocumento
			,TMP.NumeroDocumento
			,TMP.Nombres
			,TMP.Apellidos
			,TMP.IdArea
			,TMP.NombreArea
			,TMP.IdSubArea
			,TMP.NombreSubArea
			,TMP.UsuarioCreacion
			,TMP.FechaCreacion
			,TMP.UsuarioModificacion
			,TMP.FechaModificacion
			,TMP.[row]
	FROM (
		SELECT
			 A.IdEmmpleado
			,A.IdTipoDocumento
			,B.NombreTipoDocumento
			,A.NumeroDocumento
			,A.Nombres
			,A.Apellidos
			,A.IdArea
			,C.NombreArea
			,A.IdSubArea
			,D.NombreSubArea
			,A.UsuarioCreacion
			,A.FechaCreacion
			,A.UsuarioModificacion
			,A.FechaModificacion
			,ROW_NUMBER() OVER (ORDER BY A.IdEmmpleado) AS [row]
		FROM [dbo].[Empleados] A
		INNER JOIN [dbo].[TiposDocumento] B
			ON A.IdTipoDocumento = B.IdTipoDocumento
		INNER JOIN [dbo].[Areas] C
			ON A.IdArea = C.IdArea
		INNER JOIN [dbo].[SubAreas] D
			ON A.IdSubArea = D.IdSubArea
		WHERE ( A.NumeroDocumento LIKE ( CASE WHEN @termino IS NULL THEN A.NumeroDocumento ELSE '%' + @termino + '%' END ) )
		OR ( (A.Nombres + ' ' + A.Apellidos) LIKE ( CASE WHEN @termino IS NULL THEN ( A.Nombres + ' ' + A.Apellidos ) ELSE '%' + @termino + '%' END ) )
		OR ( B.NombreTipoDocumento LIKE ( CASE WHEN @termino IS NULL THEN B.NombreTipoDocumento ELSE '%' + @termino + '%' END ) )
		OR ( C.NombreArea LIKE ( CASE WHEN @termino IS NULL THEN C.NombreArea ELSE '%' + @termino + '%' END ) )
		OR ( D.NombreSubArea LIKE ( CASE WHEN @termino IS NULL THEN D.NombreSubArea ELSE '%' + @termino + '%' END ) )

	) AS TMP
	

	SET @cantRegistros = (SELECT COUNT(*) FROM @tmpEmpleados)

	SELECT *
	FROM @tmpEmpleados TMP
	WHERE ( TMP.row BETWEEN (((@PaginaActual - 1) * @TamanoPagina)+1) AND (@PaginaActual * @TamanoPagina ))

	IF ( @cantRegistros % @TamanoPagina ) > 0
	BEGIN
		SET @cantPaginas = (@cantRegistros / @TamanoPagina ) + 1
	END
	ELSE
	BEGIN
		SET @cantPaginas = (@cantRegistros / @TamanoPagina )
	END

	PRINT @cantPaginas

	RETURN @cantPaginas

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Empleados_Seleccionar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Empleados_Seleccionar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_Empleados_Seleccionar]
 @IdEmmpleado NUMERIC
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT
		 A.IdEmmpleado
		,A.IdTipoDocumento
		,B.NombreTipoDocumento
		,A.NumeroDocumento
		,A.Nombres
		,A.Apellidos
		,A.IdArea
		,C.NombreArea
		,A.IdSubArea
		,D.NombreSubArea
		,A.UsuarioCreacion
		,A.FechaCreacion
		,A.UsuarioModificacion
		,A.FechaModificacion
	FROM [dbo].[Empleados] a
	INNER JOIN [dbo].[TiposDocumento] B
		ON A.IdTipoDocumento = B.IdTipoDocumento
	INNER JOIN [dbo].[Areas] C
		ON A.IdArea = C.IdArea
	INNER JOIN [dbo].[SubAreas] D
		ON A.IdSubArea = D.IdSubArea
	WHERE A.IdEmmpleado = @IdEmmpleado

	SET NOCOUNT OFF; 
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Empleados_Actualizar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Empleados_Actualizar] AS')
END
GO

ALTER PROCEDURE usp_Empleados_Actualizar
	@IdEmmpleado	numeric,
	@IdTipoDocumento	numeric,
	@NumeroDocumento	nvarchar(100),
	@Nombres	nvarchar(1000),
	@Apellidos	nvarchar(1000),
	@IdArea	numeric,
	@IdSubArea	numeric,
	@Usuario numeric
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	DECLARE @strMsg VARCHAR(2000)

	BEGIN TRY
		BEGIN TRAN
			IF @IdEmmpleado <= 0
			BEGIN
				INSERT INTO [dbo].[Empleados] 
				(IdTipoDocumento,NumeroDocumento,Nombres,Apellidos,IdArea,IdSubArea,UsuarioCreacion,FechaCreacion)
				VALUES
				(@IdTipoDocumento,@NumeroDocumento,@Nombres,@Apellidos,@IdArea,@IdSubArea,@Usuario,GETDATE())

				SET @IdEmmpleado = SCOPE_IDENTITY()
			END
			ELSE
			BEGIN
				UPDATE [dbo].[Empleados]
					SET IdTipoDocumento = @IdTipoDocumento,
						NumeroDocumento = @NumeroDocumento,
						Nombres = @Nombres,
						Apellidos = @Apellidos,
						IdArea = @IdArea,
						IdSubArea = @IdSubArea,
						UsuarioModificacion = @Usuario,
						FechaModificacion = GETDATE()
				WHERE IdEmmpleado = @IdEmmpleado
			END
		COMMIT TRAN

		SELECT @IdEmmpleado
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: dbo.usp_Empleados_Actualizar ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Empleados_Eliminar]') AND TYPE IN (N'P', N'PC'))
BEGIN
	EXEC(N'CREATE PROCEDURE [dbo].[usp_Empleados_Eliminar] AS')
END
GO

ALTER PROCEDURE [dbo].[usp_Empleados_Eliminar] 
	@IdEmpleado NUMERIC
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  

	DELETE FROM [dbo].[Empleados]
	WHERE IdEmmpleado = @IdEmpleado
END
GO