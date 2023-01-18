Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for ExportFactoryTest and is intended
'''to contain all ExportFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ExportFactoryTest


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
    '''A test for InsertIndex
    '''</summary>
    <TestMethod()> _
    Public Sub InsertIndexTest()
        Dim exportId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim indexName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim indexTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim indexValue As String = String.Empty ' TODO: Initialize to an appropriate value
        ExportFactory.InsertIndex(exportId, indexName, indexTypeName, indexValue)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for InsertDocument
    '''</summary>
    <TestMethod()> _
    Public Sub InsertDocumentTest()
        Dim name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim zambaLink As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = ExportFactory.InsertDocument(name, docTypeName, zambaLink)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ExportFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub ExportFactoryConstructorTest()
        Dim target As ExportFactory = New ExportFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
