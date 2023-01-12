Imports System.Collections

Imports Zamba.Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for WFStepsFactoryTest and is intended
'''to contain all WFStepsFactoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WFStepsFactoryTest


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
    '''A test for GetStepById
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepByIdTest()
        Dim stepId As Long = 72 ' TODO: Initialize to an appropriate value
        Dim expected As IWFStep = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As IWFStep
        actual = WFStepsFactory.GetStepById(stepId)
        Assert.AreNotEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetStepsByWorkId
    '''</summary>
    <TestMethod()> _
    Public Sub GetStepsByWorkIdTest()
        Dim WfId As Long = 21 ' TODO: Initialize to an appropriate value
        Dim expected As SortedList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As SortedList
        actual = WFStepsFactory.GetStepsByWorkId(WfId)
        Assert.AreNotEqual(expected, actual)
        Assert.IsTrue(DirectCast(actual, IWorkFlow).Steps(0).wfstates.count > 0)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
