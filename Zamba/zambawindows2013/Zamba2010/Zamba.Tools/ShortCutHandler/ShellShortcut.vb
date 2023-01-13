Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms

Friend Class ShellShortcut
    Implements IDisposable

    Private Const INFOTIPSIZE As Integer = 1024
    Private Const MAX_PATH As Integer = 260

    Private Const SW_SHOWNORMAL As Integer = 1
    Private Const SW_SHOWMINIMIZED As Integer = 2
    Private Const SW_SHOWMAXIMIZED As Integer = 3
    Private Const SW_SHOWMINNOACTIVE As Integer = 7


#If [UNICODE] Then
    Private m_Link As IShellLinkW
#Else
    Private m_Link As IShellLinkA
#End If
    Private m_sPath As String

    '
    ' linkPath: Path to new or existing shortcut file (.lnk).
    '
    Public Sub New(ByVal linkPath As String)

        Dim pf As IPersistFile
        Dim CLSID_ShellLink As Guid = New Guid("00021401-0000-0000-C000-000000000046")
        Dim tShellLink As Type

        ' Workaround for VB.NET compiler bug with ComImport classes
        '#If [UNICODE] Then
        '      m_Link = CType(New ShellLink(), IShellLinkW)
        '#Else
        '      m_Link = CType(New ShellLink(), IShellLinkA)
        '#End If
        tShellLink = Type.GetTypeFromCLSID(CLSID_ShellLink)
#If [UNICODE] Then
      m_Link = CType(Activator.CreateInstance(tShellLink), IShellLinkW)
#Else
        m_Link = CType(Activator.CreateInstance(tShellLink), IShellLinkA)
#End If

        m_sPath = linkPath

        If File.Exists(linkPath) Then
            pf = CType(m_Link, IPersistFile)
            pf.Load(linkPath, 0)
        End If

    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If m_Link Is Nothing Then Exit Sub
        Marshal.ReleaseComObject(m_Link)
        m_Link = Nothing
    End Sub


    '
    ' Gets or sets the argument list of the shortcut.
    '
    Public Property Arguments() As String
        Get
            Dim sb As StringBuilder = New StringBuilder(INFOTIPSIZE)
            m_Link.GetArguments(sb, sb.Capacity)
            Return sb.ToString()
        End Get
        Set(ByVal Value As String)
            m_Link.SetArguments(Value)
        End Set
    End Property

    '
    ' Gets or sets a description of the shortcut.
    '
    Public Property Description() As String
        Get
            Dim sb As StringBuilder = New StringBuilder(INFOTIPSIZE)
            m_Link.GetDescription(sb, sb.Capacity)
            Return sb.ToString()
        End Get
        Set(ByVal Value As String)
            m_Link.SetDescription(Value)
        End Set
    End Property

    '
    ' Gets or sets the working directory (aka start in directory) of the shortcut.
    '
    Public Property WorkingDirectory() As String
        Get
            Dim sb As StringBuilder = New StringBuilder(MAX_PATH)
            m_Link.GetWorkingDirectory(sb, sb.Capacity)
            Return sb.ToString()
        End Get
        Set(ByVal Value As String)
            m_Link.SetWorkingDirectory(Value)
        End Set
    End Property

    '
    ' If Path returns an empty string, the shortcut is associated with
    ' a PIDL instead, which can be retrieved with IShellLink.GetIDList().
    ' This is beyond the scope of this wrapper class.
    '
    ' Gets or sets the target path of the shortcut.
    '
    Public Property Path() As String
        Get
#If [UNICODE] Then
        Dim wfd As WIN32_FIND_DATAW
#Else
            Dim wfd As WIN32_FIND_DATAA
#End If
            Dim sb As StringBuilder = New StringBuilder(MAX_PATH)
            m_Link.GetPath(sb, sb.Capacity, wfd, SLGP_FLAGS.SLGP_UNCPRIORITY)
            Return sb.ToString()
        End Get
        Set(ByVal Value As String)
            m_Link.SetPath(Value)
        End Set
    End Property

    '
    ' Gets or sets the path of the Icon assigned to the shortcut.
    '
    Public Property IconPath() As String
        Get
            Dim sb As StringBuilder = New StringBuilder(MAX_PATH)
            Dim nIconIdx As Integer
            m_Link.GetIconLocation(sb, sb.Capacity, nIconIdx)
            Return sb.ToString()
        End Get
        Set(ByVal Value As String)
            m_Link.SetIconLocation(Value, IconIndex)
        End Set
    End Property

    '
    ' Gets or sets the index of the Icon assigned to the shortcut.
    ' Set to zero when the IconPath property specifies a .ICO file.
    '
    Public Property IconIndex() As Integer
        Get
            Dim sb As StringBuilder = New StringBuilder(MAX_PATH)
            Dim nIconIdx As Integer
            m_Link.GetIconLocation(sb, sb.Capacity, nIconIdx)
            Return nIconIdx
        End Get
        Set(ByVal Value As Integer)
            m_Link.SetIconLocation(IconPath, Value)
        End Set
    End Property

    '
    ' Retrieves the Icon of the shortcut as it will appear in Explorer.
    ' Use the IconPath and IconIndex properties to change it.
    '
    Public ReadOnly Property Icon() As Icon
        Get
            Dim sb As StringBuilder = New StringBuilder(MAX_PATH)
            Dim nIconIdx As Integer
            Dim hIcon, hInst As IntPtr
            Dim ico, clone As Icon


            m_Link.GetIconLocation(sb, sb.Capacity, nIconIdx)

            hInst = Marshal.GetHINSTANCE([GetType]().Module)
            hIcon = Native.ExtractIcon(hInst, sb.ToString(), nIconIdx)
            If hIcon.ToInt32() = 0 Then Return Nothing

            ' Return a cloned Icon, because we have to free the original ourself.
            ico = Icon.FromHandle(hIcon)
            clone = CType(ico.Clone(), Icon)
            ico.Dispose()
            Native.DestroyIcon(hIcon)
            Return clone

        End Get
    End Property

    '
    ' Gets or sets the System.Diagnostics.ProcessWindowStyle value
    ' that decides the initial show state of the shortcut target. Note that
    ' ProcessWindowStyle.Hidden is not a valid property value.
    '
    Public Property WindowStyle() As ProcessWindowStyle
        Get
            Dim nWS As Integer
            m_Link.GetShowCmd(nWS)

            Select Case nWS
                Case SW_SHOWMINIMIZED, SW_SHOWMINNOACTIVE
                    Return ProcessWindowStyle.Minimized
                Case SW_SHOWMAXIMIZED
                    Return ProcessWindowStyle.Maximized
                Case Else
                    Return ProcessWindowStyle.Normal
            End Select
        End Get
        Set(ByVal Value As ProcessWindowStyle)
            Dim nWS As Integer

            Select Case Value
                Case ProcessWindowStyle.Normal
                    nWS = SW_SHOWNORMAL
                Case ProcessWindowStyle.Minimized
                    nWS = SW_SHOWMINNOACTIVE
                Case ProcessWindowStyle.Maximized
                    nWS = SW_SHOWMAXIMIZED
                Case Else ' ProcessWindowStyle.Hidden
                    Throw New ArgumentException("Unsupported ProcessWindowStyle value.")
            End Select
            m_Link.SetShowCmd(nWS)
        End Set
    End Property

    '
    ' Gets or sets the hotkey for the shortcut.
    '
    Public Property Hotkey() As Keys
        Get
            Dim wHotkey As Short
            Dim dwHotkey As Integer

            m_Link.GetHotkey(wHotkey)

            '
            ' Convert from IShellLink 16-bit format to Keys enumeration 32-bit value
            ' IShellLink: &HMMVK
            ' Keys:  &H00MM00VK        
            '   MM = Modifier (Alt, Control, Shift)
            '   VK = Virtual key code
            '   
            dwHotkey = (wHotkey And &HFF00I) * &H100I Or (wHotkey And &HFFI)
            Return CType(dwHotkey, Keys)

        End Get
        Set(ByVal Value As Keys)
            Dim wHotkey As Short

            If (Value And Keys.Modifiers) = 0 Then
                Throw New ArgumentException("Hotkey must include a modifier key.")
            End If

            '
            ' Convert from Keys enumeration 32-bit value to IShellLink 16-bit format
            ' IShellLink: &HMMVK
            ' Keys:  &H00MM00VK        
            '   MM = Modifier (Alt, Control, Shift)
            '   VK = Virtual key code
            '   
            wHotkey = CShort(CInt(Value And Keys.Modifiers) \ &H100) Or CShort(Value And Keys.KeyCode)
            m_Link.SetHotkey(wHotkey)

        End Set
    End Property

    '
    ' Saves the shortcut to disk.
    '
    Public Sub Save()
        Dim pf As IPersistFile = CType(m_Link, IPersistFile)
        pf.Save(m_sPath, True)
    End Sub

    '
    ' Returns a reference to the internal ShellLink object,
    ' which can be used to perform more advanced operations
    ' not supported by this wrapper class, by using the
    ' IShellLink interface directly.
    '
    Public ReadOnly Property ShellLink() As Object
        Get
            Return m_Link
        End Get
    End Property


#Region "Native Win32 API functions"
    Private Class Native

        <DllImport("shell32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Function ExtractIcon(ByVal hInst As IntPtr, ByVal lpszExeFileName As String, ByVal nIconIndex As Integer) As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function DestroyIcon(ByVal hIcon As IntPtr) As Boolean
        End Function

    End Class
#End Region

End Class

' IShellLink.Resolve fFlags
<Flags()> _
Public Enum SLR_FLAGS
    SLR_NO_UI = &H1
    SLR_ANY_MATCH = &H2
    SLR_UPDATE = &H4
    SLR_NOUPDATE = &H8
    SLR_NOSEARCH = &H10
    SLR_NOTRACK = &H20
    SLR_NOLINKINFO = &H40
    SLR_INVOKE_MSI = &H80
End Enum

' IShellLink.GetPath fFlags
<Flags()> _
Public Enum SLGP_FLAGS
    SLGP_SHORTPATH = &H1
    SLGP_UNCPRIORITY = &H2
    SLGP_RAWPATH = &H4
End Enum

<StructLayoutAttribute(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> Public Structure WIN32_FIND_DATAA
    Public dwFileAttributes As Integer
    Public ftCreationTime As FILETIME
    Public ftLastAccessTime As FILETIME
    Public ftLastWriteTime As FILETIME
    Public nFileSizeHigh As Integer
    Public nFileSizeLow As Integer
    Public dwReserved0 As Integer
    Public dwReserved1 As Integer
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> Public cFileName As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> Public cAlternateFileName As String
    Private Const MAX_PATH As Integer = 260
End Structure

<StructLayoutAttribute(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> Public Structure WIN32_FIND_DATAW
    Public dwFileAttributes As Integer
    Public ftCreationTime As FILETIME
    Public ftLastAccessTime As FILETIME
    Public ftLastWriteTime As FILETIME
    Public nFileSizeHigh As Integer
    Public nFileSizeLow As Integer
    Public dwReserved0 As Integer
    Public dwReserved1 As Integer
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> Public cFileName As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> Public cAlternateFileName As String
    Private Const MAX_PATH As Integer = 260
End Structure

< _
  ComImport(), _
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown), _
  Guid("0000010B-0000-0000-C000-000000000046") _
> _
Public Interface IPersistFile

#Region "Methods inherited from IPersist"

    Sub GetClassID( _
      <Out()> ByRef pClassID As Guid)

#End Region

    <PreserveSig()> _
    Function IsDirty() As Integer

    Sub Load( _
      <MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String, _
      ByVal dwMode As Integer)

    Sub Save( _
      <MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String, _
      <MarshalAs(UnmanagedType.Bool)> ByVal fRemember As Boolean)

    Sub SaveCompleted( _
      <MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String)

    Sub GetCurFile( _
      ByRef ppszFileName As IntPtr)

End Interface

< _
  ComImport(), _
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown), _
  Guid("000214EE-0000-0000-C000-000000000046") _
> _
Public Interface IShellLinkA

    Sub GetPath( _
      <Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszFile As StringBuilder, _
      ByVal cchMaxPath As Integer, _
      <Out()> ByRef pfd As WIN32_FIND_DATAA, _
      ByVal fFlags As SLGP_FLAGS)

    Sub GetIDList( _
      ByRef ppidl As IntPtr)

    Sub SetIDList( _
      ByVal pidl As IntPtr)

    Sub GetDescription( _
      <Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszName As StringBuilder, _
      ByVal cchMaxName As Integer)

    Sub SetDescription( _
      <MarshalAs(UnmanagedType.LPStr)> ByVal pszName As String)

    Sub GetWorkingDirectory( _
      <Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszDir As StringBuilder, _
      ByVal cchMaxPath As Integer)

    Sub SetWorkingDirectory( _
      <MarshalAs(UnmanagedType.LPStr)> ByVal pszDir As String)

    Sub GetArguments( _
      <Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszArgs As StringBuilder, _
      ByVal cchMaxPath As Integer)

    Sub SetArguments( _
      <MarshalAs(UnmanagedType.LPStr)> ByVal pszArgs As String)

    Sub GetHotkey( _
      ByRef pwHotkey As Short)

    Sub SetHotkey( _
      ByVal wHotkey As Short)

    Sub GetShowCmd( _
      ByRef piShowCmd As Integer)

    Sub SetShowCmd( _
      ByVal iShowCmd As Integer)

    Sub GetIconLocation( _
      <Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszIconPath As StringBuilder, _
      ByVal cchIconPath As Integer, _
      ByRef piIcon As Integer)

    Sub SetIconLocation( _
      <MarshalAs(UnmanagedType.LPStr)> ByVal pszIconPath As String, _
      ByVal iIcon As Integer)

    Sub SetRelativePath( _
      <MarshalAs(UnmanagedType.LPStr)> ByVal pszPathRel As String, _
      ByVal dwReserved As Integer)

    Sub Resolve( _
      ByVal hwnd As IntPtr, _
      ByVal fFlags As SLR_FLAGS)

    Sub SetPath( _
      <MarshalAs(UnmanagedType.LPStr)> ByVal pszFile As String)

End Interface

< _
  ComImport(), _
  InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown), _
  Guid("000214F9-0000-0000-C000-000000000046") _
> _
Public Interface IShellLinkW

    Sub GetPath( _
      <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszFile As StringBuilder, _
      ByVal cchMaxPath As Integer, _
      <Out()> ByRef pfd As WIN32_FIND_DATAW, _
      ByVal fFlags As SLGP_FLAGS)

    Sub GetIDList( _
      ByRef ppidl As IntPtr)

    Sub SetIDList( _
      ByVal pidl As IntPtr)

    Sub GetDescription( _
      <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As StringBuilder, _
      ByVal cchMaxName As Integer)

    Sub SetDescription( _
      <MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As String)

    Sub GetWorkingDirectory( _
      <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszDir As StringBuilder, _
      ByVal cchMaxPath As Integer)

    Sub SetWorkingDirectory( _
      <MarshalAs(UnmanagedType.LPWStr)> ByVal pszDir As String)

    Sub GetArguments( _
      <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszArgs As StringBuilder, _
      ByVal cchMaxPath As Integer)

    Sub SetArguments( _
      <MarshalAs(UnmanagedType.LPWStr)> ByVal pszArgs As String)

    Sub GetHotkey( _
      ByRef pwHotkey As Short)

    Sub SetHotkey( _
      ByVal wHotkey As Short)

    Sub GetShowCmd( _
      ByRef piShowCmd As Integer)

    Sub SetShowCmd( _
      ByVal iShowCmd As Integer)

    Sub GetIconLocation( _
      <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszIconPath As StringBuilder, _
      ByVal cchIconPath As Integer, _
      ByRef piIcon As Integer)

    Sub SetIconLocation( _
      <MarshalAs(UnmanagedType.LPWStr)> ByVal pszIconPath As String, _
      ByVal iIcon As Integer)

    Sub SetRelativePath( _
      <MarshalAs(UnmanagedType.LPWStr)> ByVal pszPathRel As String, _
      ByVal dwReserved As Integer)

    Sub Resolve( _
      ByVal hwnd As IntPtr, _
      ByVal fFlags As SLR_FLAGS)

    Sub SetPath( _
      <MarshalAs(UnmanagedType.LPWStr)> ByVal pszFile As String)

End Interface

' The following does currently not compile correctly. Use
' Type.GetTypeFromCLSID() and Activator.CreateInstance() instead.
'
'< _
'  ComImport(), _
'  Guid("00021401-0000-0000-C000-000000000046") _
'> _
'Public Class ShellLink
'  'Implements IPersistFile
'  'Implements IShellLinkA
'  'Implements IShellLinkW
'End Class

