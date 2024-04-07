Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Office"), RuleDescription("Exportar a Excel"), RuleHelp("Genera un archivo de Excel con la tarea actual"), RuleFeatures(False)> <Serializable()> _
Public Class DoExportToExcel
    Inherits WFRuleParent
    Implements IDoExportToExcel, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoExportToExcel
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
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Private m_sRuta As String
    Private m_sStepName As String
    Public Overrides Sub Dispose()

    End Sub
    Public Property Ruta() As String Implements IDoExportToExcel.Ruta
        Get
            Return m_sRuta
        End Get
        Set(ByVal value As String)
            m_sRuta = value
        End Set
    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal p_sRuta As String)
        MyBase.New(Id, Name, wfstepid)
        m_sRuta = p_sRuta
        m_sStepName = WFStepBusiness.GetStepNameById(wfstepid)
        playRule = New WFExecution.PlayDoExportToExcel(Me)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Dim o As New ArrayList(0)
        o.Add(m_sRuta)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
End Class