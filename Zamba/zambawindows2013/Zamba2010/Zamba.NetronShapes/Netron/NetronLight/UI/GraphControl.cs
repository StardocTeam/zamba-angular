using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
///using Zamba.Core;

namespace Zamba.Shapes.NetronLight
{
    /// <summary>
    /// A 'light' version of the Netron graph control
    /// without all the advanced diagramming stuff
    /// see however http://netron.sf.net for more info.
    /// This control shows the simplicity with which you can still achieve good results,
    /// it's a toy-model to explore and can eventually help you if you want to go for a 
    /// bigger adventure in the full Netron control.
    /// Question and comments are welcome via the forum of The Netron Project or mail me
    /// [Illumineo@users.sourceforge.net]
    /// 
    /// Thank you for downloading the code and your feedback!
    /// 
    /// </summary>
    public class GraphControl : System.Windows.Forms.UserControl ///Zamba.AppBlock.TransparentLabel
    {
        #region Events and delegates
        /// <summary>
        /// the info coming with the show-props event
        /// </summary>
        public delegate void ShowProps(object ent);

        /// <summary>
        /// notifies the host to show the properties usually in the property grid
        /// </summary>
        public event ShowProps OnShowProps;

        #endregion

        #region Fields

        /// <summary>
        /// the collection of shapes on the canvas
        /// </summary>
        protected ShapeCollection shapes;
        /// <summary>
        /// the collection of connections on the canvas
        /// </summary>
        public ConnectionCollection connections;
        /// <summary>
        /// the entity hovered by the mouse
        /// </summary>
        protected Entity hoveredEntity;
        /// <summary>
        /// the unique entity currently selected
        /// </summary>
        protected Entity selectedEntity;
        /// <summary>
        /// whether we are tracking, i.e. moving something around
        /// </summary>
        protected bool tracking = false;
        /// <summary>
        /// just a reference point for the OnMouseDown event
        /// </summary>
        protected Point refp;
        /// <summary>
        /// the context menu of the control
        /// </summary>
        protected ContextMenu menu;
        /// <summary>
        /// A simple, general purpose random generator
        /// </summary>
        protected Random rnd;
        /// <summary>
        /// simple proxy for the propsgrid of the control
        /// </summary>
        protected Proxy proxy;

        /// <summary>
        /// drawing a grid on the canvas?
        /// </summary>
        protected bool showGrid = true;

        /// <summary>
        /// just the default gridsize used in the paint-background method
        /// </summary>
        protected Size gridSize = new Size(10, 10);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the shape collection
        /// </summary>
        public ShapeCollection Shapes
        {
            get { return shapes; }
            set { shapes = value; }
        }

        /// <summary>
        /// Gets or sets whether the grid is drawn on the canvas
        /// </summary>
        public bool ShowGrid
        {
            get { return showGrid; }
            set { showGrid = value; Invalidate(); }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Default ctor
        /// </summary>
        public GraphControl()
        {
            //double-buffering
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //init the collections
            shapes = new ShapeCollection();
            connections = new ConnectionCollection();

            //menu
            menu = new ContextMenu();
            BuildMenu();
            this.ContextMenu = menu;

            //init the randomizer
            rnd = new Random();

            //init the proxy
            proxy = new Proxy(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds the context menu
        /// </summary>
        /// 

        private void BuildMenu()
        {

            MenuItem mnuProps = new MenuItem("Propiedades", new EventHandler(OnProps));
            menu.MenuItems.Add(mnuProps);
            mnuProps.Visible = false;

            //MenuItem mnuDash = new MenuItem("-");
            //menu.MenuItems.Add(mnuDash);

            MenuItem mnuRecShape = new MenuItem("Nuevo Rectángulo", new EventHandler(OnRecShape));
            menu.MenuItems.Add(mnuRecShape);

            MenuItem mnuColor = new MenuItem("Seleccionar Color", new EventHandler(OnChangeColor));
            menu.MenuItems.Add(mnuColor);

            //MenuItem mnuAlto = new MenuItem("Modificar Alto", new EventHandler(OnChangeAlto));
            //menu.MenuItems.Add(mnuAlto);

            //MenuItem mnuAncho = new MenuItem("Modificar Ancho", new EventHandler(OnChangeAncho));
            //menu.MenuItems.Add(mnuAncho);

            MenuItem mnuTexto = new MenuItem("Modificar Texto", new EventHandler(OnChangeTexto));
            menu.MenuItems.Add(mnuTexto);

            MenuItem mnuDelete = new MenuItem("Eliminar Rectángulo", new EventHandler(OnDelete));
            menu.MenuItems.Add(mnuDelete);

            MenuItem mnuNewConnection = new MenuItem("Agregar Conexión", new EventHandler(OnNewConnection));
            menu.MenuItems.Add(mnuNewConnection);
            mnuNewConnection.Visible = false;

            MenuItem mnuShapes = new MenuItem("Objetos");
            menu.MenuItems.Add(mnuShapes);
            mnuShapes.Visible = false;

            MenuItem mnuOvalShape = new MenuItem("Oval", new EventHandler(OnOvalShape));
            mnuShapes.MenuItems.Add(mnuOvalShape);

            MenuItem mnuTLShape = new MenuItem("Texto", new EventHandler(OnTextLabelShape));
            mnuShapes.MenuItems.Add(mnuTLShape);


        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

            try
            {
                base.OnPaintBackground(e);
                Graphics g = e.Graphics;

                if (showGrid)
                    ControlPaint.DrawGrid(g, this.ClientRectangle, gridSize, this.BackColor);
            }
            catch
            {
            }


        }


        #region Menu handlers
        /// <summary>
        /// Deletes the currently selected object from the canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDelete(object sender, EventArgs e)
        {
            if (selectedEntity != null)
            {
                if (typeof(ShapeBase).IsInstanceOfType(selectedEntity))
                {
                    this.shapes.Remove(selectedEntity as ShapeBase);
                    this.Invalidate();
                }
                else
                    if (typeof(Connection).IsInstanceOfType(selectedEntity))
                    {
                        this.connections.Remove(selectedEntity as Connection);
                        this.Invalidate();
                    }

            }
        }


        /// <summary>
        /// Asks the host to show the props
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnProps(object sender, EventArgs e)
        {
            object thing;
            if (this.selectedEntity == null)
                thing = this.proxy;
            else
                thing = selectedEntity;
            if (this.OnShowProps != null)
                OnShowProps(thing);

        }

        /// <summary>
        /// Adds a rectangular shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnRecShape(object sender, EventArgs e)
        {
            AddShape(ShapeTypes.Rectangular, refp);
            this.Refresh();
        }

        private void OnOvalShape(object sender, EventArgs e)
        {
            AddShape(ShapeTypes.Oval, refp);
        }
        private void OnTextLabelShape(object sender, EventArgs e)
        {
            AddShape(ShapeTypes.TextLabel, refp);
            this.Refresh();
        }
        private void OnNewConnection(object sender, EventArgs e)
        {
            AddConnection(refp);
        }
        private void OnChangeColor(object sender, EventArgs e)
        {


            for (int k = 0; k <= this.shapes.Count - 1; k++)
            {
                ShapeBase obj = (ShapeBase)this.shapes[k];
                if (obj.IsSelected == true)
                {
                    System.Windows.Forms.ColorDialog Dialog = new System.Windows.Forms.ColorDialog();
                    Dialog.Color = obj.ShapeColor;
                    Dialog.ShowDialog();
                    obj.ShapeColor = Dialog.Color;
                    break;
                }
            }
            this.Refresh();
        }

        //		private void OnChangeAlto(object sender, EventArgs e)
        //		{
        ////		try
        ////		{
        ////
        ////
        ////			for (int k = 0; k <= this.shapes.Count - 1; k++) 
        ////			{ 
        ////				ShapeBase obj =	(ShapeBase)this.shapes[k];
        ////				if (obj.IsSelected==true)
        ////				{
        ////					int Alto = obj.Height;
        ////					Alto = CInputBox.CInputBox.ShowInputBox("Alto: ","",Alto.ToString() );
        ////					obj.Height = Alto;
        ////					break;
        ////				}
        ////			}
        ////			
        ////		}
        ////		catch
        ////		{
        ////		}
        //		}

        private void OnChangeTexto(object sender, EventArgs e)
        {
            try
            {

                for (int k = 0; k <= this.shapes.Count - 1; k++)
                {
                    ShapeBase obj = (ShapeBase)this.shapes[k];
                    if (obj.IsSelected == true)
                    {

                        String Texto = obj.Text;
                        Texto = (String)CInputBox.ShowInputBox(Texto);
                        obj.Text = Texto;
                        break;
                    }
                }

            }
            catch
            {
            }
        }


        //		private void OnChangeAncho(object sender, EventArgs e)
        //		{
        ////
        ////		try
        ////		{
        ////
        ////
        ////			for (int k = 0; k <= this.shapes.Count - 1; k++) 
        ////			{ 
        ////				ShapeBase obj =	(ShapeBase)this.shapes[k];
        ////				if (obj.IsSelected==true)
        ////				{
        ////					int Ancho = obj.Width;
        ////					Ancho = (int) CInputBox.CInputBox.ShowInputBox("Ancho: ","",Ancho.ToString() );
        ////					obj.Width = Ancho;
        ////					break;
        ////				}
        ////			}
        ////			
        ////		}
        ////		catch
        ////		{
        ////		}
        //		}

        #endregion

        //Variables que manejan la imagen
        /// <summary>
        /// zoom del alto
        /// </summary>
        public float zoomHeight = 1;
        /// <summary>
        /// zoom del ancho
        /// </summary>
        public float zoomWidth = 1;
        /// <summary>
        /// Imagen de fondo
        /// </summary>
        public Image Image;
        public Boolean isTif = false;
        public States State = Zamba.Shapes.NetronLight.GraphControl.States.Ninguno;
        public Point pOCR1;
        public Point pOCR2;
        public Rectangle recOCR;
        public enum States
        {
            Ninguno = 0,
            OCR = 1,
            Netron = 2,
            Nota = 3,
            Firma = 4
        }
         /// <summary>
        /// Paints the control
        /// </summary>
        /// <remarks>
        /// If you switch the painting order of connections and shapes the connection line
        /// will be underneath/above the shape
        /// </remarks>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //use the best quality, with a performance penalty
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            //Si hay imagen la dibuja y la escala
            if (Image != null)
            {
                if (isTif == false)
                    this.AutoScrollMinSize = new Size((int)(this.Image.Width * this.zoomWidth), (int)(this.Image.Height * this.zoomHeight));
                else
                    this.AutoScrollMinSize = new Size((int)(this.Image.Width * this.zoomWidth/2), (int)(this.Image.Height * this.zoomHeight/2));
                g.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
                g.ScaleTransform(zoomWidth, zoomHeight);
                g.DrawImage(this.Image, this.ClientRectangle.Location);
            }

            //similarly for the connections
            for (int k = 0; k < connections.Count; k++)
            {
                connections[k].Paint(g);
                connections[k].From.Paint(g);
                connections[k].To.Paint(g);
            }
            //loop over the shapes and draw
            for (int k = 0; k < shapes.Count; k++)
            {
                shapes[k].Paint(g);
                if (shapes[k].Opaque == false)
                {
                    System.Drawing.Color newcolor = System.Drawing.Color.FromArgb(128, shapes[k].ShapeColor);
                    shapes[k].shapeBrush = new SolidBrush(newcolor);
                }
                else
                {
                    System.Drawing.Color newcolor = System.Drawing.Color.FromArgb(255, shapes[k].ShapeColor);
                    shapes[k].shapeBrush = new SolidBrush(newcolor);
                }
            }

            if (this.State == Zamba.Shapes.NetronLight.GraphControl.States.OCR)
            {
                if (pOCR1.X - pOCR2.X <0 & pOCR1.Y - pOCR2.Y < 0)
                {
                    recOCR = new Rectangle(pOCR1.X, pOCR1.Y, pOCR2.X - pOCR1.X, pOCR2.Y - pOCR1.Y);
                    g.DrawRectangle(new Pen(Brushes.Blue),recOCR);
                }
                else if(pOCR1.X - pOCR2.X <0 & pOCR1.Y - pOCR2.Y > 0)
                {
                    recOCR = new Rectangle(pOCR1.X, pOCR2.Y, pOCR2.X - pOCR1.X, pOCR1.Y - pOCR2.Y);
                    g.DrawRectangle(new Pen(Brushes.Blue), recOCR);
                }
                else if(pOCR1.X - pOCR2.X >0 & pOCR1.Y - pOCR2.Y < 0)
                {
                    recOCR = new Rectangle(pOCR2.X, pOCR1.Y, pOCR1.X - pOCR2.X, pOCR2.Y - pOCR1.Y);
                    g.DrawRectangle(new Pen(Brushes.Blue), recOCR);
                }
                else 
                {
                    recOCR = new Rectangle(pOCR2.X, pOCR2.Y, pOCR1.X - pOCR2.X, pOCR1.Y - pOCR2.Y);
                    g.DrawRectangle(new Pen(Brushes.Blue), recOCR); 
                }
            }
        }


        /// <summary>
        /// Adds a shape to the canvas or diagram
        /// </summary>
        /// <param name="shape"></param>
        /// 
        public ShapeBase AddShape(ShapeBase shape)
        {
            shapes.Add(shape);
            shape.Site = this;
            this.Invalidate();
            return shape;
        }

        /// <summary>
        /// Adds a predefined shape
        /// </summary>
        /// <param name="type"></param>
        public ShapeBase AddShape(ShapeTypes type, Point location)
        {
            ShapeBase shape = null;
            switch (type)
            {
                case ShapeTypes.Rectangular:
                    shape = new SimpleRectangle(this);
                    break;
                case ShapeTypes.Oval:
                    shape = new OvalShape(this);
                    break;
                case ShapeTypes.TextLabel:
                    shape = new TextLabel(this);
                    shape.Location = location;
                    shape.ShapeColor = Color.Transparent;
                    shape.Text = "Texto (Modifique el Texto en la Ventana de Propiedades)";
                    shape.Width = 350;
                    shape.Height = 30;


                    shapes.Add(shape);
                    return shape;


            }
            if (shape == null) return null;
            shape.ShapeColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            shape.Location = location;

            if (shape.Id == 0)
            {
                ///martin///                shape.Id = ToolsBusiness.GetNewID(IdTypes.ShapeNetron);
            }

            shapes.Add(shape);
            return shape;
        }


        //		System.Collections.ArrayList ArrayId  = new System.Collections.ArrayList();

        //		private int CoreData.GetNewID()
        //		{
        //			try
        //			{
        //
        //			int id = 0;
        //			
        //				if (ArrayId.Count == 0)
        //				{
        //					String strSelect = "Select Max(Shape_Id) From ZNetronShapes";
        //					id = (int) Zamba.Servers.Server.get_Con(false,true,false).ExecuteScalar(System.Data.CommandType.Text,  strSelect);  
        //					ArrayId.Add(id);
        //					return id + 1;
        //				}	
        //				else
        //				{
        //					id = (int) ArrayId[ArrayId.Count - 1] ; 
        //					ArrayId.Add(id+ 1);
        //					return id+ 1;
        //				}
        //
        //			
        //			}
        //			catch
        //			{
        //			ArrayId.Add(1);
        //			return 1;
        //			}
        //		}

        #region AddConnection overloads
        /// <summary>
        /// Adds a connection to the diagram
        /// </summary>
        /// <param name="con"></param>
        public Connection AddConnection(Connection con)
        {
            if (con.Id == 0)
            {
                ///martin///     con.Id = ToolsBusiness.GetNewID(IdTypes.ShapeNetron);
            }
            connections.Add(con);
            con.Site = this;
            con.From.Site = this;
            con.To.Site = this;
            this.Invalidate();
            return con;
        }
        public Connection AddConnection(Point startPoint)
        {
            //let's take a random point and assume this control is not infinitesimal (bigger than 20x20).
            Point rndPoint = new Point(rnd.Next(20, this.Width - 20), rnd.Next(20, this.Height - 20));
            Connection con = new Connection(startPoint, rndPoint);
            AddConnection(con);
            //use the end-point and simulate a dragging operation, see the OnMouseDown handler
            selectedEntity = con.To;
            tracking = true;
            refp = rndPoint;
            this.Invalidate();
            return con;

        }

        public Connection AddConnection(Connector from, Connector to)
        {
            Connection con = AddConnection(from.Point, to.Point);
            from.AttachConnector(con.From);
            to.AttachConnector(con.To);


            return con;

        }

        public Connection AddConnection(Point from, Point to)
        {
            Connection con = new Connection(from, to);
            this.AddConnection(con);
            return con;
        }
        #endregion

        #region Mouse event handlers

        /// <summary>
        /// Handles the mouse-down event
        /// </summary>
        /// <param name="e"></param>
        /// 


        bool ResizingBottom = false;
        bool ResizingTop = false;
        bool ResizingLeft = false;
        bool ResizingRight = false;
        bool ResizingOCR = false;

        int Seleccionada = 0;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Point p = new Point(e.X, e.Y);
            refp = p;

            #region LMB & RMB

            //test for connectors
            for (int k = 0; k < connections.Count; k++)
            {
                if (connections[k].From.Hit(p))
                {
                    if (selectedEntity != null) selectedEntity.IsSelected = false;
                    selectedEntity = connections[k].From;
                    tracking = true;
                    refp = p;
                    return;
                }

                if (connections[k].To.Hit(p))
                {
                    if (selectedEntity != null) selectedEntity.IsSelected = false;
                    selectedEntity = connections[k].To;
                    tracking = true;
                    refp = p;
                    return;
                }
            }

            //test for connections
            for (int k = 0; k < this.connections.Count; k++)
            {
                if (connections[k].Hit(p))
                {
                    if (selectedEntity != null) selectedEntity.IsSelected = false;
                    selectedEntity = this.connections[k];
                    selectedEntity.IsSelected = true;
                    if (OnShowProps != null)
                        OnShowProps(this.connections[k]);
                    if (e.Button == MouseButtons.Right)
                    {
                        if (OnShowProps != null)
                            OnShowProps(this);
                    }
                    return;
                }
            }
            
            //Calculo la posicion del puntero con el Zoom
       
            if (this.State == Zamba.Shapes.NetronLight.GraphControl.States.OCR)
            {
                p.X = (int)(p.X / zoomWidth + HorizontalScroll.Value / zoomWidth);
                p.Y = (int)(p.Y / zoomHeight + VerticalScroll.Value / zoomWidth);
                pOCR1 = p;
                pOCR2 = p;
                ResizingOCR = true;
                tracking = false;
                this.Refresh();
            }
            else
            {
                p.X = (int)(p.X / zoomWidth + HorizontalScroll.Value / zoomWidth);
                p.Y = (int)(p.Y / zoomHeight + VerticalScroll.Value / zoomWidth);
                ResizingOCR = false;
                for (int k = 0; k < shapes.Count; k++)
                {
                   //int X = p.X;
                   //int Y = p.Y;

                    //Point cBottom = new Point((int)(shapes[k].X + shapes[k].Width / 2), shapes[k].Y + shapes[k].Height);
                    //Point cLeft = new Point(shapes[k].X, (int)(shapes[k].Y + shapes[k].Height / 2));
                    //Point cRight = new Point(shapes[k].X + shapes[k].Width, (int)(shapes[k].Y + shapes[k].Height / 2));
                    //Point cTop = new Point((int)(shapes[k].X + shapes[k].Width / 2), shapes[k].Y);

                    Int32 cBottom = 0;
                    Int32 cLeft = 0;
                    Int32 cRight = 0;
                    Int32 cTop = 0;

                    //Bordes del shape seleccionado
                    cBottom = shapes[k].Rec.Bottom;
                    cLeft = shapes[k].Rec.Left;
                    cRight = shapes[k].Rec.Right;
                    cTop = shapes[k].Rec.Top;

                    //Habilito los Resize
                    if ((cBottom - p.Y) < 5 && (cBottom - p.Y) > -5 && (cLeft - p.X) < -5 && (cRight - p.X) > 0)
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNS;
                        ResizingBottom = true;
                        //						ResizingTop = false;
                        //						ResizingLeft = false;
                        //						ResizingRight = false;
                        tracking = false;
                    }

                    if ((cRight - p.X) < 5 && (cRight - p.X) > -5 && (cBottom - p.Y) > 0 && (cTop - p.Y) < -5)
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeWE;
                        //						ResizingBottom = false;
                        //						ResizingTop = false;
                        //						ResizingLeft = false;
                        ResizingRight = true;
                        tracking = false;
                    }

                    if ((cBottom - p.Y) < 5 && (cBottom - p.Y) > -5 && (cRight - p.X) < 5 && (cRight - p.X) > -5)
                    {
                        ResizingBottom = true;
                        //						ResizingTop = false;
                        //						ResizingLeft = false;
                        ResizingRight = true;
                        tracking = false;
                    }
                    //					if(System.Math.Abs(cTop.Y - Y) < 15)
                    //					{
                    //						System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNS;
                    //						ResizingBottom = false;
                    //						ResizingTop = true;
                    //						ResizingLeft = false;
                    //						ResizingRight = false;
                    //						tracking = false;
                    //					}

                    if (System.Math.Abs(cRight) - p.X < 15 && System.Math.Abs(cBottom - p.Y) < 15)
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNWSE;
                    }

                
                    if (shapes[k].Rec.Contains(p) && ResizingRight == false && ResizingBottom == false)
                    {
                        try
                        {
                            Seleccionada = k;
                        }
                        catch
                        {
                        }

                        //shapes[k].ShapeColor = Color.WhiteSmoke;
                        tracking = true;
                        if (selectedEntity != null) selectedEntity.IsSelected = false;
                        selectedEntity = shapes[k];
                        selectedEntity.IsSelected = true;
                        refp = new Point(e.X, e.Y);
                        if (OnShowProps != null)
                            OnShowProps(this.shapes[k]);
                        if (e.Button == MouseButtons.Right)
                        {
                            if (OnShowProps != null)
                                OnShowProps(this);
                        }
                        return;
                    }
                }
        }
            if (selectedEntity != null) selectedEntity.IsSelected = false;
            selectedEntity = null;
            Invalidate();
            refp = p; //useful for all kind of things
            //nothing was selected but we'll show the props of the control in this case
            if (OnShowProps != null)
                OnShowProps(this.proxy);
            #endregion
        }

        /// <summary>
        /// Handles the mouse-up event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            ResizingBottom = false;
            ResizingTop = false;
            ResizingLeft = false;
            ResizingRight = false;

            //test if we connected a connection
            if (tracking)
            {
                Point p = new Point(e.X, e.Y);
   
                if (typeof(Connector).IsInstanceOfType(selectedEntity))
                {
                    Connector con;
                    for (int k = 0; k < shapes.Count; k++)
                    {
                        if ((con = shapes[k].HitConnector(p)) != null)
                        {
                            try
                            {
                                Seleccionada = k;
                            }
                            catch
                            {
                            }
                            con.AttachConnector(selectedEntity as Connector);
                            tracking = false;
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Arrow;
                            return;
                        }
                    }
                    (selectedEntity as Connector).Release();
                }
                tracking = false;
            }

            if (ResizingOCR == true)
            {
                ResizingOCR = false;
            }
        }


        /// <summary>
        /// Handles the mouse-move event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point p = new Point(e.X, e.Y);

            if (ResizingOCR == true)
            {
                Invalidate();
                p.X = (int)(p.X / zoomWidth + HorizontalScroll.Value / zoomWidth);
                p.Y = (int)(p.Y / zoomHeight + VerticalScroll.Value / zoomWidth);
                pOCR2 = p;
            }
            else
            {
                if (tracking)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    //Muevo el shape desde la posicion guardada a la nueva
                    selectedEntity.Move(new Point((int)((p.X - refp.X) / zoomWidth), (int)((p.Y - refp.Y) / zoomHeight)));
                    refp = p;
                    Invalidate();
                    if (typeof(Connector).IsInstanceOfType(selectedEntity))
                    {
                        HoverNone();
                        //test the connecting points of hovered shapes
                        for (int k = 0; k < shapes.Count; k++)
                        {
                            shapes[k].HitConnector(p);
                        }
                    }

                    ResizingBottom = false;
                    //				ResizingTop=false;
                    //				ResizingLeft=false;
                    ResizingRight = false;
                }

                if (tracking == false)
                {
                    try
                    {
                        //Calculo el punto con el Zoom
                        p.X = (int)(p.X / zoomWidth + HorizontalScroll.Value / zoomWidth);
                        p.Y = (int)(p.Y / zoomHeight + VerticalScroll.Value / zoomWidth);

                        //Obtengo el shape
                        for (int k = 0; k < shapes.Count; k++)
                        {
                            if (shapes[k].Rec.Contains(p))
                            {
                                Seleccionada = k;
                            }
                        }
                        //int X = p.X;
                        //int Y = p.Y;

                        //Point cBottom = new Point((int)(shapes[Seleccionada].X + shapes[Seleccionada].Width / 2), shapes[Seleccionada].Y + shapes[Seleccionada].Height);
                        //Point cLeft = new Point(shapes[Seleccionada].X, (int)(shapes[Seleccionada].Y + shapes[Seleccionada].Height / 2));
                        //Point cRight = new Point(shapes[Seleccionada].X + shapes[Seleccionada].Width, (int)(shapes[Seleccionada].Y + shapes[Seleccionada].Height / 2));
                        //Point cTop = new Point((int)(shapes[Seleccionada].X + shapes[Seleccionada].Width / 2), shapes[Seleccionada].Y);

                        Int32 cBottom = 0;
                        Int32 cLeft = 0;
                        Int32 cRight = 0;
                        Int32 cTop = 0;

                        //Bordes del shape seleccionado
                        cBottom = shapes[Seleccionada].Rec.Bottom;
                        cLeft = shapes[Seleccionada].Rec.Left;
                        cRight = shapes[Seleccionada].Rec.Right;
                        cTop = shapes[Seleccionada].Rec.Top;

                        //Si esta sobre los bordes cambio el puntero
                        if ((cBottom - p.Y) < 5 && (cBottom - p.Y) > -5 && (cLeft - p.X) < -5 && (cRight - p.X) > 5)
                        {
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNS;
                        }
                        if ((cRight - p.X) < 5 && (cRight - p.X) > -5 && (cBottom - p.Y) > 5 && (cTop - p.Y) < -5)
                        {
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeWE;
                        }
                        if ((cBottom - p.Y) < 5 && (cBottom - p.Y) > -5 && (cRight - p.X) < 5 && (cRight - p.X) > -5)
                        {
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNWSE;
                        }

                        //if (System.Math.Abs(cBottom.Y - Y) < 15 && (System.Math.Abs(cBottom.X - X) <= (shapes[Seleccionada].originalWidth / 2 + this.HorizontalScroll.Value)) && System.Math.Abs(cRight.X - X) >= 15 && tracking == false)
                        //{
                        //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNS;
                        //}
                        //if (System.Math.Abs(cRight.X - X) < 15 && (System.Math.Abs(cRight.Y - Y) <= (shapes[Seleccionada].originalHeight / 2 + this.VerticalScroll.Value)) && System.Math.Abs(cBottom.Y - Y) >= 15 && tracking == false)
                        //{
                        //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeWE;
                        //}
                        //if (System.Math.Abs(cRight.X - X) < 15 && System.Math.Abs(cBottom.Y - Y) < 15 && tracking == false)
                        //{
                        //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNWSE;
                        //}
                        //if (System.Math.Abs(cRight.X - X) >= 15 && System.Math.Abs(cBottom.Y - Y) >= 15)
                        //{
                        //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                        //}

                        //Cambio el tamaño
                        if (ResizingBottom)
                        {
                            int PrevioTop = shapes[Seleccionada].Y + shapes[Seleccionada].Height;
                            int NuevoTop = p.Y - PrevioTop;
                            int NuevoHeight = shapes[Seleccionada].Height += NuevoTop;
                            if (NuevoHeight < 20)
                            {
                                NuevoHeight = 20;
                            }
                            shapes[Seleccionada].Height = NuevoHeight;
                            this.Refresh();
                        }
                        if (ResizingTop)
                        {
                            //				int PrevioTop = shapes[Seleccionada].Y;
                            //				int NuevoTop = PrevioTop - e.Y;
                            //				int NuevoHeight = shapes[Seleccionada].Height + NuevoTop;
                            //				if (NuevoHeight < 20)
                            //				{
                            //				NuevoHeight=20;
                            //				ResizingTop=false;
                            //				}
                            //				shapes[Seleccionada].Height = NuevoHeight; 
                            //				Point Point = new Point(shapes[Seleccionada].X,shapes[Seleccionada].Y -NuevoHeight);
                            //				shapes[Seleccionada].Move(Point); 
                        }
                        if (ResizingLeft)
                        {
                            //						Point cLeft = new Point(shapes[Seleccionada].X,(int) (shapes[Seleccionada].Y +shapes[Seleccionada].Height/2));
                            //						shapes[Seleccionada].Width+=e.X -cLeft.X; 
                            //						shapes[Seleccionada].X-=e.X-cLeft.X;
                        }
                        if (ResizingRight)
                        {
                            int PrevioLeft = shapes[Seleccionada].X + shapes[Seleccionada].Width;
                            int NuevoLeft = p.X - PrevioLeft;
                            int NuevoWidth = shapes[Seleccionada].Width += NuevoLeft;
                            if (NuevoWidth < 20)
                            {
                                NuevoWidth = 20;
                            }
                            shapes[Seleccionada].Width = NuevoWidth;
                            this.Refresh();
                        }
                    }
                    catch
                    {
                    }
                }

                //hovering stuff
                for (int k = 0; k < shapes.Count; k++)
                {
                    if (shapes[k].Rec.Contains(p))
                    {
                        if (hoveredEntity != null) hoveredEntity.hovered = false;
                        shapes[k].hovered = true;
                        hoveredEntity = shapes[k];
                        hoveredEntity.Invalidate();
                        this.Refresh();
                        return;
                    }
                    else
                        shapes[k].hovered = false;
                    this.Refresh();
                }
                for (int k = 0; k < connections.Count; k++)
                {
                    if (connections[k].Hit(p))
                    {
                        if (hoveredEntity != null) hoveredEntity.hovered = false;
                        connections[k].hovered = true;
                        hoveredEntity = connections[k];
                        hoveredEntity.Invalidate();
                        return;
                    }
                }

                for (int k = 0; k < connections.Count; k++)
                {
                    if (connections[k].From.Hit(p))
                    {
                        connections[k].From.hovered = true;
                        hoveredEntity = connections[k].From;
                        hoveredEntity.Invalidate();
                        return;
                    }

                    if (connections[k].To.Hit(p))
                    {
                        connections[k].To.hovered = true;
                        hoveredEntity = connections[k].To;
                        hoveredEntity.Invalidate();
                        return;
                    }
                }
                HoverNone();
            }
        }


        #endregion

        /// <summary>
        /// Resets the hovering status of the control, i.e. the hoverEntity is set to null.
        /// </summary>
        private void HoverNone()
        {
            if (hoveredEntity != null)
            {
                hoveredEntity.hovered = false;
                hoveredEntity.Invalidate();
            }
            hoveredEntity = null;
        }

        #endregion

    }

    /// <summary>
    /// Simple proxy class for the control to display only specific properties.
    /// Not as sophisticated as the property-bag of the full Netron-control
    /// but does the job in this simple context.
    /// </summary>
    public class Proxy
    {
        #region Fields
        private GraphControl site;
        #endregion

        #region Constructor
        public Proxy(GraphControl site)
        { this.site = site; }
        #endregion

        #region Methods
        [Browsable(false)]
        public GraphControl Site
        {
            get { return site; }
            set { site = value; }
        }
        [Browsable(true), Description("The backcolor of the canvas"), Category("Layout")]
        public Color BackColor
        {
            get { return this.site.BackColor; }
            set { this.site.BackColor = value; }
        }

        [Browsable(true), Description("Gets or sets whether the grid is shown"), Category("Layout")]
        public bool ShowGrid
        {
            get { return this.site.ShowGrid; }
            set { this.site.ShowGrid = value; }
        }
        #endregion
    }
}

