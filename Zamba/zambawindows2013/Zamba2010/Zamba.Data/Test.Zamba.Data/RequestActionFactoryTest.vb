Imports System.Collections.Generic

Imports System

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for RequestActionFactoryTest and is intended
'''to contain all RequestActionFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class RequestActionFactoryTest


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
    '''A test for Update
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim requestDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim finishDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim isFinished As Boolean = False ' TODO: Initialize to an appropriate value
        Dim requestUserId As Long = 0 ' TODO: Initialize to an appropriate value
        RequestActionFactory.Update(id, requestDate, finishDate, isFinished, requestUserId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertUsers
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub InsertUsersTest()
        Dim requestActionId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim userIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        RequestActionFactory_Accessor.InsertUsers(requestActionId, userIds)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertTasks
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub InsertTasksTest()
        Dim requestActionId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim tasksAndStepIds As Dictionary(Of Long, Long) = Nothing ' TODO: Initialize to an appropriate value
        RequestActionFactory_Accessor.InsertTasks(requestActionId, tasksAndStepIds)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertRules
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub InsertRulesTest()
        Dim requestActionId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ruleIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        RequestActionFactory_Accessor.InsertRules(requestActionId, ruleIds)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertRequestAction
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub InsertRequestActionTest()
        Dim requestDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim finishDate As Nullable(Of DateTime) = New Nullable(Of DateTime) ' TODO: Initialize to an appropriate value
        Dim isFinished As Boolean = False ' TODO: Initialize to an appropriate value
        Dim requestUserId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = RequestActionFactory_Accessor.InsertRequestAction(requestDate, finishDate, isFinished, requestUserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Insert
    '''</summary>
    <TestMethod()> _
    Public Sub InsertTest1()
        Dim requestActionId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim executionDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim expected As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Object
        actual = RequestActionFactory.Insert(requestActionId, userId, stepId, taskId, ruleId, executionDate)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Insert
    '''</summary>
    <TestMethod()> _
    Public Sub InsertTest()
        Dim requestDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim finishDate As Nullable(Of DateTime) = New Nullable(Of DateTime) ' TODO: Initialize to an appropriate value
        Dim isFinished As Boolean = False ' TODO: Initialize to an appropriate value
        Dim requestUserId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ruleIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim userIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim tasksAndStepIds As Dictionary(Of Long, Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = RequestActionFactory.Insert(requestDate, finishDate, isFinished, requestUserId, ruleIds, userIds, tasksAndStepIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsersCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersCountTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = RequestActionFactory.GetUsersCount(id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUsers
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersTest()
        Dim requestActionId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = RequestActionFactory.GetUsers(requestActionId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksCountTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = RequestActionFactory.GetTasksCount(id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasks
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksTest()
        Dim requestActionId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = RequestActionFactory.GetTasks(requestActionId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRules
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesTest()
        Dim requestActionId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = RequestActionFactory.GetRules(requestActionId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRequestAction
    '''</summary>
    <TestMethod()> _
    Public Sub GetRequestActionTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = RequestActionFactory.GetRequestAction(id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetExecutionsCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetExecutionsCountTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = RequestActionFactory.GetExecutionsCount(id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetExecutedTasks
    '''</summary>
    <TestMethod()> _
    Public Sub GetExecutedTasksTest()
        Dim requestActionId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = RequestActionFactory.GetExecutedTasks(requestActionId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteUsers
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub DeleteUsersTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        RequestActionFactory_Accessor.DeleteUsers(id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteTasks
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub DeleteTasksTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        RequestActionFactory_Accessor.DeleteTasks(id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteRules
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub DeleteRulesTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        RequestActionFactory_Accessor.DeleteRules(id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        RequestActionFactory.Delete(id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ClearFinished
    '''</summary>
    <TestMethod()> _
    Public Sub ClearFinishedTest()
        RequestActionFactory.ClearFinished()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RequestActionFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub RequestActionFactoryConstructorTest()
        Dim target As RequestActionFactory = New RequestActionFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
