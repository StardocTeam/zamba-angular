Public MustInherit Class txtBaseIndexCtrl
    Inherits ZControl
    Implements IDisposable

#Region "Constructores"
    Enum Modes
        Search
        Viewer
        Index
    End Enum
#End Region

#Region "Propiedades Publicas"
    Public _IsValid As Boolean = True
    Public MustOverride Property IsValid() As Boolean
    Public MustOverride Sub RollBack()
    Public MustOverride Sub Commit()

#End Region

#Region "Inicializadores"
    Public Index As IIndex
    Public MustOverride Sub RefreshControl(ByRef index As IIndex)
    Public MustOverride Sub RefreshControlDataTemp(ByRef index As IIndex)
    Protected Event IndexChanged()
#End Region

#Region "Eventos de Enter y Tab"
    Public Shadows Event EnterPressed()
    Public Shadows Event TabPressed()
#End Region

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                Index = Nothing
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub
    Private isDisposed As Boolean

    Public Sub New()
        BackColor = ZColors.BackIndexColor
    End Sub
End Class

