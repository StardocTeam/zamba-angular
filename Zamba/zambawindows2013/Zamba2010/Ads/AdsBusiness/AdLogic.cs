using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using AdsData;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AdsBusiness
{
    public static class AdLogic
    {
        #region Constantes
        private const int MEDIUM_IMAGE_HEIGHT = 102;
        private const int MEDIUM_IMAGE_WIDTH = 136;
        private const int SMALL_IMAGE_HEIGHT = 42;
        private const int SMALL_IMAGE_WIDTH = 56;

        #endregion


        public static void Insert(AdImage ad)
        {
            if (Exists(ad))
                DeleteByName(ad);

            AdDB.Insert(ad.AdId, ad.Name, ad.Extension, ad.ImageBinary, ad.IsMain, DateTime.Now, ParsePhotoSize(ad.Size));
        }

        public static void Delete(AdImage ad)
        {
            AdDB.Delete(ad.Id);
        }

        public static void DeleteByName(AdImage ad)
        {
            AdDB.Delete(ad.AdId, ad.Name, ad.Extension);
        }


        public static Boolean Exists(AdImage ad)
        {
            return (AdDB.Exists(ad.AdId, ad.Name, ad.Extension) > 0);
        }

        public static List<AdImage> GetImages(Int64 adId)
        {

            List<AdImage> ads = new List<AdImage>();
            AdImage CurrentAd = null;
            foreach (DataRow dr in AdDB.Get(adId).Rows)
            {
                CurrentAd = new AdImage();

                CurrentAd.Id = Int64.Parse(dr["id"].ToString());
                CurrentAd.AdId = Int64.Parse(dr["AdId"].ToString());
                CurrentAd.Name = dr["FileName"].ToString();
                CurrentAd.Extension = dr["FileExtension"].ToString();
                CurrentAd.ImageBinary = (byte[])dr["Bynary"];
                CurrentAd.IsMain = Boolean.Parse(dr["IsMain"].ToString());
                CurrentAd.CreationDate = DateTime.Parse(dr["CreationDate"].ToString());
                CurrentAd.Size = ParsePhotoSize(dr["ImageSize"].ToString().ToCharArray()[0]);

                ads.Add(CurrentAd);
            }

            return ads;
        }


        public static byte[] ResizeImageFile(byte[] imageBinary, PhotoSize size)
        {
            if (size == PhotoSize.Large)
                return imageBinary;

            using (Image original = Image.FromStream(new MemoryStream(imageBinary)))
            {
                Int32 targetH, targetW;
                if (original.Height > original.Width)
                {
                    if (size == PhotoSize.Small)
                        targetH = SMALL_IMAGE_HEIGHT;
                    else
                        targetH = MEDIUM_IMAGE_HEIGHT;

                    targetW = (int)(original.Width * ((float)targetH / (float)original.Height));
                }
                else
                {
                    if (size == PhotoSize.Small)
                        targetW = SMALL_IMAGE_WIDTH;
                    else
                        targetW = MEDIUM_IMAGE_WIDTH;

                    targetH = (int)(original.Height * ((float)targetW / (float)original.Width));
                }

                using (Image imgPhoto = Image.FromStream(new MemoryStream(imageBinary)))
                {
                    // Create a new blank canvas.  The resized image will be drawn on this canvas.
                    using (Bitmap bmPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb))
                    {
                        bmPhoto.SetResolution(72, 72);

                        using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
                        {
                            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, targetW, targetH), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);

                            MemoryStream mm = new MemoryStream();
                            bmPhoto.Save(mm, ImageFormat.Jpeg);
                            return mm.GetBuffer();
                        }
                    }
                }
            }
        }


        private static char ParsePhotoSize(PhotoSize size)
        {
            char value = 'l';

            switch (size)
            {
                case PhotoSize.Small:
                    value = 's';
                    break;
                case PhotoSize.Medium:
                    value = 'm';
                    break;
                case PhotoSize.Large:
                    value = 'l';
                    break;
                default:
                    break;
            }

            return value;
        }

        private static PhotoSize ParsePhotoSize(char value)
        {

            if (value == 's')
                return PhotoSize.Small;
            if (value == 'm')
                return PhotoSize.Medium;
            if (value == 'l')
                return PhotoSize.Large;

            return PhotoSize.Large;
        }

    }
}

