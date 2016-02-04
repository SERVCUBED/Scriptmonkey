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
            reloadNum.Value = Settings.ReloadAfterPages;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Settings.CheckForUpdates = updateChk.Checked;
            Settings.RunOnPageRefresh = refreshChk.Checked;
            Settings.AutoDownloadScripts = autoChk.Checked;
            Settings.UsePublicAPI = publicApiChk.Checked;
            Settings.CacheScripts = cacheChk.Checked;
            Settings.ReloadAfterPages = (int)reloadNum.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
