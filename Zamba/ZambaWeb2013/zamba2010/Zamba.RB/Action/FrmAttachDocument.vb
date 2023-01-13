Imports System.Windows.Forms
Imports Zamba.Core
Imports Zamba.Data

Public Class FrmAttachDocument


#Region "Atributos"


    Private _currentRule As IDoAttachToDocument
    Private _totalKB As Decimal
    Private _result As IResult
    Private _limit As Int64
    Private _limitaux As String
    Private _currentSize As Decimal

#End Region


#Region "Constructores"

    Sub New(ByVal _rule As IDoAttachToDocument)
        Me.InitializeComponent()
        Me._currentRule = _rule
    End Sub

#End Region

#Region "Propiedades"

    Public Property LimitKB() As String
        Get
            Return Me._limit
        End Get
        Set(ByVal value As String)
            Me._limit = value
        End Set
    End Property

#End Region

#Region "Metodos"

#Region "Publicos"
    ''' <summary>
    ''' Metodo el cual muestra el formulario de adjuntar archivos.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel] - 06/11/09 - Created
    ''' </History>
    Public Sub Attach(Optional ByVal r As IResult = Nothing)
        Trace.WriteLineIf(ZTrace.IsInfo, "Limpiando variables del formulario")
        Me._result = r
        If Not Me._currentRule.WithLimit Then
            Me.lblLimit.Visible = False
        Else



            Dim currentsizeaux As String
            currentsizeaux = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._currentRule.CurrentSize)
            If Not r Is Nothing Then
                currentsizeaux = TextoInteligente.ReconocerCodigo(currentsizeaux, r)
            End If
            If IsNumeric(currentsizeaux) Then
                Me._currentSize = currentsizeaux
            Else
                Me._currentSize = 0
            End If
            Me._totalKB += Me._currentSize

            Me._limitaux = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._currentRule.LimitKB)
            If Not r Is Nothing Then
                Me._limitaux = TextoInteligente.ReconocerCodigo(Me._limitaux, r)
            End If
            If IsNumeric(Me._limitaux) Then
                Me._limit = Int64.Parse(Me._limitaux)
            Else
                Me._limit = 0
            End If
            Me.lblLimit.Text = "Limite: " & Me._limit & " KB."
        End If
        Me.lstFiles.Items.Clear()
        Me.txtFullPath.Text = ""
        Me.lbltotalsize.Text = "Tamaño de adjuntos: " & Me._totalKB.ToString("#.##") & " KB"
        Me.ShowDialog()
    End Sub
#End Region

#Region "Privados"
    ''' <summary>
    ''' Metodo para buscar archivos a adjuntar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel] - 06/11/09 - Created
    ''' </History>
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Trace.WriteLineIf(ZTrace.IsInfo, "Buscando archivos ")
        Me.Adjuntar.ShowDialog()
        Me.txtFullPath.Text = ""
        For Each fl As String In Me.Adjuntar.FileNames
            Me.txtFullPath.Text = Me.txtFullPath.Text & " " & Chr(34) & fl & Chr(34)
        Next
        Trace.WriteLineIf(ZTrace.IsInfo, "Archivos seleccionados: " & Me.txtFullPath.Text)
    End Sub


    ''' <summary>
    ''' Metodo el cual agrega el archivo a la lista.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel] - 06/11/09 - Created
    ''' </History>
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not String.IsNullOrEmpty(Me.txtFullPath.Text) Then
            For Each flstr As String In Me.txtFullPath.Text.Trim.Split(New String() {Chr(34) & " " & Chr(34)}, StringSplitOptions.RemoveEmptyEntries)
                flstr = flstr.Replace(Chr(34), "")
                If Me.lstFiles.Items.Contains(flstr) Then
                    MessageBox.Show("El archivo ya existe en la lista", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Dim fl As IO.FileInfo
                    Try
                        fl = New IO.FileInfo(flstr)
                    Catch
                        MessageBox.Show("El archivo seleccionado no existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End Try
                    If Not fl.Exists Then
                        MessageBox.Show("El archivo seleccionado no existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, "Agregando a la lista el archivo: " & fl.FullName)
                        Me.lstFiles.Items.Add(fl.FullName)
                    End If
                End If
            Next
            Me.GetTotalSize()
        End If
    End Sub

    ''' <summary>
    ''' Metodo el cual elimina el archivo seleccionado de la lista.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel] - 06/11/09 - Created
    ''' </History>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not Me.lstFiles.SelectedItem Is Nothing Then
            Trace.WriteLineIf(ZTrace.IsInfo, "Borrando de la lista el archivo: " & Me.lstFiles.SelectedItem.ToString)
            Me.lstFiles.Items.Remove(Me.lstFiles.SelectedItem)
            Me.GetTotalSize()
        End If
    End Sub

    ''' <summary>
    ''' Metodo el cual realiza la suma total en KB de los archivos seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel] - 06/11/09 - Created
    ''' </History>
    Private Sub GetTotalSize()
        Me._totalKB = Me._currentSize
        For Each fl As String In Me.lstFiles.Items
            Me._totalKB += New IO.FileInfo(fl).Length / 1024
        Next
        Me.lbltotalsize.Text = "Tamaño de adjuntos: " & Me._totalKB.ToString("#.##") & " KB."
        Trace.WriteLineIf(ZTrace.IsInfo, "Tamaño de adjuntos: " & Me._totalKB.ToString("#.##") & " KB")
        If Me._totalKB > Me._limit Then
            Me.lbltotalsize.ForeColor = Drawing.Color.Red
        Else
            Me.lbltotalsize.ForeColor = Drawing.Color.Black
        End If
    End Sub

    ''' <summary>
    ''' Metodo el cual limpia la lista de archivos.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel] - 06/11/09 - Created
    ''' </History>
    Private Sub btnClearList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearList.Click
        Trace.WriteLineIf(ZTrace.IsInfo, "Elimiando lista de archivos")
        Me.lstFiles.Items.Clear()
        Me._totalKB = Me._currentSize
        Me.lbltotalsize.Text = "Tamaño de adjuntos: " & Me._totalKB.ToString("#.##") & " KB."
        Me.lbltotalsize.ForeColor = Drawing.Color.Black
    End Sub

    ''' <summary>
    ''' Metodo el cual Genera los documentos con los archivos seleccionados.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel] - 06/11/09 - Created
    ''' </History>
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        If Me._currentRule.WithLimit AndAlso Me._totalKB > Me._limit Then
            Trace.WriteLineIf(ZTrace.IsInfo, "No se pueden generar los documentos ya que se excede el limite de KB")
            MessageBox.Show("Los archivos superan el limite de KB, elimine algunos para continuar", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim newresult As IResult
            For Each fl As String In lstFiles.Items
                Trace.WriteLineIf(ZTrace.IsInfo, "Generando documento para el archivo: " & fl)
                newresult = Results_Business.GetNewResult(Me._currentRule.DocTypeId, fl)

                If Not Me._result Is Nothing Then
                    For Each ind As IIndex In Me._result.Indexs
                        For Each newind As IIndex In newresult.Indexs
                            If newind.ID = ind.ID Then
                                newind.Data = ind.Data
                                newind.DataTemp = ind.Data
                                newind.dataDescription = ind.dataDescription
                                newind.dataDescriptionTemp = ind.dataDescription
                            End If
                        Next
                    Next
            
                End If
                If Results_Business.InsertDocument(newresult, False, False, False, False, False) = InsertResult.Insertado Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Documento insertado")
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar el documento")
                End If
            Next
            Me.Close()
        End If
    End Sub
#End Region

#End Region

End Class