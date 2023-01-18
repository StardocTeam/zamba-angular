''' <summary>
''' Representa un item de una precondición, flujo principal, flujo alternativo o poscondición de un caso de uso.
''' </summary>
''' <remarks></remarks>
Public Class UseCaseItems
    Implements ICore

#Region "Attributes and Properties"
    Private _id As Int64
    Private _name As String

    ''' <summary>
    ''' Id de un item de un caso de uso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID As Long Implements ICore.ID
        Get
            Return _id
        End Get
        Set(value As Long)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre o descripción de un item de un caso de uso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name As String Implements ICore.Name
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
#End Region

#Region "Constructors"
    ''' <summary>
    ''' Genera un item en un caso de uso
    ''' </summary>
    ''' <param name="id">Id de un item de un caso de uso</param>
    ''' <param name="name">Nombre o descripción de un item de un caso de uso</param>
    ''' <remarks>Representa un item de una precondición, flujo principal, flujo alternativo o poscondición de un caso de uso</remarks>
    Public Sub New(ByVal id As Int64, ByVal name As String)
        _id = id
        _name = name
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            ID = Nothing
            Name = Nothing
        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
