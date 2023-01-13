Imports System.Windows.Forms
Imports Zamba.Core
Imports Zamba.Indexs

''' <summary>
''' This class is used as a inputbox, but returns bolOK = false when it's canceled or closed
''' </summary>
''' <history>
''' Marcelo Modified 19/05/2008 - Se agrego la herencia a Zform
''' </history>
''' <remarks></remarks>
Public Class ConditionBox
    Inherits Zamba.AppBlock.ZForm

    Public Shared loadedControls As New Hashtable
    Dim _isZamba As Boolean

    Public Sub New(ByVal ListofConditions As List(Of ReportCondition))
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        Panel2.Visible = True
        Me.ListofConditions = ListofConditions
        Dim AlmostOneQuestion As Boolean

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        For Each reportcondition As ReportCondition In Me.ListofConditions
            If Not reportcondition.preg Is Nothing Then

                If Not IsNothing(reportcondition.conditions) AndAlso Not reportcondition.conditions.Equals(" IS NULL") AndAlso Not reportcondition.conditions.Equals(" IS NOT NULL") Then
                    AlmostOneQuestion = True
                    Dim conditionlabel As String

                    'conditionlabel.Text = If(reportcondition.preg IsNot Nothing, reportcondition.preg.Trim, String.Empty)
                    If reportcondition.conditions.ToUpper.Contains("LIKE") Then
                        conditionlabel = reportcondition.preg & "   " & "Contiene"
                    Else
                        conditionlabel = reportcondition.preg & "   " & reportcondition.conditions
                    End If

                    If reportcondition.currentIndex Is Nothing Then reportcondition.isZamba = False

                    _isZamba = reportcondition.isZamba

                    If reportcondition.isZamba Then

                        Dim _IndexControl As New DisplayindexCtl(reportcondition.currentIndex, True, conditionlabel)
                        _IndexControl.Dock = DockStyle.Top
                        Panel2.Controls.Add(_IndexControl)
                        Continue For
                    ElseIf Not IsNothing(reportcondition.tableName) AndAlso reportcondition.tableName.Length > 0 AndAlso reportcondition.indexName.Length > 0 Then

                        If reportcondition.tableName.IndexOf("SLST_S", StringComparison.CurrentCultureIgnoreCase) <> -1 OrElse reportcondition.tableName.IndexOf("ILST_I", StringComparison.CurrentCultureIgnoreCase) <> -1 Then

                            reportcondition.currentIndex = IndexsBussinesExt.getIndex(reportcondition.tableName.Substring(reportcondition.tableName.IndexOf("_") + 2, reportcondition.tableName.Length - reportcondition.tableName.IndexOf("_") - 2), True)
                            reportcondition.currentIndex.Operator = reportcondition.conditions

                            Dim _IndexControl As New DisplayindexCtl(reportcondition.currentIndex, True, conditionlabel)
                            _IndexControl.Dock = DockStyle.Top
                            Panel2.Controls.Add(_IndexControl)
                            Continue For
                        Else
                            Dim list As DataSet
                            Try
                                If reportcondition.tableName.ToUpper().Contains("DOC_I") Or reportcondition.tableName.ToUpper().StartsWith("DOC") Then

                                    Dim currentIndex As IIndex = IndexsBussinesExt.getIndex(reportcondition.indexId, True)

                                    If currentIndex IsNot Nothing AndAlso (currentIndex.DropDown = IndexAdditionalType.AutoSustitución OrElse currentIndex.DropDown = IndexAdditionalType.AutoSustituciónJerarquico OrElse currentIndex.Type = IndexDataType.Fecha OrElse currentIndex.Type = IndexDataType.Fecha_Hora) Then

                                        reportcondition.currentIndex = currentIndex
                                        reportcondition.currentIndex.Operator = reportcondition.conditions
                                        Dim _IndexControl As New DisplayindexCtl(reportcondition.currentIndex, True, conditionlabel)
                                        _IndexControl.Dock = DockStyle.Top
                                        Panel2.Controls.Add(_IndexControl)
                                        Continue For
                                    Else
                                        If IsNumeric(reportcondition.indexId) Then
                                            list = Servers.Server.Con.ExecuteDataset(CommandType.Text, String.Format("select distinct I{0} as ""{2}"" from {1} order by I{0}", reportcondition.indexId, reportcondition.tableName, reportcondition.indexName))
                                        Else
                                            list = Servers.Server.Con.ExecuteDataset(CommandType.Text, String.Format("select distinct I{0} as ""{2}"" from {1} order by I{0}", reportcondition.indexId, reportcondition.tableName, reportcondition.indexName))
                                        End If
                                    End If
                                ElseIf reportcondition.indexName.ToLower().Contains("fecha") Then
                                    If Servers.Server.isOracle Then
                                        ' list = Servers.Server.Con.ExecuteDataset(CommandType.Text, String.Format("select distinct {0} from {1} order by {0}", reportcondition.indexName, reportcondition.tableName))
                                    Else
                                        'list = Servers.Server.Con.ExecuteDataset(CommandType.Text, String.Format("select distinct {0} from {1} order by {0}", reportcondition.indexName, reportcondition.tableName))
                                    End If

                                Else
                                        list = Servers.Server.Con.ExecuteDataset(CommandType.Text, String.Format("select distinct {0} from {1} order by {0}", reportcondition.indexName, reportcondition.tableName))
                                End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try

                            If Not list Is Nothing AndAlso Not list.Tables.Count = 0 AndAlso list.Tables(0).Rows.Count > 0 Then

                                Dim conditionComboBox As New ComboBox
                                Dim row As DataRow = list.Tables(0).NewRow()
                                list.Tables(0).Rows.InsertAt(row, 0)

                                Dim ConditionLabelControl As New Label
                                ConditionLabelControl.Text = conditionlabel
                                ConditionLabelControl.Width = 180

                                conditionComboBox.DataSource = list.Tables(0)
                                conditionComboBox.DisplayMember = reportcondition.indexName
                                conditionComboBox.Top = ConditionLabelControl.Top
                                conditionComboBox.Width = 500
                                conditionComboBox.Left = ConditionLabelControl.Right + 10
                                conditionComboBox.Tag = reportcondition.indexName

                                Dim conditionpanel As New Panel
                                conditionpanel.Dock = DockStyle.Top
                                conditionpanel.Controls.Add(ConditionLabelControl)
                                conditionpanel.Controls.Add(conditionComboBox)
                                conditionpanel.Height = 35
                                Panel2.Controls.Add(conditionpanel)
                                Continue For
                            ElseIf reportcondition.indexName.ToLower().Contains("fecha") Then
                                Dim conditionTextBox As New DateTimePicker
                                conditionTextBox.Format = DateTimePickerFormat.Short

                                Dim ConditionLabelControl As New Label
                                ConditionLabelControl.Text = conditionlabel
                                ConditionLabelControl.Width = 180

                                conditionTextBox.Top = ConditionLabelControl.Top
                                conditionTextBox.Width = 500
                                conditionTextBox.Left = ConditionLabelControl.Right + 10
                                conditionTextBox.Tag = reportcondition.indexName

                                Dim conditionpanel As New Panel
                                conditionpanel.Dock = DockStyle.Top
                                conditionpanel.Controls.Add(ConditionLabelControl)
                                conditionpanel.Controls.Add(conditionTextBox)
                                conditionpanel.Height = 35

                                Panel2.Controls.Add(conditionpanel)
                                Continue For

                            Else

                                Dim conditionTextBox As New TextBox

                                Dim ConditionLabelControl As New Label
                                ConditionLabelControl.Text = conditionlabel
                                ConditionLabelControl.Width = 180

                                conditionTextBox.Top = ConditionLabelControl.Top
                                conditionTextBox.Width = 500
                                conditionTextBox.Left = ConditionLabelControl.Right + 10
                                conditionTextBox.Tag = reportcondition.indexName

                                Dim conditionpanel As New Panel
                                conditionpanel.Dock = DockStyle.Top
                                conditionpanel.Controls.Add(ConditionLabelControl)
                                conditionpanel.Controls.Add(conditionTextBox)
                                conditionpanel.Height = 35

                                Panel2.Controls.Add(conditionpanel)
                                Continue For
                            End If
                        End If
                    Else
                        Dim conditionTextBox As New TextBox
                        Dim ConditionLabelControl As New Label
                        ConditionLabelControl.Text = conditionlabel
                        ConditionLabelControl.Width = 180

                        conditionTextBox.Top = ConditionLabelControl.Top
                        conditionTextBox.Width = 500
                        conditionTextBox.Left = ConditionLabelControl.Right + 10
                        conditionTextBox.Tag = reportcondition.indexName

                        Dim conditionpanel As New Panel
                        conditionpanel.Dock = DockStyle.Top
                        conditionpanel.Controls.Add(ConditionLabelControl)
                        conditionpanel.Controls.Add(conditionTextBox)
                        conditionpanel.Height = 35

                        Panel2.Controls.Add(conditionpanel)
                        Continue For
                    End If
                Else
                    reportcondition.values = reportcondition.conditions
                End If
            End If
        Next

        If AlmostOneQuestion = False Then
            bolOk = True
        End If
    End Sub

    Public bolOk As Boolean
    Public ListofConditions As List(Of ReportCondition)
    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
        For Each reportcondition As ReportCondition In ListofConditions

            For Each control As Control In Panel2.Controls

                If TypeOf control Is Panel Then

                    For Each c As Control In control.Controls

                        If TypeOf c Is DisplayindexCtl Then
                            If reportcondition.indexName = DirectCast(c, DisplayindexCtl).Index.Name AndAlso reportcondition.conditions = DirectCast(control, DisplayindexCtl).Index.Operator Then
                                reportcondition.values = DirectCast(c, DisplayindexCtl).Index.DataTemp
                                Exit For
                            End If
                        ElseIf TypeOf c Is TextBox Then
                            If reportcondition.indexName = DirectCast(c, TextBox).Tag Then
                                reportcondition.values = DirectCast(c, TextBox).Text
                                Exit For
                            End If
                        ElseIf TypeOf c Is ComboBox Then
                            If reportcondition.indexName = DirectCast(c, ComboBox).Tag Then
                                reportcondition.values = DirectCast(c, ComboBox).Text
                                Exit For
                            End If
                        End If
                    Next

                Else
                    If TypeOf control Is DisplayindexCtl Then
                        If reportcondition.indexName = DirectCast(control, DisplayindexCtl).Index.Name AndAlso reportcondition.conditions = DirectCast(control, DisplayindexCtl).Index.Operator Then
                            reportcondition.values = DirectCast(control, DisplayindexCtl).Index.DataTemp
                            Exit For
                        End If
                    ElseIf TypeOf control Is TextBox Then
                        If reportcondition.indexName = DirectCast(control, TextBox).Tag Then
                            reportcondition.values = DirectCast(control, TextBox).Text
                            Exit For
                        End If
                    ElseIf TypeOf control Is ComboBox Then
                        If reportcondition.indexName = DirectCast(control, ComboBox).Tag Then
                            reportcondition.values = DirectCast(control, ComboBox).Text
                            Exit For
                        End If
                    End If
                End If
            Next
        Next
        bolOk = True
        Close()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click

        For Each control As Control In Panel2.Controls
            If TypeOf control Is DisplayindexCtl Then
                CType(control, DisplayindexCtl).Clean()
            ElseIf TypeOf control Is TextBox Then
                CType(control, TextBox).Clear()
            ElseIf TypeOf control Is ComboBox Then
                Dim combo As ComboBox = CType(control, ComboBox)
                combo.SelectedIndex = -1
                combo.SelectedItem = Nothing
                combo.Text = String.Empty
            End If
        Next
    End Sub

End Class
