Imports Zamba.Core

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for ActionsFactoryTest and is intended
'''to contain all ActionsFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ActionsFactoryTest


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
    '''A test for SaveActioninDB
    '''</summary>
    <TestMethod()> _
    Public Sub SaveActioninDBTest()
        Dim ObjectId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ObjectType As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim ActionType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim S_Object_ID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _userid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ConnectionId As Integer = 0 ' TODO: Initialize to an appropriate value
        ActionsFactory.SaveActioninDB(ObjectId, ObjectType, ActionType, S_Object_ID, _userid, ConnectionId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetUserActions
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserActionsTest()
        Dim UserId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = ActionsFactory.GetUserActions(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocumentActions
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocumentActionsTest()
        Dim documentId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = ActionsFactory.GetDocumentActions(documentId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CleanExceptions
    '''</summary>
    <TestMethod()> _
    Public Sub CleanExceptionsTest()
        ActionsFactory.CleanExceptions()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ActionsFactory Constructor
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub ActionsFactoryConstructorTest()
        Dim target As ActionsFactory_Accessor = New ActionsFactory_Accessor
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
