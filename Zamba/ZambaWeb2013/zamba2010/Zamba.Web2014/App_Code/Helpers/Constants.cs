using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Constants
/// </summary>
public class Constants
{
    public String PathExceptions = Zamba.Membership.MembershipHelper.AppTempPath  + "\\exceptions";
    public String PathRules = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Bin";
}
