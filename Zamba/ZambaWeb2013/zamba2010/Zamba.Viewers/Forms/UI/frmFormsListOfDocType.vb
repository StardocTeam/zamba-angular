Imports System.Windows.Forms
Imports Zamba.Core


Public Class frmFormsListOfDocType

    Private _form() As ZwebForm


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal forms() As ZwebForm)
        InitializeComponent()

        LoadEditFormToList(forms)
        _form = forms

    End Sub

    Public Property FormSelected() As ZwebForm()
        Get
            Return _form
        End Get
        Set(ByVal value() As ZwebForm)
            _form = value
        End Set
    End Property
    Private Sub LoadEditFormToList(ByVal forms() As ZwebForm)

        RemoveHandler lstFormList.SelectedIndexChanged, AddressOf lstFormList_SelectedIndexChanged

        Dim FormAux(forms.Length - 1) As ZwebForm
        Dim LastPath As New Hashtable
        Dim Position As Int32 = 0

        For Each Form As ZwebForm In forms
            'filtro para no mostrar los distintos tipos de formularios para un mismo doctype. ya que eso no me interesa
            'solo interesa que el usuario elija uno de ellos para luego usarlo, sino prestaria a confusion el ver tantos
            'formularios que solo se diferencian en si son edit, show, etc.
            If LastPath.ContainsKey(Form.Path) = False Then
                FormAux.SetValue(Form, Position)
                LastPath.Add(Form.Path, Form.Name)
                Position += 1
            End If
        Next
        Array.Resize(FormAux, Position)
        'FormsToAdd.CopyTo(FormAux, 0)
        lstFormList.DataSource = FormAux
        lstFormList.DisplayMember = "Name"
        lstFormList.ValueMember = "Path"
        AddHandler lstFormList.SelectedIndexChanged, AddressOf lstFormList_SelectedIndexChanged
    End Sub


    Private Sub lstFormList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFormList.SelectedIndexChanged
        Dim FormArray(_form.Length - 1) As ZwebForm
        Dim Position As Int32 = 0

        If lstFormList.Items.Count > 0 Then
            For Each Form As ZwebForm In _form
                If String.Compare(Form.Path, CType(lstFormList.SelectedValue, String)) = 0 Then
                    FormArray.SetValue(Form, Position)
                    Position += 1
                End If
            Next
            FormArray.Resize(FormArray, Position)
        End If

        _form = FormArray
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class