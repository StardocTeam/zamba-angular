Imports Zamba.Servers
Imports Zamba.AppBlock


Public Class PAQ_AddColumn_Z_GroupToNotify_GroupID
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega la columna GroupId a la tabla Z_GroupToNotify. En dicha columna se guardan IDs de grupos para notificaciones."
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("12/12/2007")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AddColumn_Z_GroupToNotify_GroupID"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AddColumn_Z_GroupToNotify_GroupID
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
            Return 35
        End Get
    End Property
#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Try

            Dim sql As New System.Text.StringBuilder
            Dim flagCreateTable As Boolean = False

            sql.Append("ALTER TABLE Z_GroupToNotify ADD [GroupId2] [numeric](18, 0) NULL")

            If Not GenerateScripts Then
                If ZPaq.ExisteTabla("Z_GroupToNotify") Then
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
            sql.Remove(0, sql.Length)

            'If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

            '    ExecSQL(GenerateScripts)
            'Else
            '    MessageBox.Show("Este paquete solo esta disponible para SQL Server.", "Zamba Paquetes", MessageBoxButtons.OK)
            '    'ExecOracle(GenerateScripts)

            'End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return True

    End Function

    Private Sub ExecSQL(ByVal generatescripts As Boolean)
        Dim sql As New System.Text.StringBuilder
        Dim flagCreateTable As Boolean = False
        Try

            'If ZPaq.IfExists("Z_GroupToNotify", Tipo.Table, False) Then

            '    Dim eleccion As DialogResult = MessageBox.Show("La tabla Z_GroupToNotify ya existe. Para Eliminarla seleccione 'Yes', para agregarle la columna GroupID seleccione 'No', para salir presione 'Cancel'", "Zamba Paquetes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)

            '    Select Case eleccion

            '        Case DialogResult.Yes
            '            sql.Append("DROP TABLE Z_GroupToNotify")
            '            flagCreateTable = True

            '        Case DialogResult.No
            sql.Append("ALTER TABLE Z_GroupToNotify ADD [GroupId2] [numeric](18, 0) NULL")

            '        Case DialogResult.Cancel
            'Exit Sub

            '    End Select

            'If Not generatescripts Then
            '    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            'Else
            '    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            '    sw.WriteLine("")
            '    sw.WriteLine(sql.ToString)
            '    sw.WriteLine("")
            '    sw.Close()
            '    sw = Nothing
            'End If
            'sql.Remove(0, sql.Length)

            'Else
            'flagCreateTable = True

            'End If


            'If flagCreateTable Then

            '    sql.Append("CREATE TABLE [dbo].[Z_GroupToNotify] (")
            '    sql.Append("[TypeId] [numeric](18, 0) NULL ,")
            '    sql.Append("[DocId] [numeric](18, 0) NULL ,")
            '    sql.Append("[UserId] [numeric](18, 0) NULL ,")
            '    sql.Append("[ExtraData] [varchar] (250) NULL ,")
            '    sql.Append("[GroupId] [numeric](18, 0) NULL")
            '    sql.Append(") ON [PRIMARY]")

            If Not generatescripts Then
                If ZPaq.ExisteTabla("Z_GroupToNotify") Then
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
            sql.Remove(0, sql.Length)

            'End If



        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

End Class
