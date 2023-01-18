Imports System

Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for NotesFactoryTest and is intended
'''to contain all NotesFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class NotesFactoryTest


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
    '''A test for SaveNote
    '''</summary>
    <TestMethod()> _
    Public Sub SaveNoteTest()
        Dim query As String = String.Empty ' TODO: Initialize to an appropriate value
        NotesFactory.SaveNote(query)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Save
    '''</summary>
    <TestMethod()> _
    Public Sub SaveTest()
        Dim NoteText As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim X As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim Y As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim NoteId As Integer = 0 ' TODO: Initialize to an appropriate value
        NotesFactory.Save(NoteText, X, Y, NoteId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for NewNote
    '''</summary>
    <TestMethod()> _
    Public Sub NewNoteTest()
        Dim table As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim columns As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Values As String = String.Empty ' TODO: Initialize to an appropriate value
        NotesFactory.NewNote(table, columns, Values)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for MoveNotes
    '''</summary>
    <TestMethod()> _
    Public Sub MoveNotesTest()
        Dim OldDOCID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim NewDOCID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim Mover As Boolean = False ' TODO: Initialize to an appropriate value
        NotesFactory.MoveNotes(OldDOCID, NewDOCID, Mover)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetNotes
    '''</summary>
    <TestMethod()> _
    Public Sub GetNotesTest()
        Dim Document_Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = NotesFactory.GetNotes(Document_Id)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As NotesFactory = New NotesFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteNotes
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteNotesTest()
        Dim DocumentId As Long = 0 ' TODO: Initialize to an appropriate value
        NotesFactory.DeleteNotes(DocumentId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest()
        Dim NoteId As Integer = 0 ' TODO: Initialize to an appropriate value
        NotesFactory.Delete(NoteId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for NotesFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub NotesFactoryConstructorTest()
        Dim target As NotesFactory = New NotesFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
