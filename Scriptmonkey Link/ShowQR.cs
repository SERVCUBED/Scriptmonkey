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
        private Color _darkColour = Color.Black;
        private Color _borderColour = Color.Black;
        private Color _backColour = Color.White;
        private int _size = 256;
        private bool _border = false;
        private int _borderWidth = 5;

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
            var b = new Bitmap(c, c);

            for (var row = 0; row < c; row++)
            {
                for (var col = 0; col < c; col++)
                {
                    var isDark = qr.IsDark(row, col);
                    b.SetPixel(row, col, isDark ? _darkColour : _backColour);
                }
            }
            
            var newImage = new Bitmap(_size, _size, PixelFormat.Format24bppRgb);
            var g = Graphics.FromImage(newImage);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.None;
            if (_border)
            {
                g.DrawImage(b, _borderWidth, _borderWidth, _size - _borderWidth * 2, _size - _borderWidth * 2);
                RectangleF[] r = {new RectangleF(0, 0, _size, _borderWidth), // Top
                    new RectangleF(0, 0, _borderWidth, _size), // Left
                    new RectangleF(_size - _borderWidth, 0, _borderWidth, _size), // Right
                    new RectangleF(0, _size - _borderWidth, _size, _borderWidth)}; // Bottom
                g.DrawRectangles(new Pen(_borderColour, _borderWidth), r);
            }
            else
                g.DrawImage(b, 0, 0, _size, _size);
            panel1.BackgroundImage = newImage;
        }

        public static void Show(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new QROptions
            {
                borderChk = {Checked = _border},
                foreBtn = {BackColor = _darkColour},
                backBtn = {BackColor = _backColour},
                borderBtn = { BackColor = _borderColour },
                sizeNum = {Value = _size},
                widthNum = {Value = _borderWidth, Enabled = _border}
            };

            f.ShowDialog();

            _border = f.borderChk.Checked;
            _darkColour = f.foreBtn.BackColor;
            _backColour = f.backBtn.BackColor;
            _borderColour = f.borderBtn.BackColor;
            _size = (int)f.sizeNum.Value;
            _borderWidth = (int)f.widthNum.Value;

            panel1.Size = new Size(_size, _size);
            Size = new Size(_size + 34, _size + 82);
            Draw();
        }
    }
}
