﻿Imports Zamba.Core

<RuleMainCategory("Zamba"), RuleCategory("Formularios"), RuleSubCategory(""), RuleDescription("Generar Formulario Dinámico"), RuleHelp("Crea un formulario dinámico a travez de un Entidad"), RuleFeatures(False)> <Serializable()> _
Public Class DoGenerateDinamicForm

    Inherits WFRuleParent
    Implements IDoGenerateDinamicForm, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDoGenerateDinamicForm

#Region "ATRIBUTOS"

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _DocType As Int64
    Private _FormId As Integer
    Private _variable As String
    Private _columnName As String
    Private _maskname As String
    Private _name As String
    Private _isValid As Boolean

#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal variable As String, ByVal doctype As Int64, ByVal FrmName As String, ByVal FormId As Integer, ByVal columnName As String)
        MyBase.New(Id, Name, WFStepid)

        Me.DocType = doctype
        Me.variable = variable
        Me.Name = FrmName
        Me.FormId = FormId
        Me.ColumnName = columnName
        playRule = New WFExecution.PlayDoGenerateDinamicForm(Me)
    End Sub

#Region "PROPIEDADES"
    Public Property ColumnName() As String Implements IDoGenerateDinamicForm.ColumnName
        Get
            Return _columnName
        End Get
        Set(ByVal value As String)
            _columnName = value
        End Set
    End Property

    Public Property variable() As String Implements IDoGenerateDinamicForm.Variable
        Get
            Return _variable
        End Get
        Set(ByVal value As String)
            _variable = value
        End Set
    End Property

    Public Property Name() As String Implements IDoGenerateDinamicForm.Name
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property FormId() As Integer Implements IDoGenerateDinamicForm.FormId
        Get
            Return _DocType
        End Get
        Set(ByVal value As Integer)
            _DocType = value
        End Set
    End Property

    Public Property DocType() As Int64 Implements IDoGenerateDinamicForm.DocType
        Get
            Return _DocType
        End Get
        Set(ByVal value As Int64)
            _DocType = value
        End Set
    End Property

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

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
#End Region

#Region "METODOS"

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overrides Sub Dispose()

    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

#End Region

End Class