using System;

namespace Zamba.WFShapes
{
	public interface ICompoundCommand : ICommand
	{
        CollectionBase<ICommand> Commands
        {
            get;
            set;
        }


	}
}
