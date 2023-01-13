Imports System

Imports System.Collections

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for ZForo_FactoryTest and is intended
'''to contain all ZForo_FactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ZForo_FactoryTest


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
    '''A test for SiguienteParent
    '''</summary>
    <TestMethod()> _
    Public Sub SiguienteParentTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim IdMensaje As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = ZForo_Factory.SiguienteParent(DocId, IdMensaje)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SiguienteId
    '''</summary>
    <TestMethod()> _
    Public Sub SiguienteIdTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = ZForo_Factory.SiguienteId(DocId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for InsertMessage
    '''</summary>
    <TestMethod()> _
    Public Sub InsertMessageTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Doctypeid As Long = 0 ' TODO: Initialize to an appropriate value
        Dim IdMensaje As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ParentId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim LinkName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Mensaje As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Fecha As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim UserId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim StateId As Integer = 0 ' TODO: Initialize to an appropriate value
        ZForo_Factory.InsertMessage(DocId, Doctypeid, IdMensaje, ParentId, LinkName, Mensaje, Fecha, UserId, StateId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetAllMessages
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllMessagesTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = ZForo_Factory.GetAllMessages(DocId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllAnswers
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllAnswersTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = ZForo_Factory.GetAllAnswers(DocId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As ZForo_Factory = New ZForo_Factory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteMessage
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteMessageTest()
        Dim DocId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim ParentId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim IdMensaje As Integer = 0 ' TODO: Initialize to an appropriate value
        ZForo_Factory.DeleteMessage(DocId, ParentId, IdMensaje)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for ZForo_Factory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub ZForo_FactoryConstructorTest()
        Dim target As ZForo_Factory = New ZForo_Factory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
