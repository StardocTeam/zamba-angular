'Esta clase representa los items de las tablas de sustitucion de los indices

Public Class SustitutionItem
    Implements ISustitutionItem

#Region "Atributos"
    Private _code As String
    Private _description As String
#End Region

#Region "Propiedades"
    Public Property Code() As String Implements ISustitutionItem.Code
        Get
            Return _code
        End Get
        Set(ByVal value As String)
            _code = value
        End Set
    End Property

    Public Property Description() As String Implements ISustitutionItem.Description
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
#End Region

#Region "Constructores"
    'Public Sub New(ByVal code As Integer, ByVal description As String)
    Public Sub New(ByVal code As String, ByVal description As String)
        _code = code
        _description = description
    End Sub
#End Region

End Class