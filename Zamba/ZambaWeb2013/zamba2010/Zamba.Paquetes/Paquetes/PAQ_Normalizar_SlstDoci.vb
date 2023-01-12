Imports Zamba.Core
''' <summary>
''' Se verifica que las slst tengan todos los items completos, o sea se busca en las doc_i que todos los valores existan
''' en las slst. De no serlo se procede a completarlos e insertarlos en las slst. Al finalizar con todos los datos completos 
''' se generan las FK de DOC_I.I a SLST.CODIGO.
''' </summary>
''' <history>
''' Tomas   23/05/2011  Created
''' </history>
''' <remarks></remarks>
Public Class PAQ_Normalizar_SlstDoci
    Inherits ZPaq
    Implements IPAQ

#Region "Atributos y propiedades"
    Private Const _name As String = "PAQ_Normalizar_SlstDoci"
    Private Const _description As String = "Se verifica que las slst tengan todos los items completos, o sea se busca en las doc_i que todos los valores existan en las slst. De no serlo se procede a completarlos e insertarlos en las slst. Al finalizar con todos los datos completos  se generan las FK de DOC_I.I a SLST.CODIGO."
    Private Const _version As String = "1"
    Private Const fechaCreacion As String = "23/05/2011"
    Private _installed As Boolean

    Private c As New Hashtable
    Private hsDociId As New Hashtable
    Private hsSlstId As New Hashtable

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
            Return EnumPaquetes.PAQ_Normalizar_DoctDiskvolume
        End Get
    End Property
    Public ReadOnly Property Orden() As Long Implements IPAQ.Orden
        Get
            Return 3
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse(fechaCreacion)
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

#End Region

#Region "Métodos"
    Public Function execute() As Boolean Implements IPAQ.Execute
        Dim dtDocsAndSlst As DataTable
        Dim dtTemp As DataTable
        Dim lstSlstViews As New Generic.List(Of String)
        Dim lstSlstTables As New Generic.List(Of String)
        Dim lstValuesToFix As New Generic.List(Of String)
        Dim slstId, docTypeId, tempIndexs, codigo, name, xprec, xscale, length, colDefinition, inputMsg As String
        'Dim msgError As String = "Ocurrio un error en el proceso. Para más información, verifique los errores."
        Dim separador As Char() = {Char.Parse("|")}

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando los documentos que contienen atributos de tipo SLST")
        dtDocsAndSlst = EDataTable("SELECT r.doc_type_id,r.index_id,i.index_name FROM index_r_doc_type r INNER JOIN doc_index i ON r.index_id=i.index_id where i.dropdown=2 ORDER BY r.doc_type_id,r.index_id")
        dtTemp = EDataTable("SELECT distinct(r.index_id) FROM index_r_doc_type r INNER JOIN doc_index i ON r.index_id=i.index_id where i.dropdown=2")

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando si las SLST son vistas o tablas")
        For Each slst As DataRow In dtTemp.Rows
            slstId = slst.Item(0).ToString
            If Count("select count(1) from sysobjects where name='SLST_S" & slstId & "' and xtype='v'") = 1 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El objeto SLST_S" & slstId & " es una VISTA")
                lstSlstViews.Add(slstId)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El objeto SLST_S" & slstId & " es una TABLA")
                lstSlstTables.Add(slstId)
            End If
        Next

        For Each row As DataRow In dtDocsAndSlst.Rows
            slstId = row.Item(1).ToString
            docTypeId = row.Item(0).ToString

            Try
                'Verifica si debe realizar la verificacion de tipo de dato
                If lstSlstTables.Contains(slstId) Then
                    'Verifica si el tipo de datos de la slst es INT. De ser asi debe ser corregido ya que Zamba no trabaja mas con ese tipo de datos.
                    If Count("select count(1) from syscolumns where name='CODIGO' and id=" & GetSlstObjectId(slstId) & " and xtype=56") = 1 Then
                        'Encuentra la definición de la columna del Atributo en la DOC_I (la definición real)
                        dtTemp = EDataTable("select t.name,c.xprec,c.xscale,c.length from syscolumns c inner join systypes t on t.xtype=c.xtype where c.id=" & GetDociObjectId(docTypeId) & " and c.name='I" & slstId & "'")

                        name = dtTemp.Rows(0).Item("name").ToString

                        'Si el tipo de dato es INT se modifica también la doc_i
                        If String.Compare(name, "int") <> 0 Then

                            If name.Contains("char") Then
                                length = dtTemp.Rows(0).Item("length").ToString
                                colDefinition = name & "(" & length & ")"
                            Else
                                xprec = dtTemp.Rows(0).Item("xprec").ToString
                                xscale = dtTemp.Rows(0).Item("xscale").ToString
                                colDefinition = name & "(" & xprec & "," & xscale & ")"
                            End If

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se crea la tabla temporal Tmp_SLST_S" & slstId)
                            ENonQuery("CREATE TABLE Tmp_SLST_S" & slstId & "(CODIGO " & colDefinition & " NOT NULL,Descripcion varchar(100) NULL) ")

                            If Count("select COUNT(1) from sysobjects where name = 'BKP_SLST_S" & slstId & "'") <> 0 Then
                                If MessageBox.Show("La tabla de backup ya existe. ¿Desea eliminarla para volver a crearla y continuar?", "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                    ENonQuery("DROP TABLE BKP_SLST_S" & slstId)
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La ejecución ha sido cancelada")
                                    Exit Function
                                End If
                            End If
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando backup de la slst")
                            ENonQuery("SELECT * INTO BKP_SLST_S" & slstId & " FROM SLST_S" & slstId)

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se pasan los datos")
                            ENonQuery("IF EXISTS(SELECT * FROM SLST_S" & slstId & ") EXEC('INSERT INTO Tmp_SLST_S" & slstId & " (Codigo, Descripcion) SELECT CONVERT(" & colDefinition & ", Codigo), CONVERT(varchar(100), Descripcion) FROM SLST_S" & slstId & " TABLOCKX')")

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Eliminando la SLST_S" & slstId)
                            ENonQuery("DROP TABLE SLST_S" & slstId)

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Renombrando la temporal a SLST_S" & slstId)
                            ENonQuery("EXECUTE sp_rename N'Tmp_SLST_S" & slstId & "', N'SLST_S" & slstId & "', 'OBJECT'")
                        End If
                    End If
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando valores de DOC_I" & docTypeId & ".I" & slstId & " que no existan en SLST")
                dtTemp = EDataTable("SELECT DISTINCT(I" & slstId & ") FROM DOC_I" & docTypeId & " WHERE I" & slstId & " IS NOT NULL AND I" & slstId & " NOT IN (SELECT CODIGO FROM SLST_S" & slstId & ")")

                If dtTemp.Rows.Count > 0 Then
                    'Verifica si el Atributo es de tipo vista, ya que si lo es no será posible corregirlo de momento.
                    If lstSlstViews.Contains(slstId) Then

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Existen valores incorrectos, pero al ser una vista no es posible corregirlos automáticamente. Verifique el reporte final para mayor información.")
                        tempIndexs = String.Empty
                        For Each r As DataRow In dtTemp.Rows
                            tempIndexs = tempIndexs & "," & r.Item(0).ToString
                        Next
                        tempIndexs = tempIndexs.Remove(0, 1)

                        'Se guarda en una lista el documento, el atributo y los valores erroneos separados por coma.
                        'Al finalizar se leen estos registros para informar que datos se deben corregir para agregar las FK.
                        lstValuesToFix.Add(docTypeId & "|" & slstId & "|" & tempIndexs)

                    Else

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Existen valores incorrectos. Se procederá a completar las descripciones faltantes")
                        For Each r As DataRow In dtTemp.Rows
                            codigo = r.Item(0).ToString

                            'Si codigo es igual a String.Empty, quiere decir que en la DOC_I dicho Atributo es de tipo
                            'varchar y que se encuentra VACIO, o sea ''. 
                            If String.IsNullOrEmpty(codigo) Then
                                inputMsg = "Complete la descripción para el Atributo '" & row.Item(2).ToString.Trim & " (" & slstId & ")' cuyo código se encuentra VACIO"
                                codigo = "''"
                            Else
                                inputMsg = "Complete la descripción para el Atributo '" & row.Item(2).ToString.Trim & " (" & slstId & ")' cuyo código es: " & codigo
                            End If
                            tempIndexs = InputBox(inputMsg, "Valor de Atributo incompleto")
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando la descripción '" & tempIndexs & "' al código " & codigo & " a la tabla SLST_S" & slstId)
                            ENonQuery("INSERT INTO SLST_S" & slstId & " VALUES(" & codigo & ",'" & tempIndexs & "')")
                        Next

                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Todos los valores se encontraban correctos")
                End If

                'If lstSlstTables.Contains(slstId) Then :  ????
                If Not lstSlstViews.Contains(slstId) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de clave primaria en la columna CODIGO de la SLST_S" & slstId)
                    If Count("select count(1) from sysobjects where xtype='PK' and parent_obj in (select id from sysobjects where name = 'SLST_S" & slstId & "')") = 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "NO EXISTE. Creando...")
                        ENonQuery("ALTER TABLE SLST_S" & slstId & " ADD PRIMARY KEY(CODIGO)")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "YA EXISTE")
                    End If

                    codigo = "FK_DOC_I" & docTypeId & "_SLST_S" & slstId
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de la FK entre la DOC_I" & docTypeId & ".I" & slstId & " y la SLST_S" & slstId & ".CODIGO")
                    If Count("select count(1) from sysobjects where xtype='f' and name='" & codigo & "'") = 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "NO EXISTE. Creando...")
                        ENonQuery("ALTER TABLE DOC_I" & docTypeId & " ADD CONSTRAINT " & codigo & " FOREIGN KEY(I" & slstId & ") REFERENCES SLST_S" & slstId & "(CODIGO) ON UPDATE NO ACTION ON DELETE NO ACTION")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "OK")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "YA EXISTE")
                    End If
                End If
            Catch ex As Exception
                raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsInfo, vbCrLf & "ERROR INESPERADO. VERIFIQUE LOS ERRORES AL FINALIZAR LA EJECUCIÓN.")
                lstValuesToFix.Add(docTypeId & "|" & slstId & "|" & ex.Message)
            End Try
        Next

        ZTrace.WriteLineIf(ZTrace.IsInfo, vbCrLf & "Proceso finalizado." & vbCrLf)
        If lstValuesToFix.Count > 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "-----------------------------------------------------------------------------")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Códigos faltantes en vistas SLST que se encuentran en atributos de tablas DOC_I")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "-----------------------------------------------------------------------------")

            codigo = String.Empty
            For Each doc As String In lstSlstTables
                codigo = doc & ", " & codigo
            Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "SLST de tipo Tabla: " & codigo)

            codigo = String.Empty
            For Each doc As String In lstSlstViews
                codigo = doc & ", " & codigo
            Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "SLST de tipo Vista: " & codigo)

            ZTrace.WriteLineIf(ZTrace.IsInfo, vbCrLf)
            codigo = String.Empty
            lstSlstViews.Clear()
            For Each toFix As String In lstValuesToFix
                docTypeId = toFix.Split(separador)(0)

                'Escribe el titulo de la entidad
                If docTypeId <> codigo Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, vbCrLf & vbCrLf & "DOC_I" & docTypeId)
                    codigo = docTypeId
                End If
                slstId = toFix.Split(separador)(1)
                tempIndexs = toFix.Split(separador)(2)

                'Escribe el Atributo con sus datos faltantes
                ZTrace.WriteLineIf(ZTrace.IsInfo, Chr(9) & "I" & slstId & ":  " & tempIndexs)

                'If tempIndexs <> msgError Then
                '    lstSlstViews.Add("UPDATE DOC_I" & docTypeId & " SET I" & slstId & "=NULL WHERE I" & slstId & " IN(" & tempIndexs & ")" & vbCrLf & "GO")
                'End If
            Next

            'ZTrace.WriteLineIf(ZTrace.IsInfo,vbCrLf & "Dado que son vistas, es imposible crearles claves foráneas, pero si es posible hacerlo con la tabla original a la que la vista apunta.")
            'ZTrace.WriteLineIf(ZTrace.IsInfo,"Para ello primero se debería agregar los códigos faltantes a las tablas originales y luego agregar la FK.")
            'ZTrace.WriteLineIf(ZTrace.IsInfo,"Otra opción es igualando a NULL los códigos de las DOC_I inexistentes.")
            'ZTrace.WriteLineIf(ZTrace.IsInfo,"A continuación se detallarán, si se encuentran disponibles, las consultas SQL para igualar los códigos inexistentes a NULL, EN CASO DE QUERER HACERLO." & vbCrLf)
            'For Each query As String In lstSlstViews
            '    ZTrace.WriteLineIf(ZTrace.IsInfo,query)
            'Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "-------------------------------------------------------------")
        End If

        If dtDocsAndSlst IsNot Nothing Then
            dtDocsAndSlst.Dispose()
            dtDocsAndSlst = Nothing
        End If
        If dtTemp IsNot Nothing Then
            dtTemp.Dispose()
            dtTemp = Nothing
        End If

        If lstValuesToFix.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function GetDociObjectId(ByVal id As String) As String
        If Not hsDociId.ContainsKey(id) Then
            Dim objectid As String = EScalar("select id from sysobjects where name='DOC_I" & id & "'").ToString
            hsDociId.Add(id, objectid)
            objectid = Nothing
        End If
        Return hsDociId(id).ToString
    End Function

    Private Function GetSlstObjectId(ByVal id As String) As String
        If Not hsSlstId.ContainsKey(id) Then
            Dim objectid As String = EScalar("select id from sysobjects where name='SLST_S" & id & "'").ToString
            hsSlstId.Add(id, objectid)
            objectid = Nothing
        End If
        Return hsSlstId(id).ToString
    End Function
#End Region

    Public Overrides Sub Dispose() Implements IDisposable.Dispose

    End Sub
End Class
