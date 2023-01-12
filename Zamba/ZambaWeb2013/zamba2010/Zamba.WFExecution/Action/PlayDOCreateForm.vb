Imports Zamba.Core.WF.WF
Imports Zamba.Data

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
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOCreateForm) As System.Collections.Generic.List(Of Core.ITaskResult)


        Dim list As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim arr As New ArrayList
        list = results

        Dim WFTB As New WFTaskBusiness()
        Dim WFB As New WFBusiness
        Dim WFSB As New WFStepBusiness

        Try
            For Each r As Core.ITaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name & ", Id " & r.TaskId)
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
                        VariablesInterReglas.Add(myRule.HashTable, newresult.ID)
                    Else
                        VariablesInterReglas.Item(myRule.HashTable) = newresult.ID
                    End If
                End If
            Next

            If myRule.AddToWf = True Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo el Workflow...")
                Dim wfid As Int64 = WFB.GetWorkflowIdByStepId(myRule.WFStepId)
                Dim wf As IWorkFlow = WFB.GetWFbyId(wfid)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo la etapa...")
                Dim NewStep As WFStep = WFSB.GetStepById(myRule.WFStepId)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando los formularios creados al Workflow " & wf.Name & "...")
                WFTB.AddResultsToWorkFLow(arr, wf, True, True, Membership.MembershipHelper.CurrentUser.ID, False)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Formularios agregados con éxito!")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Los formularios creados no serán agregados al Workflow.")
            End If
        Finally
            WFB = Nothing
            WFTB = Nothing
            WFSB = Nothing
        End Try

        Return list

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Crea y completa un documento virtual
    ''' </summary>
    ''' <param name="p_ResultOrigen">Documento actual</param>
    ''' <param name="p_iDocTypeIdVirtual">Tipo de documento virtual</param>
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
        Dim DTB As New DocTypesBusiness()

        nr.DocType = DTB.GetDocType(p_iDocTypeIdVirtual)
        DTB = Nothing
        nr.Indexs = ZCore.GetInstance().FilterIndex(p_iDocTypeIdVirtual)

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
        Dim ResultBusiness As New Results_Business
        ResultBusiness.Insert(nr, False, False, False, False, True)
        ResultBusiness = Nothing
        Return nr
    End Function
End Class
