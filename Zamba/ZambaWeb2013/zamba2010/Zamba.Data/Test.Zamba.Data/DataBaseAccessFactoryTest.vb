Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for DataBaseAccessFactoryTest and is intended
'''to contain all DataBaseAccessFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class DataBaseAccessFactoryTest


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
    '''A test for GetTabla
    '''</summary>
    <TestMethod()> _
    Public Sub GetTablaTest()
        Dim IDConsulta As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = DataBaseAccessFactory.GetTabla(IDConsulta)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetQuerys
    '''</summary>
    <TestMethod()> _
    Public Sub GetQuerysTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DataBaseAccessFactory.GetQuerys
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDscolumns
    '''</summary>
    <TestMethod()> _
    Public Sub GetDscolumnsTest()
        Dim IDConsulta As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DataBaseAccessFactory.GetDscolumns(IDConsulta)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDsClaves
    '''</summary>
    <TestMethod()> _
    Public Sub GetDsClavesTest()
        Dim IDConsulta As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DataBaseAccessFactory.GetDsClaves(IDConsulta)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllZQColumnsDs
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllZQColumnsDsTest()
        Dim id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DataBaseAccessFactory.GetAllZQColumnsDs(id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ExecuteAndGetDs
    '''</summary>
    <TestMethod()> _
    Public Sub ExecuteAndGetDsTest1()
        Dim ServerType As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim ServerName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim DBName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim DBUser As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim DBPassword As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim sql As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DataBaseAccessFactory.ExecuteAndGetDs(ServerType, ServerName, DBName, DBUser, DBPassword, sql)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ExecuteAndGetDs
    '''</summary>
    <TestMethod()> _
    Public Sub ExecuteAndGetDsTest()
        Dim sql As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DataBaseAccessFactory.ExecuteAndGetDs(sql)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DataBaseAccessFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub DataBaseAccessFactoryConstructorTest()
        Dim target As DataBaseAccessFactory = New DataBaseAccessFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
