Imports Zamba.Core

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for MessagesFactoryTest and is intended
'''to contain all MessagesFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class MessagesFactoryTest


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
    '''A test for SetAsRead
    '''</summary>
    <TestMethod()> _
    Public Sub SetAsReadTest()
        Dim Id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        MessagesFactory.SetAsRead(Id, UserId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveAutomail
    '''</summary>
    <TestMethod()> _
    Public Sub SaveAutomailTest()
        Dim automail As AutoMail = Nothing ' TODO: Initialize to an appropriate value
        MessagesFactory.SaveAutomail(automail)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for rollBack
    '''</summary>
    <TestMethod()> _
    Public Sub rollBackTest()
        Dim Id As Long = 0 ' TODO: Initialize to an appropriate value
        MessagesFactory.rollBack(Id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveAutomail
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveAutomailTest()
        Dim AutomailId As Integer = 0 ' TODO: Initialize to an appropriate value
        MessagesFactory.RemoveAutomail(AutomailId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RegisterMailToUser
    '''</summary>
    <TestMethod()> _
    Public Sub RegisterMailToUserTest()
        Dim mailId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim userId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim mailType As MessageType = New MessageType ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        MessagesFactory.RegisterMailToUser(mailId, userId, mailType, userName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for MessageRegister
    '''</summary>
    <TestMethod()> _
    Public Sub MessageRegisterTest()
        Dim Id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim De As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Body As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Subject As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim ConfirmChar As Integer = 0 ' TODO: Initialize to an appropriate value
        MessagesFactory.MessageRegister(Id, De, Body, Subject, ConfirmChar)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for isAlreadyRead
    '''</summary>
    <TestMethod()> _
    Public Sub isAlreadyReadTest()
        Dim msgid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim usr As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = MessagesFactory.isAlreadyRead(msgid, usr)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for InsertAutoMail
    '''</summary>
    <TestMethod()> _
    Public Sub InsertAutoMailTest()
        Dim am As AutoMail = Nothing ' TODO: Initialize to an appropriate value
        MessagesFactory.InsertAutoMail(am)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertAttach
    '''</summary>
    <TestMethod()> _
    Public Sub InsertAttachTest()
        Dim Id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim attachID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim attachDocTypeID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim attachFolderId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim attachIndex As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim attachName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim attachIconId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim attachDiskGroupId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim attachDocFile As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim attachOffSet As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim attachDISKVOLPATH As String = String.Empty ' TODO: Initialize to an appropriate value
        MessagesFactory.InsertAttach(Id, attachID, attachDocTypeID, attachFolderId, attachIndex, attachName, attachIconId, attachDiskGroupId, attachDocFile, attachOffSet, attachDISKVOLPATH)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for getMyMessageAttachs
    '''</summary>
    <TestMethod()> _
    Public Sub getMyMessageAttachsTest()
        Dim user_id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DSAttach = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSAttach
        actual = MessagesFactory.getMyMessageAttachs(user_id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getMessages
    '''</summary>
    <TestMethod()> _
    Public Sub getMessagesTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As dsMessages = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As dsMessages
        actual = MessagesFactory.getMessages(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAutomailNameByid
    '''</summary>
    <TestMethod()> _
    Public Sub GetAutomailNameByidTest()
        Dim Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = MessagesFactory.GetAutomailNameByid(Id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAutomailList
    '''</summary>
    <TestMethod()> _
    Public Sub GetAutomailListTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = MessagesFactory.GetAutomailList
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAutomailById
    '''</summary>
    <TestMethod()> _
    Public Sub GetAutomailByIdTest()
        Dim Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = MessagesFactory.GetAutomailById(Id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getAddressBookPath
    '''</summary>
    <TestMethod()> _
    Public Sub getAddressBookPathTest()
        Dim usr_id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = MessagesFactory.getAddressBookPath(usr_id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As MessagesFactory = New MessagesFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteMessageSended
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteMessageSendedTest()
        Dim Id As Long = 0 ' TODO: Initialize to an appropriate value
        MessagesFactory.DeleteMessageSended(Id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteMessageRecived
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteMessageRecivedTest()
        Dim Id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        MessagesFactory.DeleteMessageRecived(Id, UserId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteMessage
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteMessageTest()
        Dim idMessage As Integer = 0 ' TODO: Initialize to an appropriate value
        MessagesFactory.DeleteMessage(idMessage)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for countNewMessages
    '''</summary>
    <TestMethod()> _
    Public Sub countNewMessagesTest()
        Dim UserID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = MessagesFactory.countNewMessages(UserID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for MessagesFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub MessagesFactoryConstructorTest()
        Dim target As MessagesFactory = New MessagesFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
