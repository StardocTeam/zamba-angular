using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for ControlHandling
/// </summary>
public static class ControlsHelper
{
    public static IEnumerable<T> FindControlsOfType<T>(Control parent)
                                                    where T : Control
    {
        foreach (Control child in parent.Controls)
        {
            if (child is T)
            {
                yield return (T)child;
            }
            else if (child.Controls.Count > 0)
            {
                foreach (T grandChild in FindControlsOfType<T>(child))
                {
                    yield return grandChild;
                }
            }
        }
    }
}