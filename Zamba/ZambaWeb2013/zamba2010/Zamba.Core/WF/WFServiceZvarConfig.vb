<Serializable()>
Public Class WFServiceZvarConfig
    Implements IWFServiceZvarConfig

    Private _zvarName As String
    Private _tableName As String
    Private _rowNum As Long
    Private _colName As String
    Private _valueToAssign As Object

    Public Property ColName As String Implements IWFServiceZvarConfig.ColName
        Get
            Return _colName
        End Get
        Set(ByVal value As String)
            _colName = value
        End Set
    End Property

    Public Property RowNum As Long Implements IWFServiceZvarConfig.RowNum
        Get
            Return _rowNum
        End Get
        Set(ByVal value As Long)
            _rowNum = value
        End Set
    End Property

    Public Property TableName As String Implements IWFServiceZvarConfig.TableName
        Get
            Return _tableName
        End Get
        Set(ByVal value As String)
            _tableName = value
        End Set
    End Property

    Public Property ValueToAsign As Object Implements IWFServiceZvarConfig.ValueToAssign
        Get
            Return _valueToAssign
        End Get
        Set(ByVal value As Object)
            _valueToAssign = value
        End Set
    End Property

    Public Property ZvarName As String Implements IWFServiceZvarConfig.ZvarName
        Get
            Return _zvarName
        End Get
        Set(ByVal value As String)
            _zvarName = value
        End Set
    End Property
End Class
