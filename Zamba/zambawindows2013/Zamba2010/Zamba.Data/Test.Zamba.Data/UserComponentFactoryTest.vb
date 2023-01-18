Imports System.Collections

Imports Zamba.Core

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for UserComponentFactoryTest and is intended
'''to contain all UserComponentFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UserComponentFactoryTest


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
    '''A test for UsrTable
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub UsrTableTest()
        Dim actual As ArrayList
        actual = UserComponentFactory_Accessor.UsrTable
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UserTable
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub UserTableTest()
        Dim actual As Hashtable
        actual = UserComponentFactory_Accessor.UserTable
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DsUser
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub DsUserTest()
        Dim actual As DataSet
        actual = UserComponentFactory_Accessor.DsUser
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for validateUser
    '''</summary>
    <TestMethod()> _
    Public Sub validateUserTest1()
        Dim name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Psw As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserComponentFactory.validateUser(name, Psw)
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
        actual = UserComponentFactory.validateUser(ID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateSign
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateSignTest()
        Dim usr As IUser = Nothing ' TODO: Initialize to an appropriate value
        UserComponentFactory.UpdateSign(usr)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveUser
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveUserTest()
        Dim u As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim ug As IUserGroup = Nothing ' TODO: Initialize to an appropriate value
        UserComponentFactory.RemoveUser(u, ug)
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
        actual = UserComponentFactory.RemoveGroup(ug, u)
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
        actual = UserComponentFactory.GetUserNamebyId(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserByname
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserBynameTest()
        Dim name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserComponentFactory.GetUserByname(name)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUser
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserTest()
        Dim uid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserComponentFactory.GetUser(uid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetNewUser
    '''</summary>
    <TestMethod()> _
    Public Sub GetNewUserTest()
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IUser
        actual = UserComponentFactory.GetNewUser(Name)
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
        actual = UserComponentFactory.GetGroupUsers(GroupId)
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
        actual = UserComponentFactory.GetFilteredAllUsersArrayList(SelectedUsers)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllUsersDataset
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllUsersDatasetTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserComponentFactory.GetAllUsersDataset
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getAllUsersArrayListSorted
    '''</summary>
    <TestMethod()> _
    Public Sub getAllUsersArrayListSortedTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserComponentFactory.getAllUsersArrayListSorted
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllUsersArrayList
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllUsersArrayListTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = UserComponentFactory.GetAllUsersArrayList
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllUsers
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllUsersTest()
        Dim expected As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Hashtable
        actual = UserComponentFactory.GetAllUsers
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest()
        Dim usr As IUser = Nothing ' TODO: Initialize to an appropriate value
        UserComponentFactory.Delete(usr)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
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
        actual = UserComponentFactory.AssignGroup(u, ug)
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
        actual = UserComponentFactory.AssignGroup(ug, u)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddUser
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub AddUserTest()
        Dim usr As IUser = Nothing ' TODO: Initialize to an appropriate value
        UserComponentFactory_Accessor.AddUser(usr)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for _GetUsers
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub _GetUsersTest()
        Dim filter As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Order As String = String.Empty ' TODO: Initialize to an appropriate value
        UserComponentFactory_Accessor._GetUsers(filter, Order)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for _getuserDataset
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub _getuserDatasetTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserComponentFactory_Accessor._getuserDataset
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UserComponentFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub UserComponentFactoryConstructorTest()
        Dim target As UserComponentFactory = New UserComponentFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
