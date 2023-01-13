using System;
using System.Web.UI;
using SharpPieces.Web.Controls;
using AdsBusiness;
using System.IO;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class _Default
    : Page
{
    private Int64? AdId
    {
        get
        {
            Int64? value = null;
            Int64 TryValue;

            if (Int64.TryParse(Request.QueryString["AdId"], out TryValue))
                value = (Int64?)TryValue;

            if (null == value)
            {
                if (Int64.TryParse(Session["AdId"].ToString(), out TryValue))
                    value = (Int64?)TryValue;
            }

            return value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (AdId.HasValue)
                Session["AdId"] = AdId.Value.ToString();

            LoadImages();
        }
        catch (Exception)
        {
        }
    }
    protected void upload_FileReceived(object sender, UploadEventArgs e)
    {
        try
        {
            String directory = Server.MapPath("~/temp/");
            if (e.File != null && directory != null)
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                String FullImagePath = directory + e.File.FileName;
                if (File.Exists(FullImagePath))
                    File.Delete(FullImagePath);


                e.File.SaveAs(directory + e.File.FileName);


                FileInfo info = new FileInfo(FullImagePath);
                String FileName = info.Name.Remove(info.Name.Length - info.Extension.Length, info.Extension.Length);
                String FileExtension = info.Extension;

                AdImage UploadedImage = new AdImage(FullImagePath);
                UploadedImage.AdId = AdId.Value;
                UploadedImage.Name = FileName;
                UploadedImage.Extension = FileExtension;
                AdLogic.Insert(UploadedImage);

                AdImage UploadedMediumImage = new AdImage(FullImagePath);
                UploadedMediumImage.Size = PhotoSize.Medium;
                UploadedMediumImage.AdId = AdId.Value;
                UploadedMediumImage.Name = FileName;
                UploadedMediumImage.Extension = FileExtension;
                AdLogic.Insert(UploadedMediumImage);

                AdImage UploadedSmallImage = new AdImage(FullImagePath);
                UploadedSmallImage.Size = PhotoSize.Small;
                UploadedSmallImage.AdId = AdId.Value;
                UploadedSmallImage.Name = FileName;
                UploadedSmallImage.Extension = FileExtension;
                AdLogic.Insert(UploadedSmallImage);

                try
                {
                    File.Delete(FullImagePath);
                }
                catch (UnauthorizedAccessException uex)
                { }
            }
        }
        catch (Exception)
        {
        }
    }



    protected void btRefreshImages_Click(object sender, EventArgs e)
    {
        LoadImages();
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        try
        {
            List<Int64> ImageIds = new List<Int64>();

            ImageTemplate image = null;
            if (tblImages.Controls.Count > 0)
            {
                foreach (TableRow row in tblImages.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        foreach (Control control in cell.Controls)
                        {
                            if (control is ImageTemplate)
                            {
                                image = (ImageTemplate)control;
                                if (image.Checked)
                                    ImageIds.Add(image.ImageId);
                            }
                        }
                    }
                }
            }

            AdImage ad = new AdImage();
            foreach (Int64 id in ImageIds)
            {
                ad.Id = id;
                AdLogic.Delete(ad);
            }

            LoadImages();
        }
        catch (Exception)
        {
        }
    }


    private void LoadImages()
    {
        if (AdId.HasValue)
        {
            List<AdImage> ads = AdLogic.GetImages(AdId.Value);
            tblImages.Rows.Clear();
            if (ads != null && ads.Count > 0)
            {
                btDelete.Enabled = true;
                btRefreshImages.Enabled = true;

                String AdsDirectory = Server.MapPath("~/temp/") + AdId.ToString();
                Directory.CreateDirectory(AdsDirectory);

                foreach (String FileImage in Directory.GetFiles(AdsDirectory))
                {
                    try
                    {
                        File.Delete(FileImage);
                    }
                    catch (UnauthorizedAccessException aex)
                    {/*error de io , lo tiene otra proceso*/}
                }

                List<Control> images = new List<Control>(ads.Count);
                Control DynamicUserControl = null;
                foreach (AdImage CurrentAd in ads)
                {
                    CurrentAd.WriteTo(AdsDirectory);
                    DynamicUserControl = LoadControl("ImageTemplate.ascx");
                    ((ImageTemplate)DynamicUserControl).ImagePath = AdsDirectory + "\\" + CurrentAd.FullName;
                    ((ImageTemplate)DynamicUserControl).ImageId = CurrentAd.Id;
                    images.Add(DynamicUserControl);
                }


                TableRow CurrentRow = new TableRow();
                TableCell CurrentCell = null;
                int j = 1;
                foreach (Control CurrentControl in images)
                {
                    if (j > 8)
                    {
                        tblImages.Rows.Add(CurrentRow);
                        CurrentRow = new TableRow();
                        j = 1;
                    }
                    CurrentCell = new TableCell();
                    CurrentCell.Controls.Add(CurrentControl);
                    CurrentRow.Cells.Add(CurrentCell);
                    j++;
                }
                if (j != 0)
                    tblImages.Rows.Add(CurrentRow);
            }
            else
            {
                btDelete.Enabled = false;
                btRefreshImages.Enabled = false;
            }
        }
    }
}