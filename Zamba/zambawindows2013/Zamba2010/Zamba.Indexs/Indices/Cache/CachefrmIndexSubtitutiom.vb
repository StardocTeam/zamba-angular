Imports Zamba.Core

Public Class CachefrmIndexSubtitutiom
    Public Shared IndexSubtitutionControlList As New Hashtable

    Public Shared Function GetFrmIndexSubtitutionControl(IndexId As Int64, AutoSustitucionTable As DataTable, ReloadTable As Boolean) As frmIndexSubtitutiom
        If IndexSubtitutionControlList.ContainsKey(IndexId) AndAlso Not IndexSubtitutionControlList(IndexId) Is Nothing AndAlso ReloadTable = False Then

            Return IndexSubtitutionControlList(IndexId)
        Else
            Dim frmIndexSubtitutiom As New frmIndexSubtitutiom(IndexId, AutoSustitucionTable)
            If Not IndexSubtitutionControlList.ContainsKey(IndexId) Then
                IndexSubtitutionControlList.Add(IndexId, frmIndexSubtitutiom)
            Else
                IndexSubtitutionControlList(IndexId) = frmIndexSubtitutiom
            End If
            Return frmIndexSubtitutiom
        End If
    End Function

    Public Shared IndexSubtitutionTableList As New Hashtable

    Public Shared ReadOnly Property AutoSustitucionTable(IndexId As Int64, ParentIndexId As Int64, ParentDataTemp As String) As DataTable
        Get
            If IndexSubtitutionTableList.ContainsKey(IndexId) Then
                Return IndexSubtitutionTableList(IndexId)
            Else
                Dim _AutoSubstitucionTable As DataTable
                If ParentIndexId > 0 Then
                    _AutoSubstitucionTable = IndexsBussinesExt.GetHierarchicalTableByValue(IndexId, ParentIndexId, ParentDataTemp, True)
                    'Se aplica un parche para que la tabla _AutoSubstitucionTable tenga 
                    'el mismo nombre en sus columnas en todo el código de la clase.
                    _AutoSubstitucionTable.Columns("Value").ColumnName = AutoSubstitutionBusiness.NombreColumnaCodigo
                    _AutoSubstitucionTable.Columns("Description").ColumnName = AutoSubstitutionBusiness.NombreColumnaDescripcion
                Else
                    _AutoSubstitucionTable = AutoSubstitutionBusiness.GetIndexData(IndexId, False)
                End If

                If Not IndexSubtitutionTableList.ContainsKey(IndexId) Then
                    IndexSubtitutionTableList.Add(IndexId, _AutoSubstitucionTable)
                Else
                    IndexSubtitutionTableList(IndexId) = _AutoSubstitucionTable
                End If
                Return _AutoSubstitucionTable
            End If

        End Get
    End Property


End Class
