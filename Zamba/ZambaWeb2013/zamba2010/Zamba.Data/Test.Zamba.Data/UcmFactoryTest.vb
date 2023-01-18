Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for UcmFactoryTest and is intended
'''to contain all UcmFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UcmFactoryTest


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
    '''A test for verifyIfUserStillExistsInUCM
    '''</summary>
    <TestMethod()> _
    Public Sub verifyIfUserStillExistsInUCMTest()
        Dim con_id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim pcName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = UcmFactory.verifyIfUserStillExistsInUCM(con_id, pcName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateLicWF
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateLicWFTest()
        UcmFactory.UpdateLicWF()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateLicDoc
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateLicDocTest()
        UcmFactory.UpdateLicDoc()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveWFConnections
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveWFConnectionsTest()
        Dim ConnectionId As Long = 0 ' TODO: Initialize to an appropriate value
        UcmFactory.RemoveWFConnections(ConnectionId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveExpiredConnection
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveExpiredConnectionTest()
        Dim timeout As Short = 0 ' TODO: Initialize to an appropriate value
        UcmFactory.RemoveExpiredConnection(timeout)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveConnection
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveConnectionTest()
        Dim ConnectionID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim pcName As String = String.Empty ' TODO: Initialize to an appropriate value
        UcmFactory.RemoveConnection(ConnectionID, pcName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for MakeNewConnection
    '''</summary>
    <TestMethod()> _
    Public Sub MakeNewConnectionTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim WinUser As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim WinPC As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim ActCon As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim WF As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim TimeOut As Short = 0 ' TODO: Initialize to an appropriate value
        UcmFactory.MakeNewConnection(UserId, WinUser, WinPC, ActCon, WF, TimeOut)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetNewConnection_Id
    '''</summary>
    <TestMethod()> _
    Public Sub GetNewConnection_IdTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = UcmFactory.GetNewConnection_Id
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ActiveWorkFlowConnections
    '''</summary>
    <TestMethod()> _
    Public Sub ActiveWorkFlowConnectionsTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = UcmFactory.ActiveWorkFlowConnections
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ActiveConections
    '''</summary>
    <TestMethod()> _
    Public Sub ActiveConectionsTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = UcmFactory.ActiveConections
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UcmFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub UcmFactoryConstructorTest()
        Dim target As UcmFactory = New UcmFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
