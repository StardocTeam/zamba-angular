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
        Text = r.Name
        Result = r
        ImageIndex = r.IconId
        SubItems.Add(asignedToName)
        SubItems.Add(Result.StepId)
        SubItems.Add(Result.TaskState.ToString)
        SubItems.Add(Result.State.Name)
        If Not Result.ExpireDate = #12:00:00 AM# Then
            SubItems.Add(Result.ExpireDate.ToString)
        Else
            SubItems.Add("")
        End If
        CheckExpired()
    End Sub
#End Region

    Public Sub CheckExpired()
        If Not Result.ExpireDate = #12:00:00 AM# Then
            If Result.IsExpired Then
                ForeColor = Color.Red
                Font = New Font("Tahoma", 8.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
            Else
                ForeColor = Color.FromArgb(76,76,76)
                Font = New Font("Tahoma", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            End If
        Else
            ForeColor = Color.FromArgb(76,76,76)
            Font = New Font("Tahoma", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub
End Class
