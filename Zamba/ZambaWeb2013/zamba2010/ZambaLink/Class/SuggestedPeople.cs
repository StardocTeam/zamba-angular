using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ZambaLink
{
    public class SuggestedPeople
    {
        private static Form sp= Frm_ZLink.spForm;
        public static void Initialize(Form link)
        {         
            sp.FormClosing += Sp_FormClosing;
            sp.Show();
            sp.Visible = false;
        }
        public static void Show(Form link)
        {  
            if (sp.Visible)
            {
                FadeOut(sp);
            }
            else
            {
                sp.Height = link.Size.Height - 2;
                sp.Width = link.Width;
                sp.Location = new Point(link.Left - link.Width, link.Top);
                sp.BringToFront();
                sp.WindowState = FormWindowState.Minimized;
                sp.Visible = true;      
                 
                FadeIn(sp);
                sp.WindowState = FormWindowState.Normal;
                sp.Location = new Point(link.Left - link.Width, link.Top);
            }
        }

        public static async void FadeIn(Form o, int interval = 80)
        {    
            o.Opacity = 0;
            o.Show();
            while (o.Opacity < 1.0)
            {
                await Task.Delay(interval);
                o.Opacity += 0.05;
            }
            o.Opacity = 1;     
        }
        public static async void FadeOut(Form o, int interval = 80)
        {            
            o.Opacity = 1;
            while (o.Opacity > 0.0)
            {
                await Task.Delay(interval);
                o.Opacity -= 0.05;
            }
            o.Opacity = 0; 
            o.Hide();
        }
        private static void Sp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
                sp.Hide();           
        }
    }
}
