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
        private Font DEFAULT_FONT = new Font("Times New Roman", 12, GraphicsUnit.Point);

        public HtmlEditor()
        {
            InitializeComponent();

            //Se cargan los tipos de letra
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                ToolStripButton fontButton = new ToolStripButton();
                fontButton.Text = font.Name;
                fontButton.Click += new EventHandler(FontTypeItem_Clicked);
                cboFontType.DropDownItems.Add(fontButton);
            }

            //Se cargan los tamaños de letra (en html va del 1 al 7)
            for (int i = 1; i <= 7; i++)
            {
                ToolStripButton sizeButton = new ToolStripButton();
                sizeButton.Text = i.ToString();
                sizeButton.Click += new EventHandler(FontSizeItem_Clicked);
                cboFontSize.DropDownItems.Add(sizeButton);
            }

            //Tamaño por defecto para el texto
            richTextBox1.Font = new Font(DEFAULT_FONT.Name, DEFAULT_FONT.Size, DEFAULT_FONT.Unit);
        }

        /// <summary>
        /// Modifica el tamaño del texto seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontSizeItem_Clicked(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, ConvertToPoints(sender), GraphicsUnit.Point);
            }
            UpdateToolbar();
        }

        /// <summary>
        /// Convierte la unidad html (1 al 7) en la unidad puntos.
        /// </summary>
        /// <param name="sizeButton">Boton con el valor de 1 a 7</param>
        /// <returns></returns>
        private float ConvertToPoints(object sizeButton)
        {
            int htmlSize = int.Parse(((ToolStripButton)sizeButton).Text);

            switch (htmlSize)
            {
                case 1:
                    return 8;
                case 2:
                    return 10;
                case 3:
                    return 12;
                case 4:
                    return 14;
                case 5:
                    return 18;
                case 6:
                    return 24;
                case 7:
                    return 36;
                default:
                    return DEFAULT_FONT.Size;
            }
        }

        /// <summary>
        /// Modifica el tipo de letra del texto seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontTypeItem_Clicked(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(((ToolStripButton)sender).Text, richTextBox1.SelectionFont.Size);
            }
            UpdateToolbar();
        }

        public bool ColorButtonVisibility { get { return btnColor.Visible; } set { btnColor.Visible = value;} }

        private void richTextBox1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (!richTextBox1.InternalUpdating)
                UpdateToolbar();
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

        //private void btnFont_Click(object sender, EventArgs e)
        //{
        //    if (richTextBox1.SelectionFont != null)
        //        fontDialog1.Font = richTextBox1.SelectionFont;
        //    else
        //        fontDialog1.Font = richTextBox1.Font;

        //    if (fontDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        if (richTextBox1.SelectionFont != null)
        //            richTextBox1.SelectionFont = fontDialog1.Font;
        //        else
        //            richTextBox1.Font = fontDialog1.Font;
        //    }
        //    UpdateToolbar();
        //}

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
    }
}
