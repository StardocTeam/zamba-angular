Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for AutoSubstitutionDataFactoryTest and is intended
'''to contain all AutoSubstitutionDataFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class AutoSubstitutionDataFactoryTest


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
    '''A test for RemoveItem
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveItemTest()
        Dim code As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexId As Integer = 0 ' TODO: Initialize to an appropriate value
        AutoSubstitutionDataFactory.RemoveItem(code, indexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertIntoIListAsBoolean
    '''</summary>
    <TestMethod()> _
    Public Sub InsertIntoIListAsBooleanTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim linea As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = AutoSubstitutionDataFactory.InsertIntoIListAsBoolean(IndexId, linea)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for InsertIntoIList
    '''</summary>
    <TestMethod()> _
    Public Sub InsertIntoIListTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim linea As String = String.Empty ' TODO: Initialize to an appropriate value
        AutoSubstitutionDataFactory.InsertIntoIList(IndexId, linea)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetTable
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetTableTest()
        Dim indexid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = AutoSubstitutionDataFactory_Accessor.GetTable(indexid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetItemExist
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetItemExistTest()
        Dim _indexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _linea As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = AutoSubstitutionDataFactory_Accessor.GetItemExist(_indexId, _linea)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexData
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexDataTest()
        Dim Indexid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Reload As Boolean = False ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = AutoSubstitutionDataFactory.GetIndexData(Indexid, Reload)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getDescription
    '''</summary>
    <TestMethod()> _
    Public Sub getDescriptionTest()
        Dim Code As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = AutoSubstitutionDataFactory.getDescription(Code, IndexId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddItems
    '''</summary>
    <TestMethod()> _
    Public Sub AddItemsTest()
        Dim sustitutionItem As SustitutionItem = Nothing ' TODO: Initialize to an appropriate value
        Dim indexId As Integer = 0 ' TODO: Initialize to an appropriate value
        AutoSubstitutionDataFactory.AddItems(sustitutionItem, indexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AutoSubstitutionDataFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub AutoSubstitutionDataFactoryConstructorTest()
        Dim target As AutoSubstitutionDataFactory = New AutoSubstitutionDataFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
