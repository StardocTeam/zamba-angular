Imports Zamba.Core
Imports System.Collections
Imports System.Collections.Generic

Public Class DynamicFormState

    Private _formValues As New List(Of Hashtable)
    Private _doctypeid As Int64
    Private _formName As String
    Private _doctypeName As String
    Private _formid As Int64
    Private _edit As Boolean
    Private _useIndexProperties As Boolean
    Private _isFinish As Boolean
    Private _DynamicFormGridviewDatasource As DataSet
    Private _IsFormsRenavigated As Boolean

    Sub New(ByVal doctypeid As Int64, Optional ByVal editmode As Boolean = False, Optional ByVal formid As Int64 = -1)
        _doctypeid = doctypeid
        _edit = editmode
        _formid = formid
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Doctypeid() As Int64
        Get
            Return _doctypeid
        End Get
        Set(ByVal value As Int64)
            _doctypeid = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DynamicFormGridviewDatasource() As DataSet
        Get
            Return _DynamicFormGridviewDatasource
        End Get
        Set(ByVal value As DataSet)
            _DynamicFormGridviewDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Edit() As Boolean
        Get
            Return _edit
        End Get
        Set(ByVal value As Boolean)
            _edit = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Formid() As Int64
        Get
            Return _formid
        End Get
        Set(ByVal value As Int64)
            _formid = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DynamicFormValues() As List(Of Hashtable)
        Get
            Return _formValues
        End Get
        Set(ByVal value As List(Of Hashtable))
            _formValues = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FormName() As String
        Get
            Return _formName
        End Get
        Set(ByVal value As String)
            _formName = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DoctypeName() As String
        Get
            Return _doctypeName
        End Get
        Set(ByVal value As String)
            _doctypeName = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsFinish() As Boolean
        Get
            Return _isFinish
        End Get
        Set(ByVal value As Boolean)
            _isFinish = value
        End Set
    End Property

    ''' <summary>
    ''' Si vuelve a navegar los forms, con la opcion [Anterior] de los forms, 
    ''' utilizado para el reload de los controles
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsFormsRenavigated() As Boolean
        Get
            Return _IsFormsRenavigated
        End Get
        Set(ByVal value As Boolean)
            _IsFormsRenavigated = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseIndexProperties() As Boolean
        Get
            Return _useIndexProperties
        End Get
        Set(ByVal value As Boolean)
            _useIndexProperties = value
        End Set
    End Property

#Region "IndexsPropertiesForm"

    Dim _IndexsPropertiesFormValues As New List(Of String())

    Public Property IndexsPropertiesFormValues() As List(Of String())
        Get
            Return _IndexsPropertiesFormValues
        End Get
        Set(ByVal value As List(Of String()))
            _IndexsPropertiesFormValues = value
        End Set
    End Property

#End Region

#Region "IndexsConditionsForm"

    Private _conditionsFormValues As New List(Of Hashtable)

    Public Property ConditionsFormValues() As List(Of Hashtable)
        Get
            Return _conditionsFormValues
        End Get
        Set(ByVal value As List(Of Hashtable))
            _conditionsFormValues = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' Actualiza el form id, se utiliza para un nuevo formulario dinamico
    ''' </summary>
    ''' <param name="formId">form id</param>
    ''' <remarks></remarks>
    ''' <history>diego 30.3.2009 created</history>
    Public Sub UpdateFormId(ByVal formId As Int64)
        Try
            Me.Formid = formId

            'actualiza formid en valores del formulario de creacion
            For Each item As Hashtable In _formValues
                item.Item("QueryId") = formId
            Next

            'actualiza formid en valores del formulario condiciones
            For Each Item As Hashtable In _conditionsFormValues
                Item.Item("FormId") = formId
            Next

            'actualiza formid en valores del formulario de propiedades de atributos
            For Each Item As String() In _IndexsPropertiesFormValues
                Item(0) = formId.ToString
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
End Class