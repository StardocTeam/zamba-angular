Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Servers.Server
Imports System.Collections.Generic
Imports System.Text

Public Class PostProcess_Factory

    Public Shared Sub SetPol_InbrokerProcess(ByVal id As String)
        If Server.IsOracle Then
            Dim ParValues() As Object = {id}
            'Dim ParNames() As Object = {"IDOP"}
            ' Dim parTypes() As Object = {13}
            Try
                Con.ExecuteNonQuery("SETPOL_INBROKER_PKG.SETPOL_INBROKER", ParValues)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    Public Shared Sub SetPol_InbrokerProcessFile(ByVal id As String)
        If Server.isOracle Then
            Dim ParValues() As Object = {id}
            Dim ParNames() As String = {"IDOP"}
            ' Dim parTypes() As Object = {13}
            Try
                Con.ExecuteNonQuery("SETPOL_INBROKER_PKG.SETPOL_INBROKER", ParValues)
                'Solo  catch por si tira error con el codpoliza( a veces numerico otras no)
            Catch
            End Try
        Else
            Dim ParValues() As Object = {id}
            'Dim ParNames() As Object = {"IDOP"}
            ' Dim parTypes() As Object = {13}
            Try
                Con.ExecuteNonQuery("SetDoc_I57Inbroker", ParValues)
                'Solo  catch por si tira error con el codpoliza( a veces numerico otras no)
            Catch
            End Try
        End If
    End Sub

    Public Shared Sub SetSin_InbrokerProcess(ByVal idSiniestro As Int32)
        Try
            If Server.isOracle Then
                Dim ParValues() As Object = {idSiniestro}
                Dim ParNames() As String = {"NROSIN"}
                ' Dim parTypes() As Object = {7}
                Try
                    Con.ExecuteNonQuery("SETSIN_INBROKER_PKG.SETSIN_INBROKER", ParValues)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        Catch ex As ArgumentOutOfRangeException
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try

    End Sub

    Public Shared Sub SetSin_InbrokerProcessFile(ByVal idSiniestro As Int32)
        Dim ParValues() As Object = {idSiniestro}
        Dim ParNames() As String = {"NROSIN"}
        ' Dim parTypes() As Object = {7}
        Try
            Servers.Server.Con.ExecuteNonQuery("SETSIN_INBROKER_PKG.SETSIN_INBROKER", ParValues)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Public Shared Sub SetORD_InbrokerProcess(ByVal nroOrden As Int32)
        If Server.isOracle Then
            Dim ParValues() As Object = {nroOrden}
            'Dim ParNames() As Object = {"NROORDEN"}
            ' Dim parTypes() As Object = {13}
            Try
                Con.ExecuteNonQuery("SETORD_INBROKER_pkg.SETORD_INBROKER", ParValues)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Else

            Dim ParValues() As Object = {nroOrden}
            'Dim ParNames() As Object = {"IDOPERACION"}
            ' Dim parTypes() As Object = {13}
            Try
                Con.ExecuteNonQuery("SETORD_INBROKER", ParValues)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    Public Shared Sub SetORD_InbrokerProcessFile(ByVal nroOrden As Int32)
        If Server.isOracle Then
            Dim ParValues() As Object = {nroOrden}
            Dim ParNames() As String = {"NROORDEN"}
            ' Dim parTypes() As Object = {13}
            Try
                Con.ExecuteNonQuery("SETORD_INBROKER_pkg.SETORD_INBROKER", ParValues)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Else
            Dim ParValues() As Object = {nroOrden}
            'Dim ParNames() As Object = {"IDOPERACION"}
            ' Dim parTypes() As Object = {13}
            Try
                Con.ExecuteNonQuery("SETORD_INBROKER", ParValues)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub



    ''' <summary>
    ''' Creado para el postproceso de ordenes de pago, se hace una llamada al stored SETODP_ZAMBA_PKG
    ''' </summary>
    ''' <param name="VoucherNum"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetODP_ZambaProcess(ByVal VoucherNum As Int32, ByVal DoctypeId As Int64, ByVal indexs As Dictionary(Of String, String))
        Dim query As New StringBuilder
        query.Append("SELECT ENTITY,BANK_ACCOUNT,DOCUMENT_NUM,PAYMENT_DATE,PAYMENT_AMOUNT,CURR,")
        query.Append("STATUS,STATUS_DATE,SUPPLIER,RATE_TYPE,RATE_DATE,PAYMENT_RATE")
        query.Append(" FROM ORDENES_PAGO WHERE VOUCHER_NUM='")
        query.Append(VoucherNum)
        query.Append("'")
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        For Each r As DataRow In ds.Tables(0).Rows
            query.Remove(0, query.Length)
            query.Append("UPDATE DOC_I")
            query.Append(DoctypeId)
            query.Append(" SET ")
            query.Append(indexs.Item("ENTITY"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("ENTITY")) Then
                query.Append("'")
                query.Append(r.Item("ENTITY"))
                query.Append("'")
            Else
                query.Append(" NULL ")
            End If

            query.Append(",")
            query.Append(indexs.Item("BANK_ACCOUNT"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("BANK_ACCOUNT")) Then
                query.Append("'")
                query.Append(r.Item("BANK_ACCOUNT"))
                query.Append("'")
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("DOCUMENT_NUM"))
            query.Append(" = ")

            If Not IsDBNull(r.Item("DOCUMENT_NUM")) Then
                query.Append("'")
                query.Append(r.Item("DOCUMENT_NUM"))
                query.Append("'")
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("PAYMENT_DATE"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("PAYMENT_DATE")) Then
                query.Append(Server.Con.ConvertDateTime(r.Item("PAYMENT_DATE").ToString))
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("PAYMENT_AMOUNT"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("PAYMENT_AMOUNT")) Then
                Dim tempvalue As String = r.Item("PAYMENT_AMOUNT")
                tempvalue = tempvalue.Replace(",", ".")
                query.Append(tempvalue)
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("CURR"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("CURR")) Then
                query.Append("'")
                query.Append(r.Item("CURR"))
                query.Append("'")
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("STATUS"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("STATUS")) Then
                query.Append("'")
                query.Append(r.Item("STATUS"))
                query.Append("'")
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("STATUS_DATE"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("STATUS_DATE")) Then
                query.Append(Server.Con.ConvertDateTime(r.Item("STATUS_DATE").ToString))
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("SUPPLIER"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("SUPPLIER")) Then
                query.Append("'")
                query.Append(r.Item("SUPPLIER"))
                query.Append("'")
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("RATE_TYPE"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("RATE_TYPE")) Then
                query.Append("'")
                query.Append(r.Item("RATE_TYPE"))
                query.Append("'")
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("RATE_DATE"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("RATE_DATE")) Then
                query.Append(Server.Con.ConvertDateTime(r.Item("RATE_DATE").ToString))
            Else
                query.Append(" NULL ")
            End If
            query.Append(",")
            query.Append(indexs.Item("PAYMENT_RATE"))
            query.Append(" = ")
            If Not IsDBNull(r.Item("PAYMENT_RATE")) Then
                Dim tempvalue As String = r.Item("PAYMENT_RATE")
                tempvalue = tempvalue.Replace(",", ".")
                query.Append(tempvalue)
            Else
                query.Append(" NULL ")
            End If
            query.Append(" WHERE ")
            query.Append(indexs.Item("VOUCHER_NUM"))
            query.Append(" = '")
            query.Append(VoucherNum)
            query.Append("'")
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        Next
        '******************* declaracion del paquete **********************
        '    CREATE OR REPLACE  PACKAGE "ZAMBA"."SETODP_ZAMBA_PKG"    AS 
        'PROCEDURE SETODP_ZAMBA (VARVOUCHERNUM IN DOC_I254.I59%TYPE); 
        'END;
        '******************* cuerpo del paquete ***************************
        '    CREATE OR REPLACE  PACKAGE BODY "ZAMBA"."SETODP_ZAMBA_PKG"    AS 
        'PROCEDURE SETODP_ZAMBA (VARVOUCHERNUM IN DOC_I254.I59%TYPE)IS
        'V_ENTITY DOC_I254.I214%TYPE; V_BANK_ACCOUNT 
        'DOC_I254.I215%TYPE; V_DOCUMENT_NUM DOC_I254.I69%TYPE; 
        'V_PAYMENT_DATE DOC_I254.I216%TYPE; V_PAYMENT_AMOUNT 
        'DOC_I254.I217%TYPE; V_CURR DOC_I254.I218%TYPE; V_STATUS 
        'DOC_I254.I219%TYPE; V_STATUS_DATE DOC_I254.I220%TYPE; 
        'V_SUPPLIER DOC_I254.I221%TYPE; V_RATE_TYPE 
        'DOC_I254.I222%TYPE; V_RATE_DATE DOC_I254.I223%TYPE; 
        'V_PAYMENT_RATE DOC_I254.I224%TYPE; BEGIN 
        'SELECT ENTITY,BANK_ACCOUNT,DOCUMENT_NUM,PAYMENT_DATE,
        'PAYMENT_AMOUNT,CURR,STATUS,STATUS_DATE,SUPPLIER,RATE_TYPE,
        'RATE_DATE,PAYMENT_RATE  
        'INTO V_ENTITY,V_BANK_ACCOUNT,V_DOCUMENT_NUM,V_PAYMENT_DATE,
        'V_PAYMENT_AMOUNT,V_CURR,V_STATUS,V_STATUS_DATE,V_SUPPLIER,
        'V_RATE_TYPE,V_RATE_DATE,V_PAYMENT_RATE 
        'FROM ORDENES_PAGO 
        'WHERE VOUCHER_NUM=VARVOUCHERNUM;  
        'UPDATE DOC_I254 
        'SET I214 = V_ENTITY, I215 = V_BANK_ACCOUNT, I69 = 
        'V_DOCUMENT_NUM, I216 = V_PAYMENT_DATE, I217 = 
        'V_PAYMENT_AMOUNT, I218 = V_CURR, I219 = V_STATUS, I220 = 
        'V_STATUS_DATE, I221 = V_SUPPLIER, I222 = V_RATE_TYPE, I223 = 
        'V_RATE_DATE, I224 = V_PAYMENT_RATE 
        'WHERE I59 = VARVOUCHERNUM; COMMIT; END SETODP_ZAMBA; END 
        'SETODP_ZAMBA_PKG;

    End Sub

End Class
