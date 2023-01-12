Imports System.Collections.Generic

Imports System.Collections

Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for WFFactoryTest and is intended
'''to contain all WFFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WFFactoryTest


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
    '''A test for ZWFIInsert
    '''</summary>
    <TestMethod()> _
    Public Sub ZWFIInsertTest()
        Dim _WI As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _DTID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _RuleID As Long = 0 ' TODO: Initialize to an appropriate value
        WFFactory.ZWFIInsert(_WI, _DTID, _RuleID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ZWFIIInsert
    '''</summary>
    <TestMethod()> _
    Public Sub ZWFIIInsertTest()
        Dim _WI As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _IID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _IValue As String = String.Empty ' TODO: Initialize to an appropriate value
        WFFactory.ZWFIIInsert(_WI, _IID, _IValue)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ValidateDocIdInWF
    '''</summary>
    <TestMethod()> _
    Public Sub ValidateDocIdInWFTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim wfid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = WFFactory.ValidateDocIdInWF(DocId, wfid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for setRulesPreferencesForStatesUsersOrGroups
    '''</summary>
    <TestMethod()> _
    Public Sub setRulesPreferencesForStatesUsersOrGroupsTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ruleSectionId As RuleSectionOptions = New RuleSectionOptions ' TODO: Initialize to an appropriate value
        Dim objId As RulePreferences = New RulePreferences ' TODO: Initialize to an appropriate value
        Dim idCollectionDisabled As List(Of Integer) = Nothing ' TODO: Initialize to an appropriate value
        Dim idCollectionDisabledExpected As List(Of Integer) = Nothing ' TODO: Initialize to an appropriate value
        WFFactory.setRulesPreferencesForStatesUsersOrGroups(ruleId, ruleSectionId, objId, idCollectionDisabled)
        Assert.AreEqual(idCollectionDisabledExpected, idCollectionDisabled)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for setRulesPreferencesFor_StatesAndUsers_Or_StatesAndGroups
    '''</summary>
    <TestMethod()> _
    Public Sub setRulesPreferencesFor_StatesAndUsers_Or_StatesAndGroupsTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ruleSectionId As RuleSectionOptions = New RuleSectionOptions ' TODO: Initialize to an appropriate value
        Dim objId As RulePreferences = New RulePreferences ' TODO: Initialize to an appropriate value
        Dim stateId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim idCollectionDisabled As List(Of Integer) = Nothing ' TODO: Initialize to an appropriate value
        Dim idCollectionDisabledExpected As List(Of Integer) = Nothing ' TODO: Initialize to an appropriate value
        WFFactory.setRulesPreferencesFor_StatesAndUsers_Or_StatesAndGroups(ruleId, ruleSectionId, objId, stateId, idCollectionDisabled)
        Assert.AreEqual(idCollectionDisabledExpected, idCollectionDisabled)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetRulesPreferences
    '''</summary>
    <TestMethod()> _
    Public Sub SetRulesPreferencesTest1()
        Dim ruleid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RuleSectionid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Objid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Objvalue As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ObjOperator As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Object
        actual = WFFactory.SetRulesPreferences(ruleid, RuleSectionid, Objid, Objvalue, ObjOperator)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SetRulesPreferences
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub SetRulesPreferencesTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ruleIdExpected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RuleSectionId As RuleSectionOptions = New RuleSectionOptions ' TODO: Initialize to an appropriate value
        Dim RuleSectionIdExpected As RuleSectionOptions = New RuleSectionOptions ' TODO: Initialize to an appropriate value
        Dim ObjId As RulePreferences = New RulePreferences ' TODO: Initialize to an appropriate value
        Dim ObjIdExpected As RulePreferences = New RulePreferences ' TODO: Initialize to an appropriate value
        Dim idState As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim idStateExpected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim idCollectionDisabled As List(Of Integer) = Nothing ' TODO: Initialize to an appropriate value
        Dim idCollectionDisabledExpected As List(Of Integer) = Nothing ' TODO: Initialize to an appropriate value
        WFFactory_Accessor.SetRulesPreferences(ruleId, RuleSectionId, ObjId, idState, idCollectionDisabled)
        Assert.AreEqual(ruleIdExpected, ruleId)
        Assert.AreEqual(RuleSectionIdExpected, RuleSectionId)
        Assert.AreEqual(ObjIdExpected, ObjId)
        Assert.AreEqual(idStateExpected, idState)
        Assert.AreEqual(idCollectionDisabledExpected, idCollectionDisabled)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveWfInterval
    '''</summary>
    <TestMethod()> _
    Public Sub SaveWfIntervalTest()
        Dim Wfid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim interval As Integer = 0 ' TODO: Initialize to an appropriate value
        WFFactory.SaveWfInterval(Wfid, interval)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveWfInitialStep
    '''</summary>
    <TestMethod()> _
    Public Sub SaveWfInitialStepTest()
        Dim Wfid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim InitialStepId As Integer = 0 ' TODO: Initialize to an appropriate value
        WFFactory.SaveWfInitialStep(Wfid, InitialStepId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveWfChanges
    '''</summary>
    <TestMethod()> _
    Public Sub SaveWfChangesTest()
        Dim DsWf As DsWF = Nothing ' TODO: Initialize to an appropriate value
        WFFactory.SaveWfChanges(DsWf)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveNewName
    '''</summary>
    <TestMethod()> _
    Public Sub SaveNewNameTest()
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Work_Id As Integer = 0 ' TODO: Initialize to an appropriate value
        WFFactory.SaveNewName(Name, Work_Id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for saveItemSelectedThatCanBe_StateOrUserOrGrupo
    '''</summary>
    <TestMethod()> _
    Public Sub saveItemSelectedThatCanBe_StateOrUserOrGrupoTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ruleSectionId As RuleSectionOptions = New RuleSectionOptions ' TODO: Initialize to an appropriate value
        Dim objId As RulePreferences = New RulePreferences ' TODO: Initialize to an appropriate value
        WFFactory.saveItemSelectedThatCanBe_StateOrUserOrGrupo(ruleId, ruleSectionId, objId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveWorkFlow
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveWorkFlowTest()
        Dim wf_id As Integer = 0 ' TODO: Initialize to an appropriate value
        WFFactory.RemoveWorkFlow(wf_id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for removeOldItemsThatWereDisabled
    '''</summary>
    <TestMethod()> _
    Public Sub removeOldItemsThatWereDisabledTest()
        Dim ruleid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RuleSectionid As RuleSectionOptions = New RuleSectionOptions ' TODO: Initialize to an appropriate value
        Dim Objid As RulePreferences = New RulePreferences ' TODO: Initialize to an appropriate value
        WFFactory.removeOldItemsThatWereDisabled(ruleid, RuleSectionid, Objid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for recoverUsers_Or_Groups_belongingToAState
    '''</summary>
    <TestMethod()> _
    Public Sub recoverUsers_Or_Groups_belongingToAStateTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ruleSectionId As RuleSectionOptions = New RuleSectionOptions ' TODO: Initialize to an appropriate value
        Dim objId As RulePreferences = New RulePreferences ' TODO: Initialize to an appropriate value
        Dim stateId As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFFactory.recoverUsers_Or_Groups_belongingToAState(ruleId, ruleSectionId, objId, stateId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for recoverItemSelectedThatCanBe_StateOrUserOrGroup
    '''</summary>
    <TestMethod()> _
    Public Sub recoverItemSelectedThatCanBe_StateOrUserOrGroupTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As RulePreferences = New RulePreferences ' TODO: Initialize to an appropriate value
        Dim actual As RulePreferences
        actual = WFFactory.recoverItemSelectedThatCanBe_StateOrUserOrGroup(ruleId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWorkflowsAndTaskCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetWorkflowsAndTaskCountTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFFactory.GetWorkflowsAndTaskCount
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWorkflows
    '''</summary>
    <TestMethod()> _
    Public Sub GetWorkflowsTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFFactory.GetWorkflows
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWorkflowNameByWFId
    '''</summary>
    <TestMethod()> _
    Public Sub GetWorkflowNameByWFIdTest()
        Dim workId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFFactory.GetWorkflowNameByWFId(workId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWorkflowIdByStepId
    '''</summary>
    <TestMethod()> _
    Public Sub GetWorkflowIdByStepIdTest()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = WFFactory.GetWorkflowIdByStepId(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFToAddDocuments
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFToAddDocumentsTest()
        Dim wfid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As WorkFlow
        actual = WFFactory.GetWFToAddDocuments(wfid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFsToAddDocuments
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFsToAddDocumentsTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = WFFactory.GetWFsToAddDocuments
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFStepsDetails
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFStepsDetailsTest()
        Dim expected As DsWFStepDetails = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsWFStepDetails
        actual = WFFactory.GetWFStepsDetails
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFsByUserRightsEDIT
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFsByUserRightsEDITTest()
        Dim groupsIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DsWF = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsWF
        actual = WFFactory.GetWFsByUserRightsEDIT(groupsIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFsByUserRightMONITORING
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFsByUserRightMONITORINGTest()
        Dim groupsIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DsWF = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsWF
        actual = WFFactory.GetWFsByUserRightMONITORING(groupsIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFsByDocType
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFsByDocTypeTest()
        Dim expected As DsWFsByDocType = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsWFsByDocType
        actual = WFFactory.GetWFsByDocType
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFs
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFsTest1()
        Dim expected() As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As WorkFlow
        actual = WFFactory.GetWFs
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFs
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFsTest()
        Dim UserID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsWF = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsWF
        actual = WFFactory.GetWFs(UserID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWfNameById
    '''</summary>
    <TestMethod()> _
    Public Sub GetWfNameByIdTest()
        Dim WfId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFFactory.GetWfNameById(WfId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWfByIdAsDataSet
    '''</summary>
    <TestMethod()> _
    Public Sub GetWfByIdAsDataSetTest()
        Dim WfId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFFactory.GetWfByIdAsDataSet(WfId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWfById
    '''</summary>
    <TestMethod()> _
    Public Sub GetWfByIdTest()
        Dim WfId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsWF = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsWF
        actual = WFFactory.GetWfById(WfId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWf
    '''</summary>
    <TestMethod()> _
    Public Sub GetWfTest()
        Dim r As DsWF.WFRow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As WorkFlow
        actual = WFFactory.GetWf(r)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepsByWF
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsByWFTest()
        Dim expected As DStepsByWorkflow = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DStepsByWorkflow
        actual = WFFactory.GetStepsByWF
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetSteps
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsTest()
        Dim expected As DStepsByWorkflow = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DStepsByWorkflow
        actual = WFFactory.GetSteps
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStateOfHabilitationOfState
    '''</summary>
    <TestMethod()> _
    Public Sub GetStateOfHabilitationOfStateTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim stateId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = WFFactory.GetStateOfHabilitationOfState(ruleId, stateId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRulesPreferences
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesPreferencesTest()
        Dim ruleid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RuleSectionid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Objid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFFactory.GetRulesPreferences(ruleid, RuleSectionid, Objid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRulesByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesByStepTest()
        Dim expected As DsRulesByStep = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRulesByStep
        actual = WFFactory.GetRulesByStep
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetFullsUserWFs
    '''</summary>
    <TestMethod()> _
    Public Sub GetFullsUserWFsTest()
        Dim expected() As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As WorkFlow
        actual = WFFactory.GetFullsUserWFs
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetExpired
    '''</summary>
    <TestMethod()> _
    Public Sub GetExpiredTest()
        Dim expected As DsExpiredDocs = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsExpiredDocs
        actual = WFFactory.GetExpired
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocumentsByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocumentsByStepTest()
        Dim expected As DsDocsByStep = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocsByStep
        actual = WFFactory.GetDocumentsByStep
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocsByWF
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocsByWFTest()
        Dim expected As DsDocsByWF = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocsByWF
        actual = WFFactory.GetDocsByWF
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDelayed
    '''</summary>
    <TestMethod()> _
    Public Sub GetDelayedTest()
        Dim expected As DsDelayedDocs = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDelayedDocs
        actual = WFFactory.GetDelayed
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllUsers
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllUsersTest()
        Dim expected As SortedList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As SortedList
        actual = WFFactory.GetAllUsers
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllGroups
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllGroupsTest()
        Dim expected As SortedList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As SortedList
        actual = WFFactory.GetAllGroups
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for fillBalanceByWF
    '''</summary>
    <TestMethod()> _
    Public Sub fillBalanceByWFTest()
        Dim work_Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFFactory.fillBalanceByWF(work_Id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AsignedDocsByUser
    '''</summary>
    <TestMethod()> _
    Public Sub AsignedDocsByUserTest()
        Dim expected As DsDocsByUser = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocsByUser
        actual = WFFactory.AsignedDocsByUser
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for WFFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub WFFactoryConstructorTest()
        Dim target As WFFactory = New WFFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
