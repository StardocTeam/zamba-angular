Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for UpdaterFactoryTest and is intended
'''to contain all UpdaterFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UpdaterFactoryTest


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
    '''A test for UpdateEstreg
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateEstregTest()
        Dim strVersion As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strMachineName As String = String.Empty ' TODO: Initialize to an appropriate value
        UpdaterFactory.UpdateEstreg(strVersion, strMachineName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetEstreg
    '''</summary>
    <TestMethod()> _
    Public Sub SetEstregTest()
        Dim strLastId As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strMachineName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUserName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim srtVersion As String = String.Empty ' TODO: Initialize to an appropriate value
        UpdaterFactory.SetEstreg(strLastId, strMachineName, strUserName, srtVersion)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetVersionByReflection
    '''</summary>
    <TestMethod()> _
    Public Sub GetVersionByReflectionTest()
        Dim dllName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = UpdaterFactory.GetVersionByReflection(dllName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVerreg
    '''</summary>
    <TestMethod()> _
    Public Sub GetVerregTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UpdaterFactory.GetVerreg
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetMaxIdFromEstreg
    '''</summary>
    <TestMethod()> _
    Public Sub GetMaxIdFromEstregTest()
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = UpdaterFactory.GetMaxIdFromEstreg
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetLastestVersionFromVerreg
    '''</summary>
    <TestMethod()> _
    Public Sub GetLastestVersionFromVerregTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = UpdaterFactory.GetLastestVersionFromVerreg
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetLastestVersion
    '''</summary>
    <TestMethod()> _
    Public Sub GetLastestVersionTest()
        Dim strLastestVersion As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strLastestVersionExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUpdatePath As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strUpdatePathExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strCommand As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim strCommandExpected As String = String.Empty ' TODO: Initialize to an appropriate value
        UpdaterFactory.GetLastestVersion(strLastestVersion, strUpdatePath, strCommand)
        Assert.AreEqual(strLastestVersionExpected, strLastestVersion)
        Assert.AreEqual(strUpdatePathExpected, strUpdatePath)
        Assert.AreEqual(strCommandExpected, strCommand)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetIdFromEstregWhereMName
    '''</summary>
    <TestMethod()> _
    Public Sub GetIdFromEstregWhereMNameTest()
        Dim strMachineName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UpdaterFactory.GetIdFromEstregWhereMName(strMachineName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteFromEstregWhereMName
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteFromEstregWhereMNameTest()
        Dim strMachineName As String = String.Empty ' TODO: Initialize to an appropriate value
        UpdaterFactory.DeleteFromEstregWhereMName(strMachineName)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdaterFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub UpdaterFactoryConstructorTest()
        Dim target As UpdaterFactory = New UpdaterFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
