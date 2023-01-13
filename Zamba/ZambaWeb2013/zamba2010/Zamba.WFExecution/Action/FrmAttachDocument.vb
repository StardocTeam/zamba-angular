Imports System.Windows.Forms

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
        InitializeComponent()
        _currentRule = _rule
    End Sub

#End Region

#Region "Propiedades"

    Public Property LimitKB() As String
        Get
            Return _limit
        End Get
        Set(ByVal value As String)
            _limit = value
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
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Limpiando variables del formulario")
        _result = r
        If Not _currentRule.WithLimit Then
            lblLimit.Visible = False
        Else



            Dim currentsizeaux As String
            currentsizeaux = WFRuleParent.ReconocerVariablesValuesSoloTexto(_currentRule.CurrentSize)
            If Not r Is Nothing Then
                currentsizeaux = TextoInteligente.ReconocerCodigo(currentsizeaux, r)
            End If
            If IsNumeric(currentsizeaux) Then
                _currentSize = currentsizeaux
            Else
                _currentSize = 0
            End If
            _totalKB += _currentSize

            _limitaux = WFRuleParent.ReconocerVariablesValuesSoloTexto(_currentRule.LimitKB)
            If Not r Is Nothing Then
                _limitaux = TextoInteligente.ReconocerCodigo(_limitaux, r)
            End If
            If IsNumeric(_limitaux) Then
                _limit = Int64.Parse(_limitaux)
            Else
                _limit = 0
            End If
            lblLimit.Text = "Limite: " & _limit & " KB."
        End If
        lstFiles.Items.Clear()
        txtFullPath.Text = ""
        lbltotalsize.Text = "Tamaño de adjuntos: " & _totalKB.ToString("#.##") & " KB"
        ShowDialog()
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
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBrowse.Click
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando archivos ")
        Adjuntar.ShowDialog()
        txtFullPath.Text = ""
        For Each fl As String In Adjuntar.FileNames
            txtFullPath.Text = txtFullPath.Text & " " & Chr(34) & fl & Chr(34)
        Next
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivos seleccionados: " & txtFullPath.Text)
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
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAdd.Click
        If Not String.IsNullOrEmpty(txtFullPath.Text) Then
            For Each flstr As String In txtFullPath.Text.Trim.Split(New String() {Chr(34) & " " & Chr(34)}, StringSplitOptions.RemoveEmptyEntries)
                flstr = flstr.Replace(Chr(34), "")
                If lstFiles.Items.Contains(flstr) Then
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
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando a la lista el archivo: " & fl.FullName)
                        lstFiles.Items.Add(fl.FullName)
                    End If
                End If
            Next
            GetTotalSize()
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
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnDelete.Click
        If Not lstFiles.SelectedItem Is Nothing Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Borrando de la lista el archivo: " & lstFiles.SelectedItem.ToString)
            lstFiles.Items.Remove(lstFiles.SelectedItem)
            GetTotalSize()
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
        _totalKB = _currentSize
        For Each fl As String In lstFiles.Items
            _totalKB += New IO.FileInfo(fl).Length / 1024
        Next
        lbltotalsize.Text = "Tamaño de adjuntos: " & _totalKB.ToString("#.##") & " KB."
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tamaño de adjuntos: " & _totalKB.ToString("#.##") & " KB")
        If _totalKB > _limit Then
            lbltotalsize.ForeColor = Drawing.Color.Red
        Else
            lbltotalsize.ForeColor = Drawing.Color.Black
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
    Private Sub btnClearList_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnClearList.Click
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Elimiando lista de archivos")
        lstFiles.Items.Clear()
        _totalKB = _currentSize
        lbltotalsize.Text = "Tamaño de adjuntos: " & _totalKB.ToString("#.##") & " KB."
        lbltotalsize.ForeColor = Drawing.Color.Black
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
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAccept.Click
        If _currentRule.WithLimit AndAlso _totalKB > _limit Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pueden generar los documentos ya que se excede el limite de KB")
            MessageBox.Show("Los archivos superan el limite de KB, elimine algunos para continuar", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim RB As New Results_Business
            Dim newresult As IResult
            For Each fl As String In lstFiles.Items
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando documento para el archivo: " & fl)
                newresult = RB.GetNewResult(_currentRule.DocTypeId, fl)

                If Not _result Is Nothing Then
                    For Each ind As IIndex In _result.Indexs
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
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento insertado")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar el documento")
                End If
            Next
            Close()
        End If
    End Sub
#End Region

#End Region

End Class