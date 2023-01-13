Imports System.Collections

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for UserGroupComponentFactoryTest and is intended
'''to contain all UserGroupComponentFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UserGroupComponentFactoryTest


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
    '''A test for GroupTable
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GroupTableTest()
        Dim actual As Hashtable
        actual = UserGroupComponentFactory_Accessor.GroupTable
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Update
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateTest()
        Dim Group As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UserGroupComponentFactory.Update(Group)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for RemoveUser
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveUserTest()
        Dim ug As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim u As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UserGroupComponentFactory.RemoveUser(ug, u)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserGroupsIds
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserGroupsIdsTest()
        Dim usrid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserGroupComponentFactory.GetUserGroupsIds(usrid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getUserGroups
    '''</summary>
    <TestMethod()> _
    Public Sub getUserGroupsTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserGroupComponentFactory.getUserGroups(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetNewGroup
    '''</summary>
    <TestMethod()> _
    Public Sub GetNewGroupTest()
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUserGroup
        actual = UserGroupComponentFactory.GetNewGroup(Name)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetFilteredAllGroupsArrayList
    '''</summary>
    <TestMethod()> _
    Public Sub GetFilteredAllGroupsArrayListTest()
        Dim SelectedUserGroupsIds As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserGroupComponentFactory.GetFilteredAllGroupsArrayList(SelectedUserGroupsIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllGroupsArrayList
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllGroupsArrayListTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserGroupComponentFactory.GetAllGroupsArrayList
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllGroups
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllGroupsTest()
        Dim expected As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Hashtable
        actual = UserGroupComponentFactory.GetAllGroups
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteGroupRights
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteGroupRightsTest()
        Dim groupid As Integer = 0 ' TODO: Initialize to an appropriate value
        UserGroupComponentFactory.DeleteGroupRights(groupid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteGroup
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteGroupTest()
        Dim ug As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        UserGroupComponentFactory.DeleteGroup(ug)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AssignUser
    '''</summary>
    <TestMethod()> _
    Public Sub AssignUserTest()
        Dim u As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim ug As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UserGroupComponentFactory.AssignUser(u, ug)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Addgroup
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub AddgroupTest()
        Dim usr As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        UserGroupComponentFactory_Accessor.Addgroup(usr)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for _GetGroups
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub _GetGroupsTest()
        Dim filter As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Order As String = String.Empty ' TODO: Initialize to an appropriate value
        UserGroupComponentFactory_Accessor._GetGroups(filter, Order)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UserGroupComponentFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub UserGroupComponentFactoryConstructorTest()
        Dim target As UserGroupComponentFactory = New UserGroupComponentFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
