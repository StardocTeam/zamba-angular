Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for WFUsersFactoryTest and is intended
'''to contain all WFUsersFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WFUsersFactoryTest


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
    '''A test for GetUsersByStepID
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersByStepIDTest()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFUsersFactory.GetUsersByStepID(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetGroupsByStepID
    '''</summary>
    <TestMethod()> _
    Public Sub GetGroupsByStepIDTest()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFUsersFactory.GetGroupsByStepID(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDistinctUsersByStepID
    '''</summary>
    <TestMethod()> _
    Public Sub GetDistinctUsersByStepIDTest()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFUsersFactory.GetDistinctUsersByStepID(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAsignedUsersCountByWorkflow
    '''</summary>
    <TestMethod()> _
    Public Sub GetAsignedUsersCountByWorkflowTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = WFUsersFactory.GetAsignedUsersCountByWorkflow(workflowid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAsignedUsersCountByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetAsignedUsersCountByStepTest()
        Dim stepid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = WFUsersFactory.GetAsignedUsersCountByStep(stepid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for WFUsersFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub WFUsersFactoryConstructorTest()
        Dim target As WFUsersFactory = New WFUsersFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
