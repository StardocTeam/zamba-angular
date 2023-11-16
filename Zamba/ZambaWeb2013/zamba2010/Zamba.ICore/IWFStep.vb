Public Interface IWFStep
    Inherits IZambaCore

    Property WorkId() As Int64
    Property Description() As String
    Property Location() As Drawing.Point
    Property color() As String
    Property Width() As Int32
    Property Height() As Int32
    Property CreateDate() As Date
    Property EditDate() As Date
    Property Help() As String
    Property MaxHours() As Decimal
    Property MaxDocs() As Int32
    Property StartAtOpenDoc() As Boolean
    Property DocumentsCount() As Int32

    '<Obsolete("Evaluar si vale la pena usar el objeto completo en lugar del id", False)> _
    'Property WorkFlow() As IWorkFlow
    Property States() As List(Of IWFStepState)
    Property InitialState() As IWFStepState
    '    Property Users() As SortedList
    '   Property Groups() As SortedList
    Property RuleTareaIniciada() As IWFRuleParent
    Property RuleTareaFinalizada() As IWFRuleParent
    Property RuleTareaDerivada() As IWFRuleParent
    Property RuleTareaRechazada() As IWFRuleParent
    Property Rules() As Generic.List(Of IWFRuleParent)
    Property TasksCount() As Int32
    Property ExpiredTasksCount() As Int32
    ReadOnly Property TasksExpired() As ArrayList
    Event CountOfDocumentsChanged()
    Event SendMsg2Client(ByVal Action As String, ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal StepId As Int64)
    Sub raisemsg2Client(ByVal Action As String, ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal StepId As Int64)
End Interface