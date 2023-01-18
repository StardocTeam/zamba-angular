Imports System.Drawing

Imports System.Collections.Generic

Imports System.Data

Imports System.Collections

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for WFStepsFactoryTest and is intended
'''to contain all WFStepsFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WFStepsFactoryTest


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
    '''A test for UpdateStepPosition
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateStepPositionTest()
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstepExpected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.UpdateStepPosition(wfstep)
        Assert.AreEqual(wfstepExpected, wfstep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateStepColor
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateStepColorTest()
        Dim color As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim ID As Long = 0 ' TODO: Initialize to an appropriate value
        WFStepsFactory.UpdateStepColor(color, ID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateStep
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateStepTest()
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstepExpected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.UpdateStep(wfstep)
        Assert.AreEqual(wfstepExpected, wfstep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateInitialStep
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateInitialStepTest()
        Dim WFId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim InitialStepId As Long = 0 ' TODO: Initialize to an appropriate value
        WFStepsFactory.UpdateInitialStep(WFId, InitialStepId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateExpiredDateTask
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateExpiredDateTaskTest()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.UpdateExpiredDateTask(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for NewStep
    '''</summary>
    <TestMethod()> _
    Public Sub NewStepTest()
        Dim WF As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Help As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Description As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Location As Point = New Point ' TODO: Initialize to an appropriate value
        Dim ImageIndex As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim MaxDocs As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim MaxHours As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim StartAtOpenDoc As Boolean = False ' TODO: Initialize to an appropriate value
        Dim ImagePath As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As WFStep
        actual = WFStepsFactory.NewStep(WF, Name, Help, Description, Location, ImageIndex, MaxDocs, MaxHours, StartAtOpenDoc, ImagePath)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for InsertStep
    '''</summary>
    <TestMethod()> _
    Public Sub InsertStepTest1()
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.InsertStep(wfstep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertStep
    '''</summary>
    <TestMethod()> _
    Public Sub InsertStepTest()
        Dim WFID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Help As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Description As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Location As Point = New Point ' TODO: Initialize to an appropriate value
        Dim ImageIndex As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim MaxDocs As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim MaxHours As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Initial As Boolean = False ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = WFStepsFactory.InsertStep(WFID, Name, Help, Description, Location, ImageIndex, MaxDocs, MaxHours, Initial)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for InsertNewStep
    '''</summary>
    <TestMethod()> _
    Public Sub InsertNewStepTest()
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.InsertNewStep(wfstep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for HasDocuments
    '''</summary>
    <TestMethod()> _
    Public Sub HasDocumentsTest()
        Dim documentId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = WFStepsFactory.HasDocuments(documentId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetUserSteps
    '''</summary>
    <TestMethod()> _
    Public Sub GetUserStepsTest()
        Dim expected As SortedList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As SortedList
        actual = WFStepsFactory.GetUserSteps
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksCountAllStepsFULL
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksCountAllStepsFULLTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFStepsFactory.GetTasksCountAllStepsFULL
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksCountAllSteps
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksCountAllStepsTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFStepsFactory.GetTasksCountAllSteps
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksCountAllStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksCountAllStepTest()
        Dim expected As DsTasksUsers = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsTasksUsers
        actual = WFStepsFactory.GetTasksCountAllStep
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksCountTest()
        Dim StepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = WFStepsFactory.GetTasksCount(StepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTasksConsumedMinutes
    '''</summary>
    <TestMethod()> _
    Public Sub GetTasksConsumedMinutesTest()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = WFStepsFactory.GetTasksConsumedMinutes(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepsIdName
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsIdNameTest()
        Dim h As SortedList = Nothing ' TODO: Initialize to an appropriate value
        Dim hExpected As SortedList = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.GetStepsIdName(h)
        Assert.AreEqual(hExpected, h)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetStepsDs
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsDsTest()
        Dim stepIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFStepsFactory.GetStepsDs(stepIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepsDictionary
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsDictionaryTest()
        Dim WF As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As SortedList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As SortedList
        actual = WFStepsFactory.GetStepsDictionary(WF)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepsByWorkId
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsByWorkIdTest()
        Dim WfId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As SortedList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As SortedList
        actual = WFStepsFactory.GetStepsByWorkId(WfId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepsByWorkflows
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsByWorkflowsTest()
        Dim workflowIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFStepsFactory.GetStepsByWorkflows(workflowIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepsByWorkflow
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsByWorkflowTest()
        Dim workflowId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFStepsFactory.GetStepsByWorkflow(workflowId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetSteps
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsTest()
        Dim WF As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As SortedList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As SortedList
        actual = WFStepsFactory.GetSteps(WF)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepNameById
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepNameByIdTest()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFStepsFactory.GetStepNameById(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepIdByTaskId
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepIdByTaskIdTest()
        Dim TaskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = WFStepsFactory.GetStepIdByTaskId(TaskId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepById
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepByIdTest()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As IWFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IWFStep
        actual = WFStepsFactory.GetStepById(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepTest()
        Dim Wf As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim StepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As WFStep
        actual = WFStepsFactory.GetStep(Wf, StepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDsSteps
    '''</summary>
    <TestMethod()> _
    Public Sub GetDsStepsTest()
        Dim WFId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsSteps = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsSteps
        actual = WFStepsFactory.GetDsSteps(WFId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDsAllSteps
    '''</summary>
    <TestMethod()> _
    Public Sub GetDsAllStepsTest()
        Dim expected As DsSteps = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsSteps
        actual = WFStepsFactory.GetDsAllSteps
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocumentCountByStepId
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetDocumentCountByStepIdTest()
        Dim documentId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = WFStepsFactory_Accessor.GetDocumentCountByStepId(documentId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for FillUserSteps
    '''</summary>
    <TestMethod()> _
    Public Sub FillUserStepsTest()
        Dim WFs() As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.FillUserSteps(WFs)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for FillStepUsers
    '''</summary>
    <TestMethod()> _
    Public Sub FillStepUsersTest()
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstepExpected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim AllUsers As SortedList = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.FillStepUsers(wfstep, AllUsers)
        Assert.AreEqual(wfstepExpected, wfstep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for FillSteps
    '''</summary>
    <TestMethod()> _
    Public Sub FillStepsTest()
        Dim WF As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.FillSteps(WF)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for FillStepGroups
    '''</summary>
    <TestMethod()> _
    Public Sub FillStepGroupsTest()
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstepExpected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim AllGroups As SortedList = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.FillStepGroups(wfstep, AllGroups)
        Assert.AreEqual(wfstepExpected, wfstep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As WFStepsFactory = New WFStepsFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelStep
    '''</summary>
    <TestMethod()> _
    Public Sub DelStepTest()
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstepExpected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory.DelStep(wfstep)
        Assert.AreEqual(wfstepExpected, wfstep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CheckTransicions
    '''</summary>
    <TestMethod()> _
    Public Sub CheckTransicionsTest()
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstepExpected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = WFStepsFactory.CheckTransicions(wfstep)
        Assert.AreEqual(wfstepExpected, wfstep)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CheckRules
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub CheckRulesTest()
        Dim rule As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim ruleExpected As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim Destiny As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        WFStepsFactory_Accessor.CheckRules(rule, Destiny)
        Assert.AreEqual(ruleExpected, rule)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for WFStepsFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub WFStepsFactoryConstructorTest()
        Dim target As WFStepsFactory = New WFStepsFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
