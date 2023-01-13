using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrintControl
{
    public class PrintBarcodes
    {

      
        public  int Width = 424;

        //private Panel panel1;

        public enum AlignType
        {
            Left, Center, Right
        }

        public enum BarCodeWeight
        {
            Small = 1, Medium, Large
        }
        private AlignType align = AlignType.Center;
        private String code = "1234567890";
        public int leftMargin = 10;
        public int topMargin = 10;
        public  int height = 50;
        private bool showHeader;
        public bool showFooter;
        public String headerText = "Zamba Software";
        private BarCodeWeight weight = BarCodeWeight.Small;
        private Font headerFont = new Font("Courier", 18);
        private Font footerFont = new Font("Courier", 8);


        public String BarCode
        {
            get { return code; }
            set { code = value; }
        }

        public AlignType VertAlign
        {
            get { return align; }
            set { align = value;  }
        }

                String alphabet39 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";

        String[] coded39Char = 
		{
			/* 0 */ "000110100", 
			/* 1 */ "100100001", 
			/* 2 */ "001100001", 
			/* 3 */ "101100000",
			/* 4 */ "000110001", 
			/* 5 */ "100110000", 
			/* 6 */ "001110000", 
			/* 7 */ "000100101",
			/* 8 */ "100100100", 
			/* 9 */ "001100100", 
			/* A */ "100001001", 
			/* B */ "001001001",
			/* C */ "101001000", 
			/* D */ "000011001", 
			/* E */ "100011000", 
			/* F */ "001011000",
			/* G */ "000001101", 
			/* H */ "100001100", 
			/* I */ "001001100", 
			/* J */ "000011100",
			/* K */ "100000011", 
			/* L */ "001000011", 
			/* M */ "101000010", 
			/* N */ "000010011",
			/* O */ "100010010", 
			/* P */ "001010010", 
			/* Q */ "000000111", 
			/* R */ "100000110",
			/* S */ "001000110", 
			/* T */ "000010110", 
			/* U */ "110000001", 
			/* V */ "011000001",
			/* W */ "111000000", 
			/* X */ "010010001", 
			/* Y */ "110010000", 
			/* Z */ "011010000",
			/* - */ "010000101", 
			/* . */ "110000100", 
			/*' '*/ "011000100",
			/* $ */ "010101000",
			/* / */ "010100010", 
			/* + */ "010001010", 
			/* % */ "000101010", 
			/* * */ "010010100" 
		};


        public System.Drawing.Printing.PrintPageEventArgs PrintImage(System.Drawing.Printing.PrintPageEventArgs e)
        {
            String intercharacterGap = "0";
            String str = '*' + code.ToUpper() + '*';
            int strLength = str.Length;

            for (int i = 0; i < code.Length; i++)
            {
                if (alphabet39.IndexOf(code[i]) == -1 || code[i] == '*')
                {
                    //e.Graphics.DrawString("INVALID BAR CODE TEXT", Font, Brushes.Red, 10, 10);
                    return e;
                }
            }

            String encodedString = "";

            for (int i = 0; i < strLength; i++)
            {
                if (i > 0)
                    encodedString += intercharacterGap;

                encodedString += coded39Char[alphabet39.IndexOf(str[i])];
            }

            int encodedStringLength = encodedString.Length;
            int widthOfBarCodeString = 0;
            double wideToNarrowRatio = 3;


            if (align != AlignType.Left)
            {
                for (int i = 0; i < encodedStringLength; i++)
                {
                    if (encodedString[i] == '1')
                        widthOfBarCodeString += (int)(wideToNarrowRatio * (int)weight);
                    else
                        widthOfBarCodeString += (int)weight;
                }
            }

            int x = 0;
            int wid = 0;
            int yTop = 0;
            SizeF hSize = e.Graphics.MeasureString(headerText, headerFont);
            SizeF fSize = e.Graphics.MeasureString(code, footerFont);

            int headerX = 0;
            int footerX = 0;

            if (align == AlignType.Left)
            {
                x = leftMargin;
                headerX = leftMargin;
                footerX = leftMargin;
            }
            else if (align == AlignType.Center)
            {
                x = (Width - widthOfBarCodeString) / 2;
                headerX = (Width - (int)hSize.Width) / 2;
                footerX = (Width - (int)fSize.Width) / 2;
            }
            else
            {
                x = Width - widthOfBarCodeString - leftMargin;
                headerX = Width - (int)hSize.Width - leftMargin;
                footerX = Width - (int)fSize.Width - leftMargin;
            }

            if (showHeader)
            {
                yTop = (int)hSize.Height + topMargin;
                e.Graphics.DrawString(headerText, headerFont, Brushes.Black, headerX, topMargin);
            }
            else
            {
                yTop = topMargin;
            }

            for (int i = 0; i < encodedStringLength; i++)
            {
                if (encodedString[i] == '1')
                    wid = (int)(wideToNarrowRatio * (int)weight);
                else
                    wid = (int)weight;

                e.Graphics.FillRectangle(i % 2 == 0 ? Brushes.Black : Brushes.White, x, yTop, wid, height);

                x += wid;
            }

            yTop += height;

            if (showFooter)
                e.Graphics.DrawString(code, footerFont, Brushes.Black, footerX, yTop);
            return e;
        }

        public PaintEventArgs PaintImage(System.Windows.Forms.PaintEventArgs e)
        {
            String intercharacterGap = "0";
            String str = '*' + code.ToUpper() + '*';
            int strLength = str.Length;

            for (int i = 0; i < code.Length; i++)
            {
                if (alphabet39.IndexOf(code[i]) == -1 || code[i] == '*')
                {
                    //e.Graphics.DrawString("INVALID BAR CODE TEXT", Font, Brushes.Red, 10, 10);
                    return e;
                }
            }

            String encodedString = "";

            for (int i = 0; i < strLength; i++)
            {
                if (i > 0)
                    encodedString += intercharacterGap;

                encodedString += coded39Char[alphabet39.IndexOf(str[i])];
            }

            int encodedStringLength = encodedString.Length;
            int widthOfBarCodeString = 0;
            double wideToNarrowRatio = 3;


            if (align != AlignType.Left)
            {
                for (int i = 0; i < encodedStringLength; i++)
                {
                    if (encodedString[i] == '1')
                        widthOfBarCodeString += (int)(wideToNarrowRatio * (int)weight);
                    else
                        widthOfBarCodeString += (int)weight;
                }
            }

            int x = 0;
            int wid = 0;
            int yTop = 0;
            SizeF hSize = e.Graphics.MeasureString(headerText, headerFont);
            SizeF fSize = e.Graphics.MeasureString(code, footerFont);

            int headerX = 0;
            int footerX = 0;

            //			if (align == 1AlignType.Left)
            //			{
            x = leftMargin;
            headerX = leftMargin;
            footerX = leftMargin;
            //			}
            //			else if (align == AlignType.Center)
            //			{
            //				x = (Width - widthOfBarCodeString) / 2;
            //				headerX = (Width - (int)hSize.Width) / 2;
            //				footerX = (Width - (int)fSize.Width) / 2;
            //			}
            //			else
            //			{
            //				x = Width - widthOfBarCodeString - leftMargin;
            //				headerX = Width - (int)hSize.Width - leftMargin;
            //				footerX = Width - (int)fSize.Width - leftMargin;
            //			}

            if (showHeader)
            {
                yTop = (int)hSize.Height + topMargin;
                e.Graphics.DrawString(headerText, headerFont, Brushes.Black, headerX, topMargin);
            }
            else
            {
                yTop = topMargin;
            }

            for (int i = 0; i < encodedStringLength; i++)
            {
                if (encodedString[i] == '1')
                    wid = (int)(wideToNarrowRatio * (int)weight);
                else
                    wid = (int)weight;

                e.Graphics.FillRectangle(i % 2 == 0 ? Brushes.Black : Brushes.White, x, yTop, wid, height);

                x += wid;
            }

            yTop += height;

            if (showFooter)
                e.Graphics.DrawString(code, footerFont, Brushes.Black, footerX, yTop);
            return e;
        }




    }
}
