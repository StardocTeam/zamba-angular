Imports System.Collections.Generic
Imports Zamba.Core

''' <summary>
''' dialog que solicita tipo de formulario dinamico para funcion replicar
''' </summary>
''' <remarks></remarks>
''' <history>dalbarellos 17.04.2009</history>
Public Class frmRequestFormType
    Sub New(ByVal types As List(Of String), Optional ByVal actualtype As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call

        For Each Item As String In types
            ComboBox1.Items.Add(Item)
        Next
        ComboBox1.Items.Remove(actualtype)
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles OK_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub


    ''' <summary>
    ''' propiedad que expone el tipo de form seleccionado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 17.04.2009</history>
    Public ReadOnly Property SelectedType() As String
        Get

            Return [Enum].Parse(GetType(FormTypes), ComboBox1.SelectedItem.ToString).GetHashCode.ToString()
        End Get
    End Property



End Class
