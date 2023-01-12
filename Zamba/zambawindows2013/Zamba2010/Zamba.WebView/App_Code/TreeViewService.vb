Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Zamba.Core
Imports System.Data
Imports System.Collections

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class TreeViewService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetDocTypes() As ArrayList
        Return Zamba.Core.DocTypes.DocType.getDocTypesFactory
    End Function
    '<WebMethod()> _
    'Public Function GetDocTypes() As Generic.List(Of Zamba.Core.DocType)
    '    Dim DocTypes As New Generic.List(Of DocType)
    '    DocTypes.AddRange(Zamba.Core.DocTypes.DocType.getDocTypesFactory.ToArray)
    '    Return DocTypes
    'End Function

    <WebMethod()> _
    Public Function ValidateLogin(ByVal UserName As String, ByVal UserPassword As String) As User
        'Return Zamba.Users.Factory.SingletonRights.Rights.ValidateLogIn(UserName, UserPassword)
    End Function

    <WebMethod()> _
Public Function GetAllDocuments(ByVal DocTypeId) As dataset
        Return Zamba.Core.Results_Business.GetDocuments(DocTypeId)
    End Function

End Class
