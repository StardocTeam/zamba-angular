using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ZambaLink;

namespace ZambaLink
{
    public class GlobalSearch
    {
        public static void Show(Form link)
        {
            var gs = Frm_ZLink.gsForm;
            if (gs.Visible)
            {
                gs.Hide();
            }
            else
            {
                gs.FormClosing += Gs_FormClosing;
                gs.Height = link.Size.Height - 2;
                gs.Width = link.Width * 2;
                gs.BringToFront();
                gs.WindowState = FormWindowState.Minimized;
                gs.Show();
                gs.WindowState = FormWindowState.Normal;
                gs.Location = new Point(link.Left + link.Width, link.Top);
            }
        }
        private static void Gs_FormClosing(object sender, FormClosingEventArgs e)
        {
            Frm_ZLink.gsForm.Hide();
            e.Cancel = true;
        }
        //public static void ShowGs()
        //{
        //    Frm_ZLink.gsForm.Show();
        //}
    }
}
