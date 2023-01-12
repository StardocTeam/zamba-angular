Public Interface IDoAdminFiles
    Inherits IRule
    Property Action() As FileActions
    Property TargetPath() As String
    Property SourceVar() As String
    Property ErrorVar() As String
    Property OutputDataType() As FWDataTypes
    Property DeleteVarFiles() As Boolean
    Property Overwrite() As Boolean
End Interface
