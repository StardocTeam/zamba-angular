﻿Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for DrITest and is intended
'''to contain all DrITest Unit Tests
'''</summary>
<TestClass()> _
Public Class DrITest


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
    '''A test for comprobarIntegridad
    '''</summary>
    <TestMethod()> _
    Public Sub comprobarIntegridadTest()
        Dim target As DrI = New DrI ' TODO: Initialize to an appropriate value
        Dim DocI As DocI = Nothing ' TODO: Initialize to an appropriate value
        Dim DocD As DocI = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = target.comprobarIntegridad(DocI, DocD)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DrI Constructor
    '''</summary>
    <TestMethod()> _
    Public Sub DrIConstructorTest()
        Dim target As DrI = New DrI
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
