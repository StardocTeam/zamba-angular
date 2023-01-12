Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for DBToolsFactoryTest and is intended
'''to contain all DBToolsFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class DBToolsFactoryTest


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
    '''A test for ReEnumerarColumna
    '''</summary>
    <TestMethod()> _
    Public Sub ReEnumerarColumnaTest()
        Dim Tabla As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Columna As String = String.Empty ' TODO: Initialize to an appropriate value
        DBToolsFactory.ReEnumerarColumna(Tabla, Columna)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetServerType
    '''</summary>
    <TestMethod()> _
    Public Sub GetServerTypeTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = DBToolsFactory.GetServerType
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocCountTest1()
        Dim ID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = DBToolsFactory.GetDocCount(ID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocCountTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DBToolsFactory.GetDocCount
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllDocIByResultid
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllDocIByResultidTest()
        Dim DocID As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DBToolsFactory.GetAllDocIByResultid(DocID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetActiveDatabase
    '''</summary>
    <TestMethod()> _
    Public Sub GetActiveDatabaseTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = DBToolsFactory.GetActiveDatabase
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ContarDocTypes
    '''</summary>
    <TestMethod()> _
    Public Sub ContarDocTypesTest()
        DBToolsFactory.ContarDocTypes()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DBToolsFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub DBToolsFactoryConstructorTest()
        Dim target As DBToolsFactory = New DBToolsFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
