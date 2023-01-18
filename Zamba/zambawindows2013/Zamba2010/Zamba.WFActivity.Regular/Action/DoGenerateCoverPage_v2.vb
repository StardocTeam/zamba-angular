Imports System
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBussines
Imports System.Xml.Serialization

<RuleCategory("importacion"), RuleDescription("Generar Caratula"), RuleApproved("True"), RuleHelp("Genera una caratula a partir del documento seleccionado en la tarea"), RuleFeatures(True)> <Serializable()> _
Public Class DoGenerateCoverPage_v2
    Inherits WFRuleParent
    Implements IDoGenerateCoverPage_v2

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

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoGenerateCoverPage_v2()
        Return playRule.Play(results, Me)
    End Function


    Public Sub New(ByVal ruleID As Int64, ByVal ruleName As String, ByVal wfstepid As Int64, ByVal pDocTypeId As Int64, ByVal pPrintIndexs As Boolean, ByVal pNote As String, ByVal setprinter As Boolean, ByVal numcopies As Int16)
        MyBase.New(ruleID, ruleName, wfstepid)
        Me.DocTypeId = pDocTypeId
        Me.PrintIndexs = pPrintIndexs
        Me.Note = pNote
        Me._setPrinter = setprinter
        Me._copies = numcopies
    End Sub

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Generar Caratula"
        End Get
    End Property

    Dim mDocTypeId As Int64

    Public Property DocTypeId() As Int64 Implements IDoGenerateCoverPage_v2.DocTypeId
        Get
            Return mDocTypeId
        End Get
        Set(ByVal value As Int64)
            mDocTypeId = value
        End Set
    End Property


    Private mNote As String
    Public Property Note() As String Implements Core.IDoGenerateCoverPage_v2.Note
        Get
            Return mNote
        End Get
        Set(ByVal value As String)
            mNote = value
        End Set
    End Property

    Private mPrintIndexs As Boolean
    Public Property PrintIndexs() As Boolean Implements Core.IDoGenerateCoverPage_v2.PrintIndexs
        Get
            Return mPrintIndexs
        End Get
        Set(ByVal value As Boolean)
            mPrintIndexs = value
        End Set
    End Property

    Private _copies As Int16
    Public Property Copies() As Int16 Implements IDoGenerateCoverPage_v2.Copies
        Get
            Return _copies
        End Get
        Set(ByVal value As Int16)
            _copies = value
        End Set
    End Property

    Private _setPrinter As Boolean
    Public Property SetPrinter() As Boolean Implements IDoGenerateCoverPage_v2.SetPrinter
        Get
            Return _setPrinter
        End Get
        Set(ByVal value As Boolean)
            _setPrinter = value
        End Set
    End Property

End Class
