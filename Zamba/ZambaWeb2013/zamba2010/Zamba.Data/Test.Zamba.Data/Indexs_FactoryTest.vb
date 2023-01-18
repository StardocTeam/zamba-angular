Imports Zamba.Core

Imports System.Data

Imports System.Collections

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for Indexs_FactoryTest and is intended
'''to contain all Indexs_FactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class Indexs_FactoryTest


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
    '''A test for UpdateListItem
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateListItemTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Item As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim NewItem As String = String.Empty ' TODO: Initialize to an appropriate value
        Indexs_Factory.UpdateListItem(IndexId, Item, NewItem)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateIndex
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateIndexTest()
        Dim IndexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim AutoDisplay1 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim NoIndex1 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DropDown As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Invisible1 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Indexs_Factory.UpdateIndex(IndexId, Name, AutoDisplay1, NoIndex1, DropDown, Invisible1)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ShowInLotusNotes
    '''</summary>
    <TestMethod()> _
    Public Sub ShowInLotusNotesTest()
        Dim Doctypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim indexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim dropDown As Integer = 0 ' TODO: Initialize to an appropriate value
        Indexs_Factory.ShowInLotusNotes(Doctypeid, indexId, dropDown)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetIndexUnique
    '''</summary>
    <TestMethod()> _
    Public Sub SetIndexUniqueTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Indexs_Factory.SetIndexUnique(DocTypeId, IndexId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SetIndexRequired
    '''</summary>
    <TestMethod()> _
    Public Sub SetIndexRequiredTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Indexs_Factory.SetIndexRequired(DocTypeId, IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for retrievetablelist
    '''</summary>
    <TestMethod()> _
    Public Sub retrievetablelistTest()
        Dim IndexID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = Indexs_Factory.retrievetablelist(IndexID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for retrievearraytablelist
    '''</summary>
    <TestMethod()> _
    Public Sub retrievearraytablelistTest()
        Dim IndexID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = Indexs_Factory.retrievearraytablelist(IndexID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for retrieveArraylist
    '''</summary>
    <TestMethod()> _
    Public Sub retrieveArraylistTest()
        Dim IndexID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = Indexs_Factory.retrieveArraylist(IndexID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for LoadIndexFindTypeValues
    '''</summary>
    <TestMethod()> _
    Public Sub LoadIndexFindTypeValuesTest()
        Dim IndexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = Indexs_Factory.LoadIndexFindTypeValues(IndexId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IsDuplicated
    '''</summary>
    <TestMethod()> _
    Public Sub IsDuplicatedTest()
        Dim IndexName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IndexID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Indexs_Factory.IsDuplicated(IndexName, IndexID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for InsertIndexList
    '''</summary>
    <TestMethod()> _
    Public Sub InsertIndexListTest()
        Dim indexid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexList As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Indexs_Factory.InsertIndexList(indexid, IndexList)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for IndexIsDuplicated
    '''</summary>
    <TestMethod()> _
    Public Sub IndexIsDuplicatedTest()
        Dim IndexName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Indexs_Factory.IndexIsDuplicated(IndexName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IndexIsAsigned
    '''</summary>
    <TestMethod()> _
    Public Sub IndexIsAsignedTest()
        Dim index As Index = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Indexs_Factory.IndexIsAsigned(index)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getTableList
    '''</summary>
    <TestMethod()> _
    Public Sub getTableListTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Indexs_Factory.getTableList(IndexId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTable
    '''</summary>
    <TestMethod()> _
    Public Sub GetTableTest()
        Dim indexid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Indexs_Factory.GetTable(indexid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexValues
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexValuesTest()
        Dim expected As DSIndex = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSIndex
        actual = Indexs_Factory.GetIndexValues
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexsSchema
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexsSchemaTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Indexs_Factory.GetIndexsSchema(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexSchemaAsDataSet
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexSchemaAsDataSetTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Indexs_Factory.GetIndexSchemaAsDataSet(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexSchema
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexSchemaTest1()
        Dim DocTypeIds As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DSIndex = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSIndex
        actual = Indexs_Factory.GetIndexSchema(DocTypeIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexSchema
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexSchemaTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DSIndex = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSIndex
        actual = Indexs_Factory.GetIndexSchema(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexNames
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexNamesTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = Indexs_Factory.GetIndexNames
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexName
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexNameTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Indexs_Factory.GetIndexName(IndexId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexId
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexIdTest()
        Dim IndexName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Indexs_Factory.GetIndexId(IndexName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexDsNames
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexDsNamesTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Indexs_Factory.GetIndexDsNames
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexByPublishStates
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexByPublishStatesTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Indexs_Factory.GetIndexByPublishStates(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexByIdDataSet
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexByIdDataSetTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Indexs_Factory.GetIndexByIdDataSet(id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexById
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexByIdTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DSIndex = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSIndex
        actual = Indexs_Factory.GetIndexById(id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndex
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexTest()
        Dim expected As DSIndex = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSIndex
        actual = Indexs_Factory.GetIndex
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocIndexByIndexName
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocIndexByIndexNameTest()
        Dim _IndexName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Indexs_Factory.GetDocIndexByIndexName(_IndexName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDistinctIndexValues
    '''</summary>
    <TestMethod()> _
    Public Sub GetDistinctIndexValuesTest()
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim strRestricc As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim indexType As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Indexs_Factory.GetDistinctIndexValues(docTypeId, indexId, strRestricc, indexType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DropTable
    '''</summary>
    <TestMethod()> _
    Public Sub DropTableTest()
        Dim tabla As String = String.Empty ' TODO: Initialize to an appropriate value
        Indexs_Factory.DropTable(tabla)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for delindexlist
    '''</summary>
    <TestMethod()> _
    Public Sub delindexlistTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Indexs_Factory.delindexlist(IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for delindexitems
    '''</summary>
    <TestMethod()> _
    Public Sub delindexitemsTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexList As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Indexs_Factory.delindexitems(IndexId, IndexList)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelIndex
    '''</summary>
    <TestMethod()> _
    Public Sub DelIndexTest()
        Dim IndexId As Long = 0 ' TODO: Initialize to an appropriate value
        Indexs_Factory.DelIndex(IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteSustituciontable
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteSustituciontableTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Indexs_Factory.DeleteSustituciontable(IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for createsustituciontable
    '''</summary>
    <TestMethod()> _
    Public Sub createsustituciontableTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Indexs_Factory.createsustituciontable(IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for addindexlist
    '''</summary>
    <TestMethod()> _
    Public Sub addindexlistTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexLen As Integer = 0 ' TODO: Initialize to an appropriate value
        Indexs_Factory.addindexlist(IndexId, IndexLen)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddIndex
    '''</summary>
    <TestMethod()> _
    Public Sub AddIndexTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Type As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Len As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim AutoFill1 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim NoIndex1 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DropDown As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim AutoDisplay1 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Invisible1 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Object_Type_Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Indexs_Factory.AddIndex(id, Name, Type, Len, AutoFill1, NoIndex1, DropDown, AutoDisplay1, Invisible1, Object_Type_Id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Indexs_Factory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub Indexs_FactoryConstructorTest()
        Dim target As Indexs_Factory = New Indexs_Factory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
