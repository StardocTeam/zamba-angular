Imports zamba.data
Imports System.Windows.Forms

''' <summary>
''' This class handles all the conexions to zamba databases
''' </summary>
''' <remarks></remarks>
Public Class FuncionesZamba
    ''' <summary>
    ''' Get a new ID for the query
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNewID(ByVal isZamba As Boolean) As Int32
        Try
            Dim id As Int32 = 0
            If iszamba = True Then
                id = Zamba.Data.CoreData.GetNewID(Zamba.Core.IdTypes.ZQUERY)
                Return id
            Else
                id = ReportBuilderFactory.GetLastID()
                Return id + 1
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return 0
        End Try
    End Function


    ''' <summary>
    ''' Get the index name using it's ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIndexName(ByVal id As Int32) As String
        Dim name As String
        Try
            name = Zamba.Core.IndexsBusiness.GetIndexName(id)
            Return name
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return ""
        End Try
    End Function



    ''' <summary>
    ''' Get a docType id using it's name
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDocTypeIdByName(ByVal name As String) As Int32
        Dim id As Int32
        Try
            id = DocTypesFactory.GetDocTypeIdByName(name)
            Return id
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return 0
        End Try
    End Function




    ''' <summary>
    ''' Return true if the Zamba Tables exists
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsZamba() As Boolean
        Try
            Dim isUsing As Boolean = False
            Dim txt As New IO.StreamReader(Membership.MembershipHelper.StartUpPath & "\config.txt")
            If txt.ReadLine = "Zamba=True" Then
                isUsing = True
            End If
            txt.Close()
            Return isUsing
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return False
        End Try
    End Function
End Class
