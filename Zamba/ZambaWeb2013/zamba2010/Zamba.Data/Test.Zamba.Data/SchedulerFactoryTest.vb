Imports System.Data

Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for SchedulerFactoryTest and is intended
'''to contain all SchedulerFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SchedulerFactoryTest


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
    '''A test for UpdateFolderConfig
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateFolderConfigTest()
        Dim sIntervalo As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim sAlarmTimeDisp As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim tipoConfig As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim idCarpeta As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim lDays() As Object = Nothing ' TODO: Initialize to an appropriate value
        SchedulerFactory.UpdateFolderConfig(sIntervalo, sAlarmTimeDisp, tipoConfig, idCarpeta, lDays)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for GetFolderConfig
    '''</summary>
    <TestMethod()> _
    Public Sub GetFolderConfigTest()
        Dim idTarea As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = SchedulerFactory.GetFolderConfig(idTarea)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SchedulerFactory Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub SchedulerFactoryConstructorTest()
        Dim target As SchedulerFactory = New SchedulerFactory
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
