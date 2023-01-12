''' <summary>
''' Clase utilizada para obtener y renderizar botones dinamicos
''' </summary>
''' <remarks></remarks>
Public Class ZDynamicButton
    Implements IDynamicButton

    Private _buttonId As String
    Private _placeId As ButtonPlace
    Private _typeId As ButtonType
    Private _params As String
    Private _needRights As Boolean
    Private _viewClass As String
    Private _caption As String
    Private _buttonOrder As String
    Private _ruleId As Int64
    Private _idicon As Int32
    Private _groupName As String
    Private _groupClass As String
    Private _stepId As Int64

    Public Property ButtonId As String Implements IDynamicButton.ButtonId
        Get
            Return _buttonId
        End Get
        Set(ByVal value As String)
            _buttonId = value
        End Set
    End Property

    Public Property ButtonOrder As String Implements IDynamicButton.ButtonOrder
        Get
            Return _buttonOrder
        End Get
        Set(ByVal value As String)
            _buttonOrder = value
        End Set
    End Property

    Public Property Caption As String Implements IDynamicButton.Caption
        Get
            Return _caption
        End Get
        Set(ByVal value As String)
            _caption = value
        End Set
    End Property

    Public Property Idicon As Integer Implements IDynamicButton.Idicon
        Get
            Return _idicon
        End Get
        Set(ByVal value As Integer)
            _idicon = value
        End Set
    End Property

    Public Property NeedRights As Boolean Implements IDynamicButton.NeedRights
        Get
            Return _needRights
        End Get
        Set(ByVal value As Boolean)
            _needRights = value
        End Set
    End Property

    Public Property Params As String Implements IDynamicButton.Params
        Get
            Return _params
        End Get
        Set(ByVal value As String)
            _params = value
        End Set
    End Property

    Public Property PlaceId As ButtonPlace Implements IDynamicButton.PlaceId
        Get
            Return _placeId
        End Get
        Set(ByVal value As ButtonPlace)
            _placeId = value
        End Set
    End Property

    Public Property TypeId As ButtonType Implements IDynamicButton.TypeId
        Get
            Return _typeId
        End Get
        Set(ByVal value As ButtonType)
            _typeId = value
        End Set
    End Property

    Public Property ViewClass As String Implements IDynamicButton.ViewClass
        Get
            Return _viewClass
        End Get
        Set(ByVal value As String)
            _viewClass = value
        End Set
    End Property

    Public Property RuleId As Long Implements IDynamicButton.RuleId
        Get
            Return _ruleId
        End Get
        Set(ByVal value As Long)
            _ruleId = value
        End Set
    End Property

    Public Property GroupName As String Implements IDynamicButton.GroupName
        Get
            Return _groupName
        End Get
        Set(ByVal value As String)
            _groupName = value
        End Set
    End Property

    Public Property GroupClass As String Implements IDynamicButton.GroupClass
        Get
            Return _groupClass
        End Get
        Set(ByVal value As String)
            _groupClass = value
        End Set
    End Property

    Public Property StepId As Long Implements IDynamicButton.StepId
        Get
            Return _stepId
        End Get
        Set(value As Long)
            _stepId = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal buttonId As String,
        ByVal placeId As ButtonPlace,
        ByVal typeId As ButtonType,
        ByVal params As String,
        ByVal needRights As Boolean,
        ByVal viewClass As String,
        ByVal caption As String,
        ByVal buttonOrder As String,
        ByVal ruleId As Int64,
        ByVal idicon As Int32,
        ByVal groupName As String,
        ByVal groupClass As String,
        ByVal stepId As Long)

        _buttonId = buttonId
        _placeId = placeId
        _typeId = typeId
        _params = params
        _needRights = needRights
        _viewClass = viewClass
        _caption = caption
        _buttonOrder = buttonOrder
        _ruleId = ruleId
        _idicon = idicon
        _groupName = groupName
        _groupClass = groupClass
        _stepId = stepId
    End Sub

    Public Sub New(ByVal row As DataRow)
        If row.Table.Columns.Contains("BUTTONID") Then
            _buttonId = row("BUTTONID")
        End If
        If row.Table.Columns.Contains("PLACEDESC") Then
            _placeId = row("PLACEDESC")
        End If
        If row.Table.Columns.Contains("TYPEDESC") Then
            _typeId = row("TYPEDESC")
        End If
        If row.Table.Columns.Contains("PARAMS") Then
            If Not IsDBNull(row("PARAMS")) Then
                _params = row("PARAMS")
            End If
        End If
        If row.Table.Columns.Contains("NEEDRIGHTS") Then
            _needRights = row("NEEDRIGHTS")
        End If
        If row.Table.Columns.Contains("VIEWCLASS") Then
            If Not IsDBNull(row("VIEWCLASS")) Then
                _viewClass = row("VIEWCLASS")
            Else
                _viewClass = "list-group-item"
            End If
        End If
        If row.Table.Columns.Contains("CAPTION") Then
            If Not IsDBNull(row("CAPTION")) Then
                _caption = row("CAPTION")

            End If
        End If
        If row.Table.Columns.Contains("BUTTONORDER") Then
            If Not IsDBNull(row("BUTTONORDER")) Then
                _buttonOrder = row("BUTTONORDER")
            End If
        End If
        If row.Table.Columns.Contains("ICONID") Then
            _idicon = row("ICONID")
        End If
        If row.Table.Columns.Contains("GROUPNAME") Then
            If Not IsDBNull(row("GROUPNAME")) Then
                _groupName = row("GROUPNAME")
            Else
                _groupName = "Acciones"
            End If
        End If
        If row.Table.Columns.Contains("GROUPCLASS") Then
            If Not IsDBNull(row("GROUPCLASS")) Then
                _groupClass = row("GROUPCLASS")
            Else
                _groupClass = "list-group"
            End If
        End If

        If String.IsNullOrEmpty(_buttonId) Then
            _ruleId = 0
        Else
            _ruleId = _buttonId.Replace("zamba_rule_", String.Empty)
        End If

        If row.Table.Columns.Contains("STEP_ID") Then
            If Not IsDBNull(row("STEP_ID")) Then
                _stepId = row("STEP_ID")
            Else
                _stepId = "STEP_ID"
            End If
        End If

    End Sub
End Class
