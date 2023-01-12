using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using AdsBusiness;

public partial class ImageTemplate
    : UserControl
{

    public Boolean Checked
    {
        get { return chbSelected.Checked; }
        set { chbSelected.Checked = value; }
    }
  
    public ImageTemplate()
        : base()
    {
        imageAnchor = new HtmlAnchor();
        image = new HtmlImage();
    }
    public Int64 ImageId
    {
        get { return Int64.Parse(hdImageID.Value); }
        set { hdImageID.Value = value.ToString(); }
    }

    public ImageTemplate(Int64 imageId)
        : this()
    {
        hdImageID.Value = imageId.ToString();

    }
    private void LoadDimension(String filePath)
    {
        Image thumb = Image.FromFile(filePath);
        lbDimensions.Text = thumb.Width.ToString() + " x " + thumb.Height.ToString();
    }

    public ImageTemplate(String imagePath)
        : this()
    {
        ImagePath = imagePath;
    }

    public String ImagePath
    {
        set
        {
            image.Src = value;
            imageAnchor.HRef = value;
            LoadDimension(value);
            lbName.Text = new FileInfo(value).Name;
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        AdImage CurrentAd = new AdImage();
        CurrentAd.Id = Int64.Parse(hdImageID.Value);

        AdLogic.Delete(CurrentAd);
    }
}
