﻿Imports Zamba.Core
Imports Zamba.WFExecution


<RuleCategory("Datos"), RuleDescription("Obtener datos desde un excel"), RuleHelp("Obtiene los datos de un excel y los guarda en una variable de zamba"), RuleFeatures(False)> <Serializable()>
Public Class DoImportFromExcel
    Inherits WFRuleParent
    Implements IDoImportFromExcel


#Region "Atributos"

    Private _playrule As PlayDoImportFromExcel
    Private _file As String
    Private _excelVersion As OfficeVersion
    Private _sheetName As String
    Private _varName As String
    Private _saveAsPath As String
    Private _saveAsFileName As String
    Private _saveAs As Boolean
    Private _useSpireConverter As Boolean



#End Region

#Region "Constructores"

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal file As String, ByVal excelversion As OfficeVersion, ByVal sheetname As String, ByVal varname As String, ByVal saveAs As Boolean, ByVal saveAsPath As String, ByVal saveAsFileName As String, ByVal UseSpireConverter As Boolean, ByVal IgnoreFirstRow As Boolean)
        MyBase.New(Id, Name, WFStepid)
        Me._file = file
        Me._excelVersion = excelversion
        Me._sheetName = sheetname
        Me._varName = varname
        Me._saveAs = saveAs
        Me._saveAsPath = saveAsPath
        Me.SaveAsFileName = saveAsFileName
        Me._playrule = New PlayDoImportFromExcel(Me)
        Me._useSpireConverter = UseSpireConverter
        Me.ignoreFirstRow = IgnoreFirstRow
    End Sub

#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get

        End Get
    End Property

    Public Overrides Sub Load()

    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return Me._playrule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return _playrule.Play(results)
    End Function
    Public Property File() As String Implements Core.IDoImportFromExcel.File
        Get
            Return Me._file
        End Get
        Set(ByVal value As String)
            Me._file = value
        End Set
    End Property

    Public Property SheetName() As String Implements Core.IDoImportFromExcel.SheetName
        Get
            Return Me._sheetName
        End Get
        Set(ByVal value As String)
            Me._sheetName = value
        End Set
    End Property

    Public Property ExcelVersion() As OfficeVersion Implements Core.IDoImportFromExcel.ExcelVersion
        Get
            Return Me._excelVersion
        End Get
        Set(ByVal value As OfficeVersion)
            Me._excelVersion = value
        End Set
    End Property

    Public Property VarName() As String Implements Core.IDoImportFromExcel.VarName
        Get
            Return Me._varName
        End Get
        Set(ByVal value As String)
            Me._varName = value
        End Set
    End Property



    Public Property SaveAs() As Boolean Implements IDoImportFromExcel.SaveAs
        Get
            Return _saveAs
        End Get
        Set(ByVal value As Boolean)
            _saveAs = value
        End Set
    End Property



    Public Property SaveAsPath() As String Implements IDoImportFromExcel.SaveAsPath
        Get
            Return _saveAsPath
        End Get
        Set(ByVal value As String)
            _saveAsPath = value
        End Set
    End Property



    Public Property SaveAsFileName() As String Implements IDoImportFromExcel.SaveAsFileName
        Get
            Return _saveAsFileName
        End Get
        Set(ByVal value As String)
            _saveAsFileName = value
        End Set
    End Property

    Public Property UseSpireConverter As Boolean Implements IDoImportFromExcel.UseSpireConverter
        Get
            Return _useSpireConverter
        End Get
        Set(ByVal value As Boolean)
            _useSpireConverter = value
        End Set
    End Property

    Public Property ignoreFirstRow As Boolean Implements IDoImportFromExcel.ignoreFirstRow

End Class