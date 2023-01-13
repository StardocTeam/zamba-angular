Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.Data
Imports System.Data

Public Class AutoSubstitutionBusiness

    Shared CBT As New Threading.WaitCallback(AddressOf setDescriptionToHash)
    Shared TP As Threading.ThreadPool

    ''' <summary>
    ''' Obtiene los valores de sustitucion de la tabla especificada
    ''' </summary>
    ''' <param name="Indexid">id de tabla de sustitución</param>
    ''' <param name="Reload">se especifica si hay que volver a traer de la base el dataSet
    '''                 o se toma una copia local consultada anteriormente</param>
    ''' <returns>datatable</returns>
    ''' <history>Marcelo    Modified    13/08/2009</history>
    ''' <remarks></remarks>
    Public Shared Function GetIndexData(ByVal Indexid As Int64, ByVal Reload As Boolean) As DataTable
        If Reload OrElse Not Cache.DocTypesAndIndexs.hsIndexsDT.ContainsKey(Indexid) Then
            Dim OrderByDescripcion As Boolean = Boolean.Parse(UserPreferences.getValue("OrderListsByDescription", Sections.UserPreferences, "True"))

            Dim dt As DataTable = AutoSubstitutionDataFactory.GetTable(Indexid, OrderByDescripcion)

            If Not Cache.DocTypesAndIndexs.hsIndexsDT.ContainsKey(Indexid) Then
                Cache.DocTypesAndIndexs.hsIndexsDT.Add(Indexid, dt)
            Else
                Cache.DocTypesAndIndexs.hsIndexsDT(Indexid) = dt
            End If
        End If

        Return CType(Cache.DocTypesAndIndexs.hsIndexsDT(Indexid), DataTable)
    End Function

    Public Shared Sub InsertIntoIList(ByVal IndexId As Int32, ByVal linea As String)
        AutoSubstitutionDataFactory.InsertIntoIList(IndexId, linea)
    End Sub

    Public Shared Function InsertIntoIListAsBoolean(ByVal IndexId As Int32, ByVal linea As String) As Boolean
        Return AutoSubstitutionDataFactory.InsertIntoIListAsBoolean(IndexId, linea)
    End Function

    Public Shared ReadOnly Property NombreColumnaCodigo() As String
        Get
            Return AutoSubstitutionDataFactory.nombreColumnaCodigo
        End Get
    End Property

    Public Shared ReadOnly Property NombreColumnaDescripcion() As String
        Get
            Return AutoSubstitutionDataFactory.nombreColumnaDescripcion
        End Get
    End Property

    ''' <summary>
    ''' Devuelve la descripcion de un item para una tabla de sustitucion dada
    ''' </summary>
    ''' <param name="Code">item id</param>
    ''' <param name="IndexId">id de tabla de sustitución</param>
    ''' <returns>descripción</returns>
    ''' <History>   Marcelo 31/07/08    Modified
    '''             Marcelo 13/08/09    Modified 
    '''             Tomas   08/06/11    Modified    Se agrega el parametro opcional returnEmpty para devolver String.Empty en vez de Code al no encontrar el valor.
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Function getDescription(ByVal Code As String, ByVal IndexId As Int64, Optional ByVal inThread As Boolean = False, Optional ByVal indexType As IndexDataType = IndexDataType.Numerico, Optional ByVal returnEmpty As Boolean = False) As String
        'Se valida si el Atributo es alfanumerico o no ya que si es alfanumerico no
        'debe removerse los ceros de adelante que pueda tener el valor de Code
        If indexType <> IndexDataType.Alfanumerico AndAlso indexType <> IndexDataType.Alfanumerico_Largo Then
            While Code.StartsWith("0")
                'Se verifica que no existan ceros por delante ya que generan ERROR.
                Code = Code.Remove("0", 1)
            End While
        End If

        If Cache.DocTypesAndIndexs.hsSustIndex.Contains(IndexId) = False Then
            Dim dt As DataTable = New DataTable
            dt.Columns.Add("Codigo")
            dt.Columns.Add("Descripcion")
            Dim columns() As DataColumn = New DataColumn() {dt.Columns("Codigo")}
            dt.PrimaryKey = columns
            dt.TableName = IndexId
            Cache.DocTypesAndIndexs.hsSustIndex.Add(IndexId, dt)
        End If

        Dim dtSust As DataTable = Cache.DocTypesAndIndexs.hsSustIndex(IndexId)

        Dim dv As New DataView(dtSust)
        dv.RowFilter = "codigo='" & Code & "'"
        Dim dt2 As DataTable = dv.ToTable()

        If dt2.Rows.Count > 0 Then
            Dim desc As String = dt2.Rows(0)("Descripcion")
            Return desc
        End If

        If Cache.DocTypesAndIndexs.hsIndexsDT.ContainsKey(IndexId) Then
            Dim dtIndex As DataTable = Cache.DocTypesAndIndexs.hsIndexsDT(IndexId)
            dv = New DataView(dtIndex)
            dv.RowFilter = "codigo='" & Code & "'"
            dt2 = dv.ToTable()

            If dt2.Rows.Count > 0 Then
                dtSust.Rows.Add(dt2.Rows(0).ItemArray)
                Return dt2.Rows(0)("Descripcion")
            Else
                If returnEmpty Then
                    Return String.Empty
                Else
                    Dim objects() As Object = New Object() {Code, Code}
                    dtSust.Rows.Add(objects)
                    Return Code
                End If
            End If
        Else
            If inThread = False Then
                Dim dr As DataRow = AutoSubstitutionDataFactory.getDescriptionRow(IndexId, Code)
                If Not IsNothing(dr) Then
                    dtSust.Rows.Add(dr.ItemArray)
                    Return dr("Descripcion")
                Else
                    If returnEmpty Then
                        Return String.Empty
                    Else
                        Dim objects() As Object = New Object() {Code, Code}
                        dtSust.Rows.Add(objects)
                        Return Code
                    End If
                End If
            End If
            Dim array As ArrayList = New ArrayList()
            array.Add(IndexId)
            array.Add(Code)

            Try
                Threading.ThreadPool.SetMaxThreads(10, 10)
                Threading.ThreadPool.QueueUserWorkItem(CBT, array)
            Catch ex As Threading.SynchronizationLockException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadAbortException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadInterruptedException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadStateException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
        Return Code
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
    Public Shared Sub RemoveItems(ByVal codeList As Generic.List(Of String), ByVal indexId As Integer)

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
End Class
