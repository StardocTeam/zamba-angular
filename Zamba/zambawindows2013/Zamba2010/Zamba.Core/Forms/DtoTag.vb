''' <summary>
''' Clase utilizada como transporte. Encapsula una unidad
''' logica de una modificacion de un tag html
''' </summary>   
''' <history>
''' 	[osanchez]	07/04/2009	Created    
''' </history>
Public Class DtoTag
    Implements IDtoTag

    Private _oldTag As String
    Public Property oldTag() As String Implements IDtoTag.oldTag
        Get
            Return _oldTag
        End Get
        Set(ByVal value As String)
            _oldTag = value
        End Set
    End Property

    Private _newTag As String
    Public Property newTag() As String Implements IDtoTag.newTag
        Get
            Return _newTag
        End Get
        Set(ByVal value As String)
            _newTag = value
        End Set
    End Property
End Class