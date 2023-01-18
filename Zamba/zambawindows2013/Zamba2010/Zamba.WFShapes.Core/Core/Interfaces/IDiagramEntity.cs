using System;
using System.Drawing;
using System.Windows.Forms;
using Zamba.Core;
namespace Zamba.WFShapes
{
	public interface IDiagramEntity : IPaintable, IServiceProvider
	{

		#region Events
		event EventHandler<EntityEventArgs> OnEntityChange;
        event EventHandler<EntityEventArgs> OnEntitySelect;
        event EventHandler<EntityEventArgs> OnClick;
		#endregion

		#region Properties
		bool IsSelected {get; set;}
		IModel Model {get; set;}
		object Parent {get; set;}
        Int64 Id { get; set;}
        string Name { get; set;}
		bool Hovered {get; set;}
		object Tag {get; set;}
        string EntityName { get;}
        int SceneIndex { get; set;}
        IGroup Group { get; set;}
        bool Resizable { get;set;}
        IZambaCore ZambaObject { get;set;}
        Int64 WFStepId { get; set; }
        Int64 WorkflowId { get; set; }
        DsRules.WFRulesRow RuleRow { get; set; }
        ContextMenuStrip contextMenu { get;set;}
      
#endregion


		#region Methods
        MenuItem[] ShapeMenu();
        void RaiseOnClick(EntityEventArgs e);
        void RaiseOnMouseDown(EntityMouseEventArgs e);		
		bool Hit(Point p);
		void Invalidate();		
		void Invalidate(RectangleF rectangle);
		void Move(Point p);
        void Init(Int64 Id);
		#endregion
	}
}
