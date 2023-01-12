Imports zamba.core
'Imports Zamba.Barcode.Business
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Barcode
''' Class	 : Barcode.AutocompleteBC
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''     
''' </summary>
''' <remarks>
'''     Esta clase se reubico dentro Zamba.Business.
''' </remarks>
''' <history>
''' 	[oscar]	07/06/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<Serializable()> Public Class AutocompleteBC
    Inherits AutocompleteBCBusiness
    
    Public Sub New()
        'Se instancia para el insertar, en el módulo de administrador
    End Sub
    Public Sub New(ByVal Dtid As Int32)
        'Me.DocTypeId = Dtid
        'Me._Index = New Zamba.Core.Index
        'Me._Index = BarcodesFactory.getIndexKey(Dtid)
        MyBase.New(Dtid)
    End Sub
    '    Dim _Index As Zamba.Core.Index
    '    Public ReadOnly Property Index() As Zamba.Core.Index
    '        Get
    '            Return Me._Index
    '        End Get
    '    End Property

    Public Overrides Sub Dispose()

    End Sub
End Class

Public Class AutoCompleteBarcode_Factory
    Private Shared AC As AutocompleteBC
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Objeto AutoCompleteBC si hay configurado autocompletar en base al DocTypeID y al Indice seleccionado
    ''' </summary>
    ''' <param name="DocTypeID"></param>
    ''' <param name="IndexId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Se debe evaluar si el resultado de esto es NOTHING
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetComplete(ByVal DocTypeID As Int32, ByVal IndexId As Int32) As AutocompleteBC
        Dim BB As New BarcodesBusiness
        If IsNothing(BB.GetAutoIndexs(DocTypeID, IndexId)) Then
            Return Nothing
        Else
            AC = New AutocompleteBC(DocTypeID)
            Return AC
        End If
    End Function
    'Private Shared Function GetAutoIndexs(ByVal dt As Int32, ByVal IndexId As Int32) As DataSet
    '    Dim ds As DataSet
    '    Dim sql As String = "Select * from ZBarcodeComplete where DocTypeID=" & dt & " and Indexid=" & IndexId & " and clave=1"
    '    ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
    '    If ds.Tables(0).Rows.Count = 0 Then
    '        Return Nothing
    '    End If
    '    Return ds
    'End Function
End Class
