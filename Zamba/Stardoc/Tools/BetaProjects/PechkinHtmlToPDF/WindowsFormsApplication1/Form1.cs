using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Pechkin;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerformanceCollector pc = new PerformanceCollector("PDF creation");

            pc.FinishAction("Library initialized");

            IPechkin sc = Factory.Create(new GlobalConfig().SetMargins(new Margins(0, 0, 0, 0))
                .SetDocumentTitle("Ololo").SetCopyCount(1).SetImageQuality(100)
                .SetLosslessCompression(true).SetMaxImageDpi(200).SetOutlineGeneration(true).SetOutputDpi(1200).SetPaperOrientation(true)
                .SetPaperSize(PaperKind.A4));
   

            pc.FinishAction("Converter created");

            sc.Begin += OnScBegin;
            sc.Error += OnScError;
            sc.Warning += OnScWarning;
            sc.PhaseChanged += OnScPhase;
            sc.ProgressChanged += OnScProgress;
            sc.Finished += OnScFinished;

            pc.FinishAction("Event handlers installed");


            byte[] buf = sc.Convert(  new Pechkin.ObjectConfig()
   .SetLoadImages(true)
   .SetPrintBackground(true)
   .SetScreenMediaType(true)
   .SetCreateExternalLinks(true), new StreamReader("forms/aysadiseno/input.html").ReadToEnd());

            /*sc.Convert(new ObjectConfig().SetPrintBackground(true).SetProxyString("http://localhost:8888")
            .SetAllowLocalContent(true).SetCreateExternalLinks(false).SetCreateForms(false).SetCreateInternalLinks(false)
            .SetErrorHandlingType(ObjectConfig.ContentErrorHandlingType.Ignore).SetFallbackEncoding(Encoding.ASCII)
            .SetIntelligentShrinking(false).SetJavascriptDebugMode(true).SetLoadImages(true).SetMinFontSize(16)
            .SetRenderDelay(2000).SetRunJavascript(true).SetIncludeInOutline(true).SetZoomFactor(2.2), htmlText.Text);*/

            pc.FinishAction("conversion finished");

            if (buf == null)
            {
                MessageBox.Show("Error converting!");

                return;
            }

            //for (int i = 0; i < 1000; i++)
            {
                buf = sc.Convert(  new Pechkin.ObjectConfig()
   .SetLoadImages(true)
   .SetPrintBackground(true)
   .SetScreenMediaType(true)
   .SetCreateExternalLinks(true), new StreamReader("forms/aysadiseno/input.html").ReadToEnd());

                if (buf == null)
                {
                    MessageBox.Show("Error converting!");

                    return;
                }
            }

            pc.FinishAction("1 more conversions finished");

            try
            {
                string fn = Path.GetTempFileName() + ".pdf";

                FileStream fs = new FileStream(fn, FileMode.Create);
                fs.Write(buf, 0, buf.Length);
                fs.Close();

                pc.FinishAction("dumped file to disk");

                Process myProcess = new Process();
                myProcess.StartInfo.FileName = fn;
                myProcess.Start();

                pc.FinishAction("opened it");
            }
            catch { }

            pc.ShowInMessageBox(null);
        }

        public void SetText(string text)
        {
            Text = text;
        }

        private void OnScProgress(IPechkin converter, int progress, string progressdescription)
        {
            if (InvokeRequired)
            {
                // simple Invoke WILL NEVER SUCCEDE, because we're in the button click handler. Invoke will simply deadlock everything
                BeginInvoke((Action<string>)SetText, "Progress " + progress + ": " + progressdescription);
            }
            else
            {
                Text = ("Progress " + progress + ": " + progressdescription);
            }
        }

        private void OnScPhase(IPechkin converter, int phasenumber, string phasedescription)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => { Text = ("New Phase " + phasenumber + ": " + phasedescription); }));
            }
            else
            {
                Text = ("New Phase " + phasenumber + ": " + phasedescription);
            }
        }

        private void OnScFinished(IPechkin converter, bool success)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => { Text = ("Finished, Success: " + success); }));
            }
            else
            {
                Text = ("Finished, Success: " + success);
            }
        }

        private void OnScWarning(IPechkin converter, string warningtext)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => { MessageBox.Show("Warning: " + warningtext); }));
            }
            else
            {
               // MessageBox.Show("Warning: " + warningtext);
            }
        }

        private void OnScBegin(IPechkin converter, int expectedphasecount)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => { Text = ("Begin, PhaseCount: " + expectedphasecount); }));
            }
            else
            {
                Text = ("Begin, PhaseCount: " + expectedphasecount);
            }
        }

        private void OnScError(IPechkin converter, string errorText)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => { MessageBox.Show("Error: " + errorText); }));
            }
            else
            {
                MessageBox.Show("Error: " + errorText);
            }
        }
    }
}
