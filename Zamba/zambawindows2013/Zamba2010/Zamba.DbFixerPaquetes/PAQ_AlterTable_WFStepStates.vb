Imports Zamba.Servers
Imports zamba.AppBlock

Public Class PAQ_AlterTable_WFStepStates
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Agrega FK para el campo Step_Id con referencia a la tabla WfStep"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterTable_WFStepStates"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterTable_WFStepStates
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("24/11/06")
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
            Return 56
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As New System.Text.StringBuilder
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            Try
                sql.Append("ALTER TABLE [WFStepStates] WITH NOCHECK ADD ")
                sql.Append(ControlChars.NewLine)
                sql.Append("	CONSTRAINT [FK_StepID] FOREIGN KEY([Step_Id]) REFERENCES WfStep(Step_Id) ON DELETE CASCADE  ON UPDATE CASCADE ")
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("WFStepStates") Then
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
