using Zamba.Thumbnails;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;


namespace Zamba
{
    namespace Thumbnails
    {
        partial class NewThumbnails
        {
            /// <summary> 
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary> 
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Component Designer generated code

            /// <summary> 
            /// Required method for Designer support - do not modify 
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                this.components = new System.ComponentModel.Container();
                this.panelBarraTop = new System.Windows.Forms.Panel();
                this.panelZoom = new System.Windows.Forms.Panel();
                this.zoom = new System.Windows.Forms.NumericUpDown();
                this.lZoom = new System.Windows.Forms.Label();
                this.panelBarraNavegacion = new System.Windows.Forms.Panel();
                this.barraNavPag = new Zamba.Thumbnails.BarraNavegacionPagina();
                this.panelBarraEstado = new System.Windows.Forms.Panel();
                this.lItem = new System.Windows.Forms.Label();
                this.lPage = new System.Windows.Forms.Label();
                this.panelBarraTop.SuspendLayout();
                this.panelZoom.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.zoom)).BeginInit();
                this.panelBarraNavegacion.SuspendLayout();
                this.panelBarraEstado.SuspendLayout();
                this.SuspendLayout();
                this.vista = new ListaImagen(this.defaultImage,
                     Constante.ListaImagen.IndexIcon);
                // 
                // panelBarraTop
                // 
                this.panelBarraTop.AutoSize = true;
                this.panelBarraTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                this.panelBarraTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.panelBarraTop.Controls.Add(this.panelZoom);
                this.panelBarraTop.Controls.Add(this.panelBarraNavegacion);
                this.panelBarraTop.Dock = System.Windows.Forms.DockStyle.Top;
                this.panelBarraTop.Location = new System.Drawing.Point(0, 0);
                this.panelBarraTop.Name = "panelBarraTop";
                this.panelBarraTop.Size = new System.Drawing.Size(440, 28);
                this.panelBarraTop.TabIndex = 9;
                // 
                // panelZoom
                // 
                this.panelZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.panelZoom.Controls.Add(this.zoom);
                this.panelZoom.Controls.Add(this.lZoom);
                this.panelZoom.Location = new System.Drawing.Point(363, 3);
                this.panelZoom.Name = "panelZoom";
                this.panelZoom.Size = new System.Drawing.Size(110, 20);
                this.panelZoom.TabIndex = 13;
                // 
                // zoom
                // 
                this.zoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.zoom.BackColor = System.Drawing.SystemColors.Window;
                this.zoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.zoom.Cursor = System.Windows.Forms.Cursors.Default;
                this.zoom.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
                this.zoom.Location = new System.Drawing.Point(30, 0);
                this.zoom.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
                this.zoom.Name = "zoom";
                this.zoom.Size = new System.Drawing.Size(45, 20);
                this.zoom.TabIndex = 12;
                this.zoom.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
                this.zoom.ValueChanged += new System.EventHandler(this.zoom_ValueChanged);
                // 
                // lZoom
                // 
                this.lZoom.Left = 0;
                this.lZoom.Top = 0;
                this.lZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.lZoom.AutoSize = true;
                this.lZoom.ForeColor = System.Drawing.Color.Blue;
                this.lZoom.Location = new System.Drawing.Point(-2, 4);
                this.lZoom.Name = "lZoom";
                this.lZoom.Size = new System.Drawing.Size(39, 15);
                this.lZoom.TabIndex = 0;
                this.lZoom.Text = "Zoom";
                // 
                // panelBarraNavegacion
                // 
                this.panelBarraNavegacion.Controls.Add(this.barraNavPag);
                this.panelBarraNavegacion.Location = new System.Drawing.Point(0, 2);
                this.panelBarraNavegacion.Name = "panelBarraNavegacion";
                this.panelBarraNavegacion.Size = new System.Drawing.Size(350, 21);
                this.panelBarraNavegacion.TabIndex = 12;
                // 
                // barraNavPag
                // 
                this.barraNavPag.AutoSize = false;
                this.barraNavPag.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
                this.barraNavPag.Location = new System.Drawing.Point(0, 2);
                this.barraNavPag.Margin = new System.Windows.Forms.Padding(0);
                this.barraNavPag.MinimumSize = new System.Drawing.Size(70, 16);
                this.barraNavPag.Name = "barraNavPag";
                this.barraNavPag.Size = new System.Drawing.Size(70, 16);
                this.barraNavPag.TabIndex = 14;
                this.barraNavPag.Visible = false;
                // 
                // panelBarraEstado
                // 
                this.panelBarraEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.panelBarraEstado.Controls.Add(this.lItem);
                this.panelBarraEstado.Controls.Add(this.lPage);
                this.panelBarraEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.panelBarraEstado.Location = new System.Drawing.Point(0, 271);
                this.panelBarraEstado.Name = "panelBarraEstado";
                this.panelBarraEstado.Size = new System.Drawing.Size(440, 20);
                this.panelBarraEstado.TabIndex = 10;
                // 
                // lItem
                // 
                this.lItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                this.lItem.AutoSize = true;
                this.lItem.ForeColor = System.Drawing.Color.Black;
                this.lItem.Location = new System.Drawing.Point(3, 3);
                this.lItem.Name = "lItem";
                this.lItem.Size = new System.Drawing.Size(0, 15);
                this.lItem.TabIndex = 9;
                // 
                // lPage
                // 
                this.lPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                this.lPage.AutoSize = true;
                this.lPage.ForeColor = System.Drawing.Color.Black;
                this.lPage.Location = new System.Drawing.Point(393, 3);
                this.lPage.Name = "lPage";
                this.lPage.Size = new System.Drawing.Size(0, 15);
                this.lPage.TabIndex = 8;
                // 
                // vista
                // 
                this.vista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.vista.CheckDuplicateImage = false;
                this.vista.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
                this.vista.Definicion = Zamba.Thumbnails.ListaImagenDefinicion.High;
                this.vista.Dock = System.Windows.Forms.DockStyle.Fill;
                this.vista.ImageLoadDelay = 9;
                //this.vista.ImageSize = 25;
                this.vista.Location = new System.Drawing.Point(0, 28);
                this.vista.MultiSelect = false;
                this.vista.Name = "vista";
                this.vista.Size = new System.Drawing.Size(440, 243);
                this.vista.Sorting = System.Windows.Forms.SortOrder.Ascending;
                this.vista.TabIndex = 0;
                this.vista.UseCompatibleStateImageBehavior = false;
                this.vista.DoubleClick += new System.EventHandler(this.vista_DobleClick);
                this.vista.SelectedIndexChanged += new System.EventHandler(this.vista_SelectedItems);
                // 
                // NewThumbnails
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.Controls.Add(this.Vista);
                this.Controls.Add(this.panelBarraTop);
                this.Controls.Add(this.panelBarraEstado);
                this.MinimumSize = new System.Drawing.Size(440, 100);
                this.Name = "NewThumbnails";
                this.Size = new System.Drawing.Size(440, 291);
                this.panelBarraTop.ResumeLayout(false);
                this.panelZoom.ResumeLayout(false);
                this.panelZoom.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.zoom)).EndInit();
                this.panelBarraNavegacion.ResumeLayout(false);
                this.panelBarraNavegacion.PerformLayout();
                this.panelBarraEstado.ResumeLayout(false);
                this.panelBarraEstado.PerformLayout();
                this.ResumeLayout(false);
                this.PerformLayout();
                
                
                this.panelBarraEstado.BackColor = Color.CornflowerBlue;
                this.panelBarraEstado.BorderStyle = BorderStyle.None;

                this.panelBarraNavegacion.BackColor = Color.CornflowerBlue;
                this.panelBarraNavegacion.BorderStyle = BorderStyle.None;

                this.panelBarraTop.BackColor = Color.CornflowerBlue;
                this.panelBarraTop.BorderStyle = BorderStyle.None;
            }



            #endregion

            private ListaImagen vista = null;
            private System.Windows.Forms.Panel panelBarraTop;
            private System.Windows.Forms.Panel panelBarraEstado;
            private System.Windows.Forms.Label lItem;
            private System.Windows.Forms.Label lPage;
            private System.Windows.Forms.Panel panelBarraNavegacion;
            private System.Windows.Forms.Panel panelZoom;
            private System.Windows.Forms.NumericUpDown zoom;
            private System.Windows.Forms.Label lZoom;
            private BarraNavegacionPagina barraNavPag;


            private ImageList defaultImage = null;







        }

    
    }
}