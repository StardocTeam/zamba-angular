Imports System.Collections

Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for UserGroupsTest and is intended
'''to contain all UserGroupsTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UserGroupsTest


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
    '''A test for GroupName
    '''</summary>
    <TestMethod()> _
    Public Sub GroupNameTest()
        Dim Group_Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = UserGroups.GroupName(Group_Id)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateUserGroup
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateUserGroupTest()
        Dim Usergroup As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        UserGroups.UpdateUserGroup(Usergroup)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for IsUserGroupDuplicated
    '''</summary>
    <TestMethod()> _
    Public Sub IsUserGroupDuplicatedTest()
        Dim UserGroupName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UserGroups.IsUserGroupDuplicated(UserGroupName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsr_R_Groups
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsr_R_GroupsTest()
        Dim groupID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserGroups.GetUsr_R_Groups(groupID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserorGroupNamebyId
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserorGroupNamebyIdTest()
        Dim UserorGroupID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = UserGroups.GetUserorGroupNamebyId(UserorGroupID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserorGroupIdbyName
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserorGroupIdbyNameTest()
        Dim UserorGroupName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = UserGroups.GetUserorGroupIdbyName(UserorGroupName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserIds
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserIdsTest()
        Dim groupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = UserGroups.GetUserIds(groupId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserGroupsArrayList
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserGroupsArrayListTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserGroups.GetUserGroupsArrayList
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserGroups
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserGroupsTest()
        Dim expected As DSUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSUserGroup
        actual = UserGroups.GetUserGroups
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserGroup
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserGroupTest()
        Dim userGroupID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserGroups.GetUserGroup(userGroupID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserGoupsAsDataSet
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserGoupsAsDataSetTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserGroups.GetUserGoupsAsDataSet
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetGroupIdByName
    '''</summary>
    <TestMethod()> _
    Public Sub GetGroupIdByNameTest()
        Dim GroupName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = UserGroups.GetGroupIdByName(GroupName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As UserGroups = New UserGroups ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelUserGroup
    '''</summary>
    <TestMethod()> _
    Public Sub DelUserGroupTest()
        Dim UsergroupID As Integer = 0 ' TODO: Initialize to an appropriate value
        UserGroups.DelUserGroup(UsergroupID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelMember
    '''</summary>
    <TestMethod()> _
    Public Sub DelMemberTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim UserGroupId As Integer = 0 ' TODO: Initialize to an appropriate value
        UserGroups.DelMember(UserId, UserGroupId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddUserGroup
    '''</summary>
    <TestMethod()> _
    Public Sub AddUserGroupTest()
        Dim NewGroup As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = UserGroups.AddUserGroup(NewGroup)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddMember
    '''</summary>
    <TestMethod()> _
    Public Sub AddMemberTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim UserGroupId As Integer = 0 ' TODO: Initialize to an appropriate value
        UserGroups.AddMember(UserId, UserGroupId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UserGroups Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub UserGroupsConstructorTest()
        Dim target As UserGroups = New UserGroups
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
