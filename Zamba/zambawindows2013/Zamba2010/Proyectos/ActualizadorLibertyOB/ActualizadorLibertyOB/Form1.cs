using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text;


namespace ActualizadorLibertyOB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dialogoexaminar.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                RutaTxt.Text = dialogoexaminar.FileName;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void botonactualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RutaTxt.Text))
            {
                MessageBox.Show("Debe seleccionar un archivo", "Alerta", MessageBoxButtons.OK);
                return;
            }
            if (fecharecepcion.Value > DateTime.Now || fechadigitalizacion.Value > DateTime.Now)  
            { 
                MessageBox.Show("La fecha no debe superar al dia de hoy", "Alerta", MessageBoxButtons.OK); 
                return; 
            }
            string filecontent = GetFileContent(RutaTxt.Text);
            if (String.IsNullOrEmpty(filecontent))
                return;
            filecontent = FormatContent(filecontent);
            SaveFileContent(filecontent);
            MessageBox.Show("Se ha Actualizado el archivo con Exito");
        }

        private void SaveFileContent(string filecontent)
        {
            if (string.IsNullOrEmpty(filecontent))
            {
                MessageBox.Show("Error al Guardar", "Error", MessageBoxButtons.OK);
                return;
            }
            try
            {
                using (StreamWriter sw = new StreamWriter("Exportacion.ob"))
                {
                    sw.Write(filecontent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                               
            }
        }

        const string SEPARATOR = "\";\"";
        const string SEPARATOR2 = "\"";
        
        private string FormatContent(string filecontent)
        {
            filecontent = filecontent.Replace("\n", string.Empty);
            string[] lines = filecontent.Split(new char []{'\r','\n'});
            int count = lines.Length;
            string currentLine;
            StringBuilder sb = new StringBuilder();
            string[] currentValues;
            string recepcion = fecharecepcion.Value.ToShortDateString();
            string digitalizacion = fechadigitalizacion.Value.ToShortDateString();
            FileInfo fi;
            for (int i = 0; i < count; i++)
            {
                currentLine = lines[i];
                if (!string.IsNullOrEmpty(currentLine))
                {
                    currentValues = currentLine.Split(new char[] { '|' });
                    if (currentValues.Length > 0)
                    {
                        sb.Append(SEPARATOR2);
                        //tipo de documento
                        sb.Append(currentValues[0]); 
                        sb.Append(SEPARATOR);
                        //posicion
                        sb.Append(i + 1);
                        sb.Append(SEPARATOR);
                        //rama
                        sb.Append(currentValues[1]);
                        sb.Append(SEPARATOR);
                        //siniestro
                        sb.Append(currentValues[2]);
                        sb.Append(SEPARATOR);
                        sb.Append(recepcion);
                        sb.Append(SEPARATOR);
                        
                        //obtener nombre archivo
                        fi=new FileInfo(currentValues[currentValues.Length - 1]);
                        sb.Append(fi.Name);

                        sb.Append(SEPARATOR);
                        sb.Append(digitalizacion);
                        sb.AppendLine(SEPARATOR2);
                        
                    }

                }
            }
            return sb.ToString();
        }

        private string GetFileContent(string p)
        {
            if (string.IsNullOrEmpty(p))
                return string.Empty;
            try
            {
                if (File.Exists(p))
                {
                    using (StreamReader sr = new StreamReader(p))
                    {
                        return sr.ReadToEnd();
                    }

                }
                else 
                {
                    throw new Exception("Archivo no Encontrado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }
    }
}
