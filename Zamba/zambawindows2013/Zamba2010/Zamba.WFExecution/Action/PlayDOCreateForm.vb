Imports Zamba.Core.WF.WF

Public Class PlayDOCreateForm

    ''' <summary>
    ''' Play de la regla PlayDOCreateForm
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	18/07/2008	Modified       La propiedad HashTable actua como variable interregla
    ''' </history>
    ''' 
    Private myRule As IDOCreateForm
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)


        Dim list As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim arr As New ArrayList
        list = results

        Try
            For Each r As Core.ITaskResult In results
                Dim newresult As Core.Result

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando formulario...")
                newresult = CreateForm(r, myRule.DocTypeIdVirtual)
                ZTrace.WriteLineIf(ZTrace.IsInfo, " OK")

                If myRule.AddToWf = True Then
                    Dim TR As New Core.TempTaskResult(newresult, r.AsignedToId, r.State, r.TaskState)
                    arr.Add(TR)
                End If

                ' Si hay algún dato en la propiedad HashTable (variable doc_id)
                'If (myRule.HashTable <> "") Then
                If (Not String.IsNullOrEmpty(myRule.HashTable)) Then
                    If (VariablesInterReglas.ContainsKey(myRule.HashTable) = False) Then
                        ' El id del result se guarda en la variable interregla
                        VariablesInterReglas.Add(myRule.HashTable, newresult.ID, False)
                    Else
                        VariablesInterReglas.Item(myRule.HashTable) = newresult.ID
                    End If
                End If
            Next

            If myRule.AddToWf = True Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo el Workflow...")
                Dim wfid As Int64 = WFBusiness.GetWorkflowIdByStepId(myRule.WFStepId)
                Dim wf As IWorkFlow = WFBusiness.GetWFbyId(wfid)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo la etapa...")
                Dim NewStep As WFStep = WFStepBusiness.GetStepById(myRule.WFStepId)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando los formularios creados al Workflow " & wf.Name & "...")
                WFTaskBusiness.AddResultsToWorkFLow(arr, wf, NewStep)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Formularios agregados con éxito!")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Los formularios creados no serán agregados al Workflow.")
            End If
        Finally

        End Try

        Return list

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Crea y completa un documento virtual
    ''' </summary>
    ''' <param name="p_ResultOrigen">Documento actual</param>
    ''' <param name="p_iDocTypeIdVirtual">Entidad virtual</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	09/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function CreateForm(ByVal p_ResultOrigen As Core.Result, ByVal p_iDocTypeIdVirtual As Int32) As Core.Result
        Dim i As Int32 = 0
        Dim j As Int32 = 0
        Dim nr As New Core.NewResult

        nr.OriginalName = p_ResultOrigen.OriginalName
        nr.AutoName = p_ResultOrigen.AutoName
        nr.Disk_Group_Id = p_ResultOrigen.Disk_Group_Id
        nr.DISK_VOL_PATH = p_ResultOrigen.DISK_VOL_PATH


        nr.IconId = p_ResultOrigen.IconId
        nr.Doc_File = p_ResultOrigen.Doc_File
        nr.Name = p_ResultOrigen.Name
        nr.Object_Type_Id = p_ResultOrigen.Object_Type_Id
        nr.OffSet = p_ResultOrigen.OffSet
        nr.Platter_Id = p_ResultOrigen.Platter_Id
        nr.Thumbnails = p_ResultOrigen.Thumbnails

        nr.DocType = DocTypesBusiness.GetDocType(p_iDocTypeIdVirtual, True)
        nr.Indexs = Zamba.Core.ZCore.FilterIndex(p_iDocTypeIdVirtual)

        For i = 0 To nr.Indexs.Count - 1
            For j = 0 To p_ResultOrigen.Indexs.Count - 1
                If (nr.Indexs(i).id = p_ResultOrigen.Indexs(j).id) Then
                    nr.Indexs(i).data = p_ResultOrigen.Indexs(j).data
                    nr.Indexs(i).datatemp = p_ResultOrigen.Indexs(j).data
                    Exit For
                End If
            Next
        Next

        nr.ID = 0

        Zamba.Core.Results_Business.InsertDocument(nr, False, False, False, False, True)
        Return nr
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDOCreateForm)
        myRule = rule
    End Sub
End Class
