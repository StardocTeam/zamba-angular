Imports Zamba.Core

Imports System.Collections

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for DataBaseIndexsFactoryTest and is intended
'''to contain all DataBaseIndexsFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class DataBaseIndexsFactoryTest


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
    '''A test for VerificarServer
    '''</summary>
    <TestMethod()> _
    Public Sub VerificarServerTest()
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = DataBaseIndexsFactory.VerificarServer
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Update_DocD
    '''</summary>
    <TestMethod()> _
    Public Sub Update_DocDTest()
        Dim docd_obj As DocD = Nothing ' TODO: Initialize to an appropriate value
        Dim doc_dindex As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indices As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = DataBaseIndexsFactory.Update_DocD(docd_obj, doc_dindex, indices)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetData
    '''</summary>
    <TestMethod()> _
    Public Sub GetDataTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DataBaseIndexsFactory.GetData(taskId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for FillIndexDocType
    '''</summary>
    <TestMethod()> _
    Public Sub FillIndexDocTypeTest()
        Dim docTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DataBaseIndexsFactory.FillIndexDocType(docTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Drop_Database_Index
    '''</summary>
    <TestMethod()> _
    Public Sub Drop_Database_IndexTest()
        Dim index_name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docd_index As Integer = 0 ' TODO: Initialize to an appropriate value
        DataBaseIndexsFactory.Drop_Database_Index(index_name, docd_index)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete_Index_Table
    '''</summary>
    <TestMethod()> _
    Public Sub Delete_Index_TableTest()
        Dim doc_type_id As Integer = 0 ' TODO: Initialize to an appropriate value
        DataBaseIndexsFactory.Delete_Index_Table(doc_type_id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete_Index_Column
    '''</summary>
    <TestMethod()> _
    Public Sub Delete_Index_ColumnTest()
        Dim doc_type_id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim indexes As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        DataBaseIndexsFactory.Delete_Index_Column(doc_type_id, indexes)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete_DocD
    '''</summary>
    <TestMethod()> _
    Public Sub Delete_DocDTest()
        Dim docd_name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim doc_dindex As Integer = 0 ' TODO: Initialize to an appropriate value
        DataBaseIndexsFactory.Delete_DocD(docd_name, doc_dindex)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Create_New_Index
    '''</summary>
    <TestMethod()> _
    Public Sub Create_New_IndexTest()
        Dim docId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim indices As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As DocD = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DocD
        actual = DataBaseIndexsFactory.Create_New_Index(docId, Name, indices)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Create_Database_Index
    '''</summary>
    <TestMethod()> _
    Public Sub Create_Database_IndexTest()
        Dim docd_obj As DocD = Nothing ' TODO: Initialize to an appropriate value
        Dim doc_dindex As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Cluster As Boolean = False ' TODO: Initialize to an appropriate value
        Dim Unico As Boolean = False ' TODO: Initialize to an appropriate value
        DataBaseIndexsFactory.Create_Database_Index(docd_obj, doc_dindex, Cluster, Unico)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Create_AllDocDIndex
    '''</summary>
    <TestMethod()> _
    Public Sub Create_AllDocDIndexTest()
        Dim doc_id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indices As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = DataBaseIndexsFactory.Create_AllDocDIndex(doc_id, indices)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DataBaseIndexsFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub DataBaseIndexsFactoryConstructorTest()
        Dim target As DataBaseIndexsFactory = New DataBaseIndexsFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
