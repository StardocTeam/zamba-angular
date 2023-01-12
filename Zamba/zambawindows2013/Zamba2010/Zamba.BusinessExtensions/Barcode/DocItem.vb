''' <summary>
'''  Lista de items de documentos(id,type)
''' </summary>
''' <remarks>
'''  Solamente usada en la vista UCBarcode 
''' </remarks>
Public Class DocItem
    Implements IComparable

    Protected m_id As Decimal
    Protected m_typeId As Decimal
    Protected m_posicion As Integer


    Public Sub New(ByVal id As Decimal, _
    ByVal typeId As Decimal, _
    ByVal posicion As Integer)
        m_id = id
        m_typeId = typeId
        m_posicion = posicion
    End Sub

    Public ReadOnly Property Id() As Int32
        Get
            Return m_id
        End Get
    End Property


    Public ReadOnly Property TypeId() As Decimal
        Get
            Return m_typeId
        End Get
    End Property


    Public ReadOnly Property Position() As Integer
        Get
            Return m_posicion
        End Get
    End Property


    Public Overloads Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo

        If TypeOf obj Is DocItem Then
            Dim dato As DocItem = DirectCast(obj, DocItem)
            If dato.Position.Equals(Position) Then
                Return 0
            ElseIf dato.Position > Position Then
                Return 1
            Else
                Return -1
            End If
        End If

        Throw New ArgumentException("El objeto no es un DocItem," & _
           " No se puede comparar en DocItem.CompareTo(..)")
    End Function


    ''' <summary>
    '''  Devuelve una una instancia de lista de DocItem
    '''  cargada desde las filas de una DataTable
    ''' </summary>
    ''' <param name="table">Tabla</param>
    ''' <param name="IdColumnsNumber">
    '''    posicion de id doc en la tabla 
    ''' </param>
    ''' <param name="docTypeColumnNumber">
    '''   posicion de id tipo doc en la tabla 
    ''' </param>
    ''' <returns>una instancia de lista de DocItems</returns>
    Public Shared Function getDocItemList(ByRef table As DataTable, ByVal IdColumnsNumber As Integer, ByVal docTypeColumnNumber As Integer) As List(Of DocItem)
        Dim list As List(Of DocItem) = New List(Of DocItem)
        Dim generadorPos As Int32

        For Each item As DataRow In table.Rows

            If Not (TypeOf item.ItemArray(IdColumnsNumber) Is Decimal) Then
                Throw New Exception("La columna Id Documento no es Decimal - class DocItem")
            End If

            If Not (TypeOf item.ItemArray(docTypeColumnNumber) Is Decimal) Then
                Throw New Exception("La columna Tipo Documento no es Decimal - class DocItem")
            End If

            list.Add(New DocItem(item.ItemArray(IdColumnsNumber), item.ItemArray(docTypeColumnNumber), generadorPos))
            generadorPos += 1
        Next
        list.Sort()

        Return list
    End Function


    ''' <summary>
    ''' Devuelve el DocItem buscando pos el atributo position
    ''' </summary>
    ''' <param name="list">lista</param>
    ''' <param name="position">valor Atributo posicion</param>
    ''' <returns>un DocItem</returns>
    Public Shared Function getItem(ByRef list As List(Of DocItem), ByVal position As Integer) As DocItem
        Dim pos As Integer

        If Not IsNothing(list) Then

            If position >= 0 And position <= list.Count Then

                pos = list.BinarySearch(New DocItem(0, Nothing, position))

                If pos > 0 Then
                    Return list(pos)
                Else
                    Return Nothing
                End If
            End If
        End If

        Return Nothing
    End Function



End Class
