Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for SectionFactoryTest and is intended
'''to contain all SectionFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SectionFactoryTest


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
    '''A test for UpdateDocGroup
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateDocGroupTest()
        Dim DocGroupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocGroupName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim DocGroupParentId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DocGroupIcon As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsDocGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocGroup
        actual = SectionFactory.UpdateDocGroup(DocGroupId, DocGroupName, DocGroupParentId, DocGroupIcon)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for RemoveDocType
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveDocTypeTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DocGroupId As Long = 0 ' TODO: Initialize to an appropriate value
        SectionFactory.RemoveDocType(DocTypeId, DocGroupId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LoadDocTypes
    '''</summary>
    <TestMethod()> _
    Public Sub LoadDocTypesTest()
        Dim DocGroupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = SectionFactory.LoadDocTypes(DocGroupId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocGroups
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocGroupsTest1()
        Dim expected As DsDocGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocGroup
        actual = SectionFactory.GetDocGroups
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocGroups
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocGroupsTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsDocGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocGroup
        actual = SectionFactory.GetDocGroups(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocGroupChilds
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocGroupChildsTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = SectionFactory.GetDocGroupChilds(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DocGroupIsDuplicated
    '''</summary>
    <TestMethod()> _
    Public Sub DocGroupIsDuplicatedTest()
        Dim DocGroupName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = SectionFactory.DocGroupIsDuplicated(DocGroupName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DocGroupHasSubGroups
    '''</summary>
    <TestMethod()> _
    Public Sub DocGroupHasSubGroupsTest()
        Dim DocGroupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = SectionFactory.DocGroupHasSubGroups(DocGroupId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DocGroupHasAsigned
    '''</summary>
    <TestMethod()> _
    Public Sub DocGroupHasAsignedTest()
        Dim DocGroupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = SectionFactory.DocGroupHasAsigned(DocGroupId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DelDocGroup
    '''</summary>
    <TestMethod()> _
    Public Sub DelDocGroupTest()
        Dim DocGroupId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsDocGroup = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsDocGroup
        actual = SectionFactory.DelDocGroup(DocGroupId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AsignDocType
    '''</summary>
    <TestMethod()> _
    Public Sub AsignDocTypeTest()
        Dim Orden As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DocGroupId As Long = 0 ' TODO: Initialize to an appropriate value
        SectionFactory.AsignDocType(Orden, DocTypeId, DocGroupId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddDocGroup
    '''</summary>
    <TestMethod()> _
    Public Sub AddDocGroupTest()
        Dim DocGroupName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim DocGroupParentId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocGroupIcon As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = SectionFactory.AddDocGroup(DocGroupName, DocGroupParentId, DocGroupIcon)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SectionFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub SectionFactoryConstructorTest()
        Dim target As SectionFactory = New SectionFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
