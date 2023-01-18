Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for UserFactory_MailTest and is intended
'''to contain all UserFactory_MailTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UserFactory_MailTest


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
    '''A test for UpdateUserPasswordById
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateUserPasswordByIdTest()
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _userPassword As String = String.Empty ' TODO: Initialize to an appropriate value
        UserFactory.Mail.UpdateUserPasswordById(_userId, _userPassword)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateUserNameById
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateUserNameByIdTest()
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _userName As String = String.Empty ' TODO: Initialize to an appropriate value
        UserFactory.Mail.UpdateUserNameById(_userId, _userName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateProveedorById
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateProveedorByIdTest()
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _proveedorSMTP As String = String.Empty ' TODO: Initialize to an appropriate value
        UserFactory.Mail.UpdateProveedorById(_userId, _proveedorSMTP)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateAllById
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateAllByIdTest()
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _password As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _proveedorSMTP As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _puerto As Short = 0 ' TODO: Initialize to an appropriate value
        Dim _correo As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _mailServer As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _mailType As Short = 0 ' TODO: Initialize to an appropriate value
        Dim _baseMail As String = String.Empty ' TODO: Initialize to an appropriate value
        UserFactory.Mail.UpdateAllById(_userId, _userName, _password, _proveedorSMTP, _puerto, _correo, _mailServer, _mailType, _baseMail)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetNewUser
    '''</summary>
    <TestMethod()> _
    Public Sub SetNewUserTest()
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _password As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _proveedorSMTP As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _puerto As Short = 0 ' TODO: Initialize to an appropriate value
        Dim _correo As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _mailServer As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _mailType As Short = 0 ' TODO: Initialize to an appropriate value
        Dim _baseMail As String = String.Empty ' TODO: Initialize to an appropriate value
        UserFactory.Mail.SetNewUser(_userId, _userName, _password, _proveedorSMTP, _puerto, _correo, _mailServer, _mailType, _baseMail)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetUserById
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserByIdTest()
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserFactory.Mail.GetUserById(_userId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetMailConfigById
    '''</summary>
    <TestMethod()> _
    Public Sub GetMailConfigByIdTest()
        Dim _userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UserFactory.Mail.GetMailConfigById(_userId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for FillUserMailConfigByRef
    '''</summary>
    <TestMethod()> _
    Public Sub FillUserMailConfigByRefTest1()
        Dim _user As IUser = Nothing ' TODO: Initialize to an appropriate value
        Dim _userExpected As IUser = Nothing ' TODO: Initialize to an appropriate value
        UserFactory.Mail.FillUserMailConfigByRef(_user)
        Assert.AreEqual(_userExpected, _user)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for FillUserMailConfigByRef
    '''</summary>
    <TestMethod()> _
    Public Sub FillUserMailConfigByRefTest()
        Dim lngUserID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim strUserMail As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserMailExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserPass As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserPassExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserProveedorSMTP As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserProveedorSMTPExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim shtUserPuerto As Short = 0 ' TODO: Initialize to an appropriate value
        Dim shtUserPuertoExpected As Short = 0 ' TODO: Initialize to an appropriate value
        Dim strUserServidor As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserServidorExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim enmUserMailType As MailTypes = New MailTypes ' TODO: Initialize to an appropriate value
        Dim enmUserMailTypeExpected As MailTypes = New MailTypes ' TODO: Initialize to an appropriate value
        Dim strUserUName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserUNameExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserBaseMail As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserBaseMailExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        UserFactory.Mail.FillUserMailConfigByRef(lngUserID, strUserMail, strUserPass, strUserProveedorSMTP, shtUserPuerto, strUserServidor, enmUserMailType, strUserUName, strUserBaseMail)
        Assert.AreEqual(strUserMailExpected, strUserMail)
        Assert.AreEqual(strUserPassExpected, strUserPass)
        Assert.AreEqual(strUserProveedorSMTPExpected, strUserProveedorSMTP)
        Assert.AreEqual(shtUserPuertoExpected, shtUserPuerto)
        Assert.AreEqual(strUserServidorExpected, strUserServidor)
        Assert.AreEqual(enmUserMailTypeExpected, enmUserMailType)
        Assert.AreEqual(strUserUNameExpected, strUserUName)
        Assert.AreEqual(strUserBaseMailExpected, strUserBaseMail)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Mail Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub UserFactory_MailConstructorTest()
        Dim target As UserFactory.Mail = New UserFactory.Mail
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
