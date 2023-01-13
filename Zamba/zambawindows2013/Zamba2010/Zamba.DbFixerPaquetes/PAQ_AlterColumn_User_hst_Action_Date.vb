'ver
Imports Zamba.Servers
Imports Zamba.Core
Public Class PAQ_AlterColumn_User_hst_Action_Date
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim strAlter As String
        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, "DROP INDEX INDEXDOCID")
            Catch
            End Try
            strAlter = "CREATE INDEX INDEXDOCID ON DOC_NOTES (" + Chr(34) + "DOC_ID" + Chr(34) + ")"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("DOC_NOTES") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strAlter.ToString)
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(strAlter.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Else
            strAlter = "CREATE INDEX INDEXDOCID ON DOC_NOTES (" & Chr(34) & "DOC_ID" & Chr(34) & ")"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("DOC_NOTES") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strAlter.ToString)
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(strAlter.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

        End If
        Return True
    End Function
#End Region

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterColumn_User_hst_Action_Date"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Actualiza la tabla User_hst, pasa la columa Action_Date de Varchar a Date."
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
    Public Property installed() As Boolean Implements IPAQ.installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterColumn_User_hst_Action_Date
        End Get
    End Property

    Public ReadOnly Property orden() As Long Implements IPAQ.orden
        Get
            Return 63
        End Get
    End Property
#End Region

End Class
