Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_AlterTables_DOC_T
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        'TODO store: SPGetDoc_type_id
        Dim ds As New DataSet
        Dim ok As Int16 = 0, errors As Int16 = 0

        ds = Servers.Server.Con.ExecuteDataset(CommandType.Text, "Select doc_type_id from doc_type")
        For i As Int16 = 0 To Convert.ToInt16(ds.Tables(0).Rows.Count - 1)
            Try
                Dim id As Int32, DITable As String, DTTable As String
                id = Convert.ToInt32(ds.Tables(0).Rows.Item(i)("doc_type_id"))
                DTTable = "DOC_T" & id.ToString.Trim
                DITable = "DOC_I" & id.ToString.Trim
                If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, "ALTER TABLE " & DITable & " ADD FOREIGN KEY(DOC_ID) REFERENCES " & DTTable & "(doc_ID) ON DELETE CASCADE")
                    Server.Con.ExecuteNonQuery(CommandType.Text, "ALTER TABLE " & DITable & " ADD FOREIGN KEY(DOC_ID) REFERENCES " & DTTable & "(doc_ID) ON UPDATE CASCADE")
                Else
                    Server.Con.ExecuteNonQuery(CommandType.Text, "ALTER TABLE " & DITable & " add CONSTRAINT fk_DOC_ID FOREIGN KEY (DOC_ID) REFERENCES " & DTTable & " (DOC_ID) ON DELETE CASCADE")
                    Server.Con.ExecuteNonQuery(CommandType.Text, "ALTER TABLE " & DITable & " add CONSTRAINT fk_DOC_ID FOREIGN KEY (DOC_ID) REFERENCES " & DTTable & " (DOC_ID) ON UPDATE CASCADE")
                End If
                ok += Convert.ToInt16(1)
            Catch ex As Exception
                errors += Convert.ToInt16(1)
            End Try
        Next
        Dim total As Int32
        total = ok + errors
        MessageBox.Show("Tablas actualizadas: " & ok.ToString.Trim & "   -   Tablas que contenían las relaciones antes de ejecutar el paquete:  " & errors.ToString.Trim & "  - Total de tablas actualizadas: " & total.ToString.Trim)
        Return True
    End Function

#End Region

#Region "Propiedades"

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Establece las relaciones en cascada. Version 1.0 10/01/2006"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTables_DOC_T"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTables_DOC_T
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("01/01/2006")
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
            Return 61
        End Get
    End Property

#End Region

End Class
