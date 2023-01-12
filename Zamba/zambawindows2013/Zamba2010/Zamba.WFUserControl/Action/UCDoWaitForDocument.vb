'Imports Zamba.WFBusiness

Imports Zamba.Indexs

Public Class UCDoWaitForDocument
    Inherits ZRuleControl

    Friend WithEvents ucIndexAndUsers As UCIndexAndUserSelection


#Region "Atributos"

    Private _indexs As Index()
    Private _currentRule As IDoWaitForDocument

#End Region

#Region "Constructores"

    Public Sub New(ByRef _currentRule As IDoWaitForDocument, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(_currentRule, _wfPanelCircuit)
        InitializeComponent()

        ' Para que se adapte al tamaño del panel 2 Reglas, porque por alguna razon no se hacia el dock fill
        Dim size As New Size(1500, 1500)
        Width = size.Width
        Height = size.Height

        CurrentRule = _currentRule

        If Not IsNothing(CurrentRule) Then

            If Not IsNothing(CurrentRule.DocType) AndAlso Not String.IsNullOrEmpty(CurrentRule.IValue) AndAlso Not String.IsNullOrEmpty(CurrentRule.IndiceID) Then
                ucIndexAndUsers = New UCIndexAndUserSelection(CurrentRule.RuleID, CurrentRule.DocType, CurrentRule.IndiceID, CurrentRule.IValue)

            ElseIf Not IsNothing(CurrentRule.DocType) AndAlso CurrentRule.DocType <> 0 Then
                ucIndexAndUsers = New UCIndexAndUserSelection(CurrentRule.RuleID, CurrentRule.DocType)

            Else

                ucIndexAndUsers = New UCIndexAndUserSelection(CurrentRule.RuleID)

            End If
            RemoveHandler ucIndexAndUsers.eBtnGuardarClick, AddressOf btnGuardar_Click
            AddHandler ucIndexAndUsers.eBtnGuardarClick, AddressOf btnGuardar_Click
            btnGuardar.Visible = False
            ucIndexAndUsers.Dock = DockStyle.Fill
            Controls.Add(ucIndexAndUsers)
            ucIndexAndUsers.Dock = DockStyle.Fill
            tbRule.Controls.Add(ucIndexAndUsers)
            'Me.tbRule.Controls.Add(Me.ucIndexAndUsers.cmbDocTypes)
            ucIndexAndUsers.cmbDocTypes.Visible = True
            'Me.cmbDocTypes = ucIndexAndUsers.cmbDocTypes

        End If


        If Not IsNothing(_currentRule) AndAlso Not IsNothing(_currentRule.IndiceID) Then

            ucIndexAndUsers = New UCIndexAndUserSelection(_currentRule.RuleID, _currentRule.DocType, _currentRule.IndiceID, _currentRule.IValue)
            ucIndexAndUsers.Dock = DockStyle.Fill
            Controls.Add(ucIndexAndUsers)

        End If


        Dim ds As DataSet = Zamba.Core.DocTypes.DocType.GetDocTypesDataSet()
        cmbDocTypes.DataSource = ds.Tables(0)
        cmbDocTypes.ValueMember = "doc_type_id"
        cmbDocTypes.DisplayMember = "doc_type_name"
        cmbDocTypes.DropDownStyle = ComboBoxStyle.DropDownList

    End Sub

#End Region

#Region "Propiedades"

    Public Property CurrentRule() As IDoWaitForDocument
        Get
            Return _currentRule
        End Get
        Set(ByVal value As IDoWaitForDocument)
            _currentRule = value
        End Set
    End Property

    Public Property Indexs() As Index()
        Get
            Return _indexs
        End Get
        Set(ByVal value As Index())
            _indexs = value
        End Set
    End Property

#End Region

#Region "Eventos"

    'Private Sub btnGuardar_Click(ByRef IndexControllerControls As Control.ControlCollection, ByVal cmbSelectedValue As Object) Handles ucIndexAndUsers.eBtnGuardarClick
    Private Sub btnGuardar_Click(ByRef IndexControllerControls As Control.ControlCollection, ByVal cmbSelectedValue As Object)

        If Not IsNothing(IndexControllerControls) Then

            Dim count As Int16 = 0

            Dim sValue As String = String.Empty
            Dim sID As String = String.Empty

            Try
                For Each cc As Control In IndexControllerControls

                    If TypeOf (cc) Is SimpleIndexSearchCtrl Then
                        Dim c As SimpleIndexSearchCtrl = DirectCast(c, SimpleIndexSearchCtrl)
                        If c.Index.DataTemp <> String.Empty Then

                            If count = 0 Then
                                sValue = c.Index.DataTemp
                            Else
                                sValue = sValue & "|" & c.Index.DataTemp
                            End If

                            If count = 0 Then
                                sID = c.Index.ID.ToString()
                            Else
                                sID = sID & "|" & c.Index.ID.ToString()
                            End If

                            count = 1


                        End If


                    End If

                Next

                Dim a As Array = ConvertStringToArray(sValue)

            Catch ex As Exception

                Zamba.Core.ZClass.raiseerror(ex)

            End Try


            Try

                CurrentRule.DocType = Convert.ToInt32(cmbSelectedValue)
                CurrentRule.IndiceID = sID
                CurrentRule.IValue = sValue

            Catch ex As InvalidCastException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            Try
                WFRulesBusiness.UpdateParamItem(CurrentRule, 0, CurrentRule.DocType)
                WFRulesBusiness.UpdateParamItem(CurrentRule, 1, CurrentRule.IndiceID)
                WFRulesBusiness.UpdateParamItem(CurrentRule, 2, CurrentRule.IValue)
                UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        End If

    End Sub

#End Region

#Region "Métodos"

    Private Function ConvertStringToArray(ByVal _incomString As String) As String()

        If (_incomString.Length > 0) Then

            Dim _indexArray As String() = _incomString.Split("|")

            Return _indexArray

        Else

            Return Nothing

        End If

    End Function

#End Region




#Region "Bckp 26/09/07"
    '#Region "Atributos"

    '    Private _docTypeId as Int64
    '    Private _indexs As Index()
    '    Private _ruleIndex As Index()
    '    Private _currentRule As IDoWaitForDocument

    '#End Region

    '#Region "Constructores"

    '    Public Sub New(ByRef _currentRule As IDoWaitForDocument)

    '        InitializeComponent()

    '        Me.CurrentRule = _currentRule

    '        'ruleLoadcmbDocTypes()

    '        If Not IsNothing(_currentRule) AndAlso Not IsNothing(_currentRule.IndiceID) Then
    '            CreateRuleIndex()
    '        End If

    '    End Sub

    '#End Region

    '#Region "Propiedades"

    '    Public Property CurrentRule() As IDoWaitForDocument
    '        Get
    '            Return _currentRule
    '        End Get
    '        Set(ByVal value As IDoWaitForDocument)
    '            _currentRule = value
    '        End Set
    '    End Property

    '    Public Property DocTypeId() As Int32
    '        Get
    '            Return _docTypeId
    '        End Get
    '        Set(ByVal value As Int32)
    '            _docTypeId = value
    '        End Set
    '    End Property

    '    Public Property Indexs() As Index()
    '        Get
    '            Return _indexs
    '        End Get
    '        Set(ByVal value As Index())
    '            _indexs = value
    '        End Set
    '    End Property

    '#End Region

    '#Region "Eventos"
    '    Private Sub cmbDocTypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDocTypes.SelectedIndexChanged
    '        Me.cmbDocTypes.Text = Me.cmbDocTypes.Text.Trim

    '        If Not IsNothing(Me.cmbDocTypes.SelectedItem) AndAlso Me.cmbDocTypes.DisplayMember <> String.Empty AndAlso Me.cmbDocTypes.ValueMember <> String.Empty Then

    '            If Not IsNothing(Me.CurrentRule) AndAlso Not IsNothing(Me.CurrentRule.DocType) AndAlso Not IsNothing(Me.CurrentRule.IndiceID) AndAlso Not IsNothing(Me.CurrentRule.IValue) AndAlso Me.cmbDocTypes.SelectedValue = Me.CurrentRule.DocType Then
    '                ShowRuleIndexs()
    '            Else
    '                ShowIndexs()
    '            End If

    '        End If
    '    End Sub

    '    Private Sub UCDoWaitForDocument_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

    '        Try
    '            If Not IsNothing(Me.CurrentRule) AndAlso Not IsNothing(Me.CurrentRule.DocType) AndAlso Not IsNothing(Me.CurrentRule.IndiceID) AndAlso Not IsNothing(Me.CurrentRule.IValue) AndAlso Not IsNothing(Me._ruleIndex) Then
    '                ruleLoadcmbDocTypes()
    '                ShowRuleIndexs()
    '                RemoveHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged
    '                AddHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged

    '            Else
    '                LoadcmbDocTypes()
    '                ShowIndexs()
    '                RemoveHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged
    '                AddHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged

    '            End If

    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try

    '    End Sub

    '    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

    '        If Not IsNothing(Me.Indexs) Then

    '            Dim count As Int16 = 0

    '            Dim sValue As String = String.Empty
    '            Dim sID As String = String.Empty

    '            Dim x As Int32 = 0


    '            Try
    '                For Each c As Zamba.Indexs.SimpleIndexSearchCtrl In IndexController1.Controls

    '                    If c.Index.DataTemp <> String.Empty Then

    '                        If count = 0 Then
    '                            sValue = c.Index.DataTemp
    '                        Else
    '                            sValue = sValue & "|" & c.Index.DataTemp
    '                        End If

    '                        If count = 0 Then
    '                            sID = Me.Indexs(x).ID.ToString()
    '                        Else
    '                            sID = sID & "|" & Me.Indexs(x).ID.ToString()
    '                        End If

    '                        count = 1


    '                    End If

    '                    x = x + 1

    '                Next

    '                Dim a As Array = ConvertStringToArray(sValue)

    '            Catch ex As Exception

    '                Zamba.Core.ZClass.raiseerror(ex)

    '            End Try


    '            Try

    '                Me.CurrentRule.DocType = Decimal.ToInt32(Me.cmbDocTypes.SelectedValue)
    '                Me.CurrentRule.IndiceID = sID
    '                Me.CurrentRule.IValue = sValue

    '            Catch ex As InvalidCastException
    '                Zamba.Core.ZClass.raiseerror(ex)
    '            Catch ex As Exception
    '                Zamba.Core.ZClass.raiseerror(ex)
    '            End Try

    '            Try
    '                WFRulesBusiness.UpdateParamItem(Me.CurrentRule, 0, Me.CurrentRule.DocType)
    '                WFRulesBusiness.UpdateParamItem(Me.CurrentRule, 1, Me.CurrentRule.IndiceID)
    '                WFRulesBusiness.UpdateParamItem(Me.CurrentRule, 2, Me.CurrentRule.IValue)

    '            Catch ex As Exception
    '                Zamba.Core.ZClass.raiseerror(ex)
    '            End Try

    '        End If

    '    End Sub

    '#End Region

    '#Region "Métodos"

    '    'Carga el ComboBox con los Doctypes, muestra el Nombre y el valor es el Id.
    '    Private Sub LoadcmbDocTypes()
    '        Try

    '            'Saco el evento SelectedIndexChanged para que no salte cuando se
    '            'asigna el ValueMember y el DisplayMember
    '            RemoveHandler cmbDocTypes.SelectedIndexChanged, AddressOf cmbDocTypes_SelectedIndexChanged
    '            Dim ds As DataSet = Zamba.Core.DocTypes.DocType.GetDocTypesDataSet()
    '            Me.cmbDocTypes.DataSource = ds.Tables(0)
    '            Me.cmbDocTypes.ValueMember = "DOC_TYPE_ID"
    '            Me.cmbDocTypes.DisplayMember = "DOC_TYPE_NAME"

    '        Catch ex As Exception

    '            Zamba.Core.ZClass.raiseerror(ex)

    '        End Try

    '    End Sub

    '    'Carga el ComboBox y selecciona el valor de la regla
    '    Private Sub ruleLoadcmbDocTypes()

    '        'Cargo el cmb
    '        LoadcmbDocTypes()

    '        'Si no es nada selecciono el valor de la regla
    '        If Not IsNothing(Me.CurrentRule.DocType) Then
    '            Me.cmbDocTypes.SelectedValue = Me.CurrentRule.DocType
    '        End If

    '    End Sub

    '    'Carga los atributos del DocType seleccionado en el ComboBox
    '    'en la coleccion me.Indexs. Y los muestra en el IndexController
    '    Private Sub ShowIndexs()

    '        'Si hay algo seleccionado hago todo el proceso
    '        If Not IsNothing(Me.cmbDocTypes.SelectedItem) Then
    '            'Si hay atributos cargados en la coleccion los borro
    '            If Not IsNothing(Me.Indexs) Then
    '                Me.Indexs = Nothing
    '            End If
    '            'Obtengo los atributos nuevos
    '            Me.Indexs = GetAllIndex()
    '            'Y si no me devuelve nada los muestro
    '            If Not IsNothing(Me.Indexs) Then
    '                Me.IndexController1.ClearIndexs()
    '                Me.IndexController1.ShowIndex(Me.Indexs)
    '            End If
    '        End If
    '    End Sub

    '    'Muestra los atributos de la regla
    '    Private Sub ShowRuleIndexs()

    '        'Si hay algo seleccionado hago todo el proceso
    '        If Not IsNothing(Me.cmbDocTypes.SelectedItem) Then
    '            'Si hay atributos cargados en la coleccion los borro
    '            If Not IsNothing(Me.Indexs) Then
    '                Me.Indexs = Nothing
    '            End If
    '            'Obtengo los atributos nuevos
    '            Me.Indexs = GetAllIndex()

    '            'Recorro cada Atributo de la regla
    '            For Each _ruleIndex As Index In Me._ruleIndex
    '                'Recorro los atributos del docType
    '                For Each _index As Index In Me.Indexs
    '                    'Si el ID es el mismo cargo los valores en el atributo del DocType
    '                    If _ruleIndex.ID = _index.ID Then
    '                        '_index.DataTemp = _ruleIndex.DataTemp
    '                        _index.DataTemp = _ruleIndex.DataTemp
    '                        _index.Data = _ruleIndex.DataTemp
    '                        Exit For
    '                    End If
    '                Next
    '            Next


    '            'Y si no me devuelve nada los muestro
    '            If Not IsNothing(Me.Indexs) Then
    '                Me.IndexController1.ClearIndexs()
    '                Me.IndexController1.ShowIndex(Me.Indexs)
    '            End If
    '        End If
    '    End Sub


    '    'Devuelve todos los atributos del item(DocType) seleccionado en el ComboBox
    '    Private Function GetAllIndex() As Index()

    '        Dim IndexsAux As Index() = {}

    '        Dim dsIndex As DataSet

    '        Dim i As Int16 = 0

    '        Try

    '            dsIndex = Indexs_Factory.GetIndexSchemaAsDataSet(Decimal.ToInt32(Me.cmbDocTypes.SelectedValue))

    '            ReDim IndexsAux(dsIndex.Tables(0).Rows.Count - 1)
    '            ReDim Me.Indexs(dsIndex.Tables(0).Rows.Count - 1)

    '            For Each r As DataRow In dsIndex.Tables(0).Rows

    '                IndexsAux(i) = BuildIndex(r)

    '                i = i + 1

    '            Next

    '            Return IndexsAux

    '        Catch ex As Exception
    '            Zamba.Core.ZClass.raiseerror(ex)
    '        End Try

    '        Return IndexsAux

    '    End Function

    '    'Dado un valor DocType.Id se devuelven sus atributos cargados con los valores 
    '    'del segundo parámetro.
    '    Private Function ruleGetIndexsById(ByVal _i As Int32(), ByVal _v As String()) As Index()

    '        Dim dsIndex As DataSet
    '        Dim newIndexs(_i.Length - 1) As Index
    '        Dim i As Int32 = 0

    '        Try
    '            For Each _index As Int32 In _i

    '                dsIndex = Indexs_Factory.GetIndexByIdDataSet(_index)

    '                'No pongo dsIndex.Tables(0).Rows(0) para que si no hay nada
    '                'no haga exception, por eso el 'exit for'.
    '                For Each r As DataRow In dsIndex.Tables(0).Rows

    '                    newIndexs(i) = BuildIndex(r)

    '                    If Not IsNothing(_v) Then
    '                        newIndexs(i).DataTemp = _v(i)
    '                    End If

    '                    i = i + 1

    '                    Exit For

    '                Next

    '            Next

    '            Return newIndexs

    '        Catch ex As Exception
    '            Zamba.Core.ZClass.raiseerror(ex)
    '        End Try

    '        Return Nothing

    '    End Function


    '    'Lee las propiedades de la regla y con eso genera los atributos de la regla.
    '    Private Sub CreateRuleIndex()

    '        Try
    '            Dim _v As String()
    '            Dim _a As String()
    '            If String.Compare(Me.CurrentRule.IndiceID, String.Empty) <> 0 Then
    '                If Not IsNothing(Me.CurrentRule.IValue) Then
    '                    _v = ConvertStringToArray(Me.CurrentRule.IValue)
    '                End If
    '                _a = ConvertStringToArray(Me.CurrentRule.IndiceID)

    '                Dim _IIDs(_a.Length - 1) As Int32
    '                Dim _IValues(_a.Length - 1) As String
    '                Dim i As Int32 = 0


    '                For i = 0 To _a.Length - 1
    '                    _IIDs(i) = Int32.Parse(_a(i))
    '                    If Not IsNothing(_v) Then
    '                        _IValues(i) = _v(i)
    '                    End If

    '                Next

    '                Me._ruleIndex = ruleGetIndexsById(_IIDs, _IValues)

    '            End If

    '        Catch ex As Exception
    '            Zamba.Core.ZClass.raiseerror(ex)
    '        End Try
    '    End Sub


    '    'Creo un Atributo a partir de una Row con los datos del Atributo
    '    Private Function BuildIndex(ByVal r As DataRow) As Index

    '        Try

    '            Dim _id As Int32 = Decimal.ToInt32(r("INDEX_ID"))
    '            Dim _name As String = r("INDEX_NAME").ToString().Trim()
    '            Dim _typeInt As Int16 = Int32.Parse(r("INDEX_TYPE").ToString.Trim)
    '            Dim _type As IndexDataType = _typeInt
    '            Dim _len As Int32 = Decimal.ToInt32(r("INDEX_LEN"))
    '            Dim _aTypeInt As Int16 = Decimal.ToInt32(r("DROPDOWN"))
    '            Dim _aType As IndexAdditionalType = _aTypeInt




    '            Dim _newIndex As New Index(_name, _id, _type, _len, _aType)

    '            Return _newIndex

    '        Catch ex As Exception
    '            Zamba.Core.ZClass.raiseerror(ex)
    '        End Try

    '        Return Nothing

    '    End Function


    '    Private Function ConvertStringToArray(ByVal _incomString As String) As String()

    '        If (_incomString.Length > 0) Then

    '            Dim _indexArray As String() = _incomString.Split("|")

    '            Return _indexArray

    '        Else

    '            Return Nothing

    '        End If

    '    End Function
    '    Private Function ConvertArrayToString(ByVal _incomArray As String()) As String

    '        Dim i As Int16 = 0

    '        Dim _indexString As String = String.Empty

    '        For Each s As String In _incomArray

    '            If i = 0 Then
    '                _indexString = s
    '            Else
    '                _indexString = _indexString & "|" & s

    '            End If

    '        Next

    '        Return _indexString

    '    End Function

    '#End Region
#End Region


End Class
