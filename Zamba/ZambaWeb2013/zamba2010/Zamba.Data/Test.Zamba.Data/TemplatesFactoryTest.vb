Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for TemplatesFactoryTest and is intended
'''to contain all TemplatesFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class TemplatesFactoryTest


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
    '''A test for SaveTemplates
    '''</summary>
    <TestMethod()> _
    Public Sub SaveTemplatesTest()
        Dim DsTemplates As DataSet = Nothing ' TODO: Initialize to an appropriate value
        TemplatesFactory.SaveTemplates(DsTemplates)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ObtainTemplatePath
    '''</summary>
    <TestMethod()> _
    Public Sub ObtainTemplatePathTest()
        Dim target As TemplatesFactory = New TemplatesFactory ' TODO: Initialize to an appropriate value
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.ObtainTemplatePath(id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTemplates
    '''</summary>
    <TestMethod()> _
    Public Sub GetTemplatesTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = TemplatesFactory.GetTemplates
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteTemplate
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTemplateTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        TemplatesFactory.DeleteTemplate(id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for TemplatesFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub TemplatesFactoryConstructorTest()
        Dim target As TemplatesFactory = New TemplatesFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
