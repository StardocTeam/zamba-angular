Imports System.Collections

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for IntegrityFactoryTest and is intended
'''to contain all IntegrityFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class IntegrityFactoryTest


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
    '''A test for getAllDrI
    '''</summary>
    <TestMethod()> _
    Public Sub getAllDrITest()
        Dim expected As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Hashtable
        actual = IntegrityFactory.getAllDrI
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getAllDocI
    '''</summary>
    <TestMethod()> _
    Public Sub getAllDocITest()
        Dim expected As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Hashtable
        actual = IntegrityFactory.getAllDocI
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getAllDocD
    '''</summary>
    <TestMethod()> _
    Public Sub getAllDocDTest()
        Dim expected As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Hashtable
        actual = IntegrityFactory.getAllDocD
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for crearListaDoc
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub crearListaDocTest()
        Dim dsTemp As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim sColumnaDoc As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Hashtable
        actual = IntegrityFactory_Accessor.crearListaDoc(dsTemp, sColumnaDoc)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IntegrityFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub IntegrityFactoryConstructorTest()
        Dim target As IntegrityFactory = New IntegrityFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
