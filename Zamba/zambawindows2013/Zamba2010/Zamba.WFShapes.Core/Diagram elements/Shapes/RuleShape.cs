using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.WFShapes;

namespace Zamba.WFShapes
{
    public class RuleShape : ComplexShapeBase, ISerializable
    {

        #region Fields
        ExpansionIcon xicon;
        private Pen pen;
        protected Connector cBottom, cLeft, cRight, cTop;
        private int bodyHeight = 90;
        private int listHeight = 20;
        private BodyType bodyType = BodyType.FreeText;
        private string title;
        private string subTitle;
        private string bodyText;
        private bool collapsed = true;
        private Font boldFont;
        private RuleShapeItemCollection mList = new RuleShapeItemCollection();
        private string freeText = string.Empty;
        private Boolean showTasks = false;

        private LabelMaterial textMaterial;

        private CollectionBase<IShapeMaterial> listMaterials = new CollectionBase<IShapeMaterial>();

        private GraphicsPath path, gradientPath;

        private Region darkRegion, gradientRegion;

        private Brush gradientBrush;
        StringFormat sf = StringFormat.GenericTypographic;

        #endregion

        #region Constructors


        public RuleShape(Int64 Id, IModel model)
            : base(model)
        {
            Init(Id);
        }
        protected RuleShape(SerializationInfo info, StreamingContext context)
            : base(null)
        {
        }

        private RuleShape()
            : base()
        {
        }
        public RuleShape(Int64 Id)
            : base()
        {
            Init(Id);
        }
        public override void Init(Int64 Id)
        {
            base.Init(Id);
            this.Id = Id;
            this.Name = "Rule Shape";

            if (RuleRow != null)
            {
                title = RuleRow.Name;
                if (!RuleRow._Class.ToLower().Equals("ifbranch"))
                {
                    subTitle = RuleRow._Class;
                }
            }
            this.Resizable = false;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            //the initial size
            //Transform(0, 0, 200, 50);
            //the bold font
            boldFont = new Font(ArtPallet.DefaultFont, FontStyle.Bold);
            //handle the click to enable the collapse/expand icon
            this.OnMouseDown += new EventHandler<EntityMouseEventArgs>(RuleShape_Click);

            mList.OnItemAdded += new EventHandler<CollectionEventArgs<RuleShapeItem>>(mList_OnItemAdded);

            #region The xpand icon material
            if (RuleRow == null || !RuleRow._Class.ToLower().Equals("ifbranch"))
            {
                //we use a custom version of the ClickableIconMaterial
                xicon = new ExpansionIcon();
                //Rectangle rec = new Rectangle(new Point(Rectangle.Right -20, Rectangle.Y + 5), xicon.Icon.Size);            
                xicon.Transform(new Rectangle(new Point(Rectangle.Right - 17, Rectangle.Y + 7), xicon.Icon.Size));
                xicon.Gliding = false;
                Children.Add(xicon);
            }
            #endregion

            #region The free text
            textMaterial = new LabelMaterial();
            //textMaterial.Transform(new Rectangle(Rectangle.X + 5, Rectangle.Y + 25, Rectangle.Width - 10, bodyHeight));
            textMaterial.Transform(new Rectangle(Location.X + 5, Location.Y + Height / 2, Width, Height));
            //todo: que getquotation traiga la descripcion de la etapa y otros datos
            textMaterial.Text = GetQuotation();
            textMaterial.Visible = false;
            Children.Add(textMaterial);

            #endregion

            #region The icon material
            //Obtengo el icono
            //ClickableIconMaterial icon = new ClickableIconMaterial("Resources.RuleShape.png");
            if (RuleRow != null)
            {
                string NewPath;
                NewPath = null;
                NewPath = WfShapesBusiness.GetIconsPathString(RuleRow.Id.ToString());
                if (NewPath != null)
                {
                    ClickableIconMaterial icono = new ClickableIconMaterial();//Image.FromFile(NewPath);

                    if (System.IO.File.Exists(NewPath) == true)
                    {
                        icono.Icon = (Bitmap)Image.FromFile(NewPath);
                    }

                    if (icono.Icon != null)
                    {
                        //throw new InconsistencyException("The icon resource of the class shape could not be found.");
                        Size size = new Size(18, 18);
                        //Lo transformo para adaptarlo al shape
                        icono.Transform(new Rectangle(new Point(Rectangle.X + 5, Rectangle.Y + 5), size));
                        icono.Gliding = false;
                        //Lo agrego
                        Children.Add(icono);
                    }
                }
            }
            #endregion


            #region Connectors
            cTop = new Connector(new Point((int)(Rectangle.Left + Rectangle.Width / 2), Rectangle.Top), Model);
            cTop.Name = "Top connector";
            cTop.Parent = this;
            Connectors.Add(cTop);

            cRight = new Connector(new Point(Rectangle.Right, (int)(Rectangle.Top + Rectangle.Height / 2)), Model);
            cRight.Name = "Right connector";
            cRight.Parent = this;
            Connectors.Add(cRight);

            cBottom = new Connector(new Point((int)(Rectangle.Left + Rectangle.Width / 2), Rectangle.Bottom), Model);
            cBottom.Name = "Bottom connector";
            cBottom.Parent = this;
            Connectors.Add(cBottom);

            cLeft = new Connector(new Point(Rectangle.Left, (int)(Rectangle.Top + Rectangle.Height / 2)), Model);
            cLeft.Name = "Left connector";
            cLeft.Parent = this;
            Connectors.Add(cLeft);
            #endregion

        }

        #endregion

        #region Properties
        public override string EntityName
        {
            get
            {
                return "Rule Shape";
            }
        }
        public BodyType BodyType
        {
            get
            {
                return bodyType;
            }
            set
            {
                bodyType = value;
                this.UpdateBody();
            }
        }
        public void Collapse()
        {
            this.collapsed = true;
            this.xicon.Collapsed = true;
            UpdateBody();
            this.Invalidate();
        }

        public void Expand()
        {
            collapsed = false;
            this.xicon.Collapsed = false;
            UpdateBody();
            this.Invalidate();
        }

        public bool Collapsed
        {
            get
            {
                return collapsed;
            }
            //set{collapsed = value;}
        }

        public RuleShapeItemCollection List
        {
            get
            {
                return this.mList;
            }
        }
        public string SubTitle
        {
            get
            {
                return subTitle;
            }
            set
            {
                subTitle = value;
                this.Invalidate();
            }
        }

        public string FreeText
        {
            get
            {
                return freeText;
            }
            set
            {
                freeText = value;
                UpdateBody();
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public Boolean ShowTasks
        {
            get
            {
                return showTasks;
            }
            set
            {
                showTasks = value;
            }
        }
        #endregion

        #region Methods

        private string GetQuotation()
        {
            return "Descripcion de la Etapa";
        }

        public override bool Hit(System.Drawing.Point p)
        {
            Rectangle r = new Rectangle(p, new Size(5, 5));
            return Rectangle.Contains(r);
        }

        public override void Paint(Graphics g)
        {
            #region Set the quality of the drawing
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            #region Artist's material
            if (IsSelected || Hovered)
                pen = ArtPallet.HighlightPen;
            else
                pen = Pens.Gray;
            #endregion

            #region Shape's shadow and container
            path = new GraphicsPath();
            path.AddArc(Rectangle.X, Rectangle.Y, 20, 20, -180, 90);
            path.AddLine(Rectangle.X + 10, Rectangle.Y, Rectangle.X + Rectangle.Width - 10, Rectangle.Y);
            path.AddArc(Rectangle.X + Rectangle.Width - 20, Rectangle.Y, 20, 20, -90, 90);
            path.AddLine(Rectangle.X + Rectangle.Width, Rectangle.Y + 10, Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height - 10);
            path.AddArc(Rectangle.X + Rectangle.Width - 20, Rectangle.Y + Rectangle.Height - 20, 20, 20, 0, 90);
            path.AddLine(Rectangle.X + Rectangle.Width - 10, Rectangle.Y + Rectangle.Height, Rectangle.X + 10, Rectangle.Y + Rectangle.Height);
            path.AddArc(Rectangle.X, Rectangle.Y + Rectangle.Height - 20, 20, 20, 90, 90);
            path.AddLine(Rectangle.X, Rectangle.Y + Rectangle.Height - 10, Rectangle.X, Rectangle.Y + 10);
            //shadow
            darkRegion = new Region(path);
            darkRegion.Translate(5, 5);
            g.FillRegion(ArtPallet.ShadowBrush, darkRegion);

            //background
            g.FillPath(Brushes.White, path);

            #endregion

            #region the header
            if (collapsed)
            {
                //paint the gradient
                gradientBrush = new LinearGradientBrush(new Point(((int)(Rectangle.X)), ((int)(Rectangle.Y))), new Point(((int)(Rectangle.X + Rectangle.Width)), ((int)(Rectangle.Y))), ShapeColor, Color.White);
                gradientRegion = new Region(path);
                g.FillRegion(gradientBrush, gradientRegion);
            }
            else
            {
                gradientPath = new GraphicsPath();
                gradientPath.AddArc(Rectangle.X + 1, Rectangle.Y + 1, 18, 18, -180, 90);
                gradientPath.AddLine(Rectangle.X + 11, Rectangle.Y + 1, Rectangle.X + Rectangle.Width - 11, Rectangle.Y + 1);
                gradientPath.AddArc(Rectangle.X + Rectangle.Width - 19, Rectangle.Y + 1, 18, 18, -90, 90);
                gradientPath.AddLine(Rectangle.X + Rectangle.Width - 1, Rectangle.Y + 50, Rectangle.X + 1, Rectangle.Y + 50);
                //gradient
                gradientBrush = new LinearGradientBrush(new Point(((int)(Rectangle.X)), ((int)(Rectangle.Y))), new Point(((int)(Rectangle.X + Rectangle.Width)), ((int)(Rectangle.Y))), ShapeColor, Color.White);
                gradientRegion = new Region(gradientPath);
                g.FillRegion(gradientBrush, gradientRegion);
            }
            #endregion

            #region Border
            //the border
            g.DrawPath(pen, path);
            #endregion

            #region Text and body

            g.DrawString(title, boldFont, Brushes.Black, new Rectangle(Rectangle.X + 23, Rectangle.Y + 5, Rectangle.Width - 45, 27));
            g.DrawString(subTitle, ArtPallet.DefaultFont, Brushes.Black, new RectangleF(Rectangle.X + 20, Rectangle.Y + 35, Rectangle.Width - 10, 30), sf);

            if (!collapsed && bodyType == BodyType.List)
            {

                if (bodyText.Length == 0)
                    g.DrawString("No items were added.", ArtPallet.DefaultFont, Brushes.Black, new RectangleF(Rectangle.X + 5, Rectangle.Y + 55, Rectangle.Width - 10, bodyHeight), sf);
                else
                    g.DrawString(bodyText, ArtPallet.DefaultFont, Brushes.Black, new RectangleF(Rectangle.X + 35, Rectangle.Y + 50 + 10, Rectangle.Width - 10, listHeight), sf);

                //for(int k = 0; k<mList.Count; k++)
                //g.DrawImage(new Bitmap(this.GetType(), "Resources.SPP.ico"),Rectangle.X + 18, Rectangle.Y + 55 + 15 + k*13);
            }
            #endregion

            #region The material
            foreach (IPaintable material in Children)
            {
                material.Paint(g);
            }
            #endregion

            #region The connectors
            //the connectors
            for (int k = 0; k < Connectors.Count; k++)
            {
                Connectors[k].Paint(g);
            }
            #endregion

        }

        private void UpdateBody()
        {
            if (collapsed)
            {
                Transform(Rectangle.X, Rectangle.Y, this.ZambaObject.Width, this.ZambaObject.Height);

                textMaterial.Visible = false;
                foreach (IShapeMaterial material in listMaterials)
                    material.Visible = false;
                return;
            }
            else
            {
                Transform(Rectangle.X, Rectangle.Y, this.ZambaObject.Width, this.ZambaObject.Height + 60);
            }
            if (BodyType == BodyType.FreeText)
            {
                textMaterial.Visible = true;
                textMaterial.Text = "Regla: " + RuleRow._Class;
                foreach (IShapeMaterial material in listMaterials)
                    material.Visible = false;
            }
            else
            {
                textMaterial.Visible = false;
                foreach (IShapeMaterial material in listMaterials)
                    material.Visible = true;
            }
        }

        public Cursor GetCursor(PointF p)
        {
            RectangleF collapseRect = new RectangleF(((int)(Rectangle.Right - 20)), ((int)(Rectangle.Y + 5)), 16, 16);

            if (collapseRect.Contains(p))
            {
                return Cursors.Hand;
            }
            collapseRect = new RectangleF(Rectangle.X + 5, Rectangle.Y + 55, 18, 18);
            if (collapseRect.Contains(p))
            {
                return Cursors.Hand;
            }



            if (collapseRect.Contains(p))
            {
                return Cursors.Hand;
            }

            //fall back to the default
            return Cursors.Default;//base.GetCursor(p);
        }

        private void mList_OnItemAdded(object sender, CollectionEventArgs<RuleShapeItem> e)
        {
            if (this.bodyType == BodyType.List)
                this.UpdateBody();
        }

        #region Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {


        }

        #endregion

        #region Mouse handler
        private void RuleShape_Click(object shape, EntityMouseEventArgs e)
        {
            ClickHandler(e);
        }
        private void ClickHandler(EntityMouseEventArgs e)
        {
            #region Test the main expansion icon
            RectangleF collapseRect = new RectangleF(((int)(Rectangle.Right - 20)), ((int)(Rectangle.Y + 5)), 16, 16);

            if (collapseRect.Contains(e.X, e.Y))
            {
                //switch the main state
                if (collapsed) //expand
                {
                    collapsed = false;
                    cBottom.Move(0, Rectangle.Top + bodyHeight + 50 + 10 - cBottom.Point.Y);
                    cLeft.Move(0, Rectangle.Top + bodyHeight / 2 + 30 - cLeft.Point.Y);
                    cRight.Move(0, Rectangle.Top + bodyHeight / 2 + 30 - cRight.Point.Y);
                    //cBottom.Point = new Point((int) (Rectangle.Left+Rectangle.Width/2),Rectangle.Top + rec+70);

                }
                else //collapse
                {
                    collapsed = true;			//set main collapsed off
                    //cBottom.Point = new Point((int) (Rectangle.Left+Rectangle.Width/2),Rectangle.Top+50);	
                    cBottom.Move(0, Rectangle.Top + 50 - cBottom.Point.Y);
                    cLeft.Move(0, Rectangle.Top + 25 - cLeft.Point.Y);
                    cRight.Move(0, Rectangle.Top + 25 - cRight.Point.Y);
                }



                this.Invalidate();
                return;
            }

            #endregion


        }

        #endregion
        #endregion

        #region Custom ClickableIconMaterial for the xpansion icon
        private class ExpansionIcon : ClickableIconMaterial
        {
            public override void MouseDown(MouseEventArgs e)
            {
                base.MouseDown(e);
                if (e.Clicks == 1)
                {
                    if ((this.Shape as RuleShape).Collapsed)
                    {
                        (this.Shape as RuleShape).Expand();
                    }
                    else
                    {
                        (this.Shape as RuleShape).Collapse();
                    }
                }

            }
            private Bitmap downBmp;
            private Bitmap upBmp;

            private bool mCollapsed;
            public bool Collapsed
            {
                get
                {
                    return mCollapsed;
                }
                set
                {
                    if (value)
                        this.Icon = downBmp;
                    else
                        this.Icon = upBmp;
                    //this.Icon = rigthb  downBmp;
                    mCollapsed = value;
                }
            }

            public ExpansionIcon()
                : base()
            {
                //fetch the two bitmaps
                try
                {
                    downBmp = GetBitmap("Resources.down.ico");
                    upBmp = GetBitmap("Resources.up.ico");
                    this.Icon = downBmp;
                }
                catch (Exception exc)
                {
                    throw new InconsistencyException("The necessary resource could not be found.", exc);
                }

            }

        }
        #endregion
    }

    #region Utility classes

    public class RuleShapeItemCollection : Zamba.WFShapes.CollectionBase<RuleShapeItem>, IEnumerable<RuleShapeItem>
    {


    }





    public class RuleShapeItem
    {
        #region Fields
        private string mText;
        private Bitmap mIcon;
        #endregion

        #region Properties
        public string Text
        {
            get
            {
                return mText;
            }
            set
            {
                mText = value;
            }
        }

        public Bitmap Icon
        {
            get
            {
                return mIcon;
            }
            set
            {
                mIcon = value;
            }
        }

        #endregion

        public RuleShapeItem()
        {
        }

        public RuleShapeItem(string text)
        {
            this.mText = text;
        }

        public RuleShapeItem(string text, Bitmap icon)
            : this(text)
        {
            this.mIcon = icon;
        }

    }
    public class RuleShapeEventArgs : EventArgs
    {
        private RuleShapeItem item;

        public RuleShapeItem Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }

        public RuleShapeEventArgs(RuleShapeItem item)
        {
            this.item = item;
        }
    }
    #endregion
}
