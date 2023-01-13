Imports Zamba.Core


''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.CoreData
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Funciones comunes para Zamba
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public NotInheritable Class CoreDataTables
    Inherits ZClass
    Public Overrides Sub Dispose()

    End Sub

    Public Shared Function VerificarTablas() As ArrayList
        Dim DSDT As New DataSet 'Dataset de Doc_Type
        Dim DSDB As New DataSet 'Dataset con las tablas reales
        Dim Arreglo As New ArrayList
        Dim i As Int32
        'Dim X As Int32 = 0
        Dim DocTypeId As String
        'Stores:
        'Get_DocTypesID: devuelve Id y Nombre de Doc_Types
        'ContarTablas: Muestra todas las tablas Doc_XX
        Try
            If Server.isOracle Then

                Dim parValues() As Object = {2}
                DSDT = Server.Con.ExecuteDataset("Get_DocTypesID_Pkg.Get_DocTypesID", parValues)
                DSDB = Server.Con.ExecuteDataset("ContarTablas_PKG.ContarTablas", parValues)
                If DSDT.Tables(0).Rows.Count > 0 Then
                    For i = 0 To DSDT.Tables(0).Rows.Count - 1
                        DocTypeId = DSDT.Tables(0).Rows(i).Item("Doc_Type_Id")
                        Dim DT As String = "DOC_T" & DocTypeId
                        'Dim DI As String = "DOC_I" & DocTypeId
                        'Dim DD As String = "DOC_D" & DocTypeId

                        Dim FindT As Boolean
                        Dim FindI As Boolean
                        Dim FindD As Boolean
                        Dim DVT As New DataView(DSDB.Tables(0), "Table_Name = '" & DT & "'", "TABLE_NAME", DataViewRowState.CurrentRows)
                        If DVT.Count > 0 Then
                            FindT = True
                        Else
                            'Lo agrego al Dataset
                            Arreglo.Add(DT.ToString)
                        End If
                        Dim DVI As New DataView(DSDB.Tables(0), "TABLE_NAME = '" & DT & "'", "TABLE_NAME", DataViewRowState.CurrentRows)
                        If DVI.Count > 0 Then
                            FindI = True
                        Else
                            Arreglo.Add(DT.ToString)
                        End If
                        Dim DVD As New DataView(DSDB.Tables(0), "TABLE_NAME = '" & DT & "'", "TABLE_NAME", DataViewRowState.CurrentRows)
                        If DVD.Count > 0 Then
                            FindD = True
                        Else
                            Arreglo.Add(DT.ToString)
                        End If
                    Next
                End If
            Else   'SQL Server
                DSDT = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "Get_DocTypesID")
                DSDB = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "ContarTablas")
                If DSDT.Tables(0).Rows.Count > 0 Then
                    For i = 0 To DSDT.Tables(0).Rows.Count - 1
                        DocTypeId = DSDT.Tables(0).Rows(i).Item("Doc_Type_Id")
                        Dim DT As String = "DOC_T" & DocTypeId
                        Dim DI As String = "DOC_I" & DocTypeId
                        'Dim DD As String = "DOC_D" & DocTypeId

                        Dim FindT As Boolean
                        Dim FindI As Boolean
                        Dim FindD As Boolean
                        Dim DVT As New DataView(DSDB.Tables(0), "OBJECT_NAME = '" & DT & "'", "OBJECT_NAME", DataViewRowState.CurrentRows)
                        If DVT.Count > 0 Then
                            FindT = True
                        Else
                            'Lo agrego al Dataset
                            Arreglo.Add(DT.ToString)
                            'X = X + 1
                        End If
                        Dim DVI As New DataView(DSDB.Tables(0), "OBJECT_NAME = '" & DT & "'", "OBJECT_NAME", DataViewRowState.CurrentRows)
                        If DVI.Count > 0 Then
                            FindI = True
                        Else
                            Arreglo.Add(DT.ToString)
                            'X = X + 1
                        End If
                        Dim DVD As New DataView(DSDB.Tables(0), "OBJECT_NAME = '" & DT & "'", "OBJECT_NAME", DataViewRowState.CurrentRows)
                        If DVD.Count > 0 Then
                            FindD = True
                        Else
                            Arreglo.Add(DT.ToString)
                            'X = X + 1
                        End If
                    Next
                End If
            End If
            Return Arreglo
        Catch ex As Exception
            MessageBox.Show("Error 202. " & Err.Description, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function


End Class
