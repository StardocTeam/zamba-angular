using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;

namespace AdsBusiness
{
    public sealed class AdImage
    {
    
        #region Atributos
        private Int64 _id;
        private Int64 _adId;
        private String _name;
        private String _extension;
        private DateTime _creationDate;
        private Byte[] _imageBinary;
        private Boolean _isMain;
        private PhotoSize _size; 
        #endregion

        #region Propiedades
        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int64 AdId
        {
            get { return _adId; }
            set { _adId = value; }
        }
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public String Extension
        {
            get { return _extension; }
            set { _extension = value; }
        }
        public String FullName
        {
            get
            {
                String name = _name;

                switch (_size)
                {
                    case PhotoSize.Small:
                        name += "_small";
                        break;
                    case PhotoSize.Medium:
                        name += "_medium";
                        break;
                    default:
                        break;
                }
                name += _extension;

                return name;
            }
        }
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
        public Byte[] ImageBinary
        {
            get { return _imageBinary; }
            set { _imageBinary = value; }
        }
        public Boolean IsMain
        {
            get { return _isMain; }
            set { _isMain = value; }
        }
        public PhotoSize Size
        {
            get { return _size; }
            set
            {
                _size = value;
                _imageBinary = AdLogic.ResizeImageFile(_imageBinary, value);
            }
        } 
        #endregion

        #region Constructores
        public AdImage()
        { }
        public AdImage(string filePath)
        {
            this.LoadImage(filePath);
        }
        #endregion

        public void LoadImage(String originalImage)
        {
            MemoryStream stream = null;
            Image image = null;
            try
            {
                image = Image.FromFile(originalImage);
                stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                _imageBinary = stream.ToArray();
            }
            finally
            {
                if (null != image)
                {
                    image.Dispose();
                    image = null;
                }
                if (null != stream)
                {
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
            }
        }

        public void WriteTo(String directory)
        {

            String ImagePath = directory + "\\" + FullName;
            MemoryStream ms = null;
            Image returnImage = null;
            try
            {
                if (File.Exists(ImagePath))
                    File.Delete(ImagePath);

                ms = new MemoryStream(_imageBinary);
                returnImage = Image.FromStream(ms);
                returnImage.Save(ImagePath);
            }
            catch (UnauthorizedAccessException iex)
            {/*error de io , lo tiene otra proceso*/            }
            catch (Exception ex)
            { }
            finally
            {
                if (null != returnImage)
                {
                    returnImage.Dispose();
                    returnImage = null;
                }
                if (null != ms)
                {
                    ms.Close();
                    ms.Dispose();
                    ms = null;
                }
            }
        }
    }
}
