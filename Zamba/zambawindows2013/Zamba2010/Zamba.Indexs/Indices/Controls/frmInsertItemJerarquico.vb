Imports System.Collections.Generic
Imports Zamba.Core

Public Class frmInsertItemJerarquico
    Inherits ZForm
    Implements IDisposable

    ''' <summary>
    ''' Esta clase se utiliza para enviar los codigo/descripcion formateados de una slst
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CodeDesc
        Public Sub New(ByVal code As String, ByVal description As String)
            Me.Code = code
            Me.Description = description
        End Sub
        Public Property Code() As String
        Public Property Description() As String
    End Class

    Private _indexId As Int32
    Private _codigosExistentes As List(Of String)

    'se modifico para que acepte codigos alfanumericos[sebastian 11/12/2008]
    'Public Event NewItem(ByVal IndexId As Int32, ByVal codigo As Integer, ByVal descripcion As String)
    Public Event NewItem(ByVal IndexId As Int32, ByVal codigo As String, ByVal descripcion As String)

    Private Sub New()
        InitializeComponent()
    End Sub
    Private Sub New(ByVal indexId As Int64)
        Me.New()
        _indexId = indexId
    End Sub
    'Public Sub New(ByVal indexId As Int32, ByVal existingCodes As Generic.List(Of Integer))
    Public Sub New(ByVal indexId As Int64, ByVal existingCodes As List(Of String), ByVal indexParentID As Int64)
        'se modifico para que acepte codigos alfanumericos[sebastian 11/12/2008]
        Me.New(indexId)
        _codigosExistentes = existingCodes

        Dim parentIndType As IndexAdditionalType = IndexsBusiness.GetIndexDropDownType(indexParentID)
        Dim childIndType As IndexAdditionalType = IndexsBusiness.GetIndexDropDownType(indexId)

        If parentIndType = IndexAdditionalType.AutoSustitución OrElse parentIndType = IndexAdditionalType.AutoSustituciónJerarquico Then
            cmbParent.DataSource = FormatItmes(AutoSubstitutionBusiness.GetIndexData(indexParentID, True))
            cmbParent.DisplayMember = "Description"
            cmbParent.ValueMember = "Code"
        Else
            Dim List As List(Of String) = IndexsBusiness.GetDropDownList(CInt(indexParentID))
            If Not List Is Nothing Then cmbParent.DataSource = List
        End If

        If childIndType = IndexAdditionalType.AutoSustitución OrElse childIndType = IndexAdditionalType.AutoSustituciónJerarquico Then
            cmbChild.DataSource = FormatItmes(AutoSubstitutionBusiness.GetIndexData(indexId, True))
            cmbChild.DisplayMember = "Description"
            cmbChild.ValueMember = "Code"
        Else
            Dim List As List(Of String) = IndexsBusiness.GetDropDownList(CInt(IndexID))
            If Not List Is Nothing then cmbChild.DataSource = List
        End If
    End Sub

    ''' <summary>
    ''' Devuelve una lista de objetos codigo/descripcion con la descipcion armada con <codigo> - <descripcion>
    ''' </summary>
    ''' <param name="dataTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatItmes(ByVal dataTable As DataTable) As List(Of CodeDesc)
        Return (From row As DataRow In dataTable.Rows
                Select New CodeDesc(row("Codigo"), row("Codigo") & " - " & row("Descripcion"))).ToList()
    End Function

    Private Sub cmdAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdAceptar.Click
        Try
            If CamposValidos() Then
                _codigosExistentes.Add(cmbParent.SelectedValue & "|" & cmbChild.SelectedValue)
                'se modifico para que acepte codigos alfanumericos[sebastian 11/12/2008]
                'RaiseEvent NewItem(_indexId, Int32.Parse(txtCodigo.Text), txtDescripcion.Text)
                RaiseEvent NewItem(_indexId, cmbParent.SelectedValue, cmbChild.SelectedValue)
            Else
                Exit Sub
            End If
            cmbChild.SelectedIndex = 0
            cmbChild.Focus()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function CamposValidos() As Boolean
        If cmbParent.SelectedIndex < 0 Then
            MessageBox.Show("El valor padre no puede estar vacio.", "Error", MessageBoxButtons.OK)
            cmbParent.Focus()

            Return False
        ElseIf cmbChild.SelectedIndex < 0 Then
            MessageBox.Show("El valor hijo no puede estar vacio.", "Error", MessageBoxButtons.OK)
            cmbChild.Focus()

            Return False
        ElseIf _codigosExistentes.Contains(cmbParent.SelectedValue & "|" & cmbChild.SelectedValue) Then
            MessageBox.Show("La combinación seleccionada ya existe en la lista.", "Error", MessageBoxButtons.OK)
            cmbParent.Focus()

            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Event ModifiedIndex(ByVal Codigo As String, ByVal description As String)
    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnModify.Click
        RaiseEvent ModifiedIndex(cmbParent.SelectedValue, cmbChild.SelectedValue)
        DialogResult = DialogResult.OK
        Close()
    End Sub
End Class