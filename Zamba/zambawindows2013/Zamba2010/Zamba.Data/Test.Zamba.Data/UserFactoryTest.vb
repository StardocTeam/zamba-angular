Imports System.Collections.Generic

Imports System.Collections

Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for UserFactoryTest and is intended
'''to contain all UserFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UserFactoryTest


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
    '''A test for validateUser
    '''</summary>
    <TestMethod()> _
    Public Sub validateUserTest1()
        Dim name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Psw As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserFactory.validateUser(name, Psw)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for validateUser
    '''</summary>
    <TestMethod()> _
    Public Sub validateUserTest()
        Dim ID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserFactory.validateUser(ID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateSign
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateSignTest()
        Dim usr As IUser = Nothing ' TODO: Initialize to an appropriate value
        UserFactory.UpdateSign(usr)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdatePicture
    '''</summary>
    <TestMethod()> _
    Public Sub UpdatePictureTest()
        Dim usr As IUser = Nothing ' TODO: Initialize to an appropriate value
        UserFactory.UpdatePicture(usr)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Update
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateTest()
        Dim usr As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UserFactory.Update(usr)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for RemoveUser
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveUserTest()
        Dim u As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim ug As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        UserFactory.RemoveUser(u, ug)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveGroup
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveGroupTest()
        Dim ug As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim u As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UserFactory.RemoveGroup(ug, u)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZoptDataBaseValues
    '''</summary>
    <TestMethod()> _
    Public Sub GetZoptDataBaseValuesTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserFactory.GetZoptDataBaseValues
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsersWithMailsNames
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersWithMailsNamesTest2()
        Dim userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As User = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As User
        actual = UserFactory.GetUsersWithMailsNames(userId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsersWithMailsNames
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersWithMailsNamesTest1()
        Dim userIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As List(Of User) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of User)
        actual = UserFactory.GetUsersWithMailsNames(userIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsersWithMailsNames
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersWithMailsNamesTest()
        Dim expected As List(Of User) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of User)
        actual = UserFactory.GetUsersWithMailsNames
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsersNamesAsDataSet
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersNamesAsDataSetTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserFactory.GetUsersNamesAsDataSet
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsersNames
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersNamesTest()
        Dim expected As List(Of User) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of User)
        actual = UserFactory.GetUsersNames
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserSignById
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserSignByIdTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = UserFactory.GetUserSignById(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsersDatasetOrderbyName
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersDatasetOrderbyNameTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserFactory.GetUsersDatasetOrderbyName
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsersArrayList
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersArrayListTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserFactory.GetUsersArrayList
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsers
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersTest()
        Dim userIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As List(Of IUser) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of IUser)
        actual = UserFactory.GetUsers(userIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserNamebyId
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserNamebyIdTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = UserFactory.GetUserNamebyId(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserID
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserIDTest()
        Dim name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = UserFactory.GetUserID(name)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserGroupsIdsByUserid
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserGroupsIdsByUseridTest()
        Dim userid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Long)
        actual = UserFactory.GetUserGroupsIdsByUserid(userid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserByName
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserByNameTest()
        Dim UserName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserFactory.GetUserByName(UserName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserById
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserByIdTest()
        Dim userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserFactory.GetUserById(userId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetNewUser
    '''</summary>
    <TestMethod()> _
    Public Sub GetNewUserTest()
        Dim sessionName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userLastName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserFactory.GetNewUser(sessionName, userName, userLastName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetGroupUsers
    '''</summary>
    <TestMethod()> _
    Public Sub GetGroupUsersTest()
        Dim GroupId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserFactory.GetGroupUsers(GroupId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetGroupToNotifyAsIDArray
    '''</summary>
    <TestMethod()> _
    Public Sub GetGroupToNotifyAsIDArrayTest()
        Dim typeId As GroupToNotifyTypes = New GroupToNotifyTypes ' TODO: Initialize to an appropriate value
        Dim _groupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected() As Long = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As Long
        actual = UserFactory.GetGroupToNotifyAsIDArray(typeId, _groupId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetFilteredAllUsersArrayList
    '''</summary>
    <TestMethod()> _
    Public Sub GetFilteredAllUsersArrayListTest()
        Dim SelectedUsers As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserFactory.GetFilteredAllUsersArrayList(SelectedUsers)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for FillGroups
    '''</summary>
    <TestMethod()> _
    Public Sub FillGroupsTest()
        Dim User As IUser = Nothing ' TODO: Initialize to an appropriate value
        UserFactory.FillGroups(User)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As UserFactory = New UserFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteGroup
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteGroupTest()
        Dim u As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim ug As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        UserFactory.DeleteGroup(u, ug)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest1()
        Dim userId As Long = 0 ' TODO: Initialize to an appropriate value
        UserFactory.Delete(userId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest()
        Dim usr As IUser = Nothing ' TODO: Initialize to an appropriate value
        UserFactory.Delete(usr)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CompareUser
    '''</summary>
    <TestMethod()> _
    Public Sub CompareUserTest()
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Users As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UserFactory.CompareUser(Name, Users)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for BuildUser
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub BuildUserTest()
        Dim CurrentRow As DataRow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserFactory_Accessor.BuildUser(CurrentRow)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for BuildGroup
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub BuildGroupTest()
        Dim CurrentRow As DataRow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUserGroup
        actual = UserFactory_Accessor.BuildGroup(CurrentRow)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AssignGroup
    '''</summary>
    <TestMethod()> _
    Public Sub AssignGroupTest1()
        Dim u As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim ug As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UserFactory.AssignGroup(u, ug)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AssignGroup
    '''</summary>
    <TestMethod()> _
    Public Sub AssignGroupTest()
        Dim ug As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim u As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UserFactory.AssignGroup(ug, u)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddUser
    '''</summary>
    <TestMethod()> _
    Public Sub AddUserTest()
        Dim user As IUser = Nothing ' TODO: Initialize to an appropriate value
        UserFactory.AddUser(user)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UserFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub UserFactoryConstructorTest()
        Dim target As UserFactory = New UserFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
