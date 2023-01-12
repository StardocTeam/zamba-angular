<AttributeUsage(AttributeTargets.Class)> _
Public Class RuleCategory
    Inherits Attribute
    Implements IRuleCategory

    ' The constructor is called when the attribute is set.
    Public Sub New(ByVal Category As String)
        _Category = Category
    End Sub

    ' Keep a variable internally ...
    Protected _Category As String

    ' .. and show a copy to the outside world.
    Public Property Category() As String Implements IRuleCategory.Category
        Get
            Return _Category
        End Get
        Set(ByVal Value As String)
            _Category = Value
        End Set
    End Property

End Class

<AttributeUsage(AttributeTargets.Class)> _
Public Class RuleDescription
    Inherits Attribute
    Implements IRuleDescription

    ' The constructor is called when the attribute is set.
    Public Sub New(ByVal Description As String)
        _description = Description
    End Sub

    ' Keep a variable internally ...
    Protected _description As String

    ' .. and show a copy to the outside world.
    Public Property Description() As String Implements IRuleDescription.Description
        Get
            Return _description
        End Get
        Set(ByVal Value As String)
            _description = Value
        End Set
    End Property

End Class

<AttributeUsage(AttributeTargets.Class)> _
Public Class RuleHelp
    Inherits Attribute
    Implements IRuleHelp

    ' The constructor is called when the attribute is set.
    Public Sub New(ByVal Help As String)
        _help = Help
    End Sub

    ' Keep a variable internally ...
    Protected _help As String

    ' .. and show a copy to the outside world.
    Public Property Help() As String Implements IRuleHelp.Help
        Get
            Return _help
        End Get
        Set(ByVal Value As String)
            _help = Value
        End Set
    End Property

End Class

<AttributeUsage(AttributeTargets.Class)> _
Public Class RuleFeatures
    Inherits Attribute
    Implements IRuleFeatures


    Public Sub New(ByVal blnIsUI As Boolean)
        Me.Is_UI = blnIsUI
    End Sub


    ' Keep a variable internally ...
    Protected Is_UI As Boolean

    ' .. and show a copy to the outside world.
    Public Property IsUI() As Boolean Implements IRuleFeatures.IsUI
        Get
            Return Me.Is_UI
        End Get
        Set(ByVal value As Boolean)
            Me.Is_UI = value
        End Set
    End Property

End Class

<AttributeUsage(AttributeTargets.Class)> _
Public Class RuleIconId
    Inherits Attribute
    Implements IRuleIconId


    Public Sub New(ByVal RuleIconId As String)
        Me._RuleIconId = RuleIconId
    End Sub


    ' Keep a variable internally ...
    Protected _RuleIconId As String

    ' .. and show a copy to the outside world.
    Public Property IconId() As String Implements IRuleIconId.IconId
        Get
            Return Me._RuleIconId
        End Get
        Set(ByVal value As String)
            Me._RuleIconId = value
        End Set
    End Property

End Class
