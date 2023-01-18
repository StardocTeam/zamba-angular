Imports zamba.Servers
Public Class PAQ_AlterTable_DOC_I
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega la clave primaria en la columna 'DOC_ID' de cada tabla DOC_I."
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTable_DOC_I"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_DOC_I
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("09/03/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

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
            Return 43
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Try
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                AddKeySQL(GenerateScripts)
            Else
                AddKeyOracle(GenerateScripts)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return True
    End Function
    Private Sub AddKeySQL(ByVal generatescripts As Boolean)
        Dim sql As String
        Dim j As Int32
        Dim Name As String = "PrmKey"
        Dim tabla As DataTable = GetIDS()
        For j = 0 To tabla.Rows.Count - 1
            Try
                sql = "ALTER TABLE DOC_I" & tabla.Rows(j).Item(0).ToString() & " ADD CONSTRAINT " & Name & (j + 1).ToString & "  PRIMARY KEY CLUSTERED(DOC_ID)ON [PRIMARY]"
                If generatescripts = False Then
                    If ZPaq.ExisteTabla("DOC_I" & tabla.Rows(j).Item(0).ToString()) Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Next
    End Sub
    Private Sub AddKeyOracle(ByVal generatescipts As Boolean)
        Dim sql As String
        Dim j As Int32
        Dim Name As String = "PrmKey"
        Dim tabla As DataTable = GetIDS()
        For j = 0 To tabla.Rows.Count - 1
            Try
                sql = "ALTER TABLE " & Chr(34) & "DOC_I" & tabla.Rows(j).Item(0).ToString() & Chr(34) & " ADD (CONSTRAINT " & Name & (j + 1).ToString & " PRIMARY KEY(" & Chr(34) & "DOC_ID" & Chr(34) & "))"
                If generatescipts = False Then
                    If ZPaq.ExisteTabla("DOC_I" & tabla.Rows(j).Item(0).ToString()) Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Next
    End Sub
    Private Function GetIDS() As DataTable
        'TODO store: SPGetDoc_type_id
        Try
            Dim sql As String = "Select DOC_TYPE_ID from doc_Type order by DOC_TYPE_ID"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            If Not IsNothing(ds) Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        Catch
            Return Nothing
        End Try
    End Function
#End Region

End Class
