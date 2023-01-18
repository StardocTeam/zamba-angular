Public Class PlayDoAttachToDocument

#Region "Atributos"

    Private _myrule As IDoAttachToDocument
    Private _frmAttach As FrmAttachDocument

#End Region

#Region "Constructores"

    Sub New(ByVal rule As IDoAttachToDocument)
        _myrule = rule
        _frmAttach = New FrmAttachDocument(rule)
    End Sub

#End Region

#Region "Metodos Publicos"

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        If results.Count > 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de tareas a adjuntar documentos " & results.Count)
            For Each r As IResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Llamo al formulario para agregar archivos")
                _frmAttach.Attach(r)
            Next
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Llamo al formulario para agregar archivos")
            _frmAttach.Attach()
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Finaliza la ejecucion de la regla" & results.Count)
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

#End Region

End Class
