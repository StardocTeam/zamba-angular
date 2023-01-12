Imports ZAMBA.AppBlock
Imports ZAMBA.Core
Public MustInherit Class txtBaseIndexCtrl
    Inherits ZwhiteControl

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

End Class

