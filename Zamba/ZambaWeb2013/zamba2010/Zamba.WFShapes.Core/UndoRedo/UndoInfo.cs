using System;

namespace Zamba.WFShapes
{
	public  class CommandInfo
	{
		private ICommand  mCommand;
		private IUndoSupport mHandler;

		public ICommand Command
		{
			get{return mCommand;}
			set{mCommand = value;}
		}

		public IUndoSupport Handler
		{
			get{return mHandler;}
			set{mHandler = value;}
		}

		public CommandInfo()
		{  			
		}
	}
}
