// --------------------------------------------------------------------------
// Copyright (c) MEMOS Software s.r.o. All rights reserved.
//
// Outlook Panel Demostration
// 
// File     : PanelContainer.cs
// Author   : Lukas Neumann <lukas.neumann@memos.cz>
// Created  : 080622
//
// -------------------------------------------------------------------------- 

using System;
using System.Drawing;
using System.Windows.Forms;

namespace OutlookPanel
{
    /// <summary>
    /// Container control which mimics Outlook panel drawing style and which holds custom nested panel
    /// </summary>
    partial class PanelContainer : UserControl
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PanelContainer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Attach nested panel control
        /// </summary>
        /// <param name="control"></param>
        public void AttachControl(UserControl control)
        {
            this.splitContainer1.Panel2.Controls.Add(control);
            control.Dock = DockStyle.Fill;
           // control.BackColor = OutlookThemes.BackgroundColor;
        }

        /// <summary>
        /// Custom procedure for background drawing
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            using (Pen pen = new Pen(OutlookThemes.BorderColor))
            {
                //Draw a border around the control, to provide Outlook-like design
                Rectangle rect = new Rectangle(this.pnlContainer.Left - 1, this.pnlContainer.Top - 1, this.pnlContainer.Width + 1, this.pnlContainer.Height + 1);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        /// <summary>
        /// Size change procedure
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            //The size has been change, invalidate the control
            this.Invalidate();
        }

        /// <summary>
        /// Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarControlContainer_Load(object sender, EventArgs e)
        {
            //Set the background color according to the current Outlook theme
            this.BackColor = OutlookThemes.BackgroundColor;
        }
    }
}