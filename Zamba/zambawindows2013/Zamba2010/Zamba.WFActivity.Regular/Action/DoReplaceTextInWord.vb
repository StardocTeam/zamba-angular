﻿Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Office"), RuleDescription("Reemplazar Texto en word"), RuleHelp("Permite reemplazar diferentes partes de un texto en un documento word a partir de uno o varios parametros."), RuleFeatures(False)> <Serializable()> _
Public Class DoReplaceTextInWord
    Inherits WFRuleParent
    Implements IDoReplaceTextInWord, IRuleValidate


#Region "Atributos"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As WFExecution.PlayDoReplaceTextInWord
    Private _wordPath As String
    Private _replaceFields As String
    Private _newPath As String
    Private _caseSensitive As Boolean
    Private _saveOriginalPath As Boolean
    Private _isValid As Boolean

#End Region

#Region "Constructor"

    Public Sub New(ByVal Id As Int64, ByVal name As String, ByVal wfstepid As Int64, ByVal wordpath As String,
                   ByVal replacefields As String, ByVal newpath As String, ByVal casesensitive As Boolean, ByVal saveorginal As Boolean)
        MyBase.New(Id, name, wfstepid)
        _replaceFields = replacefields
        playRule = New WFExecution.PlayDoReplaceTextInWord(Me)
        _caseSensitive = casesensitive
        _newPath = newpath
        _replaceFields = replacefields
        _wordPath = wordpath
        _saveOriginalPath = saveorginal
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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides Sub Load()

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

    Public Property WordPath() As String Implements IDoReplaceTextInWord.WordPath
        Get
            Return _wordPath
        End Get
        Set(ByVal value As String)
            _wordPath = value
        End Set
    End Property

    Public Property ReplaceFields() As String Implements IDoReplaceTextInWord.ReplaceFields
        Get
            Return _replaceFields
        End Get
        Set(ByVal value As String)
            _replaceFields = value
        End Set
    End Property

    Public Property SaveOriginalPath() As Boolean Implements IDoReplaceTextInWord.SaveOriginalPath
        Get
            Return _saveOriginalPath
        End Get
        Set(ByVal value As Boolean)
            _saveOriginalPath = value
        End Set
    End Property

    Public Property NewPath() As String Implements IDoReplaceTextInWord.NewPath
        Get
            Return _newPath
        End Get
        Set(ByVal value As String)
            _newPath = value
        End Set
    End Property

    Public Property CaseSensitive() As Boolean Implements IDoReplaceTextInWord.CaseSensitive
        Get
            Return _caseSensitive
        End Get
        Set(ByVal value As Boolean)
            _caseSensitive = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
           Return "Reemplazo texto de Word"
        End Get
    End Property
End Class