Imports Zamba.Core

Imports System.Collections

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for CreateTablesFactoryTest and is intended
'''to contain all CreateTablesFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class CreateTablesFactoryTest


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
    '''A test for UpdateIntoSustitucion
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateIntoSustitucionTest()
        Dim Tabla As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Codigo As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Descripcion As String = String.Empty ' TODO: Initialize to an appropriate value
        CreateTablesFactory.UpdateIntoSustitucion(Tabla, Codigo, Descripcion)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertIntoSustitucion
    '''</summary>
    <TestMethod()> _
    Public Sub InsertIntoSustitucionTest()
        Dim Tabla As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Codigo As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Descripcion As String = String.Empty ' TODO: Initialize to an appropriate value
        CreateTablesFactory.InsertIntoSustitucion(Tabla, Codigo, Descripcion)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertIndexList
    '''</summary>
    <TestMethod()> _
    Public Sub InsertIndexListTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexList As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        CreateTablesFactory.InsertIndexList(IndexId, IndexList)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ExportSustitucionTable
    '''</summary>
    <TestMethod()> _
    Public Sub ExportSustitucionTableTest()
        Dim file As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim separador As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.ExportSustitucionTable(file, separador, IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DropView
    '''</summary>
    <TestMethod()> _
    Public Sub DropViewTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.DropView(DocTypeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DropSustitucionTable
    '''</summary>
    <TestMethod()> _
    Public Sub DropSustitucionTableTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.DropSustitucionTable(IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        CreateTablesFactory.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelTempTables
    '''</summary>
    <TestMethod()> _
    Public Sub DelTempTablesTest()
        CreateTablesFactory.DelTempTables()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelIndexList
    '''</summary>
    <TestMethod()> _
    Public Sub DelIndexListTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.DelIndexList(IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelIndexItems
    '''</summary>
    <TestMethod()> _
    Public Sub DelIndexItemsTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexList As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        CreateTablesFactory.DelIndexItems(IndexId, IndexList)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelIndexColumn
    '''</summary>
    <TestMethod()> _
    Public Sub DelIndexColumnTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexIdArray As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        CreateTablesFactory.DelIndexColumn(DocTypeId, IndexIdArray)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteTable
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTableTest()
        Dim Table As String = String.Empty ' TODO: Initialize to an appropriate value
        CreateTablesFactory.DeleteTable(Table)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteFromSustitucion
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteFromSustitucionTest()
        Dim Tabla As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Codigo As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Descripcion As String = String.Empty ' TODO: Initialize to an appropriate value
        CreateTablesFactory.DeleteFromSustitucion(Tabla, Codigo, Descripcion)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CreateView
    '''</summary>
    <TestMethod()> _
    Public Sub CreateViewTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.CreateView(DocTypeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CreateTextIndex
    '''</summary>
    <TestMethod()> _
    Public Sub CreateTextIndexTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.CreateTextIndex(DocTypeId, IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CreateSustitucionTable
    '''</summary>
    <TestMethod()> _
    Public Sub CreateSustitucionTableTest()
        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.CreateSustitucionTable(Index)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CreateFriendlyView
    '''</summary>
    <TestMethod()> _
    Public Sub CreateFriendlyViewTest()
        Dim DocType As DocType = Nothing ' TODO: Initialize to an appropriate value
        CreateTablesFactory.CreateFriendlyView(DocType)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for BulkInsertSustitucionTable
    '''</summary>
    <TestMethod()> _
    Public Sub BulkInsertSustitucionTableTest()
        Dim FileName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim separador As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.BulkInsertSustitucionTable(FileName, separador, IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for BorrarSustitucionTable
    '''</summary>
    <TestMethod()> _
    Public Sub BorrarSustitucionTableTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.BorrarSustitucionTable(IndexId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddIndexList
    '''</summary>
    <TestMethod()> _
    Public Sub AddIndexListTest()
        Dim IndexId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexLen As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.AddIndexList(IndexId, IndexLen)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddIndexColumn
    '''</summary>
    <TestMethod()> _
    Public Sub AddIndexColumnTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IndexIdArray As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim IndexTypeArray As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim IndexLenArray As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        CreateTablesFactory.AddIndexColumn(DocTypeId, IndexIdArray, IndexTypeArray, IndexLenArray)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddDocsTables
    '''</summary>
    <TestMethod()> _
    Public Sub AddDocsTablesTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        CreateTablesFactory.AddDocsTables(DocTypeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CreateTablesFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub CreateTablesFactoryConstructorTest()
        Dim target As CreateTablesFactory = New CreateTablesFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
