Imports Zamba.Core

''' <summary>
''' Crea los volumenes -1 y 0 en la tabla DISK_VOLUME. Luego recorre los tipos de 
''' documentos y a aquellos que no tienen creada la FK por VOL_ID se les crea.
''' </summary>
''' <history>
''' Tomas   23/05/2011  Created
''' </history>
''' <remarks></remarks>
Public Class PAQ_Normalizar_DoctDiskvolume
    Inherits ZPaq
    Implements IPAQ

#Region "Atributos y propiedades"
    Private Const _name As String = "PAQ_Normalizar_DoctDiskvolume"
    Private Const _description As String = "Crea los volumenes -1 y 0 en la tabla DISK_VOLUME. Luego recorre los entidades y a aquellos que no tienen creada la FK por VOL_ID se les crea."
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
            Return EnumPaquetes.PAQ_Normalizar_DoctDiskvolume
        End Get
    End Property
    Public ReadOnly Property Orden() As Long Implements IPAQ.Orden
        Get
            Return 1
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

#End Region

#Region "Métodos"
    Public Function execute() As Boolean Implements IPAQ.Execute
        Dim dt As DataTable = Nothing
        Dim existe As Int32
        Dim docTypeId As String

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando la existencia del volumen -1 en DISK_VOLUME")
        existe = Count("SELECT COUNT(1) FROM DISK_VOLUME WHERE DISK_VOL_ID = -1")
        If existe = 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "INSERTANDO")
            ENonQuery("INSERT INTO [DISK_VOLUME] ([DISK_VOL_ID],[DISK_VOL_NAME],[DISK_VOL_SIZE],[DISK_VOL_TYPE],[DISK_VOL_COPY],[DISK_VOL_PATH],[DISK_VOL_SIZE_LEN],[DISK_VOL_STATE],[DISK_VOL_LSTOFFSET],[DISK_VOL_FILES]) VALUES (-1,'Virtual',0,1,0,'',0,0,0,0)")
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "OK" & vbCrLf)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando la existencia del volumen 0 en DISK_VOLUME")
        existe = Count("SELECT COUNT(1) FROM DISK_VOLUME WHERE DISK_VOL_ID = 0")
        If existe = 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "INSERTANDO")
            ENonQuery("INSERT INTO [DISK_VOLUME] ([DISK_VOL_ID],[DISK_VOL_NAME],[DISK_VOL_SIZE],[DISK_VOL_TYPE],[DISK_VOL_COPY],[DISK_VOL_PATH],[DISK_VOL_SIZE_LEN],[DISK_VOL_STATE],[DISK_VOL_LSTOFFSET],[DISK_VOL_FILES]) VALUES (0,'Virtual2',0,1,0,'',0,0,0,0)")
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "OK" & vbCrLf)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo Entidades" & vbCrLf)
        dt = EDataTable("SELECT DOC_TYPE_ID FROM DOC_TYPE")
        For Each dr As DataRow In dt.Rows
            docTypeId = dr.Item(0).ToString
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando la existencia de la FK DOC_T" & docTypeId & " en DISK_VOLUME")
            existe = Count("select count(1) from sysobjects where xtype = 'F' and name like '%doc_t" & docTypeId & "%disk_volume%'")
            If existe = 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "NO EXISTE. CREANDO CLAVE FORANEA")
                ENonQuery("ALTER TABLE DOC_T" & docTypeId & " ADD CONSTRAINT FK_DOC_T" & docTypeId & "_DISK_VOLUME FOREIGN KEY(VOL_ID) REFERENCES DISK_VOLUME (DISK_VOL_ID) ON UPDATE NO ACTION ON DELETE NO ACTION")
                ZTrace.WriteLineIf(ZTrace.IsInfo, "OK" & vbCrLf)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "EXISTE" & vbCrLf)
            End If
        Next

        If dt IsNot Nothing Then
            dt.Dispose()
            dt = Nothing
        End If

        Return True
    End Function
#End Region
    Public Overrides Sub Dispose() Implements IDisposable.Dispose

    End Sub

End Class
