Imports Zamba.Core


Public Class UCIndexIndexerViewer
    Inherits ZControl
#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Trace.Indent()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instancio el UCIndexIndexerViewer")
        'Add any initialization after the InitializeComponent() call
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If Panel5 IsNot Nothing Then
                    For i As Int16 = 0 To Panel5.Controls.Count - 1
                        Panel5.Controls(i).Dispose()
                    Next
                    Panel5.Dispose()
                    Panel5 = Nothing
                End If
                If Result IsNot Nothing Then
                    Result.Dispose()
                    Result = Nothing
                End If
                If hashindexs IsNot Nothing Then
                    hashindexs.Clear()
                    hashindexs = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Public WithEvents Panel5 As ZPanel
    '  Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    '  Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    '   Friend WithEvents PicBox As System.Windows.Forms.PictureBox
    ' Friend WithEvents DsResults1 As DsResults
    '  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    '   Friend WithEvents Timer1 As System.Windows.Forms.Timer
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel5 = New ZPanel()
        SuspendLayout
        '
        'Panel5
        '
        Panel5.AutoScroll = true
        Panel5.BackColor = Color.White
        Panel5.Dock = DockStyle.Fill
        Panel5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Panel5.ForeColor = Color.FromArgb(76, 76, 76)
        Panel5.Location = New Point(0, 0)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(202, 320)
        Panel5.TabIndex = 2
        '
        'UCIndexIndexerViewer
        '
        AllowDrop = true
        BackColor = Color.White
        CausesValidation = false
        Controls.Add(Panel5)
        Font = New Font("Tahoma", 9.75!, FontStyle.Bold)
        Location = New Point(143, 0)
        Name = "UCIndexIndexerViewer"
        Size = New Size(202, 320)
        ResumeLayout(false)

    End Sub

#End Region

    Dim Result As IResult
    Private LastShowedDocId As Int64 = 0

    Private Sub GeneracionIndices(ByRef Result As IResult, Optional ByVal ReplaceData As Boolean = False)
        Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(Me.Result.DocTypeId, Membership.MembershipHelper.CurrentUser.ID, True, True)
        Dim i As Integer
        For i = Result.Indexs.Count - 1 To 0 Step -1

            'Verifico si el dato ya fue cargado en la hash
            If Not hashindexs.ContainsKey(DirectCast(Me.Result.Indexs(i), Index).ID) Then
                If Not String.IsNullOrEmpty(DirectCast(Me.Result.Indexs(i), Index).DataTemp) Then
                    hashindexs.Add(DirectCast(Me.Result.Indexs(i), Index).ID, DirectCast(Me.Result.Indexs(i), Index).DataTemp)
                Else
                    hashindexs.Add(DirectCast(Me.Result.Indexs(i), Index).ID, String.Empty)
                End If
            ElseIf ReplaceData = False Then
                DirectCast(Me.Result.Indexs(i), Index).DataTemp = DirectCast(Result.Indexs(i), Index).DataTemp
                DirectCast(Me.Result.Indexs(i), Index).Data = DirectCast(Result.Indexs(i), Index).Data
            End If

            Try
                Dim ind As Index = DirectCast(Me.Result.Indexs(i), Index)
                Dim ctrl As New DisplayindexCtl(ind, True)
                RemoveHandler ctrl.DataChanged, AddressOf IndexsChanged
                RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                AddHandler ctrl.DataChanged, AddressOf IndexsChanged
                AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                AddHandler ctrl.DataChanged, AddressOf DataChanged
                ctrl.Dock = DockStyle.Top

                Dim IR As IndexsRightsInfo = DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo)
                Dim dsIndexsPropertys As DataSet = DocTypesBusiness.GetIndexsProperties(Me.Result.DocTypeId)

                For Each indexid As Int64 In IRI.Keys
                    If indexid = ctrl.Index.ID Then
                        For Each Index As DataRow In dsIndexsPropertys.Tables(0).Rows
                            If ctrl.Index.ID = CLng(Index("Index_Id")) AndAlso Index("MustComplete") = 1 _
                                AndAlso ctrl.Controls(1).Text.Contains("*") = False Then

                                ctrl.Controls(1).Text = ctrl.Controls(1).Text + " *"
                                Exit For
                            ElseIf IR.GetIndexRightValue(RightsType.IndexRequired) = True AndAlso ctrl.Controls(1).Text.Contains("*") = False Then
                                ctrl.Controls(1).Text = ctrl.Controls(1).Text + " *"
                                Exit For
                            End If
                        Next
                        Exit For
                    End If
                Next
                DelegateAddControls(ctrl)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Next

        Try
            'invierto tabindexs
            Dim tindex As Int32 = Panel5.Controls.Count - 1
            For Each control As Control In Panel5.Controls
                control.TabIndex = tindex
                tindex -= 1
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    'Private Sub autocomplete(ByRef Result As Zamba.Core.Result, ByVal index As Zamba.Core.Index)
    '    'Dim AC As Zamba.Codes.AutocompleteBC = Zamba.Codes.AutoCompleteBarcode_Factory.GetComplete(Me.CboDocType.SelectedValue, index.Id)
    '    'If Not IsNothing(AC) Then
    '    '    Me.LoadIndexViewer(AC.Complete(result, index))
    '    'End If
    'End Sub
    Private Sub IndexsChanged(ByVal Index As IIndex)

        RaiseEvent IndexChanged(Result, Index)

    End Sub
    Public Event IndexChanged(ByRef Result As IResult, ByVal Index As IIndex)
    Public Shadows Event EnterPressed()
    Private Sub Enter_KeyDown()
        RaiseEvent EnterPressed()
    End Sub
    Public Shadows Event TabPressed()
    Private Sub Tab_KeyDown()
        RaiseEvent TabPressed()
    End Sub
    Delegate Sub DAddControls()
    Private Ctrl As DisplayindexCtl
    Private Sub DelegateAddControls(ByVal Ctrl As DisplayindexCtl)
        Me.Ctrl = Ctrl
        Dim D1 As New DAddControls(AddressOf AddControls)
        Invoke(D1)
    End Sub
    Private Sub AddControls()
        Panel5.Controls.Add(Ctrl)
    End Sub
    Private Sub DataChanged(ByVal Index As IIndex)
        hashindexs(Index.ID) = Index.DataTemp
    End Sub

    Public Function IsValid() As Boolean
        ' Dim Indexs As New ArrayList
        For Each c As DisplayindexCtl In Panel5.Controls
            If c.isValid = False Then
                Return False
                Exit For
            End If
        Next
        Return True
    End Function

    Private Function GetIndexs() As ArrayList
        Dim result As New ArrayList

        For Each c As DisplayindexCtl In Panel5.Controls
            result.Add(c.Index)
        Next
        Return result
    End Function

    Private hashindexs As New Hashtable
    Public Sub ShowDocument(ByRef Result As IResult, ByVal ReplaceData As Boolean)
        Me.Result = Result

        Try
            Panel5.Visible = False
            Panel5.Controls.Clear()
            GeneracionIndices(Result)
            Panel5.Visible = True
        Catch ex As ComponentModel.Win32Exception
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            LastShowedDocId = Me.Result.DocTypeId
        End Try
        UserBusiness.Rights.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.View, Result.Name)
    End Sub

    ''' <summary>
    ''' [Sebastian] 03-07-09 MOIDFIED se corrigieron warnings
    ''' </summary>
    ''' <param name="ReplaceData"></param>
    ''' <remarks></remarks>
    Private Sub RefreshDatosIndices(Optional ByVal ReplaceData As Boolean = False)
        Dim i As Integer = Result.Indexs.Count - 1
        Dim b As Int32

        For i = Result.Indexs.Count - 1 To 0 Step -1

            'Verifico si el dato ya fue cargado en la hash
            If Not hashindexs.ContainsKey(DirectCast(Result.Indexs(i), Index).ID) Then
                'cargo el dato vacio
                If String.Compare(DirectCast(Result.Indexs(i), Index).Data, String.Empty) <> 0 Then
                    hashindexs.Add(DirectCast(Result.Indexs(i), Index).ID, DirectCast(Result.Indexs(i), Index).Data)
                Else
                    hashindexs.Add(DirectCast(Result.Indexs(i), Index).ID, String.Empty)
                End If
            Else
                If ReplaceData Then
                    hashindexs(DirectCast(Result.Indexs(i), Index).ID) = DirectCast(Result.Indexs(i), Index).Data
                    DirectCast(Result.Indexs(i), Index).DataTemp = DirectCast(Result.Indexs(i), Index).Data
                Else
                    DirectCast(Result.Indexs(i), Index).Data = hashindexs(DirectCast(Result.Indexs(i), Index).ID).ToString
                End If
            End If
            If Panel5.Controls.Count >= (b + 1) Then
                Dim ctrl As DisplayindexCtl = DirectCast(Panel5.Controls(b), DisplayindexCtl)
                Dim ind As Zamba.Core.Index = DirectCast(Result.Indexs(i), Index)
                ctrl.ReloadindexData(ind)
                b += +1
            End If
        Next
    End Sub


    Public Sub cleanIndexs()
        Try
            Dim i As Int32
            For i = 0 To Result.Indexs.Count - 1
                DirectCast(Result.Indexs(i), Index).Data = String.Empty
                DirectCast(Result.Indexs(i), Index).DataTemp = String.Empty
                DirectCast(Result.Indexs(i), Index).dataDescription = String.Empty
                DirectCast(Result.Indexs(i), Index).dataDescriptionTemp = String.Empty
            Next
            If IsNothing(hashindexs) = False Then hashindexs.Clear()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
