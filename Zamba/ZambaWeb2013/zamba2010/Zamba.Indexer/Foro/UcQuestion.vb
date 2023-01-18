Imports Zamba.Core
Imports Zamba.Foro
Imports Zamba.Foro.components
Imports Zamba.Data
Imports zamba.corecontrols
Imports zamba.appblock
Public Class UCQuestion
    Inherits Zamba.AppBlock.ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ForoTreeView As System.Windows.Forms.TreeView
    Friend WithEvents lblSinResultados As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ForoTreeView = New System.Windows.Forms.TreeView
        Me.lblSinResultados = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ForoTreeView
        '
        Me.ForoTreeView.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.ForoTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ForoTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ForoTreeView.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForoTreeView.ForeColor = System.Drawing.Color.Blue
        Me.ForoTreeView.Indent = 25
        Me.ForoTreeView.Location = New System.Drawing.Point(0, 0)
        Me.ForoTreeView.Name = "ForoTreeView"
        Me.ForoTreeView.Size = New System.Drawing.Size(625, 304)
        Me.ForoTreeView.TabIndex = 4
        '
        'lblSinResultados
        '
        Me.lblSinResultados.AutoSize = True
        Me.lblSinResultados.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.lblSinResultados.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSinResultados.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblSinResultados.Location = New System.Drawing.Point(24, 19)
        Me.lblSinResultados.Name = "lblSinResultados"
        Me.lblSinResultados.Size = New System.Drawing.Size(330, 16)
        Me.lblSinResultados.TabIndex = 5
        Me.lblSinResultados.Text = "NO SE ENCONTRARON TEMAS PARA ESTE DOCUMENTO"
        Me.lblSinResultados.Visible = False
        '
        'UCQuestion
        '
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.LightBlue
        Me.Controls.Add(Me.lblSinResultados)
        Me.Controls.Add(Me.ForoTreeView)
        Me.Name = "UCQuestion"
        Me.Size = New System.Drawing.Size(625, 304)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private selectedNode As TreeNode

    Public Sub FillTreeview(ByVal Mensajes As ArrayList)
        Me.ForoTreeView.Nodes.Clear()
        Dim i As Int16
        For i = 0 To CShort(Mensajes.Count - 1)
            Me.ForoTreeView.Nodes.Add(DirectCast(Mensajes(i), MensajeForo).Mensaje)
        Next
    End Sub

    Public Sub CargarEnTreeview(ByVal ArrayMensajes As ArrayList, ByVal ArrayRespuestas As ArrayList)
        Me.ForoTreeView.Nodes.Clear()
        If IsNothing(ArrayMensajes) Then Exit Sub

        'Se agregan los temas principales con sus respuestas.
        Dim i As Integer
        For i = 0 To ArrayMensajes.Count - 1
            Dim Mensaje As MensajeForo = DirectCast(ArrayMensajes(i), MensajeForo)
            Dim ZForoNode As New ZForoNode(Mensaje)
            Dim len As Int32 = Mensaje.Mensaje.Length - 1
            If len > 70 Then len = 70
            ZForoNode.Text = Mensaje.Name & "  (" & Mensaje.Fecha.ToShortDateString & " - " & Mensaje.UserName & ")"
            ZForoNode.NodeFont = New Font(Me.Font, FontStyle.Bold)
            ZForoNode.ForeColor = SetNodeColor(Mensaje.Fecha, Mensaje.DiasVto)
            ZForoNode.ToolTipText = Mensaje.Mensaje
            Me.ForoTreeView.Nodes.Add(ZForoNode)

            BuscarRespuestas(ArrayRespuestas, ZForoNode)
        Next
        Me.ForoTreeView.ExpandAll()

        If Me.ForoTreeView.Nodes.Count > 0 Then
            SelectNode(Me.ForoTreeView.Nodes(0))
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

    Public Event MessageSelected(ByVal Mensaje As MensajeForo)

    Private Sub ForoTreeView_NodeClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles ForoTreeView.NodeMouseClick
        If Not ForoTreeView.SelectedNode.Equals(e.Node) Then
            SelectNode(e.Node)
        End If
    End Sub
    Private Sub SelectNode(ByVal node As TreeNode)
        Dim ZNode As ZNode = DirectCast(node, ZNode)
        selectedNode = node
        Me.ForoTreeView.SelectedNode = node
        RaiseEvent MessageSelected(DirectCast(ZNode.ZambaCore, MensajeForo))
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
        Dim node As ZNode = DirectCast(Me.ForoTreeView.SelectedNode, ZNode)
        Dim messages As New Generic.List(Of Int32)

        Try
            If IsNothing(node) = False Then
                'Carga todo el array con el nodo seleccionado y sus hijos
                GetSelectedAndChildsIds(messages, node)
                Me.ForoTreeView.SelectedNode.Remove()

                'Selecciona el primer nodo por defecto para recargar los valores
                'del mensaje/adjuntos/participantes y no queden datos viejos.
                If Me.ForoTreeView.Nodes.Count > 0 Then
                    SelectNode(Me.ForoTreeView.Nodes(0))
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
            Dim ZNode As ZNode = DirectCast(Me.ForoTreeView.SelectedNode, ZNode)
            If IsNothing(ZNode) OrElse IsNothing(ZNode.ZambaCore) Then Return Nothing
            Dim Mensaje As MensajeForo = DirectCast(ZNode.ZambaCore, MensajeForo)
            ArrayMensaje.Add(Mensaje.Id)
            ArrayMensaje.Add(Mensaje.Name)
            ArrayMensaje.Add(Mensaje.GroupId)

            Dim parentID As Int64 = 0
            Dim parent As Zamba.AppBlock.ZForoNode = Me.ForoTreeView.SelectedNode.Parent
            While Not IsNothing(parent)
                If Not IsNothing(Me.ForoTreeView.SelectedNode.Parent) Then
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
            Me.lblSinResultados.Visible = True
        Else
            Me.lblSinResultados.Visible = False
        End If
    End Sub

    Public Sub CargarMsgEnTreeview(ByVal Mensaje As MensajeForo)
        Dim ZForoNode As New ZForoNode(Mensaje)
        Dim len As Int32 = Mensaje.Mensaje.Length - 1
        If len > 70 Then len = 70
        ZForoNode.ForeColor = Color.DarkBlue
        ZForoNode.ToolTipText = Mensaje.Mensaje
        ZForoNode.Text = Mensaje.Name & " (" & Mensaje.Fecha.ToShortDateString & " " & Mensaje.UserName & ") "

        If Mensaje.ParentId = 0 Then
            'NUEVO MENSAJE
            ZForoNode.NodeFont = New Font(Me.Font, FontStyle.Bold)
            ZForoNode.Expand()
            Me.ForoTreeView.Nodes.Add(ZForoNode)
        Else
            'NUEVA RESPUESTA
            DirectCast(Me.ForoTreeView.SelectedNode, ZNode).Nodes.Add(ZForoNode)
            DirectCast(Me.ForoTreeView.SelectedNode, ZNode).Expand()
        End If

        SelectNode(ZForoNode)
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
