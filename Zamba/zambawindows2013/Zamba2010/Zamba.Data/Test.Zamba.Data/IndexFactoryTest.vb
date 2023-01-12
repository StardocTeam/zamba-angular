Imports System.Collections.Generic

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for IndexFactoryTest and is intended
'''to contain all IndexFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class IndexFactoryTest


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
    '''A test for GetIndexValues
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexValuesTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim indexIds As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Dictionary(Of Long, String) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Dictionary(Of Long, String)
        actual = IndexFactory.GetIndexValues(taskId, docTypeId, indexIds)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexsTest()
        Dim docTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = IndexFactory.GetIndexs(docTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IndexFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub IndexFactoryConstructorTest()
        Dim target As IndexFactory = New IndexFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
