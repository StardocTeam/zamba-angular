Imports Zamba.Servers
Imports Zamba.AppBlock

Public Class PAQ_CreateStore_Zsp_Workflow_100
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea procedimientos Zsp_Workflow_100"
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("04/05/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "1.5.9"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateStore_Zsp_Workflow_100"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateStore_Zsp_Workflow_100
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
            Return 80
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Try
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

                ExecSQL(GenerateScripts)
            Else
                MessageBox.Show("Este paquete solo esta disponible para SQL Server.", "Zamba Paquetes", MessageBoxButtons.OK)
                'ExecOracle(GenerateScripts)

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return True

    End Function

    'Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
    '    Dim sql As New System.Text.StringBuilder
    '    Dim var As Boolean

    '    '---------------------------------
    '    'Zsp_workflow_100_UpdateParamItem
    '    '---------------------------------
    '    If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
    '        'SQL
    '        If Not ZPaq.IfExists("Zsp_workflow_100_UpdateParamItem", ZPaq.Tipo.StoredProcedure, False) Then
    '            If MessageBox.Show("El Procedimiento Almacenado 'Zsp_workflow_100_UpdateParamItem' ya existe. Desea borrarlo?", "Creacion de Tabla", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) = DialogResult.Yes Then
    '                sql.Append("DROP procedure Zsp_workflow_100_UpdateParamItem")
    '                If GenerateScripts = False Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                    sw.WriteLine("")
    '                    sw.WriteLine(sql.ToString)
    '                    sw.WriteLine("")
    '                    sw.Close()
    '                    sw = Nothing
    '                End If
    '                var = True
    '                sql.Remove(0, sql.Length)
    '            End If
    '        Else
    '            var = True
    '        End If
    '        If var = True Then
    '            sql.Append("CREATE  PROCEDURE Zsp_workflow_100_UpdateParamItem @Value varchar(2000), @RuleId numeric, @Item numeric AS if (select count(*) from WFRuleParamItems where  rule_id=@RuleId  And Item = @Item) > 0 Begin UPDATE WFRuleParamItems set value=@Value where rule_id=@RuleId  And Item = @Item End Else Begin INSERT INTO WFRuleParamItems (Rule_id, Item, Value) VALUES( @RuleId, @Item,  @Value) End")
    '            Try
    '                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '            Catch ex As Exception
    '                MsgBox("Error en la creación del paquete 'Zsp_workflow_100_UpdateParamItem'")
    '            End Try
    '        End If


    '    Else
    '                'ORACLE
    '                sql.Append("CREATE OR REPLACE PACKAGE zsp_GetMyMessagesNew_Pkg AS type t_cursor is ref cursor;PROCEDURE zsp_GetMyMessagesNew(my_id IN MSG_DEST.user_id%type,io_cursor OUT t_cursor);END zsp_GetMyMessagesNew_Pkg;")
    '                Try
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Catch ex As Exception
    '                    MsgBox("Error al ejecutar el paquete")
    '                End Try
    '                sql.Remove(0, sql.Length)
    '                sql.Append("CREATE OR REPLACE PACKAGE BODY zsp_GetMyMessagesNew_Pkg AS PROCEDURE zsp_GetMyMessagesNew(my_id IN MSG_DEST.user_id%type,io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor for Select msg_id from MSG_DEST where MSG_DEST.READ='0' and ")
    '                sql.Append("user_id = my_id and MSG_DEST.deleted='0';io_cursor := v_cursor; END zsp_GetMyMessagesNew;	End zsp_GetMyMessagesNew_Pkg;")
    '                Try
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Catch ex As Exception
    '                    MsgBox("Error al ejecutar el paquete")
    '                End Try
    '    End If

    '        sql = Nothing
    '        Return True
    'End Function

    Private Shared Sub ExecSQL(ByVal generatescripts As Boolean)
        Dim sql As New System.Text.StringBuilder
        Try

            If ZPaq.IfExists("Zsp_workflow_100_UpdateParamItem", ZPaq.Tipo.StoredProcedure, False) Then
                If MessageBox.Show("El Procedimiento Almacenado 'Zsp_workflow_100_UpdateParamItem' ya existe. Desea borrarlo?", "Creacion de Tabla", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) = DialogResult.Yes Then
                    sql.Append("DROP PROCEDURE Zsp_workflow_100_UpdateParamItem")
                    If Not generatescripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                    sql.Remove(0, sql.Length)
                End If

            Else

                sql.Append("CREATE PROCEDURE Zsp_workflow_100_UpdateParamItem @Value varchar(2000), @RuleId numeric, @Item numeric AS if (select count(*) from WFRuleParamItems where  rule_id=@RuleId  And Item = @Item) > 0 Begin UPDATE WFRuleParamItems set value=@Value where rule_id=@RuleId  And Item = @Item End Else Begin INSERT INTO WFRuleParamItems (Rule_id, Item, Value) VALUES( @RuleId, @Item,  @Value) End")

                If Not generatescripts Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If

                sql.Remove(0, sql.Length)

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub
    'Private Shared Sub ExecOracle(ByVal generatescripts As Boolean)
    '    Dim sql As New System.Text.StringBuilder
    '    Try
    '        '--ENCABEZADO DE PAQUETE ORACLE
    '        sql.Append("CREATE OR REPLACE PACKAGE " & Chr(34) & "Zsp_workflow_100_UpdateParamItem" & Chr(34) & " as type ")
    '        sql.Append(" t_cursor is ref cursor;")
    '        sql.Append("Procedure UPDATEPARAMITEMS(Value IN varchar2(2000), RuleId IN number, Item IN number)")
    '        sql.Append(" end Zsp_workflow_100_UpdateParamItem;")

    '        If generatescripts = False Then
    '            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '        Else
    '            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '            sw.WriteLine("")
    '            sw.WriteLine(sql.ToString)
    '            sw.WriteLine("")
    '            sw.Close()
    '            sw = Nothing
    '        End If

    '        sql.Remove(0, sql.Length)


    '        '' AS if (select count(*) from WFRuleParamItems where  rule_id=@RuleId  And Item = @Item) > 0 Begin UPDATE WFRuleParamItems set value=@Value where rule_id=@RuleId  And Item = @Item End Else Begin INSERT INTO WFRuleParamItems (Rule_id, Item, Value) VALUES( @RuleId, @Item,  @Value) End")
    '        'sql.Append("Procedure GETVERSIONDETAILS(Param_docId in number,io_cursor out t_cursor);")
    '        'sql.Append("Procedure INSERTVERSIONCOMMENT(Par_docId IN number,Par_comment in varchar2);")
    '        'sql.Append("Procedure INSERTPUBLISH(Parm_publishid IN number,Parm_docid IN number,Parm_userid IN number,Par_publishdate in date);")


    '        'sql.Append("CREATE PROCEDURE Zsp_workflow_100_UpdateParamItem @Value varchar(2000), @RuleId numeric, @Item numeric AS if (select count(*) from WFRuleParamItems where  rule_id=@RuleId  And Item = @Item) > 0 Begin UPDATE WFRuleParamItems set value=@Value where rule_id=@RuleId  And Item = @Item End Else Begin INSERT INTO WFRuleParamItems (Rule_id, Item, Value) VALUES( @RuleId, @Item,  @Value) End")

    '        '--CUERPO DEL PAQUETE
    '        sql.Append("CREATE OR REPLACE PACKAGE BODY " & Chr(34) & "Zsp_workflow_100_UpdateParamItem" & Chr(34) & " as Procedure UPDATEPARAMITEMS(Value IN varchar2(2000), RuleId IN number, Item IN number)")
    '        sql.Append("BEGIN ")
    '        sql.Append(" open v_cursor for ;")

    '        sql.Append(" io_cursor:=v_cursor;")
    '        sql.Append(" end UPDATEPARAMITEMS;")

    '        '--CUERPO DEL PAQUETE
    '        sql.Append("CREATE OR REPLACE PACKAGE BODY " & Chr(34) & "Zsp_workflow_100_UpdateParamItem" & Chr(34) & " as Procedure GETVERSIONDETAILS(Param_docId in number,io_cursor out t_cursor) is v_cursor t_cursor;")
    '        sql.Append("BEGIN ")
    '        sql.Append(" open v_cursor for Select * from ZComment where docid= Param_docId;")
    '        sql.Append(" io_cursor:=v_cursor;")
    '        sql.Append(" end GETVERSIONDETAILS;")

    '        sql.Append(" Procedure INSERTVERSIONCOMMENT(Par_docId in number,Par_comment in varchar2)                      ")
    '        sql.Append(" IS")
    '        sql.Append(" v_cursor t_cursor; ")
    '        sql.Append(" begin ")
    '        sql.Append("  INSERT INTO ZComment VALUES (Par_docId,Par_comment,sysdate); COMMIT; ")
    '        sql.Append(" end INSERTVERSIONCOMMENT;")
    '        sql.Append("procedure INSERTPUBLISH(Parm_publishid IN number,Parm_docid IN number,Parm_userid IN number,Par_publishdate in date) is v_cursor t_cursor;  begin INSERT INTO Z_Publish(PublishId, DocId, UserId, PublishDate) VALUES(Parm_publishid, Parm_docid, Parm_userid, Par_publishdate); COMMIT;  end INSERTPUBLISH;")
    '        sql.Append("END ZSP_VERSION_300;")
    '        '---
    '        If generatescripts = False Then
    '            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '        Else
    '            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '            sw.WriteLine("")
    '            sw.WriteLine(sql.ToString)
    '            sw.WriteLine("")
    '            sw.Close()
    '            sw = Nothing
    '        End If

    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
#End Region

End Class


