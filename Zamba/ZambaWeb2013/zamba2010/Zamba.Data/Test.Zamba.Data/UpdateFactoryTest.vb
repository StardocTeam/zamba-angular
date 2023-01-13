Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for UpdateFactoryTest and is intended
'''to contain all UpdateFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UpdateFactoryTest


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
    '''A test for UsuariosDesactualizadosCount
    '''</summary>
    <TestMethod()> _
    Public Sub UsuariosDesactualizadosCountTest()
        Dim ServerVersion As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = UpdateFactory.UsuariosDesactualizadosCount(ServerVersion)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UsuariosActualizadosCount
    '''</summary>
    <TestMethod()> _
    Public Sub UsuariosActualizadosCountTest()
        Dim ServerVersion As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = UpdateFactory.UsuariosActualizadosCount(ServerVersion)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Usuarios_Desactualizados
    '''</summary>
    <TestMethod()> _
    Public Sub Usuarios_DesactualizadosTest()
        Dim ServerVersion As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = UpdateFactory.Usuarios_Desactualizados(ServerVersion)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Usuarios_Actualizados
    '''</summary>
    <TestMethod()> _
    Public Sub Usuarios_ActualizadosTest()
        Dim ServerVersion As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = UpdateFactory.Usuarios_Actualizados(ServerVersion)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetServerData
    '''</summary>
    <TestMethod()> _
    Public Sub GetServerDataTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = UpdateFactory.GetServerData
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ForzarActualizar
    '''</summary>
    <TestMethod()> _
    Public Sub ForzarActualizarTest()
        Dim winuser As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim serverversion As String = String.Empty ' TODO: Initialize to an appropriate value
        UpdateFactory.ForzarActualizar(winuser, serverversion)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ChangeServerVersion
    '''</summary>
    <TestMethod()> _
    Public Sub ChangeServerVersionTest()
        Dim newversion As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim path As String = String.Empty ' TODO: Initialize to an appropriate value
        UpdateFactory.ChangeServerVersion(newversion, path)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateFactoryConstructorTest()
        Dim target As UpdateFactory = New UpdateFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
