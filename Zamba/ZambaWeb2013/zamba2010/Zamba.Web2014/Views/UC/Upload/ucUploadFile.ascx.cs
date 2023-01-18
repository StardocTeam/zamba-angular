using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Views_UC_Upload_ucUploadFile : System.Web.UI.UserControl
{
    public delegate void FileUploaded(string fileName);
    public delegate void FileUploadedError(string error);
    
    public event FileUploaded _FileUploadedEvent;
    public event FileUploadedError _FileUploadedErrorEvent;

    public void uploadFile()
    {
        if (!string.IsNullOrEmpty(FileUpload.FileName))
        {
            try
            {
                string fileName = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\" + FileUpload.FileName;

                if (Directory.Exists(Zamba.Membership.MembershipHelper.AppTempPath + "\\temp") == false)
                    Directory.CreateDirectory(Zamba.Membership.MembershipHelper.AppTempPath + "\\temp");

                FileUpload.SaveAs(fileName);
                _FileUploadedEvent(fileName);
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                _FileUploadedErrorEvent(ex.ToString());
            }
        }
        else
        {
            _FileUploadedErrorEvent("Debe seleccionar un archivo.");
        }
    }
}