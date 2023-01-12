
Public Interface ITest

    Property Id() As Int64
    Property Name As String
    Property CreateDate As Date
    Property UpdateDate As Date
    Property LastExecutionDate As Date
    Property CreatedBy As Int64
    Property UpdatedBy As Int64
    Property ExecutedBy As Int64

    Property RuleId() As Int64
    Property DataDictionary As IDataDictionary
    Property TestType() As Int64


End Interface

