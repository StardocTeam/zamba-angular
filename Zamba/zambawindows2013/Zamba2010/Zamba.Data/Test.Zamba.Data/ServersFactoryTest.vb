Imports System.Collections.Generic

Imports Zamba.Servers

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for ServersFactoryTest and is intended
'''to contain all ServersFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ServersFactoryTest


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
    '''A test for server
    '''</summary>
    <TestMethod()> _
    Public Sub serverTest()
        Dim expected As Server = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Server
        ServersFactory.server = expected
        actual = ServersFactory.server
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IsConnectionValid
    '''</summary>
    <TestMethod()> _
    Public Sub IsConnectionValidTest()
        Dim intServerType As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim serverName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim dataBase As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim conUser As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim conPass As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = ServersFactory.IsConnectionValid(intServerType, serverName, dataBase, conUser, conPass)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetServerTypes
    '''</summary>
    <TestMethod()> _
    Public Sub GetServerTypesTest()
        Dim expected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of String)
        actual = ServersFactory.GetServerTypes
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetServerType
    '''</summary>
    <TestMethod()> _
    Public Sub GetServerTypeTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = ServersFactory.GetServerType
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Event_SessionTimeOut
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub Event_SessionTimeOutTest()
        ServersFactory_Accessor.Event_SessionTimeOut()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Event_ConnectionTerminated
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub Event_ConnectionTerminatedTest()
        ServersFactory_Accessor.Event_ConnectionTerminated()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ConvertDate
    '''</summary>
    <TestMethod()> _
    Public Sub ConvertDateTest()
        Dim pDate As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = ServersFactory.ConvertDate(pDate)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for BuildExecuteScalar
    '''</summary>
    <TestMethod()> _
    Public Sub BuildExecuteScalarTest1()
        Dim servertype As Server.DBTYPES = New Server.DBTYPES ' TODO: Initialize to an appropriate value
        Dim dbname As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim dbpassword As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim dbuser As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim servidor As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _commandType As CommandType = New CommandType ' TODO: Initialize to an appropriate value
        Dim commandText As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Object
        actual = ServersFactory.BuildExecuteScalar(servertype, dbname, dbpassword, dbuser, servidor, _commandType, commandText)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for BuildExecuteScalar
    '''</summary>
    <TestMethod()> _
    Public Sub BuildExecuteScalarTest()
        Dim _commandType As CommandType = New CommandType ' TODO: Initialize to an appropriate value
        Dim commandText As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Object
        actual = ServersFactory.BuildExecuteScalar(_commandType, commandText)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for BuildExecuteNonQuery
    '''</summary>
    <TestMethod()> _
    Public Sub BuildExecuteNonQueryTest1()
        Dim servertype As Server.DBTYPES = New Server.DBTYPES ' TODO: Initialize to an appropriate value
        Dim dbname As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim dbpassword As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim dbuser As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim servidor As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _commandType As CommandType = New CommandType ' TODO: Initialize to an appropriate value
        Dim commandText As String = String.Empty ' TODO: Initialize to an appropriate value
        ServersFactory.BuildExecuteNonQuery(servertype, dbname, dbpassword, dbuser, servidor, _commandType, commandText)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for BuildExecuteNonQuery
    '''</summary>
    <TestMethod()> _
    Public Sub BuildExecuteNonQueryTest()
        Dim _commandType As CommandType = New CommandType ' TODO: Initialize to an appropriate value
        Dim commandText As String = String.Empty ' TODO: Initialize to an appropriate value
        ServersFactory.BuildExecuteNonQuery(_commandType, commandText)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for BuildExecuteDataSet
    '''</summary>
    <TestMethod()> _
    Public Sub BuildExecuteDataSetTest1()
        Dim servertype As Server.DBTYPES = New Server.DBTYPES ' TODO: Initialize to an appropriate value
        Dim dbname As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim dbpassword As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim dbuser As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim servidor As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _commandType As CommandType = New CommandType ' TODO: Initialize to an appropriate value
        Dim commandText As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = ServersFactory.BuildExecuteDataSet(servertype, dbname, dbpassword, dbuser, servidor, _commandType, commandText)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for BuildExecuteDataSet
    '''</summary>
    <TestMethod()> _
    Public Sub BuildExecuteDataSetTest()
        Dim _commandType As CommandType = New CommandType ' TODO: Initialize to an appropriate value
        Dim commandText As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = ServersFactory.BuildExecuteDataSet(_commandType, commandText)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ServersFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub ServersFactoryConstructorTest()
        Dim target As ServersFactory = New ServersFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
