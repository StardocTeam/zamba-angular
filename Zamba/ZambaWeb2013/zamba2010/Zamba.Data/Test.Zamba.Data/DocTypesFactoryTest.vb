Imports System.Collections.Generic

Imports System

Imports System.Data

Imports System.Collections

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for DocTypesFactoryTest and is intended
'''to contain all DocTypesFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class DocTypesFactoryTest


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
    '''A test for UpdateSomeRowsCascade
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateSomeRowsCascadeTest()
        Dim doctypeIdParent As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexParentId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Value As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim whereindexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim wherevalue As Object = Nothing ' TODO: Initialize to an appropriate value
        DocTypesFactory.UpdateSomeRowsCascade(doctypeIdParent, indexParentId, Value, whereindexId, wherevalue)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateDocType
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateDocTypeTest()
        Dim DocType As DocType = Nothing ' TODO: Initialize to an appropriate value
        DocTypesFactory.UpdateDocType(DocType)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateCascade
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateCascadeTest()
        Dim doctypeIdParent As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexParentId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Value As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim FolderId As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.UpdateCascade(doctypeIdParent, indexParentId, Value, FolderId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetRequiredIndex
    '''</summary>
    <TestMethod()> _
    Public Sub SetRequiredIndexTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim indexid As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.SetRequiredIndex(DocTypeId, indexid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetDocTypeRight
    '''</summary>
    <TestMethod()> _
    Public Sub SetDocTypeRightTest()
        Dim DoctypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RightType As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Value As Integer = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.SetDocTypeRight(DoctypeId, RightType, Value)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveIndex
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveIndexTest()
        Dim Doctype As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim IndexId As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.RemoveIndex(Doctype, IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveDocTypefromWf
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveDocTypefromWfTest()
        Dim WFID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.RemoveDocTypefromWf(WFID, DocTypeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Removecolumn
    '''</summary>
    <TestMethod()> _
    Public Sub RemovecolumnTest()
        Dim doctype As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim indexidarray As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        DocTypesFactory.Removecolumn(doctype, indexidarray)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveAllAsociationsByDT
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveAllAsociationsByDTTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.RemoveAllAsociationsByDT(DocTypeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Remove_DocType_FromAll_DocTypesGroup
    '''</summary>
    <TestMethod()> _
    Public Sub Remove_DocType_FromAll_DocTypesGroupTest()
        Dim doctypeid As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.Remove_DocType_FromAll_DocTypesGroup(doctypeid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for LoadDocTypeRightValue
    '''</summary>
    <TestMethod()> _
    Public Sub LoadDocTypeRightValueTest()
        Dim DoctypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RightType As DocTypeRights = New DocTypeRights ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = DocTypesFactory.LoadDocTypeRightValue(DoctypeId, RightType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IndexIsLinked
    '''</summary>
    <TestMethod()> _
    Public Sub IndexIsLinkedTest()
        Dim doctypeid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = DocTypesFactory.IndexIsLinked(doctypeid, indexid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IndexCountInDocuments
    '''</summary>
    <TestMethod()> _
    Public Sub IndexCountInDocumentsTest()
        Dim Doctypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Indexid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = DocTypesFactory.IndexCountInDocuments(Doctypeid, Indexid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWFDocTypes
    '''</summary>
    <TestMethod()> _
    Public Sub GetWFDocTypesTest()
        Dim WfId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DSDOCTYPE = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSDOCTYPE
        actual = DocTypesFactory.GetWFDocTypes(WfId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexText
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexTextTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = DocTypesFactory.GetIndexText(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexSchema
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexSchemaTest()
        Dim docTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetIndexSchema(docTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexsByDocTypeIdAsDataset
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexsByDocTypeIdAsDatasetTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetIndexsByDocTypeIdAsDataset(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getIndexsByDocTypeId
    '''</summary>
    <TestMethod()> _
    Public Sub getIndexsByDocTypeIdTest()
        Dim doctypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = DocTypesFactory.getIndexsByDocTypeId(doctypeid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexsTest1()
        Dim DocType As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetIndexs(DocType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexsTest()
        Dim docTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Long)
        actual = DocTypesFactory.GetIndexs(docTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexNameById
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexNameByIdTest()
        Dim Indexid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = DocTypesFactory.GetIndexNameById(Indexid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexIdByName
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexIdByNameTest()
        Dim IndexName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = DocTypesFactory.GetIndexIdByName(IndexName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexDropDownType
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexDropDownTypeTest()
        Dim indexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = DocTypesFactory.GetIndexDropDownType(indexID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getIndexByDocTypeId
    '''</summary>
    <TestMethod()> _
    Public Sub getIndexByDocTypeIdTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.getIndexByDocTypeId(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndex
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexTest()
        Dim docTypeID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim indexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = DocTypesFactory.GetIndex(docTypeID, indexID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeWorkFlowByWfId
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeWorkFlowByWfIdTest()
        Dim WfId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetDocTypeWorkFlowByWfId(WfId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeWfIds
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeWfIdsTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = DocTypesFactory.GetDocTypeWfIds(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypesIdsAndNames
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesIdsAndNamesTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetDocTypesIdsAndNames
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypesDsDocType
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesDsDocTypeTest()
        Dim expected As DSDOCTYPE = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSDOCTYPE
        actual = DocTypesFactory.GetDocTypesDsDocType
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypesDataSet
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesDataSetTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetDocTypesDataSet
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypesChilds
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesChildsTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetDocTypesChilds
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypesbyUserRightsOfView
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesbyUserRightsOfViewTest()
        Dim UserId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RightType As RightsType = New RightsType ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = DocTypesFactory.GetDocTypesbyUserRightsOfView(UserId, RightType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypesByUser
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesByUserTest()
        Dim indexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim userId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetDocTypesByUser(indexId, userId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypesArrayList
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesArrayListTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = DocTypesFactory.GetDocTypesArrayList
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypes
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesTest()
        Dim expected As DSDOCTYPE = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSDOCTYPE
        actual = DocTypesFactory.GetDocTypes
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeNamesAndIds
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeNamesAndIdsTest()
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = DocTypesFactory.GetDocTypeNamesAndIds
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeNames
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeNamesTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = DocTypesFactory.GetDocTypeNames
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeName
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeNameTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = DocTypesFactory.GetDocTypeName(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeIdByName
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeIdByNameTest()
        Dim DocTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = DocTypesFactory.GetDocTypeIdByName(DocTypeName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeIdByIndexId
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeIdByIndexIdTest2()
        Dim indexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim userid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetDocTypeIdByIndexId(indexID, userid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeIdByIndexId
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeIdByIndexIdTest1()
        Dim indexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetDocTypeIdByIndexId(indexID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeIdByIndexId
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeIdByIndexIdTest()
        Dim indexIDs As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Long)
        actual = DocTypesFactory.GetDocTypeIdByIndexId(indexIDs)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeId
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeIdTest()
        Dim docId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = DocTypesFactory.GetDocTypeId(docId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocType
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeTest1()
        Dim docTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As IDocType = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IDocType
        actual = DocTypesFactory.GetDocType(docTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocType
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeTest()
        Dim DocTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As IDocType = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IDocType
        actual = DocTypesFactory.GetDocType(DocTypeName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocIds
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetDocIdsTest()
        Dim folderid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim doctype As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory_Accessor.GetDocIds(folderid, doctype)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAutoNameText
    '''</summary>
    <TestMethod()> _
    Public Sub GetAutoNameTextTest()
        Dim AutoNameCode As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IndexTable As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = DocTypesFactory.GetAutoNameText(AutoNameCode, IndexTable)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAutoNameCode
    '''</summary>
    <TestMethod()> _
    Public Sub GetAutoNameCodeTest()
        Dim AutoNameText As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IndexTable As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = DocTypesFactory.GetAutoNameCode(AutoNameText, IndexTable)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAutoName
    '''</summary>
    <TestMethod()> _
    Public Sub GetAutoNameTest()
        Dim AutoNameCode As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim DocTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim CreateDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim EditDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim Indexs As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = DocTypesFactory.GetAutoName(AutoNameCode, DocTypeName, CreateDate, EditDate, Indexs)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllIndexsTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetAllIndexs
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllDocTypes
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllDocTypesTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocTypesFactory.GetAllDocTypes
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllDocType
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllDocTypeTest()
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = DocTypesFactory.GetAllDocType
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DocumentsCount
    '''</summary>
    <TestMethod()> _
    Public Sub DocumentsCountTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = DocTypesFactory.DocumentsCount(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DocTypeIsDuplicated
    '''</summary>
    <TestMethod()> _
    Public Sub DocTypeIsDuplicatedTest()
        Dim DocTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = DocTypesFactory.DocTypeIsDuplicated(DocTypeName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DocTypeIsAsigned
    '''</summary>
    <TestMethod()> _
    Public Sub DocTypeIsAsignedTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = DocTypesFactory.DocTypeIsAsigned(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteTables
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTablesTest()
        Dim DocType As DocType = Nothing ' TODO: Initialize to an appropriate value
        DocTypesFactory.DeleteTables(DocType)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteRights
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteRightsTest()
        Dim doctype As DocType = Nothing ' TODO: Initialize to an appropriate value
        DocTypesFactory.DeleteRights(doctype)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteDocTypeTablesTables
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub DeleteDocTypeTablesTablesTest()
        Dim docID As String = String.Empty ' TODO: Initialize to an appropriate value
        DocTypesFactory_Accessor.DeleteDocTypeTablesTables(docID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelDocType
    '''</summary>
    <TestMethod()> _
    Public Sub DelDocTypeTest()
        Dim DocType As DocType = Nothing ' TODO: Initialize to an appropriate value
        DocTypesFactory.DelDocType(DocType)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CreateView
    '''</summary>
    <TestMethod()> _
    Public Sub CreateViewTest()
        Dim DocType As DocType = Nothing ' TODO: Initialize to an appropriate value
        DocTypesFactory.CreateView(DocType)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CopyDoc
    '''</summary>
    <TestMethod()> _
    Public Sub CopyDocTest()
        Dim DocIDOrigen As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocNameDestino As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = DocTypesFactory.CopyDoc(DocIDOrigen, DocNameDestino)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ConvertDatasettoArraylist
    '''</summary>
    <TestMethod()> _
    Public Sub ConvertDatasettoArraylistTest()
        Dim Ds As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim ColumnId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = DocTypesFactory.ConvertDatasettoArraylist(Ds, ColumnId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CheckTempFiles
    '''</summary>
    <TestMethod()> _
    Public Sub CheckTempFilesTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = DocTypesFactory.CheckTempFiles(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for checkDocTypeRow
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub checkDocTypeRowTest()
        Dim row As DataRow = Nothing ' TODO: Initialize to an appropriate value
        DocTypesFactory_Accessor.checkDocTypeRow(row)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CheckDockTypeTables
    '''</summary>
    <TestMethod()> _
    Public Sub CheckDockTypeTablesTest()
        DocTypesFactory.CheckDockTypeTables()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ChangeIndexOrder
    '''</summary>
    <TestMethod()> _
    Public Sub ChangeIndexOrderTest()
        Dim docTypeID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim selectedIndexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim selectedIndexOrder As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim modifiedIndexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim modifiedIndexOrder As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.ChangeIndexOrder(docTypeID, selectedIndexID, selectedIndexOrder, modifiedIndexId, modifiedIndexOrder)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for BorrarDocumentos
    '''</summary>
    <TestMethod()> _
    Public Sub BorrarDocumentosTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.BorrarDocumentos(DocTypeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AsignDocType2Wf
    '''</summary>
    <TestMethod()> _
    Public Sub AsignDocType2WfTest()
        Dim WFID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.AsignDocType2Wf(WFID, DocTypeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for adddoctyperelationindex
    '''</summary>
    <TestMethod()> _
    Public Sub adddoctyperelationindexTest()
        Dim indexid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim doctypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim order As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Required As Boolean = False ' TODO: Initialize to an appropriate value
        DocTypesFactory.adddoctyperelationindex(indexid, doctypeid, order, Required)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddDocType
    '''</summary>
    <TestMethod()> _
    Public Sub AddDocTypeTest()
        Dim DocType As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = DocTypesFactory.AddDocType(DocType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddColumnTextindex
    '''</summary>
    <TestMethod()> _
    Public Sub AddColumnTextindexTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim IndexId As Long = 0 ' TODO: Initialize to an appropriate value
        DocTypesFactory.AddColumnTextindex(DocTypeId, IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddColumn
    '''</summary>
    <TestMethod()> _
    Public Sub AddColumnTest()
        Dim doctype As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim IndexIdArray As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim IndexTypeArray As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim IndexLenArray As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        DocTypesFactory.AddColumn(doctype, IndexIdArray, IndexTypeArray, IndexLenArray)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DocTypesFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub DocTypesFactoryConstructorTest()
        Dim target As DocTypesFactory = New DocTypesFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
