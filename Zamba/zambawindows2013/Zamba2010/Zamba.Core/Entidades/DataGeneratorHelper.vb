Imports System.Collections.Generic

''' <summary>
''' Brinda ayuda en la generación de información para entidades y tablas en general
''' </summary>
''' <remarks></remarks>
Public Class DataGeneratorHelper
    Implements IDisposable

    ''' <summary>
    ''' Genera un objeto utilizado en trabajos de generación de información para entidades o tablas en general
    ''' </summary>
    ''' <param name="table">Tabla objetivo</param>
    ''' <param name="columnKey">Clave primaria de la tabla</param>
    ''' <param name="attributes">Lista de atributos o columnas a modificar</param>
    ''' <remarks></remarks>
    Public Sub New(table As String, _
                   columnKey As String, _
                   attributes As List(Of ColumnHelper))
        _table = table
        _columnKey = columnKey
        _columns = attributes
    End Sub

    Private _table As String
    Private _columnKey As String
    Private _columns As List(Of ColumnHelper)

    ''' <summary>
    ''' Nombre de la tabla objetivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TableName() As String
        Get
            Return _table
        End Get
        Set(ByVal value As String)
            _table = value
        End Set
    End Property

    ''' <summary>
    ''' Clave primaria de la tabla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ColumnKey() As String
        Get
            Return _columnKey
        End Get
        Set(ByVal value As String)
            _columnKey = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de atributos o columnas a modificar y generar su información
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Columns() As List(Of ColumnHelper)
        Get
            Return _columns
        End Get
        Set(ByVal value As List(Of ColumnHelper))
            _columns = value
        End Set
    End Property

    ''' <summary>
    ''' Clase utilizada para almacenar la información de que atributos o columnas serán modificadas
    ''' por el generador de datos, que tipo de información se completará y que filtros utilizará.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ColumnHelper
        ''' <summary>
        ''' Genera un objeto para almacenar la información de que atributos o columnas serán modificadas
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="category"></param>
        ''' <param name="filters"></param>
        ''' <remarks></remarks>
        Public Sub New(columnName As String, _
                       category As DataGeneratorCategory, _
                       filters As List(Of String))
            _columnName = columnName
            _category = category
            _filters = filters
        End Sub

        ''' <summary>
        ''' Genera un objeto para almacenar la información de que atributos o columnas serán modificadas
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="category"></param>
        ''' <param name="filters"></param>
        ''' <remarks></remarks>
        Public Sub New(columnName As String, _
                       category As DataGeneratorCategory)
            _columnName = columnName
            _category = category
            _filters = Nothing
        End Sub

        Private _columnName As String
        Private _category As DataGeneratorCategory
        Private _filters As List(Of String)

        ''' <summary>
        ''' Nombre del atributo o columna a modificar
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ColumnName() As String
            Get
                Return _columnName
            End Get
            Set(ByVal value As String)
                _columnName = value
            End Set
        End Property

        ''' <summary>
        ''' Categoría de la información. Representa QUE es dicha información.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Category() As DataGeneratorCategory
            Get
                Return _category
            End Get
            Set(ByVal value As DataGeneratorCategory)
                _category = value
            End Set
        End Property

        ''' <summary>
        ''' Lista de valores a filtrar para que no sean afectados por la modificación masiva de información
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Filter() As List(Of String)
            Get
                Return _filters
            End Get
            Set(ByVal value As List(Of String))
                _filters = value
            End Set
        End Property

    End Class

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


