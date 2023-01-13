using System.Windows.Forms;
using Zamba.Core;
using Zamba.Shapes.Views;
using Zamba.AdminControls;
using System;
using Zamba.WFActivity.Regular;
using Zamba.Controls;

namespace Zamba.Diagrams
{
    public partial class FrmDiagramas : Form
    {
        private string line = String.Empty;
        //Id de usuario de Zamba, obtenido por parametro o por Logueo
        public int userId;
        public UcDiagrams ucDiagrams;
        public FrmDiagramas(string commandLine)
        {
            line = commandLine;

            InitializeComponent();

            string status = string.Empty;
            DBBusiness.InitializeSystem(ObjectTypes.ModuleQuery, null, false, ref status, new ErrorReportBusiness());

        }

        private bool LoadLogin()
        {
            try
            {
                if (!string.IsNullOrEmpty(line))
                {
                    line = line.ToUpper();

                    //TRATA DE OBTENER EL USERID MANDADO COMO PARAMETRO
                    if (line.Contains("USERID="))
                    {
                        userId = int.Parse((line.Split(char.Parse("="))[1]).Split(char.Parse(" "))[0]);

                        //Remuevo los caracteres que no sirven para los parámetros.
                        line = line.Replace("USERID=" + userId.ToString(), "");
                        line = line.Trim();

                        if ((UserBusiness.Rights.ValidateLogIn(userId, ClientType.Desktop) != null))
                        {
                            /*int timeout = System.Convert.ToInt32(UserPreferences.getValue("TimeOut", Sections.UserPreferences, "20"));
                            String winusername = UserGroupBusiness.GetUserorGroupNamebyId(userId);
                            UcmServices.Login(timeout, "ZambaDiagramas", userId, winusername, Environment.MachineName, 0,  ServiceTypes.Report);*/
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("No se pudo validar el usuario de Zamba, se cerrara la aplicacion", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }
                    else
                    {
                        if ((DoLogin() == false))
                            return false;
                    }
                }
                else
                {
                    if ((DoLogin() == false))
                        return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return false;
            }

        }

        /// <summary>
        /// Realiza el login de la aplicacion
        /// </summary>
        /// <remarks></remarks>
        private bool DoLogin()
        {
            System.Reflection.Assembly Assembly = new RulesInstance().GetWFActivityRegularAssembly();
            Login login = new Login(true, false, false, ObjectTypes.ModuleDiagrams, string.Empty, string.Empty, " - " + ServiceTypes.Report.ToString(), Assembly);
            try
            {
                this.Enabled = false;


                if (login.IsDisposed == false) login.ShowDialog();
                if (login.DialogResult == DialogResult.OK)
                {
                    //Valido que los valores del app.ini coincide con los guardados en la base
                    if ((UserBusiness.ValidateDataBase()))
                    {
                        userId = System.Convert.ToInt32(Membership.MembershipHelper.CurrentUser.ID);

                        // Cuando vuelve a aparecer el formulario de login tras haber expirado el time_out, el formulario principal del cliente se
                        // deshabilita. Por lo tanto, si el cliente vuelve a loguearse, y es válido, el formulario principal debe volver a habilitarse
                        if ((this.Enabled == false))
                        {
                            this.Enabled = true;
                        }
                        return true;
                    }
                    else
                    {
                        String contacto = ZOptBusiness.GetValue("ContactoValidacion");
                        String contactoMail = ZOptBusiness.GetValue("MailContactoValidacion");

                        if (String.IsNullOrEmpty(contacto))
                            MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con Stardoc Argentina S.A.", "Atencion");
                        else if (String.IsNullOrEmpty(contactoMail))
                            MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " + contacto, "Atencion");
                        else
                            MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " + contacto + " al correo electronico " + contactoMail, "Atencion");

                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (System.Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                MessageBox.Show("Ocurrio un Error en el Sistema, al mostrar el dialogo de Usuario " + ex.ToString(), "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validación de usuario finalizada con errores at: | " + System.DateTime.Now.ToString());
                Application.Exit();
                return false;
            }
        }


        private void commandBarButton1_Click(object sender, System.EventArgs e)
        {
			IWFNodeHelper NodeHelper = new WFNodeHelper();
			Zamba.Shapes.Views.UcExportControl exportPanel = new Zamba.Shapes.Views.UcExportControl(NodeHelper);
            exportPanel.ShowDialog();

        }

        private void Refresh_Click(object sender, System.EventArgs e)
        {
            ucDiagrams.Refresh();

        }

        private void FrmDiagramas_Load(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(line))
            {
                line = line.ToUpper();
                //TRATA DE OBTENER EL USERID MANDADO COMO PARAMETRO
                if (line.Contains("USERID="))
                {
                    userId = int.Parse((line.Split(Char.Parse("="))[1]).Split(Char.Parse(" "))[0]);
                    //Remuevo los caracteres que no sirven para los parámetros.
                    line = line.Replace("USERID=" + userId.ToString(), "");
                    line = line.Trim();
                    if (!UserBusiness.Rights.ValidateLogIn(userId, ClientType.Desktop).Equals(null))
                    {
                        int timeout = int.Parse(UserPreferences.getValue("TimeOut", Sections.UserPreferences, "20"));
                        string winusername = UserGroupBusiness.GetUserorGroupNamebyId(userId);
                        UcmServices.Login(timeout, "ZambaDiagramas", userId, winusername, Environment.MachineName, 0, ServiceTypes.Report);
                        ucDiagrams = new UcDiagrams { Dock = DockStyle.Fill };
                        this.Controls.Add(ucDiagrams);
                        ucDiagrams.BringToFront();
                        ucDiagrams.AddDiagram(Core.DiagramType.Home);
                        ucDiagrams.AddDiagram(Core.DiagramType.Actors);
                        ucDiagrams.AddDiagram(Core.DiagramType.Workflows);
                        ucDiagrams.AddDiagram(Core.DiagramType.Interfaces);
                        //            ucDiagrams.AddDiagram(Core.DiagramType.Tasks);
                        ucDiagrams.AddDiagram(Core.DiagramType.Reports);
                        ucDiagrams.AddDiagram(Core.DiagramType.DocType);

                        ucDiagrams.SelectFirstPage();

                    }
                    else
                    {
                        MessageBox.Show("No se pudo validar el usuario de Zamba, se cerrara la aplicacion", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Application.Exit();
                    }
                }
            }
            else
            {
                if (LoadLogin() == true)
                {
                    try
                    {

                        //Se agrega el control de tabs
                        //var ucDiagrams = new UcDiagrams { Dock = DockStyle.Fill };
                        ucDiagrams = new UcDiagrams { Dock = DockStyle.Fill };
                        this.Controls.Add(ucDiagrams);
                        ucDiagrams.BringToFront();
                        ucDiagrams.AddDiagram(Core.DiagramType.Home);
                        ucDiagrams.AddDiagram(Core.DiagramType.Actors);
                        ucDiagrams.AddDiagram(Core.DiagramType.Workflows);
                        ucDiagrams.AddDiagram(Core.DiagramType.Interfaces);
                        //            ucDiagrams.AddDiagram(Core.DiagramType.Tasks);
                        ucDiagrams.AddDiagram(Core.DiagramType.Reports);
                        ucDiagrams.AddDiagram(Core.DiagramType.DocType);

                        ucDiagrams.SelectFirstPage();
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }

                }
                else
                    Application.Exit();
            }
        }
    }
}
