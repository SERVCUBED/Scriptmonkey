using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BHOUserScript
{
    public partial class AdvancedOptionsFrm : Form
    {
        public SettingsFile Settings;
        public AdvancedOptionsFrm(SettingsFile settings)
        {
            Settings = settings;
            InitializeComponent();
        }

        private void AdvancedOptionsFrm_Load(object sender, EventArgs e)
        {
            updateChk.Checked = Settings.CheckForUpdates;
            refreshChk.Checked = Settings.RunOnPageRefresh;
            autoChk.Checked = Settings.AutoDownloadScripts;
            publicApiChk.Checked = Settings.UsePublicAPI;
            cacheChk.Checked = Settings.CacheScripts;
            refreshPageChk.Checked = Settings.RefreshOnSave;
            injectApiChk.Checked = Settings.InjectAPI;
            notifyApiChk.Checked = Settings.InjectNotificationAPI;
            useLinkChk.Checked = Settings.UseScriptmonkeyLink;
            updateDisabledChk.Checked = Settings.UpdateDisabledScripts;
            reloadNum.Value = Settings.ReloadAfterPages;

            if (Settings.BlacklistedUrls.Length > 0)
                listBox1.Items.AddRange(Settings.BlacklistedUrls);

            publicApiChk.Enabled = notifyApiChk.Enabled = injectApiChk.Checked;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Settings.CheckForUpdates = updateChk.Checked;
            Settings.RunOnPageRefresh = refreshChk.Checked;
            Settings.AutoDownloadScripts = autoChk.Checked;
            Settings.UsePublicAPI = publicApiChk.Checked;
            Settings.CacheScripts = cacheChk.Checked;
            Settings.RefreshOnSave = refreshPageChk.Checked;
            Settings.InjectAPI = injectApiChk.Checked;
            Settings.InjectNotificationAPI = notifyApiChk.Checked;
            Settings.UseScriptmonkeyLink = useLinkChk.Checked;
            Settings.UpdateDisabledScripts = updateDisabledChk.Checked;
            Settings.ReloadAfterPages = (int)reloadNum.Value;

            Settings.BlacklistedUrls = new string[listBox1.Items.Count];
            for (int i = 0; i < listBox1.Items.Count; i++)
                Settings.BlacklistedUrls[i] = listBox1.Items[i].ToString();

            DialogResult = DialogResult.OK;
        }

        private void injectApiChk_CheckedChanged(object sender, EventArgs e)
        {
            publicApiChk.Enabled = notifyApiChk.Enabled = injectApiChk.Checked;
        }

        private void lockSettingsBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?\r\n\r\nThis option is intended for system administrators. It" + 
                " cannot easily be undone.\r\n\r\nEnable this to prevent end users from opening the options window " +
                @"and automatically adding new scripts", @"Lock Scriptmonkey settings", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            Settings.AllowUserEdit = false;
            lockSettingsBtn.Enabled = false;
        }

        private void menuCssBtn_Click(object sender, EventArgs e)
        {
            string s = Interaction.InputBox("Enter valid CSS to use in the menu command element when script menu commands are present:",
                "Scriptmonkey - Edit Menu Command CSS", Settings.MenuCommandCSS);

            if (s == "default")
                Settings.MenuCommandCSS = new SettingsFile().MenuCommandCSS;
            else if (!String.IsNullOrWhiteSpace(s))
                Settings.MenuCommandCSS = s;
        }

        private void addMatchBtn_Click(object sender, EventArgs e)
        {
            string t = Interaction.InputBox("Add URL Match");
            if (t != String.Empty)
                listBox1.Items.Add(t);
        }

        private void remMatchBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void editIncludeBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            string s = Interaction.InputBox("Edit value:", Properties.Resources.Title, listBox1.SelectedItem.ToString());
            if (!String.IsNullOrEmpty(s))
                listBox1.Items[listBox1.SelectedIndex] = s;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e) => editIncludeBtn_Click(sender, e);
    }
}
