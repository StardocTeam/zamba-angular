---------------------------------------------------------    
-- Script de actualizacion cliente boston
-- Modulo: ABM de Usuario
--
-- Creado: osanchez 19.03.2009.
-- 
---------------------------------------------------------    
GO
---------------------------------------------------------    
-- Script de actualizacion cliente boston
-- Modulo: ABM de Usuario
--
-- Creado: osanchez 19.03.2009.
-- 
--------------------------------------------------------
---------------------------------------------------------  
-- USRTABLE
--
-- Modificacion: osanchez 19.03.2009
--	change: - Se valor por defecto en la columna PASSWORD.
--
--------------------------------------------------------- 
ALTER TABLE [USRTABLE] ADD CONSTRAINT
	DF_USRTABLE_PASSWORD DEFAULT '' FOR PASSWORD
GO
---------------------------------------------------------  
-- UsrData
--
-- Modificacion: osanchez 19.03.2009
--	change: - Se agrego indice unico para Colegajo.
--
---------------------------------------------------------  
CREATE TABLE [UsrData] (
	[UserID] [numeric](18, 0) NOT NULL ,
	[CoLegajo] [smallint] NOT NULL ,
	[CoNivelAutorizacion] [smallint] NULL ,
	[NuAgencia] [smallint] NULL ,
	CONSTRAINT [PK_UsrData] PRIMARY KEY  CLUSTERED 
	(
		[UserID]
	)  ON [PRIMARY] ,
	CONSTRAINT [IX_UsrData] UNIQUE  NONCLUSTERED 
	(
		[CoLegajo]
	)  ON [PRIMARY] ,
	CONSTRAINT [FK_UsrData_USRTABLE] FOREIGN KEY 
	(
		[UserID]
	) REFERENCES [USRTABLE] (
		[ID]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
) ON [PRIMARY]
GO
---------------------------------------------------------  
-- Obtiene un id segun un objtype especifico.   
-- Si no existe el objtype lo crea. Si existe retorna el campo
-- objid y actualiza el mismo campo con el valor objid = objid + 1   
--
-- Modificado: osanchez 18.03.2009       
--
---------------------------------------------------------  
ALTER  PROCEDURE sp_GetandSetLastIdWithOutput  
(  
 @OBJTYPE int,   
 @ID int output  
)   
AS  
BEGIN
 Declare @OBJID numeric  
  
  Select @objid=count(*) from Objlastid WHERE OBJECT_TYPE_ID =@OBJTYPE  
   
 If @objid=0   
      Begin  
          Insert Into Objlastid(Object_type_ID,ObjectID) values(@objtype,0)  
      End  
   
 UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1  WHERE OBJECT_TYPE_ID =@OBJTYPE  
 SELECT @id = OBJECTID FROM OBJLASTID WHERE OBJECT_TYPE_ID = @OBJTYPE
END
GO
---------------------------------------------------------  
-- Agrega un usuario a la tabla usrtable  
--   
--   
-- Modificado: osanchez 18.03.2009   
--  change: - Se agrego ejecucion transaccional.   
--          - Se agrego el retorno del campo id.    
--
---------------------------------------------------------  
CREATE PROCEDURE sp_AddUsers  
(  
 @UName varchar(20),  
 @FirstName varchar(50),  
 @LastName varchar(50),  
 @Mail varchar(50)  
)  
AS  
 Declare @id int  
BEGIN  
 Begin tran t1  
  
 exec sp_GetandSetLastIdWithOutput 5, @id output  
   
 IF @@ERROR <> 0   
  goto error  
  
        INSERT INTO USRTABLE (ID, name, nombres, apellido, correo)  
 VALUES (@id, @UName, @FirstName, @LastName, @Mail)  
  
 IF @@ERROR <> 0   
  goto error  
  
 INSERT INTO ZMailConfig (UserID, Correo, UserName)  
 VALUES (@id, @Mail, @Mail)  
  
 IF @@ERROR <> 0   
  goto error  
    
 IF @@TRANCOUNT > 0  
    Commit tran t1  
  
 Return @id  
  
error:  
 IF @@TRANCOUNT > 0  
  Rollback tran t1    
END  
GO
---------------------------------------------------------    
-- Actualiza un usuario en la tabla usrtable y 
-- en la tabla zmailconfig       
-- 
---------------------------------------------------------  
CREATE PROCEDURE sp_UpdUser  
(  
 @UserId numeric(9),  
 @FirstName varchar(50),  
 @LastName varchar(50),  
 @Mail varchar(50)  
)  
AS  
BEGIN
 UPDATE USRTABLE  
 SET  
  nombres = @FirstName,  
  apellido = @LastName,  
  correo = @Mail   
 WHERE Id = @UserId  
  
 UPDATE ZMailConfig   
 SET   
  Correo = @Mail,  
  UserName = @Mail  
 WHERE UserId = @UserId  
END  
GO
---------------------------------------------------------    
-- Elimina un usuario.
-- La eliminacion es en cascada   
--     
--------------------------------------------------------- 
CREATE  PROCEDURE sp_DelUser  
(  
	@UserId numeric(9)  
)  
AS 
BEGIN
 DELETE FROM USRTABLE  
 WHERE USRTABLE.ID = @UserId  
END
GO
---------------------------------------------------------  
-- Agrega un usuario a la tabla usrtable y a la tabla usrdata  
--  Reimplementacion para el cliente Boston  
--   
-- Modificado: osanchez 18.03.2009   
--  change: - Se agrego el retorno del campo id.  
--  
---------------------------------------------------------  
CREATE PROCEDURE pa_UsuarioAdd  
(  
 @UName varchar(20),  
 @FirstName varchar(50),  
 @LastName varchar(50),  
 @Mail varchar(50),  
 @Colegajo numeric(9),  
 @NuNivel smallint,  
 @NuAgencia smallint  
)  
AS   
BEGIN  
 Declare @id numeric  
 exec @id = sp_AddUsers @UName, @FirstName, @LastName, @Mail  
 INSERT INTO UsrData (UserID, CoLegajo, CoNivelAutorizacion, NuAgencia)  
 VALUES (@id, @CoLegajo, @NuNivel, @NuAgencia)  
   
 select @id  
END  
GO
---------------------------------------------------------    
-- Actualiza un usuario en la tabla usrtable y en la tabla usrdata  
--  Reimplementacion para el cliente Boston.    
--     
-- Modificado: osanchez 19.03.2009     
--  change: - Se cambio el campo NuNivel por CoNivelAutorizacion.    
--          - Se agrego la clausula SET NOCOUNT para que de como resultado  
--            1 registro modificado.  
---------------------------------------------------------    
CREATE PROCEDURE pa_UsuarioUpd    
 @FirstName varchar(50),    
 @LastName varchar(50),    
 @Mail varchar(50),    
 @Colegajo numeric(9),    
 @NuNivel smallint,    
 @NuAgencia smallint    
AS    
 Declare @UserId int    
    
BEGIN      
  
 SELECT @UserId = UserId FROM UsrData    
 WHERE Colegajo = @Colegajo    
  
 SET NOCOUNT ON  
    
 exec sp_UpdUser @UserId, @FirstName, @LastName, @Mail    
  
 SET NOCOUNT OFF  
  
 UPDATE UsrData    
 SET    
  CoNivelAutorizacion = @NuNivel,    
  NuAgencia = @NuAgencia     
 WHERE UserID = @UserId    
    
end    
GO
---------------------------------------------------------    
-- Elimina un usuario.
-- La eliminacion es en cascada
-- Reimplementacion para el cliente Boston    
--     
-- Modificado: osanchez 19.03.2009     
--  change: - Se reemplazo sql de eliminacion por el  
--            sp de zamba: sp_DelUser.  
---------------------------------------------------------   
CREATE PROCEDURE pa_UsuarioDel    
(    
  @Colegajo numeric(9)    
)    
AS    
BEGIN
  Declare @id as numeric  
  
  SELECT @id = UserId FROM UsrData    
  WHERE Colegajo = @Colegajo  
  
  exec sp_DelUser @id  
END  
GO


  
