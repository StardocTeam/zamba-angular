Imports System.Data

Imports System.Collections

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for VolumeListsFactoryTest and is intended
'''to contain all VolumeListsFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class VolumeListsFactoryTest


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
    '''A test for IsDuplicate
    '''</summary>
    <TestMethod()> _
    Public Sub IsDuplicateTest()
        Dim name As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumeListsFactory.IsDuplicate(name)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDiskGroupVolumes
    '''</summary>
    <TestMethod()> _
    Public Sub GetDiskGroupVolumesTest()
        Dim DiskGroupId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = VolumeListsFactory.GetDiskGroupVolumes(DiskGroupId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDiskGroupsList
    '''</summary>
    <TestMethod()> _
    Public Sub GetDiskGroupsListTest()
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = VolumeListsFactory.GetDiskGroupsList
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getAllLists
    '''</summary>
    <TestMethod()> _
    Public Sub getAllListsTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = VolumeListsFactory.getAllLists
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetActiveDiskGroupVolumes
    '''</summary>
    <TestMethod()> _
    Public Sub GetActiveDiskGroupVolumesTest()
        Dim DiskGroupId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = VolumeListsFactory.GetActiveDiskGroupVolumes(DiskGroupId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As VolumeListsFactory = New VolumeListsFactory ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DelFromDiskGroup
    '''</summary>
    <TestMethod()> _
    Public Sub DelFromDiskGroupTest()
        Dim VolsId As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim DiskGroupId As Integer = 0 ' TODO: Initialize to an appropriate value
        VolumeListsFactory.DelFromDiskGroup(VolsId, DiskGroupId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DELDiskGroup
    '''</summary>
    <TestMethod()> _
    Public Sub DELDiskGroupTest()
        Dim DiskGroupid As Integer = 0 ' TODO: Initialize to an appropriate value
        VolumeListsFactory.DELDiskGroup(DiskGroupid)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddtoDiskGroup
    '''</summary>
    <TestMethod()> _
    Public Sub AddtoDiskGroupTest()
        Dim Vols As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim DiskGroupId As Integer = 0 ' TODO: Initialize to an appropriate value
        VolumeListsFactory.AddtoDiskGroup(Vols, DiskGroupId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddDiskGroup
    '''</summary>
    <TestMethod()> _
    Public Sub AddDiskGroupTest()
        Dim DiskGroupName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = VolumeListsFactory.AddDiskGroup(DiskGroupName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for VolumeListsFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub VolumeListsFactoryConstructorTest()
        Dim target As VolumeListsFactory = New VolumeListsFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
