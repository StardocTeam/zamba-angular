'ver
Imports Zamba.Servers
Imports Zamba.Core
Imports System.Collections.Generic

Public Class PAQ_CreateViews_DOC
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        'TODO store: SPGetDoc_type_id
        Dim lstColKeys As Dictionary(Of String(), String) = New Dictionary(Of String(), String)
        Dim lstColSelects As Dictionary(Of String, String()) = New Dictionary(Of String, String())
        Dim lstColIndexs As Dictionary(Of String, String) = New Dictionary(Of String, String)

        Dim strselect As String = "Select DOC_TYPE_ID from DOC_TYPE"
        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Dim i As Int32
        If GenerateScripts = False Then
            For i = 0 To dstemp.Tables(0).Rows.Count - 1
                Try
                    Server.CreateTables.DropView(Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("DOC_TYPE_ID")))
                Catch
                End Try
                Server.CreateTables.CreateView(Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("DOC_TYPE_ID")), lstColKeys, lstColIndexs, lstColSelects)
            Next
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            '  sw.WriteLine(Sql.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If

        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateViews_DOC"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateViews_DOC
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea las vista que unen la DOC_T y DOC_I en DOC, para la busqueda de la Version 1.6.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
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
    Public ReadOnly Property orden() As Int64 Implements IPAQ.orden
        Get
            Return 69
        End Get
    End Property
End Class
