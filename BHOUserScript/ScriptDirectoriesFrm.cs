using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

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
            string[] files = Directory.GetFiles(Scriptmonkey.scriptPath);
            for (int i = 0; i < files.Length; i++)
            {
                listBox1.Items.Add(files[i].Replace(Scriptmonkey.scriptPath, ""));
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
                        File.Copy(form.URL, Scriptmonkey.scriptPath + form.openFileDialog1.SafeFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Scriptmonkey");
                    }
                    ReloadList();
                    listBox1.SelectedItem = form.openFileDialog1.SafeFileName;
                }
                else
                {
                    try
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(form.URL, Scriptmonkey.scriptPath + form.URL.Substring(form.URL.LastIndexOf('/')));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Scriptmonkey");
                    }
                    ReloadList();
                    listBox1.SelectedItem = form.URL.Substring(form.URL.LastIndexOf('/'));
                }
                Enabled = true;
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > -1)
            {
                File.Delete(Scriptmonkey.scriptPath + listBox1.SelectedItem.ToString());
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }
    }
}
