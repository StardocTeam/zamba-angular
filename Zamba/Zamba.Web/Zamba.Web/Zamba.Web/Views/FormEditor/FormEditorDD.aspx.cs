using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Zamba.Core;
using System.Text;
using System.Web;
using Zamba.Web.App_Code.Helpers;

namespace Zamba.WebFormEditor
{

	public partial class FormEditorDD : System.Web.UI.Page
	{
		#region Master Region Constants
		private const string STARTMASTERREGION = "<!--MasterRegion-->";
		private const string ENDMASTERREGION = "<!--EndMasterRegion-->";
		private const string WEBFORMSMASTERREGION = STARTMASTERREGION +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"../../Content/Styles/thickbox.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"../../Content/Styles/jquery-ui-1.8.6.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"../../Content/Styles/ZambaUIWeb.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"../../Content/Styles/ZambaUIWebTables.css\" />" +
				"<link rel=\"stylesheet\" type=\"text/css\" href=\"../../Content/Styles/jq_datepicker.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"../../Content/Styles/GridThemes/WhiteChromeGridView.css\" />" +
				"<link rel=\"stylesheet\" type=\"text/css\" href=\"../../Content/Styles/tabber.css\" media=\"screen\"/> " +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"../../Content/Styles/GridThemes/GridViewGray.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"../../Content/Styles/jquery.galleryview-3.0-dev.css\" />" +
				"<script type=\"text/javascript\" src=\"../../scripts/jquery-2.2.2.min.js\"></script> " +
				"<script type=\"text/javascript\" src=\"../../scripts/jquery-ui-1.8.6.min.js\"></script>" +
				"<script type=\"text/javascript\" src=\"../../scripts/jq_datepicker.js\"></script>" +
                "<script type=\"text/javascript\" src=\"../../scripts/Zamba.js\"></script><script type=\"text/javascript\" src=\"../../scripts/Zamba.Fn.js\"></script><script src=\"../../scripts/Zamba.Validations.js\" type=\"text/javascript\"></script>" +
				"<script type=\"text/javascript\" src=\"../../scripts/thickbox-compressed.js\"></script>" +
				"<script type=\"text/javascript\" src=\"../../scripts/jquery.galleryview-3.0-dev.js\"></script>" +
				"<script type=\"text/javascript\" src=\"../../scripts/jquery.easing.1.3.js\"></script>" +
				"<script type=\"text/javascript\" src=\"../../scripts/jquery.timers-1.2.js\"></script>" +
				"<script type=\"text/javascript\" src=\"../../scripts/jquery.validate.min.js\"></script>" +
				"<script type=\"text/javascript\" src=\"../../scripts/jquery.validate.min.js\"></script>" +
				"<script type=\"text/javascript\" src=\"../../scripts/tabber.js\"></script>" +
				"<script type=\"text/javascript\" src=\"../../scripts/jquery.caret.1.02.min.js\"></script>" +
				ENDMASTERREGION;

		private const string WINDOWSFORMSMASTERREGION = STARTMASTERREGION +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"Content/Styles/thickbox.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"Content/Styles/jquery-ui-1.8.6.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"Content/Styles/ZambaUIWeb.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"Content/Styles/ZambaUIWebTables.css\" />" +
				"<link rel=\"stylesheet\" type=\"text/css\" href=\"Content/Styles/jq_datepicker.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"Content/Styles/GridThemes/WhiteChromeGridView.css\" />" +
				"<link rel=\"stylesheet\" type=\"text/css\" href=\"Content/Styles/tabber.css\" media=\"screen\"/> " +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"Content/Styles/GridThemes/GridViewGray.css\" />" +
				"<link rel=\"Stylesheet\" type=\"text/css\" href=\"Content/Styles/jquery.galleryview-3.0-dev.css\" />" +
				"<script type=\"text/javascript\" src=\"scripts/jquery-2.2.2.min.js\"></script> " +
				"<script type=\"text/javascript\" src=\"scripts/jquery-ui-1.8.6.min.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/jquery.layout-latest.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/jq_datepicker.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/Zamba.js\"></script>" +
            "<script type=\"text/javascript\" src=\"scripts/Zamba.Fn.js\"></script>" +
                "<script type=\"text/javascript\" src=\"scripts/thickbox-compressed.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/jquery.galleryview-3.0-dev.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/jquery.easing.1.3.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/jquery.timers-1.2.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/jquery.validate.min.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/jquery.validate.min.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/tabber.js\"></script>" +
				"<script type=\"text/javascript\" src=\"scripts/jquery.caret.1.02.min.js\"></script>" +
				ENDMASTERREGION;
        #endregion

        DocTypesBusiness DTB = new DocTypesBusiness();
        FormBusiness FB = new FormBusiness();
        protected void Page_PreInit(object sender, EventArgs e)
		{
            ZOptBusiness zopt = new ZOptBusiness();
            Page.Theme = zopt.GetValue("CurrentTheme");
            zopt = null;			
		}

		List<string> FixedReferences
		{
			get
			{
				return (List<string>)Session["fixedReferences"];
			}
			set 
			{
				Session["fixedReferences"] = value;
			}
		}

		private void Page_Load(object sender, EventArgs e)
		{
			RadEditor1.DisableFilter(Telerik.Web.UI.EditorFilters.RemoveScripts);
			RadEditor1.DisableFilter(Telerik.Web.UI.EditorFilters.EncodeScripts);

			if (!IsPostBack)
			{
                ZOptBusiness zopt = new ZOptBusiness();

                String CurrentTheme = zopt.GetValue("CurrentTheme");
                zopt = null;
                int FormId;
				String FormType = String.Empty;
				if (Request.QueryString["FId"] != null) FormId = int.Parse(Request.QueryString["FId"]); else FormId = 0;
				int currentFormTypeId;
				if (Request.QueryString["FTId"] != null) currentFormTypeId = int.Parse(Request.QueryString["FTId"]); else currentFormTypeId = 0;
				
				if (FormId == -1)
					Response.Redirect("~/FormEditorMenu.aspx");

				if (FormId > 0)
				{
					try
					{
						this.BtnTest.Enabled = true;
						var ZForm = FB.GetForm(FormId);

						FormType = ZForm.Type.ToString();

						if (string.IsNullOrEmpty(ZForm.Path)==false)
						{

							if (File.Exists(ZForm.Path))
							{
								StreamReader sr = null;
								String FormContent;
								
								try
								{
									sr = new StreamReader(ZForm.Path);
									FormContent = sr.ReadToEnd();
								}
								finally
								{
									if (sr != null)
									{
										sr.Close();
										sr.Dispose();
										sr = null;
									}
								}

								//Creamos un nueva lista para reparar las referencias.
								FixedReferences = new List<string>();

								////Quitamos tags que pueden llegar a "molestar" en la carga de los formularios.
								//FormContent = FormContent.Replace("<html>", string.Empty);
								//FormContent = FormContent.Replace("</html>", string.Empty);
								//FormContent = FormContent.Replace("<head>", string.Empty);
								//FormContent = FormContent.Replace("</head>", string.Empty);
								//FormContent = FormContent.Replace("<body>", string.Empty);
								//FormContent = FormContent.Replace("</body>", string.Empty);

								////Corregimos las referencias no encontradas.
								//FormContent = FixReferences(FormContent, "script");

								////Creamos el script temporal que cargará jquery y demás
								//FormContent = MakeTempScript(FormContent);

								FormContent = AddMasterRegion(FormContent, ZForm.Type, false);


								//Corregimos las referencias del formulario a abrir.
                                FormContent = HTML.GenerateCorrectlyReferences(FormContent, CurrentTheme);

								this.RadEditor1.Content = FormContent;
								this.BtnSave.Enabled = true;
							}
							else
							{
								this.RadEditor1.Content = "No se ha encontrado el formulario seleccionado en la ruta: " + ZForm.Path;
							}
						}
						else
						{
							this.RadEditor1.Content = "El formulario seleccionado no tiene una ruta fisica";
						}
					}
					catch (Exception ex)
					{
						this.BtnSave.Enabled = false;
						this.RadEditor1.Content = "<h2>Error al cargar el contenido del formulario</h2> " + ex.ToString();
						Zamba.Core.ZClass.raiseerror(ex);
					}
				}
				else
				{
					this.BtnSave.Enabled = true;

					string FormContent = this.RadEditor1.Content;
					FormContent = AddMasterRegion(FormContent, (FormTypes)currentFormTypeId, true);


					//Corregimos las referencias del formulario a abrir.
					FormContent = HTML.GenerateCorrectlyReferences(FormContent, CurrentTheme);

					this.RadEditor1.Content = FormContent;
				}

				if (FormId != -1)
				{
					try
					{
						foreach (var FormTypeId in Enum.GetValues(typeof(Zamba.Core.FormTypes)))
						{
							this.DropDownListFormTypes.Items.Add(new ListItem(Enum.GetName(typeof(Zamba.Core.FormTypes), FormTypeId), FormTypeId.ToString()));
						}

						this.DropDownListFormTypes.SelectedValue = currentFormTypeId.ToString();

						if (String.Compare(FormType, String.Empty) == 0)
							//insercion
							this.DropDownListFormTypes.Text = Enum.GetName(typeof(Zamba.Core.FormTypes), currentFormTypeId);
						else
							//edicion
							this.DropDownListFormTypes.Text = FormType;                        
						
					}
					catch (Exception ex)
					{
						this.lblerror.Text = ex.ToString();
						Zamba.Core.ZClass.raiseerror(ex);
					}
				}

				int DocTypeId;
				if(Request.QueryString["EId"] != null) DocTypeId = int.Parse(Request.QueryString["EId"]); else DocTypeId = 0;
				
				if (DocTypeId > 0)
				{
					PopulateTreeViewFromIndexs(RadTreeView1.Nodes, DocTypeId);
					PopulateTreeViewFromDocAsociated(RadTreeView1.Nodes, DocTypeId);
					PopulateTreeViewFromDocAsociatedIndexs(RadTreeView1.Nodes, DocTypeId);
				}

				RadTreeNode nodeControls = new RadTreeNode();
				nodeControls.Text = "Controles";
				nodeControls.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				nodeControls.Attributes["Category"] = "Controles";
				nodeControls.Value = "Controles";
				RadTreeView1.Nodes.Add(nodeControls);
			   
				PopulateTreeViewFromJquery(nodeControls.Nodes);

				RadTreeNode node1 = new RadTreeNode();
				node1.Text = "Imagenes";
				node1.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				node1.Attributes["Category"] = "Imagenes";
				node1.Value = "Imagenes de Zamba";
				RadTreeView1.Nodes.Add(node1);
				
				PopulateTreeViewFromDirectory(node1.Nodes, Server.MapPath("~/Content/Images"));

				//RadTreeNode node2 = new RadTreeNode();
				//node2.Text = "Scripts";
				//node2.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				//node2.Attributes["Category"] = "Scripts";
				//node2.Value = "Scripts";
				//RadTreeView1.Nodes.Add(node2);
				
				//PopulateTreeViewScriptsFromDirectory(node2.Nodes, Server.MapPath("~/Scripts"));

				RadTreeNode node3 = new RadTreeNode();
				node3.Text = "Imagenes";
				node3.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				node3.Attributes["Category"] = "Imagenes";
				node3.Value = "Imagenes";
				RadTreeView1.Nodes.Add(node3);

				PopulateTreeViewFromDirectory(node3.Nodes, Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme) + "/Content/Images");

				RadTreeNode node4 = new RadTreeNode();
				node4.Text = "Scripts";
				node4.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				node4.Attributes["Category"] = "Scripts";
				node4.Value = "Scripts";
				RadTreeView1.Nodes.Add(node4);

				PopulateTreeViewScriptsFromDirectory(node4.Nodes, Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme) + "/Scripts");

			 //   RadEditor1.Modules.Clear();

			}
		}

		private string AddMasterRegion(string FormContent, FormTypes FormType, bool IsNewForm)
		{
			string strToReturn = FormContent;
			int headIndex = FormContent.IndexOf("<head>");
			if (headIndex == -1)
				headIndex = FormContent.IndexOf("<HEAD>"); 

			if (FormType.ToString().Contains("Web"))
			{
				if (headIndex > 0)
					strToReturn = FormContent.Insert(headIndex + 6, ' ' + WEBFORMSMASTERREGION);
				else
					strToReturn = WEBFORMSMASTERREGION + ' ' + FormContent;
			}
			else 
			{
				if (IsNewForm)
				{
					if (headIndex > 0)
						strToReturn = FormContent.Insert(headIndex + 6, ' ' + WEBFORMSMASTERREGION);
					else
						strToReturn = WEBFORMSMASTERREGION + ' ' + FormContent;
				}
			}

			return strToReturn;
		}

		#region "Carga de scripts"
		private string MakeTempScript(string FormContent)
		{
			int tagIndex = 0;
			int closingTag = 0;
			int closeTagIndex = 0;
			StringBuilder sbScriptToSave = new StringBuilder();
			string strToAppend;
			List<string> listOfPathToAdd = new List<string>();
			int startPathIndex;
			int endPathIndex;
			int i = 0;

			//Buscamos el primer tag script y empezamos a loopear
			tagIndex = FormContent.IndexOf("<script", 0);
			while (tagIndex != -1 && tagIndex + 5 < FormContent.Length)
			{
				try
				{
					closingTag = FormContent.IndexOf('>', tagIndex);
					//Si no tiene scr es un script "embebido".
					if (FormContent.IndexOf("src=", tagIndex, closingTag - tagIndex) == -1)
					{
						closeTagIndex = FormContent.IndexOf("</script>", closingTag);

						//Obtenemos el contenido del script.
						strToAppend = FormContent.Substring(closingTag + 1, closeTagIndex - closingTag - 1);

						if (!string.IsNullOrEmpty(strToAppend))
						{
							if (strToAppend.Contains("Content/scripts/jquery-1.7.1.min.js") == false)
								sbScriptToSave.AppendLine(strToAppend);
								
							FormContent = FormContent.Replace(strToAppend, string.Empty);
						}
					}
					else
					{
						//Si tiene source entonces lo reemplazaremos por un Dummy, y agregamos ese source para cargar dinamicamente.
						startPathIndex = FormContent.IndexOf("src=", tagIndex, closingTag - tagIndex) + 5;
						endPathIndex = FormContent.IndexOf("\"", startPathIndex, closingTag - startPathIndex);
						listOfPathToAdd.Add(FormContent.Substring(startPathIndex, endPathIndex - startPathIndex));
						FormContent = ReplaceValueInFixedReferences(listOfPathToAdd[i], i, FormContent);
						i++;
					}

					tagIndex = FormContent.IndexOf("<script", tagIndex + 1);
				}
				catch (Exception ex)
				{
					Zamba.Core.ZClass.raiseerror(ex);
				}
			}


			if (sbScriptToSave.Length > 0)
			{
				try
				{
					//Generamos un nombre con un numero aleatorio para referenciar dinamicamente el script embebido en el form
					Random rndm = new Random((int)DateTime.Now.Ticks);
					string fileName = "TempScript__" + rndm.Next().ToString() + ".js";
					string pathToSave = Path.Combine(Server.MapPath("Content/scripts"), fileName);

					//Escibimos el archivo
					using (StreamWriter swTempFileWriter = new StreamWriter(pathToSave, false))
					{
						swTempFileWriter.Write(sbScriptToSave.ToString());
					}

					StringBuilder sbScriptToAppend = new StringBuilder();
					int countOfElements = listOfPathToAdd.Count;

					//Comenzamos la creacion del script para cargar todos las referencias dinamicamente
					sbScriptToAppend.Append("<script>");
					sbScriptToAppend.Append("   var script = document.createElement('script');");
					sbScriptToAppend.Append("   script.src = \"Content/scripts/jquery-1.7.1.min.js\";");
					sbScriptToAppend.Append("   var head = document.getElementsByTagName('head')[0];");
					sbScriptToAppend.Append("   script.onload = script.onreadystatechange = function() { if(this.readyState == \"complete\"){");
					sbScriptToAppend.Append("           var script2 = document.createElement('script');");
					sbScriptToAppend.Append("           script2.src = \"Content/scripts/jq_datepicker.js\";");
					int countAgregate = 0;
					if (countOfElements > 0)
					{
						int previusVarScript = 2;
						string varScriptName;
						for (int k = 3; k < countOfElements + 3; k++)
						{
							//Ver que por aca parece que hace fruta
							if (!(listOfPathToAdd[k - 3].Contains("jquery") && listOfPathToAdd[k - 3].Contains(".min.js")) &&
								!listOfPathToAdd[k - 3].Contains("jq_datepicker.js"))
							{
								varScriptName = "script" + k.ToString();
								sbScriptToAppend.Append("           script" + previusVarScript.ToString() +
									".onload = script" + previusVarScript.ToString() + ".onreadystatechange = function() {if(this.readyState == \"complete\"){");
								sbScriptToAppend.Append("               var " + varScriptName + " = document.createElement('script');");
								sbScriptToAppend.Append("               " + varScriptName + ".src = \"" + listOfPathToAdd[k - 3] + "\";");
								sbScriptToAppend.Append("               head.appendChild(" + varScriptName + ");");
								previusVarScript = k;
								if (k == countOfElements + 2)
								{
									sbScriptToAppend.Append("           " + varScriptName + ".onload = " + varScriptName + ".onreadystatechange = function() {if(this.readyState == \"complete\"){");
								}
								countAgregate++;
							}
						}
					}
					else
					{
						sbScriptToAppend.Append("           script2.onload = script2.onreadystatechange = function() {if(this.readyState == \"complete\"){");
					}

					if (countAgregate == 0 && countOfElements != 0)
					{
						sbScriptToAppend.Append("           script2.onload = script2.onreadystatechange = function() {if(this.readyState == \"complete\"){");
					}
					sbScriptToAppend.Append("               var script3 = document.createElement('script');");
					sbScriptToAppend.Append("               script3.src = \"Content/scripts/" + fileName + "\";");
					sbScriptToAppend.Append("               head.appendChild(script3);");
					sbScriptToAppend.Append("               script3.onload = script3.onreadystatechange = function() {if(this.readyState == \"complete\"){");
					sbScriptToAppend.Append("               }};");
					sbScriptToAppend.Append("           }};");
					for (int k = 0; k < countAgregate; k++)
					{
						sbScriptToAppend.Append("           }};");
					}
					sbScriptToAppend.Append("           head.appendChild(script2);");
					sbScriptToAppend.Append("   }};");
					sbScriptToAppend.Append("   head.appendChild(script);");
					sbScriptToAppend.Append("</script>");

					FormContent = FormContent.Insert(0, sbScriptToAppend.ToString());
				}
				catch (Exception ex)
				{
					this.lblerror.Text = ex.ToString();
					Zamba.Core.ZClass.raiseerror(ex);
				}
			}

			return FormContent;
		}

		private string ReplaceValueInFixedReferences(string ElementToSearch, int DummyIndex, string content)
		{
			//Buscamos si el elemento existe en la lista de referencias corregidas y lo reemplazamos
			int elementCount = FixedReferences.Count;
			bool finded = false;
			for (int i = 0; i < elementCount; i++)
			{
				if (FixedReferences[i].Contains(ElementToSearch))
				{
					string oldValue = FixedReferences[i].Split('|')[0];
					FixedReferences[i] = FixedReferences[i].Replace(oldValue, "DummyScript" + DummyIndex.ToString() + ".js");
					finded = true;
					break;
				}
			}

			//Si no lo encontro se agrega en la lista para luego ser reemplazado
			if (!finded)
			{
				FixedReferences.Add("DummyScript" + DummyIndex.ToString() + ".js" + "|" + ElementToSearch);
			}

			content = content.Replace(ElementToSearch, "DummyScript" + DummyIndex.ToString() + ".js");

			return content;
		}

		string FixReferences(string FormContent, string itemToFix)
		{
			int tagIndex = 0;
			int closingTag = 0;
			string tagToFix = string.Empty;
			string pathToFix;
			string fileName = string.Empty;

			Uri dirUri;
			Uri toReplaceUri;
			string toReplaceReference;

			//Obtiene el path de la pagina que está ejecutando.
			string executionPath = Context.Request.ServerVariables["PATH_INFO"];
			string currDirectory = Server.MapPath(executionPath.Substring(0, executionPath.LastIndexOf('/') + 1));

			//Buscamos la primer aparición del tag a reparar
			tagIndex = FormContent.IndexOf('<' + itemToFix, tagIndex);

			//Mientras se siga encontrando scripts
			while (tagIndex != -1 && tagIndex + 5 < FormContent.Length)
			{
				pathToFix = string.Empty;

				//Se busca el tag de cierre
				closingTag = FormContent.IndexOf('>', tagIndex);

				//Se obtiene el texto del tag a reparar
				tagToFix = FormContent.Substring(tagIndex, closingTag - tagIndex);

				//Si es link obtendremos el path del href, sino del src
				if (itemToFix == "link")
				{
					if (tagToFix.IndexOf("href=") > 0)
					{
						pathToFix = tagToFix.Substring(tagToFix.IndexOf("href=") + 5, tagToFix.IndexOf("\"", tagToFix.IndexOf("src=") + 5) - tagToFix.IndexOf("src=") - 3);
					}
				}
				else
				{
					if (tagToFix.IndexOf("src=") > 0)
					{
						pathToFix = tagToFix.Substring(tagToFix.IndexOf("src=") + 4, tagToFix.IndexOf("\"", tagToFix.IndexOf("src=") + 5) - tagToFix.IndexOf("src=") - 3);
					}
				}

				//Si obtuvo algo...
				if (!string.IsNullOrEmpty(pathToFix))
				{
					//Quitamos las comillas
					pathToFix = pathToFix.Replace("\"", string.Empty);

					FileInfo fInfo = new FileInfo(pathToFix);

					//Si la ruta relativa no existe hay que repararla
					if (!fInfo.Exists)
					{
						//En este caso como la página está en la raiz el curr directory está en la raiz
						dirUri = new Uri(currDirectory);
						//Buscamos primero en form
						toReplaceUri = new Uri(Server.MapPath("forms"));
						//Creamos el path
						toReplaceReference = Path.Combine(dirUri.MakeRelativeUri(toReplaceUri).ToString(), fInfo.Name);

						fInfo = new FileInfo(Path.Combine(currDirectory, toReplaceReference));
						//Si esta vez existe
						if (fInfo.Exists)
						{
							//Reemplazamos el path
							toReplaceReference = toReplaceReference.Replace('\\', '/');
							FormContent = FormContent.Replace(pathToFix, toReplaceReference);
							FormContent = FormContent.Replace("language=\"JavaScript\"", string.Empty);
							//Sumamos a los path para reemplazar al guardar y el path viejo separados por pipes
							FixedReferences.Add(toReplaceReference + '|' + pathToFix);
						}
						else
						{
							//Sino,buscamos en el content
							dirUri = new Uri(currDirectory);
							toReplaceUri = new Uri(Server.MapPath("Content"));
							toReplaceReference = Path.Combine(dirUri.MakeRelativeUri(toReplaceUri).ToString(), fInfo.Name);

							fInfo = new FileInfo(Path.Combine(Server.MapPath("/"), toReplaceReference));
							if (fInfo.Exists)
							{
								toReplaceReference = toReplaceReference.Replace('\\', '/');
								FormContent = FormContent.Replace(pathToFix, "/" + toReplaceReference);
								//Sumamos a los path para reemplazar al guardar y el path viejo separados por pipes
								FixedReferences.Add(toReplaceReference + '|' + pathToFix);
							}
							else
							{
								//Por ultimo buscamos en script
								dirUri = new Uri(currDirectory);
								toReplaceUri = new Uri(Server.MapPath("script"));
								toReplaceReference = Path.Combine(dirUri.MakeRelativeUri(toReplaceUri).ToString(), fInfo.Name);

								fInfo = new FileInfo(Path.Combine(Server.MapPath("/"), toReplaceReference));
								if (fInfo.Exists)
								{
									toReplaceReference = toReplaceReference.Replace('\\', '/');
									FormContent = FormContent.Replace(pathToFix, toReplaceReference);
									//Sumamos a los path para reemplazar al guardar y el path viejo separados por pipes
									FixedReferences.Add(toReplaceReference + '|' + pathToFix);
								}
							}
						}
					}
					fInfo = null;
				}

				tagIndex = FormContent.IndexOf('<' + itemToFix, tagIndex + 1);
			}

			return FormContent;
		}
		#endregion

		#region "Funciones Auxiliares"
		public String GetDocTypeName()
		{
			int DocTypeId;
			if (Request.QueryString["EId"] != null) DocTypeId = int.Parse(Request.QueryString["EId"]); else DocTypeId = 0;
            
			if (DocTypeId > 0)
			{
                DocTypesBusiness DTB = new DocTypesBusiness();

                return DTB.GetDocTypeName(DocTypeId);
			}
			return "No se ha seleccionado una Entidad principal para el formulario,<br/> opciones especificas de la entidad no estaran disponibles.";
		}
		

		#endregion

		#region "Funciones de Llenado de Nodos"

		//ML: Indices de la Entidad
		private void PopulateTreeViewFromIndexs(RadTreeNodeCollection rootnodes, 
			int DocTypeId)
		{
            IndexsBusiness IB = new IndexsBusiness();
            List<IIndex> Indexs = new List<IIndex>();
			RadTreeNode node1 = new RadTreeNode();
			node1.Text = "Indices";
			node1.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
			node1.Attributes["Category"] = "Indexs";
			node1.Value = "Indexs";
			rootnodes.Add(node1);

			Indexs =  IB.GetIndexsSchemaAsListOfDT(DocTypeId);
			foreach (IIndex I in Indexs)
			{
				String TagId = String.Empty;
				String TagClass = String.Empty;
				String TagAttributes = String.Empty;
				String TagOpen = String.Empty;
				String TagClose = String.Empty;

				//Define el nodo de tipo índice
				RadTreeNode node = new RadTreeNode();
				node.Text = I.Name;
				node.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				node.Attributes["Category"] = "Index";
				
				//Completa el id
				TagId = "zamba_index_" + I.ID;
				
				//Completa el valor por defecto
				if (!string.IsNullOrEmpty(I.DefaultValue))
				{
					TagClass += " haveDefaultValue";
					TagAttributes += " DefaultValue=\"" + I.DefaultValue + "\"";
				}
				
				//Completa si el campo es requerido
				if (I.Required)
				{
					TagClass += " isRequired";                    
				}
				
				//Completa la longitud del índice
				if (I.Len > 0)
				{
					TagClass += " length";
					TagAttributes += " length=\"" + I.Len.ToString() + "\"";
				}

				//Completa el tipo de dato con las validaciones necesarias
				switch (I.Type)
				{
					case IndexDataType.Fecha:
					case IndexDataType.Fecha_Hora:
						TagClass += " dataType";
						TagAttributes += " dataType=\"date\"";
						break;
					case IndexDataType.Moneda:
					case IndexDataType.Numerico_Decimales:
						TagClass += " dataType";
						TagAttributes += " dataType=\"decimal_2_16\"";
						break;
					case IndexDataType.Numerico:
					case IndexDataType.Numerico_Largo:
						TagClass += " dataType";
						TagAttributes += " dataType=\"numeric\"" ;
						break;
					case IndexDataType.Alfanumerico:
					case IndexDataType.Alfanumerico_Largo:
					case IndexDataType.None:
					case IndexDataType.Si_No:
					default:
						break;
				}
				 
				//Arma el cuerpo html del índice dependiendo del tipo de índice
				switch (I.DropDown)
				{
					case IndexAdditionalType.AutoSustitución:
						TagOpen = "<label class='IndexLabel'> " + I.Name + " </label> &nbsp; <select ";
						TagClose = "></select>";
						break;
					case IndexAdditionalType.AutoSustituciónJerarquico:
						TagOpen = "<label class='IndexLabel'> " + I.Name + " </label>  &nbsp; <select ";
						TagClose = "></select>";
						break;
					case IndexAdditionalType.DropDown:
						TagOpen = "<label class='IndexLabel' > " + I.Name + " </label>  &nbsp; <select ";
						TagClose = "></select>";
						break;
					case IndexAdditionalType.DropDownJerarquico:
						TagOpen = "<label class='IndexLabel' > " + I.Name + " </label>  &nbsp; <select ";
						TagClose = "></select>";
						break;
					case IndexAdditionalType.LineText:
						if (I.Len > 300)
						{
							TagOpen = "<label class='IndexLabel' > " + I.Name + " </label>  &nbsp;  <textarea rows='8' cols='155' ";
							TagClose = "/>";
						}
						else
						{
							TagOpen = "<label class='IndexLabel' > " + I.Name + " </label>  &nbsp;  <input type='text'";
							TagClose = "/>";
						}
						break;
					case IndexAdditionalType.NoIndex:
						break;
					default:
						TagOpen = "<label class='IndexLabel' > " + I.Name + " </label>  &nbsp; <input type='text'";
						TagClose = "/>";
						break;
				}

				//Guarda toda la configuración en el atributo TAG del nodo. Al soltar el índice sobre el editor html se procesará dicho atributo.
				node.Attributes["Tag"] = String.Format("{3} id=\"{0}\" name=\"{0}\" class=\"{1}\" {2} {4}", TagId, TagClass, TagAttributes, TagOpen, TagClose);

				node.Value = I.ID.ToString();
				node1.Nodes.Add(node);
			}

            IB = null;
		}

		//ML: Grilla de Asociados
		private void PopulateTreeViewFromDocAsociated(RadTreeNodeCollection rootnodes, 
			int DocTypeId)
		{
			RadTreeNode node1 = new RadTreeNode();
			node1.Text = "Grilla de Entidades Asociadas";
			node1.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
			node1.Attributes["Category"] = "Asociados";
			node1.Value = "Asociados";
			rootnodes.Add(node1);

		   var AsociatedDocTypes = Zamba.Core.DocTypes.DocAsociated.DocAsociatedBusiness.GetUniqueDocTypeIdsAsociation(DocTypeId);

		   foreach (Int64 DocTypeId2 in AsociatedDocTypes)
		   {			  
				   RadTreeNode node = new RadTreeNode();
				   node.Text = DTB.GetDocType(DocTypeId2).Name;
				   node.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				   node.Attributes["Category"] = "DocAsociated";

				   String TagId = String.Empty;
				   String TagClass = String.Empty;
				   String OtherAttributes = String.Empty;
				   String TagType = String.Empty;
				   String TagClose = String.Empty;
				   String TagValue = String.Empty;

				   TagId = "zamba_associated_documents_" + DocTypeId2.ToString();

				   TagClass += " tablesorter";

				   TagType = "<table ";
				   TagClose = "><caption>" + node.Text + "</caption><tbody></tbody></table>";

				   node.Attributes["Tag"] = String.Format("{3} id='{0}' class='{1}' {2} {4}", TagId, TagClass, OtherAttributes, TagType, TagClose);
				   node.Value = DocTypeId2.ToString();
				   node1.Nodes.Add(node);
			   
		   }
           
		}

		//ML: Indices de Entidades Asociadas
		private void PopulateTreeViewFromDocAsociatedIndexs(RadTreeNodeCollection rootnodes, 
			int DocTypeId)
		{
			RadTreeNode node1 = new RadTreeNode();
			node1.Text = "Indices de Entidades Asociadas";
			node1.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
			node1.Attributes["Category"] = "IndicesAsociados";
			node1.Value = "IndicesAsociados";
			rootnodes.Add(node1);

            IndexsBusiness IB = new IndexsBusiness();

            List<Int64> AsociatedDocTypes = Zamba.Core.DocTypes.DocAsociated.DocAsociatedBusiness.GetUniqueDocTypeIdsAsociation(DocTypeId);
		
			foreach (Int64 DocTypeId2 in AsociatedDocTypes)
			{
					RadTreeNode node = new RadTreeNode();
					node.Text = DTB.GetDocType(DocTypeId2).Name;
					node.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
					node.Attributes["Category"] = "DocAsociatedIndex";
					node1.Nodes.Add(node);


					List<IIndex> Indexs = new List<IIndex>();

					Indexs = IB.GetIndexsSchemaAsListOfDT(DocTypeId);
					foreach (IIndex I in Indexs)
					{
						RadTreeNode node2 = new RadTreeNode();
						node2.Text = I.Name;
						node2.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
						node2.Attributes["Category"] = "Index";
						//<select id="zamba_index_1220" name="zamba_index_1220" style="width: 175px" class="required GeneralFormControl">
						//</select>
						// <input id="zamba_index_1214" name="zamba_index_1214" size="28" onchange="$(this).valid(); MakeCalendarAfterValue('zamba_index_1214', 'zamba_index_1215'); "/>

						String TagId = String.Empty;
						String TagClass = String.Empty;
						String OtherAttributes = String.Empty;
						String TagType = String.Empty;
						String TagClose = String.Empty;
						String TagValue = String.Empty;


						TagId = "zamba_asoc_" + DocTypeId2.ToString() + "_index_" + I.ID;
						TagValue = I.DefaultValue;


						if (I.Required == true)
						{
							TagClass += " Required";
						}

						switch (I.Type)
						{
							case IndexDataType.Alfanumerico:
								break;
							case IndexDataType.Alfanumerico_Largo:
								break;
							case IndexDataType.Fecha:
								TagClass += " calendar";
								break;
							case IndexDataType.Fecha_Hora:
								TagClass += " calendar";
								break;
							case IndexDataType.Moneda:
								TagClass += " OnlyNums";
								break;
							case IndexDataType.None:
								break;
							case IndexDataType.Numerico:
								TagClass += " OnlyNums";
								break;
							case IndexDataType.Numerico_Decimales:
								TagClass += " OnlyNums";
								break;
							case IndexDataType.Numerico_Largo:
								TagClass += " OnlyNums";
								break;
							case IndexDataType.Si_No:
								break;
							default:
								break;
						}

						switch (I.DropDown)
						{
							case IndexAdditionalType.AutoSustitución:
								TagType = "<select ";
								TagClose = "></select>";
								break;
							case IndexAdditionalType.AutoSustituciónJerarquico:
								TagType = "<select ";
								TagClose = "></select>";
								break;
							case IndexAdditionalType.DropDown:
								TagType = "<select ";
								TagClose = "></select>";
								break;
							case IndexAdditionalType.DropDownJerarquico:
								TagType = "<select ";
								TagClose = "></select>";
								break;
							case IndexAdditionalType.LineText:
								TagType = "<input type=\"text\"";
								TagClose = "/>";

								break;
							case IndexAdditionalType.NoIndex:
								break;
							default:
								TagType = "<input type=\"text\"";
								TagClose = "/>";
								break;
						}

						node2.Attributes["Tag"] = String.Format("{3} id=\"{0}\" class=\"{1}\" {2} {4}", TagId, TagClass, OtherAttributes, TagType, TagClose);
						//                node.Attributes["Tag"] = String.Format("<input id='zamba_index_{0}'/>", I.ID);
						node2.Value = I.ID.ToString();
						node.Nodes.Add(node2);
					}
			}
            IB = null;
		}

		//ML: Controles de Tabs, Boton de Guardar y Cancelar, Boton de Ejecucion de Reglas
		private void PopulateTreeViewFromJquery(RadTreeNodeCollection rootnodes)
		{
				//RadTreeNode node = new RadTreeNode();
				//node.Text = "Control de Tabs/Solapas";
				//node.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				//node.Attributes["Category"] = "Control";
				String TagClass = String.Empty;
				String TagType = String.Empty;
				String TagClose = String.Empty;
				//TagClass = " tabber";
				//TagType = "<div ";
				//TagClose = "><div class='tabbertab'><h2>Nombre Solapa 1</h2></div></div>";
				//node.Attributes["Tag"] = String.Format("{1} class='{0}' {2}", TagClass, TagType, TagClose);
				//rootnodes.Add(node);

				// TagClass = String.Empty;
				// TagType = String.Empty;
				// TagClose = String.Empty;

				//RadTreeNode nodetab = new RadTreeNode();
				//nodetab.Text = "Tab/Solapa";
				//nodetab.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				//nodetab.Attributes["Category"] = "Control";
				//TagClass = " tabbertab";
				//TagType = "<div ";
				//TagClose = "><h2>Nombre Solapa</h2></div>";
				//nodetab.Attributes["Tag"] = String.Format("{1} class='{0}' {2}", TagClass, TagType, TagClose);
				//rootnodes.Add(nodetab);

				//TagClass = String.Empty;
				//TagType = String.Empty;
				//TagClose = String.Empty;

				RadTreeNode nodesave = new RadTreeNode();
				nodesave.Text = "Boton Guardar";
				nodesave.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				nodesave.Attributes["Category"] = "Control";
				TagClass = " submit";
				TagType = "<input type=\"submit\" id=\"zamba_save\" name=\"zamba_save\"  value=\"Guardar\"";
				TagClose = "></input>";
				nodesave.Attributes["Tag"] = String.Format("{1} class=\"{0}\" {2}", TagClass, TagType, TagClose);
				rootnodes.Add(nodesave);

				TagClass = String.Empty;
				TagType = String.Empty;
				TagClose = String.Empty;

				RadTreeNode nodecancel = new RadTreeNode();
				nodecancel.Text = "Boton Cancelar";
				nodecancel.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				nodecancel.Attributes["Category"] = "Control";
				TagClass = " submit";
				TagType = "<input type=\"submit\" id=\"zamba_cancel\" name=\"zamba_cancel\" value=\"Cancelar\"";
				TagClose = "></input>";
				nodecancel.Attributes["Tag"] = String.Format("{1} class=\"{0}\" {2}", TagClass, TagType, TagClose);
				rootnodes.Add(nodecancel);

				TagClass = String.Empty;
				TagType = String.Empty;
				TagClose = String.Empty;
				String TagId = String.Empty;

				//ML: Hay que agregar un popup que liste en un arbol, los WF, Etapas y Reglas existentes, que permita elejir una regla y agregar el ID de Regla al Tag.
				RadTreeNode nodeexecuterule = new RadTreeNode();
				nodeexecuterule.Text = "Boton Ejecutar Regla";
				nodeexecuterule.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				nodeexecuterule.Attributes["Category"] = "BtnExecuteRule";
				TagClass = " submit";
				TagId = "zamba_rule_";
				TagType = "<input type=\"submit\" id=\"zamba_rule\" value=\"Ejecutar\"";
				TagClose = "></input>";
				nodeexecuterule.Attributes["Tag"] = String.Format("{1} class=\"{0}\" id=\"{3}\" {2}", TagClass, TagType, TagClose, TagId);
				rootnodes.Add(nodeexecuterule);
		}

		//ML: Imagenes 
		private void PopulateTreeViewFromDirectory(RadTreeNodeCollection nodes,
				string _path)
		{
			string[] _directories = Directory.GetDirectories(_path);
			foreach (string _directory in _directories)
			{
				RadTreeNode node = new RadTreeNode();
				node.Height = 22;
				node.Text = Path.GetFileName(_directory);
				node.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				node.Attributes["Category"] = "Folder";
				nodes.Add(node);
				PopulateTreeViewFromDirectory(node.Nodes, _directory);
			}
			string[] _files = Directory.GetFiles(_path);
			foreach (string _file in _files)
			{
				if (IsSupportedFileType(_file))
				{
					RadTreeNode node = new RadTreeNode();
					node.Text = Path.GetFileName(_file);
					node.ImageUrl = ConvertAbsoluteToRelative(_file);// "~/Content/Images/" + Path.GetFileName(_file);
					node.Attributes["Category"] = "Image";
					node.Value = ConvertAbsoluteToRelative(_file);
					nodes.Add(node);
				}
			}
		}

		//ML: Scripts 
		private void PopulateTreeViewScriptsFromDirectory(RadTreeNodeCollection nodes,
				string _path)
		{
			string[] _directories = Directory.GetDirectories(_path);
			foreach (string _directory in _directories)
			{
				RadTreeNode node = new RadTreeNode();
				node.Height = 22;
				node.Text = Path.GetFileName(_directory);
				node.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
				node.Attributes["Category"] = "Folder";
				nodes.Add(node);
				PopulateTreeViewScriptsFromDirectory(node.Nodes, _directory);
			}
			string[] _files = Directory.GetFiles(_path,"*.js;*.css");
			foreach (string _file in _files)
			{
				if (IsSupportedFileType(_file))
				{
					RadTreeNode node = new RadTreeNode();
					node.Text = Path.GetFileName(_file);
					node.ImageUrl = "~/Content/Images/toolbars/bullet_ball_blue.png";
//                    node.ImageUrl = ConvertAbsoluteToRelative(_file);// "~/Content/Images/" + Path.GetFileName(_file);
					node.Attributes["Category"] = "Script";
					node.Value = ConvertAbsoluteToRelative(_file);
					nodes.Add(node);
				}
			}
		}
		
		
		#endregion
		
		private bool IsSupportedFileType(string file)
		{
			string pat = "(\\.gif|\\.jpg|\\.jpeg|\\.png)$";
			return Regex.IsMatch(file, pat, RegexOptions.IgnoreCase);
		}

		private string ConvertAbsoluteToRelative(string absolute)
		{
			string relative = absolute.Replace(MapPath(Request.ApplicationPath), Request.ApplicationPath);
			return relative.Replace("\\", "/");
		}

		/// <summary>
		/// Inserta o guarda el formulario
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			SaveForm();
		}
		
		protected void SaveForm()
		{
			try
			{
				int FormId;
				if (Request.QueryString["FId"] != null) FormId = int.Parse(Request.QueryString["FId"]); else FormId = 0;

				ZwebForm ZForm = null;

				if (FormId > 0)
				{
					ZForm = FB.GetForm(FormId);

					StreamWriter sw = null;
					try
					{
						sw = new StreamWriter(ZForm.Path);
					}
					catch (Exception)
					{
						if (File.GetAttributes(ZForm.Path).ToString().Contains("ReadOnly"))
						{
							File.SetAttributes(ZForm.Path,FileAttributes.Normal);
							sw = new StreamWriter(ZForm.Path);
						}
						else
						{
							throw ;
						}
					}
					
					string contentToSave = this.RadEditor1.Content;

					contentToSave =  VerifyAndFixForm(contentToSave);
					contentToSave = RemoveMasterRegion(contentToSave, ZForm.Type, false);

                    ZOptBusiness zopt = new ZOptBusiness();
                    String CurrentTheme = zopt.GetValue("CurrentTheme");
                    zopt = null;

					contentToSave = HTML.CleanFormReferences(contentToSave, ZForm.Type, CurrentTheme);

					var ContentArray = contentToSave.ToCharArray();

					sw.Write(ContentArray);
					sw.Flush();
					sw.Close();
					sw.Dispose();

					//Guarda la fecha de modificacion
					FB.UpdateModifiedDate(FormId);
				}
				else
				{ 
					FormId = FB.GetFormNewId() ;
					Int64 DocTypeID = int.Parse(Request.QueryString["EId"]);
					Int32 FormTypeId = int.Parse(Request.QueryString["FTId"]);
					String name = Request.QueryString["Name"];
					String path = Session["Path: " + name].ToString();

					StreamWriter sw = new StreamWriter(path);

					string contentToSave = this.RadEditor1.Content;
					contentToSave = VerifyAndFixForm(contentToSave);

					contentToSave = RemoveMasterRegion(contentToSave, (FormTypes)FormTypeId, false);

					var ContentArray = contentToSave.ToCharArray();
					
					sw.Write(ContentArray);
					sw.Flush();
					sw.Close();
					sw.Dispose();


                    ZOptBusiness zopt = new ZOptBusiness();
                    String CurrentTheme = zopt.GetValue("CurrentTheme");
                    zopt = null;
					
					CopyFiles(Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme) + "\\images", path.Remove(path.LastIndexOf("\\")) + "\\images");

					ZForm = new ZwebForm(FormId, name, name, (FormTypes)FormTypeId, path, (Int32)DocTypeID, true, true);

					FB.InsertForm(ZForm);

					Session.Remove("Path: " + name);
				
				}

				lblerror.Visible = true;
				lblerror.Text = "Formulario Generado con éxito.";
				//if (ZForm != null)
				//{
				//    //String URL = "FormEditorDD.aspx?EId=" + ZForm.DocTypeId.ToString() + "&FId=" + ZForm.ID.ToString();
				//    Response.Redirect("FormEditorDD.aspx");
				//}

			}
			catch(Exception ex)
			{
				Zamba.Core.ZClass.raiseerror(ex);
			}
		}

		private string RemoveMasterRegion(string FormContent, FormTypes FormType, bool IsNewForm)
		{
			string strToReturn = FormContent;
			int headIndex = FormContent.IndexOf("<head>");

			if (FormType.ToString().Contains("Web"))
			{
				strToReturn = ReplaceBlock(FormContent, STARTMASTERREGION, ENDMASTERREGION, string.Empty);
			}
			else
			{
				strToReturn = ReplaceBlock(FormContent, STARTMASTERREGION, ENDMASTERREGION,  WINDOWSFORMSMASTERREGION);
			}

			return strToReturn;
		}

		private string ReplaceBlock(string Content, string StartCode, string EndCode, string ReplaceValue)
		{
			string strToReturn;
			int startIndex = Content.IndexOf(StartCode);
			int endIndex = Content.IndexOf(EndCode) + EndCode.Length;

			if (startIndex < 0 || endIndex < startIndex)
				return Content;

			string toReplaceBlock = Content.Substring(startIndex, endIndex - startIndex);

			strToReturn = Content.Replace(toReplaceBlock, ReplaceValue);

			return strToReturn;
		}

		protected String VerifyAndFixForm(String contentToSave)
		{
			contentToSave = contentToSave.Replace("><", "> <");

			string[] attributesToValidate = contentToSave.Split(' ');
			StringBuilder sbToReturn = new StringBuilder();
			int maxAttr = attributesToValidate.Length;
			int idIndex;
			int endIndex;
			string Id;

			for (int i = 0; i < maxAttr; i++)
			{
				if (!attributesToValidate[i].Contains("id=" + '\"'))
				{
					if (attributesToValidate[i].Contains("id="))
					{
						idIndex = attributesToValidate[i].IndexOf('=');
						endIndex = attributesToValidate[i].LastIndexOf('>');

						if (endIndex != -1)
							Id = attributesToValidate[i].Substring(idIndex + 1, endIndex - idIndex - 1);
						else
							Id = attributesToValidate[i].Substring(idIndex + 1);

						attributesToValidate[i] = attributesToValidate[i].Replace(Id, '\"' + Id + '\"');
					}
				}

				if (!attributesToValidate[i].Contains("name=" + '\"'))
				{
					if (attributesToValidate[i].Contains("name="))
					{
						idIndex = attributesToValidate[i].IndexOf('=');
						endIndex = attributesToValidate[i].LastIndexOf('>');

						if (endIndex != -1)
							Id = attributesToValidate[i].Substring(idIndex + 1, endIndex - idIndex - 1);
						else
							Id = attributesToValidate[i].Substring(idIndex + 1);

						attributesToValidate[i] = attributesToValidate[i].Replace(Id, '\"' + Id + '\"');
					}
				}

				if (!attributesToValidate[i].Contains("value=" + '\"'))
				{
					if (attributesToValidate[i].Contains("value="))
					{
						idIndex = attributesToValidate[i].IndexOf('=');
						endIndex = attributesToValidate[i].LastIndexOf('>');

						if (endIndex != -1)
							Id = attributesToValidate[i].Substring(idIndex + 1, endIndex - idIndex - 1);
						else
							Id = attributesToValidate[i].Substring(idIndex + 1);

						attributesToValidate[i] = attributesToValidate[i].Replace(Id, '\"' + Id + '\"');
					}
				}

				if (!attributesToValidate[i].Contains("type=" + '\"'))
				{
					if (attributesToValidate[i].Contains("type="))
					{
						idIndex = attributesToValidate[i].IndexOf('=');
						endIndex = attributesToValidate[i].LastIndexOf('>');

						if (endIndex != -1)
							Id = attributesToValidate[i].Substring(idIndex + 1, endIndex - idIndex - 1);
						else
							Id = attributesToValidate[i].Substring(idIndex + 1);

						attributesToValidate[i] = attributesToValidate[i].Replace(Id, '\"' + Id + '\"');
					}
				}

				if (!attributesToValidate[i].Contains("class=" + '\"'))
				{
					if (attributesToValidate[i].Contains("class="))
					{
						idIndex = attributesToValidate[i].IndexOf('=');
						endIndex = attributesToValidate[i].LastIndexOf('>');

						if (endIndex != -1)
							Id = attributesToValidate[i].Substring(idIndex + 1, endIndex - idIndex - 1);
						else
							Id = attributesToValidate[i].Substring(idIndex + 1);

						attributesToValidate[i] = attributesToValidate[i].Replace(Id, '\"' + Id + '\"');
					}
				}

				sbToReturn.Append(attributesToValidate[i] + ' ');
			}



			return sbToReturn.ToString();
		}

		protected void BtnGoBack_Click(object sender, EventArgs e)
		{
			Response.Redirect("FormEditorMenu.aspx");
		}
		
		protected void BtnTestForm_Click(object sender, EventArgs e)
		{
			try
			{
			   
				int FormId;
				if (Request.QueryString["FId"] != null) FormId = int.Parse(Request.QueryString["FId"]); else FormId = 0;

				if (FormId > 0)
				{
                    ZOptBusiness zopt = new ZOptBusiness();
                    String TestUrl = zopt.GetValue("WFETestPage");
					zopt = null;
					String ScriptString = "$(document).ready(function() { window.open('FormEditorTest.aspx?FId=" + FormId + "'); });";
					ClientScript.RegisterClientScriptBlock(Page.GetType(),"TestScript", ScriptString, true);
				}

			}
			catch { }
		  }
		
		/// <summary>
		/// Copia todos los archivos de la carpeta content a donde esta el formulario
		/// </summary>
		/// <param name="originalPath"></param>
		/// <param name="copyPath"></param>
		protected void CopyFiles(String originalPath, String copyPath)
		{
			foreach (String f in Directory.GetFiles(originalPath))
				if (File.Exists(copyPath + f.Remove(0,f.LastIndexOf("\\")))==false)
				File.Copy(f, copyPath + f.Remove(0,f.LastIndexOf("\\")));

			foreach (String d in Directory.GetDirectories(originalPath))
			{
				if (Directory.Exists(copyPath + d.Remove(0, d.LastIndexOf("\\"))) == false)
				{
					Directory.CreateDirectory(copyPath + d.Remove(0, d.LastIndexOf("\\")));
				}

				CopyFiles(d, copyPath + d.Remove(0, d.LastIndexOf("\\")));
			}
		}
	}
}
