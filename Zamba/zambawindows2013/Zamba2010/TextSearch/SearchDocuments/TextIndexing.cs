using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;


namespace Zamba.SearchDocuments
{
   public class TextIndexing
    {

        Zamba.IndexManager.Analyzer.Indexer indexer = new Zamba.IndexManager.Analyzer.Indexer();
        private string[] patterns = { "*.htm", "*.html", "*.doc", "*.txt", "*.xls" };

        public TextIndexing()
        {
            indexer.IndexPath = Application.StartupPath;
        }

#region Indexing


        
        public void IndexFile(FileInfo fInfo)
        {
            if (fInfo.FullName.StartsWith("~") == false)
            {
                IndexManager.DocumentManagement.Document doc = new IndexManager.DocumentManagement.Document();
                doc.Name = fInfo.FullName;
                doc.ApplicationDirectory = fInfo.DirectoryName;
                doc.FileName = Path.GetFileName(fInfo.FullName);
                doc.VirtualDirectory = "";
                indexer.AddDocument(doc, External.SearchDocuments.Parsing.Parser.Parse(fInfo.FullName));

                IndexDocuments(fInfo);
                indexer.WriteIndices();

            }
        }


        public void IndexDocuments(FileInfo fInfo)
        {
            try
            {
                if (fInfo.FullName.StartsWith("~") == false)
                {
                    IndexManager.DocumentManagement.Document doc = new IndexManager.DocumentManagement.Document();
                    doc.Name = fInfo.FullName;
                    doc.ApplicationDirectory = fInfo.DirectoryName;
                    doc.FileName = Path.GetFileName(fInfo.FullName);
                    doc.VirtualDirectory = "";
                    indexer.CreateDocumentIndices(doc, External.SearchDocuments.Parsing.Parser.Parse(fInfo.FullName));
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //statusBar1.Text = "Ready";
            }
        }


        #endregion

        #region Search
        public   ArrayList Search(string Words)
        {
            IndexManager.Analyzer.Indexer localindexer = new IndexManager.Analyzer.Indexer();
            localindexer.IndexPath = indexer.IndexPath;
            localindexer.stemlevel = IndexManager.Analyzer.StemmerLevel.PruralsandPastParticiples;
            localindexer.ReadIndices();

            IndexManager.Results results = localindexer.Search(Words);
            return results.arrlist;

            
            //results = results;

//            foreach (IndexManager.Result result in results.arrlist)
  //          {
    //            if (result.Score > 0.0)
      //          {
                    string[] strItems = new string[3];
                    //strItems[0] = result.Doc.FileName;
                    //strItems[1] = result.Doc.ApplicationDirectory;
                    //strItems[2] = result.Score.ToString();
                    //lvResults.Items.Add(new ListViewItem(strItems));
        //        }
       //     }
   }
        
#endregion


    }




}
