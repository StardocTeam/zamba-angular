using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Zamba.MigrationZIRZVIR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataSet DSZIR = new DataSet();
        DataSet DSDISTINCTZIR = new DataSet();
        DataSet DSINDEXS = new DataSet();
        DataSet DSDOCTYPEDOCTYPE = new DataSet();



        private void Form1_Load(object sender, EventArgs e)
        {
            //IndexId DoctypeId UserId RightType

            Zamba.Servers.IConnection con = Servers.Server.get_Con(true, false, true);

            String DSZIRs = "Select * from zir";

            String DSDOCTYPEDOCTYPEs = "Select distinct DOCTYPEID1, DOCTYPEID2 from doc_type_r_doc_type";
            
            String DSINDEXSs = "Select * from index_r_doc_type";
            
            //String DSDISTINCTZIRs = "Select distinct userid,indexid,doctypeid from zir";
            String DSDISTINCTZIRs = "Select distinct userid,doctypeid from zir";


            DSZIR = con.ExecuteDataset(CommandType.Text, DSZIRs);
            DSDISTINCTZIR = con.ExecuteDataset(CommandType.Text, DSDISTINCTZIRs);
            DSINDEXS = con.ExecuteDataset(CommandType.Text, DSINDEXSs);
            DSDOCTYPEDOCTYPE = con.ExecuteDataset(CommandType.Text, DSDOCTYPEDOCTYPEs);

            textBox1.Text = " Migración de permisos a asociados Iniciada...\n";


           //por cada dt y usuario en ZIR (osea que tiene marcado el check de indices especificos)
            foreach (DataRow DRDZIR in DSDISTINCTZIR.Tables[0].Rows)
            { 
              


                DataView DV  = new DataView(DSINDEXS.Tables[0]);
                //busco los indices de ese DT
                DV.RowFilter = "DOC_TYPE_ID = " + DRDZIR["DOCTYPEID"].ToString();

                //por cada indice de DT
                foreach (DataRow DRIN in DV.ToTable().Rows)
                { 
                    //verifico que el indice, el dt y el usuario no este marcado en la ZIR
                    if (IsInZIR(Convert.ToInt64(DRIN["INDEX_ID"]),Convert.ToInt64(DRDZIR["USERID"]),Convert.ToInt64(DRDZIR["DOCTYPEID"])) == false)
                    {
                        //si no esta marcado, es que no se debe ver, por cuanto
                        //para cada asociado padre, se le especifica que
                        // ese dt, indice y usuario no visualizan ese indice

                        DataView DV2  = new DataView(DSDOCTYPEDOCTYPE.Tables[0]);
                        //busco los indices de ese DT
                        DV2.RowFilter = "DOCTYPEID2 = " + DRDZIR["DOCTYPEID"].ToString();

                        String Insert = "";
                        foreach (DataRow DRDTDT in DV2.ToTable().Rows)
                        {
                            try
                            {
                                //Insertar Permiso para habilitar permisos por asociados
                                String InsertRight = "insert into usr_rights(GROUPID,OBJID,RType,ADITIONAL) values(" + DRDZIR["USERID"].ToString() + ",2,133," + DRDTDT["DOCTYPEID1"].ToString() + ")";

                                Console.WriteLine(InsertRight);
                                con.ExecuteNonQuery(CommandType.Text, InsertRight);
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine(ex.Message);
                            }


                            try
                            {    
                                 Insert = "INSERT INTO ZVIR ([DoctypeParentId] ,[DoctypeId] ,[IndexId] ,[GID] ,[RightType]) " +
                            "VALUES (" + DRDTDT["DOCTYPEID1"] + "," + DRDZIR["DOCTYPEID"] + "," + DRIN["INDEX_ID"] + "," + DRDZIR["USERID"] + ",132)";

                            Console.WriteLine(Insert);
                            con.ExecuteNonQuery(CommandType.Text, Insert);
                            //Thread.Sleep(250);
                            }
                            catch (Exception ex)
                            {
                                this.textBox1.Text = this.textBox1.Text + ex.Message + "\n";
                                
                                Console.WriteLine(ex.Message);
                            }
                           
                        }
                    }

                }
            
            }
            textBox1.Text += " Migración de permisos Finalizada";

        }

        

        private  Boolean IsInZIR(Int64 INDEXID, Int64 USERID , Int64 DOCTYPEID)
        {
            String Strqry = "INDEXID = " + INDEXID.ToString() + " and UserId = " + USERID.ToString() + " and DOCTYPEID = " + DOCTYPEID.ToString() + " and Righttype = 80 ";
            if (DSZIR.Tables[0].Select(Strqry).Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
