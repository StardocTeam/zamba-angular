﻿Imports Zamba.Core

<RuleCategory("Datos"), RuleDescription("Completar tabla en Word"), RuleHelp("Permite completar con datos una tabla en un documento word respetando el formato el formato establecido de la misma."), RuleFeatures(False)> <Serializable()> _
Public Class DoCompleteTableInWord
    Inherits WFRuleParent
    Implements IDoCompleteTableInWord

#Region "Atributos"

    Private _tableIndex As Int64
    Private _pageIndex As Int64
    Private _fullPath As String
    Private _varName As String
    Private _withHeader As String
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _dataTable As String
    Private _inTable As Boolean
    Private _rownumber As Int64
    Private _fontConfig As Boolean
    Private _font As String
    Private _fontSize As Single
    Private _style As Int32
    Private _color As String
    Private _backColor As String
    Private _saveOriginalPath As Boolean
    Private playRule As WFExecution.PlayDoCompleteTableInWord

#End Region

#Region "Constructor"

    Public Sub New(ByVal Id As Int64, ByVal name As String, ByVal wfstepid As Int64, ByVal tableindex As Int64,
                   ByVal pageindex As Int64, ByVal fullpath As String, ByVal varname As String, ByVal withheader As Boolean, ByVal datatable As _
                   String, ByVal intable As Boolean, ByVal rownumber As Int64, ByVal FontConfig As Boolean, ByVal Font As String, ByVal FontSize _
                   As Single, ByVal style As Int32, ByVal color As String, ByVal BackColor As String, ByVal SaveOriginalPath As Boolean)

        MyBase.New(Id, name, wfstepid)
        Me._tableIndex = tableindex
        Me._pageIndex = pageindex
        Me._fullPath = fullpath
        Me._varName = varname
        Me._withHeader = withheader
        Me._dataTable = datatable
        Me._inTable = intable
        Me._rownumber = rownumber
        Me._fontConfig = FontConfig
        Me._font = Font
        Me._fontSize = FontSize
        Me._style = style
        Me._color = color
        Me._backColor = BackColor
        Me._saveOriginalPath = SaveOriginalPath
        Me.playRule = New WFExecution.PlayDoCompleteTableInWord(Me)


    End Sub

#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return Me._isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return Me._isLoaded
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


    Public Property FullPath As String Implements Core.IDoCompleteTableInWord.FullPath
        Get
            Return Me._fullPath
        End Get
        Set(value As String)
            Me._fullPath = value
        End Set
    End Property

    Public Property DataTable As String Implements Core.IDoCompleteTableInWord.DataTable
        Get
            Return Me._dataTable
        End Get
        Set(value As String)
            Me._dataTable = value
        End Set
    End Property

    Public Property PageIndex As Long Implements Core.IDoCompleteTableInWord.PageIndex
        Get
            Return Me._pageIndex
        End Get
        Set(value As Long)
            Me._pageIndex = value
        End Set
    End Property

    Public Property TableIndex As Long Implements Core.IDoCompleteTableInWord.TableIndex
        Get
            Return Me._tableIndex
        End Get
        Set(value As Long)
            Me._tableIndex = value
        End Set
    End Property

    Public Property VarName As String Implements Core.IDoCompleteTableInWord.VarName
        Get
            Return Me._varName
        End Get
        Set(value As String)
            Me._varName = value
        End Set
    End Property

    Public Property WithHeader As Boolean Implements Core.IDoCompleteTableInWord.WithHeader
        Get
            Return Me._withHeader
        End Get
        Set(value As Boolean)
            Me._withHeader = value
        End Set
    End Property

    Public Property InTable As Boolean Implements Core.IDoCompleteTableInWord.InTable
        Get
            Return Me._inTable
        End Get
        Set(value As Boolean)
            Me._inTable = value
        End Set
    End Property

    Public Property RowNumber As Int64 Implements Core.IDoCompleteTableInWord.RowNumber
        Get
            Return Me._rownumber
        End Get
        Set(value As Int64)
            Me._rownumber = value
        End Set
    End Property

    Public Property FontConfig As Boolean Implements Core.IDoCompleteTableInWord.FontConfig
        Get
            Return Me._fontConfig
        End Get
        Set(value As Boolean)
            Me._fontConfig = value
        End Set
    End Property

    Public Property Font As String Implements Core.IDoCompleteTableInWord.Font
        Get
            Return Me._font
        End Get
        Set(value As String)
            Me._font = value
        End Set
    End Property

    Public Property FontSize As Single Implements Core.IDoCompleteTableInWord.FontSize
        Get
            Return Me._fontSize
        End Get
        Set(value As Single)
            Me._fontSize = value
        End Set
    End Property

    Public Property Style As Int32 Implements Core.IDoCompleteTableInWord.Style
        Get
            Return Me._style
        End Get
        Set(value As Int32)
            Me._style = value
        End Set
    End Property

    Public Property Color As String Implements Core.IDoCompleteTableInWord.Color
        Get
            Return Me._color
        End Get
        Set(value As String)
            Me._color = value
        End Set
    End Property

    Public Property BackColor As String Implements Core.IDoCompleteTableInWord.BackColor
        Get
            Return Me._backColor
        End Get
        Set(value As String)
            Me._backColor = value
        End Set
    End Property

    Public Property SaveOriginalPath As Boolean Implements Core.IDoCompleteTableInWord.SaveOriginalPath
        Get
            Return Me._saveOriginalPath
        End Get
        Set(value As Boolean)
            Me._saveOriginalPath = value
        End Set
    End Property

End Class