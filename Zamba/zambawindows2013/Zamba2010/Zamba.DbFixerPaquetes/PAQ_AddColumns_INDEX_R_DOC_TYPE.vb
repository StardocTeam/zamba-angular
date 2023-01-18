'ver
Imports Zamba.Servers

Public Class PAQ_AddColumns_INDEX_R_DOC_TYPE
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Agrega las columnas MUSTCOMPLETE, COMPLETE, LOADLOTUS y SHOWLOTUS a la tabla INDEX_R_DOC_TYPE."
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "INDEX_R_DOC_TYPE"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AddColumns_INDEX_R_DOC_TYPE
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

    Public ReadOnly Property orden() As Int64 Implements IPAQ.orden
        Get
            Return 40
        End Get
    End Property
#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As String

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sql = "Alter table INDEX_R_DOC_TYPE Add MUSTCOMPLETE decimal NULL Default(0), LOADLOTUS decimal NULL Default(0), ShowLotus decimal NULL Default(0), COMPLETE decimal NULL Default(0)"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
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
            sql = "Update Index_r_doc_type set Mustcomplete=0, loadlotus=0, showlotus=0, Complete=0"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
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
        Else
            sql = "Alter table INDEX_R_DOC_TYPE Add (MUSTCOMPLETE int NULL DEFAULT 0, LOADLOTUS int NULL DEFAULT 0, ShowLotus int NULL DEFAULT 0,Complete int Null DEFAULT 0)"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
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
            sql = "Update Index_r_doc_type set Mustcomplete=0, loadlotus=0, showlotus=0, Complete=0"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
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
            End If
        Return True
    End Function

#End Region

End Class
