Imports Zamba.Controls


Public Class UCIndexAndUserSelection
    Inherits ZControl

    Private selectedDocType As Int64 = -2
    Private flagSelectedDocType As Boolean = False
    Private indexsArray As Index()
    Private indexArrayB As Index()
    Private indexIDs As String = String.empty
    Private indexValues As String = String.Empty

    Friend WithEvents ucUsuarios As UCSelectUsers

    Public Event eBtnGuardarClick(ByRef indexControllerControls As Control.ControlCollection, ByVal cmbSelectedValue As Object)


    Public Sub New(ByVal idDeAgrupamiento As Int64, Optional ByVal docType As Int64 = -2, Optional ByVal IDIndexs As String = "", Optional ByVal ValueIndexs As String = "")
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        indexIDs = IDIndexs
        indexValues = ValueIndexs

        If docType <> -2 Then

            selectedDocType = docType
            flagSelectedDocType = True

            CreateRuleIndex()

            ucUsuarios = New UCSelectUsers(GroupToNotifyTypes.Rule, idDeAgrupamiento, False, -1)
            ucUsuarios.Dock = DockStyle.Fill
            tabpUserSelection.Controls.Add(ucUsuarios)

        End If

    End Sub

    Private Sub UCIndexAndUserSelection_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If flagSelectedDocType Then

            RuleLoadcmbDocTypes()
            RuleShowIndexs()
            RemoveHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged
            AddHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged

        Else

            LoadcmbDocTypes()
            ShowIndexs()
            RemoveHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged
            AddHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged

        End If

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click

        '        Me.ucUsuarios.Aceptar()

        RaiseEvent eBtnGuardarClick(IndexController1.Controls, cmbDocTypes.SelectedValue)

    End Sub

    Private Function ConvertStringToArray(ByVal _incomString As String) As String()

        If (_incomString.Length > 0) Then

            Dim _indexArray As String() = _incomString.Split("|")

            Return _indexArray

        Else

            Return Nothing

        End If

    End Function
    Private Function ConvertArrayToString(ByVal _incomArray As String()) As String

        Dim i As Int16 = 0

        Dim _indexString As String = String.Empty

        For Each s As String In _incomArray

            If i = 0 Then
                _indexString = s
            Else
                _indexString = _indexString & "|" & s

            End If

        Next

        Return _indexString

    End Function

#Region "TabIndices"


    Private Sub cmbDocTypes_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
        cmbDocTypes.Text = cmbDocTypes.Text.Trim
    End Sub

    Private Sub cmbDocTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        cmbDocTypes.Text = cmbDocTypes.Text.Trim

        If Not IsNothing(cmbDocTypes.SelectedItem) AndAlso cmbDocTypes.DisplayMember <> String.Empty AndAlso cmbDocTypes.ValueMember <> String.Empty Then

            If flagSelectedDocType AndAlso cmbDocTypes.SelectedValue = selectedDocType Then
                RuleShowIndexs()
            Else
                ShowIndexs()
            End If

        End If

    End Sub


    'Carga el ComboBox con los Doctypes, muestra el Nombre y el valor es el Id.
    Private Sub LoadcmbDocTypes()
        Try

            'Saco el evento SelectedIndexChanged para que no salte cuando se
            'asigna el ValueMember y el DisplayMember
            RemoveHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged
            Dim ds As DataSet = Zamba.Core.DocTypes.DocType.GetDocTypesDataSet()


            cmbDocTypes.DataSource = ds.Tables(0)
            cmbDocTypes.ValueMember = "DOC_TYPE_ID"
            cmbDocTypes.DisplayMember = "DOC_TYPE_NAME"

        Catch ex As Exception

            Zamba.Core.ZClass.raiseerror(ex)

        End Try

    End Sub

    'Carga el ComboBox y selecciona el valor de la regla
    Private Sub RuleLoadcmbDocTypes()

        'Cargo el cmb
        LoadcmbDocTypes()

        'Selecciono el valor de la regla
        If flagSelectedDocType Then
            cmbDocTypes.SelectedValue = selectedDocType
        End If

    End Sub

    'Carga los atributos del DocType seleccionado en el ComboBox
    'en la coleccion me.Indexs. Y los muestra en el IndexController
    Private Sub ShowIndexs()

        'Si hay algo seleccionado hago todo el proceso
        If Not IsNothing(cmbDocTypes.SelectedItem) Then
            'Si hay atributos cargados en la coleccion los borro
            If Not IsNothing(indexsArray) Then
                indexsArray = Nothing
            End If
            'Obtengo los atributos nuevos
            indexsArray = GetAllIndex(cmbDocTypes.SelectedValue)
            'Y si no me devuelve nada los muestro
            If Not IsNothing(indexsArray) Then
                Dim DocTypes As New ArrayList
                DocTypes.Add(cmbDocTypes.SelectedValue)
                IndexController1.ClearIndexs()
                IndexController1.ShowIndex(indexsArray, DocTypes)
            End If
        End If
    End Sub

    'Muestra los atributos de la regla
    Private Sub RuleShowIndexs()

        'Si hay algo seleccionado hago todo el proceso
        If Not IsNothing(cmbDocTypes.SelectedItem) Then
            'Si hay atributos cargados en la coleccion los borro
            If Not IsNothing(indexsArray) Then
                indexsArray = Nothing
            End If
            'Obtengo los atributos nuevos
            indexsArray = GetAllIndex(cmbDocTypes.SelectedValue)

            If Not IsNothing(indexArrayB) Then
                'Recorro cada Atributo de la regla
                For Each _indexB As Index In indexArrayB
                    'Recorro los atributos del docType
                    For Each _index As Index In indexsArray
                        'Si el ID es el mismo cargo los valores en el atributo del DocType
                        If _indexB.ID = _index.ID Then
                            '_index.DataTemp = _ruleIndex.DataTemp
                            _index.DataTemp = _indexB.DataTemp
                            _index.Data = _indexB.DataTemp
                            Exit For
                        End If
                    Next
                Next
            End If

            'Y si no me devuelve nada los muestro
            If Not IsNothing(indexsArray) Then
                IndexController1.ClearIndexs()
                Dim indexlist As New ArrayList
                For Each index As Index In indexsArray
                    indexlist.Add(index)
                Next
                Dim DocTypeId As New ArrayList
                DocTypeId.Add(cmbDocTypes.SelectedValue)
                IndexController1.ShowIndex(indexsArray, DocTypeId)
            End If
        End If

    End Sub

    'Devuelve todos los atributos del item(DocType) seleccionado en el ComboBox
    Private Function GetAllIndex(ByVal docTypesSelectedValue As Int64) As Index()

        Dim IndexsAux As Index() = {}

        Dim dsIndex As DataSet

        Dim i As Int16 = 0

        Try


            dsIndex = IndexsBusiness.GetIndexSchemaAsDataSet(Decimal.ToInt32(docTypesSelectedValue))

            ReDim IndexsAux(dsIndex.Tables(0).Rows.Count - 1)
            ReDim indexsArray(dsIndex.Tables(0).Rows.Count - 1)

            For Each r As DataRow In dsIndex.Tables(0).Rows

                IndexsAux(i) = BuildIndex(r)

                i = i + 1

            Next

            Return IndexsAux


        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Return IndexsAux

    End Function

    'Creo un Atributo a partir de una Row con los datos del Atributo
    Private Function BuildIndex(ByVal r As DataRow) As Index
        Try
            Dim _id As Int32 = Decimal.ToInt32(r("INDEX_ID"))
            Dim _name As String = r("INDEX_NAME").ToString().Trim()
            Dim _typeInt As Int16 = Int32.Parse(r("INDEX_TYPE").ToString.Trim)
            Dim _type As IndexDataType = _typeInt
            Dim _len As Int32 = Decimal.ToInt32(r("INDEX_LEN"))
            Dim _aTypeInt As Int16 = Decimal.ToInt32(r("DROPDOWN"))
            Dim _aType As IndexAdditionalType = _aTypeInt

            Dim _newIndex As New Index(_name, _id, _type, _len, _aType)

            Return _newIndex
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Return Nothing
    End Function

    'Dado un valor DocType.Id se devuelven sus atributos cargados con los valores 
    'del segundo parámetro.
    Private Function ruleGetIndexsById(ByVal _i As Int32(), ByVal _v As String()) As Index()
        Dim dsIndex As DataSet
        Dim newIndexs(_i.Length - 1) As Index
        Dim i As Int32 = 0

        Try
            For Each _index As Int32 In _i
                newIndexs(i) = ZCore.GetIndex(_index)
                newIndexs(i).DataTemp = _v(i)
            Next

            Return newIndexs
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Return Nothing
    End Function

    'Lee las propiedades de la regla y con eso genera los atributos de la regla.
    Private Sub CreateRuleIndex()

        Try
            Dim _v As String() = {String.Empty}
            Dim _a As String() = {String.Empty}
            If String.Compare(indexIDs, String.Empty) <> 0 Then
                If Not IsNothing(indexValues) Then
                    _v = ConvertStringToArray(indexValues)
                End If
                _a = ConvertStringToArray(indexIDs)

                Dim _IIDs(_a.Length - 1) As Int32
                Dim _IValues(_a.Length - 1) As String
                Dim i As Int32 = 0


                For i = 0 To _a.Length - 1
                    _IIDs(i) = Int32.Parse(_a(i))
                    If Not IsNothing(_v) Then
                        _IValues(i) = _v(i)
                    End If

                Next

                indexArrayB = ruleGetIndexsById(_IIDs, _IValues)

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


#End Region


End Class
