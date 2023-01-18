Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for WFStepStatesFactoryTest and is intended
'''to contain all WFStepStatesFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WFStepStatesFactoryTest


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
    '''A test for UpdateState
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateStateTest()
        Dim columnName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim value As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        WFStepStatesFactory.UpdateState(columnName, value, id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetStateInitial
    '''</summary>
    <TestMethod()> _
    Public Sub SetStateInitialTest()
        Dim State As WFStep.State = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstepExpected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        WFStepStatesFactory.SetStateInitial(State, wfstep)
        Assert.AreEqual(wfstepExpected, wfstep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveState
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveStateTest()
        Dim State As WFStep.State = Nothing ' TODO: Initialize to an appropriate value
        WFStepStatesFactory.RemoveState(State)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for IsStepStateMemberOfStep
    '''</summary>
    <TestMethod()> _
    Public Sub IsStepStateMemberOfStepTest()
        Dim StepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim StepStateId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = WFStepStatesFactory.IsStepStateMemberOfStep(StepId, StepStateId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepsStates
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsStatesTest()
        Dim StepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsStepsTask = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsStepsTask
        actual = WFStepStatesFactory.GetStepsStates(StepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStateNameByStateId
    '''</summary>
    <TestMethod()> _
    Public Sub GetStateNameByStateIdTest()
        Dim StateId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFStepStatesFactory.GetStateNameByStateId(StateId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStateName
    '''</summary>
    <TestMethod()> _
    Public Sub GetStateNameTest()
        Dim StateId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DsStepsStates As DsStepsTask = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFStepStatesFactory.GetStateName(StateId, DsStepsStates)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetName
    '''</summary>
    <TestMethod()> _
    Public Sub GetNameTest()
        Dim StateId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFStepStatesFactory.GetName(StateId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllStates
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllStatesTest()
        Dim DsSteps As DsSteps = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DsStepState = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsStepState
        actual = WFStepStatesFactory.GetAllStates(DsSteps)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllByStepId
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllByStepIdTest()
        Dim StepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsStepState = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsStepState
        actual = WFStepStatesFactory.GetAllByStepId(StepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for FillState
    '''</summary>
    <TestMethod()> _
    Public Sub FillStateTest()
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstepExpected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim ds As DsStepState = Nothing ' TODO: Initialize to an appropriate value
        WFStepStatesFactory.FillState(wfstep, ds)
        Assert.AreEqual(wfstepExpected, wfstep)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As WFStepStatesFactory = New WFStepStatesFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddState
    '''</summary>
    <TestMethod()> _
    Public Sub AddStateTest1()
        Dim NewState As WFStep.State = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim wfstepExpected As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFStepStatesFactory.AddState(NewState, wfstep)
        Assert.AreEqual(wfstepExpected, wfstep)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddState
    '''</summary>
    <TestMethod()> _
    Public Sub AddStateTest()
        Dim Doc_State_id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Description As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Initial As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim StepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFStepStatesFactory.AddState(Doc_State_id, Description, Name, Initial, StepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for WFStepStatesFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub WFStepStatesFactoryConstructorTest()
        Dim target As WFStepStatesFactory = New WFStepStatesFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
