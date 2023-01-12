Imports System.Data

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Zamba.Data



'''<summary>
'''This is a test class for WFTasksFactory_TaskLogInformationTest and is intended
'''to contain all WFTasksFactory_TaskLogInformationTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WFTasksFactory_TaskLogInformationTest


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
    '''A test for WorkflowName
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub WorkflowNameTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.WorkflowName
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for WorkflowId
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub WorkflowIdTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = target.WorkflowId
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for TaskStateName
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub TaskStateNameTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.TaskStateName
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for TaskStateId
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub TaskStateIdTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.TaskStateId
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for TaskName
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub TaskNameTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.TaskName
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for StepName
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub StepNameTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.StepName
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for StepId
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub StepIdTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = target.StepId
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for FolderID
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub FolderIDTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = target.FolderID
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DocTypeName
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub DocTypeNameTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.DocTypeName
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DocTypeID
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub DocTypeIDTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(param0) ' TODO: Initialize to an appropriate value
        Dim actual As Long
        actual = target.DocTypeID
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetLogInformation
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub GetLogInformationTest()
        Dim taskId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim expected As WFTasksFactory_Accessor.TaskLogInformation = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As WFTasksFactory_Accessor.TaskLogInformation
        actual = WFTasksFactory_Accessor.TaskLogInformation.GetLogInformation(taskId)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Dispose
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub DisposeTest()
        'Class inheritance across assemblies is not preserved by private accessors. However, a static method AttachShadow() is provided in each private accessor class to transfer a private object from one private accessor class to another.
        Assert.Inconclusive("Class inheritance across assemblies is not preserved by private accessors. Howeve" & _
                "r, a static method AttachShadow() is provided in each private accessor class to " & _
                "transfer a private object from one private accessor class to another.")
    End Sub

    '''<summary>
    '''A test for BuildLogInformation
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub BuildLogInformationTest()
        Dim dr As DataRow = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As WFTasksFactory_Accessor.TaskLogInformation = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As WFTasksFactory_Accessor.TaskLogInformation
        actual = WFTasksFactory_Accessor.TaskLogInformation.BuildLogInformation(dr)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for TaskLogInformation Constructor
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Zamba.Data.dll")> _
    Public Sub WFTasksFactory_TaskLogInformationConstructorTest()
        Dim documentName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim docTypeID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim docTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim taskStateId As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim taskStateName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim folderID As Long = 0 ' TODO: Initialize to an appropriate value
        Dim stepId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim stepName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim workflowId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim workflowName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim target As WFTasksFactory_Accessor.TaskLogInformation = New WFTasksFactory_Accessor.TaskLogInformation(documentName, docTypeID, docTypeName, taskStateId, taskStateName, folderID, stepId, stepName, workflowId, workflowName)
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
