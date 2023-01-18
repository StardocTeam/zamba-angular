Imports System.Collections.Generic

Imports System.Data

Imports System.Collections

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for RightFactoryTest and is intended
'''to contain all RightFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class RightFactoryTest


    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''A test for GroupRights
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GroupRightsTest1()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As dsGroupRights
        actual = RightFactory_Accessor.GroupRights(id)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GroupRights
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GroupRightsTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As dsGroupRights
        actual = RightFactory_Accessor.GroupRights(id)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for VerLicenciasWorkFlow
    '''</summary>
    <TestMethod()> _
    Public Sub VerLicenciasWorkFlowTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = RightFactory.VerLicenciasWorkFlow
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for VerLicenciasDocumentales
    '''</summary>
    <TestMethod()> _
    Public Sub VerLicenciasDocumentalesTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = RightFactory.VerLicenciasDocumentales
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ValidateModuleLicense
    '''</summary>
    <TestMethod()> _
    Public Sub ValidateModuleLicenseTest()
        Dim Modulo As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory.ValidateModuleLicense(Modulo)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ValidateLogIn
    '''</summary>
    <TestMethod()> _
    Public Sub ValidateLogInTest1()
        Dim ID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = RightFactory.ValidateLogIn(ID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ValidateLogIn
    '''</summary>
    <TestMethod()> _
    Public Sub ValidateLogInTest()
        Dim User As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Password As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = RightFactory.ValidateLogIn(User, Password)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateLicenciasWorkFlow
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateLicenciasWorkFlowTest()
        Dim LicenciasActuales As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim TotalLicencias As Integer = 0 ' TODO: Initialize to an appropriate value
        RightFactory.UpdateLicenciasWorkFlow(LicenciasActuales, TotalLicencias)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateLicenciasDocumentales
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateLicenciasDocumentalesTest()
        Dim LicenciasActuales As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim TotalLicencias As String = String.Empty ' TODO: Initialize to an appropriate value
        RightFactory.UpdateLicenciasDocumentales(LicenciasActuales, TotalLicencias)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetRight
    '''</summary>
    <TestMethod()> _
    Public Sub SetRightTest()
        Dim groupid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim objectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim Rtype As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim Additional As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Value As Boolean = False ' TODO: Initialize to an appropriate value
        RightFactory.SetRight(groupid, objectId, Rtype, Additional, Value)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetIndexRights
    '''</summary>
    <TestMethod()> _
    Public Sub SetIndexRightsTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim GID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim righttypeid As Short = 0 ' TODO: Initialize to an appropriate value
        RightFactory.SetIndexRights(DocTypeId, GID, IndexId, righttypeid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveAction
    '''</summary>
    <TestMethod()> _
    Public Sub SaveActionTest()
        Dim ObjectId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ObjectType As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim ActionType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim S_Object_ID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _userid As Integer = 0 ' TODO: Initialize to an appropriate value
        RightFactory.SaveAction(ObjectId, ObjectType, ActionType, S_Object_ID, _userid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveIndexRights
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveIndexRightsTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim GID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim righttypeid As Short = 0 ' TODO: Initialize to an appropriate value
        RightFactory.RemoveIndexRights(DocTypeId, GID, IndexId, righttypeid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RegisterModule
    '''</summary>
    <TestMethod()> _
    Public Sub RegisterModuleTest()
        Dim ModuleId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ModuleName As String = String.Empty ' TODO: Initialize to an appropriate value
        RightFactory.RegisterModule(ModuleId, ModuleName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LoadRights
    '''</summary>
    <TestMethod()> _
    Public Sub LoadRightsTest()
        Dim user As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim dsgeneral As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim dsgeneralExpected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim dsarchivos As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim dsarchivosExpected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim dsdoctype As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim dsdoctypeExpected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim dsrestriction As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim dsrestrictionExpected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        RightFactory.LoadRights(user, dsgeneral, dsarchivos, dsdoctype, dsrestriction)
        Assert.AreEqual(dsgeneralExpected, dsgeneral)
        Assert.AreEqual(dsarchivosExpected, dsarchivos)
        Assert.AreEqual(dsdoctypeExpected, dsdoctype)
        Assert.AreEqual(dsrestrictionExpected, dsrestriction)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for IsUserPasswordNull
    '''</summary>
    <TestMethod()> _
    Public Sub IsUserPasswordNullTest()
        Dim usrID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory.IsUserPasswordNull(usrID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserRights
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserRightsTest1()
        Dim ObjectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim RType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim AditionalParam As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory.GetUserRights(ObjectId, RType, AditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserRights
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserRightsTest()
        Dim User As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim ObjectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim RType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim AditionalParam As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory.GetUserRights(User, ObjectId, RType, AditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserPassword
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserPasswordTest()
        Dim usrID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = RightFactory.GetUserPassword(usrID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserGroupRights
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserGroupRightsTest1()
        Dim UserGroupID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ObjectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim RType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim AditionalParam As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory.GetUserGroupRights(UserGroupID, ObjectId, RType, AditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserGroupRights
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserGroupRightsTest()
        Dim UserGroup As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim ObjectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim RType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim AditionalParam As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory.GetUserGroupRights(UserGroup, ObjectId, RType, AditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRows
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetRowsTest1()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ObjectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim RType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim AditionalParam As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected() As dsGroupRights.usr_rightsRow = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As dsGroupRights.usr_rightsRow
        actual = RightFactory_Accessor.GetRows(id, ObjectId, RType, AditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRows
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetRowsTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim objectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim rType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim aditionalParam As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected() As dsGroupRights.usr_rightsRow = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As dsGroupRights.usr_rightsRow
        actual = RightFactory_Accessor.GetRows(id, objectId, rType, aditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRight
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetRightTest1()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim objectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim rType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim aditionalParam As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory_Accessor.GetRight(id, objectId, rType, aditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRight
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetRightTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim objectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim rType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim aditionalParam As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory_Accessor.GetRight(id, objectId, rType, aditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexsRights
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexsRightsTest1()
        Dim DocTypeIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim GID As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = RightFactory.GetIndexsRights(DocTypeIds, GID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexsRights
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexsRightsTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim GID As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = RightFactory.GetIndexsRights(DocTypeId, GID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexRightValue
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexRightValueTest()
        Dim IndexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim doctypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _GID As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim RightTypeId As Short = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory.GetIndexRightValue(IndexId, doctypeid, _GID, RightTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetGroupsRights
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetGroupsRightsTest()
        Dim strselect As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As dsGroupRights = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As dsGroupRights
        actual = RightFactory_Accessor.GetGroupsRights(strselect)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeUserRightFromArchive
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeUserRightFromArchiveTest()
        Dim ds As DsDoctypeRight = Nothing ' TODO: Initialize to an appropriate value
        Dim Doc_GroupID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = RightFactory.GetDocTypeUserRightFromArchive(ds, Doc_GroupID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetArchivosUserRight
    '''</summary>
    <TestMethod()> _
    Public Sub GetArchivosUserRightTest()
        Dim ds As Data_Group_Doc = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = RightFactory.GetArchivosUserRight(ds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllDocTypesByUserRight
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllDocTypesByUserRightTest()
        Dim userid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = RightFactory.GetAllDocTypesByUserRight(userid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAditional
    '''</summary>
    <TestMethod()> _
    Public Sub GetAditionalTest()
        Dim ObjectType As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim Rtype As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = RightFactory.GetAditional(ObjectType, Rtype)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As RightFactory = New RightFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelRight
    '''</summary>
    <TestMethod()> _
    Public Sub DelRightTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ObjectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim RType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim AditionalParam As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory.DelRight(id, ObjectId, RType, AditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddRight
    '''</summary>
    <TestMethod()> _
    Public Sub AddRightTest()
        Dim Groupid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ObjectId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim RType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim AditionalParam As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = RightFactory.AddRight(Groupid, ObjectId, RType, AditionalParam)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for RightFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub RightFactoryConstructorTest()
        Dim target As RightFactory = New RightFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
