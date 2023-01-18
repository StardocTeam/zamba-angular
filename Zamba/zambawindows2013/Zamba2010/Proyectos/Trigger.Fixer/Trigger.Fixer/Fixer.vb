Imports Zamba.Servers
Imports Zamba.Core
Imports System.Text

Public Class Fixer

    Private _oracleDBName As String
    Private _tg_created As Integer = 0
    Private _tg_errors As Integer = 0
    Private _pk_created As Integer = 0
    Private _pk_errors As Integer = 0

    Public Sub New()

        ZTrace.SetLevel(4, "Trigger Fixer")

        If Server.isOracle Then
            _oracleDBName = getOracleDBName()
        End If

    End Sub

    Public Sub Fix()

        FixTriggers()
        FixPrimariKeys()
        ShowResumen()

    End Sub

    Private Sub FixTriggers()

        Dim Id As Integer
        Dim ds As New DataSet

        Log("***********************************************")
        Log("Arreglando triggers")
        Log("Leyendo documentos")

        ds = getDocTypeIds()

        If ds.Tables(0).Rows.Count > 0 Then

            Log("Se obtuvieron " & ds.Tables(0).Rows.Count.ToString() & " tipos de documentos")

            For Each doc As DataRow In ds.Tables(0).Rows

                Id = doc("DOC_TYPE_ID")

                Log("DocTypeId: " & Id.ToString())

                If Table_Exists("doc_i" & Id.ToString()) Then

                    If Not TriggerInsertExists(Id) Then
                        createTriggerInsert(Id)
                    Else
                        Log("     Trigger insert: ya existe")
                    End If

                    If Not TriggerUpdateExists(Id) Then
                        createTriggerUpdate(Id)
                    Else
                        Log("     Trigger update: ya existe")
                    End If

                Else
                    Log("     No existe la tabla DOC_I" & Id.ToString())
                End If

            Next

        Else
            Log(" *** No se obtuvo ningun documento *** ")
        End If

    End Sub

    Private Sub FixPrimariKeys()

        Dim Id As Integer
        Dim ds As New DataSet

        Log("***********************************************")
        Log("Arreglando primaris keys")
        Log("Leyendo SLST")

        ds = getSLSTIds()

        If ds.Tables(0).Rows.Count > 0 Then

            Log("Se obtuvieron " & ds.Tables(0).Rows.Count.ToString() & " SLST")

            For Each slst As DataRow In ds.Tables(0).Rows

                Id = slst("INDEX_ID")

                Log("SLST: " & Id.ToString())

                If Table_Exists("slst_s" & Id.ToString()) Then

                    If Not PrimaryKeyExists(Id) Then
                        addPrimaryKey(Id)
                    Else
                        Log("     Primary Key: ya existe")
                    End If

                Else
                    Log("     No existe la tabla SLST_S" & Id.ToString())
                End If

            Next

        End If

    End Sub

    Private Sub ShowResumen()

        Log("")
        Log("")
        Log("***********************************************")
        Log(" Proceso finalizado !!")
        Log("")
        Log("   Se crearon " & _tg_created.ToString & " triggers")
        Log("   Se produjeron " & _tg_errors.ToString & " errores con triggers")
        Log("")
        Log("   Se agregaron " & _pk_created.ToString & " primaris keys")
        Log("   Se produjeron " & _pk_errors.ToString & " errores con primaris keys")
        Log("")
        Log("***********************************************")

        Console.ReadLine()

    End Sub

    Private Function getDocTypeIds() As DataSet

        Dim ds As New DataSet

        Try
            ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT DOC_TYPE_ID FROM DOC_TYPE")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return ds

    End Function

    Private Function getSLSTIds() As DataSet

        Dim ds As New DataSet

        Try
            ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT INDEX_ID FROM DOC_INDEX WHERE DROPDOWN = 2")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return ds

    End Function

    Private Function Table_Exists(ByVal TableName As String) As Boolean

        Dim sb As New StringBuilder

        If Server.isOracle Then

            sb.Append(" SELECT COUNT(*) FROM dba_tables ")
            sb.Append(" WHERE LOWER(OWNER) = '" & _oracleDBName & "' ")
            sb.Append(" AND LOWER(TABLE_NAME) = '" & TableName.ToLower().Trim & "'")

        ElseIf Server.isSQLServer Then

            sb.Append(" SELECT COUNT(*) FROM dbo.sysobjects ")
            sb.Append(" WHERE NAME = '" & TableName & "' ")
            sb.Append(" AND OBJECTPROPERTY(id, 'IsTable') = 1 ")

        Else
            Log(" *** DB no soportada *** ")
            Return False
        End If

        If Convert.ToInt32(Server.Con.ExecuteScalar(CommandType.Text, sb.ToString)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function TriggerInsertExists(ByVal DocTypeId As Long) As Boolean

        Dim sb As New StringBuilder

        If Server.isOracle Then

            sb.Append(" SELECT COUNT(*) FROM dba_triggers ")
            sb.Append(" WHERE LOWER(OWNER) = '" & _oracleDBName & "' ")
            sb.Append(" AND LOWER(TRIGGER_NAME) = 'ti_" & DocTypeId & "' ")

        ElseIf Server.isSQLServer Then

            sb.Append(" SELECT COUNT(*) FROM dbo.sysobjects ")
            sb.Append(" WHERE LOWER(NAME) = 'ti_" & DocTypeId & "' ")
            sb.Append(" AND OBJECTPROPERTY(id, 'IsTrigger') = 1 ")

        Else
            Log(" *** DB no soportada *** ")
            Return False
        End If

        If Convert.ToInt32(Server.Con.ExecuteScalar(CommandType.Text, sb.ToString)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function TriggerUpdateExists(ByVal DocTypeId As Long) As Boolean

        Dim sb As New StringBuilder

        If Server.isOracle Then

            sb.Append(" SELECT COUNT(*) FROM dba_triggers ")
            sb.Append(" WHERE LOWER(OWNER) = '" & _oracleDBName & "' ")
            sb.Append(" AND LOWER(TRIGGER_NAME) = 'tu_" & DocTypeId & "'")

        ElseIf Server.isSQLServer Then

            sb.Append(" SELECT COUNT(*) FROM dbo.sysobjects ")
            sb.Append(" WHERE LOWER(NAME) = 'tu_" & DocTypeId & "' ")
            sb.Append(" AND OBJECTPROPERTY(id, 'IsTrigger') = 1 ")

        Else
            Log(" *** DB no soportada *** ")
            Return False
        End If

        If Convert.ToInt32(Server.Con.ExecuteScalar(CommandType.Text, sb.ToString)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub createTriggerInsert(ByVal DocTypeId As Long)

        Dim sb As New StringBuilder

        If Server.isOracle Then

            sb.Append(" CREATE OR REPLACE TRIGGER TI_" & DocTypeId & " before ")
            sb.Append(" INSERT ON DOC_I" & DocTypeId & " FOR EACH ROW BEGIN ")
            sb.Append(" :new.crdate:=sysdate; :new.lupdate:=sysdate; ")
            sb.Append(" END TI_" & DocTypeId & ";")

        ElseIf Server.isSQLServer Then

            sb.Append(" CREATE TRIGGER TI_" & DocTypeId)
            sb.Append(" ON doc_i" & DocTypeId & " FOR INSERT AS BEGIN ")
            sb.Append(" UPDATE doc_i" & DocTypeId & " SET lupdate = getdate(), ")
            sb.Append(" crdate = getdate() WHERE doc_id IN ")
            sb.Append(" (SELECT doc_id FROM Inserted) END ")

        Else
            Log(" *** DB no soportada *** ")
            Exit Sub
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, sb.ToString)

        If TriggerInsertExists(DocTypeId) Then
            Log("     Creando trigger insert: ok")
            _tg_created = _tg_created + 1
        Else
            Log("     Creando trigger insert: ERROR")
            _tg_errors = _tg_errors + 1
        End If

    End Sub

    Private Sub createTriggerUpdate(ByVal DocTypeId As Long)

        Dim sb As New StringBuilder

        If Server.isOracle Then

            sb.Append(" CREATE OR REPLACE TRIGGER TU_" & DocTypeId & " BEFORE ")
            sb.Append(" UPDATE ON DOC_I" & DocTypeId & "  FOR EACH ROW BEGIN ")
            sb.Append(" :new.lupdate:=sysdate; END tu_" & DocTypeId & ";")

        ElseIf Server.isSQLServer Then

            sb.Append(" CREATE TRIGGER TU_" & DocTypeId)
            sb.Append("	ON doc_i" & DocTypeId & " FOR UPDATE AS BEGIN ")
            sb.Append(" UPDATE doc_i" & DocTypeId & " SET lupdate = getdate() ")
            sb.Append(" WHERE doc_id IN (SELECT doc_id FROM Inserted) END ")

        Else
            Log(" *** DB no soportada *** ")
            Exit Sub
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, sb.ToString)

        If TriggerUpdateExists(DocTypeId) Then
            Log("     Creando trigger update: ok")
            _tg_created = _tg_created + 1
        Else
            Log("     Creando trigger update: ERROR")
            _tg_errors = _tg_errors + 1
        End If

    End Sub

    Private Function PrimaryKeyExists(ByVal SLSTId As Long) As Boolean

        Dim sb As New StringBuilder

        If Server.isOracle Then

            sb.Append(" SELECT COUNT(*) FROM dba_constraints  ")
            sb.Append(" WHERE LOWER(OWNER) = '" & _oracleDBName & "' ")
            sb.Append(" AND LOWER(TABLE_NAME) = 'slst_s" & SLSTId & "' ")
            sb.Append(" AND constraint_type = 'P' ")

        ElseIf Server.isSQLServer Then

            sb.Append(" SELECT COUNT(*) FROM dbo.sysobjects ")
            sb.Append(" WHERE LOWER(NAME) = 'pk_codigo" & SLSTId & "' ")
            sb.Append(" AND OBJECTPROPERTY(id, 'IsConstraint') = 1 And xtype = 'PK' ")

        Else
            Log(" *** DB no soportada *** ")
            Return False
        End If

        If Convert.ToInt32(Server.Con.ExecuteScalar(CommandType.Text, sb.ToString)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub addPrimaryKey(ByVal SLSTId As Long)

        Dim sb As New StringBuilder

        If Server.isOracle Then

            sb.Append(" ALTER TABLE SLST_S" & SLSTId)
            sb.Append(" ADD (CONSTRAINT ""PK_CODIGO" & SLSTId & """")
            sb.Append(" PRIMARY KEY (""CODIGO"")) ")

        ElseIf Server.isSQLServer Then

            sb.Append(" ALTER TABLE [SLST_S" & SLSTId & "]")
            sb.Append(" ADD CONSTRAINT PK_CODIGO" & SLSTId)
            sb.Append(" PRIMARY KEY (CODIGO) ")

        Else
            Log(" *** DB no soportada *** ")
            Exit Sub
        End If

        Server.Con.ExecuteScalar(CommandType.Text, sb.ToString)

        If PrimaryKeyExists(SLSTId) Then
            Log("     Agregando primary key: ok")
            _pk_created = _pk_created + 1
        Else
            Log("     Agregando primary key: ERROR")
            _pk_errors = _pk_errors + 1
        End If

    End Sub

    Private Function getOracleDBName() As String

        For Each Data As String In Server.Con.ConString.Split(";")
            If Data.ToLower.Contains("user id") Then
                Return Data.Split("=")(1).ToLower().Trim()
            End If
        Next

        Return String.Empty

    End Function

    Private Sub Log(ByVal mensaje As String)
        Dim aux As String = DateTime.Now.ToString("hh:mm:ss") + ": " + mensaje
        Trace.WriteLineIf(ZTrace.IsVerbose, aux)
        Console.WriteLine(aux)
    End Sub

End Class