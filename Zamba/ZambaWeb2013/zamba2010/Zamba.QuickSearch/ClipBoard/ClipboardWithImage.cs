using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using Zamba.QuickSearch;

namespace ClipBoardRing
{
    public partial class ClipboardWithImage : Form
    {
        string strClipCurrent = String.Empty;
        int xPos = 0;
        int yPos = 0;
        int intHashClipCurrent;
        Bitmap bmpClipCurrent;
        object objBmpClipCurrent;
        static bool processImage = true;

        public ClipboardWithImage()
        {
            InitializeComponent();
        }

        private void ClipboardWithImage_Load(object sender, EventArgs e)
        {
            xPos = Screen.GetWorkingArea(this).Width;
            yPos = Screen.GetWorkingArea(this).Height;
            this.Location = new Point(xPos - this.Width, yPos + this.Height);
            timer1.Enabled = true;
            this.Opacity = 0;
            AddToBoard();
        }

        private void AddToBoard()
        {
            bool addText = true;
            bool addImg = true;

            // Before retrieving Text from the Clipboard make sure the 
            // current data on Clipboard is for type text.
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                string s = Clipboard.GetDataObject().GetData(DataFormats.Text).ToString();
                //if (s != strClipCurrent)
                //{
                // Iterating through the whole ListView control Items 
                // to see if the Current Clipboard item exists in the ListView
                // This is to avoid multiple entries.
                foreach (ListViewItem lstViewItm in listView1.Items)
                {
                    if (lstViewItm.Tag.ToString() == s)
                    {
                        addText = false;
                    }
                }
                // If entry is a new one.
                if (addText)
                {
                    ListViewItem lv = new ListViewItem(s);
                    lv.Tag = s;
                    listView1.Items.Add(lv);
                    strClipCurrent = s;
                }
                //}
            }
            if (processImage)
            {
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap))
                {
                    Bitmap bmp = (Bitmap)Clipboard.GetImage();
                    Bitmap bmpTemp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    //if (bmp == null)
                    //{
                    //    string aa = "";
                    //}
                    if (bmpClipCurrent == null)
                    {
                        bmpClipCurrent = bmp;
                    }
                    if ((bmp != null) && (bmpClipCurrent != null))
                    {
                        foreach (ListViewItem lstViewItm in listView1.Items)
                        {
                            if (lstViewItm.Tag.GetType().ToString() == "System.Drawing.Bitmap")
                            {
                                if (CompareBitmaps(bmp, (Bitmap)lstViewItm.Tag))
                                {
                                    addImg = false;
                                }
                            }
                        }
                    }
                    if (addImg)
                    {
                        if ((bmp != null))
                        {
                            imageList1.Images.Add(bmp);
                            ListViewItem lv = new ListViewItem("<<Picture>>", imageList1.Images.Count - 1);
                            lv.Tag = bmp;
                            listView1.Items.Add(lv);
                            bmpClipCurrent = bmp;
                        }
                    }
                }
            }
        }

        public bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1.Size != bmp2.Size)
            {
                return false;
            }
            else
            {
                //Convert each image to a byte array
                System.Drawing.ImageConverter ic =
                    new System.Drawing.ImageConverter();
                byte[] btImage1 = new byte[1];
                btImage1 = (byte[])ic.ConvertTo(bmp1, btImage1.GetType());
                byte[] btImage2 = new byte[1];
                btImage2 = (byte[])ic.ConvertTo(bmp2, btImage2.GetType());

                //Compute a hash for each image
                SHA256Managed shaM = new SHA256Managed();
                byte[] hash1 = shaM.ComputeHash(btImage1);
                byte[] hash2 = shaM.ComputeHash(btImage2);

                //Compare the hash values
                for (int i = 0; i < hash1.Length && i < hash2.Length; i++)
                {
                    if (hash1[i] != hash2[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void tmr2_Tick(object sender, EventArgs e)
        {
            int curPos = this.Location.Y;

            if (curPos < (yPos + 30))
            {
                this.Location = new Point(xPos - this.Width, curPos + 20);
            }
            else
            {
                tmr2.Stop();
                tmr2.Enabled = false;
                //this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            AddToBoard();
        }

        private void tmr1_Tick(object sender, EventArgs e)
        {
            int curPos = this.Location.Y;
            if (curPos > yPos - this.Height)
            {
                this.Location = new Point(xPos - this.Width, curPos - 20);
            }
            else
            {
                tmr1.Stop();
                tmr1.Enabled = false;
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            bool addtext = true;
            if (listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems[0].Tag.GetType().ToString() == "System.String")
                {
                    string strSelItm = listView1.SelectedItems[0].Tag.ToString();
                    //if (strSelItm != strClipCurrent)
                    //{
                    strClipCurrent = strSelItm;
                    Clipboard.SetDataObject(strSelItm);
                    //foreach (ListViewItem lv in listView1.Items)
                    //{
                    //    if (lv.Tag.ToString() == strSelItm)
                    //    {
                    //        addtext = false;
                    //    }
                    //}
                    //if (addtext)
                    //{
                    //    ListViewItem lv = new ListViewItem(strSelItm);
                    //    lv.Tag = strSelItm;
                    //    listView1.Items.Add(lv);
                    //}
                    //}
                }
                if (listView1.SelectedItems[0].Tag.GetType().ToString() == "System.Drawing.Bitmap")
                {
                    Bitmap bmpSelItm = (Bitmap)listView1.SelectedItems[0].Tag;
                    bmpClipCurrent = bmpSelItm;
                    Clipboard.SetDataObject((Bitmap)listView1.SelectedItems[0].Tag, true);
                }
                this.Hide();
            }
            //listView1.SelectedIndex = -1;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Opacity = 1;
            tmr1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //lstBoxCpyItms.SelectedIndex = -1;
            //this.Hide();
            tmr2.Enabled = true;
        }

        private void processImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void processImageToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (processImageToolStripMenuItem.Checked)
            {
                processImage = true;
            }
            else
            {
                processImage = false;
            }
        }

        private void textOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuickSearch frm1 = new frmQuickSearch();
            frm1.Show();
            this.Close();
        }
    }
}