Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.servers
Imports System.Windows.Forms

Public Class PlayDoExecuteExplorer
    ''' <summary>
    ''' Abre un WebBrowser
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Pablo] 19-10-2010 Created 
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoExecuteExplorer) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
End Class

