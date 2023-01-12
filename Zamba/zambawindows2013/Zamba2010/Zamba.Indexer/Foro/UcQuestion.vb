Imports Zamba.Core
Public Class UCQuestion
    Inherits Zamba.AppBlock.ZControl
    Implements IDisposable

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then components.Dispose()
                If ForoTreeView IsNot Nothing Then ForoTreeView.Dispose()
                If lblSinResultados IsNot Nothing Then lblSinResultados.Dispose()
                If selectedNode IsNot Nothing Then selectedNode = Nothing
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ForoTreeView As System.Windows.Forms.TreeView
    Friend WithEvents lblSinResultados As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ForoTreeView = New System.Windows.Forms.TreeView
        lblSinResultados = New ZLabel
        SuspendLayout()
        '
        'ForoTreeView
        '
        ForoTreeView.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
        ForoTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None
        ForoTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        ForoTreeView.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ForoTreeView.ForeColor = System.Drawing.Color.Blue
        ForoTreeView.Indent = 25
        ForoTreeView.Location = New System.Drawing.Point(0, 0)
        ForoTreeView.Name = "ForoTreeView"
        ForoTreeView.Size = New System.Drawing.Size(625, 304)
        ForoTreeView.TabIndex = 4
        '
        'lblSinResultados
        '
        lblSinResultados.AutoSize = True
        lblSinResultados.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
        lblSinResultados.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSinResultados.ForeColor = System.Drawing.Color.RoyalBlue
        lblSinResultados.Location = New System.Drawing.Point(24, 19)
        lblSinResultados.Name = "lblSinResultados"
        lblSinResultados.Size = New System.Drawing.Size(330, 16)
        lblSinResultados.TabIndex = 5
        lblSinResultados.Text = "NO SE ENCONTRARON TEMAS PARA ESTE DOCUMENTO"
        lblSinResultados.Visible = False
        '
        'UCQuestion
        '
        AutoScroll = True
        BackColor = System.Drawing.Color.LightBlue
        Controls.Add(lblSinResultados)
        Controls.Add(ForoTreeView)
        Name = "UCQuestion"
        Size = New System.Drawing.Size(625, 304)
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

    Private selectedNode As TreeNode

    Public Sub FillTreeview(ByVal Mensajes As ArrayList)
        ForoTreeView.Nodes.Clear()
        Dim i As Int16
        For i = 0 To CShort(Mensajes.Count - 1)
            ForoTreeView.Nodes.Add(DirectCast(Mensajes(i), MensajeForo).Mensaje)
        Next
    End Sub

    Public Sub CargarEnTreeview(ByVal ArrayMensajes As ArrayList, ByVal ArrayRespuestas As ArrayList)
        ForoTreeView.Nodes.Clear()
        If IsNothing(ArrayMensajes) Then Exit Sub

        'Se agregan los temas principales con sus respuestas.
        Dim i As Integer
        For i = 0 To ArrayMensajes.Count - 1
            Dim Mensaje As MensajeForo = DirectCast(ArrayMensajes(i), MensajeForo)
            Dim ZForoNode As New ZForoNode(Mensaje)
            Dim len As Int32 = Mensaje.Mensaje.Length - 1
            If len > 70 Then len = 70
            ZForoNode.Text = Mensaje.Name & "  (" & Mensaje.Fecha.ToShortDateString & " - " & Mensaje.UserName & ")"
            ZForoNode.NodeFont = New Font(Font, FontStyle.Bold)
            ZForoNode.ForeColor = SetNodeColor(Mensaje.Fecha, Mensaje.DiasVto)
            ZForoNode.ToolTipText = Mensaje.Mensaje
            ForoTreeView.Nodes.Add(ZForoNode)

            BuscarRespuestas(ArrayRespuestas, ZForoNode)
        Next
        ForoTreeView.ExpandAll()

        If ForoTreeView.Nodes.Count > 0 Then
            SelectNode(ForoTreeView.Nodes(0))
        End If
    End Sub
    Private Function SetNodeColor(ByVal fecCreacion As Date, ByVal diasVto As Int32) As Color
        If Date.Compare(fecCreacion.AddDays(diasVto), Now) > 0 OrElse diasVto = 0 Then
            Return Color.DarkBlue
        Else
            Return Color.Red
        End If
    End Function
    'Obtiene el ID del nodo que inicio la conversación.
    Public Function GetParentMessageId(ByVal messageId As Int32) As Int32
        Dim node As TreeNode
        Dim zambaNode As ZNode
        Try
            If selectedNode IsNot Nothing Then
                node = GetParentNode(selectedNode)
                zambaNode = DirectCast(node, ZNode)

                If zambaNode.ZambaCore IsNot Nothing Then
                    Return DirectCast(zambaNode.ZambaCore, MensajeForo).ID
                Else
                    Return -1
                End If
            Else
                Return -1
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return -1
        Finally
            node = Nothing
            zambaNode = Nothing
        End Try
    End Function


    Private Sub BuscarRespuestas(ByVal ArrayRespuestas As ArrayList, ByRef ParentMessage As ZForoNode)
        If IsNothing(ArrayRespuestas) = False Then
            Dim i As Integer

            For i = 0 To ArrayRespuestas.Count - 1
                If DirectCast(ArrayRespuestas(i), MensajeForo).ParentId = ParentMessage.Tag Then
                    Dim mensaje As MensajeForo = DirectCast(ArrayRespuestas(i), MensajeForo)
                    Dim ZForoSubNode As New ZForoNode(mensaje)
                    Dim len As Int32

                    len = mensaje.Mensaje.Length - 1
                    If len > 40 Then len = 40
                    ZForoSubNode.Text = mensaje.Name & "  (" & mensaje.Fecha.ToShortDateString & " - " & mensaje.UserName & ")"
                    ZForoSubNode.ToolTipText = mensaje.Mensaje
                    ZForoSubNode.ForeColor = SetNodeColor(mensaje.Fecha, mensaje.DiasVto)
                    ParentMessage.Nodes.Add(ZForoSubNode)
                    ZForoSubNode.Expand()

                    BuscarRespuestas(ArrayRespuestas, ZForoSubNode)
                End If
            Next
        End If
    End Sub

    Public Event MessageSelected(ByVal Mensaje As MensajeForo, ByVal forceRefresh As Boolean)

    Private Sub ForoTreeView_NodeClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles ForoTreeView.NodeMouseClick
        If Not ForoTreeView.SelectedNode.Equals(e.Node) Then
            SelectNode(e.Node)
        End If
    End Sub
    Private Sub SelectNode(ByVal node As TreeNode, Optional ByVal forceRefresh As Boolean = False)
        Dim ZNode As ZNode = DirectCast(node, ZNode)
        selectedNode = node
        ForoTreeView.SelectedNode = node
        RaiseEvent MessageSelected(DirectCast(ZNode.ZambaCore, MensajeForo), forceRefresh)
    End Sub
    'Método recursivo para obtener los IDS de los mensajes que se 
    'encuentren en un nodo seleccionado y sus nodos hijos.
    Public Sub GetSelectedAndChildsIds(ByRef messages As Generic.List(Of Int32), ByRef node As ZNode)
        'Obtiene el objeto mensaje que contiene la información necesaria
        Dim Mensaje As MensajeForo = DirectCast(node.ZambaCore, MensajeForo)
        messages.Add(Mensaje.ID)

        If node.Nodes.Count > 0 Then
            For i As Int32 = 0 To node.Nodes.Count - 1
                GetSelectedAndChildsIds(messages, DirectCast(node.Nodes(i), ZNode))
            Next
        End If
    End Sub
    'Tomas: Se modificó la manera en que carga el arrayMensaje. Ahora lo hace recursivamente.
    Public Function DeleteMessage() As Generic.List(Of Int32)
        Dim node As ZNode = DirectCast(ForoTreeView.SelectedNode, ZNode)
        Dim messages As New Generic.List(Of Int32)

        Try
            If IsNothing(node) = False Then
                'Carga todo el array con el nodo seleccionado y sus hijos
                GetSelectedAndChildsIds(messages, node)
                ForoTreeView.SelectedNode.Remove()

                'Selecciona el primer nodo por defecto para recargar los valores
                'del mensaje/adjuntos/participantes y no queden datos viejos.
                If ForoTreeView.Nodes.Count > 0 Then
                    SelectNode(ForoTreeView.Nodes(0))
                End If

                Return messages
            Else
                Return messages
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Return messages
    End Function
    Public Function GetSeleccionado() As ArrayList
        Try
            'Creo un ArrayList con todos los datos del Mensaje
            Dim ArrayMensaje As New ArrayList
            Dim ZNode As ZNode = DirectCast(ForoTreeView.SelectedNode, ZNode)
            If IsNothing(ZNode) OrElse IsNothing(ZNode.ZambaCore) Then Return Nothing
            Dim Mensaje As MensajeForo = DirectCast(ZNode.ZambaCore, MensajeForo)
            ArrayMensaje.Add(Mensaje.Id)
            ArrayMensaje.Add(Mensaje.Name)
            ArrayMensaje.Add(Mensaje.GroupId)

            Dim parentID As Int64 = 0
            Dim parent As Zamba.AppBlock.ZForoNode = ForoTreeView.SelectedNode.Parent
            While Not IsNothing(parent)
                If Not IsNothing(ForoTreeView.SelectedNode.Parent) Then
                    parentID = parent.Tag
                End If
                parent = parent.Parent
            End While

            ArrayMensaje.Add(parentID)

            Return ArrayMensaje
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Sub SinResultados(ByVal Estado As Boolean)
        If Estado = True Then
            lblSinResultados.Visible = True
        Else
            lblSinResultados.Visible = False
        End If
    End Sub

    Public Sub CargarMsgEnTreeview(ByVal Mensaje As MensajeForo)
        Dim ZForoNode As New ZForoNode(Mensaje)
        Dim len As Int32 = Mensaje.Mensaje.Length - 1
        If len > 70 Then len = 70
        ZForoNode.ForeColor = Color.DarkBlue
        ZForoNode.ToolTipText = Mensaje.Mensaje
        If Mensaje.Name.Contains("re:") Then
            ZForoNode.Text = If(Mensaje.Mensaje.Length > 400, Mensaje.Mensaje.Substring(0, 400), Mensaje.Mensaje) & " (" & Mensaje.Fecha.ToShortDateString & " " & Mensaje.UserName & ") "
        Else
            ZForoNode.Text = Mensaje.Name & " (" & Mensaje.Fecha.ToShortDateString & " " & Mensaje.UserName & ") "
        End If

        If Mensaje.ParentId = 0 Then
            'NUEVO MENSAJE
            ZForoNode.NodeFont = New Font(Font, FontStyle.Bold)
            ZForoNode.Expand()
            ForoTreeView.Nodes.Add(ZForoNode)
        Else
            'NUEVA RESPUESTA
            DirectCast(ForoTreeView.SelectedNode, ZNode).Nodes.Add(ZForoNode)
            DirectCast(ForoTreeView.SelectedNode, ZNode).Expand()
        End If

        SelectNode(ZForoNode, True)
    End Sub
    'Método recursivo para obtener el nodo padre de X nodo (el nodo que inicia la conversación).
    Public Function GetParentNode(ByVal node As TreeNode) As TreeNode
        If node.Parent IsNot Nothing Then
            Return GetParentNode(node.Parent)
        Else
            Return node
        End If
    End Function

End Class
