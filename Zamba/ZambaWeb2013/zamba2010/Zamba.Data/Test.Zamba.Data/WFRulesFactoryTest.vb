Imports System.Collections.Generic

Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for WFRulesFactoryTest and is intended
'''to contain all WFRulesFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WFRulesFactoryTest


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
    '''A test for UpdateRuleNameByID
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateRuleNameByIDTest()
        Dim Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        WFRulesFactory.UpdateRuleNameByID(Id, Name)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateRuleName
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateRuleNameTest()
        Dim rule As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim ruleExpected As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        WFRulesFactory.UpdateRuleName(rule)
        Assert.AreEqual(ruleExpected, rule)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateRuleById
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateRuleByIdTest()
        Dim p_iRuleId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim p_bEstado As Boolean = False ' TODO: Initialize to an appropriate value
        WFRulesFactory.UpdateRuleById(p_iRuleId, p_bEstado)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateRule
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateRuleTest()
        Dim rule As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim ruleExpected As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim ParentId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ParentType As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim StepId As Integer = 0 ' TODO: Initialize to an appropriate value
        WFRulesFactory.UpdateRule(rule, ParentId, ParentType, StepId)
        Assert.AreEqual(ruleExpected, rule)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateParentRuleId
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateParentRuleIdTest()
        Dim RuleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ParentRuleID As Long = 0 ' TODO: Initialize to an appropriate value
        WFRulesFactory.UpdateParentRuleId(RuleID, ParentRuleID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateParamItem
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateParamItemTest1()
        Dim RuleAction As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim Item As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim carp As Boolean = False ' TODO: Initialize to an appropriate value
        WFRulesFactory.UpdateParamItem(RuleAction, Item, Value, carp)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateParamItem
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateParamItemTest()
        Dim RuleActionId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Item As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim carp As Boolean = False ' TODO: Initialize to an appropriate value
        WFRulesFactory.UpdateParamItem(RuleActionId, Item, Value, carp)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetFloatingRule
    '''</summary>
    <TestMethod()> _
    Public Sub SetFloatingRuleTest()
        Dim rule As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim ruleExpected As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        WFRulesFactory.SetFloatingRule(rule)
        Assert.AreEqual(ruleExpected, rule)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertUsersToNotifyAboutRuleExecution
    '''</summary>
    <TestMethod()> _
    Public Sub InsertUsersToNotifyAboutRuleExecutionTest()
        Dim ruleid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RuleSectionId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _RulePreferenceid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DestTypeid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim items As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        WFRulesFactory.InsertUsersToNotifyAboutRuleExecution(ruleid, RuleSectionId, _RulePreferenceid, DestTypeid, items)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertRuleParamItem
    '''</summary>
    <TestMethod()> _
    Public Sub InsertRuleParamItemTest1()
        Dim ZConditionParam As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim Item As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
        WFRulesFactory.InsertRuleParamItem(ZConditionParam, Item, Value)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertRuleParamItem
    '''</summary>
    <TestMethod()> _
    Public Sub InsertRuleParamItemTest()
        Dim RuleId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Item As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
        WFRulesFactory.InsertRuleParamItem(RuleId, Item, Value)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertRule
    '''</summary>
    <TestMethod()> _
    Public Sub InsertRuleTest1()
        Dim id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim WfStepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim RuleType As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ParentRuleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ParentType As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim RuleClass As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Enable As Boolean = False ' TODO: Initialize to an appropriate value
        Dim Version As Integer = 0 ' TODO: Initialize to an appropriate value
        WFRulesFactory.InsertRule(id, name, WfStepId, RuleType, ParentRuleId, ParentType, RuleClass, Enable, Version)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertRule
    '''</summary>
    <TestMethod()> _
    Public Sub InsertRuleTest()
        Dim ID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ParentRule As IRule = Nothing ' TODO: Initialize to an appropriate value
        Dim WFStepID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim RuleType As TypesofRules = New TypesofRules ' TODO: Initialize to an appropriate value
        Dim ParentType As TypesofRules = New TypesofRules ' TODO: Initialize to an appropriate value
        Dim RuleClass As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Enable As Boolean = False ' TODO: Initialize to an appropriate value
        Dim Version As Integer = 0 ' TODO: Initialize to an appropriate value
        WFRulesFactory.InsertRule(ID, ParentRule, WFStepID, Name, RuleType, ParentType, RuleClass, Enable, Version)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetUsersToNotifyAboutRuleExecution
    '''</summary>
    <TestMethod()> _
    Public Sub GetUsersToNotifyAboutRuleExecutionTest()
        Dim ruleid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RuleSectionid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Objid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ObjFieldSourceId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFRulesFactory.GetUsersToNotifyAboutRuleExecution(ruleid, RuleSectionid, Objid, ObjFieldSourceId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRuleStateById
    '''</summary>
    <TestMethod()> _
    Public Sub GetRuleStateByIdTest()
        Dim p_iRuleId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = WFRulesFactory.GetRuleStateById(p_iRuleId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRulesIdByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesIdByStepTest1()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = WFRulesFactory.GetRulesIdByStep(stepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRulesIdByStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesIdByStepTest()
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ruleTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = WFRulesFactory.GetRulesIdByStep(stepId, ruleTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRulesByWFStepID
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesByWFStepIDTest()
        Dim WFStepID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRulesByWFStepID(WFStepID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRulesByStepId
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesByStepIdTest()
        Dim StepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRulesByStepId(StepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRulesAsDsRulesID
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesAsDsRulesIDTest()
        Dim StepID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRulesAsDsRulesID(StepID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRulesAsDsRules
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesAsDsRulesTest()
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRulesAsDsRules
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRulesAsDataSet
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesAsDataSetTest()
        Dim Restriction As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFRulesFactory.GetRulesAsDataSet(Restriction)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRules
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesTest5()
        Dim WF As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRules(WF)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRules
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesTest4()
        Dim ruleIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = WFRulesFactory.GetRules(ruleIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRules
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesTest3()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = WFRulesFactory.GetRules
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRules
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesTest2()
        Dim WFs() As WorkFlow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRules(WFs)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRules
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesTest1()
        Dim WFStep As WFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRules(WFStep)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRules
    '''</summary>
    <TestMethod()> _
    Public Sub GetRulesTest()
        Dim _workFlowID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Restriction As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRules(_workFlowID, Restriction)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRuleParamItems
    '''</summary>
    <TestMethod()> _
    Public Sub GetRuleParamItemsTest()
        Dim p_iRuleID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRuleParamItems(p_iRuleID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRuleNameById
    '''</summary>
    <TestMethod()> _
    Public Sub GetRuleNameByIdTest()
        Dim p_iRuleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFRulesFactory.GetRuleNameById(p_iRuleId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRuleClass
    '''</summary>
    <TestMethod()> _
    Public Sub GetRuleClassTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = WFRulesFactory.GetRuleClass(ruleId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRuleByID
    '''</summary>
    <TestMethod()> _
    Public Sub GetRuleByIDTest()
        Dim p_iRuleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsRules = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsRules
        actual = WFRulesFactory.GetRuleByID(p_iRuleID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRule
    '''</summary>
    <TestMethod()> _
    Public Sub GetRuleTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = WFRulesFactory.GetRule(ruleId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteRuleByID
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteRuleByIDTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        WFRulesFactory.DeleteRuleByID(id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteRule
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteRuleTest()
        Dim rule As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim ruleExpected As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        WFRulesFactory.DeleteRule(rule)
        Assert.AreEqual(ruleExpected, rule)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AttachAFloatingRule
    '''</summary>
    <TestMethod()> _
    Public Sub AttachAFloatingRuleTest()
        Dim rule As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim ruleExpected As WFRuleParent = Nothing ' TODO: Initialize to an appropriate value
        Dim ParentId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ParentType As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim StepId As Integer = 0 ' TODO: Initialize to an appropriate value
        WFRulesFactory.AttachAFloatingRule(rule, ParentId, ParentType, StepId)
        Assert.AreEqual(ruleExpected, rule)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for WFRulesFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub WFRulesFactoryConstructorTest()
        Dim target As WFRulesFactory = New WFRulesFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
