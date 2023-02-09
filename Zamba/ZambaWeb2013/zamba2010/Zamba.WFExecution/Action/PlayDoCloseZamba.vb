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
        Dim ucm As New Ucm
        ucm.RemoveConnection()
  
        ActionsBusiness.CleanExceptions()
        
        Application.Exit()

    End Function

End Class
