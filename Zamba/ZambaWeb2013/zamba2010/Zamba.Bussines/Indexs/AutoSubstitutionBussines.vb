Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.Data
Imports System.Data

Public Class AutoSubstitutionBusiness


    ''' <summary>
    ''' Obtiene los valores de sustitucion de la tabla especificada
    ''' </summary>
    ''' <param name="Indexid">id de tabla de sustitución</param>
    ''' <param name="Reload">se especifica si hay que volver a traer de la base el dataSet
    '''                 o se toma una copia local consultada anteriormente</param>
    ''' <returns>datatable</returns>
    ''' <history>
    '''     Marcelo    Modified    13/08/2009
    '''     Javier     Modified    01/02/2012
    '''</history>
    ''' <remarks></remarks>
    ''' 

    Public Function GetIndexDataWithLimit(ByVal Indexid As Int64, LimitTo As Int64, Value As String) As DataTable

        Dim UP As New UserPreferences

        Dim OrderByDescripcion As Boolean = Boolean.Parse(UP.getValue("OrderListsByDescription", UPSections.UserPreferences, "True", Membership.MembershipHelper.CurrentUser.ID))

        Dim dt As DataTable = AutoSubstitutionDataFactory.GetTableWithLimit(Indexid, OrderByDescripcion, LimitTo, Value)

        Return dt
        UP = Nothing
    End Function

    Public Function GetIndexData(ByVal Indexid As Int64, ByVal Reload As Boolean) As DataTable

        Dim UP As New UserPreferences

        If Reload OrElse Not Cache.DocTypesAndIndexs.hsIndexsDT.ContainsKey(Indexid) Then
            Dim OrderByDescripcion As Boolean = Boolean.Parse(UP.getValue("OrderListsByDescription", UPSections.UserPreferences, "True", Membership.MembershipHelper.CurrentUser.ID))

            Dim dt As DataTable = AutoSubstitutionDataFactory.GetTable(Indexid, OrderByDescripcion)

            SyncLock Cache.DocTypesAndIndexs.hsIndexsDT
                If Not Cache.DocTypesAndIndexs.hsIndexsDT.ContainsKey(Indexid) Then
                    Cache.DocTypesAndIndexs.hsIndexsDT.Add(Indexid, dt)
                Else
                    Cache.DocTypesAndIndexs.hsIndexsDT(Indexid) = dt
                End If
            End SyncLock
        End If
        Return CType(Cache.DocTypesAndIndexs.hsIndexsDT(Indexid), DataTable)
        UP = Nothing
    End Function

    Public Shared Sub InsertIntoIList(ByVal IndexId As Int32, ByVal linea As String)
        AutoSubstitutionDataFactory.InsertIntoIList(IndexId, linea)
    End Sub

    Public Shared Function InsertIntoIListAsBoolean(ByVal IndexId As Int32, ByVal linea As String) As Boolean
        Return AutoSubstitutionDataFactory.InsertIntoIListAsBoolean(IndexId, linea)
    End Function

    Shared CBT As New Threading.WaitCallback(AddressOf setDescriptionToHash)
    Shared TP As Threading.ThreadPool

    Public Function getDescription(ByVal Code As String, ByVal IndexId As Int64) As String
        Try
            If Code IsNot Nothing AndAlso Code <> String.Empty Then
                Dim dr As DataRow = AutoSubstitutionDataFactory.getDescriptionRow(IndexId, Code)
                If dr Is Nothing Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("No se encontro el codigo {0} en la lista del Indice {1}", Code, IndexId))
                    Return Code
                End If
                Return dr("Descripcion")
            Else
                Return Code
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("No se encontro el codigo {0} en la lista del Indice {1}", Code, IndexId))
            Return Code
        End Try

    End Function

    Public Function getCode(ByVal Description As String, ByVal IndexId As Int64) As String
        Try
            If Description IsNot Nothing AndAlso Description <> String.Empty Then
                Dim code As String = AutoSubstitutionDataFactory.getCode(IndexId, Description)
                Return code
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("No se encontro la descripcion {0} en la lista del Indice {1}", Description, IndexId))
            Return String.Empty
        End Try

    End Function


    ''' <summary>
    ''' Guarda el valor en las listas de sustitucion
    ''' </summary>
    ''' <param name="values"></param>
    ''' <remarks></remarks>
    Private Shared Sub setDescriptionToHash(ByVal values As Object)
        Dim indexId As Int64 = DirectCast(values, ArrayList)(0)
        Dim code As String = DirectCast(values, ArrayList)(1).ToString()

        Dim dr As DataRow = AutoSubstitutionDataFactory.getDescriptionRow(indexId, code)
        Dim dt As DataTable = Cache.DocTypesAndIndexs.hsSustIndex(indexId)
        dt.Rows.Add(dr.ItemArray)
    End Sub


    'Quita un item de sustitucion de acuerdo a su codigo
    Public Shared Sub RemoveItems(ByVal codeList As Generic.List(Of Integer), ByVal indexId As Integer)

        For i As Integer = 0 To codeList.Count - 1
            AutoSubstitutionDataFactory.RemoveItem(codeList.Item(i), indexId)
        Next

    End Sub
    'Agrega items de sustitucion a un indice
    Public Shared Sub AddItems(ByVal items As Generic.List(Of SustitutionItem), ByVal indexID As Integer)

        For i As Integer = 0 To items.Count - 1
            AutoSubstitutionDataFactory.AddItems(items.Item(i), indexID)
        Next

    End Sub
    Public Shared Sub UpdateAddItem(ByVal codigo As String, ByVal descripcion As String, ByVal LastCode As String, ByVal IndexId As String)

        'For i As Integer = 0 To items.Count - 1
        AutoSubstitutionDataFactory.UpdateAddedItem(codigo, descripcion, LastCode, IndexId)
        'Next
    End Sub

    Public Shared Sub ClearHashTables()
        If Not IsNothing(Cache.DocTypesAndIndexs.hsIndexsDT) Then
            Cache.DocTypesAndIndexs.hsIndexsDT.Clear()
            Cache.DocTypesAndIndexs.hsIndexsDT = Nothing
            Cache.DocTypesAndIndexs.hsIndexsDT = New SynchronizedHashtable()
        End If
        If Not IsNothing(Cache.DocTypesAndIndexs.hsSustIndex) Then
            Cache.DocTypesAndIndexs.hsSustIndex.Clear()
            Cache.DocTypesAndIndexs.hsSustIndex = Nothing
            Cache.DocTypesAndIndexs.hsSustIndex = New SynchronizedHashtable()
        End If
        If Not IsNothing(Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue) Then
            Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue.Clear()
            Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue = Nothing
            Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue = New SynchronizedHashtable()
        End If
    End Sub

End Class
