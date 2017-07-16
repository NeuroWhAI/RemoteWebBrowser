using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWebBrowser
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        //###########################################################################################################################

        private void Form_Main_Load(object sender, EventArgs e)
        {
            CreateNewTab();
        }

        private TabBrowser CreateNewTab()
        {
            TabPage tab = new TabPage();


            var browser = new TabBrowser();
            browser.Location = new Point(0, 0);
            browser.Dock = DockStyle.Fill;

            browser.WhenDocumentCompleted += (string title, string uri) =>
            {
                tab.Text = (title.Length == 0 ? "null" : title);
                tab.ToolTipText = uri;
            };

            browser.WhenPopup += (string uri) =>
            {
                var popup = CreateNewTab();
                popup.Navigate(uri);

                this.tabControl_web.SelectedIndex = this.tabControl_web.TabCount - 2;
            };


            tab.Controls.Add(browser);
            
            this.tabControl_web.TabPages.Insert(this.tabControl_web.TabCount - 1, tab);

            this.tabControl_web.SelectedTab = tab;


            return browser;
        }

        private void tabControl_web_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl_web.SelectedTab.Tag as string == "+")
            {
                CreateNewTab();
            }
            else if (this.tabControl_web.TabCount > 2)
            {
                this.contextMenuStrip_tab.Show(this.tabControl_web.SelectedTab, 0, 0);
            }
        }

        private void ToolStripMenuItem_close_Click(object sender, EventArgs e)
        {
            this.tabControl_web.TabPages.RemoveAt(this.tabControl_web.SelectedIndex);
        }
    }
}
