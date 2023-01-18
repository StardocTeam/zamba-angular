Public Interface IDoDismemberObject_v2

    ReadOnly Property Zvars() As List(Of IDoDismemberObject_v2.IZvarVariable)
    Property AssemblyPath() As String
    Property RawZvars() As String
    Property ObjectName() As String

    Sub AddZvar(ByVal className As String, ByVal propertyName As String, ByVal zvarValue As String)
    Function ParseZvars(ByVal rawValue As String) As List(Of IZvarVariable)
    Function ParseZvar(ByVal rawValue As List(Of IZvarVariable)) As String

    Public Interface IZvarVariable
        Property ClassName() As String
        Property PropertyName() As String
        Property ZvarName() As String
    End Interface
End Interface
