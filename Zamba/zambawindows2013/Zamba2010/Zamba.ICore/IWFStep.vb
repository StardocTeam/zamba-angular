Public Interface IWFStep
    Inherits IZambaCore

    Property WorkId() As Int64
    Property Description() As String
    Property MaxHours() As Decimal
    Property MaxDocs() As Int32
    Property StartAtOpenDoc() As Boolean
    Property DocumentsCount() As Int32
    Property States() As List(Of IWFStepState)
    Property InitialState() As IWFStepState
    Property DSRules() As DsRules ' Generic.List(Of IWFRuleParent)
    Property ExpiredTasksCount() As Int32
    Event CountOfDocumentsChanged()
    Event SendMsg2Client(ByVal Action As String, ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal StepId As Int64)
    Sub raisemsg2Client(ByVal Action As String, ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal StepId As Int64)
End Interface
