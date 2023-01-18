Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for IconsFactoryTest and is intended
'''to contain all IconsFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class IconsFactoryTest


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
    '''A test for UpdatePicturespath
    '''</summary>
    <TestMethod()> _
    Public Sub UpdatePicturespathTest()
        Dim Type As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
        IconsFactory.UpdatePicturespath(Type, Path)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateIconspath
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateIconspathTest()
        Dim Type As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
        IconsFactory.UpdateIconspath(Type, Path)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetPicturespath
    '''</summary>
    <TestMethod()> _
    Public Sub SetPicturespathTest()
        Dim Type As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
        IconsFactory.SetPicturespath(Type, Path)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetIconspath
    '''</summary>
    <TestMethod()> _
    Public Sub SetIconspathTest()
        Dim Type As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
        IconsFactory.SetIconspath(Type, Path)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetServerImagesPath
    '''</summary>
    <TestMethod()> _
    Public Sub GetServerImagesPathTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = IconsFactory.GetServerImagesPath
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetServerImages
    '''</summary>
    <TestMethod()> _
    Public Sub GetServerImagesTest()
        Dim expected As DsImageServer = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DsImageServer
        actual = IconsFactory.GetServerImages
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetPicturesPath
    '''</summary>
    <TestMethod()> _
    Public Sub GetPicturesPathTest()
        Dim Type As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = IconsFactory.GetPicturesPath(Type)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIconsPathString
    '''</summary>
    <TestMethod()> _
    Public Sub GetIconsPathStringTest()
        Dim iconKey As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = IconsFactory.GetIconsPathString(iconKey)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetIconsPath
    '''</summary>
    <TestMethod()> _
    Public Sub GetIconsPathTest()
        Dim IconsType As IconsFactory.IconsType = New IconsFactory.IconsType ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = IconsFactory.GetIconsPath(IconsType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As IconsFactory = New IconsFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for IconsFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub IconsFactoryConstructorTest()
        Dim target As IconsFactory = New IconsFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
