using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

/// <summary>
/// Clase base de la vista de botones dinamicos
/// </summary>
public partial class DynamicButtonPartialViewBase : UserControl
{
    List<IDynamicButton> _renderButtons;
    /// <summary>
    /// Modelo a renderar
    /// </summary>
    public List<IDynamicButton> RenderButtons 
    { 
        get
        { 
            return _renderButtons; 
        }
        set
        {
            _renderButtons = value;
        }
    }

    public DynamicButtonPartialViewBase()
    {
        _renderButtons = null;
    }

    public DynamicButtonPartialViewBase(List<IDynamicButton> model)
    {
        _renderButtons = model;
    }
}