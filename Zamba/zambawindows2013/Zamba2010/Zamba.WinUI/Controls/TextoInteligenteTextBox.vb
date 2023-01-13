Imports System.Drawing
Imports System.Reflection
Imports Microsoft.VisualBasic.Information
Imports System.Diagnostics
Imports System.Text.RegularExpressions
Imports Zamba.Core

Public Class TextoInteligenteTextBox
    Inherits System.Windows.Forms.RichTextBox

#Region " Código generado por el Diseñador de Windows Forms "



    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                'components.Dispose()
            End If
        End If
        'MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'TextoInteligenteTextBox
        '
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim MiObjeto As Object
    Dim FirstTime As Boolean = True
    Dim OldPosition As System.Int32
    Dim WithEvents cboOpcionesTextoInteligente As cmbTextoInteligente
    Dim FlagEscape As Boolean = False

    Private Sub TextoInteligente_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try
            If FlagEscape = False AndAlso TextLength > 1 AndAlso SelectionStart > 0 AndAlso String.Compare(Text.Substring(SelectionStart - 2, 2), "<<") = 0 Then
                ShowCmb()
                ModificarColores()
            Else
                FlagEscape = False
            End If
        Catch ex As Exception
        End Try
    End Sub


    ''' <summary>
    ''' Agrega una propiedad al combo
    ''' </summary>
    ''' <param name="Item"></param>
    ''' <remarks></remarks>
    Private Sub AgregarItem(ByVal Item As PropertyInfo)
        If Not IsNothing(cboOpcionesTextoInteligente) Then

            Dim Description As String = String.Empty
            If Item.GetCustomAttributes(True).Length > 1 Then
                Description &= "("
                For Each p As String In Item.GetCustomAttributes(True)(2).Propiedad
                    Description &= p & ","
                Next
                Description = Description.Remove(Description.Length - 1, 1)
                If Not Description.Equals(String.Empty) Then
                    Description &= ")"
                End If
            End If
            cboOpcionesTextoInteligente.lstItems.Items.Add(Item.Name & Description)
        End If
    End Sub

    Private Sub AgregarItem(ByVal Item As FieldInfo)
        If Not IsNothing(cboOpcionesTextoInteligente) Then

            Dim Description As String = String.Empty
            If Item.GetCustomAttributes(True).Length > 1 Then
                Description &= "("
                For Each p As String In Item.GetCustomAttributes(True)(1).Propiedad
                    Description &= p & ","
                Next
                Description = Description.Remove(Description.Length - 1, 1)
                Description &= ")"
            End If
            cboOpcionesTextoInteligente.lstItems.Items.Add(Item.Name & Description)
        End If
    End Sub
    Private Sub AgregarAllFromAssembly()
        Try
            Dim SySAssem As Assembly = Assembly.LoadWithPartialName("Zamba.BusinessExtensions")

            For Each SySType As Type In SySAssem.GetTypes
                If String.Compare(SySType.Name, "ALLOBJECTS", True) = 0 Then
                    For Each P As PropertyInfo In SySType.GetProperties
                        Try
                            AgregarItem(P)
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                    Exit For
                End If
            Next
        Catch ex As Exception
            'ZTrace.WriteLineIf(ZTrace.IsInfo,ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene si la propiedad de un objeto es publica o no
    ''' </summary>
    ''' <param name="OBJ"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EsPublica(ByVal OBJ As Object) As Boolean
        Try
            If String.Compare(OBJ.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEPROPERTYINFO", True) = 0 Then
                If String.Compare(OBJ.GetCustomAttributes(True)(0).Propiedad.ToString, "PROPIEDADPUBLICA", True) = 0 Then
                    Return True
                Else
                    Return False
                End If
            ElseIf String.Compare(OBJ.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEFIELDINFO", True) = 0 Then
                If String.Compare(OBJ.GetCustomAttributes(True)(0).Propiedad.ToString, "PROPIEDADPUBLICA", True) = 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                If String.Compare(OBJ.GetCustomAttributes(True)(0).Propiedad.ToString, "PROPIEDADPUBLICA", True) = 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            'ZTrace.WriteLineIf(ZTrace.IsInfo,ex.ToString)
            Return (False)
        End Try
    End Function

    ''' <summary>
    ''' Agrega los items a ser mostrados en la lista
    ''' </summary>
    ''' <history>   Marcelo 24/01/2011  Modified</history>
    ''' <remarks></remarks>
    Private Sub AddNewItems()
        Try
            'Obtiene el textointeligente que se esta escribiendo
            Dim TextoHasta As String = Text.Substring(0, SelectionStart)

            Dim newtext As String = TextoHasta.Replace(">>.<<", "¦").Replace("<<", "§")
            If newtext.Contains("§") Then
                newtext = newtext.Remove(0, newtext.LastIndexOf("§"))
            End If
            newtext = newtext.Replace("§", " <<").Replace("¦", ">>.<<")

            'primer nivel
            If newtext.EndsWith("<<") AndAlso newtext.EndsWith(">>.<<") = False Then
                cboOpcionesTextoInteligente.lstItems.Items.Clear()
                AgregarAllFromAssembly()
                Exit Sub
            End If


            'esto es para cuando es segundo nivel o mas con un objeto y una propiedad
            'Dim Objetos() As String = TextoDesde.Replace(">.<", "|").Replace("<", "|").Replace(">", "|").Replace(".", "").Split("|")
            Dim Objetos As MatchCollection = Regex.Matches(newtext, "<<[^<<>>]+>>")
            '            Dim Objetos As MatchCollection = Regex.Matches(TextoDesde, "<<[^<<>>]+>>\.<<[^<<>>]+>>")
            If Objetos.Count = 0 Then
                If Text.EndsWith(">>.<<") Then
                    Objetos = Regex.Matches(newtext, "<<[^<<>>]+>>\.<<")
                    '   Objetos = Regex.Matches(Text, "<<[^<<>>]+>>\.<<")
                    If Objetos.Count = 0 Then Exit Sub
                Else
                    Exit Sub
                End If
            End If
            cboOpcionesTextoInteligente.lstItems.Items.Clear()

            If Objetos.Count = 1 AndAlso String.IsNullOrEmpty(Objetos.Item(0).Value.Trim) Then
                AgregarAllFromAssembly()
                Exit Sub
            End If

            Dim Niveles As Int32 = Objetos.Count + 2
            Dim ZBusinesssAssembly As Assembly = Assembly.LoadWithPartialName("Zamba.BusinessExtensions")

            'Dim SysType As Type = ZBusinesssAssembly.GetType("ALLOBJECTS")

            For Each SySType As Type In ZBusinesssAssembly.GetTypes

                If String.Compare(SySType.Name, "ALLOBJECTS", True) = 0 Then

                    MiObjeto = Nothing

                    Dim VarBoolean As Boolean
                    Dim NivelFinal As Int32 = Niveles - 2
                    For NivelActual As Int32 = 0 To NivelFinal
                        VarBoolean = False

                        If NivelActual = NivelFinal Then
                            'ULTIMO NIVEL
                            If IsNothing(MiObjeto) Then
                                For Each PP As PropertyInfo In SySType.GetProperties
                                    If EsPublica(PP) Then AgregarItem(PP)
                                Next

                                For Each FF As FieldInfo In SySType.GetFields
                                    If EsPublica(FF) Then AgregarItem(FF)
                                Next
                            ElseIf String.Compare(MiObjeto.GetType.ToString.ToUpper, "SYSTEM.REFLECTION.RUNTIMEPROPERTYINFO", True) = 0 Then
                                For Each PP As PropertyInfo In MiObjeto.PropertyType.GetProperties
                                    Try
                                        If EsPublica(PP) Then
                                            AgregarItem(PP)
                                        End If
                                    Catch ex As Exception
                                        ZClass.raiseerror(ex)
                                    End Try
                                Next
                                For Each FF As FieldInfo In MiObjeto.PropertyType.GetFields
                                    If EsPublica(FF) Then AgregarItem(FF)
                                Next
                            ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEFIELDINFO", True) = 0 Then
                                For Each PP As PropertyInfo In MiObjeto.FieldType.GetProperties
                                    If EsPublica(PP) Then AgregarItem(PP)
                                Next
                                For Each FF As FieldInfo In MiObjeto.FieldType.Getfields
                                    If EsPublica(FF) Then AgregarItem(FF)
                                Next
                            Else
                                For Each PP As PropertyInfo In MiObjeto.GetType.GetProperties
                                    If EsPublica(PP) Then AgregarItem(PP)
                                Next

                                For Each FF As FieldInfo In MiObjeto.FieldType.GetFields
                                    If EsPublica(FF) Then AgregarItem(FF)
                                Next
                            End If
                        ElseIf NivelActual = 0 Then
                            'PRIMER NIVEL: ALLOBJECTS

                            If IsNothing(MiObjeto) Then
                                For Each PP As PropertyInfo In SySType.GetProperties
                                    If String.Compare(PP.Name, Objetos.Item(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                        MiObjeto = PP
                                        VarBoolean = True
                                        Exit For
                                    End If
                                Next
                                If VarBoolean = False Then
                                    For Each FF As FieldInfo In SySType.GetFields
                                        If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = FF
                                            Exit For
                                        End If
                                    Next
                                End If
                            ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEPROPERTYINFO", True) = 0 Then
                                For Each PP As PropertyInfo In MiObjeto.PropertyType.GetProperties
                                    If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                        MiObjeto = PP
                                        VarBoolean = True
                                        Exit For
                                    End If
                                Next
                                If VarBoolean = False Then
                                    For Each FF As FieldInfo In MiObjeto.PropertyType.Getfields
                                        If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = FF
                                            Exit For
                                        End If
                                    Next
                                End If
                            ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEFIELDINFO", True) = 0 Then
                                For Each PP As PropertyInfo In MiObjeto.FieldType.GetProperties
                                    If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                        MiObjeto = PP
                                        VarBoolean = True
                                        Exit For
                                    End If
                                Next
                                If VarBoolean = False Then
                                    For Each FF As FieldInfo In MiObjeto.FieldType.Getfields
                                        If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = FF
                                            Exit For
                                        End If
                                    Next
                                End If
                            Else
                                For Each PP As PropertyInfo In MiObjeto.GetType.GetProperties
                                    If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                        MiObjeto = PP
                                        VarBoolean = True
                                        Exit For
                                    End If
                                Next
                                If VarBoolean = False Then
                                    For Each FF As FieldInfo In MiObjeto.FieldType.GetFields
                                        If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = FF
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                            'PRIMER NIVEL: ALLOBJECTS
                        Else
                            'OTROS NIVELES
                            If IsNothing(MiObjeto) Then
                                For Each PP As PropertyInfo In SySType.GetProperties
                                    If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                        MiObjeto = PP
                                        VarBoolean = True
                                        Exit For
                                    End If
                                Next
                                If VarBoolean = False Then
                                    For Each FF As FieldInfo In SySType.GetFields
                                        If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = FF
                                            Exit For
                                        End If
                                    Next
                                End If
                            ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEPROPERTYINFO", True) = 0 Then
                                For Each PP As PropertyInfo In MiObjeto.PropertyType.GetProperties
                                    If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                        MiObjeto = PP
                                        VarBoolean = True
                                        Exit For
                                    End If
                                Next
                                If VarBoolean = False Then
                                    For Each FF As FieldInfo In MiObjeto.PropertyType.Getfields
                                        If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = FF
                                            Exit For
                                        End If
                                    Next
                                End If
                            ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEFIELDINFO", True) = 0 Then
                                For Each PP As PropertyInfo In MiObjeto.FieldType.GetProperties
                                    If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                        MiObjeto = PP
                                        VarBoolean = True
                                        Exit For
                                    End If
                                Next
                                If VarBoolean = False Then
                                    For Each FF As FieldInfo In MiObjeto.FieldType.Getfields
                                        If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = FF
                                            Exit For
                                        End If
                                    Next
                                End If
                            Else
                                For Each PP As PropertyInfo In MiObjeto.GetType.GetProperties
                                    If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                        MiObjeto = PP
                                        VarBoolean = True
                                        Exit For
                                    End If
                                Next
                                If VarBoolean = False Then
                                    For Each FF As FieldInfo In MiObjeto.FieldType.GetFields
                                        If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = FF
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    Next

                    Exit For

                End If

            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Pone colores a los signos de textointeligente
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ModificarColores()
        Try
            If FirstTime = True Then
                FirstTime = False
                SelectionStart = TextLength
            End If

            Dim HH As Int32 = SelectionStart

            SelectionStart = 0
            SelectionLength = Text.Length
            SelectionColor = Color.Black
            SelectionFont = New Font(Font, FontStyle.Regular)

            Dim Texto As String = Text
            Dim i As Int32

            Dim V() As String = Texto.Split("<<")
            Dim Pos As Int32 = 0
            For i = 0 To V.Length - 2
                Dim Item As String = V(i)
                SelectionStart = Item.Length + Pos
                SelectionLength = 1
                SelectionColor = Color.Blue
                SelectionFont = New Font(Font, FontStyle.Bold)
                Pos += Item.Length + 1
            Next

            Dim VV() As String = Texto.Split(">>")
            Dim Poss As Int32 = 0
            For i = 0 To VV.Length - 2
                Dim Item As String = VV(i)
                SelectionStart = Item.Length + Poss
                SelectionLength = 1
                SelectionColor = Color.Blue
                SelectionFont = New Font(Font, FontStyle.Bold)
                Poss += Item.Length + 1
            Next

            Dim VVV() As String = Texto.Split(".")
            Dim Posss As Int32 = 0
            For i = 0 To VVV.Length - 2
                Dim Item As String = VVV(i)
                SelectionStart = Item.Length + Posss
                SelectionLength = 1
                SelectionColor = Color.Red
                SelectionFont = New Font(Font, FontStyle.Bold)
                Posss += Item.Length + 1
            Next

            Dim VVVV() As String = Texto.Split("+")
            Dim PosssS As Int32 = 0
            For i = 0 To VVVV.Length - 2
                Dim Item As String = VVVV(i)
                SelectionStart = Item.Length + PosssS
                SelectionLength = 1
                SelectionColor = Color.Green
                SelectionFont = New Font(Font, FontStyle.Bold)
                PosssS += Item.Length + 1
            Next

            Dim VVVVV() As String = Texto.Split(Microsoft.VisualBasic.ControlChars.Quote)
            Dim Posssss As Int32 = 0
            For i = 0 To VVVVV.Length - 2
                Dim Item As String = VVVVV(i)
                SelectionStart = Item.Length + Posssss
                SelectionLength = 1
                SelectionColor = Color.Goldenrod
                SelectionFont = New Font(Font, FontStyle.Bold)
                Posssss += Item.Length + 1
            Next

            Dim VVVVVV() As String = Texto.Split("/")
            Dim Possssss As Int32 = 0
            For i = 0 To VVVVVV.Length - 2
                Dim Item As String = VVVVVV(i)
                SelectionStart = Item.Length + Possssss
                SelectionLength = 1
                SelectionColor = Color.Tomato
                SelectionFont = New Font(Font, FontStyle.Bold)
                Possssss += Item.Length + 1
            Next

            SelectionLength = 0
            SelectionStart = HH
            SelectionColor = Color.Black
            SelectionFont = New Font(Font, FontStyle.Regular)
        Catch ex As System.Exception
            'ZTrace.WriteLineIf(ZTrace.IsInfo,ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Muestra el listado de las propiedades
    ''' </summary>
    ''' <history>   Marcelo 24/01/2011  Modified</history>
    ''' <remarks></remarks>
    ''' 

    Private Sub ShowCmb()
        Try

            cboOpcionesTextoInteligente = New cmbTextoInteligente()

            AddNewItems()

            If cboOpcionesTextoInteligente.lstItems.Items.Count > 0 Then
                OldPosition = SelectionStart

                cboOpcionesTextoInteligente.StartPosition = FormStartPosition.CenterScreen
                cboOpcionesTextoInteligente.ShowDialog()

                If cboOpcionesTextoInteligente.blnAcept Then
                    Dim NeedMore As Boolean
                    If Microsoft.VisualBasic.Information.IsNothing(cboOpcionesTextoInteligente.lstItems.SelectedItem) = False Then

                        Dim currentSelectionStart As Int32 = SelectionStart
                        Dim lf = Convert.ToChar(10)
                        'Me.Text = Me.Text.Replace(lf & " ", lf)
                        'Me.Text = Me.Text.Replace(lf, lf & " ")

                        'Completa el texto con la seleccion del listado de propiedades
                        Dim Text1 As String = Text.Substring(0, currentSelectionStart)
                        ' Dim PreviusSpace As Int32 = Me.Text.LastIndexOf(" ", currentSelectionStart, currentSelectionStart - 1)
                        ' If PreviusSpace = -1 Then PreviusSpace = Me.Text.LastIndexOf(lf, currentSelectionStart, currentSelectionStart - 1)
                        'If PreviusSpace = -1 Then PreviusSpace = 0
                        'Dim Text4 As String = Me.Text.Substring(PreviusSpace, currentSelectionStart - PreviusSpace + 1)

                        Dim Text2 As String = cboOpcionesTextoInteligente.lstItems.SelectedItem.ToString
                        Dim text3 As String = Text.Substring(currentSelectionStart)
                        RemoveHandler KeyUp, AddressOf TextoInteligente_KeyPress

                        If Text1.EndsWith(">>.") Then
                            Text = Text1 & text3 & Text2 & ">>.<<"
                            NeedMore = True
                        ElseIf Text1.EndsWith(">>.<<") Then
                            Text = Text1 & Text2 & ">>" & text3
                            NeedMore = False
                        ElseIf Text1.EndsWith(".<<") Then
                            Text = Text1 & Text2 & ">>" & text3
                            NeedMore = False
                        ElseIf Text1.EndsWith("<<") Then
                            Text = Text1 & Text2 & ">>.<<" & text3
                            NeedMore = True
                        Else
                            Text = Text1 & Text2 & ">>" & text3
                            NeedMore = False
                        End If
                        AddHandler KeyUp, AddressOf TextoInteligente_KeyPress
                    End If

                    SelectionStart = 0
                    SelectionLength = Text.Length
                    SelectionColor = Color.Black
                    SelectionFont = New Font(Font, FontStyle.Regular)
                    SelectionStart = OldPosition + cboOpcionesTextoInteligente.lstItems.SelectedItem.ToString.Length + 5

                    closeCmb()
                    If NeedMore Then ShowCmb()
                Else
                    closeCmb()

                    FlagEscape = True
                End If
            Else
                If Text.EndsWith(".<<") Then
                    SelectionStart = TextLength
                    Text = Text.Substring(0, SelectionStart - 3)
                    SelectionStart = TextLength
                Else
                    If Text.IndexOf(">>.<<<") <> -1 Then
                        Text = Text.Replace(">>.<<<", ">>.<<")
                        SelectionStart = TextLength
                    End If

                    If Text.IndexOf(".<<>>.") <> -1 Then
                        Text = Text.Replace(".<<>>.", ".")
                        SelectionStart = TextLength
                    End If

                    If Text.Length >= SelectionStart Then
                        If Text.Substring(0, SelectionStart).EndsWith(".<<") Then
                            Text = Text.Remove(SelectionStart - 3, 3)
                        End If
                    End If
                End If
                closeCmb()
            End If
        Catch ex As Exception
        Finally
            RemoveHandler KeyUp, AddressOf TextoInteligente_KeyPress
            AddHandler KeyUp, AddressOf TextoInteligente_KeyPress
        End Try

    End Sub

    ''' <summary>
    ''' Cierra el listado de propiedades
    ''' </summary>
    ''' <history>Marcelo Created 12/01/10</history>
    ''' <remarks></remarks>
    Private Sub closeCmb()
        If Not IsNothing(cboOpcionesTextoInteligente) Then
            cboOpcionesTextoInteligente.Close()
            cboOpcionesTextoInteligente.Dispose()
            cboOpcionesTextoInteligente = Nothing
        End If
    End Sub
End Class