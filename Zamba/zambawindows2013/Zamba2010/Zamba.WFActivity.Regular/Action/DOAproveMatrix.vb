Imports Zamba.Core


<RuleMainCategory("Tareas"), RuleCategory("Desicion"), RuleSubCategory(""), RuleDescription("Resolver Matriz de Aprobacion"), RuleHelp("Resuleve una tabla de datos, para la secuencia de aprobaciones"), RuleFeatures(False)> <Serializable()>
Public Class DOApproveMatrix
    Inherits WFRuleParent
    Implements IDOApproveMatrix, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDOApproveMatrix
    Public Overrides ReadOnly Property IsFull() As Boolean

    Public Overrides ReadOnly Property IsLoaded() As Boolean

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub



    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64,
                   ByVal MatrixEntityId As Int64,
                   ByVal AmountIndex As Int64,
                   ByVal SecuenceIndex As Int64,
                   ByVal LevelIndex As Int64,
                   ByVal ApproverVariable As String,
                   ByVal SecuenceVariable As String,
                   ByVal LevelVariable As String,
                   ByVal OutputIndex1 As Int64,
                   ByVal OutputIndex2 As Int64,
                   ByVal OutputIndex3 As Int64,
                   ByVal OutputVariable1 As String,
                   ByVal OutputVariable2 As String,
                   ByVal OutputVariable3 As String,
                   ByVal RegistryEntityId As Int64,
                   ByVal RegistryActionIndex As Int64,
                   ByVal RegistryIdIndex As Int64,
                   ByVal Actions As String,
                   ByVal ApproverIndex As Int64, ByVal IsApproverVariable As String, ByVal ApproversListVariable As String
                   )
        MyBase.New(Id, Name, WFStepid)
        Me.MatrixEntityId = MatrixEntityId

        Me.AmountIndex = AmountIndex
        Me.SecuenceIndex = SecuenceIndex
        Me.LevelIndex = LevelIndex
        Me.SecuenceVariable = SecuenceVariable
        Me.LevelVariable = LevelVariable

        Me.OutputIndex1 = OutputIndex1
        Me.OutputIndex2 = OutputIndex2
        Me.OutputIndex3 = OutputIndex3
        Me.OutputVariable1 = OutputVariable1
        Me.OutputVariable2 = OutputVariable2
        Me.OutputVariable3 = OutputVariable3

        Me.RegistryEntityId = RegistryEntityId
        Me.RegistryActionIndex = RegistryActionIndex
        Me.RegistryIdIndex = RegistryIdIndex
        Me.RegistryActions = RegistryActions

        Me.ApproverIndex = ApproverIndex
        Me.ApproverVariable = ApproverVariable

        Me.IsApproverVariable = IsApproverVariable
        Me.ApproversListVariable = ApproversListVariable

        playRule = New Zamba.WFExecution.PlayDOApproveMatrix(Me)
    End Sub

    Public Property IsValid As Boolean Implements IRuleValidate.isValid

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Property MatrixEntityId As Long Implements IDOApproveMatrix.MatrixEntityId

    Public Property OutputIndex1 As Long Implements IDOApproveMatrix.OutputIndex1
    Public Property OutputIndex2 As Long Implements IDOApproveMatrix.OutputIndex2
    Public Property OutputIndex3 As Long Implements IDOApproveMatrix.OutputIndex3
    Public Property OutputVariable1 As String Implements IDOApproveMatrix.OutputVariable1
    Public Property OutputVariable2 As String Implements IDOApproveMatrix.OutputVariable2
    Public Property OutputVariable3 As String Implements IDOApproveMatrix.OutputVariable3
    Public Property AmountIndex As Long Implements IDOApproveMatrix.AmountIndex
    Public Property SecuenceIndex As Long Implements IDOApproveMatrix.SecuenceIndex
    Public Property LevelIndex As Long Implements IDOApproveMatrix.LevelIndex
    Public Property RegistryActionIndex As Long Implements IDOApproveMatrix.RegistryActionIndex

    Public Property ApproverIndex As Long Implements IDOApproveMatrix.ApproverIndex


    Public Property RegistryEntityId As Long Implements IDOApproveMatrix.RegistryEntityId


    Public Property RegistryIdIndex As Long Implements IDOApproveMatrix.RegistryIdIndex


    Public Property RegistryActions As String Implements IDOApproveMatrix.RegistryActions

    Public Property ApproverVariable As String Implements IDOApproveMatrix.ApproverVariable

    Public Property SecuenceVariable As String Implements IDOApproveMatrix.SecuenceVariable


    Public Property LevelVariable As String Implements IDOApproveMatrix.LevelVariable

    Public Property ApproversListVariable As String Implements IDOApproveMatrix.ApproversListVariable

    Public Property IsApproverVariable As String Implements IDOApproveMatrix.IsApproverVariable


    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
End Class