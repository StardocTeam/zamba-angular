Imports System.Data

Imports System.Collections

Imports Zamba.Core

Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for SchedulesFactoryTest and is intended
'''to contain all SchedulesFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SchedulesFactoryTest


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
    '''A test for updateTask
    '''</summary>
    <TestMethod()> _
    Public Sub updateTaskTest()
        Dim _id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _fechaini As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim _frecuencia As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _fechafin As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim _ultima As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim _activo As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _id_reporte As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _formato As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _carpeta As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _imprimir As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _min As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _desc As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim origen As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim repet As Integer = 0 ' TODO: Initialize to an appropriate value
        SchedulesFactory.updateTask(_id, _fechaini, _frecuencia, _fechafin, _ultima, _activo, _id_reporte, _formato, _carpeta, _imprimir, _min, _desc, origen, repet)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for setRepeticiones
    '''</summary>
    <TestMethod()> _
    Public Sub setRepeticionesTest()
        Dim repet As Integer = 0 ' TODO: Initialize to an appropriate value
        SchedulesFactory.setRepeticiones(repet)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for setIdReporte
    '''</summary>
    <TestMethod()> _
    Public Sub setIdReporteTest()
        Dim reporte As Integer = 0 ' TODO: Initialize to an appropriate value
        SchedulesFactory.setIdReporte(reporte)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for setFechaInicial
    '''</summary>
    <TestMethod()> _
    Public Sub setFechaInicialTest()
        Dim fecha_ini As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        SchedulesFactory.setFechaInicial(fecha_ini)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for setFechaFinal
    '''</summary>
    <TestMethod()> _
    Public Sub setFechaFinalTest()
        Dim fecha_fin As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        SchedulesFactory.setFechaFinal(fecha_fin)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for setActivo
    '''</summary>
    <TestMethod()> _
    Public Sub setActivoTest()
        Dim act As Integer = 0 ' TODO: Initialize to an appropriate value
        SchedulesFactory.setActivo(act)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for reprogramarTarea
    '''</summary>
    <TestMethod()> _
    Public Sub reprogramarTareaTest()
        Dim Tarea As csTarea = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = SchedulesFactory.reprogramarTarea(Tarea)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetSchedules
    '''</summary>
    <TestMethod()> _
    Public Sub GetSchedulesTest1()
        Dim ObjectTypeId As ObjectTypes = New ObjectTypes ' TODO: Initialize to an appropriate value
        Dim ObjectId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = SchedulesFactory.GetSchedules(ObjectTypeId, ObjectId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetSchedules
    '''</summary>
    <TestMethod()> _
    Public Sub GetSchedulesTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = SchedulesFactory.GetSchedules
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetScheduleByTaskId
    '''</summary>
    <TestMethod()> _
    Public Sub GetScheduleByTaskIdTest()
        Dim strid As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = SchedulesFactory.GetScheduleByTaskId(strid)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetReportesIds
    '''</summary>
    <TestMethod()> _
    Public Sub GetReportesIdsTest()
        Dim expected As DataSet = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As DataSet
        actual = SchedulesFactory.GetReportesIds
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getRepeticiones
    '''</summary>
    <TestMethod()> _
    Public Sub getRepeticionesTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = SchedulesFactory.getRepeticiones
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetMaxId
    '''</summary>
    <TestMethod()> _
    Public Sub GetMaxIdTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = SchedulesFactory.GetMaxId
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getMailTo
    '''</summary>
    <TestMethod()> _
    Public Sub getMailToTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = SchedulesFactory.getMailTo
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getMailCCO
    '''</summary>
    <TestMethod()> _
    Public Sub getMailCCOTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = SchedulesFactory.getMailCCO
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getMailCC
    '''</summary>
    <TestMethod()> _
    Public Sub getMailCCTest()
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = SchedulesFactory.getMailCC
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getIdReporte
    '''</summary>
    <TestMethod()> _
    Public Sub getIdReporteTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = SchedulesFactory.getIdReporte
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getFechaInicial
    '''</summary>
    <TestMethod()> _
    Public Sub getFechaInicialTest()
        Dim expected As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim actual As DateTime
        actual = SchedulesFactory.getFechaInicial
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getFechaFinal
    '''</summary>
    <TestMethod()> _
    Public Sub getFechaFinalTest()
        Dim expected As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim actual As DateTime
        actual = SchedulesFactory.getFechaFinal
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetDESByReporteId
    '''</summary>
    <TestMethod()> _
    Public Sub GetDESByReporteIdTest()
        Dim strRepId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = SchedulesFactory.GetDESByReporteId(strRepId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for getActivo
    '''</summary>
    <TestMethod()> _
    Public Sub getActivoTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = SchedulesFactory.getActivo
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteSchedule
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteScheduleTest()
        Dim TaskId As Integer = 0 ' TODO: Initialize to an appropriate value
        SchedulesFactory.DeleteSchedule(TaskId)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for CheckSchedule
    '''</summary>
    <TestMethod()> _
    Public Sub CheckScheduleTest()
        Dim Schedule As Schedule = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = SchedulesFactory.CheckSchedule(Schedule)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for cargarTareasHoy
    '''</summary>
    <TestMethod()> _
    Public Sub cargarTareasHoyTest()
        Dim Prog As iProgramador = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Hashtable
        actual = SchedulesFactory.cargarTareasHoy(Prog)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for addTask
    '''</summary>
    <TestMethod()> _
    Public Sub addTaskTest()
        Dim _id As Long = 0 ' TODO: Initialize to an appropriate value
        Dim _fechaini As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim _frecuencia As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _fechafin As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim _ultima As DateTime = New DateTime ' TODO: Initialize to an appropriate value
        Dim _activo As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _id_reporte As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _formato As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _carpeta As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _imprimir As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim _min As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim _desc As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim origen As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim repet As Integer = 0 ' TODO: Initialize to an appropriate value
        SchedulesFactory.addTask(_id, _fechaini, _frecuencia, _fechafin, _ultima, _activo, _id_reporte, _formato, _carpeta, _imprimir, _min, _desc, origen, repet)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for addMails
    '''</summary>
    <TestMethod()> _
    Public Sub addMailsTest()
        Dim mto As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim mcc As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim mcco As String = String.Empty ' TODO: Initialize to an appropriate value
        SchedulesFactory.addMails(mto, mcc, mcco)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for SchedulesFactory Constructor
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub SchedulesFactoryConstructorTest()
        Dim target As SchedulesFactory_Accessor = New SchedulesFactory_Accessor
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
