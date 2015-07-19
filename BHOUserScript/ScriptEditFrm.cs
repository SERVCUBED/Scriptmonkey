using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Diagnostics;

namespace BHOUserScript
{
    public partial class ScriptEditFrm : Form
    {
        public Script editedScript = new Script();
        public string editPath;

        public ScriptEditFrm()
        {
            InitializeComponent();
        }

        public void LoadFromEditedScript()
        {
            enabledChk.Checked = editedScript.Enabled;
            nameTxt.Text = editedScript.Name;
            authorTxt.Text = editedScript.Author;
            descriptionTxt.Text = editedScript.Description;
            fileTxt.Text = editedScript.Path;
            updateTxt.Text = editedScript.UpdateURL;
            versionTxt.Text = editedScript.Version;

            listBox1.Items.Clear();
            for (int i = 0; i < editedScript.Include.Length; i++)
            {
                listBox1.Items.Add(editedScript.Include[i]);
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (nameTxt.Text == "")
            {
                nameTxt.BackColor = Color.PaleVioletRed;
                return;
            }

            if (fileTxt.Text == "")
            {
                fileTxt.BackColor = Color.PaleVioletRed;
                return;
            }

            editedScript.Enabled = enabledChk.Checked;
            editedScript.Name = nameTxt.Text;
            editedScript.Author = authorTxt.Text;
            editedScript.Description = descriptionTxt.Text;
            editedScript.Path = fileTxt.Text;
            editedScript.InstallDate = DateTime.UtcNow;
            editedScript.UpdateURL = updateTxt.Text;
            editedScript.Version = versionTxt.Text;
            editedScript.LastUsedBHOVersion = Scriptmonkey.CurrentVersion();

            editedScript.Include = new string[listBox1.Items.Count];
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                editedScript.Include[i] = listBox1.Items[i].ToString();
            }

            DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScriptDirectoriesFrm form = new ScriptDirectoriesFrm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                fileTxt.Text = form.SelectedPath;
                LoadFromParse(ParseScriptMetadata.Parse(form.SelectedPath));
            }
        }

        private void addMatchBtn_Click(object sender, EventArgs e)
        {
            string t = Interaction.InputBox("Add URL Match");
            if (t != "")
                listBox1.Items.Add(t);
        }

        private void remMatchBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void LoadFromParse(Script s)
        {
            nameTxt.Text = s.Name;
            authorTxt.Text = s.Author;
            versionTxt.Text = s.Version;
            listBox1.Items.Clear();
            for (int i = 0; i < s.Include.Length; i++)
            {
                if (s.Include[i] != null)
                    listBox1.Items.Add(s.Include[i]);
            }
            descriptionTxt.Text = s.Description;
            updateTxt.Text = s.UpdateURL;
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (fileTxt.Text == "")
                return;
            Process p = new Process();
            p.StartInfo.FileName = editPath;
            p.StartInfo.Arguments = Scriptmonkey.scriptPath + fileTxt.Text;
            p.Start();
        }

        private void refBtn_Click(object sender, EventArgs e)
        {
            if (fileTxt.Text != "")
            {
                // Double check file actually exists
                if (System.IO.File.Exists(Scriptmonkey.scriptPath + fileTxt.Text))
                    LoadFromParse(ParseScriptMetadata.Parse(fileTxt.Text));
            }
        }
    }
}
