using System;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using System.Data;
using Diagram = Zamba.Shapes.Views.Diagram;
using Zamba.Core.Enumerators;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using MindFusion.Diagramming;
using System.Linq;

namespace Zamba.Shapes.Controllers
{
    class RuleFormDetailsController : IDiagramController, IDiagramFiltereable, IRefresh
    {
        private GenericShape _ShpRoot;
        private FlowchartLayout _Layout = null;
        private object[] _params;
        Diagram wfDiagram;

        public IDiagram GetDiagram(object[] parameters)
        {
            return FillDiagram(parameters);
        }

        private IDiagram FillDiagram(object[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                //Diagrama donde se comienzan a agregar los nodos
                wfDiagram = new Diagram();
                wfDiagram.DiagramType = DiagramType.Forms;

                IZwebForm form = (IZwebForm)parameters[1];
                //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
                ZCoreView rootObject = new ZCoreView(form.ID, "Formulario: " + form.Name);
                _ShpRoot = new FormShape(wfDiagram, form);
                _ShpRoot.IsRoot = true;

                GenericShape ruleRoot = new GenericShape(wfDiagram, new ZCoreView(0, "Acciones en formulario"), _ShpRoot);
                GenericShape zvarRoot = new GenericShape(wfDiagram, new ZCoreView(0, "Variables de Zamba"), _ShpRoot);

                if (form != null)
                {
                    FileInfo fi = null;

                    if (form.UseBlob)
                    {
                        FormBusinessExt frmBusinessExt = new FormBusinessExt();
                        string path;

                        try
                        {
                            path = frmBusinessExt.CopyBlobToTemp((ZwebForm)form, false);
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                            path = form.Path;
                        }

                        fi = new FileInfo(path);
                    }
                    else
                    {
                        fi = new FileInfo(form.Path);
                    }

                    if (fi.Exists)
                    {
                        StreamReader fs = new StreamReader(fi.FullName);
                        string strForm = fs.ReadToEnd().ToLower();
                        string strBody = strForm.Substring(strForm.IndexOf("<body>", StringComparison.CurrentCultureIgnoreCase));

                        FormBusinessExt fb = new FormBusinessExt();
                        List<IWFRuleParent> formRules = fb.GetFormRuleInCoreView(strBody);

                        RulesController rc = new RulesController();

                        if (formRules != null)
                        {
                            foreach (IWFRuleParent item in formRules.Where((r) => r != null))
                            {
                                rc.AddRuleShape(ruleRoot, (WFRuleParent)item, null, 10, wfDiagram);
                            }
                        }

                        List<IIndex> formIndexs = fb.GetFormIndexs(strBody, true);

                        DocTypesController dtController = new DocTypesController();
                        GenericController gController = new GenericController();
                        TableNode tn;
                        if (formIndexs != null)
                        {
                            tn = dtController.GetIndexesAsTable(wfDiagram, formIndexs);
                            gController.SetRelation(wfDiagram, _ShpRoot, tn);
                        }

                        List<string> formZvar = fb.GetFormZvarInCoreView(strBody);
                        if (formZvar != null)
                        {
                            rc.AddZvarShape(wfDiagram, formZvar, zvarRoot);
                        }
                    }
                }

                //Se organizan los objetos del diagrama
                SetLayout(wfDiagram, (GenericShape)_ShpRoot);

                //Se devuelve el diagrama
                return wfDiagram;
            }

            return null;
        }

        private void SetLayout(Diagram diagram, GenericShape genericShape)
        {
            //Layout del arbol principal
            _Layout = new FlowchartLayout();

            //Se actualiza el diseño
            _Layout.Arrange(diagram);

            //Dibuja el diagrama acomodando todo lo modificado
            diagram.ResizeToFitItems(25, false);
        }

        public IDiagram ApplyActorFilter(string actorName, DiagramType diagramType, object[] parameters)
        {
            return FillDiagram(parameters);
        }

        public IDiagram Refresh(object[] parameters)
        {
            return FillDiagram(parameters);
        }
    }
}
