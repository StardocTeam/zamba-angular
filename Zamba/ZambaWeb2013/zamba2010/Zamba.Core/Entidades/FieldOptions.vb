Public Class FieldOptions
    Implements IFieldOptions

    Dim _controlId As String
    Dim _selectOptions As String
    Dim _additionalValidationButton As String
    Dim _postAjaxFunction As String

    Public Property ControlId As String Implements IFieldOptions.ControlId
        Get
            Return _controlId
        End Get
        Set(ByVal value As String)
            _controlId = value
        End Set
    End Property

    Public Property SelectOptions As String Implements IFieldOptions.SelectOptions
        Get
            Return _selectOptions
        End Get
        Set(ByVal value As String)
            _selectOptions = value
        End Set
    End Property

    Public Property AdditionalValidationButton As String Implements IFieldOptions.AdditionalValidationButton
        Get
            Return _additionalValidationButton
        End Get
        Set(ByVal value As String)
            _additionalValidationButton = value
        End Set
    End Property

    Public Property PostAjaxFunction As String Implements IFieldOptions.PostAjaxFunction
        Get
            Return _postAjaxFunction
        End Get
        Set(ByVal value As String)
            _postAjaxFunction = value
        End Set
    End Property
End Class
