using System;
using System.Drawing;
using System.Windows.Forms;
namespace Zamba.WFShapes
{
	public abstract class ConnectionBase : DiagramEntityBase, IConnection
	{

		#region Fields

		private IConnector mFrom;
		private IConnector mTo;
        private long mId1;
        private long mId2;
        protected Label mlabel = new Label();

		#endregion

		#region Properties
		public IConnector From
		{
			get{return mFrom;}
			set{mFrom = value;}
		}
		public IConnector To
		{
			get{return mTo;}
			set{mTo = value;}
		}
        public long Id1
        {
            get { return mId1; }
            set { mId1 = value; }
        }
        public long Id2
        {
            get { return mId2; }
            set { mId2 = value; }
        }
        public Label label
        {
            get { return this.mlabel; }
            set { mlabel = value; }
        }

		#endregion

		#region Constructor
		protected ConnectionBase(IModel site) : base(site)
		{
		
		}

        protected ConnectionBase(Point from, Point to) : base()
        {
            this.mFrom = new Connector(from);
            this.mFrom.Parent = this;
            this.mTo = new Connector(to);
            this.mTo.Parent = this;
        }
        //protected ConnectionBase(long Id1, long Id2) : base()
        //{
        //    this.Id1 = new long(Id1);
        //    this.Id2 = new long(Id2);
        //}
        #endregion

        #region Methods
        public override void Paint(Graphics g)
        {
            if (g == null)
                throw new ArgumentNullException("The Graphics object is 'null'");
            From.Paint(g);
            To.Paint(g);
        }
        public override MenuItem[] ShapeMenu()
        {
            return null;
        }
        #endregion




    }
}
