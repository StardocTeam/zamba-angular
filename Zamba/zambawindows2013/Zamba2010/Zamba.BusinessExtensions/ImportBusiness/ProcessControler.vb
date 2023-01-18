Imports System.Threading
Public Class ProcessControler
    Inherits ZClass
    Public Shared Sub CatchError(ByVal ex As Exception)
        zamba.core.zclass.raiseerror(ex)
    End Sub
    Public Overrides Sub Dispose()

    End Sub
    Public Process As Process

    Public Sub ExecuteProcess(ByVal Process As Process)
        Try
            Me.Process = Process
            Dim T As New Thread(AddressOf StartProcess)
            T.Start()
            Thread.Sleep(30000)
        Catch ex As Threading.SynchronizationLockException
            zamba.core.zclass.raiseerror(ex)
        Catch ex As Threading.ThreadAbortException
            zamba.core.zclass.raiseerror(ex)
        Catch ex As Threading.ThreadInterruptedException
            zamba.core.zclass.raiseerror(ex)
        Catch ex As Threading.ThreadStateException
            zamba.core.zclass.raiseerror(ex)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub StartProcess()
        Dim PE As New ProcessEngine
        PE.InitialProcess(Process)
    End Sub




End Class
