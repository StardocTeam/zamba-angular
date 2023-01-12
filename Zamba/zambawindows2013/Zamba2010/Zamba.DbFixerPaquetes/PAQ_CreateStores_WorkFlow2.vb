Imports zamba.servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32
Imports System.IO
Public Class PAQ_CreateStores_WorkFlow2
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub


#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateStores_WorkFlow2"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStores_WorkFlow2
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("22/12/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea Stores varios para workflow, en SQL Server"
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("22/12/2006")
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
            Return 87
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

            If ZPaq.IfExists("ZDelWfByWfId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure ZDelWfByWfId  ")
            Else
                'Create
                sql.Append("CREATE Procedure ZDelWfByWfId  ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pWork_ID numeric as DELETE wfworkflow where work_id = @pWork_ID")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)


            If ZPaq.IfExists("ZDelWFDByTaskId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure ZDelWFDByTaskId  ")
            Else
                'Create
                sql.Append("CREATE Procedure  ZDelWFDByTaskId  ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pTaskId numeric as DELETE WFDOCUMENT WHERE Task_ID = @pTaskId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZDelWFStepByStepId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure ZDelWFStepByStepId   ")
            Else
                'Create
                sql.Append("CREATE Procedure ZDelWFStepByStepId   ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pStepId numeric as DELETE wfSTEP where STEP_id = @pStepId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZDelWFStepStatesByStateId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure ZDelWFStepStatesByStateId  ")
            Else
                'Create
                sql.Append("CREATE Procedure  ZDelWFStepStatesByStateId ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pStateID numeric AS DELETE WFStepStates where doc_state_Id = @pStateID")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZGetAllWF ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure ZGetAllWF   ")
            Else
                'Create
                sql.Append("CREATE Procedure  ZGetAllWF  ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("as SELECT * FROM WFworkflow")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZGetAllWFStep ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  ZGetAllWFStep  ")
            Else
                'Create
                sql.Append("CREATE Procedure  ZGetAllWFStep  ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append(" as Select * from wfstep")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZGetViewWFStepsByWfID ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  ZGetViewWFStepsByWfID  ")
            Else
                'Create
                sql.Append("CREATE Procedure  ZGetViewWFStepsByWfID  ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pWFId numeric as Select * from ZViewWFSTEPS where WORK_ID = @pWFId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZGetVWFStepsByStepId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  ZGetVWFStepsByStepId  ")
            Else
                'Create
                sql.Append("CREATE Procedure ZGetVWFStepsByStepId   ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pStepId numeric as Select * from ZViewWFSTEPS where step_id = @pStepId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZGetWFDStepIdByDocId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure ZGetWFDStepIdByDocId   ")
            Else
                'Create
                sql.Append("CREATE Procedure ZGetWFDStepIdByDocId   ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pDocId numeric  as select step_Id from wfdocument where doc_id= @pDocId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZGetWFSSByStepId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure ZGetWFSSByStepId   ")
            Else
                'Create
                sql.Append("CREATE Procedure ZGetWFSSByStepId   ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pStepId numeric as Select * from WFStepStates where step_id = @pStepId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZGetWFSSByStepIdStateId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  ZGetWFSSByStepIdStateId  ")
            Else
                'Create
                sql.Append("CREATE Procedure  ZGetWFSSByStepIdStateId  ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pStateId numeric, @pStepId numeric as Select * from WFStepStates where doc_state_id=@pStateId  or step_id = @pStepId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZGetWFStepByWorkId ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  ZGetWFStepByWorkId  ")
            Else
                'Create
                sql.Append("CREATE Procedure ZGetWFStepByWorkId   ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pWorkId numeric as Select * from wfstep where work_id= @pWorkId")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZInsWFDocument1 ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure ZInsWFDocument1   ")
            Else
                'Create
                sql.Append("CREATE Procedure ZInsWFDocument1   ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pFolderId numeric ,@pStepId numeric ,@pDocId numeric , @pDTId numeric ,@pStateId numeric , ")
            sql.Append("@pName varchar(250) , @pICONID numeric , @pCheckIn datetime, @pAsigned numeric ,@pExclusive numeric, ")
            sql.Append("@pExpireDate datetime as INSERT INTO WFDOCUMENT ")
            sql.Append("(FOLDER_ID,STEP_ID,DOC_ID,DOC_TYPE_ID,Do_State_Id,NAME,ICONID,CheckIn,User_asigned,exclusive,ExpireDate) ")
            sql.Append("VALUES (@pFolderId ,@pStepId ,@pDocId , @pDTId ,@pStateId , @pName , @pICONID , @pCheckIn,@pAsigned,@pExclusive, @pExpireDate)")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZInsWFRulesHST ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  ZInsWFRulesHST  ")
            Else
                'Create
                sql.Append("CREATE Procedure  ZInsWFRulesHST  ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append(" @pFolderId numeric , @pStepId numeric ,@pDoc_Id numeric ,@pDocTypeid numeric, @pRuleId numeric ,@pResult numeric,")
            sql.Append("@pUsrId numeric ,@pEjecDate datetime ,@pData nvarchar(400) ")
            sql.Append("as INSERT INTO WFRULESHST (FOLDER_ID,STEP_ID,DOC_ID,DOC_TYPE_ID,Rule_Id,Result,User_Id,Ejecution_Date,Data) ")
            sql.Append("VALUES (@pFolderId,@pStepId,@pDoc_Id,@pDocTypeid,@pRuleId,@pResult,@pUsrId,@pEjecDate,@pData)")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZInsWFStepHST ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure ZInsWFStepHST   ")
            Else
                'Create
                sql.Append("CREATE Procedure ZInsWFStepHST   ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pDocId numeric , @pDocTypeid numeric , @pFolderId numeric ,@pStepId numeric ,@pCiDocStateId numeric ,")
            sql.Append("@pcheckin datetime ,@pChkOut datetime ,@pUChkIn numeric ,@pUChkOut numeric ,@pCoDocStateId numeric AS ")
            sql.Append("INSERT INTO WFSTEPHST (DOC_ID,DOC_TYPE_ID,FOLDER_ID,STEP_ID,ci_Doc_State_Id,checkin,checkout ,ucheckin,ucheckout ,co_doc_state_id) ")
            sql.Append("VALUES(@pDocId,@pDocTypeid,@pFolderId,@pStepId,@pCiDocStateId,@pcheckin,@pChkOut,@pUChkIn,@pUChkOut, @pCoDocStateId)")

            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            '------------------------------------------
            If ZPaq.IfExists("ZInsWfStepStates ", Tipo.StoredProcedure, False) = True Then
                'Alter
                sql.Append("ALTER Procedure  ZInsWfStepStates  ")
            Else
                'Create
                sql.Append("CREATE Procedure  ZInsWfStepStates  ")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("@pStateID numeric, @pDesc nvarchar(250), @pStepId numeric, @pName varchar(50), @pInitial numeric AS INSERT INTO WFStepStates (Doc_State_id,Description,Step_Id,Name,Initial) ")
            sql.Append("VALUES ( @pStateID , @pDesc , @pStepId , @pName , @pInitial )")
            sql2.Append(sql.ToString)
            sql2.Append(ControlChars.NewLine)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)


            sw.WriteLine("")
            sw.WriteLine(sql2.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

#End Region
End Class
