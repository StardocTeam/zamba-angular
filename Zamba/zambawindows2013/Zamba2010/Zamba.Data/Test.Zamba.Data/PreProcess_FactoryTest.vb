Imports System.Collections

Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for PreProcess_FactoryTest and is intended
'''to contain all PreProcess_FactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class PreProcess_FactoryTest


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
    '''A test for savePreprocess
    '''</summary>
    <TestMethod()> _
    Public Sub savePreprocessTest()
        Dim preid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim preprocess As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        PreProcess_Factory.savePreprocess(preid, preprocess)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for getSlst_s45
    '''</summary>
    <TestMethod()> _
    Public Sub getSlst_s45Test()
        Dim CodeCompany As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = PreProcess_Factory.getSlst_s45(CodeCompany)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetPreProcessById
    '''</summary>
    <TestMethod()> _
    Public Sub GetPreProcessByIdTest()
        Dim Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As dsIPPreprocess = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As dsIPPreprocess
        actual = PreProcess_Factory.GetPreProcessById(Id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for PreProcess_Factory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub PreProcess_FactoryConstructorTest()
        Dim target As PreProcess_Factory = New PreProcess_Factory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
