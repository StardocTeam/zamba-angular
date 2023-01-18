using System;

namespace Zamba.WFShapes
{
	public interface IUndoSupport
	{
		void Undo();
		void Redo();
	}
}
