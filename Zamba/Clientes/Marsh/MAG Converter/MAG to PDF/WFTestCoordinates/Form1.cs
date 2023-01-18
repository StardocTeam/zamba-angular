using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using static Tesseract.ZambaOCR;

namespace WFTestCoordinates
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp";
            openFileDialog.Title = "Selecciona una imagen";
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog.FileName;
                string msg = string.Empty;

                foreach (Rectangular rect in GetHarcodedCoordinates())
                {
                    ZambaOCR ZOCR = new ZambaOCR();
                    var imgText = ZOCR.GetText(file, rect, string.Empty, new FileInfo(file).Directory.FullName);
                    ZOCR = null;
                        msg = (rect.SectorName + Environment.NewLine + imgText);
                        MessageBox.Show(null, msg, "Contenido de poliza", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            
            }
        }

        private List<Rectangular> GetHarcodedCoordinates()
        {
            var lr = new List<Rectangular>();
            lr.Add(new Rectangular(new Point(129,677),new Point(1725,881),"Datos de asegurado"));
            lr.Add(new Rectangular(new Point(873, 351), new Point(1200, 382), "Seccion"));
            lr.Add(new Rectangular(new Point(1753,836), new Point(2051,890),"Nro Poliza"));
            lr.Add(new Rectangular(new Point(957,1021),new Point(1633,1081), "Vigencia"));
            lr.Add(new Rectangular( new Point(125,1172), new Point(1775,2381), "Descripcion"));
            return lr;
        }
    }
}
