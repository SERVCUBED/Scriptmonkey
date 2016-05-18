using System;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class FileEditForm : Form
    {
        private string _url;

        public FileEditForm(string url, string title = null)
        {
            InitializeComponent();
            _url = url;

            if (title != null)
                Text += $" ({title})";

            richTextBox1.Text = Db.ReadFile(url,String.Empty);
            statusLbl.Text = richTextBox1.TextLength + @"C " + richTextBox1.Lines.Length + 'L';
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            statusLbl.Text = richTextBox1.TextLength + @"C " + richTextBox1.Lines.Length + 'L';
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Db.WriteFile(_url, richTextBox1.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }
    }
}
