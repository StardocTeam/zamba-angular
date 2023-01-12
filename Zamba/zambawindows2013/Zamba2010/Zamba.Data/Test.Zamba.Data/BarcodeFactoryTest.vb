Imports System.Collections

Imports Zamba.Core

Imports System

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for BarcodeFactoryTest and is intended
'''to contain all BarcodeFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class BarcodeFactoryTest


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
    '''A test for SaveRemark
    '''</summary>
    <TestMethod()> _
    Public Sub SaveRemarkTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim remark As String = String.Empty ' TODO: Initialize to an appropriate value
        BarcodeFactory.SaveRemark(UserId, remark)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for QuerySelectCaratulas
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub QuerySelectCaratulasTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = BarcodeFactory_Accessor.QuerySelectCaratulas
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for LoadRemarks
    '''</summary>
    <TestMethod()> _
    Public Sub LoadRemarksTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = BarcodeFactory.LoadRemarks(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for insertZbarcodecomplete
    '''</summary>
    <TestMethod()> _
    Public Sub insertZbarcodecompleteTest()
        Dim psClave As Boolean = False ' TODO: Initialize to an appropriate value
        Dim sp_DocTypeid As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim psIndexid As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim psTabla As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim psColumna As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim newOrden As String = String.Empty ' TODO: Initialize to an appropriate value
        BarcodeFactory.insertZbarcodecomplete(psClave, sp_DocTypeid, psIndexid, psTabla, psColumna, newOrden)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertBarCode
    '''</summary>
    <TestMethod()> _
    Public Sub InsertBarCodeTest()
        Dim newresultid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim CaratulaId As Integer = 0 ' TODO: Initialize to an appropriate value
        BarcodeFactory.InsertBarCode(newresultid, DocTypeId, UserId, CaratulaId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Insertar
    '''</summary>
    <TestMethod()> _
    Public Sub InsertarTest()
        Dim doctypeid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim tabla As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim col As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim esclave As Boolean = False ' TODO: Initialize to an appropriate value
        Dim condition As String = String.Empty ' TODO: Initialize to an appropriate value
        BarcodeFactory.Insertar(doctypeid, indexid, tabla, col, esclave, condition)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for getZBARCODECOMPLETE
    '''</summary>
    <TestMethod()> _
    Public Sub getZBARCODECOMPLETETest1()
        Dim doctypeid As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = BarcodeFactory.getZBARCODECOMPLETE(doctypeid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getZBARCODECOMPLETE
    '''</summary>
    <TestMethod()> _
    Public Sub getZBARCODECOMPLETETest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = BarcodeFactory.getZBARCODECOMPLETE
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getStartDate
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub getStartDateTest()
        Dim p_date As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = BarcodeFactory_Accessor.getStartDate(p_date)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetSql
    '''</summary>
    <TestMethod()> _
    Public Sub GetSqlTest()
        Dim doctypeid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim indexvalue As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim indexvalueExpected As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = BarcodeFactory.GetSql(doctypeid, indexvalue)
        Assert.AreEqual(indexvalueExpected, indexvalue)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetSentencia
    '''</summary>
    <TestMethod()> _
    Public Sub GetSentenciaTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DataTemp As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim DataTempExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = BarcodeFactory.GetSentencia(DocTypeId, DataTemp)
        Assert.AreEqual(DataTempExpected, DataTemp)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetLastOrden
    '''</summary>
    <TestMethod()> _
    Public Sub GetLastOrdenTest()
        Dim dt As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Short = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Short
        actual = BarcodeFactory.GetLastOrden(dt)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getInforme
    '''</summary>
    <TestMethod()> _
    Public Sub getInformeTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = BarcodeFactory.getInforme
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getIndexKey
    '''</summary>
    <TestMethod()> _
    Public Sub getIndexKeyTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Index = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Index
        actual = BarcodeFactory.getIndexKey(id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getEndDate
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub getEndDateTest()
        Dim p_date As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = BarcodeFactory_Accessor.getEndDate(p_date)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDsIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub GetDsIndexsTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = BarcodeFactory.GetDsIndexs(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeAndDocIdByCaratulaId
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeAndDocIdByCaratulaIdTest()
        Dim CaratulaId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = BarcodeFactory.GetDocTypeAndDocIdByCaratulaId(CaratulaId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAutoIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub GetAutoIndexsTest()
        Dim dt As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = BarcodeFactory.GetAutoIndexs(dt, IndexId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAutoCompleteIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub GetAutoCompleteIndexsTest()
        Dim doctypeid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = BarcodeFactory.GetAutoCompleteIndexs(doctypeid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for dsFilterCaratulas
    '''</summary>
    <TestMethod()> _
    Public Sub dsFilterCaratulasTest3()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim fechaInicial As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim fechaFinal As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = BarcodeFactory.dsFilterCaratulas(UserId, fechaInicial, fechaFinal)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for dsFilterCaratulas
    '''</summary>
    <TestMethod()> _
    Public Sub dsFilterCaratulasTest2()
        Dim fechaInicial As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim fechaFinal As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = BarcodeFactory.dsFilterCaratulas(fechaInicial, fechaFinal)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for dsFilterCaratulas
    '''</summary>
    <TestMethod()> _
    Public Sub dsFilterCaratulasTest1()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim fecha As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = BarcodeFactory.dsFilterCaratulas(UserId, fecha)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for dsFilterCaratulas
    '''</summary>
    <TestMethod()> _
    Public Sub dsFilterCaratulasTest()
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = BarcodeFactory.dsFilterCaratulas(UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for dsAllCaratulas
    '''</summary>
    <TestMethod()> _
    Public Sub dsAllCaratulasTest()
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = BarcodeFactory.dsAllCaratulas
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for deleteZbarcodecomplete
    '''</summary>
    <TestMethod()> _
    Public Sub deleteZbarcodecompleteTest2()
        Dim sp_DocTypeid As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim index As String = String.Empty ' TODO: Initialize to an appropriate value
        BarcodeFactory.deleteZbarcodecomplete(sp_DocTypeid, index)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for deleteZbarcodecomplete
    '''</summary>
    <TestMethod()> _
    Public Sub deleteZbarcodecompleteTest1()
        BarcodeFactory.deleteZbarcodecomplete()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for deleteZbarcodecomplete
    '''</summary>
    <TestMethod()> _
    Public Sub deleteZbarcodecompleteTest()
        Dim sp_DocTypeid As String = String.Empty ' TODO: Initialize to an appropriate value
        BarcodeFactory.deleteZbarcodecomplete(sp_DocTypeid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for BorroEnZBarcode
    '''</summary>
    <TestMethod()> _
    Public Sub BorroEnZBarcodeTest()
        Dim CaratulaId As Integer = 0 ' TODO: Initialize to an appropriate value
        BarcodeFactory.BorroEnZBarcode(CaratulaId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for BarcodeFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub BarcodeFactoryConstructorTest()
        Dim target As BarcodeFactory = New BarcodeFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
