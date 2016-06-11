using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Consulgo.QrCode4cs;

namespace Scriptmonkey_Link
{
    public partial class ShowQR : Form
    {
        private readonly string _text;
        private Color _darkColor = Color.Black;

        private ShowQR(string text)
        {
            InitializeComponent();
            _text = text;
            Draw();
        }

        private void Draw()
        {
            int typeNumber;
            if (_text.Length < 26)
                typeNumber = 2;
            else if (_text.Length < 72)
                typeNumber = 5;
            else if (_text.Length < 125)
                typeNumber = 7;
            else if (_text.Length < 203)
                typeNumber = 10;
            else if (_text.Length < 298)
                typeNumber = 12;
            else if (_text.Length < 407)
                typeNumber = 15;
            else if (_text.Length < 534)
                typeNumber = 17;
            else if (_text.Length < 669)
                typeNumber = 20;
            else
            {
                Close();
                return;
            }

            QRCode qr;
            try
            {
                qr = new QRCode(typeNumber, QRErrorCorrectLevel.M);
                qr.AddData(_text);
                qr.Make();
            }
            catch (Exception)
            {
                Close();
                return;
            }

            var c = qr.GetModuleCount();
            var bitmap = new bool[c][];
            var b = new Bitmap(c, c);

            for (var row = 0; row < c; row++)
            {
                bitmap[row] = new bool[c];

                for (var col = 0; col < c; col++)
                {
                    var isDark = qr.IsDark(row, col);
                    bitmap[row][col] = isDark;
                    b.SetPixel(row, col, isDark ? _darkColor : Color.White);
                }
            }

            var size = 256;
            var newImage = new Bitmap(size, size, PixelFormat.Format24bppRgb);
            var g = Graphics.FromImage(newImage);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.None;
            g.DrawImage(b, 0, 0, size, size);
            panel1.BackgroundImage = newImage;
        }

        public static void Show(string text)
        {
            if (text == null)
                return;
            var f = new ShowQR(text);
            f.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            (panel1.BackgroundImage as Bitmap)?.Save(saveFileDialog1.FileName, ImageFormat.Png);
        }

        private void setColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = _darkColor;
            if (colorDialog1.ShowDialog() != DialogResult.OK)
                return;
            _darkColor = colorDialog1.Color;
            Draw();
        }
    }
}
