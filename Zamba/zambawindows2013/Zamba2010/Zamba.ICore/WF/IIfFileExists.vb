Public Interface IIfFileExists
    Inherits IRule
    Property SearchPath() As String
    Property SearchOption() As IO.SearchOption
    Property TextoInteligente() As String
End Interface