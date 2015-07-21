using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class ScriptDirectoriesFrm : Form
    {
        public ScriptDirectoriesFrm()
        {
            InitializeComponent();
        }

        public string SelectedPath = "";

        private void ScriptDirectoriesFrm_Load(object sender, EventArgs e)
        {
            ReloadList();
        }

        private void ReloadList()
        {
            listBox1.Items.Clear();
            string[] files = Directory.GetFiles(Scriptmonkey.ScriptPath);
            for (int i = 0; i < files.Length; i++)
            {
                listBox1.Items.Add(files[i].Replace(Scriptmonkey.ScriptPath, ""));
            }
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                SelectedPath = listBox1.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            AddScriptFrm form = new AddScriptFrm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Enabled = false; // Disable form while working
                if (form.FromFile)
                {
                    try
                    {
                        File.Copy(form.Url, Scriptmonkey.ScriptPath + form.openFileDialog1.SafeFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Resources.Title);
                    }
                    ReloadList();
                    listBox1.SelectedItem = form.openFileDialog1.SafeFileName;
                }
                else
                {
                    try
                    {
                        var webClient = new WebClient();
                        webClient.DownloadFile(form.Url, Scriptmonkey.ScriptPath + form.Url.Substring(form.Url.LastIndexOf('/')));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Resources.Title);
                    }
                    ReloadList();
                    listBox1.SelectedItem = form.Url.Substring(form.Url.LastIndexOf('/'));
                }
                Enabled = true;
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > -1)
            {
                File.Delete(Scriptmonkey.ScriptPath + listBox1.SelectedItem.ToString());
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }
    }
}
