// --------------------------------------------------------------------------
// Copyright (c) MEMOS Software s.r.o. All rights reserved.
//
// Outlook Panel Demonstration
// 
// File     : SubclassedWindow.cs
// Author   : Lukas Neumann <lukas.neumann@memos.cz>
// Created  : 080622
//
// -------------------------------------------------------------------------- 

using System;
using System.Windows.Forms;
using OutlookPanel;

namespace OutlookPanel
{
    /// <summary>
    /// Helper class to catch WM_SIZE messages via subclassing
    /// </summary>
    sealed class SubclassedWindow : NativeWindow
    {
        /// <summary>
        /// Size of subclassed window has changed
        /// </summary>
        public event EventHandler SizeChanged;

        /// <summary>
        /// Window procedure
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == (int)SafeNativeMethods.WindowsMessages.WM_SIZE)
                OnSizeChanged();
        }

        /// <summary>
        /// Raise SizeChanged event
        /// </summary>
        private void OnSizeChanged()
        {
            if (SizeChanged != null)
                SizeChanged(this, null);
        }
    }
}