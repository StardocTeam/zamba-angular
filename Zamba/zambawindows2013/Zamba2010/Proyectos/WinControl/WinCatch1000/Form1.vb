Imports SystemApi
Imports System.IO


Public Class Form1

    Private _Estado As Int32 = 0
    Private winHandle As IntPtr = System.IntPtr.Zero
    Private oldParent As IntPtr = System.IntPtr.Zero
    Dim proc As System.Diagnostics.Process = Nothing
    Dim timOut As Int32 = 0
    Dim longVal As IntPtr = System.IntPtr.Zero
    Dim longCount As Int32 = 0
    Dim itemId As Int32 = 0
    Dim flagCatch As Boolean = False

    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim x As IntPtr

        
        If winHandle <> System.IntPtr.Zero Then

            WindowsApi.Usr32Api.SetParent(winHandle, oldParent)
            lblApiSMCW.Text = WindowsApi.Usr32Api.SendMessage(winHandle, WindowsApi.Usr32Api.WM_CLOSE, 0, 0).ToString()

            Me.txtHwnd.Text = ""
            Me.txtTitulo.Text = ""

            Me.winHandle = System.IntPtr.Zero
            Me.Panel1.BackColor = Color.Red
            e.Cancel = True

        End If


    End Sub



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    Private Sub TmrProc_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TmrProc.Tick
        Dim x As IntPtr
        Dim strName As String = txtTitulo.Text
        Dim strClassName As String = txtClassName.Text
        Try
            If flagCatch = False Then
                If chkUseProc.Checked Then

                    winHandle = proc.MainWindowHandle

                Else


                    If txtPath.Text = String.Empty Then
                        Dim bodyType As List(Of String) = New List(Of String)
                        bodyType.Add(" - Message (HTML) ")
                        bodyType.Add(" - Message (Plain Text) ")
                        bodyType.Add(" - Message (Rich Text) ")
                        bodyType.Add(" - Mensaje (HTML) ")
                        bodyType.Add(" - Mensaje (Texto sin formato) ")
                        bodyType.Add(" - Mensaje (Texto enriquecido) ")

                        For Each bType As String In bodyType
                            winHandle = WindowsApi.Usr32Api.FindWindow(strClassName, strName & bType)
                            If winHandle <> IntPtr.Zero Then
                                Exit For
                            End If
                        Next

                    Else

                        winHandle = proc.MainWindowHandle

                    End If


                End If

            Else

                winHandle = Convert.ToInt32(txtHwnd.Text)

            End If


            'If proc.MainWindowHandle <> System.IntPtr.Zero And winHandle = System.IntPtr.Zero Then
            '    winHandle = proc.MainWindowHandle
            'End If


            If winHandle <> IntPtr.Zero Then
                flagCatch = False

                txtHwnd.Text = Convert.ToString(winHandle)
                lblParent.Text = Convert.ToString(WindowsApi.Usr32Api.GetParent(winHandle))

                lblPanelParent.Text = Convert.ToString(Me.Panel1.Handle)


                x = WindowsApi.Usr32Api.SetParent(winHandle, Me.Panel1.Handle)
                MsgPanelSetSize()
                longVal = WindowsApi.Usr32Api.GetSystemMenu(winHandle, 0)
                longCount = WindowsApi.Usr32Api.GetMenuItemCount(longVal)
                itemId = WindowsApi.Usr32Api.GetMenuItemID(longVal, longCount - 1)

                'WindowsApi.Usr32Api.DeleteMenu(longVal,longCount - 1 , 1024)


                TmrProc.Enabled = False
            Else
                If timOut >= 300 Then
                    TmrProc.Enabled = False
                Else
                    timOut = timOut + 1
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub MsgPanelSetSize()
        Try
            Dim ret As IntPtr
            Dim width As Int32 = Me.Panel1.Width
            Dim height As Int32 = Me.Panel1.Height


            ret = WindowsApi.Usr32Api.SetWindowPos(winHandle, 1, 0, 0, width, height, 0)
            ret = WindowsApi.Usr32Api.ShowWindow(winHandle, WindowsApi.Usr32Api.SW_MAXIMIZE)

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub btnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
        Dim x As IntPtr

        lblApiCW.Text = ""
        lblApiDesWin.Text = ""
        lblApiSMCP.Text = ""
        lblApiSMCW.Text = ""
        lblCloseProc.Text = ""
        Try
            proc = New System.Diagnostics.Process()
            proc.StartInfo.UseShellExecute = True

            If txtPath.Text.Trim <> String.Empty Then
                proc.StartInfo.FileName = txtPath.Text
                'proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                proc.Start()
            Else
                If System.Diagnostics.Process.GetProcessesByName("RE Insert.msg").Count = 0 Then
                    proc.StartInfo.FileName = Windows.Forms.Application.StartupPath & "\RE Insert.msg"
                    txtTitulo.Text = "RE: Insert"
                    txtClassName.Text = WindowsApi.ClassNames.rctrl_renwnd32
                    'proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    proc.Start()
                End If
            End If

            


            'If proc.MainWindowHandle <> System.IntPtr.Zero Then
            '    winHandle = proc.MainWindowHandle
            'End If


            TmrProc.Enabled = True

        Catch ex As Exception
            Throw ex
        End Try



    End Sub

    Private Sub btnKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKill.Click
        Try
            If Not proc.HasExited Then
                WindowsApi.Usr32Api.SetParent(winHandle, oldParent)
                lblCloseProc.Text = proc.CloseMainWindow().ToString()
                proc.Close()
            End If


        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub btnCloseWin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseWin.Click
        Try
            If winHandle <> System.IntPtr.Zero Then
                lblApiCW.Text = WindowsApi.Usr32Api.CloseWindow(winHandle).ToString()
            End If
        Catch ex As Exception
            Throw ex
        End Try



    End Sub

    Private Sub btnSMCloseWin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMCloseWin.Click
        Try

            If winHandle <> System.IntPtr.Zero Then

                WindowsApi.Usr32Api.SetParent(winHandle, oldParent)
                lblApiSMCW.Text = WindowsApi.Usr32Api.SendMessage(winHandle, WindowsApi.Usr32Api.WM_CLOSE, 0, 0).ToString()

            End If
        Catch ex As Exception
            Throw ex
        End Try




    End Sub

    Private Sub btnSMSysCWin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSysCWin.Click
        Try
            If winHandle <> System.IntPtr.Zero Then

                WindowsApi.Usr32Api.SetParent(winHandle, oldParent)
                lblApiSMCP.Text = WindowsApi.Usr32Api.SendMessage(winHandle, WindowsApi.Usr32Api.WM_SYSCOMMAND, WindowsApi.Usr32Api.SC_CLOSE, 0).ToString()

            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub btnDestroyWin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDestroyWin.Click
        Try
            If winHandle <> System.IntPtr.Zero Then
                WindowsApi.Usr32Api.SetParent(winHandle, oldParent)
                lblApiDesWin.Text = WindowsApi.Usr32Api.DestroyWindow(winHandle).ToString()

            End If
        Catch ex As Exception
            Throw ex
        End Try
        
    End Sub

   

    Private Sub btnGacTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGacTest.Click
        lblReturnGacTest.Text = GacTest.GetHelloWorld()

    End Sub

    Private Sub btnCatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatch.Click
        flagCatch = True
        TmrProc.Enabled = True

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Not proc.HasExited Then
                WindowsApi.Usr32Api.SetParent(winHandle, oldParent)
                proc.Kill()
            End If

        Catch ex As Exception
            Throw ex
        End Try


    End Sub
End Class
