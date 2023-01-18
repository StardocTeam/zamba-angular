namespace Zamba.FormBrowser.Razor
{
    using System.Web.Razor;
    using System.Collections.Generic;
    using Zamba.Core;
    using System.IO;
    using Microsoft.CSharp;
    using System.CodeDom.Compiler;
    using System.Data;
    using System;
    using System.Reflection;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using Zamba.Membership;
    using System.Diagnostics;

    public class ZRazorEngine
    {
        private TemplateBase _currentTemplate;
        private RazorTemplateEngine _engine;
        private string _stringTemplate;
        private IZwebForm _currentForm;
        private string _classFormName;
        private string _classAssemblyName;
        private string _assemblyPath;

        private TemplateBase CurrentTemplate
        {
            get
            {
                return _currentTemplate;
            }
            set
            {
                _currentTemplate = value;
            }
        }

        public ZRazorEngine(IZwebForm form, string stringTemplate)
            : this(new string[] { "System", "System.Data", "System.Collections.Generic", "System.Web.UI" }, form, stringTemplate)
        {
        }

        public ZRazorEngine(IEnumerable<string> nameSpacesToImport, IZwebForm form, string stringTemplate)
        {
            // a. Use the C# language (you could detect this based on the file extension if you want to)
            RazorEngineHost host = new RazorEngineHost(new CSharpRazorCodeLanguage());

            // b. Set the base class
            host.DefaultBaseClass = typeof(TemplateBase).FullName;

            // c. Set the output namespace and type name
            host.DefaultNamespace = "GeneratedForms";
            _classFormName = GetClassFormName(form);
            host.DefaultClassName = _classFormName;

            // d. Set the imported namespaces
            foreach (string nameSpace in nameSpacesToImport)
            {
                host.NamespaceImports.Add(nameSpace);
            }

            // Create the template engine using this host
            _engine = new RazorTemplateEngine(host);

            _stringTemplate = stringTemplate;
            _currentForm = form;

            _classAssemblyName = GetAssemblyName(_classFormName);
            _assemblyPath = Path.Combine(RazorAssemblyDirectory.FullName, _classAssemblyName);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Clase: " + _classAssemblyName + " Ruta: " + _assemblyPath);
        }

        public ZRazorEngine(IZwebForm form)
            : this(new string[] { "System", "System.Data", "System.Collections.Generic", "System.Web.UI" }, form, string.Empty)
        {
        }

        public string Execute(Hashtable hs)
        {
            try
            {
                if (_currentForm.Rebuild)
                {
                      ZTrace.WriteLineIf(ZTrace.IsVerbose,"Formulario marcado para Rebuild");
                    CurrentTemplate = CompileForm();
                    _currentForm.Rebuild = false;
                    SetFormAsBuilded();

                }
                else
                {
                      ZTrace.WriteLineIf(ZTrace.IsVerbose,"Formulario NO marcado para Rebuild");
                      if (!File.Exists(_assemblyPath))
                      {
                          ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("La DLL {0} no existe, se recompilara el formulario", _assemblyPath));

                          CurrentTemplate = CompileForm();
                      }
                      else
                      {
                          ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("La DLL {0} existe, no es necesario recompilar el formulario", _assemblyPath));
                          CurrentTemplate = GetRazorClass(_assemblyPath, false);
                      }
                }

                CurrentTemplate.DataSource = hs;
                CurrentTemplate.Execute();
                String CurrentTemplateBuffer = CurrentTemplate.Buffer.ToString();
                return CurrentTemplateBuffer;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
            finally
            {
                if (CurrentTemplate != null && CurrentTemplate.Buffer != null)
                {
                    CurrentTemplate.Buffer.Clear();
                    CurrentTemplate = null;
                }
            }
        }

        private void SetFormAsBuilded()
        {
            FormBusiness FB = new FormBusiness();
            FB.SetFormBuilded(_currentForm.ID);
            FB = null;
        }

        #region RazorClassGeneration
        public TemplateBase CompileForm()
        {
  	
	       if (File.Exists(_assemblyPath))
            { 
    ZTrace.WriteLineIf(ZTrace.IsVerbose,String.Format("Se elimina la DLL {0} existente",_assemblyPath));

                File.Delete(_assemblyPath);
            }
            GeneratorResults razorResult = GenerateTemplateCode();

            CSharpCodeProvider codeProvider;
            string compiledCode;
            GenerateCode(razorResult, out codeProvider, out compiledCode);
                 ZTrace.WriteLineIf(ZTrace.IsVerbose,"Se compila la DLL");

            CompilerResults results = CompileInAssembly(razorResult, codeProvider);

            if (results.Errors.HasErrors)
            {
                 ZTrace.WriteLineIf(ZTrace.IsVerbose,"Compilacion con errores");
     CompilerError err = results.Errors
                                           .OfType<CompilerError>()
                                           .Where(ce => !ce.IsWarning)
                                           .First();
               Exception ex = new Exception(String.Format("Error Compiling Template: ({0}, {1}) {2} \r\n CompiledCode: {3}",
                                              err.Line, err.Column, err.ErrorText, compiledCode));
                ZClass.raiseerror(ex);
                throw ex;
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Compilacion correcta");
                return GetRazorClass(_assemblyPath, true);
            }
      
   
         }

        private TemplateBase GetRazorClass(string outputAssemblyName, bool reloadClass)
        {
            if (!reloadClass && _hsSingletonFormTypeInstances.ContainsKey(outputAssemblyName))
            {
                 ZTrace.WriteLineIf(ZTrace.IsVerbose,"Se reutiliza Assembly de cache");
                return (TemplateBase)_hsSingletonFormTypeInstances[outputAssemblyName];
            }
            // Load the assembly

            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se carga DLL {0}", outputAssemblyName));
            Assembly asm = Assembly.LoadFrom(outputAssemblyName);
            if (asm == null)
            {
               
                Exception ex =  new Exception("Error loading template assembly");
             ZClass.raiseerror(ex);
                throw ex;
            }
            
            // Get the template type
            ZTrace.WriteLineIf(ZTrace.IsVerbose, _engine.Host.DefaultClassName);
            Type typ = asm.GetType(string.Format("GeneratedForms.{0}", _engine.Host.DefaultClassName));
            if (typ == null)
            {
             Exception ex =     new Exception(string.Format("Could not find type RazorOutput.Template in assembly {0}", asm.FullName));
                ZClass.raiseerror(ex);
                throw ex;
            }
            else
            {
                TemplateBase newTemplate = Activator.CreateInstance(typ) as TemplateBase;
                if (newTemplate == null)
                {
                     Exception ex =   new Exception("Could not construct RazorOutput.Template or it does not inherit from TemplateBase");
                ZClass.raiseerror(ex);
                throw ex;
                }
                else
                {
                    _hsSingletonFormTypeInstances[outputAssemblyName] = newTemplate;
                    return newTemplate;
                }
            }
        }

        private CompilerResults CompileInAssembly(GeneratorResults razorResult, CSharpCodeProvider codeProvider)
        {
            return codeProvider.CompileAssemblyFromDom(
                new CompilerParameters(new string[] { //Assemblies to reference
                    typeof(ZRazorEngine).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"), 
                    typeof(DataTable).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"), 
                    typeof(System.ComponentModel.MarshalByValueComponent).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"), 
                    typeof(System.Xml.Serialization.IXmlSerializable).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"),
                    typeof(System.Web.UI.HtmlTextWriter).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"),
                }, _assemblyPath),
            razorResult.GeneratedCode);
        }

        private static void GenerateCode(GeneratorResults razorResult, out CSharpCodeProvider codeProvider, out string compiledCode)
        {
            codeProvider = new CSharpCodeProvider();

            // Generate the code
            using (StringWriter sw = new StringWriter())
            {
                codeProvider.GenerateCodeFromCompileUnit(razorResult.GeneratedCode, sw, new CodeGeneratorOptions());
                compiledCode = sw.GetStringBuilder().ToString();
            }
        }

        private GeneratorResults GenerateTemplateCode()
        {
            // Generate code for the template
            GeneratorResults razorResult = null;
            using (TextReader rdr = new StringReader(_stringTemplate))
            {
                razorResult = _engine.GenerateCode(rdr);
            }
            return razorResult;
        }
        #endregion

        #region Static

        static Hashtable _hsSingletonFormTypeInstances = new Hashtable();
        static DirectoryInfo RazorAssemblyDirectory = MembershipHelper.AppTempDir("\\RazorClasses");

        public static bool IsFormGenerated(IZwebForm form)
        {
            string assemblyPath = Path.Combine(RazorAssemblyDirectory.FullName, GetAssemblyName(GetClassFormName(form)));
            if (_hsSingletonFormTypeInstances.ContainsKey(form) || File.Exists(assemblyPath))
                return true;
            else
                return false;
        }

        private static string GetClassFormName(IZwebForm form)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(form.Name.Trim());
                sb.Replace(' ', '_');
                sb.Replace('-', '_');
                sb.Append('_');
                sb.Append(form.Type.ToString().Trim());
                return sb.ToString();
            }
            finally
            {
                sb.Clear();
            }
        }

        private static string GetAssemblyName(string formClassName)
        {
            return string.Format("GeneratedForms.{0}.dll", formClassName);
        }
        #endregion
    }
}
