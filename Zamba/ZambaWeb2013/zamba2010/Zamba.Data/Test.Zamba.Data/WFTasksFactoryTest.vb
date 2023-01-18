Imports System.Collections.Generic

Imports System.Data

Imports System

Imports Zamba.Core

Imports System.Collections

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for WFTasksFactoryTest and is intended
'''to contain all WFTasksFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WFTasksFactoryTest


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
    '''A test for UpdateTaskState
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateTaskStateTest1()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskStateId As Integer = 0 ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateTaskState(taskId, taskStateId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateTaskState
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateTaskStateTest()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateTaskState(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateState
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateStateTest2()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stateId As Integer = 0 ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateState(taskId, stepId, stateId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateState
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateStateTest1()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateState(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateState
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateStateTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim stateId As Integer = 0 ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateState(taskId, stepId, stateId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateExpiredDate
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateExpiredDateTest1()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateExpiredDate(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateExpiredDate
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateExpiredDateTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expireDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateExpiredDate(taskId, expireDate)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateExclusive
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateExclusiveTest1()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateExclusive(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateExclusive
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateExclusiveTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim isExclusive As Boolean = False ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateExclusive(taskId, isExclusive)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateDistribuir
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateDistribuirTest()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateDistribuir(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateAssign
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateAssignTest2()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim asignedToUserId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim asignedByUserId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim asignedDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim taskState As TaskStates = New TaskStates ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateAssign(taskId, asignedToUserId, asignedByUserId, asignedDate, taskState)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateAssign
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateAssignTest1()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim asignedToUserId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim asignedByUserId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim asignedDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim taskStateId As Integer = 0 ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateAssign(taskId, asignedToUserId, asignedByUserId, asignedDate, taskStateId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateAssign
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateAssignTest()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.UpdateAssign(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveIntoIP_Task
    '''</summary>
    <TestMethod()> _
    Public Sub SaveIntoIP_TaskTest1()
        Dim alFiles As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IPTASKID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim conf_Id As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        WFTasksFactory.SaveIntoIP_Task(alFiles, IPTASKID, conf_Id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveIntoIP_Task
    '''</summary>
    <TestMethod()> _
    Public Sub SaveIntoIP_TaskTest()
        Dim alFiles As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IPTASKID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ZipOrigen As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim conf_Id As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        WFTasksFactory.SaveIntoIP_Task(alFiles, IPTASKID, ZipOrigen, conf_Id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogViewTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogViewTaskTest1()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim user As IUser = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogViewTask(task, user)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogViewTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogViewTaskTest()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim state As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogViewTask(taskID, taskName, docTypeId, docTypeName, folderId, stepId, stepName, state, userName, workflowId, workflowName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogStartTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogStartTaskTest1()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim state As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogStartTask(taskID, taskName, docTypeId, docTypeName, folderId, stepId, stepName, state, userName, workflowId, workflowName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogStartTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogStartTaskTest()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogStartTask(task)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogRejectTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogRejectTaskTest2()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim user As IUser = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogRejectTask(task, user)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogRejectTask
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub LogRejectTaskTest1()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim state As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory_Accessor.LogRejectTask(taskID, taskName, docTypeId, docTypeName, folderId, stepId, stepName, state, userName, workflowId, workflowName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogRejectTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogRejectTaskTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogRejectTask(taskId, userName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogFinishTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogFinishTaskTest2()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogFinishTask(taskId, userName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogFinishTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogFinishTaskTest1()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim User As IUser = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogFinishTask(task, User)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogFinishTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogFinishTaskTest()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim state As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogFinishTask(taskID, taskName, docTypeId, docTypeName, folderId, stepId, stepName, state, userName, workflowId, workflowName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogChengeExpireDate
    '''</summary>
    <TestMethod()> _
    Public Sub LogChengeExpireDateTest()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogChengeExpireDate(task)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogCheckOut
    '''</summary>
    <TestMethod()> _
    Public Sub LogCheckOutTest2()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim AsignedBy As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogCheckOut(task, AsignedBy)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogCheckOut
    '''</summary>
    <TestMethod()> _
    Public Sub LogCheckOutTest1()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim stateName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogCheckOut(taskID, taskName, docTypeId, docTypeName, folderId, stepId, stepName, stateName, userName, workflowId, workflowName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogCheckOut
    '''</summary>
    <TestMethod()> _
    Public Sub LogCheckOutTest()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogCheckOut(task)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogCheckIn
    '''</summary>
    <TestMethod()> _
    Public Sub LogCheckInTest2()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogCheckIn(task)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogCheckIn
    '''</summary>
    <TestMethod()> _
    Public Sub LogCheckInTest1()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim checkIn As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogCheckIn(taskId, checkIn, userName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogCheckIn
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub LogCheckInTest()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim statename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim taskCheckIn As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        WFTasksFactory_Accessor.LogCheckIn(taskID, taskName, docTypeId, docTypeName, folderId, stepId, stepName, statename, userName, workflowId, workflowName, taskCheckIn)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogChangeStepState
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub LogChangeStepStateTest2()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim state As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim stateName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory_Accessor.LogChangeStepState(taskID, taskName, docTypeId, docTypeName, folderId, stepId, stepName, state, stateName, userName, workflowId, workflowName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogChangeStepState
    '''</summary>
    <TestMethod()> _
    Public Sub LogChangeStepStateTest1()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogChangeStepState(task)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogChangeStepState
    '''</summary>
    <TestMethod()> _
    Public Sub LogChangeStepStateTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogChangeStepState(taskId, userName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogChangeExpireDate
    '''</summary>
    <TestMethod()> _
    Public Sub LogChangeExpireDateTest1()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim state As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogChangeExpireDate(taskID, taskName, docTypeId, docTypeName, folderId, stepId, stepName, state, userName, workflowId, workflowName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogChangeExpireDate
    '''</summary>
    <TestMethod()> _
    Public Sub LogChangeExpireDateTest()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expireDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogChangeExpireDate(taskID, expireDate, userName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogAsignedTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogAsignedTaskTest2()
        Dim taskID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim taskName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim state As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim userName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogAsignedTask(taskID, taskName, docTypeId, docTypeName, folderId, stepId, stepName, state, userName, workflowId, workflowName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogAsignedTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogAsignedTaskTest1()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim asignedUserName As String = String.Empty ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogAsignedTask(taskId, asignedUserName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LogAsignedTask
    '''</summary>
    <TestMethod()> _
    Public Sub LogAsignedTaskTest()
        Dim task As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.LogAsignedTask(task)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertTasks
    '''</summary>
    <TestMethod()> _
    Public Sub InsertTasksTest1()
        Dim Tasks As List(Of ITaskResult) = Nothing ' TODO: Initialize to an appropriate value
        Dim WFId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim StepId As Long = 0 ' TODO: Initialize to an appropriate value
        WFTasksFactory.InsertTasks(Tasks, WFId, StepId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertTasks
    '''</summary>
    <TestMethod()> _
    Public Sub InsertTasksTest()
        Dim Tasks As List(Of ITaskResult) = Nothing ' TODO: Initialize to an appropriate value
        Dim WF As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim p_StepId As Long = 0 ' TODO: Initialize to an appropriate value
        WFTasksFactory.InsertTasks(Tasks, WF, p_StepId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertTask
    '''</summary>
    <TestMethod()> _
    Public Sub InsertTaskTest()
        Dim T As ITaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim WFId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim StepId As Long = 0 ' TODO: Initialize to an appropriate value
        WFTasksFactory.InsertTask(T, WFId, StepId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetWFTASKANDUSERASIGNED
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFTASKANDUSERASIGNEDTest()
        Dim expected As DsTasksUsers = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsTasksUsers
        actual = WFTasksFactory.GetWFTASKANDUSERASIGNED
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserTasks
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserTasksTest()
        Dim WFs() As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DsDocuments = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocuments
        actual = WFTasksFactory.GetUserTasks(WFs)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksToExpireGroupByUser
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksToExpireGroupByUserTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim FromHours As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ToHours As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTasksToExpireGroupByUser(workflowid, FromHours, ToHours)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksToExpireGroupByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksToExpireGroupByStepTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim FromHours As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ToHours As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTasksToExpireGroupByStep(workflowid, FromHours, ToHours)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksByWfIdAndStepId
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksByWfIdAndStepIdTest()
        Dim target As WFTasksFactory = New WFTasksFactory ' TODO: Initialize to an appropriate value
        Dim wfId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = target.GetTasksByWfIdAndStepId(wfId, stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksByWfId
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksByWfIdTest()
        Dim wfId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTasksByWfId(wfId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksBySteps
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksByStepsTest()
        Dim stepIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = WFTasksFactory.GetTasksBySteps(stepIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksByStepTest()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = WFTasksFactory.GetTasksByStep(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksBalanceGroupByWorkflow
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksBalanceGroupByWorkflowTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTasksBalanceGroupByWorkflow(workflowid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksBalanceGroupByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksBalanceGroupByStepTest()
        Dim stepid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTasksBalanceGroupByStep(stepid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksAverageTimeInSteps
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksAverageTimeInStepsTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Hashtable
        actual = WFTasksFactory.GetTasksAverageTimeInSteps(workflowid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksAverageTimeByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksAverageTimeByStepTest()
        Dim stepid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Hashtable
        actual = WFTasksFactory.GetTasksAverageTimeByStep(stepid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasks
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksTest2()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = WFTasksFactory.GetTasks(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasks
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksTest1()
        Dim taskIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTasks(taskIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasks
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksTest()
        Dim WF As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DsDocuments = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocuments
        actual = WFTasksFactory.GetTasks(WF)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTaskHistoryByResultId
    '''</summary>
    <TestMethod()> _
    Public Sub GetTaskHistoryByResultIdTest()
        Dim TaskId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTaskHistoryByResultId(TaskId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTaskConsumedMinutesByWorkflowGroupByUsers
    '''</summary>
    <TestMethod()> _
    Public Sub GetTaskConsumedMinutesByWorkflowGroupByUsersTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTaskConsumedMinutesByWorkflowGroupByUsers(workflowid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTaskConsumedMinutesByStepGroupByUsers
    '''</summary>
    <TestMethod()> _
    Public Sub GetTaskConsumedMinutesByStepGroupByUsersTest()
        Dim stepid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTaskConsumedMinutesByStepGroupByUsers(stepid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTaskById
    '''</summary>
    <TestMethod()> _
    Public Sub GetTaskByIdTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsDocuments = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocuments
        actual = WFTasksFactory.GetTaskById(taskId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTaskAprobementsHistoryByTaskId
    '''</summary>
    <TestMethod()> _
    Public Sub GetTaskAprobementsHistoryByTaskIdTest()
        Dim TaskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTaskAprobementsHistoryByTaskId(TaskId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTask
    '''</summary>
    <TestMethod()> _
    Public Sub GetTaskTest()
        Dim taskId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetTask(taskId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexTypeByName
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexTypeByNameTest()
        Dim indexname As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Short = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Short
        actual = WFTasksFactory.GetIndexTypeByName(indexname)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexDropDownType
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexDropDownTypeTest()
        Dim indexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Short = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Short
        actual = WFTasksFactory.GetIndexDropDownType(indexId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetExpiredTasksGroupByUser
    '''</summary>
    <TestMethod()> _
    Public Sub GetExpiredTasksGroupByUserTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetExpiredTasksGroupByUser(workflowid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetExpiredTasksGroupByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetExpiredTasksGroupByStepTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetExpiredTasksGroupByStep(workflowid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocId
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocIdTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = WFTasksFactory.GetDocId(taskId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAsignedTasksCountsGroupByUser
    '''</summary>
    <TestMethod()> _
    Public Sub GetAsignedTasksCountsGroupByUserTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetAsignedTasksCountsGroupByUser(workflowid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAsignedTasksCountsGroupByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetAsignedTasksCountsGroupByStepTest()
        Dim workflowid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFTasksFactory.GetAsignedTasksCountsGroupByStep(workflowid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As WFTasksFactory = New WFTasksFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest1()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        WFTasksFactory.Delete(taskId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.Delete(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CountFilesInIP_Task
    '''</summary>
    <TestMethod()> _
    Public Sub CountFilesInIP_TaskTest()
        Dim conf_Id As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = WFTasksFactory.CountFilesInIP_Task(conf_Id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CompleteTask
    '''</summary>
    <TestMethod()> _
    Public Sub CompleteTaskTest()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFTasksFactory.CompleteTask(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddResultsToWorkFLowTask
    '''</summary>
    <TestMethod()> _
    Public Sub AddResultsToWorkFLowTaskTest()
        Dim Results As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim WfId As Integer = 0 ' TODO: Initialize to an appropriate value
        WFTasksFactory.AddResultsToWorkFLowTask(Results, WfId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for WFTasksFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub WFTasksFactoryConstructorTest()
        Dim target As WFTasksFactory = New WFTasksFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
