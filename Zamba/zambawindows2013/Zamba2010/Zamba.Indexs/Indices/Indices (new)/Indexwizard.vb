''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Indexs
''' Class	 : Indexs.Indexwizard
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Factory para crear atributos mediante el Asistente
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	05/07/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Indexwizard
    Inherits Zamba.Core.Index

    Public Enum TExpresiones
        Mail = 1
        Web = 2
    End Enum
#Region "Clases"
    Public Class Visual
        Public Viewer As Boolean
        Public Indexer As Boolean
        Public Caratulas As Boolean
        Public Search As Boolean
    End Class
    Public Class TObligatorio
        Public LotusNotes As Boolean = False
        Public Zamba As Boolean = False
    End Class
    Public Class Expresiones
        Public DeSistema As TExpresiones
        Public DeUsuario As String
    End Class
#End Region

    Public Visibilidad As Visual
    Public Obligatorio As TObligatorio
    Public ExpresionesRegulares As Expresiones
#Region "Constructores"
    Public Sub New()
        MyBase.New()
        Visibilidad = New Visual
        Obligatorio = New TObligatorio
        ExpresionesRegulares = New Expresiones
    End Sub
    Public Sub New(ByVal indexName As String, ByVal Index_Id As Integer, ByVal indexType As Int16, ByVal Index_Len As Integer, ByVal DropDown As Int16)
        MyBase.New(indexName, Index_Id, indexType, Index_Len, IndexAdditionalType.DropDown)
        Visibilidad = New Visual
        Obligatorio = New TObligatorio
        ExpresionesRegulares = New Expresiones
    End Sub
#End Region
End Class

