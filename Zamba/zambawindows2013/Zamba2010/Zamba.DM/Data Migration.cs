using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Zamba.Data;
using Zamba.Servers;
using Zamba.Core;
using Zamba;

namespace Zamba.DM
{
    public partial class Form1 : Form
    {
//        public string ConStr = "Data Source=zambades;User ID=zamba;Password=Stardoc2015;Unicode=True";
        public Form1()
        {
            InitializeComponent();
            string status = string.Empty;
            DBBusiness.InitializeSystem(ObjectTypes.DBtoXML, null, false, ref status, new ErrorReportBusiness());
        }

        class table
        {
            public table(string name, decimal time, List<String> cols, string state, int totalrows, int toReplaceRows, string error)
            {
                Name = name;
                Cols = cols;
                TotalRows = totalrows;
                ToReplaceRows = toReplaceRows;
                Segundos = time;
                State = state;
                Error = error;
                
                
            }

            public string Name { get; set; }
            public List<String> Cols { get; set; }
            public int TotalRows { get; set; }
            public int ToReplaceRows { get; set; }
            public int ReplacedRows { get; set; }
            public string State { get; set; }
            public string Error { get; set; }
            public decimal Segundos { get; set; }
                  
            
        }

        private List<table> Tablesskyp = new List<table>();
        private List<table> Tablesok = new List<table>();
        private List<table> Tables = new List<table>();
        private List<table> Tableserror = new List<table>();

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
txtFind.Text =        ZOptBusiness.GetValue("MigracionServidorOrigen");

txtToRep.Text = ZOptBusiness.GetValue("MigracionServidorDestino");
            }
            catch (Exception)
            {
                
                throw;
            }
        }
     

        private void LoadTables()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = Tables;
            this.dataGridView1.Update();
            this.dataGridView1.Refresh();

            this.dataGridView2.DataSource = null;
            this.dataGridView2.DataSource = Tableserror;
            this.dataGridView2.Update();
            this.dataGridView2.Refresh();

            this.dataGridView3.DataSource = null;
            this.dataGridView3.DataSource = Tablesskyp;
            this.dataGridView3.Update();
            this.dataGridView3.Refresh();


            this.dataGridView4.DataSource = null;
            this.dataGridView4.DataSource = Tablesok;
            this.dataGridView4.Update();
            this.dataGridView4.Refresh();


            Application.DoEvents();
        }

        private void FillTables()
        {
            try
            {
                lblstatus.Text = "Cargando Tablas";
                progressBar1.Value = 0; 
                Application.DoEvents();

                AlltotalRows = 0;
                AllReplacedRows = 0;

                var tabla = "MIGRACIONDESC";//"prueba";
                //  dataGridView1.DataSource = GetDataTable("select tabla,COLUMNA from " + tabla + " order by tabla asc");
                Tables = new List<table>();
                DataTable dt2 = GetDataTable("select tabla,count(1) from " + tabla + " group by tabla");

                progressBar1.Maximum = dt2.Rows.Count;
                foreach (DataRow dr in dt2.Rows)
                {
                    try
                    {
                        lblstatus.Text = "Recorriendo Tablas";
                        Application.DoEvents();

                        DataTable dt3 = GetDataTable("select columna from " + tabla + " where tabla='" + dr[0] + "'");
                        List<string> listCol = new List<string>();

                        string query = "select count(1) from " + dr[0];
                        var totalRows = ReturnScalar(query);

                        int colcount = 0;
                        for (var i = 0; i <= dt3.Rows.Count - 1; i++)
                        {
                            colcount++;

                            string colname = dt3.Rows[i].ItemArray[0].ToString();
                            listCol.Add(colname);
                            if (colcount == 1)
                            {
                                query += string.Format(" where lower({0}) like '%{1}%' ", colname, txtFind.Text.ToLower());
                            }
                            else
                            {
                                query += string.Format(" or lower({0}) like '%{1}%' ", colname, txtFind.Text.ToLower());
                            }
                        }

                        dt3.Dispose();
                        var toReplacedRows = 0;
                        lblstatus.Text = "Calculando Tabla " + dr[0];
                        Application.DoEvents();
                        toReplacedRows = ReturnScalar(query);
                        AlltotalRows += toReplacedRows;

                        this.lbltotal.Text = AlltotalRows.ToString();

                     

                        table t = new table(dr[0].ToString(), 0, listCol, "Pendiente", totalRows, toReplacedRows, "");

                        if (t.ToReplaceRows == 0)
                        {
                            t.State = "Skyp";
                            Tablesskyp.Add(t);
                        }
                        else
                        {
                            Tables.Add(t);
                        }
                        LoadTables();
                    }
                    catch (Exception ex)
                    {
                        if (checkBox1.Checked)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    progressBar1.Increment(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }
          
            public DataTable GetDataTable (string query)
        {
            using (DataTable dt = new DataTable())
            {
                    dt.Load(Zamba.Servers.Server.get_Con().ExecuteReader(CommandType.Text, query));
                    return dt;
            }
        }
            //cargo los datos
            public int ReturnScalar(string query)
           {
             
                    return int.Parse(Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, query).ToString());
               
            }
    
    
        public  void ExecuteNonQuery()
            {
                DateTime starttime = DateTime.Now;
                decimal estimate = 0;
                string errorTable = string.Empty;
                this.progressBar1.Maximum = AlltotalRows;
                progressBar1.Value = 0;

                Application.DoEvents();

            string query = string.Empty;

                foreach (table t in Tables)
                {
                    DateTime starttabletime = DateTime.Now;
                    try 
	                {


                        if (t.ToReplaceRows > 0)
                    {
                         query = String.Format("update {0} ", t.Name);
                        string whereclause = " where ";
                        int colcount = 0;
                        foreach (var col in t.Cols)
                        {
                            colcount++;
                            if (colcount == 1)
                            {
                                query += String.Format(" set {0} =replace({0}, '{1}', '{2}')",
                                                          col, txtFind.Text, txtToRep.Text.ToLower());
                                whereclause += string.Format(" lower({0}) like '%{1}%'", col, txtFind.Text.ToLower());
                            }
                            else
                            {
                                query += String.Format(" ,{0} =replace({0}, '{1}', '{2}')",
                                                            col, txtFind.Text, txtToRep.Text.ToLower());
                                whereclause += string.Format(" or lower({0}) like '%{1}%'", col, txtFind.Text.ToLower());
                            }
                        }
                        query += whereclause;

                            lblstatus.Text = "Ejecutando Tabla " + t.Name;
                            Application.DoEvents();

                         t.ReplacedRows = Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, query);
                        
                            AllReplacedRows += t.ReplacedRows;
                          
                            lblcurrent.Text = AllReplacedRows.ToString();
                       
                            progressBar1.Increment(t.ToReplaceRows);

                            if (t.ReplacedRows == t.ToReplaceRows)
                            {
                                t.State = "Finalizado";
                                Tablesok.Add(t);
                            }
                            else
                            {
                                t.State = "Error";
                                errorTable += t.Name + ", ";
                                t.Error = "Filas remplazadas " + t.ReplacedRows + " query: " + query;
                                Tableserror.Add(t);
                                AllErrors += t.ReplacedRows;
                                lblerrors.Text = t.Error;
                                if (checkBox1.Checked)
                                {
                                    MessageBox.Show(t.Error, "Error");
                                } 
                            }

                    }
                    else
                    {
                        t.State = "Skyp";
                    }

                        }
	catch (Exception ex)
	{
		
		  t.State = "Error";
                            errorTable += t.Name + ", ";
                            t.Error = "Filas remplazadas " + t.ReplacedRows + " query: " + query + " error: " + ex.ToString();
                            AllErrors += t.ToReplaceRows;
                            lblerrors.Text = AllErrors.ToString();
                            if (checkBox1.Checked)
                            {
                                MessageBox.Show(errorTable,"Error");
                            } 
	}
                   
                    TimeSpan span =   DateTime.Now - starttime;
                    decimal duration = decimal.Parse(Math.Round(span.TotalSeconds,2).ToString());
                    estimate = Math.Round(AlltotalRows * duration / AllReplacedRows / 60,2) ;
                    lblestimate.Text = estimate.ToString();

                    TimeSpan spantable = DateTime.Now - starttabletime;
                    decimal durationtable = decimal.Parse(Math.Round(spantable.TotalSeconds, 2).ToString());
                    t.Segundos = durationtable;
                    LoadTables();

                }
                lblestimate.Text = "0";
            }

        private void button1_Click(object sender, EventArgs e)
        {
            FillTables();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            ExecuteNonQuery();
            
            }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ZOptBusiness.InsertUpdateValue("MigracionServidorOrigen", txtFind.Text);
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        private void txtToRep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ZOptBusiness.InsertUpdateValue("MigracionServidorDestino", txtToRep.Text);
            }
            catch (Exception)
            {
                
                throw;
            }


        }


        public int AlltotalRows { get; set; }

        public int AllReplacedRows { get; set; }

        public int AllErrors { get; set; }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

