using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba;
using Zamba.Core;
//using Zamba.Tools;

namespace Zamba.VolumeCheck
{
    public partial class VolumeCheck : Form
    {
        public VolumeCheck()
        {
            InitializeComponent();
        }

        Int64 LastEntityId = 0;
        Int64 LastDocId = 0;
        string _root;

        private void button1_Click(object sender, EventArgs e)
        {
            _root = textBox3.Text;
            WalkDirectory(new DirectoryInfo(_root), 0 , new List<string>());
            //Comparacion();
        }


        private void WalkDirectory(DirectoryInfo directory, Int32 cont, List<String> folders)
        {
            string fName, fSize, docId, ext;
            try
            {
                if(cont < 3)
                {
                    DirectoryInfo [] subDirectories = directory.GetDirectories();
                    
                    foreach (DirectoryInfo subDirectory in subDirectories)
                    {
                        folders.Add(subDirectory.Name);
                        WalkDirectory(subDirectory, cont+1,folders);
                    }
                }else{
                    foreach (FileInfo file in directory.GetFiles())
                    {
                         if (file.Exists)
                         {
                             fName = file.Name;
                             string[] split = fName.Split('.');
                             docId = split[0];
                             ext = split[1];
                             fSize = file.Length.ToString();
                             InsertIntoTable(docId, _root, folders[0], folders[1], folders[2], fName, ext, fSize);
                         }

                    }
                }

                folders.RemoveAt(cont - 1);

                return;
            }
            catch (Exception )
            {
                return;
            }
        }

        private void InsertIntoTable(string docId, string root, string volName ,string idEntity, string offset, string docFile, string ext, string fileSize)
        {
            string insertquery = 
                string.Format(
                    "INSERT INTO Z_VOLUME_CHECK_FS (DOC_ID, ROOT, VOL_NAME,ID_ENTITY, OFFSET, DOC_FILE, EXTENSION, FILE_SIZE) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", 
                    docId, root, volName, idEntity, offset, docFile, ext, fileSize
                    );
            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, insertquery);
        }
  
        private void Comparacion()
        {
            string Entitywhereclause = this.textBox1.Text;
            if (Entitywhereclause != string.Empty) Entitywhereclause = " and " + Entitywhereclause;

            DataSet ds = Zamba.Servers.Server.get_Con()
                .ExecuteDataset(CommandType.Text,
                    string.Format(
                        "select doc_type_id, doc_type_name from doc_type where doc_type_id > {0} {1} order by doc_type_id ",
                        LastEntityId, Entitywhereclause));


            string Rowswhereclause = this.textBox2.Text;

            if (Rowswhereclause != string.Empty) Rowswhereclause = " where " + Rowswhereclause;
            foreach (DataRow R in ds.Tables[0].Rows)
            {
                Int64 EntityId = Int64.Parse(R["DOC_TYPE_ID"].ToString());
                LastEntityId = EntityId;


                string query =
                    string.Format(
                        "select doc_id,name, crdate, ludate, platterid, disk_vol_path + '\' + convert(varchar,{0}) + '\' + convert(varchar,offset) + '\' + doc_file from doc_t{0} t inner join disk_volume v on t.vol_id = v.disk_vol_id {1}",
                        EntityId, Entitywhereclause);
                //Barcode no digitalizado
                // excluir volumenes < 1
                // doc_file <> ''
                //doc_file <> null


                IDataReader dr = Zamba.Servers.Server.get_Con().ExecuteReader(CommandType.Text, query);

                while (dr.Read())
                {
                    LastDocId = Int64.Parse(dr.GetString(0).ToString());
                    string Name = dr.GetString(1).ToString();
                    string FilePath = dr.GetString(5).ToString();

                    FileInfo fi = new FileInfo(FilePath);
                    Boolean Exists = fi.Exists;

                    string insertquery = "";


                    if (!Exists)
                    {
                        insertquery =
                            string.Format(
                                "insert into volumecheck (EntityId, Id, Name, Exists, Path) values ({0},{1},'{2}',{3},{4})",
                                EntityId, LastDocId, Name, Exists, FilePath);
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, insertquery);
                        
                        //grilla
                    }

                    //ver que pasa si da error y como continua.
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string status = string.Empty;
            DBBusiness.InitializeSystem(ObjectTypes.VolumeCheck,null, false, ref status, new ErrorReportBusiness());
        }



    }
}
