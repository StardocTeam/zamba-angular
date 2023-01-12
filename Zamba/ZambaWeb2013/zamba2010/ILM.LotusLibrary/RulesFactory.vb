Namespace business
    Public Class Login

        Dim _Usuario As String
        Dim _Contraseña As String
        Dim _UserId As Integer

        Property UserId() As Integer
            Get
                Return _UserId
            End Get
            Set(ByVal Value As Integer)
                _UserId = Value
            End Set
        End Property

        Property Usuario() As String
            Get
                Return _Usuario
            End Get
            Set(ByVal Value As String)
                _Usuario = Value
            End Set
        End Property

        Property Contraseña() As String
            Get
                Return _Contraseña
            End Get
            Set(ByVal Value As String)
                _Contraseña = Value
            End Set
        End Property

        Public Function ValidarUserPass(ByVal User As String, ByVal Pass As String) As Boolean
            Dim Data As New DATA
            Return Data.ValidarUser(User, Pass)
        End Function

    End Class
    Public Class Save
        Public Sub SaveShownIndex(ByVal dtid As Int32, ByVal indexid As Int32, ByVal Mustcomplete As Boolean)
            Dim Data As New DATA
            Data.SaveShownIndexs(dtid, indexid, Mustcomplete)
            ' Data.Dispose()
        End Sub
    End Class

    Public Class Registro

        Dim _UserId As Integer
        Dim _CONF_NOMUSERRED As String
        Dim _CONF_NOMUSERNOTES As String
        'Dim _DireccionCorreoNotes As String
        Dim _CONF_PATHREMOTOARCH As String
        Dim _CONF_PATHLOCAL As String
        Dim _CONF_MAILSERVER As String
        Dim _CONF_BASEMAIL As String
        Dim _CONF_REINTENTO As Integer
        Dim _CONF_NOMARCHTXT As String
        Dim _CONF_CHARSREEMPSUBJ As String
        Dim _LISTADOFORMS As New ArrayList
        Dim _CONF_EJECUTABLE As String
        Dim _CONF_NOTESCTRL As String
        Dim _NOMBRE As String
        Dim _CONF_PATHARCH As String
        Dim _CONF_VISTAEXPORTACION As String
        Dim _CONF_PAPELERA As String
        Dim _CONF_SEQMSG As Integer
        Dim _CONF_SEQATT As Integer
        Dim _CONF_LOCKEO As Integer
        Dim _CONF_ACUMIMG As Integer
        Dim _CONF_LIMIMG As Integer
        Dim _CONF_DESTEXT As Integer
        Dim _CONF_TEXTOSUBJECT As String
        Dim _CONF_BORRAR As String
        Dim _CONF_ARCHCTRL As String
        Dim _CONF_SCHEDULEVAR As String
        Dim _CONF_SCHEDULESEL As String
        Dim _ACTIVO As Integer
        Dim _CONF_SEQIMG As Integer



        Public Property ACTIVO() As Integer
            Get
                Return _ACTIVO
            End Get
            Set(ByVal value As Integer)
                _ACTIVO = Value
            End Set
        End Property
        Public Property CONF_ACUMIMG() As Integer
            Get
                Return _CONF_ACUMIMG
            End Get
            Set(ByVal value As Integer)
                _CONF_ACUMIMG = value
            End Set
        End Property
        Public Property CONF_ARCHCTRL() As String
            Get
                Return _CONF_ARCHCTRL
            End Get
            Set(ByVal value As String)
                _CONF_ARCHCTRL = Value
            End Set
        End Property
        Public Property CONF_BORRAR() As String
            Get
                Return _CONF_BORRAR
            End Get
            Set(ByVal value As String)
                _CONF_BORRAR = Value
            End Set
        End Property
        Public Property CONF_DESTEXT() As Integer
            Get
                Return _CONF_DESTEXT
            End Get
            Set(ByVal value As Integer)
                _CONF_DESTEXT = Value
            End Set
        End Property
        Public Property CONF_LIMIMG() As Integer
            Get
                Return _CONF_LIMIMG
            End Get
            Set(ByVal value As Integer)
                _CONF_LIMIMG = Value
            End Set
        End Property
        Public Property CONF_LOCKEO() As Integer
            Get
                Return _CONF_LOCKEO
            End Get
            Set(ByVal value As Integer)
                _CONF_LOCKEO = Value
            End Set
        End Property
        Public Property CONF_NOTESCTRL() As String
            Get
                Return Me._CONF_NOTESCTRL
            End Get
            Set(ByVal Value As String)
                Me._CONF_NOTESCTRL = Value
            End Set
        End Property
        Public Property CONF_NOTESCTRL1() As String
            Get
                Return _CONF_NOTESCTRL
            End Get
            Set(ByVal value As String)
                _CONF_NOTESCTRL = value
            End Set
        End Property
        Public Property CONF_PAPELERA() As String
            Get
                Return _CONF_PAPELERA
            End Get
            Set(ByVal value As String)
                _CONF_PAPELERA = Value
            End Set
        End Property
        Public Property CONF_PATHARCH() As String
            Get
                Return _CONF_PATHARCH
            End Get
            Set(ByVal value As String)
                _CONF_PATHARCH = Value
            End Set
        End Property
        Property CONF_PATHLOCAL() As String
            Get
                Return _CONF_PATHLOCAL
            End Get
            Set(ByVal Value As String)
                _CONF_PATHLOCAL = Value
            End Set
        End Property
        Public Property CONF_SCHEDULESEL() As String
            Get
                Return _CONF_SCHEDULESEL
            End Get
            Set(ByVal value As String)
                _CONF_SCHEDULESEL = value
            End Set
        End Property
        Public Property CONF_SCHEDULEVAR() As String
            Get
                Return _CONF_SCHEDULEVAR
            End Get
            Set(ByVal value As String)
                _CONF_SCHEDULEVAR = value
            End Set
        End Property
        Public Property CONF_SEQATT() As Integer
            Get
                Return _CONF_SEQATT
            End Get
            Set(ByVal value As Integer)
                _CONF_SEQATT = value
            End Set
        End Property
        Public Property CONF_SEQIMG() As Integer
            Get
                Return _CONF_SEQIMG
            End Get
            Set(ByVal value As Integer)
                _CONF_SEQIMG = Value
            End Set
        End Property
        Public Property CONF_SEQMSG() As Integer
            Get
                Return _CONF_SEQMSG
            End Get
            Set(ByVal value As Integer)
                _CONF_SEQMSG = value
            End Set
        End Property
        Public Property CONF_TEXTOSUBJECT() As String
            Get
                Return _CONF_TEXTOSUBJECT
            End Get
            Set(ByVal value As String)
                _CONF_TEXTOSUBJECT = Value
            End Set
        End Property
        Public Property CONF_VISTAEXPORTACION() As String
            Get
                Return _CONF_VISTAEXPORTACION
            End Get
            Set(ByVal value As String)
                _CONF_VISTAEXPORTACION = Value
            End Set
        End Property
        Public Property NOMBRE() As String
            Get
                Return _NOMBRE
            End Get
            Set(ByVal value As String)
                _NOMBRE = Value
            End Set
        End Property
        Property UserId() As Integer
            Get
                Return _UserId
            End Get
            Set(ByVal Value As Integer)
                _UserId = Value
            End Set
        End Property
        Property CONF_NOMUSERRED() As String
            Get
                Return _CONF_NOMUSERRED
            End Get
            Set(ByVal Value As String)
                _CONF_NOMUSERRED = Value
            End Set
        End Property
        Property CONF_NOMUSERNOTES() As String
            Get
                Return _CONF_NOMUSERNOTES
            End Get
            Set(ByVal Value As String)
                _CONF_NOMUSERNOTES = Value
            End Set
        End Property

        Property CONF_PATHREMOTOARCH() As String
            Get
                Return _CONF_PATHREMOTOARCH
            End Get
            Set(ByVal Value As String)
                _CONF_PATHREMOTOARCH = Value
            End Set
        End Property
        Property CONF_MAILSERVER() As String
            Get
                Return _CONF_MAILSERVER
            End Get
            Set(ByVal Value As String)
                _CONF_MAILSERVER = Value
            End Set
        End Property
        Property CONF_BASEMAIL() As String
            Get
                Return _CONF_BASEMAIL
            End Get
            Set(ByVal Value As String)
                _CONF_BASEMAIL = Value
            End Set
        End Property
        Property CONF_REINTENTO() As Integer
            Get
                Return _CONF_REINTENTO
            End Get
            Set(ByVal Value As Integer)
                _CONF_REINTENTO = Value
            End Set
        End Property
        Property CONF_NOMARCHTXT() As String
            Get
                Return _CONF_NOMARCHTXT
            End Get
            Set(ByVal Value As String)
                _CONF_NOMARCHTXT = Value
            End Set
        End Property
        Property CONF_CHARSREEMPSUBJ() As String
            Get
                Return _CONF_CHARSREEMPSUBJ
            End Get
            Set(ByVal Value As String)
                _CONF_CHARSREEMPSUBJ = Value
            End Set
        End Property
        Property ListadoForms() As ArrayList
            Get
                Return _LISTADOFORMS
            End Get
            Set(ByVal Value As ArrayList)
                _LISTADOFORMS = Value
            End Set
        End Property
        Property CONF_EJECUTABLE() As String
            Get
                Return _CONF_EJECUTABLE
            End Get
            Set(ByVal Value As String)
                _CONF_EJECUTABLE = Value
            End Set
        End Property

        Public Sub Validar(ByVal reg As Registro)
            If reg.CONF_NOMARCHTXT = String.Empty Then Throw New Exception("El nombre no puede ser vacio")
            If reg.CONF_BASEMAIL = String.Empty Then Throw New Exception("El path de la base de mail no puede ser vacio")
            If reg.CONF_EJECUTABLE = String.Empty Then Throw New Exception("El path del ejecutable no puede ser vacio")
            ' Esto se comento porque no era necesario que haya valor en dicha columa
            If reg.CONF_PATHREMOTOARCH = String.Empty And reg.CONF_PATHLOCAL = String.Empty Then
                Throw New Exception("Debe completar el path remoto para txt, attahments e imagenes o el path local")
            ElseIf reg.CONF_PATHREMOTOARCH = reg.CONF_PATHLOCAL Then
                Throw New Exception("El path local no puede ser igual al path remoto para txt, attachments e imagenes")
            End If

            If reg.CONF_MAILSERVER = String.Empty Then Throw New Exception("El servidor de no puede ser vacio")
            If reg.CONF_NOMUSERNOTES = String.Empty Then Throw New Exception("El usuario de notes no puede ser vacio")
            If reg.CONF_NOMUSERRED = String.Empty Then Throw New Exception("El usuario de red no puede ser vacio")
            If Val(reg.CONF_REINTENTO) = 0 Then Throw New Exception("La cantidad de intentos no puede ser vacio")
            If reg.CONF_CHARSREEMPSUBJ = String.Empty Then Throw New Exception("Los caracteres a reemplazar no pueden ser vacios")
            'If reg.DireccionCorreoNotes = String.Empty Then Throw New Exception("La dirección de correo de notes no puede ser vacio")
        End Sub
        Public Sub Guardar(ByVal reg As Registro, ByVal Accion As String)
            Try
                'Validar(reg)

                Dim Guardar As New DATA
            
                Guardar.GuardarDatos(reg, Accion)

                'Guardar.GuardarListadoForms(Array2, reg.UserId, Accion)



            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function CargarDatos(ByVal UserId As String) As Registro

            Dim DataAccess As New DATA
            Dim Ds As New DataSet
            Dim Ds2 As New DataSet
            Dim reg As New Registro

            If UserId = 0 Then Return reg
            Ds = DataAccess.CargarDatos(UserId)
            Ds2 = DataAccess.CargarListadoForms(UserId)

            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(23)) Then reg.CONF_NOMUSERRED = Ds.Tables(0).Rows(0).Item(23)
            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(22)) Then reg.CONF_NOMUSERNOTES = Ds.Tables(0).Rows(0).Item(22)
            'If  Not IsDBNull(Ds.Tables(0).Rows(0).Item(0)) Then reg.DireccionCorreoNotes = Ds.Tables(0).Rows(0).Item(XXXX)
            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(2)) Then reg.CONF_PATHREMOTOARCH = Ds.Tables(0).Rows(0).Item(2)
            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(5)) Then reg.CONF_PATHLOCAL = Ds.Tables(0).Rows(0).Item(5)
            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(3)) Then reg.CONF_MAILSERVER = Ds.Tables(0).Rows(0).Item(3)
            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(4)) Then reg.CONF_BASEMAIL = Ds.Tables(0).Rows(0).Item(4)
            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(26)) Then reg.CONF_REINTENTO = Ds.Tables(0).Rows(0).Item(26)
            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(8)) Then reg.CONF_NOMARCHTXT = Ds.Tables(0).Rows(0).Item(8)
            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(24)) Then reg.CONF_CHARSREEMPSUBJ = Ds.Tables(0).Rows(0).Item(24)

            Dim i As Integer
            For i = 0 To Ds2.Tables(0).Rows.Count - 1
                reg.ListadoForms.Add(Ds2.Tables(0).Rows(i).Item(0))
            Next

            If Not IsDBNull(Ds.Tables(0).Rows(0).Item(21)) Then reg.CONF_EJECUTABLE = Ds.Tables(0).Rows(0).Item(21)
            reg.UserId = Ds.Tables(0).Rows(0).Item(0)

            Return reg
        End Function
        Public Function CargarTabla() As DataSet
            Dim DataAccess As New DATA
            Return DataAccess.CargarTabla
        End Function
        Public Function ValidarUsuarioZamba(ByVal Usuario As String) As Integer
            Dim DataAccess As New DATA
            Return DataAccess.ValidarUsuarioZamba(Usuario)
        End Function

        Public Function VerificarExistencia(ByVal usrid As Int32) As Boolean
            Dim DataAccess As New DATA
            Return DataAccess.VerificarExistencia(usrid)
        End Function

        Public Function GetFormsAsigned(ByVal usrid As Int32) As ArrayList
            Dim ds As DataSet = DATA.ObtenerForms(usrid)
            Dim Vec As ArrayList = New ArrayList
            Dim i As Int16
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Vec.Add(ds.Tables(0).Rows(i).Item(0))
            Next
            Return Vec
        End Function
        Public Function BuscarId(ByVal Usuario As String) As Integer
            Dim DataAccess As New DATA
            Return DataAccess.GetUserID(Usuario)
        End Function
        Public Function ActualizarCheckBox()
            Dim actualizar As New DATA
            Return actualizar.actualizar()
        End Function

        Public Function Verificar()
            Dim ds As DataSet

        End Function


        Public Function Eliminar(ByVal Id As Integer) As Integer
            Dim DataAccess As New DATA
            Return DataAccess.EliminarItem(Id)
        End Function

    End Class
End Namespace