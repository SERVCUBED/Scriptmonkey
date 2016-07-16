using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BHOUserScript
{
    /// <summary>
    /// Main options form. Opened on menu button press.
    /// </summary>
    public partial class Options : Form
    {
        public readonly Db Prefs;

        public Options(Db prefs)
        {
            Prefs = prefs;
            InitializeComponent();
            enabledChk.Checked = Prefs.Settings.Enabled;
            Debug.Assert(Text != null, "Text != null");
            Text += " v" + Scriptmonkey.CurrentVersion();
#if DEBUG
            Text += @"d";
#else
            Text += @"r";
#endif
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
                    (Prefs[i].Description == String.Empty ? String.Empty : ": " + Prefs[i].Description));
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                ScriptEditFrm sw = new ScriptEditFrm(true, 
                    Prefs.AllScripts[listBox1.SelectedIndex].Type == Script.ValueType.StyleSheet)
                {
                    EditedScript = Prefs.AllScripts[listBox1.SelectedIndex]
                };
                sw.LoadFromEditedScript();
                if (sw.ShowDialog() == DialogResult.OK)
                {
                    int index = listBox1.SelectedIndex;
                    Prefs.AllScripts[index] = sw.EditedScript;
                    RefreshList();
                    listBox1.SetSelected(index, true);
                }
                sw.Dispose();
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            addContextMenuStrip1.Show(this, new System.Drawing.Point(
                addBtn.Left, addBtn.Top + addBtn.Height));
        }

        private void addItem(Script.ValueType type)
        {
            var sw = new ScriptEditFrm(false, type == Script.ValueType.StyleSheet);
            
            if (sw.ShowDialog() == DialogResult.OK)
            {
                Prefs.AllScripts.Add(sw.EditedScript);
                RefreshList();
            }
            else
            {
                // If file was selected and then form cancelled delete file
                if (sw.FileName != String.Empty && File.Exists(Scriptmonkey.ScriptPath + sw.FileName))
                    File.Delete(Scriptmonkey.ScriptPath + sw.FileName);
            }
            sw.Dispose();
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            if (File.Exists(Scriptmonkey.ScriptPath + Prefs.AllScripts[listBox1.SelectedIndex].Path))
                File.Delete(Scriptmonkey.ScriptPath + Prefs.AllScripts[listBox1.SelectedIndex].Path);

            // Delete any saved resources
            if (Prefs[listBox1.SelectedIndex].Resources.Count > 0)
            {
                foreach (var u in Prefs[listBox1.SelectedIndex].Resources.Select(resource => 
                Scriptmonkey.ResourcePath + Prefs[listBox1.SelectedIndex].Path + '.' + resource.Key).Where(File.Exists))
                {
                    try
                    {
                        File.Delete(u);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

            Prefs.AllScripts.RemoveAt(listBox1.SelectedIndex);
            eachEnabledChk.Enabled = false;
            RefreshList();
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
            AdvancedOptionsFrm frm = new AdvancedOptionsFrm(Prefs.Settings);
            if (frm.ShowDialog() == DialogResult.OK)
                Prefs.Settings = frm.Settings;
        }
        
        private void newScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addItem(Script.ValueType.Script);
        }

        private void newCSSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addItem(Script.ValueType.StyleSheet);
        }
    }
}