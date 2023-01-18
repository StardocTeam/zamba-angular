''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.DocD
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' CLASE QUE CONTIENE TODOS LOS DATOS DE LOS ATRIBUTOS QUE HAY EN LA BASE DE DATOS           '
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class DocD
    Implements IDocD

#Region " Atributos "
    Private _index_Id As Integer
    Private _index_Name As String = String.Empty
    Private _alAsigned As New ArrayList
    Private _alNoAsigned As New ArrayList
    Private _index_type As IndexsType = IndexsType.index
    'ESTAS DOS PROPIEDADES SIRVEN PARA VERIFICAR SI el atributo fue MODIFICADO O 
    'CREADO EN LA BASE DE DATOS, ESTO ME PERMITE HACER UN USO MAS EFICIENTE DE LA BASE
    Private _index_created As Boolean = False
    Private _index_modified As Boolean = False
#End Region

#Region " Propiedades "
    Public Property Index_Id() As Integer Implements IDocD.Index_Id
        Get
            Return _index_Id
        End Get
        Set(ByVal Value As Integer)
            _index_Id = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Propiedad. Devuelve o Establece el nombre para un Atributo de Base de Datos
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Index_Name() As String Implements IDocD.Index_Name
        Get
            Return _index_Name
        End Get
        Set(ByVal Value As String)
            _index_Name = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Propiedad. Devuelve un ArrayList con las columnas que conforman el Atributo
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Asigned_Columns() As ArrayList Implements IDocD.Asigned_Columns
        Get
            Return _alAsigned
        End Get
        Set(ByVal Value As ArrayList)
            _alAsigned = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Propiedad. Devuelve las columnas que no conforman el Atributo
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property No_Asigned_Columns() As ArrayList Implements IDocD.No_Asigned_Columns
        Get
            Return _alNoAsigned
        End Get
        Set(ByVal Value As ArrayList)
            _alNoAsigned = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Propiedad. Devuelve o Establece si el atributo fue creado
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Index_Created() As Boolean Implements IDocD.Index_Created
        Get
            Return _index_created
        End Get
        Set(ByVal Value As Boolean)
            _index_created = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Propiedad. Devuelve o Establece si el atributo fue modificado
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Index_Modified() As Boolean Implements IDocD.Index_Modified
        Get
            Return _index_modified
        End Get
        Set(ByVal Value As Boolean)
            _index_modified = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Propiedad. Devuelve o establece el tipo de Atributo, "Unique, Not Null, etc"
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Index_type() As IndexsType Implements IDocD.Index_type
        Get
            Return _index_type
        End Get
        Set(ByVal Value As IndexsType)
            _index_type = Value
        End Set
    End Property
#End Region
End Class