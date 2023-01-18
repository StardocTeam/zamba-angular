using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace PrintControl
{
    public partial class ControlPrint : PrintDocument
    {
        private Control m_ctrl;

        private bool stretch;

        /// <summary>
        /// Set true to stretch the control to fill a single printed page
        /// </summary>
        public bool StretchControl
        {
            set { stretch = value; }
            get { return stretch; }
        }

        private int width;
        private int height;
        private bool scale;
        /// <summary>
        /// The width of the control to print. You can change this value if the automatically 
        /// calculated width does not fit all the elements of the control
        /// </summary>
        public int PrintWidth
        {
            set { width = value; }
            get { return width; }
        }

        public bool ScaleControl
        {
            get { return scale; }
            set { scale = value; }
        }
        /// <summary>
        /// The height of the control to print. You can change this value if the automatically
        /// calculated height does not fit all the elements of the control
        /// </summary>
        public int PrintHeight
        {
            set { height = value; }
            get { return height; }
        }

        private int inbetween;
        private Bitmap Image;
        /// <summary>
        /// The area to be reprinted between pages if there are more than one
        /// pages to be printed, to prevent data loss
        /// </summary>
        public int RepeatArea
        {
            get { return inbetween; }
            set { inbetween = value; }
        }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public ControlPrint()
        {
            InitializeComponent();
            m_ctrl = new Control();
            stretch = false;
            width = m_ctrl.Width;
            height = m_ctrl.Height;
            inbetween = 10;
        }

        /// <summary>
        /// Intialize the component with the selected control
        /// </summary>
        /// <param name="print">The control to printed</param>
        public ControlPrint(Control print)
        {
            InitializeComponent();
            SetControl(print);
            stretch = false;
            inbetween = 10;
        }

        /// <summary>
        /// Initialize the component with the selected control specifying
        /// whether to stretch or not
        /// </summary>
        /// <param name="print">The control to be printed</param>
        /// <param name="Stretch">Set true to stretch the control to fill a single printed page</param>
        public ControlPrint(Control print, bool Str)
        {
            InitializeComponent();
            SetControl(print);
            stretch = Str;
            inbetween = 10;
        }

        /// <summary>
        /// Initialize the component with the selected control specifying
        /// specific width and height
        /// </summary>
        /// <param name="print">The control to be printed</param>
        /// <param name="Width">Printed width</param>
        /// <param name="Height">Printed height</param>
        public ControlPrint(Control print, int Width, int Height)
        {
            InitializeComponent();
            SetControl(print);
            stretch = false;
            width = Width;
            height = Height;
            inbetween = 10;
        }

        /// <summary>
        /// Intialize the component with a specific container, Insignificant
        /// </summary>
        /// <param name="container"></param>
        private ControlPrint(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            m_ctrl = new Control();
            stretch = false;
            inbetween = 10;
        }

        /// <summary>
        /// Set the control to be printed
        /// </summary>
        /// <param name="print">The control you wish to print</param>
        public void SetControl(Control print)
        {
            m_ctrl = print;
            Size size = m_ctrl.GetPreferredSize(Size.Empty);
            width = size.Width;
            height = size.Height;
            printedheight = 0;
            printedwidth = 0;

            DockStyle OldDock = m_ctrl.Dock;
            Size NewSize = new Size(width, height);
            Size OldSize = new Size(m_ctrl.Width, m_ctrl.Height);
            Control parent = m_ctrl;
            List<Control> Parents = new List<Control>();
            List<Size> OldSizes = new List<Size>();

            m_ctrl.Dock = DockStyle.None;
            m_ctrl.Size = NewSize;

            //enumerate the parents
            while (parent.Parent != null)
            {
                Parents.Add(parent.Parent);
                OldSizes.Add(parent.Parent.Size);
                parent = parent.Parent;
            }
            //Make sure that the size changes otherwise resize the parents
            while (m_ctrl.Size == OldSize)
            {
                foreach (Control c in Parents)
                {
                    c.Size = new Size(c.Width + 100, c.Height + 100);
                }
                m_ctrl.Size = NewSize;
            }

            Image = new Bitmap(width, height);

            m_ctrl.DrawToBitmap(Image, new Rectangle(0, 0, width, height));

            //Restore the control's state
            for (int i = 0; i < Parents.Count; i++)
                Parents[i].Size = OldSizes[i];
            m_ctrl.Size = new Size(OldSize.Width, OldSize.Height);
            m_ctrl.Dock = OldDock;
        }

        /// <summary>
        /// Set the control with a specified height & width
        /// </summary>
        /// <param name="print">The control to be printed</param>
        /// <param name="Width">Control's width</param>
        /// <param name="Height">Control's height</param>
        public void SetControl(Control print, int Width, int Height)
        {
            m_ctrl = print;
            width = Width;
            height = Height;
            printedheight = 0;
            printedwidth = 0;
        }

        public ArrayList Cols
        {
            get { return ColsToPrint; }
            set { ColsToPrint = value; }
        }
        public ArrayList Rows
        {
            get { return RowsToPrint; }
            set { RowsToPrint = value; }
        }

        private int printedheight;
        private int printedwidth;
        private bool IsLarge;
        private bool IsLong;
        private ArrayList ColsToPrint;
        private ArrayList RowsToPrint;
        private int CountWidthPages = 0;
        private int CountHeightPages = 0;
        private void ControlPrint_PrintPage(object sender, PrintPageEventArgs e)
        {
            int printwidth = width;
            int printheight = height;

            if (this.scale)
            {
                //print dimentions will be according to page size and margins

                if (printwidth > e.MarginBounds.Width)
                {
                    printheight = (int)(((float)e.MarginBounds.Width / (float)printwidth) * printheight);
                    printwidth = e.MarginBounds.Width;
                }
            }

            IsLong = (printheight - printedheight > e.MarginBounds.Height) || Rows.Count-1 > CountHeightPages;
            IsLarge = (printwidth - printedwidth > e.MarginBounds.Width) || Cols.Count-1 > CountWidthPages ;

            //if too long or too large, we will need more papers
            if (IsLong || IsLarge  && !stretch)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;

            //Will we stretch the image?
            if (stretch)
            {
                GraphicsUnit gp = GraphicsUnit.Point;
                e.Graphics.DrawImage(Image, e.MarginBounds, Image.GetBounds(ref gp), gp);
                e.HasMorePages = false;
            }
            else
            {
                float ScaleF = (float)height / (float)printheight;
                float ScaleW = (float)width / (float)printwidth;

                printheight -= printedheight;
                printwidth -= printedwidth;

                if (Rows != null && Rows.Count > CountHeightPages)
                    printheight = (int)Rows[CountHeightPages];
                else
                    if (printheight > e.MarginBounds.Height)
                        printheight = e.MarginBounds.Height;

                if (Cols != null && Cols.Count > CountWidthPages)
                    printwidth = (int)Cols[CountWidthPages];
                else
                    if (printwidth > e.MarginBounds.Width)
                        printwidth = e.MarginBounds.Width;

                Rectangle rect = new Rectangle((int)(printedwidth * ScaleW), (int)(printedheight * ScaleF), (int)(printwidth * ScaleW), (int)(printheight * ScaleF));
                Bitmap b2 = Image.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);
                e.Graphics.DrawImage(b2, new Rectangle((e.PageBounds.Width/2) - (printwidth/2), e.MarginBounds.Top, printwidth, printheight));
            }
            if (e.HasMorePages)
            {
                //Change the printed height, and don't forget the RepeatArea
                if (Cols != null && Cols.Count > CountWidthPages)
                    printedwidth += (int)Cols[CountWidthPages];
                else
                    printedwidth += e.MarginBounds.Width;

                CountWidthPages++;

                if (!IsLarge && Cols.Count == CountWidthPages)
                {
                    printedheight += (int)Rows[CountHeightPages];
                    printedwidth = 0;
                    CountWidthPages = 0;
                    CountHeightPages++;
                }
            }
            else
            {
                printedheight = 0;
                printedwidth = 0;
                CountWidthPages = 0;
                CountHeightPages = 0;
            }
        }

        /// <summary>
        /// Draw the control fully to a bitmap & return it
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBitmap()
        {
            //Get old states
            Size NewSize = new Size(width, height);
            DockStyle OldDock = m_ctrl.Dock;
            Size OldSize = new Size(m_ctrl.Width, m_ctrl.Height);
            Control parent = m_ctrl;
            List<Control> Parents = new List<Control>();
            List<Size> OldSizes = new List<Size>();
            while (parent.Parent != null)
            {
                Parents.Add(parent.Parent);
                OldSizes.Add(parent.Parent.Size);
                parent = parent.Parent;
            }
            m_ctrl.Dock = DockStyle.None;
            m_ctrl.Size = NewSize;
            while (m_ctrl.Size == OldSize)
            {
                foreach (Control c in Parents)
                {
                    c.Size = new Size(c.Width + 100, c.Height + 100);
                }
                m_ctrl.Size = NewSize;
            }
            Bitmap b = new Bitmap(width, height);
            //Draw
            m_ctrl.DrawToBitmap(b, new Rectangle(0, 0, width, height));
            //Restore states
            for (int i = 0; i < Parents.Count; i++)
                Parents[i].Size = OldSizes[i];
            m_ctrl.Size = new Size(OldSize.Width, OldSize.Height);
            m_ctrl.Dock = OldDock;
            return b;
        }

        private List<TreeNode> Nodes; // All enumerated nodes are here
        private void EnumNodes(TreeNode TN)
        {
            if (!Nodes.Contains(TN) && (TN.Parent == null || (TN.Parent != null && TN.Parent.IsExpanded)))
                Nodes.Add(TN);
            //Enum all subnodes of the current node
            foreach (TreeNode R in TN.Nodes)
                EnumNodes(R);
        }
        
        /// <summary>
        /// Return the best size that fits the control
        /// </summary>
        /// <returns></returns>
        public Size CalculateSize()
        {
            //Identify the control's type
            if (m_ctrl.GetType().AssemblyQualifiedName.IndexOf("TreeView") >= 0)
            {
                if (((TreeView)m_ctrl).Nodes.Count == 0)
                {
                    //if no elemts return 1X1 to avoid exceptions
                    return new Size(1, 1);
                }
                else
                {
                    int TempW = 0, TempH = 0;
                    Nodes = new List<TreeNode>();
                    foreach (TreeNode T in ((TreeView)m_ctrl).Nodes)
                        EnumNodes(T);
                    foreach (TreeNode N in Nodes)
                    {
                        if (TempW < N.Bounds.Right)
                            TempW = N.Bounds.Right;
                    }
                    TempH = Nodes.Count * ((TreeView)m_ctrl).ItemHeight;
                    return new Size(TempW + 30, TempH + 30);
                }
            }
            else if (m_ctrl.GetType().AssemblyQualifiedName.IndexOf("ListView") >= 0)
            {
                if (((ListView)m_ctrl).Items.Count > 0)
                {
                    //This will work if the listview is in details mode
                    ((ListView)m_ctrl).Items[0].EnsureVisible();
                    int tempwidth = ((ListView)m_ctrl).Items[0].Bounds.Width;
                    int tempheight = ((ListView)m_ctrl).Items[((ListView)m_ctrl).Items.Count - 1].Bounds.Bottom;
                    //This is for other modes
                    foreach (ListViewItem i in ((ListView)m_ctrl).Items)
                    {
                        if (tempwidth < i.Bounds.Right)
                            tempwidth = i.Bounds.Right;
                        if (tempheight < i.Bounds.Bottom)
                            tempheight = i.Bounds.Bottom;
                    }
                    return new Size(tempwidth + 30, tempheight + 30);
                }
                else
                    return new Size(1, 1);
            }
            else if (m_ctrl.Controls.Count > 0)
            {
                //Just in case the form didn't calculate its PreferredSize properly
                int tempwidth = 1;
                int tempheight = 1;
                foreach (Control c in m_ctrl.Controls)
                {
                    if (c.Bounds.Right > tempwidth)
                        tempwidth = c.Bounds.Right;
                    if (c.Bounds.Bottom > tempheight)
                        tempheight = c.Bounds.Bottom;
                }
                return new Size(tempwidth, tempheight);
            }
            else
           
                return new Size(m_ctrl.PreferredSize.Width, m_ctrl.PreferredSize.Height);
           
            return Size.Empty;
        }

        /// <summary>
        /// Apply the best size that fits the control
        /// </summary>
        public void ApplyBestSize()
        {
            Size temp = CalculateSize();
            width = temp.Width;
            height = temp.Height;
        }
    }
}
