
'Clases para almacenar Notas 

Public Class BarcodeRemark
    Implements IBarcodeRemark

#Region " Atributos "
    Private _UserId As Integer
    Private _Remark As String = String.Empty
    Private _Order As Integer
#End Region

#Region " Propiedades "
    Public Property UserId() As Integer Implements IBarcodeRemark.UserId
        Get
            Return _UserId
        End Get
        Set(ByVal Value As Integer)
            _UserId = Value
        End Set
    End Property
    Public Property Remark() As String Implements IBarcodeRemark.Remark
        Get
            Return _Remark
        End Get
        Set(ByVal Value As String)
            _Remark = Value
        End Set
    End Property
    Public Property Order() As Integer Implements IBarcodeRemark.Order
        Get
            Return _Order
        End Get
        Set(ByVal Value As Integer)
            _Order = Value
        End Set
    End Property
#End Region

#Region " Constructores "

#End Region
End Class
