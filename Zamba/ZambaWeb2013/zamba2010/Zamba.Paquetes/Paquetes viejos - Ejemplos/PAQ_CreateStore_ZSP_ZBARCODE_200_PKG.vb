Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32

Public Class PAQ_CreateStore_ZSP_ZBARCODE_200_PKG
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateStore_ZSP_ZBARCODE_200_PKG"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateStore_ZSP_ZBARCODE_200_PKG
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea el Store PAQ_ZSP_ZBARCODE_200_PKG PARA ORACLE "
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("04/12/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.Installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.Orden
        Get
            Return 81
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        ' Dim strcreate As String
        Dim bResultado As Boolean = True

        Dim sql As New System.Text.StringBuilder
        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
            Try
                ZPaq.IfExists("PAQ_ZSP_ZBARCODE_200_PKG", ZPaq.Tipo.Package, Me.CanDrop)

                sql.Append("CREATE OR REPLACE PACKAGE  ")
                sql.Append(Chr(34) & "ZSP_ZBARCODE_200_PKG" & Chr(34))
                sql.Append(" as    procedure getPathForIdTypeIdDoc")
                sql.Append("( doc_id in numeric, doc_type_id in numeric );  end;")

                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)

                sql.Remove(0, sql.Length)
                sql.Append("CREATE OR REPLACE PACKAGE BODY ")
                sql.Append(Chr(34) & "ZSP_ZBARCODE_200_PKG" & Chr(34))
                sql.Append(" as    procedure getPathForIdTypeIdDoc")
                sql.Append("( doc_id in numeric, doc_type_id in numeric )")
                sql.Append("       is         consulta  varchar2(1000) := '';")
                sql.Append("         resultado varchar2(1000) := '';   ")
                sql.Append("             begin          dbms_output.enable(1000000);     ")
                sql.Append("         consulta := ' select ( DV.DISK_VOL_PATH || ''\'' || ")
                sql.Append("DV.DISK_VOL_ID || ''\'' || DT.DOC_TYPE_ID ' ||         ")
                sql.Append("             ' || ''\'' || DT.OFFSET || ''\'' || DT.DOC_FILE ) ")
                sql.Append("as RutaArchivo ' ||                       ' from disk_volume ")
                sql.Append(" DV inner join doc_t' || doc_type_id ||        ")
                sql.Append("               '  DT  on DV.disk_vol_id=DT.vol_id where DT.doc_id=' ")
                sql.Append("|| doc_id ||                       '  and DT.doc_type_id=' ")
                sql.Append("|| doc_type_id ||  ' order by DT.doc_id asc, DT.doc_type_id asc'; ")
                sql.Append("             execute immediate consulta into resultado;       ")
                sql.Append("            dbms_output.put_line(resultado);       end; end;")
                Dim sw2 As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw2.WriteLine("")
                sw2.WriteLine(sql.ToString)
                sw2.WriteLine("")
                sw2.Close()
                sw2 = Nothing

                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)

                'If GenerateScripts = False Then
                '	Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                'Else
                '	Dim sw3 As New IO.StreamWriter(application.StartupPath & "\scripts.txt", True)
                '	sw3.WriteLine("")
                '	sw3.WriteLine(sql.ToString)
                '	sw3.WriteLine("")
                '	sw3.Close()
                '	sw3 = Nothing
                'End If
            Catch ex As Exception
                ZException.Log(ex, False)
                bResultado = False
            End Try
        End If

        Return bResultado

    End Function

#End Region





End Class
