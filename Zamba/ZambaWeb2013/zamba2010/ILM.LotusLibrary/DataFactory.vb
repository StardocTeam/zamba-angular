Imports Zamba.Servers
Public Class DATA
    Implements IDisposable

    Dim _UserId As Integer
    Property UserId() As Integer
        Get
            Return _UserId
        End Get
        Set(ByVal Value As Integer)
            _UserId = Value
        End Set
    End Property

    Public Sub GuardarDatos(ByVal Reg As business.Registro, ByVal Accion As String)
        Try
            'NO GUARDA LA DIRECCION DE EMAIL DEL USUARIO, QUE ES EL ARRAY(2)
            If Accion = "Guardar" Then
                Dim strupdate As String = "Update USRNOTES Set CONF_REINTENTO = " & Reg.CONF_REINTENTO & ", CONF_CHARSREEMPSUBJ = '" & Reg.CONF_CHARSREEMPSUBJ & "', CONF_NOMARCHTXT = '" & Reg.CONF_NOMARCHTXT & "', CONF_BASEMAIL = '" & Reg.CONF_BASEMAIL & "', CONF_EJECUTABLE = '" & Reg.CONF_EJECUTABLE & "', CONF_PATHREMOTOARCH = '" & Reg.CONF_PATHREMOTOARCH & "', CONF_PATHARCH  ='" & Reg.CONF_PATHARCH & "',CONF_MAILSERVER ='" & Reg.CONF_MAILSERVER & "', CONF_NOMUSERNOTES = '" & Reg.CONF_NOMUSERNOTES & "', CONF_NOMUSERRED = '" & Reg.CONF_NOMUSERRED & "', CONF_ARCHCTRL='" & Reg.CONF_ARCHCTRL & "' Where Id =" & UserId   '& "',CONF_VISTAEXPORTACION='ExportoNotes',CONF_SEQMSG=0,CONF_SEQATT=0,CONF_LOCKEO=0,CONF_ACUMIMG=0,CONF_LIMIMG=0,CONF_DESTEXT=10240,CONF_TEXTOSUBJECT='EXPORTADO A ZAMBA',CONF_BORRAR='SI',CONF_SCHEDULESEL=1,CONF_SCHEDULEVAR=0,CONF_SEQIMG=0" & " Where Id =" & UserId
                'Dim strupdate As String = "Update USRNOTES Set CONF_REINTENTO = " & Val(Array(0)) & ", CONF_CHARSREEMPSUBJ = '" & Array(1) & "', CONF_NOMARCHTXT = '" & Array(3) & "', CONF_BASEMAIL = '" & Array(4) & "', CONF_EJECUTABLE = '" & Array(5) & "' Where Id =" & UserId
                Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
            End If

            If Accion = "Insertar" Then
                Dim strinsert As String = "Insert Into USRNOTES (CONF_PAPELERA,NOMBRE,ACTIVO,CONF_REINTENTO,CONF_CHARSREEMPSUBJ,CONF_NOMARCHTXT,CONF_BASEMAIL,CONF_EJECUTABLE,CONF_PATHREMOTOARCH,CONF_PATHARCH,CONF_MAILSERVER,CONF_NOMUSERNOTES,CONF_NOMUSERRED,Id,CONF_ARCHCTRL,CONF_VISTAEXPORTACION,CONF_SEQMSG,CONF_SEQATT,CONF_LOCKEO,CONF_ACUMIMG,CONF_LIMIMG,CONF_DESTEXT,CONF_TEXTOSUBJECT,CONF_BORRAR,CONF_SCHEDULESEL,CONF_SCHEDULEVAR,CONF_SEQIMG) VALUES ('(TrashExporto)','" & Reg.NOMBRE & "',1," & Reg.CONF_REINTENTO & ",'" & Reg.CONF_CHARSREEMPSUBJ & "','" & Reg.CONF_NOMARCHTXT & "','" & Reg.CONF_BASEMAIL & "','" & Reg.CONF_EJECUTABLE & "','" & Reg.CONF_PATHREMOTOARCH & "','" & Reg.CONF_PATHARCH & "','" & Reg.CONF_MAILSERVER & "','" & Reg.CONF_NOMUSERNOTES & "','" & Reg.CONF_NOMUSERRED & "'," & Reg.UserId & ",'" & Reg.CONF_ARCHCTRL & "','ExportoNotes',0,0,0,0,10240,'','" & Reg.CONF_TEXTOSUBJECT & "','SI',1,0,0)"
                Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
            End If
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try
    End Sub
    Public Sub GuardarListadoForms(ByVal Array As ArrayList, ByVal UserId As Integer, ByVal Accion As String)
        'BORRA LOS REGISTROS DEL USUARIO E INGRESA LOS NUEVOS
        Try
            If Accion = "Guardar" Then
                Dim strDelete As String = "Delete From USR_R_FORM Where IDUSR = " & UserId
                Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
            End If

            Dim i As Integer
            For i = 0 To Array.Count - 1
                Dim strupdate As String = "Insert Into USR_R_FORM (IDUSR,IDFORM) VALUES (" & UserId & "," & Array(i) & ")"
                Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
            Next

        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function ObtenerForms(ByVal usrid As Int32) As DataSet
        Dim sql As String = "Select IDFORM From USR_R_FORM Where IDUSR = " & usrid
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        sql = Nothing
        Return ds
    End Function

    Public Sub New()
        Dim server As New Server
        server.MakeConnection()
        server.dispose()
    End Sub
    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

    Public Function ValidarUser(ByVal Usuario As String, ByVal Contraseña As String) As Boolean
        Try
            Dim Id As Integer = GetUserID(Usuario)
            UserId = Id
            Dim strSelect As String = "Select password from usrtable where id=" & Id
            Dim PassVerdadera As String = Server.Con.ExecuteScalar(CommandType.Text, strSelect).ToString()
            If Contraseña = Zamba.Tools.Encryption.DecryptString(PassVerdadera, key, iv) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function GetUserID(ByVal name As String) As Int32
        Try
            Dim sql As String = "Select id from usrtable where name='" & name & "'"
            Return Server.Con.ExecuteScalar(CommandType.Text, sql)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function CargarTabla() As DataSet
        Try
            Dim strCargarTabla As String = "Select * From USRNOTES order by Nombre"
            Return Server.Con.ExecuteDataset(CommandType.Text, strCargarTabla)
        Catch ex As Exception

        End Try
    End Function
    Public Function LoadFormsToExport() As DataSet
        Try
            Dim sql As String = "Select * from frmnotes"
            'Terminar
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Return ds
        Catch ex As Exception
        End Try
    End Function
    Public Function CargarDatos(ByVal UserId As Integer) As DataSet
        Dim strCargar As String = "Select * From USRNOTES Where ID=" & UserId
        'TODO PATRICIO: PASAR A STORE PROCEDURE
        Return Server.Con.ExecuteDataset(CommandType.Text, strCargar)
    End Function
    Public Function VerificarExistencia(ByVal Userid As Integer) As Boolean
        Dim strSelectUsuario As String = "Select ID from usrnotes where ID=" & Userid 'aca va el nombre
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strSelectUsuario)
        If Not IsNothing(ds) Then
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function
    Public Function CargarListadoForms(ByVal UserId As Integer) As DataSet
        Dim strCargar As String = "Select IDFORM From USR_R_FORM Where IDUSR=" & UserId
        'TODO PATRICIO: PASAR A STORE PROCEDURE
        Return Server.Con.ExecuteDataset(CommandType.Text, strCargar)
    End Function
    Public Function ValidarUsuarioZamba(ByVal Usuario As String) As Integer
        Dim UserId As Integer = GetUserID(Usuario)
        If UserId = 0 Then Return 1

        Dim sql As String = "Select * From USRNOTES where ID=" & UserId
        If Server.Con.ExecuteDataset(CommandType.Text, sql).Tables(0).Rows.Count > 0 Then Return 2

        Return 3
    End Function
    Public Shared Function GetDocTypes() As DataSet
        Try
            Dim sql As String = "Select DOC_TYPE_ID, DOC_TYPE_NAME from doc_Type order by doc_type_name"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Return ds
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function GetDocTypeID(ByVal name As String) As Int32
        Dim sql As String = "Select DOC_TYPE_ID from doc_type where doc_type_name='" & name & "'"
        Return Server.Con.ExecuteScalar(CommandType.Text, sql)
    End Function
    Public Shared Function GetIndexType(ByVal indexid As Int32) As Int16
        Dim sql As String = "Select dropdown from doc_Index where Index_Id=" & indexid
        Return Server.Con.ExecuteScalar(CommandType.Text, sql)
    End Function
    Public Shared Function SaveShownIndexs(ByVal dtid As Int32, ByVal indexid As Int32, ByVal Mustcomplete As Boolean)
        Dim sql As String
        If DATA.GetIndexType(indexid) = 2 Then
            sql = "UPDATE Index_R_Doc_Type set Showlotus=1, Loadlotus=1, mustcomplete=" & CInt(Mustcomplete) & " Where Index_ID=" & indexid & " and DOC_TYPE_ID=" & dtid
        Else
            sql = "UPDATE Index_R_Doc_Type set Showlotus=1, Loadlotus=0, mustcomplete=" & CInt(Mustcomplete) & " Where Index_ID=" & indexid & " and DOC_TYPE_ID=" & dtid
        End If

    End Function

    Public Function actualizar()
        Dim sql As New System.Text.StringBuilder
        Dim DsUsers As New DataSet
        Dim id As Int32
        Dim i As Int32
        Dim b As Int32

        Try
            sql.Append("delete USR_R_FORM") 'borro los datos de la tabla
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        sql.Remove(0, sql.Length)

        Try
            sql.Append("select id from usrnotes order by id") ' obtener ids de usuarios
            DsUsers = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        sql.Remove(0, sql.Length)

        For b = 0 To DsUsers.Tables(0).Rows.Count - 1
            For i = 1 To 9
                id = DsUsers.Tables(0).Rows(b).Item(0)
                sql.Append("INSERT INTO USR_R_FORM VALUES(")
                sql.Append(i)
                sql.Append(",")
                sql.Append(id)
                sql.Append(")")
                Try
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString) 'actualiza tabla 
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
                sql.Remove(0, sql.Length)
            Next
        Next
    End Function

    Public Sub Dispose() Implements System.IDisposable.Dispose

    End Sub
    Public Function EliminarItem(ByVal Id As Integer) As Integer
        Try
            Dim strSql As String = "Delete From USRNOTES Where ID = " & Id
            Server.Con.ExecuteNonQuery(CommandType.Text, strSql)

        Catch ex As Exception
            Return 0            
        End Try

    End Function
#Region "Zopt"
    Public Shared Function GetCustomAddressBookList() As DataSet
        Dim sql As String = "Select * from zopt where item like '%AddressBookCustom%'"
        Return Server.Con.ExecuteDataset(CommandType.Text, sql)
    End Function
#End Region
End Class