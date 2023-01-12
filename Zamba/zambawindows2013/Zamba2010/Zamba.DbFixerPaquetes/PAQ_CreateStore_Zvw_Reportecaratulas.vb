Imports zamba.servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32
Public Class PAQ_CreateStore_Zvw_Reportecaratulas
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateStore_Zvw_Reportecaratulas"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStore_Zvw_Reportecaratulas
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("05/12/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea Vista Reportecaratulas "
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("05/12/2006")
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
            Return 83
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Try
            Dim sql As System.Text.StringBuilder
            sql = New System.Text.StringBuilder
            If ZPaq.IfExists("Zvw_Reportecaratulas", Tipo.View, False) = True Then
                'Alter
                sql.Append("ALTER View Zvw_Reportecaratulas")
            Else
                'Create
                sql.Append("CREATE View Zvw_Reportecaratulas")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("As")
            sql.Append(ControlChars.NewLine)
            sql.Append("select     b.ID as " & Chr(34) & "Nro de Caratula" & Chr(34) & ", ")
            sql.Append(ControlChars.NewLine)
            sql.Append("            d.Doc_Type_name as " & Chr(34) & "Nombre del Tipo de Documento" & Chr(34) & ", ")
            sql.Append(ControlChars.NewLine)
            sql.Append("            ( count(b.ID) - 1 ) as " & Chr(34) & "Nro de Replicas" & Chr(34) & ",")
            sql.Append(ControlChars.NewLine)
            sql.Append("            count(b.Doc_Type_Id) as " & Chr(34) & "Nro de Documentos" & Chr(34) & ",")
            sql.Append(ControlChars.NewLine)
            sql.Append("            b.SCANNED as " & Chr(34) & "Escaneado" & Chr(34) & ",")
            sql.Append(ControlChars.NewLine)
            sql.Append("            b.box as " & Chr(34) & "Nro de Caja" & Chr(34) & ",")
            sql.Append(ControlChars.NewLine)
            sql.Append("            ( count(b.ID) - ( count(b.ID) - 1 ) ) as " & Chr(34) & "Diferencia" & Chr(34) & ",")
            sql.Append(ControlChars.NewLine)
            sql.Append("            b.Doc_Type_Id as " & Chr(34) & "Tipo de Documento" & Chr(34) & ",")
            sql.Append(ControlChars.NewLine)
            sql.Append("            b.doc_id as " & Chr(34) & "Nro de Documento" & Chr(34))
            sql.Append(ControlChars.NewLine)
            sql.Append("from        zbarcode b join Doc_Type d")
            sql.Append(ControlChars.NewLine)
            sql.Append("on          b.Doc_Type_id = d.Doc_Type_Id ")
            sql.Append(ControlChars.NewLine)
            sql.Append("group by   b.ID,")
            sql.Append(ControlChars.NewLine)
            sql.Append("            b.Doc_Type_Id,")
            sql.Append(ControlChars.NewLine)
            sql.Append("            b.BOX,")
            sql.Append(ControlChars.NewLine)
            sql.Append("            b.SCANNED,")
            sql.Append(ControlChars.NewLine)
            sql.Append("            d.doc_type_name,")
            sql.Append(ControlChars.NewLine)
            sql.Append("            b.Doc_Type_Id,")
            sql.Append(ControlChars.NewLine)
            sql.Append("")
            sql.Append(ControlChars.NewLine)
            sql.Append("            b.Doc_Id")

            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub ViewZWV_WFHISTORYCICO_200()
        Dim sql As System.Text.StringBuilder
        Try
            sql = New System.Text.StringBuilder
            If ZPaq.IfExists("ZWV_WFHISTORYCICO_200", Tipo.View, False) = True Then
                'Alter
                sql.Append("ALTER View ZWV_WFHISTORYCICO_200")
            Else
                'Create
                sql.Append("CREATE View ZWV_WFHISTORYCICO_200")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append(ControlChars.NewLine)
            sql.Append("--aaaaaa")
            sql.Append(ControlChars.NewLine)
            sql.Append("As")
            sql.Append(ControlChars.NewLine)
            sql.Append("SELECT ZVW_WFHISTORYCI_200.[Id Documento], ZVW_WFHISTORYCI_200.[Check In], ZVW_WFHISTORYCI_200.[Check Out], ")
            sql.Append(ControlChars.NewLine)
            sql.Append("       ZVW_WFHISTORYCI_200.IdEtapa, ZVW_WFHISTORYCI_200.[Estado Inicial], ZVW_WFHISTORYCI_200.[Estado Final], ")
            sql.Append(ControlChars.NewLine)
            sql.Append("       ZVW_WFHISTORYCI_200.[Id Usuario Check In], ZVW_WFHISTORYCI_200.[Id Usuario Check Out], ")
            sql.Append(ControlChars.NewLine)
            sql.Append("       ZVW_WFHISTORYCI_200.DOC_TYPE_NAME AS [Tipo Documento], ZVW_WFHISTORYCI_200.Etapa, ")
            sql.Append(ControlChars.NewLine)
            sql.Append("       ZVW_WFHISTORYCI_200.UsuarioCI, ")
            sql.Append(ControlChars.NewLine)
            sql.Append("       ZVW_WFHISTORYCI_200.EstadoCI, USRTABLE.NAME AS UsuarioCO, WFStepStates.Name AS EstadoCO")
            sql.Append(ControlChars.NewLine)
            sql.Append("FROM   ZVW_WFHISTORYCI_200 LEFT OUTER JOIN")
            sql.Append(ControlChars.NewLine)
            sql.Append("       USRTABLE ON ZVW_WFHISTORYCI_200.[Id Usuario Check Out] = USRTABLE.ID LEFT OUTER JOIN")
            sql.Append(ControlChars.NewLine)
            sql.Append("       WFStepStates ON ZVW_WFHISTORYCI_200.[Estado Final] = WFStepStates.Doc_State_ID")
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

        Catch ex As Exception
            Return
        End Try
    End Sub

    Private Sub ViewZVW_WFHISTORYCI_200()
        Dim sql As System.Text.StringBuilder
        Try
            sql = New System.Text.StringBuilder
            If ZPaq.IfExists("ZVW_WFHISTORYCI_200", Tipo.View, False) = True Then
                'Alter
                sql.Append("ALTER View ZVW_WFHISTORYCI_200")
            Else
                'Create
                sql.Append("CREATE View ZVW_WFHISTORYCI_200")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("--aaaaaa")
            sql.Append(ControlChars.NewLine)
            sql.Append("As")
            sql.Append(ControlChars.NewLine)
            sql.Append("SELECT     TOP 100 PERCENT WFStepHst.Doc_Id AS [Id Documento], WFStepHst.CheckIn AS [Check In], WFStepHst.CheckOut AS [Check Out], ")
            sql.Append(ControlChars.NewLine)
            sql.Append("            WFStepHst.Step_Id AS IdEtapa, WFStepHst.CI_Doc_state_id AS [Estado Inicial], WFStepHst.CO_Doc_state_id AS [Estado Final], ")
            sql.Append(ControlChars.NewLine)
            sql.Append("            WFStepHst.UCheckIn AS [Id Usuario Check In], WFStepHst.UCheckOut AS [Id Usuario Check Out], ")
            sql.Append(ControlChars.NewLine)
            sql.Append("            DOC_TYPE.DOC_TYPE_NAME, WFStep.Name AS Etapa, USRTABLE.NAME AS UsuarioCI, WFStepStates.Name AS EstadoCI")
            sql.Append(ControlChars.NewLine)
            sql.Append("FROM        WFStepHst INNER JOIN ")
            sql.Append(ControlChars.NewLine)
            sql.Append("            DOC_TYPE ON WFStepHst.Doc_Type_Id = DOC_TYPE.DOC_TYPE_ID INNER JOIN")
            sql.Append(ControlChars.NewLine)
            sql.Append("            WFStep ON WFStepHst.Step_Id = WFStep.step_Id LEFT OUTER JOIN")
            sql.Append(ControlChars.NewLine)
            sql.Append("            USRTABLE ON WFStepHst.UCheckIn = USRTABLE.ID LEFT OUTER JOIN ")
            sql.Append(ControlChars.NewLine)
            sql.Append("            WFStepStates ON WFStepHst.CI_Doc_state_id = WFStepStates.Doc_State_ID")
            sql.Append(ControlChars.NewLine)
            sql.Append("ORDER BY    WFStepHst.Doc_Id")
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

        Catch ex As Exception
            Return
        End Try
    End Sub
#End Region

End Class
