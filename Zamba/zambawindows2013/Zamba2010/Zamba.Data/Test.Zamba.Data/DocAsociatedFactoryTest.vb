Imports System.Data

Imports System.Collections

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for DocAsociatedFactoryTest and is intended
'''to contain all DocAsociatedFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class DocAsociatedFactoryTest


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
    '''A test for IfDocAsocExists
    '''</summary>
    <TestMethod()> _
    Public Sub IfDocAsocExistsTest()
        Dim DocId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim FolderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = DocAsociatedFactory.IfDocAsocExists(DocId, FolderId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocTypeAsociation
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocTypeAsociationTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = DocAsociatedFactory.GetDocTypeAsociation(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocAsociatedCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocAsociatedCountTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Short = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Short
        actual = DocAsociatedFactory.GetDocAsociatedCount(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDocAsoc
    '''</summary>
    <TestMethod()> _
    Public Sub GetDocAsocTest()
        Dim FolderId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim ArrayDocTypes As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = DocAsociatedFactory.GetDocAsoc(FolderId, ArrayDocTypes)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As DocAsociatedFactory = New DocAsociatedFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteAsociaton
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteAsociatonTest()
        Dim DT1Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DT2Id As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Ind1 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Ind2 As Integer = 0 ' TODO: Initialize to an appropriate value
        DocAsociatedFactory.DeleteAsociaton(DT1Id, DT2Id, Ind1, Ind2)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AssocitedAlredyExist
    '''</summary>
    <TestMethod()> _
    Public Sub AssocitedAlredyExistTest()
        Dim DocType1 As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim DocType2 As DocType = Nothing ' TODO: Initialize to an appropriate value
        Dim Indice1 As Index = Nothing ' TODO: Initialize to an appropriate value
        Dim Indice2 As Index = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = DocAsociatedFactory.AssocitedAlredyExist(DocType1, DocType2, Indice1, Indice2)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AsociatedExists
    '''</summary>
    <TestMethod()> _
    Public Sub AsociatedExistsTest()
        Dim R As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim RExpected As TaskResult = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = DocAsociatedFactory.AsociatedExists(R)
        Assert.AreEqual(RExpected, R)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DocAsociatedFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub DocAsociatedFactoryConstructorTest()
        Dim target As DocAsociatedFactory = New DocAsociatedFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
