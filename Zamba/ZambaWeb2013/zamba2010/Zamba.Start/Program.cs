using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zamba.Start
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] args = Environment.GetCommandLineArgs();
            SingleInstanceController controller = new SingleInstanceController();
            controller.Run(args);
        }

    }

    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        public SingleInstanceController()
        {
            IsSingleInstance = true;

            StartupNextInstance += this_StartupNextInstance;

            Startup += this_StartUp;
        }

        void this_StartUp(object sender, StartupEventArgs e)
        {
            Start form = new Start();
            if (e.CommandLine.Count > 1)
            {
                DialogResult frmResult;
                String noticiaLegal = ZOptBusiness.GetValue("LegalNoticeMessage");

                if (String.IsNullOrEmpty(noticiaLegal) == true)
                {
                    frmResult = MessageBox.Show("Este sistema es para ser utilizado solamente por usuarios autorizados. Toda la información contenida en los sistemas es propiedad de la Empresa y pueden ser supervisados, cifrados, leídos, copiados o capturados y dados a conocer de alguna manera, solamente por personas autorizadas." + (char)13 + "El uso del sistema por cualquier persona, constituye de su parte un expreso consentimiento al monitoreo, intervención, grabación, lectura, copia o captura y revelación de tal intervención." + (char)13 + "EL usuario debe saber que en la utilización del sistema no tendrá privacidad frente a los derechos de la empresa responsable del sistema." + (char)13 + "El uso indebido o no autorizado de este sistema genera  responsabilidad para el infractor, quién por ello estará sujeto al resultado de las acciones civiles y penales que la Empresa considere pertinente realizar en defensa de sus derechos y resguardo de la privacidad del sistema." + (char)13 + "Si Usted no presta conformidad con las reglas precedentes y no está de acuerdo con ellas, desconéctese ahora.", "Noticia Legal", MessageBoxButtons.OKCancel);
                }
                else
                {
                    frmResult = MessageBox.Show(noticiaLegal, "Noticia Legal", MessageBoxButtons.OKCancel);
                }

                if (frmResult == DialogResult.Cancel)
                {
                    Thread.CurrentThread.Abort();
                }

                form.validateUser(true);
                form.ExecuteCurrentClient(e.CommandLine[1]);
            }

        }

        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            Start form = MainForm as Start; //My derived form type
            if (e.CommandLine.Count > 1)
            {
                form.ExecuteCurrentClient(e.CommandLine[1]);
            }
            else
            {
                form.Show();
            }
        }

        protected override void OnCreateMainForm()
        {
            MainForm = new Start();
        }
    }

}
