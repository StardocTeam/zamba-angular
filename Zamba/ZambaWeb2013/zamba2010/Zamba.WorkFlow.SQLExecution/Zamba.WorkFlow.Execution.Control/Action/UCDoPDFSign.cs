using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using Zamba.PDFSigner;
//using iTextSharp.text.pdf;


namespace Zamba.WorkFlow.Execution.Control
{    
    public partial class UCDoPDFSign : Form
    {
        //Certificate S/N
        private string certSN;

        public UCDoPDFSign()
        {
            InitializeComponent();
        }


        private void debug(string txt)
        {
            DebugBox.AppendText(txt + System.Environment.NewLine);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile;
            openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Filter = "PDF files *.pdf|*.pdf";
            openFile.Title = "Select a file";
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            fullPath.Text = openFile.FileName;

            PdfReader reader = new PdfReader(fullPath.Text);
            foreach (KeyValuePair<string, string> key in reader.Info)
            {
                switch (key.Key)
                {
                    case "Author":
                        author.Text = key.Value;
                        break;
                    case "Creator":
                        creator.Text = key.Value;
                        break;
                    case "Title":
                        title.Text = key.Value;
                        break;
                    case "Subject":
                        subject.Text = key.Value;
                        break;
                    case "Keywords":
                        keywords.Text = key.Value;
                        break;
                    case "Producer":
                        producer.Text = key.Value;
                        break;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFile;
            saveFile = new System.Windows.Forms.SaveFileDialog();
            saveFile.Filter = "PDF files (*.pdf)|*.pdf";
            saveFile.Title = "Save PDF File";
            if (saveFile.ShowDialog() != DialogResult.OK)
                return;
            fileName.Text = saveFile.FileName;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.OpenFileDialog openFile;
            //openFile = new System.Windows.Forms.OpenFileDialog();
            //openFile.Filter = "Certificate files *.pfx|*.pfx";
            //openFile.Title = "Select a file";
            //if (openFile.ShowDialog() != DialogResult.OK)
            //    return;
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection sel = X509Certificate2UI.SelectFromCollection(store.Certificates, null, null, X509SelectionFlag.SingleSelection);

            if (sel.Count != 0)
            {
                X509Certificate2 cert = sel[0];
                certSN = sel[0].SerialNumber;
                certificate.Text = certSN;//(sel[0].Issuer).ToUpper().Replace("CN=", "");               
                lblCert.Text = (sel[0].Issuer).ToUpper().Replace("CN=", "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var txtVal = new List<TextBox>();
            txtVal.Add(fullPath);
            txtVal.Add(fileName);
           // txtVal.Add(certificate);
           
           if (!ValidateFiles(txtVal) || string.IsNullOrEmpty(certSN))
            {
                MessageBox.Show("Por favor complete los campos requeridos", "Zamba Software");
                return;
            }
            try
            {              
                var sPdf = new PDFSign();
                sPdf.Sign(fullPath.Text, fileName.Text, author.Text, title.Text, subject.Text, keywords.Text, creator.Text,
                producer.Text, certificate.Text, password.Text, reason.Text, contact.Text, location.Text, writePDF.Checked);
                MessageBox.Show("Firma aplicada correctamente", "Zamba Software");
            }       
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al procesar la firma de archivo PDF: " + ex.ToString());
            }
         
        }

        private bool ValidateFiles(List<Zamba.AppBlock.TextoInteligente> txt)
        {
            foreach (TextBox t in txt)
            {
                if (string.IsNullOrEmpty(t.Text)  )
                {//|| !System.IO.File.Exists(t.Text)
                    MessageBox.Show("Por favor complete el dato resaltado");
                    t.Focus();
                    return false;
                }
            }
            return true;
        }

        private void fileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void producer_TextChanged(object sender, EventArgs e)
        {

        }

        private void creator_TextChanged(object sender, EventArgs e)
        {

        }

        private void keywords_TextChanged(object sender, EventArgs e)
        {

        }

        private void subject_TextChanged(object sender, EventArgs e)
        {

        }

        private void title_TextChanged(object sender, EventArgs e)
        {

        }

        private void author_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void DebugBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void certificate_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void fullPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void writePDF_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void location_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void contact_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void reason_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}