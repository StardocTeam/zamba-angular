using Zamba.Core.Enumerators;
using Zamba.AppBlock;
using Zamba.Core;
using System;
using System.Collections;
using System.Windows.Forms;
using Zamba.Core.Enumerators;
using Zamba.Core.WF.WF;
using System.Data;
using Telerik.WinControls.UI;
using static Telerik.WinControls.UI.RadTreeView;
using System.Drawing;

public class WFTreeDiagram : ZControl
{

	#region " Windows Form Designer generated code "

	public WFTreeDiagram(IWFNodeHelper WFNodeHelper)
		: base()
	{
		this.NodeHelper = WFNodeHelper;
		//This call is required by the Windows Form Designer.
		InitializeComponent();

		//Add any initialization after the InitializeComponent() call
	}
	/// <summary>
	/// Actualiza para la regla do design el nodo cuando se convierte la regla.
	/// </summary>
	/// <history>
	/// [Sebastian] 21-10-2009 CREATED
	/// </history>
	/// <remarks></remarks>

	//Form overrides dispose to clean up the component list.
	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing)
			{
				if ((components != null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		catch
		{
		}
	}

	//Required by the Windows Form Designer

	private System.ComponentModel.IContainer components;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.  
	//Do not modify it using the code editor.
	internal System.Windows.Forms.ContextMenu TreeViewContextMenu;
	internal RadTreeView TreeView1;
	internal System.Windows.Forms.MenuItem MnuLine;
	internal System.Windows.Forms.MenuItem mnuSeleccionar;
	internal System.Windows.Forms.MenuItem mnuExpadirRegla;
	internal ZToolBar Toolbar;
	internal ToolStripTextBox txtRuleId;
	internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
		this.TreeViewContextMenu = new System.Windows.Forms.ContextMenu();
		this.MnuLine = new System.Windows.Forms.MenuItem();
		this.mnuSeleccionar = new System.Windows.Forms.MenuItem();
		this.mnuExpadirRegla = new System.Windows.Forms.MenuItem();
		this.TreeView1 = new RadTreeView();
		this.Toolbar = new ZToolBar();
		this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
		this.txtRuleId = new ToolStripTextBox();
		this.Toolbar.SuspendLayout();
		this.SuspendLayout();
		// 
		// TreeViewContextMenu
		// 
		this.TreeViewContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.MnuLine,
			this.mnuSeleccionar,
			this.mnuExpadirRegla});
		// 
		// MnuLine
		// 
		this.MnuLine.Index = 0;
		this.MnuLine.Text = "-";
		// 
		// mnuSeleccionar
		// 
		this.mnuSeleccionar.Index = 1;
		this.mnuSeleccionar.Text = "Seleccionar";
		// 
		// mnuExpadirRegla
		// 
		this.mnuExpadirRegla.Index = 2;
		this.mnuExpadirRegla.Text = "Expandir todo";
		// 
		// TreeView1
		// 
		this.TreeView1.AllowDrop = true;
		this.TreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
		| System.Windows.Forms.AnchorStyles.Left)
		| System.Windows.Forms.AnchorStyles.Right)));
		this.TreeView1.BackColor = System.Drawing.Color.White;
		//' this.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.TreeView1.ContextMenu = this.TreeViewContextMenu;
		this.TreeView1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.TreeView1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76);
		this.TreeView1.FullRowSelect = true;
		this.TreeView1.HideSelection = false;
		//' this.TreeView1.Indent = 22;
		this.TreeView1.Location = new System.Drawing.Point(0, 28);
		this.TreeView1.Name = "TreeView1";
		this.TreeView1.Size = new System.Drawing.Size(200, 292);
		this.TreeView1.TabIndex = 98;
		this.TreeView1.NodeCheckedChanged += new TreeNodeCheckedEventHandler(this.TreeView1_AfterCheck);
		this.TreeView1.NodeFormatting += new TreeNodeFormattingEventHandler(this.TreeViewElement_NodeFormatting);
		this.TreeView1.AllowDrop = true;
		this.TreeView1.FullRowSelect = true;
		this.TreeView1.HideSelection = false;
		this.TreeView1.HotTracking = true;
		this.TreeView1.Location = new System.Drawing.Point(0, 0);
		this.TreeView1.Margin = new System.Windows.Forms.Padding(0);
		this.TreeView1.ShowLines = false;
		this.TreeView1.ShowNodeToolTips = true;
		this.TreeView1.ShowRootLines = false;
		this.TreeView1.ItemHeight = 22;
		this.TreeView1.ExpandAnimation = ExpandAnimation.None;
		this.TreeView1.AllowPlusMinusAnimation = false;
		this.TreeView1.PlusMinusAnimationStep = 1;
		this.TreeView1.TreeViewElement.AutoSizeItems = true;
		this.TreeView1.TreeViewElement.AllowAlternatingRowColor = false;
		this.TreeView1.FullRowSelect = true;
		this.TreeView1.ShowExpandCollapse = true;
		this.TreeView1.ShowRootLines = false;
		this.TreeView1.ShowLines = false;
		this.TreeView1.EnableTheming = true;
		this.TreeView1.ThemeName = "TelerikMetroBlue";
		this.TreeView1.TreeIndent = 18;
		// 
		// Toolbar
		// 
		this.Toolbar.Items.AddRange(new ToolStripItem[] {
			this.ToolStripLabel1,
			this.txtRuleId});
		this.Toolbar.Location = new System.Drawing.Point(0, 0);
		this.Toolbar.Name = "Toolbar";
		this.Toolbar.Size = new System.Drawing.Size(200, 25);
		this.Toolbar.TabIndex = 0;
		this.Toolbar.Text = "ZToolBar1";
		// 
		// ToolStripLabel1
		// 
		this.ToolStripLabel1.Name = "ToolStripLabel1";
		this.ToolStripLabel1.Size = new System.Drawing.Size(45, 22);
		this.ToolStripLabel1.Text = "Buscar:";
		// 
		// txtRuleId
		// 
		this.txtRuleId.AutoToolTip = true;
		this.txtRuleId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtRuleId.Name = "txtRuleId";
		this.txtRuleId.Size = new System.Drawing.Size(100, 25);
		this.txtRuleId.ToolTipText = "Ingrese el ID o nombre de la regla a buscar";
		// 
		// WFTreeDiagram
		// 
		this.BackColor = System.Drawing.Color.White;
		this.Controls.Add(this.Toolbar);
		this.Controls.Add(this.TreeView1);
		this.Name = "WFTreeDiagram";
		this.Size = new System.Drawing.Size(200, 320);
		this.Toolbar.ResumeLayout(false);
		this.Toolbar.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private IWorkFlow _WF;
	private Zamba.AppBlock.ZIconsList _IL;
	private Zamba.Core.IPrintTreeViewHelper _treeViewHelper = null;
	private WFRuleParent WFRule;
	private IUserGroup _group;
	private DataGridView dgvWorkflow = null;

	public ArrayList CheckedNodes;
	public WFTreeDiagram(Zamba.AppBlock.ZIconsList IL, IWFNodeHelper WFNodeHelper)
		: base()
	{
		this.NodeHelper = WFNodeHelper;

		//This call is required by the Windows Form Designer.
		InitializeComponent();
	}

	public WFTreeDiagram(System.Collections.Generic.List<Zamba.Core.WorkFlow> WF, Zamba.AppBlock.ZIconsList IL, IUserGroup Group, bool HideSearchToolbar, bool useChecks, bool LoadAllRules, bool LoadParentRules, IWFNodeHelper WFNodeHelper)
		: this(WFNodeHelper)
	{

		_IL = IL;
		_group = Group;
		if (HideSearchToolbar)
		{
			this.Toolbar.Visible = false;
		}
		if (useChecks)
		{
			this.TreeView1.CheckBoxes = true;
		}

		LoadWfs(WF, IL, LoadAllRules, LoadParentRules);

		TreeNodeCheckedEventHandler handler = default(TreeNodeCheckedEventHandler);
		TreeView1.NodeCheckedChanged -= handler;
		TreeView1.NodeCheckedChanged += handler;
	}


	public IWorkFlow WF
	{
		get { return _WF; }
		set { _WF = value; }
	}

	public IWFNodeHelper NodeHelper { get; private set; }

	public void LoadWfs(System.Collections.Generic.List<Zamba.Core.WorkFlow> wfs,
		Zamba.AppBlock.ZIconsList IL, bool LoadAllRules, bool LoadParentRules)
	{
		try
		{
			InitNode Root = new InitNode();
			Root.Text = "Procesos";
			foreach (Zamba.Core.WorkFlow WF in wfs)
			{
				if ((this._WF == null) || WF.ID != this._WF.ID)
				{
					this._WF = WF;
					LoadRules(WF, TreeView1, LoadAllRules, LoadParentRules);
				}
			}
		}
		catch (Exception ex)
		{
			Zamba.Core.ZClass.raiseerror(ex);
		}

	}

	#region "Events"
	public delegate void RadTreeViewSelectedEventHandler();
	public event RadTreeViewEventHandler AfterCheck;


	private void TreeViewElement_NodeFormatting(object sender, Telerik.WinControls.UI.TreeNodeFormattingEventArgs args)
	{
		IBaseWFNode Node = (IBaseWFNode)args.Node;
		NodeWFTypes NodeType = Node.NodeWFType;

		Image currentImage = NodeHelper.GetnodeImage(NodeType);

		args.NodeElement.ImageElement.Image = currentImage;
		args.NodeElement.ClipDrawing = false;
		args.NodeElement.ImageElement.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentBounds;
		args.NodeElement.ContentElement.DisableHTMLRendering = true;
		args.NodeElement.ImageElement.StretchHorizontally = false;
		args.NodeElement.ImageElement.ImageLayout = ImageLayout.Stretch;
		args.NodeElement.ImageElement.MinSize = new Size(20, 20);


        args.NodeElement.BackColor = Color.White;
        args.NodeElement.BackColor2 = Color.White;
        args.NodeElement.BackColor3 = Color.White;
        args.NodeElement.BackColor4 = Color.White;


    }

    //Dentro de este evento esta la logica de checkeo del arbol.
    private void TreeView1_AfterCheck(object sender, RadTreeViewEventArgs e)
	{
		try
		{
			//Verifico si es BaseWFNode
			if (e.Node is BaseWFNode)
			{
				BaseWFNode BaseWFNode = (BaseWFNode)e.Node;
				//si checkeo un nodo instancio un ArrayList para poder despues realizar la impresion.
				if (e.Node.Checked)
				{
					if (this.CheckedNodes == null)
					{
						this.CheckedNodes = new ArrayList();
					}

					switch (BaseWFNode.NodeWFType)
					{
						case NodeWFTypes.WorkFlow:
							Zamba.Core.WFNode WFNode = (Zamba.Core.WFNode)e.Node;
							this.CheckedNodes.Add(WFNode.WorkFlow.ID.ToString() + "|" + ShapeType.Workflow.ToString());
							//Se checkean los step correspondientes
							foreach (RadTreeNode node in ((RadTreeNode)(WFNode)).Nodes)
							{
								if (node != null)
								{
									node.Checked = true;
								}
							}
							break;
						case NodeWFTypes.Etapa:
						case NodeWFTypes.Estado:
							Zamba.Core.EditStepNode StepNode = (Zamba.Core.EditStepNode)e.Node;
							this.CheckedNodes.Add(StepNode.WFStep.ID.ToString() + "|" + ShapeType.Step.ToString());
							//se checkean las reglas correspondientes
							foreach (RadTreeNode node in ((RadTreeNode)(StepNode)).Nodes)
							{
								if (node != null)
								{
									node.Checked = true;
								}
							}
							break;
						case NodeWFTypes.TipoDeRegla:
							Zamba.Core.RuleTypeNode ruleTypeNode = (Zamba.Core.RuleTypeNode)e.Node;
							foreach (RadTreeNode node in ((RadTreeNode)(ruleTypeNode)).Nodes)
							{
								if (node != null)
								{
									node.Checked = true;
								}
							}

							break;
						case NodeWFTypes.Regla:
						case NodeWFTypes.FloatingRule:
							Zamba.Core.RuleNode ruleNode = (Zamba.Core.RuleNode)e.Node;
							this.CheckedNodes.Add(ruleNode.RuleId.ToString() + "|" + ShapeType.Rule.ToString());
							break;
					}
				}
				else
				{
					switch (BaseWFNode.NodeWFType)
					{
						case NodeWFTypes.WorkFlow:
							Zamba.Core.WFNode WFNode = (Zamba.Core.WFNode)e.Node;
							this.CheckedNodes.Remove(WFNode.WorkFlow.ID.ToString() + "|" + ShapeType.Workflow.ToString());
							//Se descheckean los Step correspondientes
							foreach (RadTreeNode node in ((RadTreeNode)(WFNode)).Nodes)
							{
								if (node != null)
								{
									node.Checked = false;
								}
							}

							break;
						case NodeWFTypes.Etapa:
						case NodeWFTypes.Estado:

							Zamba.Core.EditStepNode StepNode = (Zamba.Core.EditStepNode)e.Node;
							this.CheckedNodes.Remove(StepNode.WFStep.ID.ToString() + "|" + ShapeType.Step.ToString());
							//Se descheckean las reglas
							foreach (RadTreeNode node in ((RadTreeNode)(StepNode)).Nodes)
							{
								if (node != null)
								{
									node.Checked = false;
								}
							}
							break;
						case NodeWFTypes.TipoDeRegla:
							Zamba.Core.RuleTypeNode ruleTypeNode = (Zamba.Core.RuleTypeNode)e.Node;
							foreach (RadTreeNode node in ((RadTreeNode)(ruleTypeNode)).Nodes)
							{
								if (node != null)
								{
									node.Checked = false;
								}
							}

							break;
						case NodeWFTypes.Regla:
						case NodeWFTypes.FloatingRule:
							Zamba.Core.RuleNode ruleNode = (Zamba.Core.RuleNode)e.Node;
							this.CheckedNodes.Remove(ruleNode.RuleId.ToString() + "|" + ShapeType.Rule.ToString());
							break;
					}
				}
			}
			//En caso de que el nodo no sea BaseWFNode se verifica que sea un RadTreeNode
			// si este lo es obtengo el valor de la propiedad Checked y se lo seteo a los hijos.
			else
			{
				if (e.Node is RadTreeNode)
				{
					foreach (RadTreeNode node in ((RadTreeNode)(e.Node)).Nodes)
					{
						if (node != null)
						{
							node.Checked = ((RadTreeNode)(e.Node)).Checked;
						}
					}

				}

			}
		}
		catch (Exception ex)
		{
			ZClass.raiseerror(ex);
		}
	}
	#endregion

	/// <summary>
	/// Agrega la grilla con todos los permisos de las reglas de las etapas de un workflow determinado
	/// </summary>
	/// <remarks></remarks>
	private void ShowWorkflowGrid(Int64 wfId)
	{
		dgvWorkflow = new DataGridView();

		{
			dgvWorkflow.Dock = DockStyle.Fill;
			dgvWorkflow.AllowUserToAddRows = false;
			dgvWorkflow.AllowUserToDeleteRows = false;
			dgvWorkflow.AllowUserToResizeColumns = true;
			dgvWorkflow.AllowUserToResizeRows = false;
			dgvWorkflow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvWorkflow.RowHeadersVisible = false;
			dgvWorkflow.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvWorkflow.ShowRowErrors = false;
			dgvWorkflow.ShowEditingIcon = false;
			dgvWorkflow.DataSource = WFBusiness.GetUserHabilitatedRules(_group.ID, wfId);
		}
	}
	private void mnuExpadirRegla_Click(System.Object sender, System.EventArgs e)
	{
		TreeView1.SelectedNode.ExpandAll();
	}



	private void LoadRules(WorkFlow wf, RadTreeView treeView, bool LoadAllRules, bool LoadParentRules)
	{
		try
		{
			//Nodo Inicial
			WFNode Toproot = new WFNode(wf);
			treeView.Nodes.Add(Toproot);
			//Nodos de Etapas
			foreach (WFStep s in wf.Steps.Values)
			{
				try
				{
					WFStep step = s;
					EditStepNode stepNode = new EditStepNode(ref step, wf.InitialStep);
					//quito los nodos innecesarios
					stepNode.Nodes.Remove((RadTreeNode)stepNode.RightNode);
					stepNode.Nodes.Remove((RadTreeNode)stepNode.InputNode);
					stepNode.Nodes.Remove((RadTreeNode)stepNode.InputValidationNode);
					stepNode.Nodes.Remove((RadTreeNode)stepNode.OutputNode);
					stepNode.Nodes.Remove((RadTreeNode)stepNode.OutputValidationNode);
					stepNode.Nodes.Remove((RadTreeNode)stepNode.UpdateNode);
					Toproot.Nodes.Add(stepNode);

					AddRulesNodes(stepNode, LoadParentRules);
				}
				catch (Exception ex)
				{
					ZClass.raiseerror(ex);
				}
			}
		}
		catch (Exception ex)
		{
			ZClass.raiseerror(ex);
		}
	}



	private void AddRulesNodes(EditStepNode stepNode, bool LoadParentRules)
	{
		try
		{
			//Agrega las reglas padres que dependen del primer nodo del tipo de reglas
			bool IsFirst = true;
			foreach (Zamba.Core.DsRules.WFRulesRow r in stepNode.WFStep.DSRules.WFRules)
			{
				//todo: ml:  seria ideal tener las propiedades de las reglas en el ds
				IWFRuleParent rule = Zamba.Core.WFRulesBusiness.GetInstanceRuleById((Int64)r.Id, stepNode.WFStep.ID, true);
				if (r.ParentId == 0)
				{
					switch ((TypesofRules)r.ParentType)
					{
						case TypesofRules.Entrada:
							AddRuleNode((BaseWFNode)stepNode.InputNode, ref rule, rule.DisableChildRules, LoadParentRules);
							break;
						case TypesofRules.ValidacionEntrada:
							AddRuleNode((BaseWFNode)stepNode.InputValidationNode, ref rule, rule.DisableChildRules, LoadParentRules);
							break;
						case TypesofRules.Salida:
							AddRuleNode((BaseWFNode)stepNode.OutputNode, ref rule, rule.DisableChildRules, LoadParentRules);
							break;
						case TypesofRules.ValidacionSalida:
							AddRuleNode((BaseWFNode)stepNode.OutputValidationNode, ref rule, rule.DisableChildRules, LoadParentRules);
							break;
						case TypesofRules.Actualizacion:
							AddRuleNode((BaseWFNode)stepNode.UpdateNode, ref rule, rule.DisableChildRules, LoadParentRules);
							break;
						case TypesofRules.Planificada:
							AddRuleNode((BaseWFNode)stepNode.ScheduleNode, ref rule, rule.DisableChildRules, LoadParentRules);
							break;
						case TypesofRules.Eventos:
							AddEventRuleNode((BaseWFNode)stepNode.EventNode, ref rule, LoadParentRules);
							break;
						case TypesofRules.AccionUsuario:

							RuleTypeNode UserActionNode = default(RuleTypeNode);
							string Name = string.Empty;

							if (IsFirst)
							{
								UserActionNode = (RuleTypeNode)stepNode.UserActionNode;
								IsFirst = false;
							}
							else
							{
								UserActionNode = new RuleTypeNode(TypesofRules.AccionUsuario, stepNode.WFStep.ID);

								int indx = Convert.ToInt32(stepNode.Nodes.IndexOf((RadTreeNode)stepNode.ScheduleNode));

								stepNode.Nodes.Insert(indx, UserActionNode);
							}

							AddRuleNode(UserActionNode, ref rule, rule.DisableChildRules, LoadParentRules);


							if (string.IsNullOrEmpty(Name))
							{
								UserActionNode.UpdateUserActionNodeName(rule.Name);
							}
							else
							{
								UserActionNode.UpdateUserActionNodeName(Name);
							}

							break;
						case TypesofRules.Floating:
							AddFloatingRuleNode((BaseWFNode)stepNode.FloatingNode, ref rule, LoadParentRules, 0);
							break;
					}
				}
			}
		}
		catch (Exception ex)
		{
			ZClass.raiseerror(ex);
		}
	}


	private bool? AddRuleNode(BaseWFNode parentNode, ref IWFRuleParent rule, bool? DisableChilds, bool LoadParentRules)
	{
		int parentId = 0;

		try
		{
			if (parentNode.GetType() == typeof(RuleNode))
				parentId = (int)((RuleNode)parentNode).RuleId;

			RuleNode RuleNode = new RuleNode((WFRuleParent)rule, parentId);

			if (DisableChilds == true)
			{
				RuleNode.ImageIndex = 37;
				//RuleNode.SelectedImageIndex = 37;
			}

			parentNode.Nodes.Add(RuleNode);

			if (LoadParentRules)
			{
				foreach (WFRuleParent R in rule.ChildRules)
				{
					IWFRuleParent ChildRule = R;
					AddRuleNode(RuleNode, ref ChildRule, DisableChilds, LoadParentRules);

				}
			}
		}
		catch (Exception ex)
		{
			ZClass.raiseerror(ex);
		}
		return (bool?)DisableChilds;
	}

	private void AddFloatingRuleNode(BaseWFNode ParentNode, ref IWFRuleParent rule, bool LoadParentRules, Int32 parentId)
	{
		try
		{
			IWFRuleParent r = rule;

			//agrega la regla al nodo y verifica si tiene child para agregar
			RuleNode ruleNode = new RuleNode((WFRuleParent)r, parentId);
			ParentNode.Nodes.Add(ruleNode);
			if (LoadParentRules)
			{
				foreach (WFRuleParent R in rule.ChildRules)
				{
					IWFRuleParent ChildRule = R;
					AddFloatingRuleNode(ruleNode, ref ChildRule, LoadParentRules, (int)r.ID);
				}
			}
		}
		catch (Exception ex)
		{
			ZClass.raiseerror(ex);
		}
	}

	/// <summary>
	/// Método que sirve para agregar los nodos relacionados a eventos
	/// </summary>
	/// <remarks></remarks>
	/// <history> 
	///     [Tomas] 21/04/2009  Created
	/// </history>
	private void AddEventRuleNode(BaseWFNode parentNode, ref IWFRuleParent rule, bool LoadParentRules)
	{
		bool contieneEvento = false;
		Int32 eventoIndex = default(Int32);
		string ruleTypeName = rule.RuleType.ToString();
		int parentId = 0;

		try
		{
			if (parentNode.GetType() == typeof(RuleNode))
				parentId = (int)((RuleNode)parentNode).RuleId;

			RuleNode RuleNode = new RuleNode((WFRuleParent)rule, parentId);

			//Comprueba que el nodo con el nombre del evento exista
			foreach (RadTreeNode nodo in parentNode.Nodes)
			{
				if (string.Compare(nodo.Text, ruleTypeName) == 0)
				{
					contieneEvento = true;
					eventoIndex = nodo.Index;
					break;
				}
			}

			//En caso de existir se agrega a ese nodo, en caso de no 
			//existir, lo crea y luego se agrega al nodo.
			if (contieneEvento)
			{
				//Obtiene el nodo del evento donde se agregará la regla
				RadTreeNode EventNode = parentNode.Nodes[eventoIndex];
				//Agrego la regla al nodo del evento que corresponde
				EventNode.Nodes.Add(RuleNode);
				//Agrego las reglas hijas a la regla principal
				if (LoadParentRules)
				{
					foreach (WFRuleParent R in rule.ChildRules)
					{
						IWFRuleParent Rule_Node = R;
						AddRuleNode(RuleNode, ref Rule_Node, R.DisableChildRules, true);
					}
				}
			}
			else
			{
				//Agrega el nodo del evento
				RuleTypeNode EventNode = new RuleTypeNode(rule.RuleType, rule.WFStepId);
				EventNode.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 7, System.Drawing.FontStyle.Regular);
				EventNode.ImageIndex = 28;
				// EventNode.SelectedImageIndex = 28;

				parentNode.Nodes.Add(EventNode);
				//Agrega el nodo de la regla al nodo del evento y luego sus hijos.
				EventNode.Nodes.Add(RuleNode);
				if (LoadParentRules)
				{
					foreach (WFRuleParent R in rule.ChildRules)
					{
						IWFRuleParent Rule_Node = R;
						AddRuleNode(RuleNode, ref Rule_Node, R.DisableChildRules, true);
					}
				}
			}
		}
		catch (Exception ex)
		{
			ZClass.raiseerror(ex);
		}
	}
}

