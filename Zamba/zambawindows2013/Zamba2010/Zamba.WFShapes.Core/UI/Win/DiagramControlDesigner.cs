using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.IO;
using System.Reflection;

namespace Zamba.WFShapes.Win
{
	/// <summary>
	/// Control designer of the graph-control
	/// </summary>
    internal class DiagramControlDesigner : ControlDesigner
	{
		

		#region Properties

        /// <summary>
        /// the AnotherOne field
        /// </summary>
        private int mAnotherOne;
        /// <summary>
        /// Gets or sets the AnotherOne
        /// </summary>
        [Browsable(true)]
        public int AnotherOne
        {
            get { return mAnotherOne; }
            set { mAnotherOne = value; }
        }
	
		/// <summary>
		/// Gets the verbs of the control
		/// </summary>
		public override System.ComponentModel.Design.DesignerVerbCollection Verbs
		{
			get
			{		
				DesignerVerbCollection col=new DesignerVerbCollection();
				col.Add(new DesignerVerb("About",new EventHandler(About)));
				col.Add(new DesignerVerb("Help",new EventHandler(ZambaSite)));
				return col;
			}
		}
		#endregion

		#region Constructor
		public DiagramControlDesigner()
		{
            //Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Zamba.WFShapes.UI.AboutSplash.jpg");
					
            //bmp= Bitmap.FromStream(stream) as Bitmap;
            //stream.Close();
            //stream=null;

		}
		#endregion

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create action list collection
                DesignerActionListCollection actionLists = new DesignerActionListCollection();

                // Add custom action list
                actionLists.Add(new DiagramControlDesignerActionList(this.Component));

                // Return to the designer action service
                return actionLists;
            }
        }

		public override void Initialize(System.ComponentModel.IComponent component)
		{
			base.Initialize (component);
			(component as Control).AllowDrop = false;
			(component as DiagramControl).BackColor = Color.White;
			//(component as GraphControl).EnableContextMenu = true;
		}
		

		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			base.OnPaintAdornments (pe);
			System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
			
			//pe.Graphics.DrawImage(bmp,10,100,530,228);
			
		}


		

		
		private void About(object sender, EventArgs e)
		{
			//Form frm = new Zamba.GraphLib.AboutForm(true);
			//frm.ShowDialog();
		}

		private void ZambaSite(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://Zamba.sf.net");
		}
	
	}
}
