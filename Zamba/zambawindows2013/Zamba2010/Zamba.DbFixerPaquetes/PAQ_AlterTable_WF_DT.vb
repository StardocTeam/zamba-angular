Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_AlterTable_WF_DT
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Agrega una FK para el campo DocTypeId de la tabla WF_DT con referencia al campo DOC_TYPE_ID de la tabla DOC_TYPE"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterTable_WF_DT"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterTable_WF_DT
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("27/11/06")
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
            Return 54
        End Get
    End Property

#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As New System.Text.StringBuilder
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            Try
                sql.Append("ALTER TABLE [WF_DT] WITH NOCHECK ADD  ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_WF_DT_DOC_TYPE] FOREIGN KEY([DocTypeId]) REFERENCES DOC_TYPE(DOC_TYPE_ID) ON DELETE CASCADE  ON UPDATE CASCADE ")

                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("WF_DT") Then
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
                'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
            sql.Remove(0, sql.Length)


        End If
        sql = Nothing
        Return True

    End Function

#End Region

End Class
