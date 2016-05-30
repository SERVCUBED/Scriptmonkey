using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scriptmonkey_Link
{
    public partial class CookieManager : Form
    {
        private string _path;

        public CookieManager()
        {
            _path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);

            InitializeComponent();

            GetFiles();
        }

        private void GetFiles()
        {
            var files = Directory.GetFiles(_path);
            foreach (var file in files)
            {
                try
                {
                    var contents = File.ReadAllLines(file);
                    if (contents.Length < 9 || String.IsNullOrWhiteSpace(contents[2]))
                        continue;

                    var relative = file.GetFilename();
                    var i = new ListViewItem(contents[2].Substring(0, contents[2].Length - 1));
                    i.SubItems.Add(((contents.Length - 1) / 9 + 1).ToString());
                    i.SubItems.Add(relative);
                    listView1.Items.Add(i);
                }
                catch (Exception) { }
            }
            listView1.Sort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            foreach (ListViewItem selectedItem in listView1.SelectedItems)
            {
                var filename = _path + Path.DirectorySeparatorChar + selectedItem.SubItems[2].Text;
                if (File.Exists(filename))
                    File.Delete(filename);
                listView1.Items.Remove(selectedItem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you sure?\r\nPress Yes to delete all cookies.",
                    @"Scriptmonkey Link: Delete all cookies?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) !=
                    DialogResult.Yes)
                return;

            if (listView1.Items.Count == 0)
                return;

            try
            {
                foreach (var file in Directory.GetFiles(_path))
                {
                    File.Delete(file);
                }
                listView1.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error clearing all cookies: " + Environment.NewLine + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            GetFiles();
        }
    }

    public static class StringTools
    {
        public static string GetFilename(this string s)
        {
            var i = s.LastIndexOf(Path.DirectorySeparatorChar) + 1;
            return s.Substring(i, s.Length - i);
        }
    }
}
