//************************************************************************************
//    *  AlterDbTypeInIndex *
//  Autor: Javier Colombera
//  Fecha: 02/01/2011
//
//  La aplicación se encarga de convertir todos los índices de tipo númerico, que en 
//  SQLSERVER se encuentran en int, a numeric(9,0).
//  Salva las constraint default solamente, los indices y desbordamientos se tratan
//  Manualmente
//************************************************************************************


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.Data;
using Zamba.Servers;
using System.Diagnostics;

namespace AlterDbTypeInIndex
{
    public partial class Form1 : Form
    {
        DSDOCTYPE DsDoctype;
        private IConnection con;

        public Form1()
        {
            InitializeComponent();

            Trace.Listeners.Add(new TextWriterTraceListener(ZTrace.GetTempDir("\\Exceptions").FullName  + "\\Trace AlterdbTypeInIndex" + " - " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt", "AlterDbTypeInIndex"));
            Trace.AutoFlush = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LblMensaje.Text = "";
        }

         private void EjecutarUpdate(Int32 doctypeId, Index idx)
        {           
             //ALTER TABLE dbo.doc_exy ALTER COLUMN column_a DECIMAL (5, 2)
            StringBuilder qry = new StringBuilder();
            qry.Append("ALTER TABLE DOC_I");
            qry.Append(doctypeId);
            qry.Append(" ALTER COLUMN I");
            qry.Append(idx.ID);
            qry.Append(" NUMERIC(9,0)");
            this.txtQuery.Text = this.txtQuery.Text + qry.ToString() + "\r\n";
            System.Diagnostics.Trace.WriteLine(qry.ToString());
            System.Diagnostics.Trace.Flush();

            con = Server.get_Con(true, true, true);
            con.ExecuteNonQuery(CommandType.Text, qry.ToString());
            con = null;
        }

         private bool isreferenced(Int32 doctypeId, Index idx)
         {
             //ALTER TABLE dbo.doc_exy ALTER COLUMN column_a DECIMAL (5, 2)
             StringBuilder qry = new StringBuilder();
             qry.Append("select isreferenced from index_r_doc_type where index_id="); 
             qry.Append(idx.ID);
             qry.Append(" and doc_type_id=");
             qry.Append(doctypeId);
                          
             System.Diagnostics.Trace.WriteLine(qry.ToString());
             System.Diagnostics.Trace.Flush();

             con = Server.get_Con(true, true, true);
             Int32 resultado = Convert.ToInt32(con.ExecuteScalar(CommandType.Text, qry.ToString()));

             if (resultado == 1)
             {
                System.Diagnostics.Trace.WriteLine("TRUE");
                return true;
             }                 
             else
             {
                System.Diagnostics.Trace.WriteLine("FALSE");
                return false;
             }                 

             con = null;
         }

         private void EjecutarProceso()
         {
             try
             {
                 LblMensaje.Text = "Ejecutando Proceso de cambio INT a NUMERIC(9,0)";
                 bool errores = false;


                 //User
                 Zamba.Core.IUser zambaUser = UserBusiness.GetUserByname(Properties.Settings.Default.UserName);
                 UserBusiness.SetCurrentUser(zambaUser);

                 //Cargar Doctypes de la bd
                 DsDoctype = DocTypesBusiness.GetDocTypesDsDocType();
                 //Para cada Doctype obtener sus índices
                 foreach (DataRow row in DsDoctype.Tables[0].Rows)
                 {
                     Int32 docTypeId = Convert.ToInt32(row["DOC_TYPE_ID"]);
                     DataSet IndexDS = DocTypesBusiness.GetIndexsProperties(docTypeId, false);
                     foreach (DataRow rowIn in IndexDS.Tables[0].Rows)
                     {

                         Index _index = ZCore.GetIndex(Convert.ToInt64(rowIn["INDEX_ID"]));

                         //Index _index = IndexsBussines.g(Convert.ToInt32(rowIn["INDEX_ID"]));

                         if (_index.Type == IndexDataType.Numerico)
                         {
                             try
                             {
                                 cambiarCampo(docTypeId, _index);
                             }
                             catch (Exception ex)
                             {
                                 System.Diagnostics.Trace.WriteLine(ex.Message);
                                 System.Diagnostics.Trace.Flush();
                                 TxtErrores.Text = TxtErrores.Text + ex.Message + "\r\n";
                                 TxtErrores.Text = TxtErrores.Text + "                    " + "\r\n";
                                 TxtErrores.Text = TxtErrores.Text + "*******************" + "\r\n";
                                 errores = true;
                             }

                         }
                     }
                 }

                 if (errores)
                     MessageBox.Show("Actualización de Indices numericos realizada pero con errores, revise trace y exxcepcion para tratar manualmente");
                 else
                     MessageBox.Show("Actualización de Indices numericos realizado correctamente");
                 LblMensaje.Text = "Proceso Finalizado";

             }
             catch (Exception ex)
             {
                 System.Diagnostics.Trace.WriteLine(ex.Message);
                 System.Diagnostics.Trace.Flush();
                 TxtErrores.Text = TxtErrores.Text + ex.Message;
                 ZClass.raiseerror(ex);
             }
         }

         private void cambiarCampo(Int32 doctypeId, Index idx)
         {
             //ALTER TABLE dbo.doc_exy ALTER COLUMN column_a DECIMAL (5, 2) ;
             if (isreferenced(doctypeId, idx) == false)
             {
                 if (ConstraintSQL.HasConstraints("DOC_I" + doctypeId, "I" + idx.ID))
                 {
                     DataSet dsCons = ConstraintSQL.GetConstraints("DOC_I" + doctypeId, "I" + idx.ID);

                     ConstraintSQL cs = new ConstraintSQL();

                     cs._columnName = dsCons.Tables[0].Rows[0][4].ToString();
                     cs._constraintCatalog = dsCons.Tables[0].Rows[0][0].ToString();
                     cs._constraintName = dsCons.Tables[0].Rows[0][3].ToString();
                     cs._constraintSchema = dsCons.Tables[0].Rows[0][2].ToString();
                     cs._defaultClause = dsCons.Tables[0].Rows[0][6].ToString();
                     cs._ordinalPosition = dsCons.Tables[0].Rows[0][5].ToString();
                     cs._tableName = dsCons.Tables[0].Rows[0][1].ToString();


                     //Eliminar Constraint Default
                     ConstraintSQL.DropDefaultConstraint(cs);

                     EjecutarUpdate(doctypeId, idx);

                     //Crear Constraint Default
                     ConstraintSQL.CreateDefaultConstraint(cs);

                 }
                 else
                 {
                     EjecutarUpdate(doctypeId, idx);
                 }
             }
             else
             {
                 System.Diagnostics.Trace.WriteLine("Indice referencial: I" + idx.ID + " no se realiza cambio");
             }
         }
       
        private void txtQuery_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            EjecutarProceso();
        }     

    }
}
