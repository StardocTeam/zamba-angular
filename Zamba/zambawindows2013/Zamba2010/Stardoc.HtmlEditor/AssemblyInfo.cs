using System.Reflection;
using System.Security.Permissions;

// define the securty sectting for the class
// ask for full trust or unmanaged code access (both should not be needed)
// [assembly:PermissionSetAttribute(SecurityAction.RequestMinimum, Name="FullTrust")]
[assembly:SecurityPermissionAttribute(SecurityAction.RequestMinimum, Flags=SecurityPermissionFlag.UnmanagedCode)]

[assembly: AssemblyTitle("HtmlEditorControl")]
[assembly: AssemblyDescription("HTML Editor Control")]
