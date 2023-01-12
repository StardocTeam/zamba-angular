Imports Zamba.Core
''' <summary>
''' Agrega todas las PK en las DOC_T que haga falta. Agrega las FK que haga falta sobre las DOC_T relacionadas con la DOC_i y la DOC_B.
''' </summary>
''' <history>
''' Tomas   23/09/2011  Created
''' </history>
''' <remarks></remarks>
Public Class PAQ_AgregarClavesDOC
    Inherits ZPaq
    Implements IPAQ



#Region "Atributos y propiedades"
    Private Const _name As String = "PAQ_AgregarClavesDOC"
    Private Const _description As String = "Agrega todas las PK en las DOC_T que haga falta. Agrega las FK que haga falta sobre las DOC_T relacionadas con la DOC_i y la DOC_B."
    Private Const _version As String = "1"
    Private Const fechaCreacion As String = "23/09/2011"
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

    Public Overrides Sub Dispose() Implements IDisposable.Dispose

    End Sub
#End Region

#Region "Métodos"
    Public Function execute() As Boolean Implements IPAQ.Execute
        ZTrace.WriteLineIf(ZTrace.IsInfo, "////////////////////////////////////////////////////////////////////////////////" & vbCrLf)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando los tipos de documento" & vbCrLf)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "////////////////////////////////////////////////////////////////////////////////" & vbCrLf)

        Dim dtDocTypeIds As DataTable = EDataTable("SELECT DOC_TYPE_ID FROM DOC_TYPE")
        Dim doct, doci, docb As String
        Dim resultado As Boolean

        For Each row As DataRow In dtDocTypeIds.Rows
            doct = "DOC_T" & row(0).ToString
            doci = "DOC_I" & row(0).ToString
            docb = "DOC_B" & row(0).ToString

            If Servers.Server.isOracle Then
                Try
                    Try
                        If Count("select count(1) from all_objects where OBJECT_TYPE ='INDEX' and OBJECT_NAME = '" & doct & "_PK'") = 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando la PK en la tabla " & doct)
                            ENonQuery("ALTER TABLE " & doct & " ADD CONSTRAINT " & doct & "_PK PRIMARY KEY (DOC_ID)")
                            RaiseInfo("Agregando PK a " & doct)
                        End If
                    Catch ex As Exception
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message)
                        raiseerror(ex)
                        ZClass.RaiseNotifyError(ex.Message)
                    End Try
                    Try

                        If Count("select count(1) from all_objects where OBJECT_TYPE ='INDEX' and OBJECT_NAME = '" & doci & "_FK'") = 0 = 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando la FK en la tabla " & doci)
                            ENonQuery("ALTER TABLE " & doci & " ADD CONSTRAINT " & doci & "_FK FOREIGN KEY (DOC_ID) REFERENCES  " & doct & "(DOC_ID)")
                            RaiseInfo("Agregando FK a " & doci)
                        End If
                    Catch ex As Exception
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message)
                        raiseerror(ex)
                        ZClass.RaiseNotifyError(ex.Message)
                    End Try
                    Try

                        If Count("select count(1) from all_objects where OBJECT_TYPE ='INDEX' and OBJECT_NAME = '" & docb & "_FK'") = 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando la FK en la tabla " & docb)
                            ENonQuery("ALTER TABLE " & docb & " ADD CONSTRAINT " & docb & "_FK FOREIGN KEY (DOC_ID) REFERENCES  " & doct & "(DOC_ID)")
                            RaiseInfo("Agregando FK a " & docb)
                        End If
                    Catch ex As Exception
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message)
                        raiseerror(ex)
                        ZClass.RaiseNotifyError(ex.Message)
                    End Try


                    resultado = True
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message)
                    raiseerror(ex)
                    ZClass.RaiseNotifyError(ex.Message)
                End Try
            Else
                Try
                    If Count("select count(1) from sysobjects where xtype='PK' and parent_obj in (select id from sysobjects where name='" & doct & "')") = 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando la PK en la tabla " & doct)
                        ENonQuery("ALTER TABLE " & doct & " ADD PRIMARY KEY(DOC_ID)")
                    End If

                    If Count("select count(1) from sysobjects o inner join sysforeignkeys f on f.constid = o.id where f.fkey=4 and o.parent_obj in (select id from sysobjects where name='" & doci & "')") = 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando la FK en la tabla " & doci)
                        ENonQuery("ALTER TABLE " & doci & " ADD FOREIGN KEY (DOC_ID) REFERENCES " & doct & "(DOC_ID)ON DELETE CASCADE")
                    End If

                    If Count("select count(1) FROM sysobjects where xtype='F' and parent_obj in (select id from sysobjects where name='" & docb & "')") = 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando la FK en la tabla " & docb)
                        ENonQuery("ALTER TABLE " & docb & " ADD FOREIGN KEY (DOC_ID) REFERENCES " & doct & "(DOC_ID)ON DELETE CASCADE")
                    End If

                    resultado = True
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message)
                    raiseerror(ex)
                End Try
            End If
        Next

        If resultado Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Proceso ejecutado con éxito")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "////////////////////////////////////////////////////////////////////////////////" & vbCrLf)
            Return True
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Proceso ejecutado con ERRORES")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "////////////////////////////////////////////////////////////////////////////////" & vbCrLf)
            Return False
        End If

    End Function
#End Region

End Class
