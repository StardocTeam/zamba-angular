Namespace WindowsApi

    Public Class Usr32Api

#Region "Constants"

        Public Const SM_CXSCREEN As Int32 = 0
        Public Const SM_CYSCREEN As Int32 = 1
        Public Const WM_SYSCOMMAND As Int32 = &H112
        Public Const SC_CLOSE As Int32 = &HF060I
        Public Const SW_MAXIMIZE As Int32 = 3
        Public Const SW_MINIMIZE As Int32 = 6
        Public Const SW_NORMAL As Int32 = 1
        Public Const SW_HIDE As Int32 = 0
        Public Const WM_CLOSE As Int32 = &H10
        Public Const WM_COMMAND As Int32 = &H111
        Public Const WM_RBUTTONDBLCLK As Int32 = &H206




#End Region

#Region "Functions"

        Public Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

        Public Declare Function SetParent Lib "user32.dll" Alias "SetParent" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr

        Public Declare Function GetParent Lib "user32.dll" Alias "GetParent" (ByVal hwnd As IntPtr) As IntPtr

        Public Declare Function SetWindowPos Lib "user32.dll" Alias "SetWindowPos" (ByVal hwnd As IntPtr, ByVal hWndInsertAfter As Int32, ByVal x As Int32, ByVal y As Int32, ByVal cx As Int32, ByVal cy As Int32, ByVal wFlags As Int32) As IntPtr

        Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As IntPtr

        Public Declare Function ShowWindow Lib "user32.dll" Alias "ShowWindow" (ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As IntPtr

        Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As IntPtr, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Int32) As IntPtr

        Public Declare Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpdwProcessId As Int32) As IntPtr

        Public Declare Function GetSystemMenu Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal bRevert As Int32) As IntPtr

        Public Declare Function DeleteMenu Lib "user32.dll" (ByVal hMenu As IntPtr, ByVal nPosition As Int32, ByVal wFlags As Int32) As IntPtr

        Public Declare Function GetMenuItemCount Lib "user32.dll" (ByVal hMenu As IntPtr) As Int32

        Public Declare Function GetMenu Lib "user32.dll" (ByVal hwnd As IntPtr) As IntPtr

        Public Declare Function GetMenuItemID Lib "user32.dll" (ByVal hMenu As IntPtr, ByVal nPos As Int32) As Int32

        Public Declare Function CloseWindow Lib "user32.dll" (ByVal hwnd As IntPtr) As Int32

        Public Declare Function DestroyWindow Lib "user32.dll" (ByVal hwnd As IntPtr) As Int32


#End Region

    End Class

    Public Class ClassNames

        Public Const rctrl_renwnd32 As String = "rctrl_renwnd32"


    End Class


End Namespace
