Imports System.Collections.Generic

Namespace cache

    '[Ezequiel] 01/05/2011 - Clase en la cual se guardan las opciones de reglas.
    Public Class RulesOptions

        ''' <summary>
        ''' El constructor es privado
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub New()

        End Sub

        Public Shared _DsRulesOptionsByRuleId As New SynchronizedHashtable


        Public Shared Sub ClearAll()
            If _DsRulesOptionsByRuleId IsNot Nothing Then
                _DsRulesOptionsByRuleId.Clear()
            Else
                _DsRulesOptionsByRuleId = New SynchronizedHashtable()
            End If
        End Sub
    End Class
End Namespace