Imports Zamba.Servers
Imports zamba.Core
Imports Microsoft.Win32
Imports system.Text

Public Class PAQ_CreateTable_WFInsert
    Inherits ZPaq
    Implements IPAQ


    Public Overrides Sub Dispose()

    End Sub

#Region "IPAQ Members"
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la tabla ZI (donde se guardan los datos de los documentos entrantes), ZWFI (donde se guardan docs entrantes), ZWFII (donde se guardan los indices del documento en espera)."
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTableZI"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_WFInsert
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sqlBuilder As New StringBuilder
        Dim flagException As Boolean = False
        Dim exBuilder As New System.Text.StringBuilder()

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

            sqlBuilder.Append("CREATE TABLE [ZI] (")
            sqlBuilder.Append("[InsertID] [numeric](18, 0) NOT NULL ,")
            sqlBuilder.Append("[DTID] [numeric](18, 0) NOT NULL ,")
            sqlBuilder.Append("[DocID] [numeric](18, 0) NOT NULL ,")
            sqlBuilder.Append("[FolderID] [numeric](18, 0) NOT NULL ,")
            sqlBuilder.Append("[IDate] [datetime] NOT NULL ,")
            sqlBuilder.Append("[RuleID] [nvarchar] (254) NULL ")
            sqlBuilder.Append(")")
            Try
                If Not ZPaq.ExisteTabla("ZI") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sqlBuilder.ToString())
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla ZI ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sqlBuilder.ToString())
                exBuilder.Append("Error: ")
                exBuilder.AppendLine(ex.ToString())
                exBuilder.AppendLine()
            End Try

            sqlBuilder.Remove(0, sqlBuilder.Length)
            sqlBuilder.Append("CREATE TABLE [ZWFI] (")
            sqlBuilder.Append("[WI] [numeric](18, 0) NOT NULL ,")
            sqlBuilder.Append("[DTID] [numeric](18, 0) NOT NULL ,")
            sqlBuilder.Append("[RuleID] [numeric](18, 0) NOT NULL ,")
            sqlBuilder.Append("[InsertID] [numeric](18, 0) NULL ")
            sqlBuilder.Append(")")
            Try
                If Not ZPaq.ExisteTabla("ZWFI") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sqlBuilder.ToString())
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla ZWFI ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sqlBuilder.ToString())
                exBuilder.Append("Error: ")
                exBuilder.AppendLine(ex.ToString())
                exBuilder.AppendLine()
            End Try

            sqlBuilder.Remove(0, sqlBuilder.Length)
            sqlBuilder.Append("CREATE TABLE [ZWFII] (")
            sqlBuilder.Append("[WI] [numeric](18, 0) NOT NULL ,")
            sqlBuilder.Append("[IID] [numeric](18, 0) NOT NULL ,")
            sqlBuilder.Append("[IValue] [varchar](50) NOT NULL ")
            sqlBuilder.Append(")")
            Try
                If Not ZPaq.ExisteTabla("ZWFII") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sqlBuilder.ToString())
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla ZWFII ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sqlBuilder.ToString())
                exBuilder.Append("Error: ")
                exBuilder.AppendLine(ex.ToString())
                exBuilder.AppendLine()
            End Try

        Else
            sqlBuilder.Append("CREATE TABLE ZI (InsertID numeric NOT NULL, DTID numeric NOT NULL, DocID numeric NOT NULL, FolderID numeric NOT NULL, IDate date NOT NULL, RuleID nvarchar2(254) NULL)")
            Try
                If Not ZPaq.ExisteTabla("ZI") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sqlBuilder.ToString())
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla ZI ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sqlBuilder.ToString())
                exBuilder.Append("Error: ")
                exBuilder.AppendLine(ex.ToString())
                exBuilder.AppendLine()
            End Try

            sqlBuilder.Remove(0, sqlBuilder.Length)
            sqlBuilder.Append("CREATE TABLE ZWFI (WI numeric NOT NULL, DTID numeric NOT NULL, RuleID numeric NOT NULL, InsertID numeric NULL)")
            Try
                If Not ZPaq.ExisteTabla("ZWFI") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sqlBuilder.ToString())
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla ZWFI ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sqlBuilder.ToString())
                exBuilder.Append("Error: ")
                exBuilder.AppendLine(ex.ToString())
                exBuilder.AppendLine()
            End Try

            sqlBuilder.Remove(0, sqlBuilder.Length)
            sqlBuilder.Append("CREATE TABLE ZWFII (WI numeric NOT NULL, IID numeric NOT NULL, IValue nvarchar2(50) NOT NULL)")
            Try
                If Not ZPaq.ExisteTabla("ZWFII") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sqlBuilder.ToString())
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla ZWFII ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sqlBuilder.ToString())
                exBuilder.Append("Error: ")
                exBuilder.AppendLine(ex.ToString())
                exBuilder.AppendLine()
            End Try

        End If

        If flagException Then
            Throw New Exception(Me.name + exBuilder.ToString())
        End If

        Return True

    End Function
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
            Return 22
        End Get
    End Property
#End Region

#Region "ZPaq Members"
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("22/11/2007")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("22/11/2007")
        End Get
    End Property
#End Region

End Class
