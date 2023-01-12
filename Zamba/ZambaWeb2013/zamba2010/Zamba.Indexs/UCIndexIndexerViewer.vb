Imports System.Windows.Forms
Imports ZAMBA.Core
Imports ZAMBA.AppBlock


Public Class UCIndexIndexerViewer
    Inherits ZControl
#Region " Windows Form Designer generated code "
    Public Sub New(Optional ByVal offline As Boolean = False)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Trace.Indent()
        Trace.WriteLineIf(ZTrace.IsVerbose, "Instancio el FrmViewer" & Now.ToString)
        'Add any initialization after the InitializeComponent() call
        Me.offline = offline
    End Sub

    'Form overrides dispose to clean up the component list.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    'Try
    'If disposing Then
    '  If Not (components Is Nothing) Then
    '	 components.Dispose()
    '  End If
    'End If
    'Catch
    'End Try
    'Try
    'Me.disposeIndexEditing()
    'Catch ex As Exception
    'zamba.core.zclass.raiseerror(ex)
    'End Try
    'MyBase.Dispose(disposing)
    'End Sub

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel5 = New Zamba.AppBlock.ZPanel
        Me.SuspendLayout()
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.ForeColor = System.Drawing.Color.Black
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(202, 320)
        Me.Panel5.TabIndex = 2
        Me.Panel5.AutoScroll = True
        '
        'UCIndexIndexerViewer
        '
        Me.AllowDrop = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.CausesValidation = False
        Me.Controls.Add(Me.Panel5)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Location = New System.Drawing.Point(143, 0)
        Me.Name = "UCIndexIndexerViewer"
        Me.Size = New System.Drawing.Size(202, 320)
        Me.AutoScroll = False
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private offline As Boolean
    'Dim FlagFirstTime As Boolean = True
    Dim Result As IResult
    'Dim FlagSameDocType As Boolean = False
    '    Dim FlagRightReIndex As Boolean = False

    'Se usa para no recargar los mismos indices de un entidad
    Private LastShowedDocId As Int64 = 0

    Private Sub GeneracionIndices()
        Dim i As Integer = Me.Result.Indexs.Count - 1
        Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(Me.Result.DocTypeId, UserBusiness.CurrentUser.ID, True, True)
        For i = Me.Result.Indexs.Count - 1 To 0 Step -1

            'Verifico si el dato ya fue cargado en la hash
            If Me.hashindexs.ContainsKey(DirectCast(Me.Result.Indexs(i), Index).ID) = False Then
                'cargo el dato vacio
                '             Me.hashindexs.Add(Me.Result.Indexs(i).Id, "")
                If Not String.IsNullOrEmpty(DirectCast(Me.Result.Indexs(i), Index).DataTemp) Then
                    Me.hashindexs.Add(DirectCast(Me.Result.Indexs(i), Index).ID, DirectCast(Me.Result.Indexs(i), Index).DataTemp)
                Else
                    Me.hashindexs.Add(DirectCast(Me.Result.Indexs(i), Index).ID, String.Empty)
                End If
            Else
                DirectCast(Me.Result.Indexs(i), Index).DataTemp = CType(Me.hashindexs(DirectCast(Me.Result.Indexs(i), Index).ID), String)
                DirectCast(Me.Result.Indexs(i), Index).Data = CType(Me.hashindexs(DirectCast(Me.Result.Indexs(i), Index).ID), String)
            End If

            'If IsNothing(Me.IndexName) = False Then
            '    Dim h As Int16
            '    For h = 0 To IndexName.Count - 1
            '        If Me.Result.Indexs(i).name = IndexName(h) Then
            '            Me.Result.Indexs(i).data = IndexData(h)
            '            'cargo el dato que viene por default a la hash para reutilizarse en los agregados
            '            Me.hashindexs(Me.Result.Indexs(i).Id) = IndexData(h)
            '            Exit For
            '        End If
            '    Next
            'End If

            Dim ind As Index = DirectCast(Me.Result.Indexs(i), Index)
            'Dim di As Doc_Index = ZAMBA.Core.Doc_IndexFactory.GetIndex(ind)
            Dim ctrl As New DisplayindexCtl(ind, True)
            RemoveHandler ctrl.DataChanged, AddressOf IndexsChanged
            RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
            RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
            AddHandler ctrl.DataChanged, AddressOf IndexsChanged
            AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
            AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
            If offline = False Then
                RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                AddHandler ctrl.DataChanged, AddressOf DataChanged
            End If
            ctrl.Dock = DockStyle.Top

            Dim IR As IndexsRightsInfo = DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo)
            Dim dsIndexsPropertys As DataSet = DocTypesBusiness.GetIndexsProperties(Me.Result.DocTypeId, True)

            For Each indexid As Int64 In IRI.Keys
                If indexid = ctrl.Index.ID Then
                    For Each Index As DataRow In dsIndexsPropertys.Tables(0).Rows
                        If ctrl.Index.ID = CLng(Index("Index_Id")) AndAlso Index("MustComplete") = 1 _
                            AndAlso ctrl.Controls(2).Text.Contains("*") = False Then

                            ctrl.Controls(2).Text = ctrl.Controls(2).Text + " *"
                            Exit For
                        ElseIf IR.GetIndexRightValue(RightsType.IndexRequired) = True AndAlso ctrl.Controls(2).Text.Contains("*") = False Then
                            ctrl.Controls(2).Text = ctrl.Controls(2).Text + " *"
                            Exit For
                        End If
                    Next
                    'If IR.GetIndexRightValue(RightsType.IndexRequired) Then
                    '    ctrl.Controls(2).Text = ctrl.Controls(2).Text + " *"
                    'End If

                    Exit For

                End If
            Next


            Me.DelegateAddControls(ctrl)
        Next

        'invierto tabindexs
        Dim tindex As Int32 = Me.Panel5.Controls.Count - 1
        For Each control As control In Me.Panel5.Controls
            control.TabIndex = tindex
            tindex -= 1
        Next
    End Sub
    'Private Sub autocomplete(ByRef Result As Zamba.Core.Result, ByVal index As Zamba.Core.Index)
    '    'Dim AC As Zamba.Codes.AutocompleteBC = Zamba.Codes.AutoCompleteBarcode_Factory.GetComplete(Me.CboDocType.SelectedValue, index.Id)
    '    'If Not IsNothing(AC) Then
    '    '    Me.LoadIndexViewer(AC.Complete(result, index))
    '    'End If
    'End Sub
    Private Sub IndexsChanged(ByVal Index As IIndex)
        RaiseEvent IndexChanged(Me.Result, Index)
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
        Me.Invoke(D1)
    End Sub
    Private Sub AddControls()
        Me.Panel5.Controls.Add(Ctrl)
    End Sub
    Private Sub DataChanged(ByVal Index As IIndex)
        Me.hashindexs(Index.ID) = Index.DataTemp
    End Sub

    Public Function IsValid() As Boolean
        ' Dim Indexs As New ArrayList
        For Each c As DisplayindexCtl In Me.Panel5.Controls
            If c.isValid = False Then
                Return False
                Exit For
            End If
        Next
        Return True
    End Function

    Private Function GetIndexs() As ArrayList
        Dim result As New ArrayList

        For Each c As DisplayindexCtl In Me.Panel5.Controls
            result.Add(c.Index)
        Next
        Return result
    End Function

    Private hashindexs As New Hashtable
    '    Dim IndexData As ArrayList  'se usa para el AgregarACarpeta que se dispara en el FrmViewer
    '   Dim IndexName As ArrayList
    ' Dim DocTypeId As Int32    'se usa para el AgregarACarpeta que se dispara en el FrmViewer

    '    Public Sub ShowDocument(ByRef Result As Zamba.Core.Result, ByVal IndexName As ArrayList, ByVal IndexData As ArrayList, ByVal DocTypeId As Int32)
    Public Sub ShowDocument(ByRef Result As IResult, ByVal ReplaceData As Boolean)
        Me.Result = Result
        '       Me.IndexData = IndexData
        '      Me.IndexName = IndexName

        'YA NO RECIBE EL DOCTYPEID COMO PARAMETRO SINO QUE USA EL DEL RESULT
        '  Dim DocTypeId As Integer = Result.DocTypeId


        Try
            If Me.LastShowedDocId <> 0 AndAlso Me.LastShowedDocId = Me.Result.DocType.ID Then
                Me.RefreshDatosIndices(ReplaceData)
            Else

                Me.Panel5.Controls.Clear()
                Me.GeneracionIndices()
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            Me.LastShowedDocId = Me.Result.DocType.ID
        End Try
        '  Me.FlagFirstTime = False
        UserBusiness.Rights.SaveAction(Result.ID, Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.View, Result.Name)
    End Sub

    ''' <summary>
    ''' [Sebastian] 03-07-09 MOIDFIED se corrigieron warnings
    ''' </summary>
    ''' <param name="ReplaceData"></param>
    ''' <remarks></remarks>
    Private Sub RefreshDatosIndices(Optional ByVal ReplaceData As Boolean = False)
        Dim i As Integer = Me.Result.Indexs.Count - 1
        Dim b As Int32
        For i = Me.Result.Indexs.Count - 1 To 0 Step -1
            'Dim i As Integer
            'For i = 0 To Me.Result.Indexs.Count - 1

            'Dim ctrl As DisplayindexCtl = Me.Panel5.Controls(b)

            'Verifico si el dato ya fue cargado en la hash
            If Me.hashindexs.ContainsKey(DirectCast(Me.Result.Indexs(i), Index).ID) = False Then
                'cargo el dato vacio
                '          Me.hashindexs.Add(Me.Result.Indexs(i).Id, "")
                If String.Compare(DirectCast(Me.Result.Indexs(i), Index).Data, String.Empty) <> 0 Then
                    'Me.hashindexs.Add(Me.Result.Indexs(i).Id, Me.Result.Indexs(i).data)
                    Me.hashindexs.Add(DirectCast(Me.Result.Indexs(i), Index).ID, DirectCast(Me.Result.Indexs(i), Index).Data)
                Else
                    Me.hashindexs.Add(DirectCast(Me.Result.Indexs(i), Index).ID, String.Empty)
                End If
            Else
                If ReplaceData Then
                    Me.hashindexs(DirectCast(Me.Result.Indexs(i), Index).ID) = DirectCast(Me.Result.Indexs(i), Index).Data
                    DirectCast(Me.Result.Indexs(i), Index).DataTemp = DirectCast(Me.Result.Indexs(i), Index).Data
                Else
                    DirectCast(Me.Result.Indexs(i), Index).Data = Me.hashindexs(DirectCast(Me.Result.Indexs(i), Index).ID).ToString
                End If
            End If
            'If IsNothing(Me.IndexName) = False Then
            '    Dim h As Int16
            '    For h = 0 To IndexName.Count - 1
            '        If Me.Result.Indexs(i).name = IndexName(h) Then
            '            Me.Result.Indexs(i).data = IndexData(h)
            '            'cargo el dato que viene por default a la hash para reutilizarse en los agregados
            '            Me.hashindexs(Me.Result.Indexs(i).Id) = IndexData(h)
            '        End If
            '    Next
            'End If
            If Me.Panel5.Controls.Count >= (b + 1) Then
                Dim ctrl As DisplayindexCtl = DirectCast(Me.Panel5.Controls(b), DisplayindexCtl)
                Dim ind As Zamba.Core.Index = DirectCast(Me.Result.Indexs(i), Index)
                ctrl.ReloadindexData(ind)
                b += +1
            End If
        Next
    End Sub

    Private Sub UCIndexViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.BtnSave.Enabled = False
    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
        Select Case CInt(e.Button.Tag)
            Case 0
                'TODO: Hacer metodo que imprima los indices
            Case 1
            Case 2
                Me.cleanIndexs()
        End Select
    End Sub
    Public Sub cleanIndexs()
        Try
            Dim i As Int32
            For i = 0 To Me.Result.Indexs.Count - 1
                DirectCast(Me.Result.Indexs(i), Index).Data = String.Empty
                DirectCast(Me.Result.Indexs(i), Index).DataTemp = String.Empty
                DirectCast(Me.Result.Indexs(i), Index).dataDescription = String.Empty
                DirectCast(Me.Result.Indexs(i), Index).dataDescriptionTemp = String.Empty
            Next
            If IsNothing(Me.hashindexs) = False Then Me.hashindexs.Clear()
            '            If IsNothing(Me.IndexData) = False Then Me.IndexData.Clear()
            '           If IsNothing(Me.IndexName) = False Then Me.IndexName.Clear()
            '            Me.ShowDocument(Result, IndexName, IndexData, DocTypeId)
            Me.ShowDocument(Result, False)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
End Class
