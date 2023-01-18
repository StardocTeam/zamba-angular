
Public Class Barcode

    Private _id As Integer
    Private _datetime As Date
    Private _userid As Integer
    Private _scanned As Boolean
    Private _scanneddate As Date
    Private _doc_id As Integer

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = Value
        End Set
    End Property
    Public Property datetime() As Date
        Get
            Return _datetime
        End Get
        Set(ByVal Value As Date)
            _datetime = Value
        End Set
    End Property
    Public Property userid() As Integer
        Get
            Return _userid
        End Get
        Set(ByVal Value As Integer)
            _userid = Value
        End Set
    End Property
    Public Property scanned() As Boolean
        Get
            Return _scanned
        End Get
        Set(ByVal Value As Boolean)
            _scanned = Value
        End Set
    End Property
    Public Property scanneddate() As Date
        Get
            Return _scanneddate
        End Get
        Set(ByVal Value As Date)
            _scanneddate = Value
        End Set
    End Property
    Public Property doc_id() As Integer
        Get
            Return _doc_id
        End Get
        Set(ByVal Value As Integer)
            _doc_id = Value
        End Set
    End Property

    Sub New()
    End Sub

End Class


Public Class BarcodeRemark_Motor
    Inherits Zamba.Core.ZClass

    'Public Shared Function LoadRemarks(ByVal UserId As Integer) As ArrayList
    '    Dim ds As New DataSet
    '    Try
    '        Dim strSelect As String = "SELECT USERID,REMARK,ORDER FROM zbarcode_remark WHERE USERID =" & UserId & " order by 3"
    '        ds = server.Con.ExecuteDataset(CommandType.Text, strSelect)
    '        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '            Dim bcremark As New BarcodeRemark
    '            bcremark.UserId = ds.Tables(0).Rows(i).Item(0)
    '            bcremark.Remark = ds.Tables(0).Rows(i).Item(1)
    '            bcremark.Order = ds.Tables(0).Rows(i).Item(2)
    '            LoadRemarks.Add(bcremark)
    '        Next
    '        Return LoadRemarks
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Function

    'Public Shared Sub SaveRemark(ByVal UserId As Integer, ByVal remark As String)
    '    If remark = "" Then Exit Sub
    '    Try
    '        'subo el order de todos en 1
    '        Dim strUpdate As String = "UPDATE zbarcode_remark SET zbarcode_remark.order = zbarcode_remark.order + 1 where userid=" & UserId
    '        server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)

    '        'borro el order que sea igual a 6
    '        Dim strDelete As String = "DELETE FROM zbarcode_remark WHERE zbarcode_remark.order => 6 and userid=" & UserId
    '        server.Con.ExecuteNonQuery(CommandType.Text, strDelete)

    '        'inserto el nuevo comentario en la posicion 1
    '        Dim strInsert As String = "INSERT INTO zbarcode_remark VALUES(" & UserId & ",'" & remark & "',1)"
    '        server.Con.ExecuteNonQuery(CommandType.Text, strInsert)

    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub

    Public Overrides Sub Dispose()

    End Sub
End Class