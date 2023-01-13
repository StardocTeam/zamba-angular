using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Zamba.HtmlRichTextBox
{
    public partial class HtmlEditor : UserControl
    {
        object HtmlBuffer;

        public HtmlEditor()
        {
            InitializeComponent();
        }

        private void richTextBox1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (!richTextBox1.InternalUpdating)
                UpdateToolbar(); //Update the toolbar buttons
        }

        /// <summary>
        ///     Update the toolbar button statuses
        /// </summary>
        public void UpdateToolbar()
        {
            //This is done incase 2 different fonts are selected at the same time
            //If that is the case there is no selection font so I use the default
            //font instead.
            Font fnt;

            if (richTextBox1.SelectionFont != null)
                fnt = richTextBox1.SelectionFont;
            else
                fnt = richTextBox1.Font;

            //Do all the toolbar button checks
            //btnBold.Pushed = fnt.Bold; //bold button
            //btnItalic.Pushed = fnt.Italic; //italic button
            //this.btUnderline.Pushed = fnt.Underline; //underline button
            //this.btnStrikeOut.Pushed = fnt.Strikeout; //strikeout button
            //tbbLeft.Pushed = (richTextBox1.SelectionAlignment == HorizontalAlignment.Left); //justify left
            //tbbCenter.Pushed = (richTextBox1.SelectionAlignment == HorizontalAlignment.Center); //justify center
            //tbbRight.Pushed = (richTextBox1.SelectionAlignment == HorizontalAlignment.Right); //justify right

            //tbbSuperScript.Pushed = richTextBox1.IsSuperScript();
            //tbbSubScript.Pushed = richTextBox1.IsSubScript();
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style ^ FontStyle.Bold);
            }
            UpdateToolbar();
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style ^ FontStyle.Italic);
            }
            UpdateToolbar();
        }

        private void btUnderline_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style ^ FontStyle.Underline);
            }
            UpdateToolbar();
        }

        private void btnStrikeOut_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style ^ FontStyle.Strikeout);
            }
            UpdateToolbar();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
                fontDialog1.Font = richTextBox1.SelectionFont;
            else
                fontDialog1.Font = richTextBox1.Font;

            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectionFont != null)
                    richTextBox1.SelectionFont = fontDialog1.Font;
                else
                    richTextBox1.Font = fontDialog1.Font;
            }
            UpdateToolbar();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = richTextBox1.SelectionColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
            UpdateToolbar();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();            
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        //private void tb1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        //{
        //    //Switch based on the tooltip of the button pressed
        //    //OR: This could be changed to switch on the actual button pressed (e.g. e.Button and the case would be tbbBold)
        //    switch (e.Button.ToolTipText)
        //    {
        //        case "Bold":
        //            {
        //                if (richTextBox1.SelectionFont != null)
        //                {
        //                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style ^ FontStyle.Bold);
        //                }

        //            }
        //            break;

        //        case "Italic":
        //            {
        //                if (richTextBox1.SelectionFont != null)
        //                {
        //                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style ^ FontStyle.Italic);
        //                }
        //            }
        //            break;

        //        case "Underline":
        //            {
        //                if (richTextBox1.SelectionFont != null)
        //                {
        //                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style ^ FontStyle.Underline);
        //                }
        //            }
        //            break;

        //        case "Strikeout":
        //            {
        //                if (richTextBox1.SelectionFont != null)
        //                {
        //                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style ^ FontStyle.Strikeout);
        //                }
        //            }
        //            break;

        //        case "Super":
        //            {
        //                if (tbbSuperScript.Pushed)
        //                {
        //                    richTextBox1.SetSuperScript(true);
        //                    richTextBox1.SetSubScript(false);
        //                }
        //                else
        //                {
        //                    richTextBox1.SetSuperScript(false);
        //                }
        //            }
        //            break;

        //        case "Sub":
        //            {
        //                if (tbbSubScript.Pushed)
        //                {
        //                    richTextBox1.SetSubScript(true);
        //                    richTextBox1.SetSuperScript(false);
        //                }
        //                else
        //                {
        //                    richTextBox1.SetSubScript(false);
        //                }
        //            }
        //            break;
        //        case "Left":
        //            {
        //                //change horizontal alignment to left
        //                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        //            }
        //            break;

        //        case "Right":
        //            {
        //                //change horizontal alignment to right
        //                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        //            }
        //            break;

        //        case "Center":
        //            {
        //                //change horizontal alignment to center
        //                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        //            }
        //            break;

        //        case "Open":
        //            {
        //                //try
        //                //{
        //                //    if ((ofd1.ShowDialog() == DialogResult.OK) && (ofd1.FileName.Length > 0))
        //                //    {
        //                //        string strExt = System.IO.Path.GetExtension(ofd1.FileName).ToLower();

        //                //        if (strExt == ".rtf")
        //                //        {
        //                //            richTextBox1.LoadFile(ofd1.FileName, RichTextBoxStreamType.RichText);
        //                //        }
        //                //        else if (strExt == ".txt")
        //                //        {
        //                //            richTextBox1.LoadFile(ofd1.FileName, RichTextBoxStreamType.PlainText);
        //                //        }
        //                //        else if ((strExt == ".htm") || (strExt == ".html"))
        //                //        {
        //                //            // Read the HTML format
        //                //            StreamReader sr = File.OpenText(ofd1.FileName);
        //                //            string strHTML = sr.ReadToEnd();
        //                //            sr.Close();

        //                //            richTextBox1.AddHTML(strHTML);
        //                //        }
        //                //    }
        //                //}
        //                //catch
        //                //{
        //                //    MessageBox.Show("There was an error loading the file: " + ofd1.FileName);
        //                //}
        //            }
        //            break;

        //        case "Save":
        //            {
        //                //if ((sfd1.ShowDialog() == DialogResult.OK) && (sfd1.FileName.Length > 0))
        //                //{
        //                //    string strExt = System.IO.Path.GetExtension(sfd1.FileName).ToLower();

        //                //    if (strExt == ".rtf")
        //                //    {
        //                //        richTextBox1.SaveFile(sfd1.FileName);
        //                //    }
        //                //    else if (strExt == ".txt")
        //                //    {
        //                //        richTextBox1.SaveFile(sfd1.FileName, RichTextBoxStreamType.PlainText);
        //                //    }
        //                //    else if ((strExt == ".htm") || (strExt == ".html"))
        //                //    {
        //                //        try
        //                //        {
        //                //            // save as HTML format
        //                //            string strText = richTextBox1.GetHTML(true, true);

        //                //            if (File.Exists(sfd1.FileName))
        //                //                File.Delete(sfd1.FileName);

        //                //            StreamWriter sr = File.CreateText(sfd1.FileName);
        //                //            sr.Write(strText);
        //                //            sr.Close();
        //                //        }
        //                //        catch
        //                //        {
        //                //            MessageBox.Show("There was an error saving the file: " + sfd1.FileName);
        //                //        }
        //                //    }
        //                //}
        //            }
        //            break;

        //        case "Font":
        //            {
        //                if (richTextBox1.SelectionFont != null)
        //                    fontDialog1.Font = richTextBox1.SelectionFont;
        //                else
        //                    fontDialog1.Font = richTextBox1.Font;

        //                if (fontDialog1.ShowDialog() == DialogResult.OK)
        //                {
        //                    if (richTextBox1.SelectionFont != null)
        //                        richTextBox1.SelectionFont = fontDialog1.Font;
        //                    else
        //                        richTextBox1.Font = fontDialog1.Font;
        //                }
        //            }
        //            break;

        //        case "Color":
        //            {
        //                colorDialog1.Color = richTextBox1.SelectionColor;

        //                if (colorDialog1.ShowDialog() == DialogResult.OK)
        //                {
        //                    richTextBox1.SelectionColor = colorDialog1.Color;
        //                }
        //            }
        //            break;
        //    }

        //    UpdateToolbar(); //Update the toolbar buttons
        //}

      
    }
}
