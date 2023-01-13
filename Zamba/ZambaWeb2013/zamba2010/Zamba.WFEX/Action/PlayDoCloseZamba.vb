Imports Zamba.Core
Imports Zamba.Data
Imports System.IO
Imports Zamba.Tools
Imports Zamba.Core.WF.WF
Imports System.Windows.Forms

Public Class PlayDoCloseZamba

    Private _myrule As IDoCloseZamba

    ''' <summary>
    ''' Constructor de la ejecución de la regla PlayDoCloseZamba
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <history>
    '''     Javier 07/01/2011   Created
    ''' </history>
    Public Sub New(ByVal rule As IDoCloseZamba)
        Me._myrule = rule
    End Sub

    ''' <summary>
    ''' Regla que cierra Zamba
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier 07/01/2011   Created
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Trace.WriteLineIf(ZTrace.IsInfo, "Ejecutando regla de cierre de Zamba")

        Trace.WriteLineIf(ZTrace.IsInfo, "Quitando conexiones")
        Ucm.RemoveConnection()

        Trace.WriteLineIf(ZTrace.IsInfo, "Limpiando exceptions")
        Users.Actions.CleanExceptions()

        Trace.WriteLineIf(ZTrace.IsInfo, "Cerrando la aplicacion")
        Application.Exit()
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
