using System;
using System.Web.UI;

public partial class ErrorHandler
    : UserControl
{
    public Exception Error
    {
        set
        {
            if (null != value)
            {
                lbTitle.Text = value.Message;                
                lbError.Text = value.StackTrace;
            }
        }
    }
}
