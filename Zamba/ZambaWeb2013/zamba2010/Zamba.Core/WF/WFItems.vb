Imports System.Windows.Forms
Imports System.Drawing

Public Class ListViewItemTask
    Inherits ListViewItem

#Region " Atributos "
    Private _result As ITaskResult
#End Region

#Region " Propiedades "
    Public Property Result() As ITaskResult
        Get
            Return _result
        End Get
        Set(ByVal Value As iTaskResult)
            _result = Value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal r As TaskResult, ByVal asignedToName As String)
        MyBase.New()
        Me.Text = r.Name
        Me.Result = r
        Me.ImageIndex = r.IconId
        Me.SubItems.Add(asignedToName)
        Me.SubItems.Add(Result.WfStep.Name)
        Me.SubItems.Add(Result.TaskState.ToString)
        Me.SubItems.Add(Result.State.Name)
        If Not Result.ExpireDate = #12:00:00 AM# Then
            Me.SubItems.Add(Result.ExpireDate.ToString)
        Else
            Me.SubItems.Add("")
        End If
        CheckExpired()
    End Sub
#End Region

    Public Sub CheckExpired()
        If Not Result.ExpireDate = #12:00:00 AM# Then
            If Me.Result.IsExpired Then
                Me.ForeColor = Color.Red
                Me.Font = New Font("Tahoma", 8.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
            Else
                Me.ForeColor = Color.Black
                Me.Font = New Font("Tahoma", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            End If
        Else
            Me.ForeColor = Color.Black
            Me.Font = New Font("Tahoma", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub
End Class
