using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            useLinkChk.Checked = Settings.UseScriptmonkeyLink;
            reloadNum.Value = Settings.ReloadAfterPages;

            publicApiChk.Enabled = injectApiChk.Checked;
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
            Settings.UseScriptmonkeyLink = useLinkChk.Checked;
            Settings.ReloadAfterPages = (int)reloadNum.Value;
            DialogResult = DialogResult.OK;
        }

        private void injectApiChk_CheckedChanged(object sender, EventArgs e)
        {
            publicApiChk.Enabled = injectApiChk.Checked;
        }

        private void lockSettingsBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?\r\n\r\nThis option is intended for system administrators. It" + 
                " cannot easily be undone.\r\n\r\nEnable this to prevent end users from opening the options window " +
                @"and automatically adding new scripts", @"Lock Scriptmonkey settings", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            Settings.AllowUserEdit = false;
            lockSettingsBtn.Enabled = false;
        }
    }
}
