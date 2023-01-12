Public Interface IDOApproveMatrix
    Inherits IRule
    Property MatrixEntityId As Int64
    Property OutputIndex1 As Int64
    Property OutputIndex2 As Int64
    Property OutputIndex3 As Int64
    Property OutputVariable1 As String
    Property OutputVariable2 As String
    Property OutputVariable3 As String

    Property AmountIndex As Int64
    Property SecuenceIndex As Int64
    Property LevelIndex As Int64
    Property ApproverIndex As Int64

    Property ApproverVariable As String
    Property SecuenceVariable As String
    Property LevelVariable As String

    Property ApproversListVariable As String
    Property IsApproverVariable As String

    Property RegistryEntityId As Int64
    Property RegistryIdIndex As Int64
    Property RegistryActionIndex As Int64
    Property RegistryActions As String

End Interface