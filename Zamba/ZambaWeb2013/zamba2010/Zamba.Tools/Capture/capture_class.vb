
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace Capture
    Partial Public Class capture_class
#Region "Exported WIN APIs"
        <DllImport("GDI32.dll")>
        Public Shared Function BitBlt(hdcDest As Integer, nXDest As Integer, nYDest As Integer, nWidth As Integer, nHeight As Integer, hdcSrc As Integer,
            nXSrc As Integer, nYSrc As Integer, dwRop As Integer) As Boolean

        End Function

        <DllImport("GDI32.dll")>
        Public Shared Function CreateCompatibleBitmap(hdc As Integer, nWidth As Integer, nHeight As Integer) As Integer

        End Function
        <DllImport("GDI32.dll")>
        Public Shared Function CreateCompatibleDC(hdc As Integer) As Integer

        End Function

        <DllImport("GDI32.dll")>
        Public Shared Function DeleteDC(hdc As Integer) As Boolean

        End Function

        <DllImport("GDI32.dll")>
        Public Shared Function DeleteObject(hObject As Integer) As Boolean

        End Function


        <DllImport("gdi32.dll")>
        Private Shared Function CreateDC(lpszDriver As String, lpszDevice As String, lpszOutput As String, lpInitData As IntPtr) As Integer

        End Function

        <DllImport("GDI32.dll")>
        Public Shared Function GetDeviceCaps(hdc As Integer, nIndex As Integer) As Integer

        End Function

        <DllImport("GDI32.dll")>
        Public Shared Function SelectObject(hdc As Integer, hgdiobj As Integer) As Integer

        End Function

        <DllImport("User32.dll")>
        Public Shared Function GetDesktopWindow() As Integer

        End Function

        <DllImport("User32.dll")>
        Public Shared Function GetWindowDC(hWnd As Integer) As Integer

        End Function

        <DllImport("User32.dll")>
        Public Shared Function ReleaseDC(hWnd As Integer, hDC As Integer) As Integer

        End Function
#End Region

        'function to capture screen section       
        Public Shared Function CaptureScreen(ByVal FilePath As String) As Image

            Dim noofscreens As Integer = 0
            Dim screens As Screen()

            screens = Screen.AllScreens
            noofscreens = screens.Length

            
            Dim maxwidth As Integer = 0, maxheight As Integer = 0
            For i As Integer = 0 To noofscreens - 1
                If maxwidth < (screens(i).Bounds.X + screens(i).Bounds.Width) Then
                    maxwidth = screens(i).Bounds.X + screens(i).Bounds.Width
                End If
                If maxheight < (screens(i).Bounds.Y + screens(i).Bounds.Height) Then
                    maxheight = screens(i).Bounds.Y + screens(i).Bounds.Height
                End If
            Next

            'create DC for the entire virtual screen
            Dim hdcSrc As Integer = CreateDC("DISPLAY", Nothing, Nothing, IntPtr.Zero)

            Dim hdcDest As Integer = CreateCompatibleDC(hdcSrc)

            Dim hBitmap As Integer = CreateCompatibleBitmap(hdcSrc, maxwidth, maxheight)

            SelectObject(hdcDest, hBitmap)

            ' set the destination area White - a little complicated
            Dim bmp As New Bitmap(maxwidth, maxheight)

            Dim ii As Image = DirectCast(bmp, Image)

            Dim gf As Graphics = Graphics.FromImage(ii)

            Dim hdc As IntPtr = gf.GetHdc()

            BitBlt(hdcDest, 0, 0, maxwidth, maxheight, CInt(hdc),
                0, 0, &HFF0062)
            'use whiteness flag to make destination screen white
            gf.Dispose()
            ii.Dispose()
            bmp.Dispose()

            'Now copy the areas from each screen on the destination hbitmap
            Dim screendata As Screen() = Screen.AllScreens
            Dim X__3 As Integer, X1 As Integer, Y__4 As Integer, Y1 As Integer
            For i As Integer = 0 To screendata.Length - 1
                ' no common area
                If screendata(i).Bounds.X > (0 + maxwidth) OrElse (screendata(i).Bounds.X + screendata(i).Bounds.Width) < 0 OrElse screendata(i).Bounds.Y > (0 + maxheight) OrElse (screendata(i).Bounds.Y + screendata(i).Bounds.Height) < 0 Then

                Else
                    ' something  common
                    If 0 < screendata(i).Bounds.X Then

                        X__3 = screendata(i).Bounds.X
                    Else

                        X__3 = 0
                    End If
                    If (0 + maxwidth) > (screendata(i).Bounds.X + screendata(i).Bounds.Width) Then
                        X1 = screendata(i).Bounds.X + screendata(i).Bounds.Width

                    Else
                        X1 = 0 + maxwidth

                    End If

                    If 0 < screendata(i).Bounds.Y Then

                        Y__4 = screendata(i).Bounds.Y
                    Else

                        Y__4 = 0
                    End If
                    If (0 + maxheight) > (screendata(i).Bounds.Y + screendata(i).Bounds.Height) Then
                        Y1 = screendata(i).Bounds.Y + screendata(i).Bounds.Height

                    Else
                        Y1 = 0 + maxheight

                    End If
                    ' Main API that does memory data transfer
                    'SRCCOPY AND CAPTUREBLT

                    BitBlt(hdcDest, X__3 - 0, Y__4 - 0, X1 - X__3, Y1 - Y__4, hdcSrc,
                        X__3, Y__4, &H40000000 Or &HCC0020)

                End If

            Next

            Dim imf As Image = Image.FromHbitmap(New IntPtr(hBitmap))
            imf.Save(FilePath)


            DeleteDC(hdcSrc)

            DeleteDC(hdcDest)

            DeleteObject(hBitmap)

        End Function





    End Class
End Namespace
