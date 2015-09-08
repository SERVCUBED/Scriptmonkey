using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;

namespace BHOUserScript
{
    public partial class ScriptEditFrm : Form
    {
        public Script EditedScript = new Script();
        public Db Prefs;
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
            foreach (string t in EditedScript.Include)
            {
                listBox1.Items.Add(t);
            }

            excludesBox.Items.Clear();
            foreach (string t in EditedScript.Exclude)
            {
                excludesBox.Items.Add(t);
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

            EditedScript.Exclude = new string[excludesBox.Items.Count];
            for (int i = 0; i < excludesBox.Items.Count; i++)
            {
                EditedScript.Exclude[i] = excludesBox.Items[i].ToString();
            }

            DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddScriptFrm form = new AddScriptFrm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Enabled = false; // Disable form while working

                // Delete old script
                if (File.Exists(Scriptmonkey.ScriptPath + fileTxt.Text))
                    File.Delete(Scriptmonkey.ScriptPath + fileTxt.Text);

                if (form.FromFile)
                {
                    try
                    {
                        File.Copy(form.Url, Scriptmonkey.ScriptPath + form.openFileDialog1.SafeFileName);
                        fileTxt.Text = form.openFileDialog1.SafeFileName;
                        LoadFromParse(ParseScriptMetadata.Parse(fileTxt.Text));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Resources.Title);
                    }
                }
                else
                {
                    try
                    {
                        var webClient = new WebClient();
                        var f_ = WebUtility.HtmlDecode(form.Url);
                        webClient.DownloadFile(form.Url, Scriptmonkey.ScriptPath + f_.Substring(f_.LastIndexOf('/')));
                        fileTxt.Text = f_.Substring(f_.LastIndexOf('/'));
                        LoadFromParse(ParseScriptMetadata.Parse(fileTxt.Text));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Resources.Title);
                    }
                }
                Enabled = true;
            }
        }

        private void addMatchBtn_Click(object sender, EventArgs e)
        {
            string t = Interaction.InputBox("Add URL Match");
            if (t != "")
                listBox1.Items.Add(t);
            CheckURLWarningLabel();
        }

        private void remMatchBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            CheckURLWarningLabel();
        }

        private void LoadFromParse(Script s)
        {
            nameTxt.Text = s.Name;
            authorTxt.Text = s.Author;
            versionTxt.Text = s.Version;
            listBox1.Items.Clear();
            foreach (string t in s.Include)
            {
                if (t != null)
                    listBox1.Items.Add(t);
            }
            excludesBox.Items.Clear();
            foreach (string t in s.Exclude)
            {
                if (t != null)
                    excludesBox.Items.Add(t);
            }
            descriptionTxt.Text = s.Description;
            updateTxt.Text = s.UpdateUrl;
            CheckURLWarningLabel();
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

        private void CheckURLWarningLabel()
        {
            var is_ = listBox1.Items.Count > 0;
            noURLWarningLbl.Visible = !is_;
            listBox1.Enabled = is_;
        }

        private void CheckExcludeWarningLabel()
        {
            var is_ = excludesBox.Items.Count > 0;
            noExcludeWarningLbl.Visible = !is_;
            excludesBox.Enabled = is_;
        }

        private void ScriptEditFrm_Load(object sender, EventArgs e)
        {
            CheckURLWarningLabel();
            CheckExcludeWarningLabel();
        }

        private void addExcludeBtn_Click(object sender, EventArgs e)
        {
            string t = Interaction.InputBox("Add URL Exclude");
            if (t != "")
                excludesBox.Items.Add(t);
            CheckExcludeWarningLabel();
        }

        private void remExcludeBtn_Click(object sender, EventArgs e)
        {
            if (excludesBox.SelectedIndex > -1)
                excludesBox.Items.RemoveAt(excludesBox.SelectedIndex);
            CheckExcludeWarningLabel();
        }
    }
}
