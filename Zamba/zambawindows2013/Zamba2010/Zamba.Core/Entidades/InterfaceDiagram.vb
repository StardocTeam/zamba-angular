Public Class InterfaceDiagram
    Inherits ZambaCore
    Implements IInterfaceDiagram

#Region " Atributos "
    Private _Id As Int64
    Private _stepId As Int64
    Private _Name As String

    Private _isFull As Boolean
    Private _isLoaded As Boolean
#End Region
#Region " Propiedades "
    Public Property Name As String Implements IInterfaceDiagram.Name
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Public Property ID As Int64 Implements IInterfaceDiagram.ID
        Get
            Return _Id
        End Get
        Set(value As Int64)
            _Id = value
        End Set
    End Property

    Public Property StepID As Int64 Implements IInterfaceDiagram.StepID
        Get
            Return _stepId
        End Get
        Set(value As Int64)
            _stepId = value
        End Set
    End Property

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
#End Region

#Region " Constructores "
    Public Sub New()
        _isLoaded = True
        _isFull = True
    End Sub
#End Region


    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
End Class
