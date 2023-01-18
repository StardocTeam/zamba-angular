// --------------------------------------------------------------------------
// Copyright (c) MEMOS Software s.r.o. All rights reserved.
//
// Outlook Panel Demonstration
// 
// File     : OutlookThemes.cs
// Author   : Lukas Neumann <lukas.neumann@memos.cz>, Roman Stefko <roman.stefko@memos.cz>
// Created  : 080622
//
// -------------------------------------------------------------------------- 

using System;
using System.Drawing;
using Microsoft.Win32;

namespace OutlookPanel
{
    /// <summary>
    /// Class to determine the theme of Outlook which is active
    /// </summary>
    public static class OutlookThemes
    {
        /// <summary>
        /// Border color
        /// </summary>
        private static readonly Color[] _borderColor = { Color.FromArgb(10, 36, 106), Color.FromArgb(0, 0, 128), Color.FromArgb(75, 75, 111), Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111) };

       
        /// <summary>
        /// Gradien light color
        /// </summary>
        private static readonly Color[] _backgroundColor = { Color.FromArgb(245, 245, 244), Color.FromArgb(195, 218, 249), Color.FromArgb(243, 243, 247), Color.FromArgb(242, 240, 228), Color.FromKnownColor(KnownColor.Control) };

        /// <summary>
        /// Gets current theme.
        /// </summary>
        /// <returns></returns>
        public static XpTheme CurrentTheme
        {
            get
            {
                if (!SafeNativeMethods.IsThemeActive())
                    return XpTheme.None;

                RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\ThemeManager"
                    );

                if (key != null)
                {
                    if ((string)key.GetValue("ThemeActive") == "1")
                    {
                        // Get current theme name.
                        string theme = (string)key.GetValue("ColorName");

                        if (theme != null)
                        {
                            if (String.Compare(theme, "NormalColor", true) == 0)
                                return XpTheme.Blue;

                            if (String.Compare(theme, "Metallic", true) == 0)
                                return XpTheme.Silver;

                            if (String.Compare(theme, "HomeStead", true) == 0)
                                return XpTheme.Green;
                        }
                    }
                }

                return XpTheme.None;
            }
        }


        /// <summary>
        /// Gets background color.
        /// </summary>
        public static Color BackgroundColor
        {
            get { return _backgroundColor[(int) CurrentTheme]; }
        }

        /// <summary>
        /// Gets border color.
        /// </summary>
        /// <returns></returns>
        public static Color BorderColor
        {
            get { return _borderColor[(int) CurrentTheme]; }
        }
        
    }

    /// <summary>
    /// Enumeration of xp themes.
    /// </summary>
    public enum XpTheme
    {
     
        /// <summary>
        /// No active theme.
        /// </summary>
        None = 0,

        /// <summary>
        /// Blue theme.
        /// </summary>
        Blue = 1,

        /// <summary>
        /// Silver theme.
        /// </summary>
        Silver = 2,

        /// <summary>
        /// Green theme.
        /// </summary>
        Green = 3,

        /// <summary>
        /// Gray style.
        /// </summary>
        Gray=4
    }
}