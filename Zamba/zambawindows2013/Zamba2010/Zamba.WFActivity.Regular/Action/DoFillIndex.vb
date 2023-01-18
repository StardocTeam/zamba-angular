Imports Zamba.Core
Imports System.Text

''' Project	 : Zamba.Business
''' Class	 : Core.DoFillIndex
''' <summary>
''' Regla para completar Atributos
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
<RuleMainCategory("Atributos"), RuleCategory("Autocompletar"), RuleSubCategory(""), RuleDescription("Completar Atributos"), RuleHelp("Permite completar atributos con valores sin interaccion con el usuario"), RuleFeatures(False)> <Serializable()> _
Public Class DoFillIndex
    Inherits WFRuleParent
    Implements IDoFillIndex, IRuleValidate
    Private _indexId As String
    Private _primaryValue As String
    Private _secondaryValue As String
    Private _OverWriteIndex As String
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoFillIndex

    Public Overrides Sub Dispose()

    End Sub

    Private _isLoaded As Boolean
    Private _isFull As Boolean


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
    Public Property OverWriteIndex() As String Implements IDoFillIndex.OverWriteIndex
        Get
            Return _OverWriteIndex
        End Get
        Set(ByVal value As String)
            _OverWriteIndex = value
        End Set
    End Property

    Public Property IndexId() As String Implements IDoFillIndex.IndexId
        Get
            Return _indexId
        End Get
        Set(ByVal value As String)
            _indexId = value
        End Set
    End Property

    Public Property PrimaryValue() As String Implements IDoFillIndex.PrimaryValue
        Get
            Return _primaryValue
        End Get
        Set(ByVal value As String)
            _primaryValue = value
        End Set
    End Property

    Public Property SecondaryValue() As String Implements IDoFillIndex.SecondaryValue
        Get
            Return _secondaryValue
        End Get
        Set(ByVal value As String)
            _secondaryValue = value
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

    Public Sub ClearRule() Implements IDoFillIndex.ClearRule
        IndexId = String.Empty
        PrimaryValue = String.Empty
        SecondaryValue = String.Empty
        OverWriteIndex = String.Empty
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal Index_Id As String, ByVal primaryValue As String, ByVal secondaryValue As String, ByVal OverWrite As String)
        MyBase.New(Id, Name, wfstepid)

        IndexId = Index_Id
        Me.PrimaryValue = primaryValue
        Me.SecondaryValue = secondaryValue
        OverWriteIndex = OverWrite

        playRule = New Zamba.WFExecution.PlayDoFillIndex(Me)
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

 Public Overrides ReadOnly Property MaskName() As String
        Get
            Dim strBuilder As StringBuilder
            strBuilder = New StringBuilder
            strBuilder.Append("Completo el indice ")

            If IndexId <> 0 Then
                Dim index As IIndex = IndexsBusiness.GetIndexById(IndexId, True)
                If Not index Is Nothing Then
                    strBuilder.Append(index.Name)
                Else
                    strBuilder.Append(IndexId)
                End If
                strBuilder.Append(" con el valor ")

                If Index.Type = IndexDataType.Si_No Then
                    If Index.Data = 0 Then
                        strBuilder.Append("Falso")
                    Else
                        strBuilder.Append("Verdadero")
                    End If
                Else
                    strBuilder.Append(Index.Data)
                End If
            End If

            Return strBuilder.ToString
            strBuilder = Nothing
        End Get
    End Property
End Class
