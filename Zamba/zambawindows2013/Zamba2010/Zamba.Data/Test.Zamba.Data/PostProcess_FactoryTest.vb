Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for PostProcess_FactoryTest and is intended
'''to contain all PostProcess_FactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class PostProcess_FactoryTest


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
    '''A test for SetSin_InbrokerProcessFile
    '''</summary>
    <TestMethod()> _
    Public Sub SetSin_InbrokerProcessFileTest()
        Dim idSiniestro As Integer = 0 ' TODO: Initialize to an appropriate value
        PostProcess_Factory.SetSin_InbrokerProcessFile(idSiniestro)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetSin_InbrokerProcess
    '''</summary>
    <TestMethod()> _
    Public Sub SetSin_InbrokerProcessTest()
        Dim idSiniestro As Integer = 0 ' TODO: Initialize to an appropriate value
        PostProcess_Factory.SetSin_InbrokerProcess(idSiniestro)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetPol_InbrokerProcessFile
    '''</summary>
    <TestMethod()> _
    Public Sub SetPol_InbrokerProcessFileTest()
        Dim id As String = String.Empty ' TODO: Initialize to an appropriate value
        PostProcess_Factory.SetPol_InbrokerProcessFile(id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetPol_InbrokerProcess
    '''</summary>
    <TestMethod()> _
    Public Sub SetPol_InbrokerProcessTest()
        Dim id As String = String.Empty ' TODO: Initialize to an appropriate value
        PostProcess_Factory.SetPol_InbrokerProcess(id)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetORD_InbrokerProcessFile
    '''</summary>
    <TestMethod()> _
    Public Sub SetORD_InbrokerProcessFileTest()
        Dim nroOrden As Integer = 0 ' TODO: Initialize to an appropriate value
        PostProcess_Factory.SetORD_InbrokerProcessFile(nroOrden)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetORD_InbrokerProcess
    '''</summary>
    <TestMethod()> _
    Public Sub SetORD_InbrokerProcessTest()
        Dim nroOrden As Integer = 0 ' TODO: Initialize to an appropriate value
        PostProcess_Factory.SetORD_InbrokerProcess(nroOrden)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetODP_ZambaProcess
    '''</summary>
    <TestMethod()> _
    Public Sub SetODP_ZambaProcessTest()
        Dim VoucherNum As Integer = 0 ' TODO: Initialize to an appropriate value
        PostProcess_Factory.SetODP_ZambaProcess(VoucherNum)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for PostProcess_Factory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub PostProcess_FactoryConstructorTest()
        Dim target As PostProcess_Factory = New PostProcess_Factory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
