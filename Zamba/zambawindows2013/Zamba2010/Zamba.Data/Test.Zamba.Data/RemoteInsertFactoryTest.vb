Imports System.Collections.Generic

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for RemoteInsertFactoryTest and is intended
'''to contain all RemoteInsertFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class RemoteInsertFactoryTest


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
    '''A test for SaveDocumentInserted
    '''</summary>
    <TestMethod()> _
    Public Sub SaveDocumentInsertedTest()
        Dim temporaryId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim statusId As Integer = 0 ' TODO: Initialize to an appropriate value
        RemoteInsertFactory.SaveDocumentInserted(temporaryId, statusId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveDocumentError
    '''</summary>
    <TestMethod()> _
    Public Sub SaveDocumentErrorTest()
        Dim temporaryId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim errorMessage As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim statusId As Integer = 0 ' TODO: Initialize to an appropriate value
        RemoteInsertFactory.SaveDocumentError(temporaryId, errorMessage, statusId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RemoveDocumentsToInsert
    '''</summary>
    <TestMethod()> _
    Public Sub RemoveDocumentsToInsertTest()
        Dim ids As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        RemoteInsertFactory.RemoveDocumentsToInsert(ids)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetDocumentsToInsert
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocumentsToInsertTest()
        Dim expected As DataTable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataTable
        actual = RemoteInsertFactory.GetDocumentsToInsert
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for RemoteInsertFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub RemoteInsertFactoryConstructorTest()
        Dim target As RemoteInsertFactory = New RemoteInsertFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
