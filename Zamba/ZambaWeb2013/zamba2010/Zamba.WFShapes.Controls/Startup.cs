using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace Zamba.WFShapes.Controls
{
    /// <summary>
    /// The startup class holding the <see cref="STAThread"/>
    /// </summary>
    public class Startup
    {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //Using the enablevisualstyles method XP visual styles are applied to your winforms when the application is running on Windows XP.
            Application.EnableVisualStyles();
            //Setting the property to false will make the control use GDI for drawing text
            Application.SetCompatibleTextRenderingDefault(false);
            //Launch the messaging loop
          //  Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
    
        }
    }
}
