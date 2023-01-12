Namespace WindowsApi

    Public Class Usr32Api

#Region "Constants"

        Public Const GW_HWNDNEXT As Int32 = 2
        Public Const GWL_STYLE As Int32 = -16
        Public Const MF_BYCOMMAND As Int32 = &H0I
        Public Const MF_BYPOSITION As Int32 = &H400I
        Public Const MF_GRAYED As Int32 = &H1
        Public Const MF_REMOVE As Int32 = &H1000
        Public Const MF_DISABLED As Int32 = &H2
        Public Const SC_CLOSE As Int32 = &HF060I
        Public Const SC_MAXIMIZE As Int32 = &HF030
        Public Const SC_MINIMIZE As Int32 = &HF020
        Public Const SC_MOVE As Int32 = &HF010
        Public Const SC_NEXTWINDOW As Int32 = &HF040
        Public Const SC_RESTORE As Int32 = &HF120
        Public Const SC_PREVWINDOW As Int32 = &HF050
        Public Const SC_SIZE As Int32 = &HF000
        Public Const SC_DEFAULT As Int32 = &HF160
        Public Const SC_MOUSEMENU As Int32 = &HF090
        Public Const SM_CXSCREEN As Int32 = 0
        Public Const SM_CYSCREEN As Int32 = 1
        Public Const SW_MAXIMIZE As Int32 = 3
        Public Const SW_MINIMIZE As Int32 = 6
        Public Const SW_NORMAL As Int32 = 1
        Public Const SW_HIDE As Int32 = 0
        Public Const WM_CLOSE As Int32 = &H10
        Public Const WM_COMMAND As Int32 = &H111
        Public Const WM_RBUTTONDBLCLK As Int32 = &H206
        Public Const WM_SYSCOMMAND As Int32 = &H112
        Public Const WS_CHILD As Int32 = &H40000000
        Public Const WS_CHILDWINDOW As Int32 = (WS_CHILD)
        Public Const WS_CAPTION As Int32 = &HC00000
        Public Const WS_MAXIMIZE As Int32 = &H1000000
        Public Const WS_SYSMENU As Int32 = &H80000
        Public Const WS_MAXIMIZEBOX As Int32 = &H10000
        Public Const WS_MINIMIZEBOX As Int32 = &H20000

#End Region

#Region "Functions"

        Public Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
        Public Declare Function SetParent Lib "user32.dll" Alias "SetParent" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
        Public Declare Function GetParent Lib "user32.dll" Alias "GetParent" (ByVal hwnd As IntPtr) As IntPtr
        Public Declare Function SetWindowPos Lib "user32.dll" Alias "SetWindowPos" (ByVal hwnd As IntPtr, ByVal hWndInsertAfter As Int32, ByVal x As Int32, ByVal y As Int32, ByVal cx As Int32, ByVal cy As Int32, ByVal wFlags As Int32) As IntPtr
        Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As IntPtr
        Public Declare Function ShowWindow Lib "user32.dll" Alias "ShowWindow" (ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As IntPtr
        Public Declare Function SetActiveWindow Lib "user32.dll" (ByVal hwnd As IntPtr) As Int32
        Public Declare Function EnableMenuItem Lib "user32.dll" Alias "EnableMenuItem" (ByVal hMenu As IntPtr, ByVal uIDEnableItem As UInt32, ByVal uEnable As UInt32) As Boolean
        Public Declare Function GetSystemMenu Lib "user32.dll" Alias "GetSystemMenu" (ByVal hWnd As IntPtr, ByVal bRevert As Boolean) As IntPtr
        Public Declare Function DrawMenuBar Lib "user32.dll" Alias "DrawMenuBar" (ByVal hwnd As IntPtr) As Boolean
        Public Declare Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpdwProcessId As IntPtr) As IntPtr
        Public Declare Function GetSystemMenu Lib "user32.dll" Alias "GetSystemMenu" (ByVal hwnd As IntPtr, ByVal bRevert As Int32) As IntPtr
        Public Declare Function DeleteMenu Lib "user32.dll" (ByVal hMenu As IntPtr, ByVal nPosition As Int32, ByVal wFlags As Int32) As IntPtr
        Public Declare Function GetMenuItemCount Lib "user32.dll" (ByVal hMenu As IntPtr) As Int32
        Public Declare Function GetMenu Lib "user32.dll" (ByVal hwnd As IntPtr) As IntPtr
        Public Declare Function GetMenuItemID Lib "user32.dll" (ByVal hMenu As IntPtr, ByVal nPos As Int32) As Int32
        Public Declare Function GetWindowLong Lib "user32.dll" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Int32) As Int32
        Public Declare Function SetWindowLong Lib "user32.dll" Alias "SetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Int32, ByVal dwNewLong As Int32) As Int32
        Public Declare Function GetWindow Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal wCmd As Int32) As IntPtr
        Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As IntPtr, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Int32) As IntPtr
        Public Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAccess As Int32, ByVal bInheritHandle As IntPtr, ByVal dwProcessId As IntPtr) As Int32
        Public Declare Function CloseHandle Lib "kernel32.dll" (ByVal hObject As IntPtr) As Int32
        Public Declare Function GetModuleFileNameEx Lib "psapi.dll" Alias "GetModuleFileNameExA" (ByVal hProcess As IntPtr, ByVal hModule As IntPtr, ByVal lpFilename As String, ByVal nSize As Int32) As Int32

#End Region

    End Class

    Public Class ClassNames

        Public Const rctrl_renwnd32 As String = "rctrl_renwnd32"
        Public Const OpusApp As String = "OpusApp"

    End Class

    'Partial Public Class NativeMethods
    '    ''' Return Type: HMENU->HMENU__*
    '    '''hWnd: HWND->HWND__*
    '    '''bRevert: BOOL->int
    '    Public Declare Function GetSystemMenu Lib "user32.dll" Alias "GetSystemMenu" _
    '        (ByVal hWnd As IntPtr, _
    '         ByVal bRevert As Boolean) _
    '         As IntPtr

    '    ''' Return Type: BOOL->int
    '    '''hMnu: HMENU->HMENU__*
    '    '''uPosition: UINT->unsigned int
    '    '''uFlags: UINT->unsigned int
    '    '''uIDNewItem: UINT_PTR->unsigned int
    '    '''lpNewItem: LPCSTR->CHAR*
    '    Public Declare Function ModifyMenuA Lib "user32.dll" Alias "ModifyMenuA" _
    '        (ByVal hWnd As IntPtr, _
    '         ByVal uPosition As UInt32, _
    '         ByVal uFlags As UInt32, _
    '         ByVal uIDNewItem As UIntPtr, _
    '         ByVal lpNewItem As String) _
    '         As Boolean

    '    ''' Return Type: BOOL->int
    '    '''hWnd: HWND->HWND__*
    '    Public Declare Function DrawMenuBar Lib "user32.dll" Alias "DrawMenuBar" _
    '        (ByVal hWnd As IntPtr) As Boolean
    'End Class
    'Partial Public Class NativeConstants
    '    ''' SC_CLOSE -> 0xF060
    '    Public Const SC_CLOSE As UInt32 = 61536

    '    ''' SC_MAXIMIZE -> 0xF030
    '    Public Const SC_MAXIMIZE As UInt32 = 61488

    '    ''' SC_MINIMIZE -> 0xF020
    '    Public Const SC_MINIMIZE As UInt32 = 61472

    '    ''' MF_DISABLED -> 0x00000002L
    '    Public Const MF_DISABLED As UInt32 = 2
    'End Class

    'Private Sub SetMenuItem(ByVal winHandle As IntPtr, ByVal vEI As UInt32, ByVal vMF As UInt32, ByVal newItemID As UIntPtr, ByVal lPNewItem As String)
    '    Dim lHwnd As IntPtr = NativeMethods.GetSystemMenu(winHandle, False)
    '    Dim lResult As Boolean = NativeMethods.ModifyMenuA(lHwnd, vEI, vMF, newItemID, lPNewItem)
    '    NativeMethods.DrawMenuBar(winHandle)
    'End Sub


End Namespace
