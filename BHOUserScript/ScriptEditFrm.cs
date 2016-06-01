using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Runtime.InteropServices;
using BHOUserScript.Properties;

namespace BHOUserScript
{
    public partial class ScriptEditFrm : Form
    {
        public Script EditedScript = new Script();
        public string EditPath;
        public string FileName = String.Empty;
        private readonly bool _isCss;

        public ScriptEditFrm(bool editing, bool isCss)
        {
            _isCss = isCss;

            InitializeComponent();

            if (isCss)
            {
                updateTxt.Enabled = false;
                menuCmdChk.Enabled = false;
                clearValsBtn.Enabled = false;
                versionTxt.Enabled = false;
                EditedScript.Type = Script.ValueType.StyleSheet;
            }

            if (editing)
            {
                clearValsBtn.Visible = true;
                browseBtn.Enabled = false;
            }

            Text = (editing ? "Edit" : "New") + @" Users" + (isCss ? "tyle" : "cript");
        }

        public void LoadFromEditedScript()
        {
            enabledChk.Checked = EditedScript.Enabled;
            nameTxt.Text = EditedScript.Name;
            authorTxt.Text = EditedScript.Author;
            descriptionTxt.Text = EditedScript.Description;
            FileName = EditedScript.Path;
            updateTxt.Text = EditedScript.UpdateUrl;
            versionTxt.Text = EditedScript.Version;
            menuCmdChk.Checked = EditedScript.ShowMenuCommands;

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
            if (nameTxt.Text == String.Empty)
            {
                nameTxt.BackColor = Color.PaleVioletRed;
                return;
            }

            if (FileName == String.Empty)
            {
                browseBtn.Focus();
                return;
            }

            EditedScript.Enabled = enabledChk.Checked;
            EditedScript.Name = nameTxt.Text;
            EditedScript.Author = authorTxt.Text;
            EditedScript.Description = descriptionTxt.Text;
            EditedScript.Path = FileName;
            EditedScript.InstallDate = DateTime.UtcNow;
            EditedScript.UpdateUrl = updateTxt.Text;
            EditedScript.Version = versionTxt.Text;
            EditedScript.ShowMenuCommands = menuCmdChk.Checked;

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

            using(StreamReader str = new StreamReader(Scriptmonkey.ScriptPath + EditedScript.Path))
            {
                try
                {
                    string contents = str.ReadToEnd();
                    EditedScript = ParseScriptMetadata.UpdateParsedData(EditedScript, contents, 
                        EditedScript.Type == Script.ValueType.StyleSheet);
                }
                catch (Exception ex)
                {
                    if (Scriptmonkey.LogAndCheckDebugger(ex, "Unable to parse resources"))
                        throw;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddScriptFrm form = new AddScriptFrm(_isCss);
            if (form.ShowDialog() == DialogResult.OK && !String.IsNullOrWhiteSpace(form.Url))
            {
                Enabled = false; // Disable form while working

                // Delete old script
                if (!String.IsNullOrEmpty(FileName) && File.Exists(Scriptmonkey.ScriptPath + FileName))
                    File.Delete(Scriptmonkey.ScriptPath + FileName);

                string prefix = Scriptmonkey.GenerateRandomString();

                if (form.FromFile)
                {
                    try
                    {
                        if (!File.Exists(form.Url))
                        {
                            MessageBox.Show(@"File does not exist or access is denied.", Resources.Title, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            form.Dispose();
                            return;
                        }

                        File.Copy(form.Url, Scriptmonkey.ScriptPath + prefix + form.openFileDialog1.SafeFileName);
                        FileName = prefix + form.openFileDialog1.SafeFileName;
                        LoadFromParse(ParseScriptMetadata.Parse(FileName, _isCss));
                        browseBtn.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        if (Scriptmonkey.LogAndCheckDebugger(ex))
                            throw;
                    }
                }
                else
                {
                    try
                    {
                        var webClient = new WebClient();
                        FileName = prefix + (_isCss? ".css" : ".user.js");
                        webClient.DownloadFile(form.Url, Scriptmonkey.ScriptPath + FileName);
                        LoadFromParse(ParseScriptMetadata.Parse(FileName, _isCss));
                        browseBtn.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Unable to download file: " + Environment.NewLine + ex.Message, Resources.Title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FileName = String.Empty;
                        if (Scriptmonkey.LogAndCheckDebugger(ex))
                            throw;
                    }
                    if (form.Url.StartsWith("https://servc.eu/p/scriptmonkey/new_files/"))
                    {
                        // Show edit file dialog
                        editBtn_Click(null, null);
                        // As a template has been used, the complete metadata may have changed, so check again
                        LoadFromParse(ParseScriptMetadata.Parse(FileName, _isCss));
                    }
                }
                Enabled = true;
            }
            form.Dispose();
        }

        private void addMatchBtn_Click(object sender, EventArgs e)
        {
            string t = Interaction.InputBox("Add URL Match");
            if (t != String.Empty)
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
            foreach (string t in s.Include.Where(t => t != null))
            {
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
            enabledChk.Checked = s.Enabled;
            CheckURLWarningLabel();
        }


        [DllImport("shell32.dll", EntryPoint = "PathIsExe", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PathIsExe([MarshalAs(UnmanagedType.LPWStr)]string szfile);

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (FileName == String.Empty)
            {
                browseBtn.Focus();
                return;
            }

            var f = new FileEditForm(Scriptmonkey.ScriptPath + FileName, _isCss, nameTxt.Text);
            if (f.ShowDialog() == DialogResult.Retry && PathIsExe(EditPath))
            {
                var p = new Process
                {
                    StartInfo =
                {
                    FileName = EditPath,
                    Arguments = Scriptmonkey.ScriptPath + FileName
                }
                };
                p.Start();
            }
        }

        private void refBtn_Click(object sender, EventArgs e)
        {
            if (FileName == String.Empty) return;

            // Double check file actually exists
            if (File.Exists(Scriptmonkey.ScriptPath + FileName))
                LoadFromParse(ParseScriptMetadata.Parse(FileName, _isCss));
            else
            {
                MessageBox.Show(@"File has been deleted or is inaccessible. Select another file.", Resources.Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                browseBtn.Enabled = true;
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
            if (t != String.Empty)
                excludesBox.Items.Add(t);
            CheckExcludeWarningLabel();
        }

        private void remExcludeBtn_Click(object sender, EventArgs e)
        {
            if (excludesBox.SelectedIndex > -1)
                excludesBox.Items.RemoveAt(excludesBox.SelectedIndex);
            CheckExcludeWarningLabel();
        }

        private void clearValsBtn_Click(object sender, EventArgs e)
        {
            EditedScript.SavedValues = new System.Collections.Generic.Dictionary<string, string>();
            EditedScript.MenuCommands = null;
        }

        private void editIncludeBtn_Click(object sender, EventArgs e) => EditValue(listBox1);

        private void editExcludeBtn_Click(object sender, EventArgs e) => EditValue(excludesBox);

        private void EditValue(ListBox box)
        {
            if (box.SelectedIndex >= 0)
            {
                string s = Interaction.InputBox("Edit value:", Resources.Title, box.SelectedItem.ToString());
                if (!String.IsNullOrEmpty(s))
                    box.Items[box.SelectedIndex] = s;
            }
        }
    }
}
