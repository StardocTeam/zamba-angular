Imports System.Text

Imports System.Collections.Generic

Imports System.Data

Imports System

Imports System.Collections

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for Results_FactoryTest and is intended
'''to contain all Results_FactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class Results_FactoryTest


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
    '''A test for ZWFIUpdateInsertID
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub ZWFIUpdateInsertIDTest1()
        Dim _WI As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _InsertID As Integer = 0 ' TODO: Initialize to an appropriate value
        Results_Factory_Accessor.ZWFIUpdateInsertID(_WI, _InsertID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ZWFIUpdateInsertID
    '''</summary>
    <TestMethod()> _
    Public Sub ZWFIUpdateInsertIDTest()
        Dim _waitIDs() As Long = Nothing ' TODO: Initialize to an appropriate value
        Dim _InsertID As Long = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.ZWFIUpdateInsertID(_waitIDs, _InsertID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ZWFIIValidation
    '''</summary>
    <TestMethod()> _
    Public Sub ZWFIIValidationTest()
        Dim _dt As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim _dtExpected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim _IID() As Long = Nothing ' TODO: Initialize to an appropriate value
        Dim _IValue() As String = Nothing ' TODO: Initialize to an appropriate value
        Dim expected() As Long = Nothing ' TODO: Initialize to an appropriate value
        Dim actual() As Long
        actual = Results_Factory.ZWFIIValidation(_dt, _IID, _IValue)
        Assert.AreEqual(_dtExpected, _dt)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ZWFIIIndexValidation
    '''</summary>
    <TestMethod()> _
    Public Sub ZWFIIIndexValidationTest()
        Dim _WI As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _IID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _IValue As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Results_Factory.ZWFIIIndexValidation(_WI, _IID, _IValue)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ZWFIIIndexsValidation
    '''</summary>
    <TestMethod()> _
    Public Sub ZWFIIIndexsValidationTest()
        Dim _WI As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _IID() As Long = Nothing ' TODO: Initialize to an appropriate value
        Dim _IValue() As String = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Results_Factory.ZWFIIIndexsValidation(_WI, _IID, _IValue)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for VerifyIfWaitingDocuments
    '''</summary>
    <TestMethod()> _
    Public Sub VerifyIfWaitingDocumentsTest()
        Dim ruleId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Short = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Short
        actual = Results_Factory.VerifyIfWaitingDocuments(ruleId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ValidatePublishableIndexsStateExistance
    '''</summary>
    <TestMethod()> _
    Public Sub ValidatePublishableIndexsStateExistanceTest()
        Dim DocTypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Indexid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim eventId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Results_Factory.ValidatePublishableIndexsStateExistance(DocTypeid, Indexid, eventId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ValidateIsDocTypeInZI
    '''</summary>
    <TestMethod()> _
    Public Sub ValidateIsDocTypeInZITest()
        Dim docType As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Results_Factory.ValidateIsDocTypeInZI(docType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateResultsVersionedDataWhenDelete
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateResultsVersionedDataWhenDeleteTest()
        Dim DocTypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim parentid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim docid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim RootDocumentId As Long = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.UpdateResultsVersionedDataWhenDelete(DocTypeid, parentid, docid, RootDocumentId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateRegisterDocument
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateRegisterDocumentTest()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ReindexFlag As Boolean = False ' TODO: Initialize to an appropriate value
        Dim isvirtual As Boolean = False ' TODO: Initialize to an appropriate value
        Results_Factory.UpdateRegisterDocument(Result, ReindexFlag, isvirtual)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateLastResultVersioned
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateLastResultVersionedTest()
        Dim DocTypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim parentid As Long = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.UpdateLastResultVersioned(DocTypeid, parentid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for updatedocfile
    '''</summary>
    <TestMethod()> _
    Public Sub updatedocfileTest()
        Dim Result As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim docfile As String = String.Empty ' TODO: Initialize to an appropriate value
        Results_Factory.updatedocfile(Result, docfile)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for unLockDocument
    '''</summary>
    <TestMethod()> _
    Public Sub unLockDocumentTest()
        Dim DocID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.unLockDocument(DocID, UserId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetVersionComment
    '''</summary>
    <TestMethod()> _
    Public Sub SetVersionCommentTest()
        Dim rID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim rComment As String = String.Empty ' TODO: Initialize to an appropriate value
        Results_Factory.SetVersionComment(rID, rComment)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetRuleIDNotWaiting
    '''</summary>
    <TestMethod()> _
    Public Sub SetRuleIDNotWaitingTest()
        Dim lngRuleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim lngInsertID As Long = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.SetRuleIDNotWaiting(lngRuleID, lngInsertID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for setParentVersion
    '''</summary>
    <TestMethod()> _
    Public Sub setParentVersionTest()
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docId As Long = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.setParentVersion(docTypeId, docId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SelectWaitingDocTypeInZWFI
    '''</summary>
    <TestMethod()> _
    Public Sub SelectWaitingDocTypeInZWFITest()
        Dim _DTID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = Results_Factory.SelectWaitingDocTypeInZWFI(_DTID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SearchIndexForWebServices
    '''</summary>
    <TestMethod()> _
    Public Sub SearchIndexForWebServicesTest()
        Dim lngIndexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim enmIndexType As IndexDataType = New IndexDataType ' TODO: Initialize to an appropriate value
        Dim strComparador As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strValue As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim lngDocTypeID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim restriction As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.SearchIndexForWebServices(lngIndexID, enmIndexType, strComparador, strValue, lngDocTypeID, restriction)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SearchIndex
    '''</summary>
    <TestMethod()> _
    Public Sub SearchIndexTest1()
        Dim lngIndexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim enmIndexType As IndexDataType = New IndexDataType ' TODO: Initialize to an appropriate value
        Dim strComparador As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strValue As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim lngDocTypeID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.SearchIndex(lngIndexID, enmIndexType, strComparador, strValue, lngDocTypeID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SearchIndex
    '''</summary>
    <TestMethod()> _
    Public Sub SearchIndexTest()
        Dim lngIndexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim enmIndexType As IndexDataType = New IndexDataType ' TODO: Initialize to an appropriate value
        Dim strComparador As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strValue As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim lngDocTypeID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim restriction As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.SearchIndex(lngIndexID, enmIndexType, strComparador, strValue, lngDocTypeID, restriction)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SearchbyIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub SearchbyIndexsTest()
        Dim indexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexType As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim dt As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim IndexData As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.SearchbyIndexs(indexId, indexType, dt, IndexData)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SaveVersionComment
    '''</summary>
    <TestMethod()> _
    Public Sub SaveVersionCommentTest1()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Results_Factory.SaveVersionComment(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveVersionComment
    '''</summary>
    <TestMethod()> _
    Public Sub SaveVersionCommentTest()
        Dim ResultID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ResultComment As String = String.Empty ' TODO: Initialize to an appropriate value
        Results_Factory.SaveVersionComment(ResultID, ResultComment)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SavePublishableIndexsState
    '''</summary>
    <TestMethod()> _
    Public Sub SavePublishableIndexsStateTest()
        Dim dataid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocTypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Indexid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim eventId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DefValue As String = String.Empty ' TODO: Initialize to an appropriate value
        Results_Factory.SavePublishableIndexsState(dataid, DocTypeid, Indexid, eventId, DefValue)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SavePublish
    '''</summary>
    <TestMethod()> _
    Public Sub SavePublishTest()
        Dim publishid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim userid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim publishdate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Results_Factory.SavePublish(publishid, docid, userid, publishdate)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveName
    '''</summary>
    <TestMethod()> _
    Public Sub SaveNameTest()
        Dim Result As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Results_Factory.SaveName(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveIndexText
    '''</summary>
    <TestMethod()> _
    Public Sub SaveIndexTextTest()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim Data As String = String.Empty ' TODO: Initialize to an appropriate value
        Results_Factory.SaveIndexText(Result, Data)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveIndexDataNoReindex
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub SaveIndexDataNoReindexTest()
        Dim Result As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim i As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Columns As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim ColumnsExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Values As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim ValuesExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Results_Factory_Accessor.SaveIndexDataNoReindex(Result, i, Columns, Values)
        Assert.AreEqual(ResultExpected, Result)
        Assert.AreEqual(ColumnsExpected, Columns)
        Assert.AreEqual(ValuesExpected, Values)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveIndexDataAndReindex
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub SaveIndexDataAndReindexTest()
        Dim Result As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim i As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim strset As StringBuilder = Nothing ' TODO: Initialize to an appropriate value
        Results_Factory_Accessor.SaveIndexDataAndReindex(Result, i, strset)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveIndexData
    '''</summary>
    <TestMethod()> _
    Public Sub SaveIndexDataTest1()
        Dim Result As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim ReindexFlag As Boolean = False ' TODO: Initialize to an appropriate value
        Dim changeEvent As Boolean = False ' TODO: Initialize to an appropriate value
        Dim OnlySpecifiedIndexsids As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Results_Factory.SaveIndexData(Result, ReindexFlag, changeEvent, OnlySpecifiedIndexsids)
        Assert.AreEqual(ResultExpected, Result)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SaveIndexData
    '''</summary>
    <TestMethod()> _
    Public Sub SaveIndexDataTest()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ReindexFlag As Boolean = False ' TODO: Initialize to an appropriate value
        Results_Factory.SaveIndexData(Result, ReindexFlag)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ReplaceFile
    '''</summary>
    <TestMethod()> _
    Public Sub ReplaceFileTest()
        Dim Result As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim NewDocumentFile As String = String.Empty ' TODO: Initialize to an appropriate value
        Results_Factory.ReplaceFile(Result, NewDocumentFile)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ReplaceDocument
    '''</summary>
    <TestMethod()> _
    Public Sub ReplaceDocumentTest()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Results_Factory.ReplaceDocument(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveDocTypeWF
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveDocTypeWFTest()
        Dim DocTypeID As Integer = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.RemoveDocTypeWF(DocTypeID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RegisterDocument
    '''</summary>
    <TestMethod()> _
    Public Sub RegisterDocumentTest()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim reIndexFlag As Boolean = False ' TODO: Initialize to an appropriate value
        Dim isVirtual As Boolean = False ' TODO: Initialize to an appropriate value
        Results_Factory.RegisterDocument(Result, reIndexFlag, isVirtual)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for MakeTable
    '''</summary>
    <TestMethod()> _
    Public Sub MakeTableTest()
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim tableType As Results_Factory.TableType = New Results_Factory.TableType ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.MakeTable(docTypeId, tableType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for MakefileName
    '''</summary>
    <TestMethod()> _
    Public Sub MakefileNameTest1()
        Dim File As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.MakefileName(File)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for MakefileName
    '''</summary>
    <TestMethod()> _
    Public Sub MakefileNameTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.MakefileName
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for LockDocument
    '''</summary>
    <TestMethod()> _
    Public Sub LockDocumentTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.LockDocument(DocId, UserId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for IsLocked
    '''</summary>
    <TestMethod()> _
    Public Sub IsLockedTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Results_Factory.IsLocked(DocId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IsDocTypeInWF
    '''</summary>
    <TestMethod()> _
    Public Sub IsDocTypeInWFTest()
        Dim DocTypeid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Results_Factory.IsDocTypeInWF(DocTypeid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for InsertZI
    '''</summary>
    <TestMethod()> _
    Public Sub InsertZITest()
        Dim _InsertID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _DTID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _DocID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _FolderID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _IDate As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Results_Factory.InsertZI(_InsertID, _DTID, _DocID, _FolderID, _IDate)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetZWFIIbyWI
    '''</summary>
    <TestMethod()> _
    Public Sub GetZWFIIbyWITest()
        Dim wI As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZWFIIbyWI(wI)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZWFIbyWI
    '''</summary>
    <TestMethod()> _
    Public Sub GetZWFIbyWITest()
        Dim wI As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZWFIbyWI(wI)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZWFIbyRuleID
    '''</summary>
    <TestMethod()> _
    Public Sub GetZWFIbyRuleIDTest()
        Dim lngRuleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZWFIbyRuleID(lngRuleID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZWFIbyDocType
    '''</summary>
    <TestMethod()> _
    Public Sub GetZWFIbyDocTypeTest()
        Dim lngDocType As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZWFIbyDocType(lngDocType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZIWhereRuleIDAndDocID
    '''</summary>
    <TestMethod()> _
    Public Sub GetZIWhereRuleIDAndDocIDTest()
        Dim lngRuleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim docID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZIWhereRuleIDAndDocID(lngRuleID, docID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZIWhereRuleID
    '''</summary>
    <TestMethod()> _
    Public Sub GetZIWhereRuleIDTest()
        Dim lngRuleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZIWhereRuleID(lngRuleID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZIWhereInsertID
    '''</summary>
    <TestMethod()> _
    Public Sub GetZIWhereInsertIDTest()
        Dim lngInsertID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZIWhereInsertID(lngInsertID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZIWhereDocID
    '''</summary>
    <TestMethod()> _
    Public Sub GetZIWhereDocIDTest()
        Dim lngDocID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZIWhereDocID(lngDocID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZIbyWI
    '''</summary>
    <TestMethod()> _
    Public Sub GetZIbyWITest()
        Dim wI As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZIbyWI(wI)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetZIbyDocType
    '''</summary>
    <TestMethod()> _
    Public Sub GetZIbyDocTypeTest()
        Dim docType As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetZIbyDocType(docType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWIFromZWFI
    '''</summary>
    <TestMethod()> _
    Public Sub GetWIFromZWFITest()
        Dim ruleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetWIFromZWFI(ruleID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWIFromZIWhereRuleID
    '''</summary>
    <TestMethod()> _
    Public Sub GetWIFromZIWhereRuleIDTest()
        Dim lngRuleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Long)
        actual = Results_Factory.GetWIFromZIWhereRuleID(lngRuleID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetWatingDocumentMails
    '''</summary>
    <TestMethod()> _
    Public Sub GetWatingDocumentMailsTest()
        Dim lngRuleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetWatingDocumentMails(lngRuleID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVersionCommentDate
    '''</summary>
    <TestMethod()> _
    Public Sub GetVersionCommentDateTest()
        Dim ResultId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.GetVersionCommentDate(ResultId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVersionComment
    '''</summary>
    <TestMethod()> _
    Public Sub GetVersionCommentTest()
        Dim ResultId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.GetVersionComment(ResultId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRuleIDsFromZIWhereInsertID
    '''</summary>
    <TestMethod()> _
    Public Sub GetRuleIDsFromZIWhereInsertIDTest()
        Dim lngInsertID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Long)
        actual = Results_Factory.GetRuleIDsFromZIWhereInsertID(lngInsertID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getResultsData
    '''</summary>
    <TestMethod()> _
    Public Sub getResultsDataTest()
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim genIndex As List(Of ArrayList) = Nothing ' TODO: Initialize to an appropriate value
        Dim comparateValue As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim searchValue As Boolean = False ' TODO: Initialize to an appropriate value
        Dim strRestricc As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.getResultsData(docTypeId, indexId, genIndex, comparateValue, searchValue, strRestricc)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getResultsCount
    '''</summary>
    <TestMethod()> _
    Public Sub getResultsCountTest()
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim genIndex As List(Of ArrayList) = Nothing ' TODO: Initialize to an appropriate value
        Dim RestrictionAndSortExpression As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Results_Factory.getResultsCount(docTypeId, indexId, genIndex, RestrictionAndSortExpression)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getResultsAndPageQueryResults
    '''</summary>
    <TestMethod()> _
    Public Sub getResultsAndPageQueryResultsTest()
        Dim PageId As Short = 0 ' TODO: Initialize to an appropriate value
        Dim PageSize As Short = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim genIndex As List(Of ArrayList) = Nothing ' TODO: Initialize to an appropriate value
        Dim RestrictionAndSortExpression As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim SymbolToReplace As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim BySimbolReplace As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = Results_Factory.getResultsAndPageQueryResults(PageId, PageSize, docTypeId, indexId, genIndex, RestrictionAndSortExpression, SymbolToReplace, BySimbolReplace)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetPublishEventsIds
    '''</summary>
    <TestMethod()> _
    Public Sub GetPublishEventsIdsTest()
        Dim EventName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Results_Factory.GetPublishEventsIds(EventName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetPublishEvents
    '''</summary>
    <TestMethod()> _
    Public Sub GetPublishEventsTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetPublishEvents
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetPublishableIndexsStates
    '''</summary>
    <TestMethod()> _
    Public Sub GetPublishableIndexsStatesTest()
        Dim idDocType As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetPublishableIndexsStates(idDocType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getPathForIdTypeIdDoc
    '''</summary>
    <TestMethod()> _
    Public Sub getPathForIdTypeIdDocTest()
        Dim doc_id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim doc_type_id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.getPathForIdTypeIdDoc(doc_id, doc_type_id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetParentVersionId
    '''</summary>
    <TestMethod()> _
    Public Sub GetParentVersionIdTest()
        Dim DocTypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim docid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = Results_Factory.GetParentVersionId(DocTypeid, docid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetParentsCountForVersions
    '''</summary>
    <TestMethod()> _
    Public Sub GetParentsCountForVersionsTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim doctypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Results_Factory.GetParentsCountForVersions(DocId, doctypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetNewVersionID
    '''</summary>
    <TestMethod()> _
    Public Sub GetNewVersionIDTest()
        Dim RootId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim doctype As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim originalDocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Results_Factory.GetNewVersionID(RootId, doctype, originalDocId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetName
    '''</summary>
    <TestMethod()> _
    Public Sub GetNameTest()
        Dim ResultId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.GetName(ResultId, DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetInitialStep
    '''</summary>
    <TestMethod()> _
    Public Sub GetInitialStepTest()
        Dim WFID As Short = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Results_Factory.GetInitialStep(WFID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexValueFromZWFII
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexValueFromZWFIITest()
        Dim wI As Long = 0 ' TODO: Initialize to an appropriate value
        Dim indexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.GetIndexValueFromZWFII(wI, indexID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexValueFromDocI
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexValueFromDocITest()
        Dim docType As Long = 0 ' TODO: Initialize to an appropriate value
        Dim iID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim docID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim iValue As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim iValueExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = Results_Factory.GetIndexValueFromDocI(docType, iID, docID, iValue)
        Assert.AreEqual(iValueExpected, iValue)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexValueFromDoc_I
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexValueFromDoc_ITest()
        Dim docType As Long = 0 ' TODO: Initialize to an appropriate value
        Dim docID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim indexID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.GetIndexValueFromDoc_I(docType, docID, indexID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexSchema
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexSchemaTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetIndexSchema(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexsTest()
        Dim result As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim resultExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim data As Boolean = False ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = Results_Factory.GetIndexs(result, data)
        Assert.AreEqual(resultExpected, result)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexFromZWFII
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexFromZWFIITest()
        Dim wI As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetIndexFromZWFII(wI)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexData
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexDataTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DocId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetIndexData(DocTypeId, DocId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getFullPathByDocIdandIndex
    '''</summary>
    <TestMethod()> _
    Public Sub getFullPathByDocIdandIndexTest()
        Dim DocTypeID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexValue As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.getFullPathByDocIdandIndex(DocTypeID, indexId, indexValue)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetFullNameWhereDocID
    '''</summary>
    <TestMethod()> _
    Public Sub GetFullNameWhereDocIDTest()
        Dim lngDocType As Long = 0 ' TODO: Initialize to an appropriate value
        Dim lngDocID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.GetFullNameWhereDocID(lngDocType, lngDocID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetFullName
    '''</summary>
    <TestMethod()> _
    Public Sub GetFullNameTest()
        Dim ResultId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.GetFullName(ResultId, DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocuments
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocumentsTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DsResults = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsResults
        actual = Results_Factory.GetDocuments(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocumentData
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocumentDataTest()
        Dim ds As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim dt As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim i As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetDocumentData(ds, dt, i)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypesZWFI
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesZWFITest()
        Dim ruleID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetDocTypesZWFI(ruleID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypesZI
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypesZITest()
        Dim dTID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetDocTypesZI(dTID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocIDsFromZI
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocIDsFromZITest()
        Dim docType As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = Results_Factory.GetDocIDsFromZI(docType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetCreatorUser
    '''</summary>
    <TestMethod()> _
    Public Sub GetCreatorUserTest()
        Dim docid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.GetCreatorUser(docid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAutoNameCode
    '''</summary>
    <TestMethod()> _
    Public Sub GetAutoNameCodeTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory.GetAutoNameCode(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for FillIndexData
    '''</summary>
    <TestMethod()> _
    Public Sub FillIndexDataTest()
        Dim Result As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Results_Factory.FillIndexData(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteResultFromWorkflows
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteResultFromWorkflowsTest()
        Dim docid As Long = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.DeleteResultFromWorkflows(docid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeletePublishableIndexsState
    '''</summary>
    <TestMethod()> _
    Public Sub DeletePublishableIndexsStateTest()
        Dim DocTypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Indexid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DefValue As String = String.Empty ' TODO: Initialize to an appropriate value
        Results_Factory.DeletePublishableIndexsState(DocTypeid, Indexid, DefValue)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteFromZWFII
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteFromZWFIITest()
        Dim wI As Long = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.DeleteFromZWFII(wI)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteFromZWFI
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteFromZWFITest()
        Dim wI As Long = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.DeleteFromZWFI(wI)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteFromZI
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteFromZITest()
        Dim lngDocID As Long = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.DeleteFromZI(lngDocID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest2()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim fullPath As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim deleteFile As Boolean = False ' TODO: Initialize to an appropriate value
        Results_Factory.Delete(taskId, DocTypeId, fullPath, deleteFile)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest1()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim delfile As Boolean = False ' TODO: Initialize to an appropriate value
        Results_Factory.Delete(Result, delfile)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest()
        Dim Result As IResult = Nothing ' TODO: Initialize to an appropriate value
        Dim delfile As Boolean = False ' TODO: Initialize to an appropriate value
        Results_Factory.Delete(Result, delfile)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CreateUpdateQuery
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub CreateUpdateQueryTest()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim tableName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory_Accessor.CreateUpdateQuery(Result, tableName)
        Assert.AreEqual(ResultExpected, Result)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CreateInsertQuery
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub CreateInsertQueryTest1()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim tableName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim isVirtual As Boolean = False ' TODO: Initialize to an appropriate value
        Dim fileLen As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory_Accessor.CreateInsertQuery(Result, tableName, isVirtual, fileLen)
        Assert.AreEqual(ResultExpected, Result)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CreateInsertQuery
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub CreateInsertQueryTest()
        Dim Result As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As NewResult = Nothing ' TODO: Initialize to an appropriate value
        Dim tableName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = Results_Factory_Accessor.CreateInsertQuery(Result, tableName)
        Assert.AreEqual(ResultExpected, Result)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CountChildsVersions
    '''</summary>
    <TestMethod()> _
    Public Sub CountChildsVersionsTest()
        Dim DocTypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim parentid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = Results_Factory.CountChildsVersions(DocTypeid, parentid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CompleteIndexData
    '''</summary>
    <TestMethod()> _
    Public Sub CompleteIndexDataTest1()
        Dim docTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim docId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim indexs As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim indexsExpected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Results_Factory.CompleteIndexData(docTypeId, docId, indexs)
        Assert.AreEqual(indexsExpected, indexs)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CompleteIndexData
    '''</summary>
    <TestMethod()> _
    Public Sub CompleteIndexDataTest()
        Dim Result As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Results_Factory.CompleteIndexData(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CompleteDocument
    '''</summary>
    <TestMethod()> _
    Public Sub CompleteDocumentTest()
        Dim Result As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim ResultExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Results_Factory.CompleteDocument(Result)
        Assert.AreEqual(ResultExpected, Result)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddDocTypeToWF
    '''</summary>
    <TestMethod()> _
    Public Sub AddDocTypeToWFTest()
        Dim DocTypeID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim WfID As Integer = 0 ' TODO: Initialize to an appropriate value
        Results_Factory.AddDocTypeToWF(DocTypeID, WfID)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for aCompleteDocument
    '''</summary>
    <TestMethod()> _
    Public Sub aCompleteDocumentTest()
        Dim Document As Result = Nothing ' TODO: Initialize to an appropriate value
        Dim DocumentExpected As Result = Nothing ' TODO: Initialize to an appropriate value
        Results_Factory.aCompleteDocument(Document)
        Assert.AreEqual(DocumentExpected, Document)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Results_Factory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub Results_FactoryConstructorTest()
        Dim target As Results_Factory = New Results_Factory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
