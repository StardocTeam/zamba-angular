Imports Zamba.Core
Imports Zamba.AppBlock
''' <summary>
''' 
''' </summary>
''' <history>
''' Marcelo Modified 19/05/2008 - Se agrego la herencia a Zform
''' </history>
''' <remarks></remarks>
Public Class frmcreateviews
    Inherits ZForm
    Dim CRV As New Zamba.Core.CreateTablesBusiness

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Generate()
    End Sub

    Private Sub frmcreateviews_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LoadDocTypes()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Dim DocTypes As New ArrayList
    Private Sub LoadDocTypes()
        Try
            DocTypes = Zamba.Core.DocTypesBusiness.GetDocTypesArrayList
            For Each DT As DocType In DocTypes
                Me.CheckedListBox1.Items.Add(DT.Name)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Generate()
        Try
            Dim i As Int32
            Me.ListBox1.Items.Clear()
            Dim DT As DocType = Nothing
            For i = 0 To Me.CheckedListBox1.CheckedIndices.Count - 1
                Try
                    DT = Me.DocTypes(Me.CheckedListBox1.CheckedIndices(i))
                    CRV.CreateFriendlyView(DT)
                    Me.ListBox1.Items.Add(DT.Name & " OK")
                Catch ex As Exception
                    Try
                        Me.ListBox1.Items.Add("ERROR " & DT.Name & " " & ex.ToString)
                    Catch exc As Exception
                        Me.ListBox1.Items.Add("Vista ERROR " & ex.ToString)
                    End Try
                End Try
                Application.DoEvents()
            Next
            MsgBox("Creacion finalizada")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub chkselectall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkselectall.CheckedChanged
        Try
            Dim i As Int32
            For i = 0 To Me.CheckedListBox1.Items.Count - 1
                Me.CheckedListBox1.SetItemChecked(i, Me.chkselectall.Checked)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
