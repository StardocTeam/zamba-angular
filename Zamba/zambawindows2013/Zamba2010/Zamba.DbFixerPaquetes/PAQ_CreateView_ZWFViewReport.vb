Imports Zamba.Servers

Public Class PAQ_CreateView_ZWFViewReport
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub
#Region "Propiedades"
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("27/09/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
#End Region

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea la Vista para el informe de Workflow"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        If Server.ServerType = Server.DBTYPES.MSSQLServer7Up OrElse Server.ServerType = Server.DBTYPES.MSSQLServer Then
            Return ExecuteSQL(GenerateScripts)
        Else
            Return ExecuteOracle(GenerateScripts)
        End If
    End Function

    Private Function ExecuteSQL(ByVal scripts As Boolean) As Boolean
        'TODO Store: SP_ZWFViewReport
        Dim sb As New System.Text.StringBuilder
        Try
            sb.Append("CREATE VIEW ZWFViewReport")
            sb.Append(ControlChars.NewLine)
            sb.Append("AS")
            sb.Append(ControlChars.NewLine)
            sb.Append("SELECT *,(SELECT name FROM usrtable WHERE usrtable.id = wfdocument.user_asigned) AS NombreUsuario,(SELECT name FROM wfstep WHERE wfstep.step_id = wfdocument.step_id) AS Etapa,")
            sb.Append(ControlChars.NewLine)
            sb.Append("(SELECT name FROM WFWorkflow WHERE WFWorkflow.work_id =(SELECT work_id FROM wfstep")
            sb.Append(ControlChars.NewLine)
            sb.Append("WHERE wfstep.step_Id = wfdocument.step_Id)) AS Workflow")
            sb.Append(ControlChars.NewLine)
            sb.Append("FROM WFDocument")
            If scripts Then
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\Scripts.txt", True)
                sw.WriteLine(sb.ToString)
                sw.Close()
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, sb.ToString)
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
            sb = Nothing
        End Try
    End Function
    Private Function ExecuteOracle(ByVal scripts As Boolean) As Boolean
        'TODO Store: SP_ZWFViewReport
        Dim sb As New System.Text.StringBuilder
        Try
            sb.Append("CREATE OR REPLACE VIEW ZWFViewReport")
            sb.Append(ControlChars.NewLine)
            sb.Append("AS")
            sb.Append(ControlChars.NewLine)
            sb.Append("SELECT *,(SELECT name FROM usrtable WHERE usrtable.id = wfdocument.user_asigned) AS NombreUsuario,(SELECT name FROM wfstep WHERE wfstep.step_id = wfdocument.step_id) AS Etapa,")
            sb.Append(ControlChars.NewLine)
            sb.Append("(SELECT name FROM WFWorkflow WHERE WFWorkflow.work_id =(SELECT work_id FROM wfstep")
            sb.Append(ControlChars.NewLine)
            sb.Append("WHERE wfstep.step_Id = wfdocument.step_Id)) AS Workflow")
            sb.Append(ControlChars.NewLine)
            sb.Append("FROM WFDocument")
            If scripts Then
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\Scripts.txt", True)
                sw.WriteLine(sb.ToString)
                sw.Close()
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, sb.ToString)
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
            sb = Nothing
        End Try
    End Function
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_workflowViewReport"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateView_ZWFViewReport
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
            Return 68
        End Get
    End Property

End Class
