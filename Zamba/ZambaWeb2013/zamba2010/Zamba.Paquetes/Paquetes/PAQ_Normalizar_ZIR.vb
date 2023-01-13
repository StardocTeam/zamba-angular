Imports Zamba.Core
''' <summary>
''' Verifica que no existan permisos duplicados. En caso de existir los elimina.
''' Luego crea la PK en ZIR y verifica que los valores de DocTypeId e IndexId 
''' existan en DOC_TYPE y DOC_INDEX respectivamente. En caso de no existir se le
''' informa al usuario que X registros se eliminaran por inconsistentes y al finalizar
''' se intentará crear la FK entre ZIR-DOC_TYPE y entre ZIR-DOC_INDEX.
''' </summary>
''' <history>
''' Tomas   23/05/2011  Created
''' </history>
''' <remarks></remarks>
Public Class PAQ_Normalizar_ZIR
    Inherits ZPaq
    Implements IPAQ

#Region "Atributos y propiedades"
    Private Const _name As String = "PAQ_Normalizar_ZIR"
    Private Const _description As String = "Verifica que no existan permisos duplicados. En caso de existir los elimina. Luego crea la PK en ZIR y verifica que los valores de DocTypeId e IndexId existan en DOC_TYPE y DOC_INDEX respectivamente. En caso de no existir se le informa al usuario que X registros se eliminaran por inconsistentes y al finalizar se intentará crear la FK entre ZIR-DOC_TYPE y entre ZIR-DOC_INDEX."
    Private Const _version As String = "1"
    Private Const _fechaCreacion As String = "23/05/2011"
    Private _installed As Boolean


    Public ReadOnly Property Description() As String Implements IPAQ.Description
        Get
            Return _description
        End Get
    End Property
    Public Property Installed() As Boolean Implements IPAQ.Installed
        Get
            Return _installed
        End Get
        Set(ByVal value As Boolean)
            _installed = value
        End Set
    End Property
    Public ReadOnly Property Name() As String Implements IPAQ.Name
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property Number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_Normalizar_ZIR
        End Get
    End Property
    Public ReadOnly Property Orden() As Long Implements IPAQ.Orden
        Get
            Return 2
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse(_fechaCreacion)
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return _version
        End Get
    End Property
    Public ReadOnly Property DependenciesIDs() As System.Collections.Generic.List(Of Int64) Implements IPAQ.DependenciesIDs
        Get
            Return New Generic.List(Of Int64)
        End Get
    End Property

    Public Overrides Sub Dispose() Implements IDisposable.Dispose

    End Sub
#End Region

#Region "Métodos"
    Public Function execute() As Boolean Implements IPAQ.Execute
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando permisos repetidos")
        If Count("select count(1) from zir group by IndexId,DoctypeId,UserId,RightType having count(1) > 1") > 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontraron permisos repetidos." & vbCrLf & "Creando tabla temporal Tmp_ZIR")

            If Count("select COUNT(1) from sysobjects where name = 'Tmp_ZIR'") <> 0 Then
                If MessageBox.Show("La tabla Tmp_ZIR ya existe. ¿Desea eliminarla para volver a crearla y continuar?", "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    ENonQuery("DROP TABLE Tmp_ZIR")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La ejecución ha sido cancelada")
                    Exit Function
                End If
            End If
            ENonQuery("CREATE TABLE Tmp_ZIR (IndexId numeric(18, 0) NOT NULL,DoctypeId numeric(18, 0) NOT NULL,UserId numeric(18, 0) NOT NULL,RightType numeric(4, 0) NOT NULL,id numeric(18, 0) NOT NULL IDENTITY (1, 1)) ")

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando backup llamado ZIR_BKP")
            If Count("select COUNT(1) from sysobjects where name = 'ZIR_BKP'") <> 0 Then
                If MessageBox.Show("La tabla ZIR_BKP ya existe. ¿Desea eliminarla para volver a crearla y continuar?", "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    ENonQuery("DROP TABLE ZIR_BKP")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La ejecución ha sido cancelada")
                    Exit Function
                End If
            End If
            ENonQuery("CREATE TABLE ZIR_BKP (IndexId numeric(18, 0) NOT NULL,DoctypeId numeric(18, 0) NOT NULL,UserId numeric(18, 0) NOT NULL,RightType numeric(4, 0) NOT NULL,id numeric(18, 0) NOT NULL IDENTITY (1, 1)) ")

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Copiando registros de ZIR a Tmp_ZIR")
            ENonQuery("SET IDENTITY_INSERT Tmp_ZIR OFF")
            ENonQuery("IF EXISTS(SELECT * FROM ZIR) EXEC('INSERT INTO Tmp_ZIR (IndexId, DoctypeId, UserId, RightType) SELECT IndexId, DoctypeId, UserId, RightType FROM ZIR TABLOCKX')")

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Copiando registros de ZIR a ZIR_BKP")
            ENonQuery("SET IDENTITY_INSERT ZIR_BKP OFF")
            ENonQuery("IF EXISTS(SELECT * FROM ZIR) EXEC('INSERT INTO ZIR_BKP (IndexId, DoctypeId, UserId, RightType) SELECT IndexId, DoctypeId, UserId, RightType FROM ZIR TABLOCKX')")

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Eliminando registros repetidos de la tabla Tmp_ZIR")
            ENonQuery("delete from Tmp_ZIR where id not in(select min(Id) from Tmp_ZIR group by IndexId,DoctypeId,UserId,RightType having count(1) > 1 union select min(id) from Tmp_ZIR group by IndexId,DoctypeId,UserId,RightType having count(1) = 1)")

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Eliminando tabla ZIR")
            ENonQuery("DROP TABLE ZIR")

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Eliminando columna temporal ID de Tmp_ZIR")
            ENonQuery("ALTER TABLE Tmp_ZIR DROP COLUMN ID")

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Renombrando tabla Tmp_ZIR a ZIR")
            ENonQuery("EXECUTE sp_rename N'Tmp_ZIR', N'ZIR', 'OBJECT'")

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Proceso ejecutado correctamente")
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontraron permisos repetidos")
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, vbCrLf & "Buscando la existencia de clave primaria en ZIR")
        If Count("select COUNT(1) from sysobjects where xtype = 'pk' and name like '%zir%'") = 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No existe. Creando clave primaria")
            ENonQuery("ALTER TABLE ZIR ADD CONSTRAINT PK_ZIR PRIMARY KEY CLUSTERED(IndexId,DoctypeId,UserId,RightType)")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "OK" & vbCrLf)
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "EXISTE")
        End If

        'Datatable temporal para detectar inconsistencias en los datos
        Dim dt As DataTable = Nothing

        ZTrace.WriteLineIf(ZTrace.IsInfo, vbCrLf & "Buscando la existencia de clave foranea FK_ZIR_DOC_INDEX")
        If Count("select COUNT(1) from sysobjects where xtype = 'f' and name = 'FK_ZIR_DOC_INDEX'") = 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando la clave foranea FK_ZIR_DOC_INDEX")
            dt = EDataTable("select distinct(indexid) from zir where indexid not in (select index_id from doc_index)")
            If dt.Rows.Count > 0 Then
                'Se obtienen los docTypeIds a eliminar
                Dim docTypeIds As String = String.Empty
                For Each docTypeId As DataRow In dt.Rows
                    docTypeIds = docTypeIds & docTypeId.Item(0).ToString & ","
                Next
                docTypeIds = docTypeIds.Remove(docTypeIds.Length - 1, 1)

                If MessageBox.Show("Los siguientes Ids de atributos no existen: " & docTypeIds & vbCrLf & "Presione OK para eliminarlos de la ZIR.", "Datos duplicados", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = DialogResult.OK Then
                    ENonQuery("DELETE ZIR WHERE INDEXID IN (" & docTypeIds & ")")
                Else
                    Exit Function
                End If
            End If

            ENonQuery("ALTER TABLE ZIR ADD CONSTRAINT FK_ZIR_DOC_INDEX FOREIGN KEY(IndexId) REFERENCES DOC_INDEX(INDEX_ID) ON UPDATE NO ACTION ON DELETE NO ACTION")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "OK")
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "EXISTE")
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, vbCrLf & "Buscando la existencia de clave foranea FK_ZIR_DOC_TYPE")
        If Count("select COUNT(1) from sysobjects where xtype = 'f' and name = 'FK_ZIR_DOC_TYPE'") = 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando la clave foranea FK_ZIR_DOC_TYPE")
            dt = EDataTable("select distinct(doctypeid) from zir where doctypeid not in (select doc_type_id from doc_type)")
            If dt.Rows.Count > 0 Then
                'Se obtienen los docTypeIds a eliminar
                Dim docTypeIds As String = String.Empty
                For Each docTypeId As DataRow In dt.Rows
                    docTypeIds = docTypeIds & docTypeId.Item(0).ToString & ","
                Next
                docTypeIds = docTypeIds.Remove(docTypeIds.Length - 1, 1)

                If MessageBox.Show("Los siguientes Ids de entidades no existen: " & docTypeIds & vbCrLf & "Presione OK para eliminarlos de la ZIR.", "Datos duplicados", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = DialogResult.OK Then
                    ENonQuery("DELETE ZIR WHERE DOCTYPEID IN (" & docTypeIds & ")")
                Else
                    Exit Function
                End If
            End If

            ENonQuery("ALTER TABLE ZIR ADD CONSTRAINT FK_ZIR_DOC_TYPE FOREIGN KEY(DoctypeId) REFERENCES DOC_TYPE(DOC_TYPE_ID) ON UPDATE NO ACTION ON DELETE NO ACTION")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "OK")
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "EXISTE")
        End If

        If dt IsNot Nothing Then
            dt.Dispose()
            dt = Nothing
        End If

        Return True
    End Function
#End Region

End Class
