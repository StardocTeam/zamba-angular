Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for NegociosIntegridadTest and is intended
'''to contain all NegociosIntegridadTest Unit Tests
'''</summary>
<TestClass()> _
Public Class NegociosIntegridadTest


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
    '''A test for LogMsg
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub LogMsgTest()
        Dim target As NegociosIntegridad_Accessor = New NegociosIntegridad_Accessor ' TODO: Initialize to an appropriate value
        Dim iTipo As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim sMensaje As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = target.LogMsg(iTipo, sMensaje)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for insertarRelacionesNoUsadas
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub insertarRelacionesNoUsadasTest()
        Dim target As NegociosIntegridad_Accessor = New NegociosIntegridad_Accessor ' TODO: Initialize to an appropriate value
        Dim DtId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.insertarRelacionesNoUsadas(DtId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for insertarIndicesErroneos
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub insertarIndicesErroneosTest()
        Dim target As NegociosIntegridad_Accessor = New NegociosIntegridad_Accessor ' TODO: Initialize to an appropriate value
        Dim DrI As DrI = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.insertarIndicesErroneos(DrI)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for comprobarIntegridad
    '''</summary>
    <TestMethod()> _
    Public Sub comprobarIntegridadTest()
        Dim target As NegociosIntegridad = New NegociosIntegridad ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = target.comprobarIntegridad
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for NegociosIntegridad Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub NegociosIntegridadConstructorTest()
        Dim target As NegociosIntegridad = New NegociosIntegridad
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
