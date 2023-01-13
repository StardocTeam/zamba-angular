Imports System.Text
Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32

Public Class PAQ_AlterTable_WFStepHst
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterTable_WFStepHst"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterTable_WFStepHst
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Modifica la Tabla WFStepHst agregandole las columnas DocTypeId, StepId, WorkflowId y WorkflowName."
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("28/12/2007")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("28/12/2007")
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
            Return 89
        End Get
    End Property

#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute

        Dim strcreate As String = String.Empty
        Dim exBuilder As New StringBuilder
        Dim flagError As Boolean = False

        Try
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                strcreate = "ALTER TABLE wfstephst ADD [WorkflowName] [varchar] (50) NULL"
            Else
                strcreate = "ALTER TABLE wfstephst ADD WorkflowName NVARCHAR2(50) NULL"
            End If
            If Not GenerateScripts Then
                If ZPaq.ExisteTabla("WFStepHst") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Throw New Exception(Me.name & ": la tabla WFStepHst no existe en la base de datos.")
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(strcreate.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            flagError = True
            exBuilder.AppendLine(strcreate)
            exBuilder.Append("Error: ")
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        Try
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                strcreate = "ALTER TABLE wfstephst ADD [WorkflowId] [numeric](18, 0) NULL"
            Else
                strcreate = "ALTER TABLE wfstephst ADD WorkflowId NUMBER(10) NULL"
            End If
            If Not GenerateScripts Then
                If ZPaq.ExisteTabla("WFStepHst") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Throw New Exception(Me.name & ": la tabla WFStepHst no existe en la base de datos.")
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(strcreate.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            flagError = True
            exBuilder.AppendLine(strcreate)
            exBuilder.Append("Error: ")
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        Try
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                strcreate = "ALTER TABLE wfstephst ADD [StepId] [numeric](18, 0) NULL"
            Else
                strcreate = "ALTER TABLE wfstephst ADD StepId NUMBER(10) NULL"
            End If
            If Not GenerateScripts Then
                If ZPaq.ExisteTabla("WFStepHst") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Throw New Exception(Me.name & ": la tabla WFStepHst no existe en la base de datos.")
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(strcreate.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            flagError = True
            exBuilder.AppendLine(strcreate)
            exBuilder.Append("Error: ")
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        Try
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                strcreate = "ALTER TABLE wfstephst ADD [DocTypeId] [numeric](18, 0) NULL"
            Else
                strcreate = "ALTER TABLE wfstephst ADD DocTypeId NUMBER(10) NULL"
            End If
            If Not GenerateScripts Then
                If ZPaq.ExisteTabla("WFStepHst") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Throw New Exception(Me.name & ": la tabla WFStepHst no existe en la base de datos.")
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(strcreate.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            flagError = True
            exBuilder.AppendLine(strcreate)
            exBuilder.Append("Error: ")
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        If flagError Then
            Throw New Exception(exBuilder.ToString())
        Else
            Return True
        End If

    End Function

#End Region

End Class

