Imports ZAMBA.Core

Public Class PreLoadComponent

#Region "PreLoadComponents"
    Private PreLoad As ZAMBA.PreLoad.PreLoadEngine
    Public Sub PreLoadComponents()
        Try                     
                PreLoad = New Zamba.PreLoad.PreLoadEngine()
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Llamando al PreLoad")

              If UserPreferences.getValue("UpdateFormsOnZambaLoad", Sections.FormPreferences, "False") Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando formularios")
                    PreLoad.PreLoadObjects
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Formularios actualizados")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Actulización de Formularios deshabilitada")
                End If

                PreLoad.PreLoadObjects()             
           
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region
End Class
