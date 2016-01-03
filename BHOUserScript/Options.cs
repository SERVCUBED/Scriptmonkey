using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BHOUserScript
{
    /// <summary>
    /// Main options form. Opened on menu button press.
    /// </summary>
    public partial class Options : Form
    {
        public Db Prefs;
        private readonly string _editPath;

        public Options(Db prefs)
        {
            Prefs = prefs;
            InitializeComponent();
            _editPath = Prefs.Settings.EditorPath;
            enabledChk.Checked = Prefs.Settings.Enabled;
            Text += " v" + Scriptmonkey.CurrentVersion().ToString();
            refreshChk.Checked = Prefs.Settings.RefreshOnSave;
        }
        
        private void okBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Options_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < Prefs.AllScripts.Count; i++)
            {
                listBox1.Items.Add(Prefs[i].Name + 
                    (Prefs[i].Description == "" ? "" : ": " + Prefs[i].Description));
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                ScriptEditFrm sw = new ScriptEditFrm(true)
                {
                    EditedScript = Prefs.AllScripts[listBox1.SelectedIndex],
                    EditPath = _editPath,
                    Prefs = this.Prefs
                };
                sw.LoadFromEditedScript();
                if (sw.ShowDialog() == DialogResult.OK)
                {
                    int index = listBox1.SelectedIndex;
                    Prefs.AllScripts[index] = sw.EditedScript;
                    RefreshList();
                    listBox1.SetSelected(index, true);
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            var sw = new ScriptEditFrm(false) {EditPath = _editPath, Prefs = this.Prefs};
            if (sw.ShowDialog() == DialogResult.OK)
            {
                Prefs.AllScripts.Add(sw.EditedScript);
                RefreshList();
            }
            else
            {
                // If file was selected and then form cancelled delete file
                if (sw.fileTxt.Text != "" && File.Exists(Scriptmonkey.ScriptPath + sw.fileTxt.Text))
                    File.Delete(Scriptmonkey.ScriptPath + sw.fileTxt.Text);
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                if (File.Exists(Scriptmonkey.ScriptPath + Prefs.AllScripts[listBox1.SelectedIndex].Path))
                    File.Delete(Scriptmonkey.ScriptPath + Prefs.AllScripts[listBox1.SelectedIndex].Path);
                Prefs.AllScripts.RemoveAt(listBox1.SelectedIndex);
                eachEnabledChk.Enabled = false;
                RefreshList();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            eachEnabledChk.Enabled = (listBox1.SelectedIndex > -1);

            if (listBox1.SelectedIndex > -1)
            {
                eachEnabledChk.Checked = Prefs.AllScripts[listBox1.SelectedIndex].Enabled;
            }
        }

        private void eachEnabledChk_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                Prefs.AllScripts[listBox1.SelectedIndex].Enabled = eachEnabledChk.Checked;
            }
        }

        private void enabledChk_CheckedChanged(object sender, EventArgs e)
        {
            Prefs.Settings.Enabled = enabledChk.Checked;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editBtn.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var p = new Process
            {
                StartInfo =
                {
                    FileName = "explorer.exe",
                    Arguments = Scriptmonkey.InstallPath
                }
            };
            p.Start();
            DialogResult = DialogResult.OK;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1 && listBox1.SelectedIndex > 0)
            {
                var index = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(index);
                var t = Prefs[index];
                Prefs.AllScripts.RemoveAt(index);
                Prefs.AllScripts.Insert(index - 1, t);
                RefreshList();
                listBox1.SetSelected(index - 1, true);
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1 && listBox1.SelectedIndex < listBox1.Items.Count - 1)
            {
                var index = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(index);
                var t = Prefs[index];
                Prefs.AllScripts.RemoveAt(index);
                Prefs.AllScripts.Insert(index + 1, t);
                RefreshList();
                listBox1.SetSelected(index + 1, true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetUserscriptsFrm form = new GetUserscriptsFrm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                okBtn.PerformClick();
            }
            form.Dispose();
        }

        private void Options_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void optionsBtn_Click(object sender, EventArgs e)
        {
            optionsContextMenuStrip.Show(this, new System.Drawing.Point(
                optionsBtn.Left, optionsBtn.Top + optionsBtn.Height));
        }

        private void setScriptEditorPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scriptEditorOpenFileDialog.FileName = Prefs.Settings.EditorPath;
            if (scriptEditorOpenFileDialog.ShowDialog() == DialogResult.OK)
                Prefs.Settings.EditorPath = scriptEditorOpenFileDialog.FileName;
        }

        private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prefs.Reset(new Exception("User initiated. Are you sure you want to reset? Press 'Do Nothing' to cancel."));
            RefreshList();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Prefs.Settings.RefreshOnSave = refreshChk.Checked;
        }
    }
}