Imports System.Collections.Generic

Imports System.Collections

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for FormFactoryTest and is intended
'''to contain all FormFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class FormFactoryTest


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
    '''A test for UpdateForm
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateFormTest()
        Dim Form As ZwebForm = Nothing ' TODO: Initialize to an appropriate value
        FormFactory.UpdateForm(Form)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertForm
    '''</summary>
    <TestMethod()> _
    Public Sub InsertFormTest()
        Dim Form As ZwebForm = Nothing ' TODO: Initialize to an appropriate value
        FormFactory.InsertForm(Form)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetWFForms
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFFormsTest1()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim WfStepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim FormType As FormTypes = New FormTypes ' TODO: Initialize to an appropriate value
        Dim expected() As ZwebForm = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As ZwebForm
        actual = FormFactory.GetWFForms(DocTypeId, WfStepId, FormType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFForms
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFFormsTest()
        Dim WfStepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected() As ZwebForm = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As ZwebForm
        actual = FormFactory.GetWFForms(WfStepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVirtualDocumentsByRightsOfView
    '''</summary>
    <TestMethod()> _
    Public Sub GetVirtualDocumentsByRightsOfViewTest()
        Dim Formtype As FormTypes = New FormTypes ' TODO: Initialize to an appropriate value
        Dim userid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim doctypesByRights As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = FormFactory.GetVirtualDocumentsByRightsOfView(Formtype, userid, doctypesByRights)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetShowAndEditFormsCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetShowAndEditFormsCountTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = FormFactory.GetShowAndEditFormsCount(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetShowAndEditForms
    '''</summary>
    <TestMethod()> _
    Public Sub GetShowAndEditFormsTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected() As ZwebForm = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As ZwebForm
        actual = FormFactory.GetShowAndEditForms(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetFormsByType
    '''</summary>
    <TestMethod()> _
    Public Sub GetFormsByTypeTest()
        Dim Formtype As FormTypes = New FormTypes ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = FormFactory.GetFormsByType(Formtype)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetForms
    '''</summary>
    <TestMethod()> _
    Public Sub GetFormsTest1()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = FormFactory.GetForms
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetForms
    '''</summary>
    <TestMethod()> _
    Public Sub GetFormsTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim FormType As FormTypes = New FormTypes ' TODO: Initialize to an appropriate value
        Dim expected() As ZwebForm = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As ZwebForm
        actual = FormFactory.GetForms(DocTypeId, FormType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetFormNewId
    '''</summary>
    <TestMethod()> _
    Public Sub GetFormNewIdTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = FormFactory.GetFormNewId
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetForm
    '''</summary>
    <TestMethod()> _
    Public Sub GetFormTest()
        Dim formID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ZwebForm = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ZwebForm
        actual = FormFactory.GetForm(formID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As FormFactory = New FormFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteForm
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteFormTest()
        Dim Form As ZwebForm = Nothing ' TODO: Initialize to an appropriate value
        FormFactory.DeleteForm(Form)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CreateAutoForm
    '''</summary>
    <TestMethod()> _
    Public Sub CreateAutoFormTest()
        Dim DocType As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim Indexs() As Index = Nothing ' TODO: Initialize to an appropriate value
        Dim File As String = String.Empty ' TODO: Initialize to an appropriate value
        FormFactory.CreateAutoForm(DocType, Indexs, File)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AsignForm2WfStep
    '''</summary>
    <TestMethod()> _
    Public Sub AsignForm2WfStepTest()
        Dim Form As ZwebForm = Nothing ' TODO: Initialize to an appropriate value
        Dim WfStepId As Integer = 0 ' TODO: Initialize to an appropriate value
        FormFactory.AsignForm2WfStep(Form, WfStepId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for FormFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub FormFactoryConstructorTest()
        Dim target As FormFactory = New FormFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
