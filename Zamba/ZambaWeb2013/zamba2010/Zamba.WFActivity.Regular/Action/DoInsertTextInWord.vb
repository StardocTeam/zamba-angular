Imports Zamba.Core

<RuleCategory("Datos"), RuleDescription("Agregar Texto en word"), RuleHelp("Permite agregar una tabla en un documento word a partir de uno o varios parametros."), RuleFeatures(False)> <Serializable()> _
Public Class DoInsertTextInWord
    Inherits WFRuleParent
    Implements IDoInsertTextInWord


#Region "Atributos"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As WFExecution.PlayDoInsertTextInWord
    Private _wordPath As String
    Private _variable As String
    Private _newPath As String
    Private _section As Int32
    Private _fontConfig As Boolean
    Private _font As String
    Private _fontSize As Single
    Private _style As Int32
    Private _color As String
    Private _backColor As String
    Private _textAsTable As Boolean
    Private _saveOriginalPath As Boolean
#End Region

#Region "Constructor"

    Public Sub New(ByVal Id As Int64, ByVal name As String, ByVal wfstepid As Int64, ByVal wordpath As String,
                   ByVal variables As String, ByVal newpath As String, ByVal section As Int32, ByVal saveOriginalPath As Boolean, ByVal fontConfig As Boolean, ByVal font As String, ByVal fontSize As Single, ByVal style As Int32, ByVal color As String, ByVal backColor As String, ByVal textAsTable As Boolean)
        MyBase.New(Id, name, wfstepid)
        Me.playRule = New WFExecution.PlayDoInsertTextInWord(Me)

        Me._newPath = newpath
        Me._variable = variables
        Me._wordPath = wordpath
        Me._section = section
        Me._saveOriginalPath = saveOriginalPath
        Me._fontConfig = fontConfig
        Me._font = font
        Me._fontSize = fontSize
        Me._style = style
        Me._color = color
        Me._backColor = backColor
        Me._textAsTable = textAsTable
    End Sub
#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

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

    Public Overrides Sub Load()

    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return Me.playRule.Play(results)
    End Function

    Public Overrides Function PlayWeb(ByVal results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As List(Of ITaskResult)
        Return Me.playRule.Play(results)
    End Function

    Public Property WordPath() As String Implements IDoInsertTextInWord.WordPath
        Get
            Return _wordPath
        End Get
        Set(ByVal value As String)
            _wordPath = value
        End Set
    End Property

    Public Property Variable() As String Implements IDoInsertTextInWord.Variable
        Get
            Return _variable
        End Get
        Set(ByVal value As String)
            _variable = value
        End Set
    End Property

    Public Property NewPath() As String Implements IDoInsertTextInWord.NewPath
        Get
            Return _newPath
        End Get
        Set(ByVal value As String)
            _newPath = value
        End Set
    End Property

    Public Property Section() As Int32 Implements IDoInsertTextInWord.Section
        Get
            Return _section
        End Get
        Set(ByVal value As Int32)
            _section = value
        End Set
    End Property

    Public Property SaveOriginalPath() As Boolean Implements IDoInsertTextInWord.SaveOriginalPath
        Get
            Return _saveOriginalPath
        End Get
        Set(value As Boolean)
            _saveOriginalPath = value
        End Set
    End Property
    Public Property FontConfig As Boolean Implements IDoInsertTextInWord.FontConfig
        Get
            Return Me._fontConfig
        End Get
        Set(value As Boolean)
            Me._fontConfig = value
        End Set
    End Property

    Public Property Font As String Implements IDoInsertTextInWord.Font
        Get
            Return Me._font
        End Get
        Set(value As String)
            Me._font = value
        End Set
    End Property

    Public Property FontSize As Single Implements IDoInsertTextInWord.FontSize
        Get
            Return Me._fontSize
        End Get
        Set(value As Single)
            Me._fontSize = value
        End Set
    End Property

    Public Property Style As Int32 Implements IDoInsertTextInWord.Style
        Get
            Return Me._style
        End Get
        Set(value As Int32)
            Me._style = value
        End Set
    End Property

    Public Property Color As String Implements IDoInsertTextInWord.Color
        Get
            Return Me._color
        End Get
        Set(value As String)
            Me._color = value
        End Set
    End Property

    Public Property BackColor As String Implements IDoInsertTextInWord.backColor
        Get
            Return Me._backColor
        End Get
        Set(value As String)
            Me._backColor = value
        End Set
    End Property

    Public Property TextAsTable As Boolean Implements IDoInsertTextInWord.textAsTable
        Get
            Return Me._textAsTable
        End Get
        Set(value As Boolean)
            Me._textAsTable = value
        End Set
    End Property


End Class

