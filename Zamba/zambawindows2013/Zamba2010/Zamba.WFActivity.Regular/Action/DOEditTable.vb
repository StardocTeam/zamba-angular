Imports Zamba.Core

<RuleMainCategory("Base de Datos"), RuleCategory("Edicion"), RuleSubCategory(""), RuleDescription("Editar Tabla de Datos"), RuleHelp("Permite editar los datos obtenidos de una variable"), RuleFeatures(False)> <Serializable()> _
Public Class DOEditTable
    Inherits WFRuleParent
    Implements IDOEditTable, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean

    Private playRule As Zamba.WFExecution.PlayDOEDITTABLE
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

    Private m_VarSource As String
    Private m_VarDestiny As String
    Private m_KeyColumn As String
    Private m_EditColumn As String
    Private m_EditType As Int64


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal p_VarSource As String, ByVal p_KeyColumn As String, ByVal p_EditColumn As String, ByVal p_EditType As Int64, ByVal p_VarDestiny As String)
        MyBase.New(Id, Name, WFStepid)
        m_VarSource = p_VarSource
        m_KeyColumn = p_KeyColumn
        m_EditColumn = p_EditColumn
        m_EditType = p_EditType
        m_VarDestiny = p_VarDestiny
        playRule = New WFExecution.PlayDOEDITTABLE(Me)
    End Sub

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
    Public Property EditColumn() As String Implements IDOEditTable.EditColumn
        Get
            Return m_EditColumn
        End Get
        Set(ByVal value As String)
            m_EditColumn = value
        End Set
    End Property

    Public Property VarDestiny() As String Implements IDOEditTable.VarDestiny
        Get
            Return m_VarDestiny
        End Get
        Set(ByVal value As String)
            m_VarDestiny = value
        End Set
    End Property

    Public Property VarSource() As String Implements IDOEditTable.VarSource
        Get
            Return m_VarSource
        End Get
        Set(ByVal value As String)
            m_VarSource = value
        End Set
    End Property

    Public Property KeyColumn() As String Implements IDOEditTable.KeyColumn
        Get
            Return m_KeyColumn
        End Get
        Set(ByVal value As String)
            m_KeyColumn = value
        End Set
    End Property

    Public Property EditType() As Int64 Implements IDOEditTable.EditType
        Get
            Return m_EditType
        End Get
        Set(ByVal value As Int64)
            m_EditType = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property
End Class
