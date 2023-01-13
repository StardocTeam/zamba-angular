<Serializable()> Public MustInherit Class ZBaseCore
    Inherits ZClass
    Implements IZBaseCore

#Region " Atributos "
    Private _id As Int64
    Private _name As String
#End Region

#Region " Propiedades "
    <PropiedadesType(Propiedades.PropiedadPublica)> _
        Public Overridable Property Name() As String Implements IZBaseCore.Name
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value.Trim
        End Set
    End Property
    Public Property ID() As Int64 Implements IZBaseCore.ID
        Get
            Return _id
        End Get
        Set(ByVal value As Int64)
            _id = value
        End Set
    End Property
    Public MustOverride ReadOnly Property IsFull() As Boolean Implements ILazyLoad.IsFull
    Public MustOverride ReadOnly Property IsLoaded() As Boolean Implements ILazyLoad.IsLoaded
#End Region

#Region " Constructores "
    Public Sub New()

    End Sub
#End Region

    Public MustOverride Sub FullLoad() Implements ILazyLoad.FullLoad
    Public MustOverride Sub Load() Implements ILazyLoad.Load

    Public Shared Event ForceLoad(ByVal instance As IZBaseCore)
    Public Shared Sub CallForceLoad(ByVal instance As IZBaseCore)
        RaiseEvent ForceLoad(instance)
    End Sub

#Region "Rules"
    ''' <summary>
    ''' Event for load rules preferences
    ''' </summary>
    ''' <param name="instance"></param>
    ''' <history>
    '''     Marcelo Created 01/10/2009
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Event loadRulePreference(ByRef instance As IWFRuleParent)
    Public Shared Sub CallLoadRulePreference(ByRef instance As IWFRuleParent)
        RaiseEvent loadRulePreference(instance)
    End Sub
#End Region
End Class