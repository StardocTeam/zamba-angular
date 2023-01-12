Imports System.Collections

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Office



'''<summary>
'''This is a test class for OutlookInteropTest and is intended
'''to contain all OutlookInteropTest Unit Tests
'''</summary>
<TestClass()> _
Public Class OutlookInteropTest


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
    '''A test for GetNewMailItem
    '''</summary>
    <TestMethod()> _
    Public Sub GetNewMailItemTest()
        Throw New NotImplementedException("Rehacer test")
        'Dim target As OutlookInterop = New OutlookInterop ' TODO: Initialize to an appropriate value
        'Dim tempPath As String = "C:\Documents and Settings\Oscar Sanchez\Application Data\Zamba Software\1.msg" ' TODO: Initialize to an appropriate value
        'Dim modal As Boolean = True ' TODO: Initialize to an appropriate value
        'Dim waitForSend As Boolean = True ' TODO: Initialize to an appropriate value
        'Dim Params As New Hashtable  ' TODO: Initialize to an appropriate value
        'Dim ParamsExpected As New Hashtable  ' TODO: Initialize to an appropriate value
        'Dim automaticSend As Boolean = False ' TODO: Initialize to an appropriate value
        'Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        'Dim actual As Boolean

        'Params.Add("ReplyMail", True)
        'Params.Add("ReplyMsgPath", "C:\Documents and Settings\Oscar Sanchez\Application Data\Zamba Software\1.msg")

        'Try
        '    actual = target.GetNewMailItem(tempPath, modal, waitForSend, Params, automaticSend)
        'Catch ex As Exception
        '    Throw ex
        'End Try
        'Assert.AreEqual(ParamsExpected, Params)
        'Assert.AreEqual(expected, actual)
        'Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
