Imports System.Data

Imports System.Collections

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for IndexLinkFactoryTest and is intended
'''to contain all IndexLinkFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class IndexLinkFactoryTest


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
    '''A test for UpdateInfo
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateInfoTest()
        Dim ili As IndexLinkInfo = Nothing ' TODO: Initialize to an appropriate value
        IndexLinkFactory.UpdateInfo(ili)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Update
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateTest()
        Dim il As IndexLink = Nothing ' TODO: Initialize to an appropriate value
        IndexLinkFactory.Update(il)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveNewLink
    '''</summary>
    <TestMethod()> _
    Public Sub SaveNewLinkTest()
        Dim ii As IndexLink = Nothing ' TODO: Initialize to an appropriate value
        IndexLinkFactory.SaveNewLink(ii)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SaveLinkInfo
    '''</summary>
    <TestMethod()> _
    Public Sub SaveLinkInfoTest()
        Dim linfo As IndexLinkInfo = Nothing ' TODO: Initialize to an appropriate value
        IndexLinkFactory.SaveLinkInfo(linfo)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for linkindexs
    '''</summary>
    <TestMethod()> _
    Public Sub linkindexsTest()
        Dim DocTypeId1 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DocTypeId2 As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Index1 As Index = Nothing ' TODO: Initialize to an appropriate value
        Dim Index2 As Index = Nothing ' TODO: Initialize to an appropriate value
        Dim CheckAll As Boolean = False ' TODO: Initialize to an appropriate value
        IndexLinkFactory.linkindexs(DocTypeId1, DocTypeId2, Index1, Index2, CheckAll)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetLinks
    '''</summary>
    <TestMethod()> _
    Public Sub GetLinksTest()
        Dim IndexLink As IndexLink = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = IndexLinkFactory.GetLinks(IndexLink)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetlinkedIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub GetlinkedIndexsTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = IndexLinkFactory.GetlinkedIndexs
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetindexsLinks
    '''</summary>
    <TestMethod()> _
    Public Sub GetindexsLinksTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = IndexLinkFactory.GetindexsLinks
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteLinkedIndexs
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteLinkedIndexsTest()
        Dim DocTypeName1 As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim DocTypeName2 As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IndexName1 As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim IndexName2 As String = String.Empty ' TODO: Initialize to an appropriate value
        IndexLinkFactory.DeleteLinkedIndexs(DocTypeName1, DocTypeName2, IndexName1, IndexName2)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteindexLinkInfo
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteindexLinkInfoTest()
        Dim ili As IndexLinkInfo = Nothing ' TODO: Initialize to an appropriate value
        IndexLinkFactory.DeleteindexLinkInfo(ili)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Delete
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTest()
        Dim il As IndexLink = Nothing ' TODO: Initialize to an appropriate value
        IndexLinkFactory.Delete(il)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddLinkInfo
    '''</summary>
    <TestMethod()> _
    Public Sub AddLinkInfoTest()
        Dim indexlink As IndexLink = Nothing ' TODO: Initialize to an appropriate value
        Dim linfo As IndexLinkInfo = Nothing ' TODO: Initialize to an appropriate value
        IndexLinkFactory.AddLinkInfo(indexlink, linfo)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for IndexLinkFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub IndexLinkFactoryConstructorTest()
        Dim target As IndexLinkFactory = New IndexLinkFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
