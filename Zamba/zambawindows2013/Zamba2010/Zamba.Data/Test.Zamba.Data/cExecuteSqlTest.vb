Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for cExecuteSqlTest and is intended
'''to contain all cExecuteSqlTest Unit Tests
'''</summary>
<TestClass()> _
Public Class cExecuteSqlTest


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
    '''A test for SQL
    '''</summary>
    <TestMethod()> _
    Public Sub SQLTest()
        Dim target As cExecuteSql = New cExecuteSql ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        target.SQL = expected
        actual = target.SQL
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for QueryType
    '''</summary>
    <TestMethod()> _
    Public Sub QueryTypeTest()
        Dim target As cExecuteSql = New cExecuteSql ' TODO: Initialize to an appropriate value
        Dim expected As cExecuteSql.ReturnType = New cExecuteSql.ReturnType ' TODO: Initialize to an appropriate value
        Dim actual As cExecuteSql.ReturnType
        target.QueryType = expected
        actual = target.QueryType
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ExecuteSQL
    '''</summary>
    <TestMethod()> _
    Public Sub ExecuteSQLTest()
        Dim target As cExecuteSql = New cExecuteSql ' TODO: Initialize to an appropriate value
        Dim rettype As cExecuteSql.ReturnType = New cExecuteSql.ReturnType ' TODO: Initialize to an appropriate value
        Dim sql As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Object
        actual = target.ExecuteSQL(rettype, sql)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub DisposeTest1()
        Dim target As cExecuteSql_Accessor = New cExecuteSql_Accessor ' TODO: Initialize to an appropriate value
        Dim disposing As Boolean = False ' TODO: Initialize to an appropriate value
        target.Dispose(disposing)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As cExecuteSql = New cExecuteSql ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for cExecuteSql Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub cExecuteSqlConstructorTest()
        Dim target As cExecuteSql = New cExecuteSql
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
