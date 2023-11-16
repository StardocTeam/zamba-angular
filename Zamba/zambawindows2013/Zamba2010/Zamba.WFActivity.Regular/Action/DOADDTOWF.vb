Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory("Workflow"), RuleSubCategory("Asociar nuevo"), RuleDescription("Agregar a Workflow"), RuleHelp("Permite agregar un documento al workflow."), RuleFeatures(False)> <Serializable()> _
Public Class DOADDTOWF
    Inherits WFRuleParent
    Implements IDOADDTOWF, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOADDTOWF
    Private _isValid As Boolean

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepID As Int64, ByVal p_WorkId As Int32, ByVal p_openTask As Boolean)
        ' La variable wfStepId no es la etapa inicial del workflow (p_WorkId) al que se quiere agregar la tarea
        ' Corresponde al workflow en donde est� contenido actualmente el documento y la regla
        MyBase.New(Id, Name, wfStepID)
        m_iWorkId = p_WorkId
        m_openTask = p_openTask
        playRule = New WFExecution.PlayDOADDTOWF(Me)
    End Sub

    'Properties
    Private m_iWorkId As Int32
    Private m_openTask As Boolean

    Public Function WorkFlowName() As String Implements IDOADDTOWF.WorkFlowName
        Return Zamba.Data.WFFactory.GetWfNameById(m_iWorkId)
    End Function

    Public Property WorkId() As Int32 Implements IDOADDTOWF.WorkId
        Get
            Return m_iWorkId
        End Get
        Set(ByVal value As Int32)
            m_iWorkId = value
        End Set
    End Property



    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        'Dim o As New ArrayList(0)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Try
                If m_iWorkId = 0 Then
                    Return "Agregar a WorkFlow."
                Else
                    Return "Agregar al WorkFlow " & WorkFlowName()
                End If
                'Return "Valido o Realizo"
            Catch ex As Exception
                raiseerror(ex)
            End Try
            Return "Agregar a WorkFlow."
        End Get
    End Property

    Public Property IDOADDTOWF_openTask As Boolean Implements IDOADDTOWF.openTask
        Get
            Return m_openTask
        End Get
        Set(value As Boolean)
            m_openTask = value
        End Set
    End Property

End Class