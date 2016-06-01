using System;
using System.Drawing;
using System.Windows.Forms;
using ScintillaNET;

namespace BHOUserScript
{
    public partial class FileEditForm : Form
    {
        private string _url;
        private int _maxLineNumberCharLength;

        public FileEditForm(string url, bool isCss, string title = null)
        {
            InitializeComponent();
            _url = url;

            if (title != null)
                Text += $" ({title})";

            scintilla1.Lexer = isCss ? Lexer.Css : Lexer.Cpp;
            scintilla1.StyleResetDefault();
            scintilla1.Styles[Style.Default].Font = "Consolas";
            scintilla1.Styles[Style.Default].Size = 10;
            scintilla1.StyleClearAll();
            
            scintilla1.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            scintilla1.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0);
            scintilla1.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0);
            scintilla1.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128);
            scintilla1.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            scintilla1.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21);
            scintilla1.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21);
            scintilla1.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21);
            scintilla1.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            scintilla1.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla1.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;

            scintilla1.SetKeywords(0, isCss? Properties.Resources.CssKeywords : Properties.Resources.JsKeywords);

            scintilla1.Text = Db.ReadFile(url,String.Empty);
            statusLbl.Text = scintilla1.TextLength + @"C " + scintilla1.Lines.Count + 'L';
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            statusLbl.Text = scintilla1.TextLength + @"C " + scintilla1.Lines.Count + 'L';

            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla1.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == _maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla1.Margins[0].Width = scintilla1.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            _maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void scintilla1_CharAdded(object sender, CharAddedEventArgs e)
        {
            // Find the word start
            var currentPos = scintilla1.CurrentPosition;
            var wordStartPos = scintilla1.WordStartPosition(currentPos, true);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0)
            {
                scintilla1.AutoCShow(lenEntered, Properties.Resources.JsKeywords);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Db.WriteFile(_url, scintilla1.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
