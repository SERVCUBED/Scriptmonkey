using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BHOUserScript
{
    public partial class ScriptEditFrm : Form
    {
        public Script EditedScript = new Script();
        public string EditPath;

        public ScriptEditFrm()
        {
            InitializeComponent();
        }

        public void LoadFromEditedScript()
        {
            enabledChk.Checked = EditedScript.Enabled;
            nameTxt.Text = EditedScript.Name;
            authorTxt.Text = EditedScript.Author;
            descriptionTxt.Text = EditedScript.Description;
            fileTxt.Text = EditedScript.Path;
            updateTxt.Text = EditedScript.UpdateUrl;
            versionTxt.Text = EditedScript.Version;

            listBox1.Items.Clear();
            for (int i = 0; i < EditedScript.Include.Length; i++)
            {
                listBox1.Items.Add(EditedScript.Include[i]);
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

            EditedScript.Enabled = enabledChk.Checked;
            EditedScript.Name = nameTxt.Text;
            EditedScript.Author = authorTxt.Text;
            EditedScript.Description = descriptionTxt.Text;
            EditedScript.Path = fileTxt.Text;
            EditedScript.InstallDate = DateTime.UtcNow;
            EditedScript.UpdateUrl = updateTxt.Text;
            EditedScript.Version = versionTxt.Text;
            EditedScript.LastUsedBhoVersion = Scriptmonkey.CurrentVersion();

            EditedScript.Include = new string[listBox1.Items.Count];
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                EditedScript.Include[i] = listBox1.Items[i].ToString();
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
            updateTxt.Text = s.UpdateUrl;
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (fileTxt.Text == "")
                return;
            var p = new Process
            {
                StartInfo =
                {
                    FileName = EditPath,
                    Arguments = Scriptmonkey.ScriptPath + fileTxt.Text
                }
            };
            p.Start();
        }

        private void refBtn_Click(object sender, EventArgs e)
        {
            if (fileTxt.Text != "")
            {
                // Double check file actually exists
                if (File.Exists(Scriptmonkey.ScriptPath + fileTxt.Text))
                    LoadFromParse(ParseScriptMetadata.Parse(fileTxt.Text));
            }
        }
    }
}
