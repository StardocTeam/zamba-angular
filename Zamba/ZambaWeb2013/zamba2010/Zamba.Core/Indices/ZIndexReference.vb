Public Class ZIndexReference
    Implements IZIndexReference

    Private mIndexId As Int64
    Private mIndexName As String
    Private mIndexDocTypeId As Int64
    Private mTable As String
    Private mColumn As String
    Private mOperator As String

    Public Sub New(ByVal pIndexId As Int64, ByVal pIndexName As String, ByVal pIndexDocTypeId As Int64, ByVal pTable As String, ByVal pColumn As String, ByVal pOperator As String)
        Me.mIndexId = pIndexId
        Me.mIndexName = pIndexName
        Me.mIndexDocTypeId = pIndexId
        Me.mTable = pTable
        Me.mColumn = pColumn
        Me.mOperator = pOperator
    End Sub

    Property IndexId() As Int64 Implements IZIndexReference.IndexId
        Get
            Return mIndexId
        End Get
        Set(ByVal value As Int64)
            mIndexId = value
        End Set
    End Property
    Property IndexName() As String Implements IZIndexReference.IndexName
        Get
            Return mIndexName
        End Get
        Set(ByVal value As String)
            mIndexName = value
        End Set
    End Property
    Property IndexDocTypeId() As Int64 Implements IZIndexReference.IndexDocTypeId
        Get
            Return mIndexDocTypeId
        End Get
        Set(ByVal value As Int64)
            mIndexDocTypeId = value
        End Set
    End Property
    Property Table() As String Implements IZIndexReference.Table
        Get
            Return mTable
        End Get
        Set(ByVal value As String)
            mTable = value
        End Set
    End Property
    Property Column() As String Implements IZIndexReference.Column
        Get
            Return mColumn
        End Get
        Set(ByVal value As String)
            mColumn = value
        End Set
    End Property
    Property Operador() As String Implements IZIndexReference.Operador
        Get
            Return mOperator
        End Get
        Set(ByVal value As String)
            mOperator = value
        End Set
    End Property
    Property IndexNameAndOperatorAndColumn() As String
        Get
            Return (mIndexName + " " + mOperator + " " + mColumn)
        End Get
        Set(ByVal value As String)
            mOperator = value
        End Set
    End Property

End Class