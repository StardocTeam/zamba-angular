Imports Zamba.Servers

Public Class PAQ_CreateStore_sp_GetLogInformation
    Inherits ZPaq
    Implements IPAQ


    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Paquete que crea 1 Stored Procedure que se encarga de traer información para el log de una tarea"
        End Get
    End Property

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateStore_sp_GetLogInformation"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStore_sp_GetLogInformation
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
            Return 72
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("21/12/2006")
        End Get
    End Property

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute

        Select Case Server.ServerType
            Case Server.DBTYPES.MSSQLServer, Server.DBTYPES.MSSQLServer7Up

                Dim QueryBuilder As New System.Text.StringBuilder()
                QueryBuilder.Append("if exists (select * from sysobjects where id = object_id(N'[sp_GetLogInformation]') and OBJECTPROPERTY(id, N'IsProcedure') = 1) ")
                QueryBuilder.Append("drop procedure [sp_GetLogInformation] GO SET QUOTED_IDENTIFIER OFF GO SET ANSI_NULLS OFF GO ")
                QueryBuilder.Append("CREATE PROCEDURE sp_GetLogInformation ( @taskId as numeric(10,0) ) AS ")
                QueryBuilder.Append("Select WFDocument.Name as DocumentName, WFDocument.DOC_TYPE_ID as DocTypeId, DOC_TYPE.DOC_TYPE_NAME as DocTypeName, ")
                QueryBuilder.Append("WFDocument.Folder_Id as FolderId, WFDocument.Step_Id as StepId, WFStep.Name, WFDocument.Work_Id, WFWorkflow.Name ")
                QueryBuilder.Append("From WFDocument Inner Join DOC_TYPE ON WFDocument.DOC_TYPE_ID = DOC_TYPE.DOC_TYPE_ID Inner Join WFStep ")
                QueryBuilder.Append("ON WFDocument.Step_Id = WFStep.step_Id Inner Join WFWorkflow ON WFDocument.work_id = WFWorkflow.work_id ")
                QueryBuilder.Append("Where WFDocument.Doc_Id = @taskId GO SET QUOTED_IDENTIFIER OFF GO SET ANSI_NULLS ON GO ")

                Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

                QueryBuilder.Remove(0, QueryBuilder.Length)
                QueryBuilder = Nothing
        End Select


    End Function

#End Region
    
End Class
