Imports zamba.servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32

Public Class PAQ_CreateView_ZVW_WFHISTORYCI_200
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateView_ZVW_WFHISTORYCI_200"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateView_ZVW_WFHISTORYCI_200
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("17/11/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea las vistas para workflow:Zwv_WfHistoryCICO_200 y Zwv_WfHistoryCI_200"
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("17/11/2006")
        End Get
    End Property
#End Region
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Try
            Dim sql As System.Text.StringBuilder
            sql = New System.Text.StringBuilder
            If ZPaq.IfExists("ZVW_WFHISTORYCI_200", Tipo.View, False) = True Then
                'Alter
                sql.Append("ALTER View ZVW_WFHISTORYCI_200")
            Else
                'Create
                sql.Append("CREATE View ZVW_WFHISTORYCI_200")
            End If
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
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            '------****2da vista****-----------------------------------------------
            If ZPaq.IfExists("ZWV_WFHISTORYCICO_200", Tipo.View, False) = True Then
                'Alter
                sql.Append("ALTER View ZWV_WFHISTORYCICO_200")
            Else
                'Create
                sql.Append("CREATE View ZWV_WFHISTORYCICO_200")
            End If
            sql.Append(ControlChars.NewLine)
            sql.Append("As")
            sql.Append(ControlChars.NewLine)
            sql.Append("SELECT TOP 100 PERCENT ")
            sql.Append(ControlChars.NewLine)
            sql.Append("'Id Documento' =")
            sql.Append(ControlChars.NewLine)
            sql.Append("CASE")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN WFStepHst.doc_Id IS NULL THEN 'Sin Etapa'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		ELSE CAST(WFStepHst.doc_Id AS VARCHAR(20))")
            sql.Append(ControlChars.NewLine)
            sql.Append("	END,")
            sql.Append(ControlChars.NewLine)
            sql.Append("	'Etapa' =")
            sql.Append(ControlChars.NewLine)
            sql.Append("	CASE")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN WFStep.Name IS NULL THEN 'Sin Etapa'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		ELSE CAST(WFStep.Name AS VARCHAR(20))")
            sql.Append(ControlChars.NewLine)
            sql.Append("	END,")
            sql.Append(ControlChars.NewLine)
            sql.Append("	'Ingreso'=")
            sql.Append(ControlChars.NewLine)
            sql.Append("	CASE")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN WFStepHst.CheckIn IS null THEN 'Sin Fecha'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN WFStepHst.CheckIn = 0 THEN 'Sin Fecha'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		ELSE CAST(WFStepHst.CheckIn AS VARCHAR(40))")
            sql.Append(ControlChars.NewLine)
            sql.Append("	END,")
            sql.Append(ControlChars.NewLine)
            sql.Append("	'Estado Inicial' =")
            sql.Append(ControlChars.NewLine)
            sql.Append("	CASE")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN WFStepHst.CI_Doc_state_id IS null THEN 'Sin Estado'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN WFStepHst.CI_Doc_state_id = 0 THEN 'Sin Estado'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		ELSE CAST(WFStepHst.CI_Doc_state_id AS VARCHAR(15))")
            sql.Append(ControlChars.NewLine)
            sql.Append("	END,")
            sql.Append(ControlChars.NewLine)
            sql.Append("	'Egreso'=")
            sql.Append(ControlChars.NewLine)
            sql.Append("	CASE")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN WFStepHst.CheckOut IS null THEN 'Sin Fecha'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN WFStepHst.CheckOut = 0 THEN 'Sin Fecha'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		ELSE CAST(WFStepHst.CheckOut AS VARCHAR(40))")
            sql.Append(ControlChars.NewLine)
            sql.Append("	END,")
            sql.Append(ControlChars.NewLine)
            sql.Append("	'Usuario Egreso'=")
            sql.Append(ControlChars.NewLine)
            sql.Append("	CASE")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN USRTABLE.name IS NULL THEN 'Sin Usuario'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		ELSE CAST(USRTABLE.name AS VARCHAR(10))")
            sql.Append(ControlChars.NewLine)
            sql.Append("	END,")
            sql.Append(ControlChars.NewLine)
            sql.Append("	'Estado Final' =")
            sql.Append(ControlChars.NewLine)
            sql.Append("	CASE")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN WFStep.name IS NULL THEN 'Sin Estado'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		ELSE CAST(WFStep.name AS VARCHAR(15))")
            sql.Append(ControlChars.NewLine)
            sql.Append("	END,")
            sql.Append(ControlChars.NewLine)

            'sql.Append("	'Id Documento'=")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	CASE")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		WHEN WFStepHst.Doc_Id IS NULL THEN 'Sin Id'")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		WHEN WFStepHst.Doc_Id = 0 THEN 'Sin Id'")
            'sql.Append(ControlChars.NewLine))
            'sql.Append("		ELSE CAST(WFStepHst.Doc_Id AS VARCHAR(10))")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	END,")
            'sql.Append(ControlChars.NewLine)

            'sql.Append("	'Id Step' =")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	CASE")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		WHEN WFStepHst.Step_Id  IS null THEN 'Sin Id'")
            ''sql.Append(ControlChars.NewLine)
            'sql.Append("		WHEN WFStepHst.Step_Id  = 0 THEN 'Sin Id'")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		ELSE CAST(WFStepHst.Step_Id AS VARCHAR(10))")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	END,")
            'sql.Append(ControlChars.NewLine)
            sql.Append("	'Tipo de Documento' =")
            sql.Append(ControlChars.NewLine)
            sql.Append("	CASE")
            sql.Append(ControlChars.NewLine)
            sql.Append("		WHEN DOC_TYPE.DOC_TYPE_NAME IS NULL THEN 'Sin Tipo'")
            sql.Append(ControlChars.NewLine)
            sql.Append("		ELSE CAST(DOC_TYPE.DOC_TYPE_NAME AS VARCHAR(40))")
            sql.Append(ControlChars.NewLine)
            sql.Append("	END")
            sql.Append(ControlChars.NewLine)

            'sql.Append("	'Id Usuario Check In' =")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	CASE")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		WHEN USRTABLE.name IS NULL THEN 'Sin Usuario'")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		WHEN WFStepHst.UCheckIn = 0 THEN 'Sin Usuario'")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		ELSE CAST(USRTABLE.name AS VARCHAR(10))")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	END,")
            'sql.Append(ControlChars.NewLine)

            'sql.Append("	'UsuarioCI' =")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	CASE")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		WHEN USRTABLE.NAME IS NULL THEN 'Sin Usuario'")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		ELSE CAST(USRTABLE.NAME AS VARCHAR(20))")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	END,")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	'EstadoCI' =")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	CASE")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		WHEN WFStepStates.Name IS NULL THEN 'Sin Usuario'")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("		ELSE CAST(WFStepStates.Name AS VARCHAR(20))")
            'sql.Append(ControlChars.NewLine)
            'sql.Append("	END")
            'sql.Append(ControlChars.NewLine)
            sql.Append("FROM")
            sql.Append(ControlChars.NewLine)
            sql.Append("	WFStepHst")
            sql.Append(ControlChars.NewLine)
            sql.Append("		INNER JOIN DOC_TYPE")
            sql.Append(ControlChars.NewLine)
            sql.Append("			ON WFStepHst.Doc_Type_Id = DOC_TYPE.DOC_TYPE_ID")
            sql.Append(ControlChars.NewLine)
            sql.Append("		INNER JOIN WFStep")
            sql.Append(ControlChars.NewLine)
            sql.Append("			ON WFStepHst.Step_Id = WFStep.step_Id ")
            sql.Append(ControlChars.NewLine)
            sql.Append("		LEFT OUTER JOIN USRTABLE")
            sql.Append(ControlChars.NewLine)
            sql.Append("			ON WFStepHst.UCheckIn = USRTABLE.ID")
            sql.Append(ControlChars.NewLine)
            sql.Append("		LEFT OUTER JOIN WFStepStates")
            sql.Append(ControlChars.NewLine)
            sql.Append("			ON WFStepHst.CI_Doc_state_id = WFStepStates.Doc_State_ID")
            sql.Append(ControlChars.NewLine)
            sql.Append("ORDER BY")
            sql.Append(ControlChars.NewLine)
            sql.Append("	WFStepHst.Doc_Id")
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
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
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            '  Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
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
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

        Catch ex As Exception
            Return
        End Try
    End Sub

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.Installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.Orden
        Get
            Return 67
        End Get
    End Property

End Class
