
<Serializable()> Public Class UserView
    Implements IUserView

#Region " Atributos "
    Private User As IUser
#End Region

#Region " Propiedades "
    Public ReadOnly Property Nombre() As String Implements IUserView.Nombre
        Get
            Return User.Name
        End Get
    End Property

    Public ReadOnly Property Id() As Int32 Implements IUserView.Id
        Get
            Return Convert.ToInt32(User.ID)
        End Get
    End Property
#End Region

#Region " Constructor "
    Public Sub New(ByVal U As IUser)
        Me.User = U
    End Sub
#End Region

End Class