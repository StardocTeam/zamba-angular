Imports System

Imports System.Collections

Imports System.Data

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for VolumesFactoryTest and is intended
'''to contain all VolumesFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class VolumesFactoryTest


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
    '''A test for VolumePath
    '''</summary>
    <TestMethod()> _
    Public Sub VolumePathTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = VolumesFactory.VolumePath(Volume, DocTypeId)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for VolIsEmpty
    '''</summary>
    <TestMethod()> _
    Public Sub VolIsEmptyTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.VolIsEmpty(Volume)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for VerificoPath
    '''</summary>
    <TestMethod()> _
    Public Sub VerificoPathTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.VerificoPath(Volume)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ValidateOffSet
    '''</summary>
    <TestMethod()> _
    Public Sub ValidateOffSetTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.ValidateOffSet(Volume)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateVolume
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateVolumeTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim VolumeExpected As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim NewName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim NewPath As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim NewSize As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim NewType As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim NewState As Integer = 0 ' TODO: Initialize to an appropriate value
        VolumesFactory.UpdateVolume(Volume, NewName, NewPath, NewSize, NewType, NewState)
        Assert.AreEqual(VolumeExpected, Volume)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for UpdateFilesinVol
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub UpdateFilesinVolTest()
        Dim DoctypeId As Long = 0 ' TODO: Initialize to an appropriate value
        VolumesFactory_Accessor.UpdateFilesinVol(DoctypeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetStateFull
    '''</summary>
    <TestMethod()> _
    Public Sub SetStateFullTest()
        Dim VolumeId As Integer = 0 ' TODO: Initialize to an appropriate value
        VolumesFactory.SetStateFull(VolumeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SetLastOffSetUsed
    '''</summary>
    <TestMethod()> _
    Public Sub SetLastOffSetUsedTest()
        Dim LastOffsetUsed As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim VolumeId As Integer = 0 ' TODO: Initialize to an appropriate value
        VolumesFactory.SetLastOffSetUsed(LastOffsetUsed, VolumeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for RetrieveVolumePath
    '''</summary>
    <TestMethod()> _
    Public Sub RetrieveVolumePathTest()
        Dim VolumeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = VolumesFactory.RetrieveVolumePath(VolumeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for RetrieveDiskGroupId
    '''</summary>
    <TestMethod()> _
    Public Sub RetrieveDiskGroupIdTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = VolumesFactory.RetrieveDiskGroupId(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for ObtengoElSiguienteVolumen
    '''</summary>
    <TestMethod()> _
    Public Sub ObtengoElSiguienteVolumenTest()
        Dim DsVols As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Volume = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Volume
        actual = VolumesFactory.ObtengoElSiguienteVolumen(DsVols, DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for LoadVolume
    '''</summary>
    <TestMethod()> _
    Public Sub LoadVolumeTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim DsVols As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IVolume
        actual = VolumesFactory.LoadVolume(DocTypeId, DsVols)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for isvolumepathInUse
    '''</summary>
    <TestMethod()> _
    Public Sub isvolumepathInUseTest()
        Dim VolPath As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.isvolumepathInUse(VolPath)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IsVolumeDuplicated
    '''</summary>
    <TestMethod()> _
    Public Sub IsVolumeDuplicatedTest()
        Dim VolumeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.IsVolumeDuplicated(VolumeName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IsValidType
    '''</summary>
    <TestMethod()> _
    Public Sub IsValidTypeTest()
        Dim Type As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.IsValidType(Type)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IsValidSize
    '''</summary>
    <TestMethod()> _
    Public Sub IsValidSizeTest()
        Dim Size As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.IsValidSize(Size)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for IsValidPath
    '''</summary>
    <TestMethod()> _
    Public Sub IsValidPathTest()
        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.IsValidPath(Path)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVolumes
    '''</summary>
    <TestMethod()> _
    Public Sub GetVolumesTest1()
        Dim TemporalVolumes As Boolean = False ' TODO: Initialize to an appropriate value
        Dim VolName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = VolumesFactory.GetVolumes(TemporalVolumes, VolName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVolumes
    '''</summary>
    <TestMethod()> _
    Public Sub GetVolumesTest()
        Dim VolumeListId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = VolumesFactory.GetVolumes(VolumeListId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVolumenPathByVolId
    '''</summary>
    <TestMethod()> _
    Public Sub GetVolumenPathByVolIdTest()
        Dim volid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = VolumesFactory.GetVolumenPathByVolId(volid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVolumeListId
    '''</summary>
    <TestMethod()> _
    Public Sub GetVolumeListIdTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = VolumesFactory.GetVolumeListId(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVolumeData
    '''</summary>
    <TestMethod()> _
    Public Sub GetVolumeDataTest1()
        Dim VolumeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IVolume
        actual = VolumesFactory.GetVolumeData(VolumeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVolumeData
    '''</summary>
    <TestMethod()> _
    Public Sub GetVolumeDataTest()
        Dim Volume As Volume = Nothing ' TODO: Initialize to an appropriate value
        Dim VolumeExpected As Volume = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IVolume
        actual = VolumesFactory.GetVolumeData(Volume)
        Assert.AreEqual(VolumeExpected, Volume)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetVolume
    '''</summary>
    <TestMethod()> _
    Public Sub GetVolumeTest()
        Dim VolumeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IVolume
        actual = VolumesFactory.GetVolume(VolumeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTemporalVolume
    '''</summary>
    <TestMethod()> _
    Public Sub GetTemporalVolumeTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IVolume
        actual = VolumesFactory.GetTemporalVolume(DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRealSizeLen
    '''</summary>
    <TestMethod()> _
    Public Sub GetRealSizeLenTest1()
        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = VolumesFactory.GetRealSizeLen(Path)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRealSizeLen
    '''</summary>
    <TestMethod()> _
    Public Sub GetRealSizeLenTest()
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Offset As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = VolumesFactory.GetRealSizeLen(DocTypeId, Path, Offset)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetRealFiles
    '''</summary>
    <TestMethod()> _
    Public Sub GetRealFilesTest()
        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = VolumesFactory.GetRealFiles(Path)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetOffSet
    '''</summary>
    <TestMethod()> _
    Public Sub GetOffSetTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim VolumeExpected As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = VolumesFactory.GetOffSet(Volume)
        Assert.AreEqual(VolumeExpected, Volume)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetAllVolumes
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllVolumesTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = VolumesFactory.GetAllVolumes
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod()> _
    Public Sub DisposeTest()
        Dim target As VolumesFactory_Accessor = New VolumesFactory_Accessor ' TODO: Initialize to an appropriate value
        target.Dispose()
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteVolume
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteVolumeTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        VolumesFactory.DeleteVolume(Volume)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for DeleteFile
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteFileTest()
        Dim path As String = String.Empty ' TODO: Initialize to an appropriate value
        VolumesFactory.DeleteFile(path)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CreateDirVolume
    '''</summary>
    <TestMethod()> _
    Public Sub CreateDirVolumeTest1()
        Dim VolPath As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.CreateDirVolume(VolPath, DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CreateDirVolume
    '''</summary>
    <TestMethod()> _
    Public Sub CreateDirVolumeTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory.CreateDirVolume(Volume, DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CreateDirsDocTypeOffset
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub CreateDirsDocTypeOffsetTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim DocTypeId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = VolumesFactory_Accessor.CreateDirsDocTypeOffset(Volume, DocTypeId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for BorrarArchivos
    '''</summary>
    <TestMethod()> _
    Public Sub BorrarArchivosTest()
        Dim DocTypeId As Long = 0 ' TODO: Initialize to an appropriate value
        VolumesFactory.BorrarArchivos(DocTypeId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for AddVolume
    '''</summary>
    <TestMethod()> _
    Public Sub AddVolumeTest()
        Dim Volume As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim VolumeExpected As IVolume = Nothing ' TODO: Initialize to an appropriate value
        Dim Temporal As Boolean = False ' TODO: Initialize to an appropriate value
        VolumesFactory.AddVolume(Volume, Temporal)
        Assert.AreEqual(VolumeExpected, Volume)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for VolumesFactory Constructor
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub VolumesFactoryConstructorTest()
        Dim target As VolumesFactory_Accessor = New VolumesFactory_Accessor
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
