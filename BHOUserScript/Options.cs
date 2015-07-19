using System.Windows.Forms;
using System.Diagnostics;

namespace BHOUserScript
{
    /// <summary>
    /// Main options form. Opened on menu button press.
    /// </summary>
    public partial class Options : Form
    {
        public db prefs;
        private string editPath;

        public Options(db _prefs)
        {
            prefs = _prefs;
            InitializeComponent();
            editPath = prefs.Settings.EditorPath;
            enabledChk.Checked = prefs.Settings.Enabled;
        }
        
        private void okBtn_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Options_Load(object sender, System.EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < prefs.AllScripts.Count; i++)
            {
                listBox1.Items.Add(prefs[i].Name + 
                    (prefs[i].Description == "" ? "" : ": " + prefs[i].Description));
            }
        }

        private void editBtn_Click(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                ScriptEditFrm sw = new ScriptEditFrm();
                sw.editedScript = prefs.AllScripts[listBox1.SelectedIndex];
                sw.editPath = editPath;
                sw.LoadFromEditedScript();
                if (sw.ShowDialog() == DialogResult.OK)
                {
                    int index = listBox1.SelectedIndex;
                    prefs.AllScripts[index] = sw.editedScript;
                    RefreshList();
                    listBox1.SetSelected(index, true);
                }
            }
        }

        private void addBtn_Click(object sender, System.EventArgs e)
        {
            ScriptEditFrm sw = new ScriptEditFrm();
            sw.editPath = editPath;
            if (sw.ShowDialog() == DialogResult.OK)
            {
                prefs.AllScripts.Add(sw.editedScript);
                RefreshList();
            }
        }

        private void removeBtn_Click(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                // Add 'are you sure' dialog (also with options to remove file)
                prefs.AllScripts.RemoveAt(listBox1.SelectedIndex);
                eachEnabledChk.Enabled = false;
                RefreshList();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            eachEnabledChk.Enabled = (listBox1.SelectedIndex > -1);

            if (listBox1.SelectedIndex > -1)
            {
                eachEnabledChk.Checked = prefs.AllScripts[listBox1.SelectedIndex].Enabled;
            }
        }

        private void eachEnabledChk_CheckedChanged(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                prefs.AllScripts[listBox1.SelectedIndex].Enabled = eachEnabledChk.Checked;
            }
        }

        private void enabledChk_CheckedChanged(object sender, System.EventArgs e)
        {
            prefs.Settings.Enabled = enabledChk.Checked;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editBtn.PerformClick();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = Scriptmonkey.installPath;
            p.Start();
        }

        private void btnMoveUp_Click(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedIndex > -1 && listBox1.SelectedIndex > 0)
            {
                int index = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(index);
                Script t = prefs[index];
                prefs.AllScripts.RemoveAt(index);
                prefs.AllScripts.Insert(index - 1, t);
                RefreshList();
                listBox1.SetSelected(index - 1, true);
            }
        }

        private void btnMoveDown_Click(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedIndex > -1 && listBox1.SelectedIndex < listBox1.Items.Count - 1)
            {
                int index = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(index);
                Script t = prefs[index];
                prefs.AllScripts.RemoveAt(index);
                prefs.AllScripts.Insert(index + 1, t);
                RefreshList();
                listBox1.SetSelected(index + 1, true);
            }
        }
    }
}