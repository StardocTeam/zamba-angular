Imports System.Data

Imports System

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for ImportsFactoryTest and is intended
'''to contain all ImportsFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ImportsFactoryTest


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
    '''A test for UpdateRow
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateRowTest1()
        Dim Row As dsIPFolder.IP_FolderRow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = ImportsFactory.UpdateRow(Row)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateRow
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateRowTest()
        Dim row As DataRow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = ImportsFactory.UpdateRow(row)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for StoreRow
    '''</summary>
    <TestMethod()> _
    Public Sub StoreRowTest()
        Dim Row As dsIPFolder.IP_FolderRow = Nothing ' TODO: Initialize to an appropriate value
        Dim iId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = ImportsFactory.StoreRow(Row, iId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for StoreIpFolderRow
    '''</summary>
    <TestMethod()> _
    Public Sub StoreIpFolderRowTest()
        Dim Row As dsIPFolder.IP_FolderRow = Nothing ' TODO: Initialize to an appropriate value
        Dim iId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim actual As [Decimal]
        actual = ImportsFactory.StoreIpFolderRow(Row, iId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for InsertIPLIST
    '''</summary>
    <TestMethod()> _
    Public Sub InsertIPLISTTest()
        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Description As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Enabled As Integer = 0 ' TODO: Initialize to an appropriate value
        ImportsFactory.InsertIPLIST(Name, Description, Enabled)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetRowByID
    '''</summary>
    <TestMethod()> _
    Public Sub GetRowByIDTest()
        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IPFolder As dsIPFolder = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As dsIPFolder = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As dsIPFolder
        actual = ImportsFactory.GetRowByID(Path, IPFolder)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetProcessList
    '''</summary>
    <TestMethod()> _
    Public Sub GetProcessListTest()
        Dim expected As DsIPList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsIPList
        actual = ImportsFactory.GetProcessList
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getProcess
    '''</summary>
    <TestMethod()> _
    Public Sub getProcessTest()
        Dim IPLIST As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DSIpType = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DSIpType
        actual = ImportsFactory.getProcess(IPLIST)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIPFOLDERCONFByFolderId
    '''</summary>
    <TestMethod()> _
    Public Sub GetIPFOLDERCONFByFolderIdTest()
        Dim FolderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As dsIPFolderConf = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As dsIPFolderConf
        actual = ImportsFactory.GetIPFOLDERCONFByFolderId(FolderId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetFolderData
    '''</summary>
    <TestMethod()> _
    Public Sub GetFolderDataTest()
        Dim FolderPath As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As dsIPFolder = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As dsIPFolder
        actual = ImportsFactory.GetFolderData(FolderPath)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetConfigurations
    '''</summary>
    <TestMethod()> _
    Public Sub GetConfigurationsTest1()
        Dim IPFolder As dsIPFolder = Nothing ' TODO: Initialize to an appropriate value
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As dsIPFolder = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As dsIPFolder
        actual = ImportsFactory.GetConfigurations(IPFolder, UserId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetConfigurations
    '''</summary>
    <TestMethod()> _
    Public Sub GetConfigurationsTest()
        Dim IPFolder As dsIPFolder = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As dsIPFolder = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As dsIPFolder
        actual = ImportsFactory.GetConfigurations(IPFolder)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As ImportsFactory = New ImportsFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelIPLIST
    '''</summary>
    <TestMethod()> _
    Public Sub DelIPLISTTest()
        Dim Id As Integer = 0 ' TODO: Initialize to an appropriate value
        ImportsFactory.DelIPLIST(Id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteRow
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteRowTest()
        Dim Row As dsIPFolder.IP_FolderRow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = ImportsFactory.DeleteRow(Row)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ImportsFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub ImportsFactoryConstructorTest()
        Dim target As ImportsFactory = New ImportsFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
