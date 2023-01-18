Public Class WFDiagram
    Inherits ZambaCore
    Implements IWF
#Region " Atributos "
    Private _WorkId As Integer
    Private _WstatId As Integer
    Private _Name As String
    Private _Description As String
    Private _InitialStepId As Integer
    Private _isFull As Boolean
    Private _isLoaded As Boolean
#End Region
#Region " Propiedades "
    Public Property Description As String Implements IWF.Description
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property

    Public Property InitialStepId As Integer Implements IWF.InitialStepId
        Get
            Return _InitialStepId
        End Get
        Set(value As Integer)
            _InitialStepId = value
        End Set
    End Property

    Public Property Name As String Implements IWF.Name
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Public Property WorkId As Integer Implements IWF.WorkId
        Get
            Return _WorkId
        End Get
        Set(value As Integer)
            _WorkId = value
        End Set
    End Property

    Public Property WstatId As Integer Implements IWF.WstatId
        Get
            Return _WstatId
        End Get
        Set(value As Integer)
            _WstatId = value
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
    End Sub
#End Region


    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
End Class
