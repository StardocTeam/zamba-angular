Imports Zamba.Data

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
            If isZamba = True Then
                id = Zamba.Data.CoreData.GetNewID(Zamba.Core.IdTypes.ZQUERY)
                Return id
            Else
                id = ReportBuilderFactory.GetLastID()
                Return id + 1
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return 0
        End Try
    End Function
    ''' <summary>
    ''' Get the docTypename using it's id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDocTypeName(ByVal id As Int32) As String
        Dim name As String
        Try
            name = DocTypesFactory.GetDocTypeName(id)
            Return name.Trim
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ""
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
            name = Zamba.Core.IndexsBusiness.GetIndexName(id, True)
            Return name
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Get all the docTypes names
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDocTypeNames() As DataTable
        Dim ds As DataTable
        Try
            ds = DocTypesFactory.GetDocTypeNamesAndIds()
            Return ds
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
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
            Zamba.Core.ZClass.raiseerror(ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Return the indexs of a doctype
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getIndexs(ByVal id As Int32) As DataSet
        Dim td As DataSet
        Try
            td = Indexs_Factory.GetIndexsSchema(id)
            Return td
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get the Index id using the name
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getIndexId(ByVal name As String) As Int32
        Dim id As Int32 = 0
        Try
            id = Zamba.Core.IndexsBusiness.GetIndexId(name)
            Return id
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return 0
        End Try
    End Function

End Class
