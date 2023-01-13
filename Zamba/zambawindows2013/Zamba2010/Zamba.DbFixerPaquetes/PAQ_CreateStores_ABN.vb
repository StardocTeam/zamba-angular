Imports zamba.servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32
Imports System.IO
Public Class PAQ_CreateStores_ABN
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_StoresMigracionABN"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStores_ABN
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("13/02/2007")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Stores para ABN"
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("13/02/2007")
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.orden
        Get
            Return 84
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Try
            Dim sql As New System.Text.StringBuilder
            Dim sql2 As New System.Text.StringBuilder
            Dim sw As New IO.StreamWriter(Path.Combine(Application.StartupPath, "Script.sql"), True)
            '----PATH P/GUARDAR TODO EL SCRIPT

            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                ' sql.Append("                USE(ABN_PRODUCCION)")
                'sql.Append(ControlChars.NewLine)
                sql.Append("                BEGIN(TRANSACTION)")
                sql.Append(ControlChars.NewLine)
                sql.Append("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE")
                sql.Append(ControlChars.NewLine)
                sql.Append("                Print() 'Creating connection_100_DeleteConnection Procedure'")
                sql.Append(ControlChars.NewLine)
                sql.Append("SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON")
                sql.Append(ControlChars.NewLine)
                sql.Append("SET NUMERIC_ROUNDABORT OFF")
                sql.Append(ControlChars.NewLine)
                sql.Append("exec('Create Proc connection_100_DeleteConnection @conid int as Delete from UCM where Con_ID=@conid')")
                sql.Append(ControlChars.NewLine)
                sql.Append("IF @@ERROR <> 0")
                sql.Append(ControlChars.NewLine)
                sql.Append("   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION")
                sql.Append(ControlChars.NewLine)
                sql.Append("IF @@TRANCOUNT = 1")
                sql.Append(ControlChars.NewLine)
                sql.Append("                BEGIN()")
                sql.Append(ControlChars.NewLine)
                sql.Append("                Print() 'connection_100_DeleteConnection Procedure Added Successfully'")
                sql.Append(ControlChars.NewLine)
                sql.Append("                COMMIT(TRANSACTION)")
                sql.Append(ControlChars.NewLine)
                sql.Append("END ELSE")
                sql.Append(ControlChars.NewLine)
                sql.Append("BEGIN()")
                sql.Append(ControlChars.NewLine)
                sql.Append("                Print() 'Failed To Add connection_100_DeleteConnection Procedure'")
                sql.Append(ControlChars.NewLine)
                sql.Append("                End")
                sql.Append(ControlChars.NewLine)
                sql.Append("                BEGIN(TRANSACTION)")
                sql.Append(ControlChars.NewLine)
                sql.Append("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE")
                sql.Append(ControlChars.NewLine)
                sql.Append("                Print() 'Creating connection_100_InsertNewConecction Procedure'")
                sql.Append(ControlChars.NewLine)
                sql.Append("SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON")
                sql.Append(ControlChars.NewLine)
                sql.Append("SET NUMERIC_ROUNDABORT OFF")
                sql.Append(ControlChars.NewLine)
                'sql.Append("exec('CREATE PROCEDURE  connection_100_InsertNewConecction @v_userId int,@v_win_User nvarchar(50),@v_win_Pc nvarchar(50),@v_con_Id int,@Time_out int,@Type int AS INSERT INTO UCM(USER_ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,Time_out,Type) VALUES (@v_UserId,GetDate(),GetDate(),@v_Win_User,@v_Win_PC,@v_con_Id,@Time_out,@Type)')")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("IF @@ERROR <> 0")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("IF @@TRANCOUNT = 1")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                BEGIN()")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                Print() 'connection_100_InsertNewConecction Procedure Added Successfully'")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                COMMIT(TRANSACTION)")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("END ELSE")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                BEGIN()")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                Print() 'Failed To Add connection_100_InsertNewConecction Procedure'")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                End")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                BEGIN(TRANSACTION)")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                Print() 'Creating connection_100_InsertNewConecction Procedure'")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("SET NUMERIC_ROUNDABORT OFF")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("if exists (select * from sysobjects where id = object_id('connection_100_InsertNewConecction') and xtype ='P')")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("	DROP PROCEDURE connection_100_InsertNewConecction")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("exec('CREATE PROCEDURE connection_100_InsertNewConecction @v_userId int,@v_win_User nvarchar(50),@v_win_Pc nvarchar(50),@v_con_Id int,@Time_out int,@Type int AS INSERT INTO UCM(USER_ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,Time_out,Type) VALUES (@v_UserId,GetDate(),GetDate(),@v_Win_User,@v_Win_PC,@v_con_Id,@Time_out,@Type)')")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("IF @@ERROR <> 0")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("         IF @@TRANCOUNT = 1  ")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                BEGIN()  ")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                Print() 'connection_100_InsertNewConecction Procedure Added Successfully'  ")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                COMMIT(TRANSACTION)  ")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("         END ELSE  ")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                BEGIN() ")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                Print() 'Failed To Add connection_100_InsertNewConecction Procedure' ")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                End ")
                'sql.Append(ControlChars.NewLine)
                'sql.Append("                BEGIN(TRANSACTION) ")
                'sql.Append(ControlChars.NewLine)

                'sql.Append("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ")
                'sql.Append("                GO() ")
                'sql.Append("                Print() 'Deleting Delete_by_timeWF Procedure' ")
                'sql.Append("                GO() ")
                'sql.Append("DROP PROCEDURE [Delete_by_timeWF] ")
                'sql.Append("                GO() ")
                'sql.Append("IF @@ERROR <> 0 ")
                'sql.Append("   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION ")
                'sql.Append("                GO() ")
                'sql.Append("IF @@TRANCOUNT = 1 ")
                'sql.Append("                BEGIN() ")
                'sql.Append("               Print() 'Delete_by_timeWF Procedure Deleted Successfully' ")
                'sql.Append("                COMMIT(TRANSACTION) ")
                'sql.Append("END ELSE ")
                'sql.Append("                BEGIN() ")
                'sql.Append("                Print() 'Failed To Delete Delete_by_timeWF Procedure' ")
                'sql.Append("End ")
                'sql.Append("GO() ")
                'sql.Append("                BEGIN(TRANSACTION) ")
                'sql.Append("                GO() ")
                'sql.Append("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ")
                'sql.Append("                GO() ")
                'sql.Append("                Print() 'Creating Delete_by_timeWF Procedure' ")
                'sql.Append("GO ")
                'sql.Append("SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON ")
                'sql.Append("                GO() ")
                'sql.Append("SET NUMERIC_ROUNDABORT OFF ")
                'sql.Append("                GO() ")
                'sql.Append("exec('CREATE PROCEDURE Delete_by_timeWF AS DELETE FROM UCM WHERE Type=1 and DATEDIFF(mi,U_TIME,GetDate())> [Time_Out]') ")
                'sql.Append("               GO() ")
                'sql.Append("IF @@ERROR <> 0 ")
                'sql.Append(" ")
                'sql.Append(" ")
                'sql.Append(" ")
                'sql.Append(" ")
                'sql.Append(" ")






                '                GO()

                'IF @@TRANCOUNT = 1
                '                    BEGIN()
                '                    Print() 'Delete_by_timeWF Procedure Added Successfully'
                '                    COMMIT(TRANSACTION)
                'END ELSE
                '                    BEGIN()
                '                    Print() 'Failed To Add Delete_by_timeWF Procedure'
                '                    End
                '                    GO()

                '--
                '-- Script To Delete GetActiveWFConect Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                '                    BEGIN(TRANSACTION)
                '                    GO()
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                '                    GO()

                '                    Print() 'Deleting GetActiveWFConect Procedure'
                '                    GO()

                'DROP PROCEDURE [GetActiveWFConect]
                '                    GO()

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                '                            GO()

                'IF @@TRANCOUNT = 1
                '                                BEGIN()
                '                                Print() 'GetActiveWFConect Procedure Deleted Successfully'
                '                                COMMIT(TRANSACTION)
                'END ELSE
                '                                BEGIN()
                '                                Print() 'Failed To Delete GetActiveWFConect Procedure'
                '                                End
                '                                GO()

                '--
                '-- Script To Create GetActiveWFConect Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                '                                BEGIN(TRANSACTION)
                '                                GO()
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                '                                GO()

                '                                Print() 'Creating GetActiveWFConect Procedure'
                '                                GO()

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                '                                GO()

                'SET NUMERIC_ROUNDABORT OFF
                '                                GO()

                'exec('Create Proc GetActiveWFConect
                'AS
                'Select Used from LIC where Type=1;')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'GetActiveWFConect Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add GetActiveWFConect Procedure'
                '                                                End
                'GO

                '--
                '-- Script To Create GetNewRights Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating GetNewRights Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('-- select * from usr_rights

                '-- GetNewRights 4,2,1,26

                'Create procedure GetNewRights
                '@userid int,
                '@objid int,
                '@rtype int,
                '@aditional int
                'AS
                'declare @cant int
                'Select @cant=count(*) from usr_rights 
                'Where Aditional=@aditional and rtype=@rtype and objid=@objid and Groupid in (Select Groupid from usr_R_group Where USRID=@userid)
                'if @cant>1
                '    Select 1
                ' else
                '    Select 0')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'GetNewRights Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add GetNewRights Procedure'
                '                                                                            End
                'GO

                '--
                '-- Script To Create GetNewRights Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating GetNewRights Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'if exists (select * from sysobjects where id = object_id('GetNewRights') and xtype ='P')
                '	DROP PROCEDURE GetNewRights
                'GO

                'exec('Create procedure GetNewRights
                '@userid int,
                '@objid int,
                '@rtype int,
                '@aditional int
                'AS
                'declare @cant int
                'Select @cant=count(*) from usr_rights 
                'Where (Aditional=@aditional and rtype=@rtype and objid=@objid) 
                'and (Groupid=@userid or Groupid in (Select Groupid from usr_R_group Where USRID=@userid))
                'if @cant>1
                '    Select 1
                ' else
                '    Select 0')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'GetNewRights Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add GetNewRights Procedure'
                '                                                                                                            End
                'GO

                '--
                '-- Script To Create INSLIQUIDADOS Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating INSLIQUIDADOS Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure INSLIQUIDADOS	(@DOC_ID	varchar(20),
                '				      	@work_id	varchar(12),
                '				      	@CheckIn 	DATETIME)
                'As
                'SET NOCOUNT ON



                'INSERT INTO LIQLNOTES
                '(DOC_ID	, work_id, CheckIn, TIMESTMP)
                'VALUES
                '(@DOC_ID, @WORK_ID, @CHECKIN, GETDATE())')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'INSLIQUIDADOS Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add INSLIQUIDADOS Procedure'
                '                                                                                                                        End
                'GO

                '--
                '-- Script To Create messages_100_CountNewMessages Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating messages_100_CountNewMessages Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create procedure messages_100_CountNewMessages @UserId integer as SELECT count(*) FROM MSG_DEST WHERE MSG_DEST.user_id=@userid AND MSG_DEST.deleted=0 and [read]=0')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'messages_100_CountNewMessages Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add messages_100_CountNewMessages Procedure'
                '                                                                                                                                    End
                'GO

                '--
                '-- Script To Create SOEstadisticas Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating SOEstadisticas Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure SOEstadisticas
                '				(@DESDE varchar(10) = '''',
                '				 @HASTA varchar(10) = '''')
                'As
                'set nocount on

                '/********************************       REPORTE      02				*********************************/
                '/*														*/      
                '/*  Destinado a: SEGUNDOS OPERADORES										*/
                '/*  Descripcion del reporte:  										   	*/
                '/*  Estadísticas, primer día de cada mes (y la posibilidad de sacarlo en cualquier momento por rango de fechas) */
                '/*														*/
                '/****************************************************************************************************************/    
                '---DECLARE @DESDE varchar(10) ,@HASTA varchar(10) 
                'DECLARE @WORKFLOW NUMERIC(10,0)
                'DECLARE @FECHA DATETIME
                'DECLARE @TABLA VARCHAR(50)
                'DECLARE @TIPODOC NUMERIC(4,0)
                'DECLARE @SELECT VARCHAR(2000) 
                'DECLARE @FDESDE DATETIME,
                '	@FHASTA DATETIME
                'DECLARE @total NUMERIC(10,0)

                'SELECT  @WORKFLOW = ''7''
                'SELECT  @FECHA = convert(datetime, ''01/29/2006'')


                'IF (@DESDE IS NULL OR @DESDE = '''') 
                '	SET @FDESDE = convert(datetime, convert(varchar(2),(DATEPART(MONTH, @FECHA))) + ''/'' + ''01'' + ''/'' + convert(varchar(4),DATEPART(YEAR, @FECHA)) )
                'IF (@HASTA IS NULL OR @HASTA = '''')
                '	SET @FHASTA = @FECHA 


                'Select datepart(hour,CONVERT(DATETIME,DOCS.checkin,102))  horarioRecepcion, 
                '	count(*) MinXHora, 
                '	SUM(CASE WHEN I179.I44 = ''VENTA'' THEN 1 ELSE 0 END) CANTVta,	
                '	SUM(CASE WHEN I179.I44 = ''COMPRA'' THEN 1 ELSE 0 END) CANTCompra
                'Into #DATOS
                'From ZAMBAPRD.WfWorkFlow 	WORKFLOW
                'Join ZAMBAPRD.Wfstep 	PASOS
                'On   WORKFLOW.Work_id 	= PASOS.Work_id 
                'Join ZAMBAPRD.wfdocument	DOCS
                'On   PASOS.step_Id 	= DOCS.step_Id
                'And  PASOS.Work_id  	= DOCS.work_id
                'Left Join ZAMBAPRD.Doc_I179      I179
                'On   I179.DOC_ID = DOCS.Doc_ID
                'Where WORKFLOW.Work_id = 7  
                'and I179.I44 is not null
                'Group by datepart(hour,CONVERT(DATETIME,DOCS.checkin,102))
                'Order by datepart(hour,CONVERT(DATETIME,DOCS.checkin,102))


                'Select ''De '' + convert(varchar(2), HorarioRecepcion) + '' a ''  + case when convert(varchar(2),HorarioRecepcion) = 12 then  ''1  hs.'' else convert(varchar(2),HorarioRecepcion + 1) + '' hs.'' end  Rango, 
                '	MinXHora, CANTVta,CANTCompra
                'into #final
                'From #DATOS
                'order by HorarioRecepcion

                '---DECLARE @TOTAL NUMERIC(15,0)
                'Select @total = sum(minxhora) from #Final


                'SELECT RANGO, MINXHORA, CONVERT(VARCHAR(10),convert(numeric(5,2),MINXHORA*100/@TOTAL)) + ''%'' PORC, CANTVta,CANTCompra
                'FROM #FINAL


                'Drop table #DATOS
                'Drop table #final
                'Set nocount off')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'SOEstadisticas Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add SOEstadisticas Procedure'
                '                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create WF_WfByTypeDocs Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating WF_WfByTypeDocs Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Proc WF_WfByTypeDocs
                'As
                'Select Distinct(WFW.Name) as Workflow,DT.Doc_Type_Name as Tipo_Documento
                '	From Doc_Type DT Inner Join WFDocument WFD on (WFD.Doc_Type_Id=DT.Doc_Type_Id)  --WFDocument WFD on (WFW.Work_Id=WFD.Work_Id)
                '			 Inner Join WFWorkflow WFW on (WFW.Work_Id=WFD.Work_Id)   
                '        Order By DT.Doc_Type_Name')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'WF_WfByTypeDocs Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add WF_WfByTypeDocs Procedure'
                '                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create WFDocsBySteps Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating WFDocsBySteps Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Proc WFDocsBySteps
                'As

                'Select WFS.Name as Etapa,WFD.Name as Documento_Nombre
                ' From WFStep WFS Inner Join wfdocument WFD On (WFS.Step_Id=WFD.Step_Id)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'WFDocsBySteps Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add WFDocsBySteps Procedure'
                '                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create WFRulesBySteps Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating WFRulesBySteps Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure WFRulesBySteps
                'As

                'Select WFS.Name as Etapa,WFR.Name as Regla
                '	From WFStep WFS Inner Join  WFRules WFR On (WFS.Step_Id=WFR.Step_Id )
                '	Order By WFS.Name')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'WFRulesBySteps Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add WFRulesBySteps Procedure'
                '                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create WFStepsByWorkflow Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating WFStepsByWorkflow Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure WFStepsByWorkflow
                'As

                'Select WFW.Name as Workflow_Nombre,WFS.Name as Etapa
                '	From WFWorkflow WFW Inner Join WFStep WFS On (WFW.Work_ID=WFS.Work_ID)
                '	Order By WFW.Name')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'WFStepsByWorkflow Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add WFStepsByWorkflow Procedure'
                '                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create WFTypeDocsByWF Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating WFTypeDocsByWF Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure WFTypeDocsByWF

                'AS

                'Select Distinct(DT.Doc_Type_Name) as Tipo_Documento,WFW.Name as Workflow
                '	From WFWorkflow WFW Inner Join WFDocument WFD on (WFW.Work_Id=WFD.Work_Id)
                '			    Inner Join Doc_Type DT on (WFD.Doc_Type_Id=DT.Doc_Type_Id)
                '        Order By WFW.Name')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'WFTypeDocsByWF Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add WFTypeDocsByWF Procedure'
                '                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create ZambaHtm333697 Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZambaHtm333697 Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure ZambaHtm333697
                '( @Cliente	VarChar(12),
                '@Servicio	VarChar(12),
                '@TipoHTML	VarChar(3),
                '@FeProc	Datetime,
                '@Total	Numeric(15,0),
                '@Direcc	VarChar(500) OutPut,
                '@Resultado	Numeric(15,0) OutPut
                ')
                'As

                '--VARIABLES CONTROL
                'Declare @Resul 		Numeric(15,0),
                '@Control 	Numeric(15,0),
                '@cuentatotal    Numeric(15,0),
                '@Paginas 	Numeric(15,3),
                '@Cuenta 	Numeric(15,3)

                '--VARIABLES CREACION DE HTML
                'Declare
                '@Archivo     	VarChar (130),
                '@Titulo 	VarChar (60),
                '@Titulo2 	VarChar (60),
                '@Sentencia 	VarChar (1000),
                '@ArchivoBorra  	VarChar (130),
                '@StringExec 	VarChar (4000),
                '@Path 		VarChar (100),
                '@HTML 		VarChar (30)

                'Set NoCount On




                'Select @Control = @Total

                'If @Control < 3000
                'Select @Paginas = 1
                'Else
                'Begin
                '	Select @Cuenta = @Control / 3000
                '	If ( @Cuenta - Floor(@Cuenta) ) <> 0
                '	Select @Paginas = Floor(@cuenta) + 1
                '	Else
                '	Select @Paginas = @Cuenta
                '                                                                                                                                                                                                                                                                                            End

                'Select @Resultado = @Paginas

                '--ARMAR LA DIRECCION DEL SERVIDOR DONDE TIENE QUE IR
                '/*aras-de012*/ Select @Path  = Convert(Varchar(100),''C:\ZambaVolumenes'')


                '--ARMA NOMBRE DEL ARCHIVO HTML
                'Select @HTML  = Convert(Varchar(30),''OpePend_'' +
                '(@cliente)) + ''_'' +
                '(Convert(varchar(2), Datepart(Day, @FeProc )) + ''-''
                '+ Convert(VarChar(2), Datepart(Month, @FeProc)) + ''-''
                '+ Convert(VarChar(4), Datepart(Year, @FeProc)))

                '--ARMA NOMBRE DE ARCHIVO DE SALIDA
                'Select @Archivo = @Path +''\''+ @HTML + ''__'' + ''.htm''

                '--GENERA DIRECCION DONDE SE DEJA EL ATTACH (parametro de OutPut)
                'Select @Direcc = Convert(Varchar(500),(@HTML + ''_''))

                '--ARMA SENTENCIA PARA GENERAR DATOS HTML
                'Select @Sentencia = ''select I50, I51 Referencia_Cliente, I52 Referencia_Banco, 
                '	Fecha, 	I48 Concepto,  I87 Producto, 
                '	I16 Importe, I49 Moneda_Operacion, I14 Nro_Operacion, 
                '	I78 Nro_Topaz, I76 Observaciones, Name estado
                'From doc_i179 I179
                'join wfdocument DOCS
                'ON I179.DOC_ID = DOCS.Doc_ID
                'Join wfstepstates ESTADO
                'on ESTADO.Step_Id = DOCS.step_Id 
                'AND Doc_State_ID = Do_State_ID  
                'WHERE i50 = '' + @cliente

                '--ARMA TITULO DE REPORTE
                'Select @Titulo = ''Operaciones pendientes''

                '--ARMA TITULO DE PAGINA
                'Select @Titulo2 = ''Operaciones pendientes''

                '---Select @archivoBorra = @Path +''\''+ @HTML + ''_'' + ''*.htm''
                '---Select @StringExec = ''Exec master..xp_cmdshell "Del '' + @archivoBorra + ''"''
                '---Exec  (@StringExec)


                'If @control <> 0
                'Begin
                '	If @Total = @control
                '	Begin
                '	Execute @Resul = sp_makewebtask
                '	@outputfile=@archivo,
                '	@query=@sentencia,
                '	@fixedfont=0,
                '	@lastupdated=1,
                '	@HTMLheader=2,
                '	@username = usrzamba,
                '	@webpagetitle=@Titulo2 ,
                '	@resultstitle=@Titulo ,
                '	@url=''http://www.abnamro.com.ar'',
                '	@reftext=''ABN AMRO Argentina'',
                '	@dbname=N''ZambaPrd'',
                '	@whentype=1,
                '	@nrowsperpage=3000,
                '	@procname=''HACEHTMLz'',
                '	@codepage=65001,
                '	@charset=N''utf-8''
                '                                                                                                                                                                                                                                                                                                                                End
                '	else
                '	Select @Resul = -24
                '                                                                                                                                                                                                                                                                                                                                    End
                ' Else
                ' Select @Resul = -32


                '--Select @Resul = @@Error
                'If @Resul <> 0
                'Select @Resul = -46		--ERROR EN ARMADO DE HTML

                'Set Nocount OfF
                'Return @Resul')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZambaHtm333697 Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZambaHtm333697 Procedure'
                '                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZDelWfByWfId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZDelWfByWfId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZDelWfByWfId @pWork_ID numeric as
                'DELETE wfworkflow where work_id = @pWork_ID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZDelWfByWfId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZDelWfByWfId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create ZDelWFDByTaskId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZDelWFDByTaskId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZDelWFDByTaskId @pTaskId numeric as
                'DELETE WFDOCUMENT WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZDelWFDByTaskId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZDelWFDByTaskId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create ZDelWFStepByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZDelWFStepByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZDelWFStepByStepId @pStepId numeric as
                'DELETE wfSTEP where STEP_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZDelWFStepByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZDelWFStepByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create ZDelWFStepStatesByStateId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZDelWFStepStatesByStateId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZDelWFStepStatesByStateId @pStateID numeric AS
                'DELETE WFStepStates where doc_state_Id = @pStateID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZDelWFStepStatesByStateId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZDelWFStepStatesByStateId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZGetAllWF Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZGetAllWF Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZGetAllWF as
                'SELECT * FROM WFworkflow')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZGetAllWF Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZGetAllWF Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZGetAllWFStep Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZGetAllWFStep Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZGetAllWFStep as
                'Select * from wfstep')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZGetAllWFStep Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZGetAllWFStep Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZGetViewWFStepsByWfID Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZGetViewWFStepsByWfID Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZGetViewWFStepsByWfID @pWFId numeric as
                'Select * from ZViewWFSTEPS where WORK_ID = @pWFId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZGetViewWFStepsByWfID Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZGetViewWFStepsByWfID Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZGetVWFStepsByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZGetVWFStepsByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZGetVWFStepsByStepId @pStepId numeric as
                'Select * from ZViewWFSTEPS where step_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZGetVWFStepsByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZGetVWFStepsByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZGetWFDStepIdByDocId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZGetWFDStepIdByDocId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZGetWFDStepIdByDocId @pDocId numeric  as
                'select step_Id from wfdocument where doc_id= @pDocId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZGetWFDStepIdByDocId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZGetWFDStepIdByDocId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZGetWFSSByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZGetWFSSByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZGetWFSSByStepId @pStepId numeric as
                'Select * from WFStepStates where step_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZGetWFSSByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZGetWFSSByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZGetWFSSByStepIdStateId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZGetWFSSByStepIdStateId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZGetWFSSByStepIdStateId @pStateId numeric, @pStepId numeric as
                'Select * from WFStepStates where doc_state_id=@pStateId  or step_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZGetWFSSByStepIdStateId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZGetWFSSByStepIdStateId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZGetWFStepByWorkId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZGetWFStepByWorkId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZGetWFStepByWorkId @pWorkId numeric as
                'Select * from wfstep where work_id= @pWorkId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZGetWFStepByWorkId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZGetWFStepByWorkId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZInsWFDocument1 Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZInsWFDocument1 Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZInsWFDocument1 @pFolderId numeric ,@pStepId numeric ,@pDocId numeric , @pDTId numeric ,@pStateId numeric , @pName varchar(250) , @pICONID numeric , @pCheckIn datetime, @pAsigned numeric ,@pExclusive numeric, @pExpireDate datetime as
                'INSERT INTO WFDOCUMENT (FOLDER_ID,STEP_ID,DOC_ID,DOC_TYPE_ID,Do_State_Id,NAME,ICONID,CheckIn,User_asigned,exclusive,ExpireDate) 
                'VALUES (@pFolderId ,@pStepId ,@pDocId , @pDTId ,@pStateId , @pName , @pICONID , @pCheckIn, @pAsigned ,@pExclusive, @pExpireDate)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZInsWFDocument1 Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZInsWFDocument1 Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create ZInsWFRulesHST Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZInsWFRulesHST Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZInsWFRulesHST @pFolderId numeric , @pStepId numeric ,@pDoc_Id numeric ,@pDocTypeid numeric ,@pRuleId numeric ,@pResult numeric ,@pUsrId numeric ,@pEjecDate datetime ,@pData nvarchar(400) as
                'INSERT INTO WFRULESHST (FOLDER_ID  ,STEP_ID   ,DOC_ID  ,DOC_TYPE_ID,Rule_Id ,Result  ,User_Id,Ejecution_Date,Data) 
                'VALUES 		       (@pFolderId , @pStepId ,@pDoc_Id,@pDocTypeid,@pRuleId,@pResult,@pUsrId,@pEjecDate    ,@pData)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZInsWFRulesHST Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZInsWFRulesHST Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create ZInsWfStepStates Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZInsWfStepStates Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZInsWfStepStates @pStateID numeric, @pDesc nvarchar(250), @pStepId numeric, @pName varchar(50), @pInitial numeric AS
                'INSERT INTO WFStepStates (Doc_State_id,Description,Step_Id,Name,Initial) 
                'VALUES ( @pStateID , @pDesc , @pStepId , @pName , @pInitial )')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZInsWfStepStates Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZInsWfStepStates Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Delete zsp_connection_100_InsertNewConecction Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Deleting zsp_connection_100_InsertNewConecction Procedure'
                'GO

                'DROP PROCEDURE [zsp_connection_100_InsertNewConecction]
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_connection_100_InsertNewConecction Procedure Deleted Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Delete zsp_connection_100_InsertNewConecction Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_connection_100_InsertNewConecction Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_connection_100_InsertNewConecction Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE  zsp_connection_100_InsertNewConecction 
                '@v_userId int,
                '@v_win_User nvarchar(50),
                '@v_win_Pc nvarchar(50),
                '@v_con_Id int,
                '@Time_out int,
                '@Type int 
                'AS 
                'INSERT INTO UCM(USER_ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,Time_out,Type) VALUES
                '(@v_UserId,GetDate(),GetDate(),@v_Win_User,@v_Win_PC,@v_con_Id,@Time_out,@Type)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_connection_100_InsertNewConecction Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_connection_100_InsertNewConecction Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Delete zsp_doctypes_100_CopyDocType Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Deleting zsp_doctypes_100_CopyDocType Procedure'
                'GO

                'DROP PROCEDURE [zsp_doctypes_100_CopyDocType]
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_doctypes_100_CopyDocType Procedure Deleted Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Delete zsp_doctypes_100_CopyDocType Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_doctypes_200_GetDocTypesByUserRights Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_doctypes_200_GetDocTypesByUserRights Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure zsp_doctypes_200_GetDocTypesByUserRights
                '@usrid int,
                '@rtype int
                'As
                'Select * from doc_type where Doc_type_id in (Select distinct(aditional) from usr_rights
                'where (GROUPID in (Select groupid from usr_r_group where usrid=@usrid) or groupId=@usrid) And (objid=2 and rtype=@rtype))
                'order by doc_type_name')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_doctypes_200_GetDocTypesByUserRights Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_doctypes_200_GetDocTypesByUserRights Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_imports_100_GetProcessHistory Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_imports_100_GetProcessHistory Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE zsp_imports_100_GetProcessHistory (@ProcessID NUMERIC) AS SELECT P_HST.ID,P_HST.Process_Date,P_HST.[User_Id], P_HST.TotalFiles,P_HST.ProcessedFiles,P_HST.Result_Id ,P_HST.SkipedFiles,P_HST.ErrorFiles,P_HST.Path,USRTABLE.Name,P_HST.Process_id,ip_results.Result ,P_HST.Hash FROM P_HST,USRTABLE,IP_RESULTS WHERE P_HST.[User_Id] = USRTABLE.Id  AND P_HST.process_ID = @ProcessID AND IP_RESULTS.RESULT_ID = P_HST.RESULT_ID ORDER BY P_HST.ID DESC')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_imports_100_GetProcessHistory Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_imports_100_GetProcessHistory Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Delete zsp_imports_100_InsertProcHistory Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Deleting zsp_imports_100_InsertProcHistory Procedure'
                'GO
                'if exists (select * from sysobjects where id = object_id('zsp_imports_100_InsertProcHistory') and xtype ='P')
                '	DROP PROCEDURE [zsp_imports_100_InsertProcHistory]
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_imports_100_InsertProcHistory Procedure Deleted Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Delete zsp_imports_100_InsertProcHistory Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Delete zsp_index_100_IndexGeneration Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Deleting zsp_index_100_IndexGeneration Procedure'
                'GO

                'if exists (select * from sysobjects where id = object_id('zsp_index_100_IndexGeneration') and xtype ='P')
                '	DROP PROCEDURE [zsp_index_100_IndexGeneration]
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_index_100_IndexGeneration Procedure Deleted Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Delete zsp_index_100_IndexGeneration Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Delete zsp_index_100_InsertLinkInfo Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Deleting zsp_index_100_InsertLinkInfo Procedure'
                'GO

                'if exists (select * from sysobjects where id = object_id('zsp_index_100_InsertLinkInfo') and xtype ='P')
                '	DROP PROCEDURE [zsp_index_100_InsertLinkInfo]
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_index_100_InsertLinkInfo Procedure Deleted Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Delete zsp_index_100_InsertLinkInfo Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_security_100_UpdUserRight Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_security_100_UpdUserRight Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROC zsp_security_100_UpdUserRight @Rightv  numeric, @Rightid  numeric AS UPDATE USER_RIGHTS SET Right_Value =@Rightv  WHERE Right_Id =@Rightid')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_security_100_UpdUserRight Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_security_100_UpdUserRight Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_users_100_GetUserActions Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_users_100_GetUserActions Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create PROC zsp_users_100_GetUserActions @UserId numeric as SELECT USER_HST.Action_Date AS Fecha, OBJECTTYPES.OBJECTTYPES AS Herramienta, RIGHTSTYPE.RIGHTSTYPE AS Accion, user_hst.s_object_id AS En FROM USER_HST,USRTABLE,OBJECTTYPES,RIGHTSTYPE WHERE USER_HST.User_Id = USRTABLE.Id AND USER_HST.Object_Type_Id = OBJECTTYPES.ObjectTypesId AND USER_HST.Action_Type = RIGHTSTYPE.RightsTypeId AND USER_HST.User_Id = @UserId ORDER BY USER_HST.Action_Date DESC')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_users_100_GetUserActions Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_users_100_GetUserActions Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create Zsp_users_200_GetUserByName Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_users_200_GetUserByName Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Proc Zsp_users_200_GetUserByName
                '@name nvarchar(20)
                'as
                'Select * from usrtable where name=@name')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_users_200_GetUserByName Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_users_200_GetUserByName Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create Zsp_users_200_GetUserByName Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_users_200_GetUserByName Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'if exists (select * from sysobjects where id = object_id('Zsp_users_200_GetUserByName') and xtype ='P')
                '	DROP PROCEDURE [Zsp_users_200_GetUserByName]
                'GO

                'exec('Create Proc Zsp_users_200_GetUserByName
                '@name nvarchar(20)
                'as
                'Select * from usrtable where name=@name')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_users_200_GetUserByName Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_users_200_GetUserByName Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_CloseTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_CloseTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_CloseTask @pTaskId numeric as UPDATE WFDOCUMENT SET USER_ASIGNED = 0 ,CheckIn = NULL,User_Asigned_By = 0 ,Date_Asigned_By = NULL WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_CloseTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_CloseTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_DeleteDocumentByTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_DeleteDocumentByTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_DeleteDocumentByTask @pTaskId numeric as DELETE WFDOCUMENT WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_DeleteDocumentByTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_DeleteDocumentByTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_DeleteRule Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_DeleteRule Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE Zsp_workflow_100_DeleteRule
                '@RuleID numeric
                'AS
                'DELETE FROM wfrules where id=@RuleID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_DeleteRule Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_DeleteRule Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_DeleteRuleParams Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_DeleteRuleParams Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE Zsp_workflow_100_DeleteRuleParams
                '@RuleID numeric
                'AS
                'Delete from wfruleparams where [id]=@RuleID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_DeleteRuleParams Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_DeleteRuleParams Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_DeleteStepById Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_DeleteStepById Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE Procedure zsp_workflow_100_DeleteStepById 
                '@pStepId numeric 
                'As 
                'DELETE WfSTEP where STEP_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_DeleteStepById Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_DeleteStepById Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_DeleteStepStateById Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_DeleteStepStateById Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_DeleteStepStateById @pStateID numeric AS DELETE WFStepStates where doc_state_Id = @pStateID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_DeleteStepStateById Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_DeleteStepStateById Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_DeleteWorkFlowByWfId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_DeleteWorkFlowByWfId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_DeleteWorkFlowByWfId @pWork_ID numeric as DELETE wfworkflow where work_id = @pWork_ID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_DeleteWorkFlowByWfId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_DeleteWorkFlowByWfId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_documentosAsignados Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_documentosAsignados Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure Zsp_workflow_100_documentosAsignados
                'AS
                'Select *, isnull(NombreUsuario,''Sin Asignar'') as Usuario from ZWFViewReport')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_documentosAsignados Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_documentosAsignados Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_FillSteps Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_FillSteps Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure Zsp_workflow_100_FillSteps
                '@work_id int
                'As
                'Select * from wfstep where work_id=@work_id')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_FillSteps Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_FillSteps Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetAllWf Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetAllWf Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetAllWf as SELECT * FROM WFworkflow')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetAllWf Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetAllWf Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetDocCountByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetDocCountByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetDocCountByStepId @pStepId numeric AS SELECT DCOUNT FROM ZVIEWWFDOCUMENTCOUNT WHERE STEP_ID = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetDocCountByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetDocCountByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetGroupIdByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetGroupIdByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetGroupIdByStepId @pStepId numeric as Select GROUPID from ZVIEWWFUSERSTEPS where step_id = @pStepId;')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetGroupIdByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetGroupIdByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetStatesByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetStatesByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetStatesByStepId @pStepId numeric as Select * from WFStepStates where step_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetStatesByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetStatesByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetStatesByStepOrState Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetStatesByStepOrState Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetStatesByStepOrState @pStateId numeric, @pStepId numeric as Select * from WFStepStates where doc_state_id=@pStateId  or step_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetStatesByStepOrState Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetStatesByStepOrState Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetStepIdByDocId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetStepIdByDocId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetStepIdByDocId @pDocId numeric  as select step_Id from wfdocument where doc_id= @pDocId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetStepIdByDocId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetStepIdByDocId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetStepsByWork Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetStepsByWork Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetStepsByWork @pWorkId numeric as Select * from wfstep where work_id= @pWorkId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetStepsByWork Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetStepsByWork Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetStepsOfUsrGroupByAdt Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetStepsOfUsrGroupByAdt Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetStepsOfUsrGroupByAdt @pAdt numeric as Select * from zstepuserGroups where aditional= @pAdt')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetStepsOfUsrGroupByAdt Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetStepsOfUsrGroupByAdt Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetViewStepsByStep Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetViewStepsByStep Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetViewStepsByStep @pStepId numeric as Select * from ZViewWFSTEPS where step_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetViewStepsByStep Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetViewStepsByStep Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_GetViewStepsByWfId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_GetViewStepsByWfId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_GetViewStepsByWfId @pWFId numeric as Select * from ZViewWFSTEPS where WORK_ID = @pWFId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_GetViewStepsByWfId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_GetViewStepsByWfId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_InsertRuleHistory Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_InsertRuleHistory Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_InsertRuleHistory @pFolderId numeric , @pStepId numeric ,@pDoc_Id numeric ,@pDocTypeid numeric ,@pRuleId numeric ,@pResult numeric ,@pUsrId numeric ,@pEjecDate datetime ,@pData nvarchar(400) as INSERT INTO WFRULESHST (FOLDER_ID  ,STEP_ID   ,DOC_ID  ,DOC_TYPE_ID,Rule_Id ,Result  ,User_Id,Ejecution_Date,Data) VALUES (@pFolderId , @pStepId ,@pDoc_Id,@pDocTypeid,@pRuleId,@pResult,@pUsrId,@pEjecDate,@pData)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_InsertRuleHistory Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_InsertRuleHistory Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_InsertRuleParamItem Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_InsertRuleParamItem Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE Zsp_workflow_100_InsertRuleParamItem
                '@RuleId numeric,
                '@Item numeric,
                '@Value varchar(1000)
                'AS
                'Insert into WFRuleParamItems(rule_id,item,value) 
                'values(@RuleId ,@Item ,@Value)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_InsertRuleParamItem Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_InsertRuleParamItem Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_InsertStep Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_InsertStep Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_InsertStep @pWFId numeric ,@pStepId numeric , @pName varchar(50),@pDesc varchar(100),@pHelp varchar(100), @pCDate datetime,@pEDate datetime , @pImgInd numeric ,@pLocX decimal ,@pLocY decimal ,@pMaxDocs numeric ,@pMaxHours numeric ,@pStartAt numeric as INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc) VALUES (@pWFId ,@pStepId,@pName ,@pDesc,@pHelp,@pCDate,@pEDate,@pImgInd ,@pLocX ,@pLocY ,@pMaxDocs ,@pMaxHours ,@pStartAt)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_InsertStep Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_InsertStep Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_InsertWorkFlow Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_InsertWorkFlow Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE Procedure zsp_workflow_100_InsertWorkFlow 
                '@pWork_ID decimal, 
                '@pWStat_Id decimal,
                '@pName varchar(50),
                '@pHelp varchar(200), 
                '@pDescription varchar(100),
                '@pDate datetime, 
                '@pEditDate datetime, 
                '@pRefreshRate numeric, 
                '@pInitialStepId numeric 
                'AS 
                'Insert into wfworkflow (work_id,Wstat_id,name,help,description,createdate,editdate,refreshrate,initialstepid) VALUES (@pWork_ID ,@pWStat_Id ,@pName ,@pHelp ,@pDescription ,@pDate,@pEditDate,@pRefreshRate ,@pInitialStepId)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_InsertWorkFlow Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_InsertWorkFlow Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_InsWF Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_InsWF Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE procedure Zsp_workflow_100_InsWF 
                '@pWork_ID decimal, 
                '@pWStat_Id decimal ,
                '@pName varchar(50),
                '@pHelp varchar(200), 
                '@pDescription varchar(100),
                '@pDate datetime, 
                '@pEditDate datetime, 
                '@pRefreshRate numeric, 
                '@pInitialStepId numeric 
                'AS
                'Insert into wfworkflow (work_id,Wstat_id,[name],[help],[description],createdate,editdate,refreshrate,initialstepid) 
                'VALUES (@pWork_ID ,@pWStat_Id ,@pName ,@pHelp ,@pDescription ,@pDate,@pEditDate,@pRefreshRate ,@pInitialStepId )')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_InsWF Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_InsWF Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_InsWFStep Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_InsWFStep Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE Zsp_workflow_100_InsWFStep 
                '@pWFId numeric,
                '@pStepId numeric, 
                '@pName varchar(50),
                '@pDesc varchar(100),
                '@pHelp varchar(100), 
                '@pCDate datetime,
                '@pEDate datetime, 
                '@pImgInd numeric,
                '@pLocX decimal,
                '@pLocY decimal,
                '@pMaxDocs numeric,
                '@pMaxHours numeric,
                '@pStartAt numeric 
                'AS
                'INSERT INTO WFSTEP(work_id,STEP_ID,[Name],[Description],[Help],CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc)
                'VALUES (@pWFId,@pStepId,@pName,@pDesc,@pHelp,@pCDate,GetDate(),@pImgInd,@pLocX,@pLocY,@pMaxDocs,@pMaxHours,@pStartAt)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_InsWFStep Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_InsWFStep Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_MoveCompleteFolder Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_MoveCompleteFolder Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create procedure zsp_workflow_100_MoveCompleteFolder @pStateId numeric,@pStepId numeric,@pCheckIn datetime,@pAsigned numeric,@pExpDate datetime,@pFolderId numeric as UPDATE WFDOCUMENT SET DO_STATE_ID= @pStateId ,STEP_ID =@pStepId ,CheckIn = @pCheckIn ,USER_ASIGNED= @pAsigned ,EXPIREDATE= @pExpDate')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_MoveCompleteFolder Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_MoveCompleteFolder Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_MoveTaskByDocId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_MoveTaskByDocId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create procedure zsp_workflow_100_MoveTaskByDocId @pStateId numeric,@pStepId numeric,@pCheckIn datetime,@pAsigned numeric,@pExpDate datetime,@pDocId numeric AS UPDATE WFDOCUMENT SET DO_STATE_ID=  @pStateId ,STEP_ID = @pStepId ,CheckIn =  @pCheckIn ,USER_ASIGNED=  @pAsigned ,EXPIREDATE= @pExpDate WHERE DOC_ID = @pDocId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_MoveTaskByDocId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_MoveTaskByDocId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_SaveIcon Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_SaveIcon Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure Zsp_workflow_100_SaveIcon
                '@locX decimal,
                '@locY decimal,
                '@StepId int
                'As
                'UPDATE WFSTEP set LocationX = @locX, LocationY = @locY 
                'Where step_id = @StepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_SaveIcon Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_SaveIcon Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdAsignedUserOpenTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdAsignedUserOpenTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdAsignedUserOpenTask @pAsignedTo numeric ,@pCheckIn numeric ,@pAsignedId numeric ,@pAsgDate datetime ,@pTaskId numeric as UPDATE WFDOCUMENT SET USER_ASIGNED = @pAsignedTo ,CheckIn = @pCheckIn,User_Asigned_By = @pAsignedId ,Date_Asigned_By = @pAsgDate WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdAsignedUserOpenTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdAsignedUserOpenTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_UpdateColor Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_UpdateColor Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure Zsp_workflow_100_UpdateColor
                '@color nvarchar,
                '@stepid int
                'AS
                'UPDATE WFSTEP set Color = @color where step_id = @stepid')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_UpdateColor Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_UpdateColor Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_UpdateExpiredDateTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_UpdateExpiredDateTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE Zsp_workflow_100_UpdateExpiredDateTask
                '@ResultId numeric,
                '@ExpireDate smalldatetime
                'AS
                'UPDATE WFDOCUMENT SET [EXPIREDATE]=@ExpireDate
                'WHERE DOC_ID = @ResultId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_UpdateExpiredDateTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_UpdateExpiredDateTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_UpdateInitialStep Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_UpdateInitialStep Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE Zsp_workflow_100_UpdateInitialStep
                '@InitialStepId numeric,
                '@WFId numeric
                'AS
                'Update WFWorkflow set InitialStepId=@InitialStepId  
                'where work_id= @WFId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_UpdateInitialStep Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_UpdateInitialStep Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_UpdateParamItem Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_UpdateParamItem Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'SET QUOTED_IDENTIFIER OFF
                'GO
                'SET ANSI_NULLS OFF
                'GO
                'exec('CREATE PROCEDURE Zsp_workflow_100_UpdateParamItem
                '@Value varchar(1000),
                '@RuleId numeric,
                '@Item numeric
                'AS
                'Update WFRuleParamItems set value=@Value  
                'where rule_id=@RuleId  And Item = @Item')
                'GO
                'SET ANSI_NULLS ON
                'GO
                'SET QUOTED_IDENTIFIER ON
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_UpdateParamItem Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_UpdateParamItem Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdClearExclusiveTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdClearExclusiveTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdClearExclusiveTask @pTaskId numeric as UPDATE WFDOCUMENT SET EXCLUSIVE=0 WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdClearExclusiveTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdClearExclusiveTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdDelegateTaskByTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdDelegateTaskByTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdDelegateTaskByTask @pStepId numeric, @pAsigned numeric, @pExpDate datetime, @pUserId numeric, @pAsgDate datetime,@pTaskId numeric as UPDATE WFDOCUMENT SET STEP_ID= @pStepId ,USER_ASIGNED = @pAsigned ,EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= @pUserId ,DATE_ASIGNED_BY= @pAsgDate ,CheckIn=NULL WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdDelegateTaskByTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdDelegateTaskByTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdDocTypeLifeCycle Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdDocTypeLifeCycle Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdDocTypeLifeCycle @WfId bit, @DocTypeID numeric as update doc_type set Life_Cycle=@WfID where doc_type_id=@DocTypeID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdDocTypeLifeCycle Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdDocTypeLifeCycle Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdDocumentDelegateTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdDocumentDelegateTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdDocumentDelegateTask @pStepId numeric, @pExpDate datetime, @pTaskId numeric as UPDATE WFDOCUMENT SET STEP_ID= @pStepId  ,USER_ASIGNED = 0 ,EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= 0 ,DATE_ASIGNED_BY=NULL,CheckIn=NULL WHERE Task_ID =  @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdDocumentDelegateTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdDocumentDelegateTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdDoSateByDocIdStateId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdDoSateByDocIdStateId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdDoSateByDocIdStateId @pStateId numeric,@pDocId numeric,@pStepId numeric as Update WFDocument Set do_state_id= @pStateId  where doc_id= @pDocId  and step_id= @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdDoSateByDocIdStateId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdDoSateByDocIdStateId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdExpireDateByDocId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdExpireDateByDocId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdExpireDateByDocId @pExpDate datetime, @pDocId numeric AS UPDATE WFDOCUMENT SET EXPIREDATE=@pExpDate WHERE DOC_ID = @pDocId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdExpireDateByDocId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdExpireDateByDocId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdInitialStateByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdInitialStateByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdInitialStateByStepId @pInitial numeric, @pStepId numeric as UPDATE WFStepStates SET Initial = @pInitial where step_id= @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdInitialStateByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdInitialStateByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdInitialStep Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdInitialStep Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdInitialStep @pIStepId numeric , @pWfid numeric as UPDATE wfworkflow SET  initialstepId = @pIStepId where work_id = @pWfid')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdInitialStep Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdInitialStep Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdInitialStepState Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdInitialStepState Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdInitialStepState @pInitial numeric, @pStateId numeric as UPDATE WFStepStates SET Initial = @pInitial where doc_state_id= @pStateId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdInitialStepState Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdInitialStepState Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdRefreshRate Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdRefreshRate Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdRefreshRate @pInterval numeric , @pWfid numeric as UPDATE wfworkflow SET  refreshrate = @pInterval where work_id = @pWfid')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdRefreshRate Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdRefreshRate Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdSetExclusiveTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdSetExclusiveTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdSetExclusiveTask @pTaskId numeric as UPDATE WFDOCUMENT SET EXCLUSIVE=1 WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdSetExclusiveTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdSetExclusiveTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdState Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdState Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdState @pStepId numeric, @pName varchar(50),@pDesc nvarchar(250), @pInitial numeric,@pStateID numeric as UPDATE WFStepStates SET STEP_ID = @pStepId  , Name = @pName , Description = @pDesc , Initial = @pInitial where Doc_State_Id = @pStateID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdState Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdState Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdStateByDocId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdStateByDocId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdStateByDocId @pStepId numeric, @pAsigned numeric, @pExclusive numeric, @pExpDate datetime, @pUserId numeric, @pDocID numeric as UPDATE WFDOCUMENT SET STEP_ID=@pStepId , USER_ASIGNED = @pAsigned ,EXCLUSIVE = @pExclusive , EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= @pUserId WHERE DOC_ID =@pDocID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdStateByDocId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdStateByDocId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdStateDescription Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdStateDescription Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdStateDescription @pName varchar(50), @pDesc nvarchar(250), @pStateId numeric as Update WFStepStates Set name= @pName , description= @pDesc where doc_state_id= @pStateId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdStateDescription Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdStateDescription Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdStepByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdStepByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdStepByStepId  @pName varchar(50) , @pDescription varchar(100) ,@pHelp varchar(100) , @pEditDate datetime , @pImgInd numeric , @pLocX decimal , @pLocY decimal , @pStart numeric ,@pMaxHours numeric , @pMaxDocs numeric , @pStepId numeric as UPDATE WFSTEP set Name = @pName ,Description = @pDescription ,Help = @pHelp ,EditDate = @pEditDate ,ImageIndex = @pImgInd ,LocationX = @pLocX , LocationY = @pLocY ,StartAtopenDoc = @pStart ,Max_Hours = @pMaxHours ,Max_Docs = @pMaxDocs where step_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdStepByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdStepByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdStetStates Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdStetStates Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create procedure zsp_workflow_100_UpdStetStates @pStepId numeric, @pName varchar(50), @pDesc nvarchar(250), @pInitial numeric, @pStateID numeric AS UPDATE WFStepStates SET STEP_ID = @pStepId  , Name = @pName , Description = @pDesc  where Doc_State_Id = @pStateID;')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdStetStates Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdStetStates Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_UpdWfInitialStepByWfId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_UpdWfInitialStepByWfId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE  Zsp_workflow_100_UpdWfInitialStepByWfId
                '@pIStepId numeric , 
                '@pWfid numeric 
                'AS
                'UPDATE wfworkflow SET  initialstepId = @pIStepId where work_id = @pWfid')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_UpdWfInitialStepByWfId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_UpdWfInitialStepByWfId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdWfName Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdWfName Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc zsp_workflow_100_UpdWfName @pName varchar(50), @pWork_Id numeric as UPDATE wfworkflow SET name = @pName where work_id = @pWork_Id')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdWfName Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdWfName Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_UpdWfNameByWfId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_UpdWfNameByWfId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE Zsp_workflow_100_UpdWfNameByWfId
                '@pName varchar(50), 
                '@pWork_Id numeric 
                'AS
                'UPDATE wfworkflow SET name = @pName where work_id = @pWork_Id')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_UpdWfNameByWfId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_UpdWfNameByWfId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create Zsp_workflow_100_UpdWfRefreshRateByWfId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_100_UpdWfRefreshRateByWfId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE Zsp_workflow_100_UpdWfRefreshRateByWfId 
                '@pInterval numeric , 
                '@pWfid numeric 
                'AS
                'UPDATE wfworkflow SET  refreshrate = @pInterval where work_id = @pWfid')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_100_UpdWfRefreshRateByWfId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_100_UpdWfRefreshRateByWfId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create zsp_workflow_100_UpdWorkFlowByWfId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating zsp_workflow_100_UpdWorkFlowByWfId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('CREATE PROCEDURE zsp_workflow_100_UpdWorkFlowByWfId 
                '@pWStat_Id decimal,
                '@pName varchar(50) ,
                '@pHelp varchar(200),
                '@pDescription varchar(100),
                '@pEditDate datetime,
                '@pRefreshRate numeric,
                '@pStepId numeric, 
                '@pWork_ID numeric 
                'AS
                'UPDATE wfworkflow SET wstat_id = @pWStat_Id , [name] = @pName ,help = @pHelp ,description = @pDescription ,editdate = @pEditDate ,refreshrate = @pRefreshRate ,InitialStepId = @pStepId  where work_id = @pWork_ID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'zsp_workflow_100_UpdWorkFlowByWfId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add zsp_workflow_100_UpdWorkFlowByWfId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create Zsp_workflow_200_getWorkflows Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_200_getWorkflows Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure Zsp_workflow_200_getWorkflows
                'As
                'Select Distinct(Select Name From WfWorkflow Where WfWorkflow.work_id =  t1.work_id) as Workflow, t1.Name as Etapa, t2.DCOUNT as Documentos from Zvw_ZVIEWWFUserSTEPS_100 t1 inner join Zvw_WFDocumentCOUNT_100 t2 On t1.step_id=t2.step_id')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_200_getWorkflows Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_200_getWorkflows Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create Zsp_workflow_200_InsertWFStep Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating Zsp_workflow_200_InsertWFStep Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('Create Procedure Zsp_workflow_200_InsertWFStep
                '@WFID int,
                '@StepId int,
                '@Name nvarchar,
                '@descripcion nvarchar,
                '@ayuda nvarchar,
                '@ImageIndex int,
                '@LocationX decimal,
                '@LocationY decimal,
                '@MaxDocs int,
                '@MaxHours int,
                '@StartAtOpenDoc int,
                '@Color nvarchar,
                '@Height decimal,
                '@Width decimal
                'AS
                'INSERT INTO WFSTEP(work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc,Color,Height,Width) 
                'VALUES(@WFID, @StepId,@Name,@descripcion,@ayuda,GetDate(),GetDate(),@ImageIndex,@LocationX, @LocationY, @MaxDocs, @MaxHours, @StartAtOpenDoc,@Color,@Height,@Width)')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'Zsp_workflow_200_InsertWFStep Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add Zsp_workflow_200_InsertWFStep Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create ZUpdStepStates Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdStepStates Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdStepStates @pStepId numeric  , @pName varchar(50), @pDesc nvarchar(250), @pInitial numeric, @pStateID numeric as
                'UPDATE WFStepStates SET STEP_ID = @pStepId  , Name = @pName , Description = @pDesc , Initial = @pInitial where Doc_State_Id = @pStateID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdStepStates Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdStepStates Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create ZUpdWfByWfID Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWfByWfID Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWfByWfID @pWStat_Id decimal ,@pName varchar(50) ,@pHelp varchar(200),@pDescription varchar(100) ,@pEditDate datetime ,@pRefreshRate numeric ,@pStepId numeric , @pWork_ID numeric as
                'UPDATE wfworkflow SET wstat_id = @pWStat_Id ,name = @pName ,help = @pHelp ,description = @pDescription ,editdate = @pEditDate ,refreshrate = @pRefreshRate ,InitialStepId = @pStepId  where work_id = @pWork_ID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWfByWfID Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWfByWfID Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create ZUpdWFD Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFD Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFD @pAsignedTo numeric ,@pCheckIn numeric ,@pAsignedId numeric ,@pAsgDate datetime ,@pTaskId numeric as
                'UPDATE WFDOCUMENT SET USER_ASIGNED = @pAsignedTo ,CheckIn = @pCheckIn ,User_Asigned_By = @pAsignedId ,Date_Asigned_By = @pAsgDate WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFD Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFD Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZUpdWFDAsignTaskByDocID Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDAsignTaskByDocID Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFDAsignTaskByDocID @pStepId numeric, @pAsigned numeric, @pExclusive numeric, @pExpDate datetime, @pUserId numeric, @pDocID numeric as
                'UPDATE WFDOCUMENT SET STEP_ID=@pStepId , USER_ASIGNED = @pAsigned ,EXCLUSIVE = @pExclusive , EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= @pUserId WHERE DOC_ID =  @pDocID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDAsignTaskByDocID Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDAsignTaskByDocID Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create ZUpdWFDCloseTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDCloseTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFDCloseTask @pTaskId numeric as
                'UPDATE WFDOCUMENT SET USER_ASIGNED = 0 ,CheckIn = NULL ,User_Asigned_By = 0 ,Date_Asigned_By = NULL WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDCloseTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDCloseTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create ZUpdWFDDlgTaskByTaskId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDDlgTaskByTaskId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFDDlgTaskByTaskId @pStepId numeric, @pExpDate datetime, @pTaskId numeric as
                'UPDATE WFDOCUMENT SET STEP_ID= @pStepId  ,USER_ASIGNED = 0 ,EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= 0 ,DATE_ASIGNED_BY=NULL , CheckIn=NULL WHERE Task_ID =  @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDDlgTaskByTaskId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDDlgTaskByTaskId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create ZUpdWFDDlgTaskByTaskId2 Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDDlgTaskByTaskId2 Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFDDlgTaskByTaskId2 @pStepId numeric, @pAsigned numeric, @pExpDate datetime, @pUserId numeric, @pAsgDate datetime,@pTaskId numeric as
                'UPDATE WFDOCUMENT SET STEP_ID= @pStepId ,USER_ASIGNED = @pAsigned ,EXPIREDATE= @pExpDate ,USER_ASIGNED_BY= @pUserId ,DATE_ASIGNED_BY= @pAsgDate ,CheckIn=NULL WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDDlgTaskByTaskId2 Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDDlgTaskByTaskId2 Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZUpdWFDExclusiveTask Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDExclusiveTask Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFDExclusiveTask @pTaskId numeric as
                'UPDATE WFDOCUMENT SET EXCLUSIVE=1 WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDExclusiveTask Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDExclusiveTask Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create ZUpdWFDExclusiveTask0 Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDExclusiveTask0 Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFDExclusiveTask0 @pTaskId numeric as
                'UPDATE WFDOCUMENT SET EXCLUSIVE=0 WHERE Task_ID = @pTaskId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDExclusiveTask0 Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDExclusiveTask0 Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create ZUpdWFDMoveFolderComplete Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDMoveFolderComplete Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create procedure ZUpdWFDMoveFolderComplete @pStateId numeric,@pStepId numeric,@pCheckIn datetime,@pAsigned numeric,@pExpDate datetime,@pFolderId numeric as
                'UPDATE WFDOCUMENT SET DO_STATE_ID= @pStateId ,STEP_ID =@pStepId ,CheckIn = @pCheckIn ,USER_ASIGNED= @pAsigned ,EXPIREDATE= @pExpDate WHERE FOLDER_ID = @pFolderId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDMoveFolderComplete Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDMoveFolderComplete Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create ZUpdWFDMoveTaskByDocId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDMoveTaskByDocId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create procedure ZUpdWFDMoveTaskByDocId @pStateId numeric,@pStepId numeric,@pCheckIn datetime,@pAsigned numeric,@pExpDate datetime,@pDocId numeric AS
                'UPDATE WFDOCUMENT SET DO_STATE_ID=  @pStateId ,STEP_ID = @pStepId ,CheckIn =  @pCheckIn ,USER_ASIGNED=  @pAsigned ,EXPIREDATE= @pExpDate WHERE DOC_ID = @pDocId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDMoveTaskByDocId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDMoveTaskByDocId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZUpdWFDocExpireDateByDocId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDocExpireDateByDocId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFDocExpireDateByDocId @pExpDate datetime, @pDocId numeric AS
                'UPDATE WFDOCUMENT SET EXPIREDATE=@pExpDate WHERE DOC_ID = @pDocId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDocExpireDateByDocId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDocExpireDateByDocId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create ZUpdWFDocExpireDateByDocId1 Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDocExpireDateByDocId1 Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFDocExpireDateByDocId1 @pExpDate datetime, @pDocId numeric AS
                'UPDATE WFDOCUMENT SET EXPIREDATE=@pExpDate WHERE DOC_ID = @pDocId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDocExpireDateByDocId1 Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDocExpireDateByDocId1 Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create ZUpdWFDStateIdByDocStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFDStateIdByDocStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFDStateIdByDocStepId @pStateId numeric,@pDocId numeric,@pStepId numeric as
                'Update WFDocument Set do_state_id= @pStateId  where doc_id= @pDocId  and step_id= @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFDStateIdByDocStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFDStateIdByDocStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create ZUpdWFSSInitialByStateId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFSSInitialByStateId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFSSInitialByStateId @pInitial numeric, @pStateId numeric as
                'UPDATE WFStepStates SET Initial = @pInitial where doc_state_id= @pStateId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFSSInitialByStateId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFSSInitialByStateId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Create ZUpdWFSSInitialByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFSSInitialByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFSSInitialByStepId @pInitial numeric, @pStepId numeric as
                'UPDATE WFStepStates SET Initial = @pInitial where step_id= @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFSSInitialByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFSSInitialByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create ZUpdWFStepByStepId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFStepByStepId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFStepByStepId  @pName varchar(50) , @pDescription varchar(100) ,@pHelp varchar(100) , @pEditDate datetime , @pImgInd numeric , @pLocX decimal , @pLocY decimal , @pStart numeric ,@pMaxHours numeric , @pMaxDocs numeric , @pStepId numeric as
                'UPDATE WFSTEP set Name = @pName ,Description = @pDescription ,Help = @pHelp ,EditDate = @pEditDate ,ImageIndex = @pImgInd ,LocationX = @pLocX , LocationY = @pLocY ,StartAtopenDoc = @pStart ,Max_Hours = @pMaxHours ,Max_Docs = @pMaxDocs where step_id = @pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFStepByStepId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFStepByStepId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO

                '--
                '-- Script To Create ZUpdWFStepStatesByStateId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZUpdWFStepStatesByStateId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZUpdWFStepStatesByStateId @pName varchar(50), @pDesc nvarchar(250), @pStateId numeric as
                'Update WFStepStates Set name= @pName , description= @pDesc where doc_state_id= @pStateId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZUpdWFStepStatesByStateId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZUpdWFStepStatesByStateId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                End
                'GO

                '--
                '-- Script To Create ZWFDELETEWorkFlow Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZWFDELETEWorkFlow Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'SET QUOTED_IDENTIFIER OFF
                'GO
                'SET ANSI_NULLS OFF
                'GO
                'exec('CREATE PROCEDURE ZWFDELETEWorkFlow
                '@WF_ID int
                'AS

                '       /*WFRuleParam*/
                '        DELETE FROM WFRuleParam WHERE RULE_ID IN (SELECT ID FROM WFRULE WHERE STEP_ID IN (SELECT STEP_ID FROM WFSTEP WHERE WORK_ID=@WF_Id))

                '        /*WFRule*/
                '        DELETE FROM WFRule WHERE STEP_ID IN (SELECT STEP_ID FROM WFSTEP WHERE WORK_ID=@WF_Id)

                '        /*WFDocument*/
                '        DELETE FROM WFDocument WHERE STEP_ID IN (SELECT STEP_ID FROM WFSTEP WHERE WORK_ID=@WF_Id)

                '        /*WFDocumentState*/
                '        DELETE FROM WFDocumentState WHERE STEP_ID IN (SELECT STEP_ID FROM WFSTEP WHERE WORK_ID=@WF_Id)

                '        /*WF_DT*/
                '        DELETE FROM WF_DT WHERE WFID =@WF_Id

                '        /*WFActionsStepRules*/
                '        DELETE FROM WFActionsStepRules WHERE WORK_ID = @WF_Id

                '        /*WFStep*/
                '        DELETE FROM WFStep WHERE WORK_ID = @WF_Id

                '        /*WFWorkflow*/
                '        DELETE FROM WFWorkflow WHERE WORK_ID = @WF_Id')
                'GO
                'SET ANSI_NULLS ON
                'GO
                'SET QUOTED_IDENTIFIER ON
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZWFDELETEWorkFlow Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZWFDELETEWorkFlow Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            End
                'GO

                '--
                '-- Script To Delete ZWFGETRULES Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Deleting ZWFGETRULES Procedure'
                'GO

                'DROP PROCEDURE [ZWFGETRULES]
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZWFGETRULES Procedure Deleted Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Delete ZWFGETRULES Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create ZWFGetRulesParamItemBySId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZWFGetRulesParamItemBySId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZWFGetRulesParamItemBySId @pStepId numeric as
                'select * from ZWFViewRulesParamsitems where StepId=@pStepId')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZWFGetRulesParamItemBySId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZWFGetRulesParamItemBySId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        End
                'GO

                '--
                '-- Script To Create ZWfUpdDtLCByDtId Procedure In HERNAN\SQL_TEST_RC1.ABN_PRODUCCION
                '-- Generated Lunes, Febrero 12, 2007, at 10:29 AM
                '--
                '-- Please backup HERNAN\SQL_TEST_RC1.ABN_PRODUCCION before executing this script
                '--


                'BEGIN TRANSACTION
                'GO
                'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                'GO

                'PRINT 'Creating ZWfUpdDtLCByDtId Procedure'
                'GO

                'SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
                'GO

                'SET NUMERIC_ROUNDABORT OFF
                'GO

                'exec('create proc ZWfUpdDtLCByDtId @WfId bit, @DocTypeID numeric as update doc_type set Life_Cycle=@WfID where doc_type_id=@DocTypeID')
                'GO

                'IF @@ERROR <> 0
                '   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
                'GO

                'IF @@TRANCOUNT = 1
                'BEGIN
                '   PRINT 'ZWfUpdDtLCByDtId Procedure Added Successfully'
                '   COMMIT TRANSACTION
                'END ELSE
                'BEGIN
                '   PRINT 'Failed To Add ZWfUpdDtLCByDtId Procedure'
                '                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    End
                'GO")


            End If

            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function
#End Region


End Class
