Imports System.Collections

Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for NotifyFactoryTest and is intended
'''to contain all NotifyFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class NotifyFactoryTest


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
    '''A test for ValidateUserInGroupToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub ValidateUserInGroupToNotifyTest()
        Dim _groupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = NotifyFactory.ValidateUserInGroupToNotify(_groupId, _userId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ValidateGroupToNotifyExist
    '''</summary>
    <TestMethod()> _
    Public Sub ValidateGroupToNotifyExistTest()
        Dim topicId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = NotifyFactory.ValidateGroupToNotifyExist(topicId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SetNewUserToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub SetNewUserToNotifyTest()
        Dim typeid As GroupToNotifyTypes = New GroupToNotifyTypes ' TODO: Initialize to an appropriate value
        Dim _groupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        NotifyFactory.SetNewUserToNotify(typeid, _groupId, _userId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetNewUserGroupToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub SetNewUserGroupToNotifyTest()
        Dim typeid As GroupToNotifyTypes = New GroupToNotifyTypes ' TODO: Initialize to an appropriate value
        Dim _groupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _userGroupId As Long = 0 ' TODO: Initialize to an appropriate value
        NotifyFactory.SetNewUserGroupToNotify(typeid, _groupId, _userGroupId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetNewMailToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub SetNewMailToNotifyTest()
        Dim typeid As GroupToNotifyTypes = New GroupToNotifyTypes ' TODO: Initialize to an appropriate value
        Dim _groupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _mail As String = String.Empty ' TODO: Initialize to an appropriate value
        NotifyFactory.SetNewMailToNotify(typeid, _groupId, _mail)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveAllData
    '''</summary>
    <TestMethod()> _
    Public Sub SaveAllDataTest()
        Dim doc_id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim typeid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim userid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim extradata As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim groupid As Long = 0 ' TODO: Initialize to an appropriate value
        NotifyFactory.SaveAllData(doc_id, typeid, userid, extradata, groupid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetUserToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserToNotifyTest()
        Dim typeid As GroupToNotifyTypes = New GroupToNotifyTypes ' TODO: Initialize to an appropriate value
        Dim Id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = NotifyFactory.GetUserToNotify(typeid, Id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserGroupToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserGroupToNotifyTest()
        Dim typeId As GroupToNotifyTypes = New GroupToNotifyTypes ' TODO: Initialize to an appropriate value
        Dim docId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = NotifyFactory.GetUserGroupToNotify(typeId, docId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetMailToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub GetMailToNotifyTest()
        Dim user As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = NotifyFactory.GetMailToNotify(user)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetGroupExternalMails
    '''</summary>
    <TestMethod()> _
    Public Sub GetGroupExternalMailsTest()
        Dim typeid As GroupToNotifyTypes = New GroupToNotifyTypes ' TODO: Initialize to an appropriate value
        Dim Id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = NotifyFactory.GetGroupExternalMails(typeid, Id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllData
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllDataTest()
        Dim doc_id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = NotifyFactory.GetAllData(doc_id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteUserToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteUserToNotifyTest()
        Dim _groupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        NotifyFactory.DeleteUserToNotify(_groupId, _userId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteUserGroupToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteUserGroupToNotifyTest()
        Dim _groupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _userGroupId As Long = 0 ' TODO: Initialize to an appropriate value
        NotifyFactory.DeleteUserGroupToNotify(_groupId, _userGroupId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteNotify
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteNotifyTest()
        Dim documentID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim typeNotify As GroupToNotifyTypes = New GroupToNotifyTypes ' TODO: Initialize to an appropriate value
        NotifyFactory.DeleteNotify(documentID, typeNotify)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteMailToNotify
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteMailToNotifyTest()
        Dim _groupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _mail As String = String.Empty ' TODO: Initialize to an appropriate value
        NotifyFactory.DeleteMailToNotify(_groupId, _mail)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for NotifyFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub NotifyFactoryConstructorTest()
        Dim target As NotifyFactory = New NotifyFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
