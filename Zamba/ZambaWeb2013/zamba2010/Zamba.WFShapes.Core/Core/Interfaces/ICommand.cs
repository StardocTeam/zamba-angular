using System;

namespace Zamba.WFShapes
{
	public interface ICommand
	{
		void Undo();
		void Redo();
		string Text{get; set;}
	}
}
