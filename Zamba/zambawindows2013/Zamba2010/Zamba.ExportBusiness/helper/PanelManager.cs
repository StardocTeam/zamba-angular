// --------------------------------------------------------------------------
// Copyright (c) MEMOS Software s.r.o. All rights reserved.
//
// Outlook Panel Demonstration
// 
// File     : PanelManager.cs
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
    /// Class that manages nested panel control
    /// </summary>
    public class PanelManager : IDisposable
    {
        private PanelContainer _panelContainer;
        private SubclassedWindow _subclassedSiblingWindow;
        private bool _changingSize;
        private bool _colapsed;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parentWindow">Parent window handle, most likely a main window of host application</param>
        /// <param name="siblingWindow">Sibling window from which we will 'steal' the space to display our own panel</param>
        public PanelManager(IntPtr parentWindow, IntPtr siblingWindow)
        {
            this._parentWindow = parentWindow;
            this._siblingWindow = siblingWindow;
                    
        }

        private readonly IntPtr _parentWindow;

        /// <summary>
        /// Parent window handle
        /// </summary>
        public IntPtr ParentWindow
        {
            get { return _parentWindow; }
        }

        private readonly IntPtr _siblingWindow;

        /// <summary>
        /// Sibling window handle
        /// </summary>
        public IntPtr SiblingWindow
        {
            get { return _siblingWindow; }
        }


        /// <summary>
        /// Clean up method
        /// </summary>
        public void Dispose()
        {
            //Dispose the container (if it was initialised)
            if (_panelContainer != null)
                _panelContainer.Dispose();

            //Dispose the subclassing wrapper (if it was initialised)
            if (_subclassedSiblingWindow != null)
                _subclassedSiblingWindow.ReleaseHandle();
            
        }

        /// <summary>
        /// Show bar control
        /// </summary>
        /// <param name="nestedControl">Nested custom panel to show</param>
        public void ShowBarControl(UserControl nestedControl)
        {
            //Create new container instance
            _panelContainer = new PanelContainer();
           // _panelContainer.BackColor = OutlookPanel.OutlookThemes.BackgroundColor;
            _panelContainer.Width = 32;
            _panelContainer.btnShowBar.Click += new EventHandler(btnShowBar_Click);
            _panelContainer.splitContainer1.SplitterMoved += new SplitterEventHandler(splitContainer1_SplitterMoved);
            //Set the parent window of the bar container
            SafeNativeMethods.SetParent(_panelContainer.Handle, this.ParentWindow );

            //Resize both sibling and our container
            ResizePanels();

            //Subclass sibling window to monitor SizeChange event
            _subclassedSiblingWindow = new SubclassedWindow();
            _subclassedSiblingWindow.AssignHandle(this.SiblingWindow);
            _subclassedSiblingWindow.SizeChanged += new EventHandler(subclassedSiblingWindow_SizeChanged);

            //Attach nested panel to our container
            _panelContainer.AttachControl(nestedControl);
            _panelContainer.Show();
            
        }

        void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this._panelContainer.splitContainer1.SplitterDistance = 15;
        }

        /// <summary>
        /// Colapsa o expande el panel de exportacion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] - 02/06/2010 Created.
        /// </history>
        void btnShowBar_Click(object sender, EventArgs e)
        {
            if (!this._colapsed)
            {
                _panelContainer.Width = 245;
                this._colapsed = true;
            }
            else
            {
                _panelContainer.Width = 32;
                this._colapsed = false;
            }
            ResizePanels();
        }

        /// <summary>
        /// Muestra o oculta el panel de exportacion.
        /// </summary>
        /// <param name="show"></param>
        /// <history>
        /// [Ezequiel] - 02/06/2010 Created.
        /// </history>
        internal void ShowExportPanel(bool show)
        {
            if (show)
            {
                _panelContainer.Width = 245;
                this._colapsed = true;
            }
            else
            {
                _panelContainer.Width = 32;
                this._colapsed = false;
            }
            ResizePanels();
        }
               
        /// <summary>
        /// Resize original sibling window and our panel container window
        /// </summary>
        private void ResizePanels()
        {
            if (_changingSize)
                return; //Prevent infinite loops

            _changingSize = true;
            
            try
            {
                //Get size of the sibling window and main parent window
                Rectangle siblingRect = SafeNativeMethods.GetWindowRectange(this.SiblingWindow);
                Rectangle parentRect = SafeNativeMethods.GetWindowRectange(this.ParentWindow);

                //Calculate position of sibling window in screen coordinates
                SafeNativeMethods.POINT topLeft = new SafeNativeMethods.POINT(siblingRect.Left, siblingRect.Top);
                SafeNativeMethods.ScreenToClient(this.ParentWindow, ref topLeft);

                //Decrease size of the sibling window
                int newWidth = parentRect.Width - topLeft.X - _panelContainer.Width;
                SafeNativeMethods.SetWindowPos(this.SiblingWindow, IntPtr.Zero, 0, 0, newWidth, siblingRect.Height, SafeNativeMethods.SWP_NOMOVE | SafeNativeMethods.SWP_NOZORDER);

                //Move the bar to correct position
                _panelContainer.Left = topLeft.X + newWidth;
                _panelContainer.Top = topLeft.Y;

                //Set correct height of the panel container
                _panelContainer.Height = siblingRect.Height;
                _panelContainer.splitContainer1.Height = siblingRect.Height;
            }
            finally
            {
                _changingSize = false;
            }
        }
     
        /// <summary>
        /// Called when size of the sibling window has been changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subclassedSiblingWindow_SizeChanged(object sender, EventArgs e)
        {
            //Since sibling has changed its size, we need to resize both windows again
            ResizePanels();
        }
    }
}