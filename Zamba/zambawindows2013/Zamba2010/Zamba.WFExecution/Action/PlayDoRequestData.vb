Imports System.Windows.Forms
Imports Zamba.Viewers
Imports Zamba.Core.WF.WF

Public Class PlayDoRequestData

    Private _myRule As IDoRequestData

    Sub New(ByVal rule As IDoRequestData)
        _myRule = rule

    End Sub

    ''' <summary>
    ''' play de la regla
    ''' </summary>
    ''' <history>
    ''' 
    ''' </history>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim frm As Form = Nothing
        Dim ucIndexs As UCIndexViewer = Nothing
        Dim dlgResult As DialogResult
        Dim resultado As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            If results IsNot Nothing AndAlso results.Count > 0 Then
                frm = New Form
                frm.Size = New System.Drawing.Point(800, 600)
                frm.ShowIcon = False
                frm.Text = "Atributos"
                frm.AutoScroll = True
                frm.ShowInTaskbar = True
                frm.StartPosition = FormStartPosition.CenterScreen
                frm.MaximizeBox = False
                frm.ControlBox = False
            End If

            For Each res As TaskResult In results
                ucIndexs = New UCIndexViewer(False)
                ucIndexs.WithDialogPanel = True
                ucIndexs.Dock = DockStyle.Fill
                frm.Controls.Add(ucIndexs)
                ucIndexs.AsignButtonsToParentForm()

                If res.DocType.IsReindex Then
                    ucIndexs.ShowEspecifiedIndexs(DirectCast(res, Result).ID, DirectCast(res, Result).DocTypeId, _myRule.ArrayIds, False)
                    dlgResult = frm.ShowDialog()
                Else
                    res.DocType.IsReindex = True
                    ucIndexs.ShowEspecifiedIndexs(DirectCast(res, Result).ID, DirectCast(res, Result).DocTypeId, _myRule.ArrayIds, True)
                    dlgResult = frm.ShowDialog()
                    res.DocType.IsReindex = False
                End If

                resultado.Add(WFTaskBusiness.GetTaskByDocIdAndStepIdAAndDocTypeId(res.ID, res.StepId, res.DocTypeId, 0))

                ucIndexs.Dispose()
                frm.Controls.Clear()

                If dlgResult = DialogResult.Cancel Then
                    If _myRule.ThrowExceptionIfCancel Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                        Throw New Exception("El usuario cancelo la ejecucion de la regla")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "El usuario ha cancelado")
                        Return Nothing
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "El usuario ha aceptado")
                End If
            Next

        Finally
            ucIndexs = Nothing
            If frm IsNot Nothing Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try

        Return resultado
    End Function

    Public Function PlayTest() As Boolean

    End Function

    Function DiscoverParams() As List(Of String)

    End Function

End Class
