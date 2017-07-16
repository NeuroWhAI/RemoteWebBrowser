using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWebBrowser
{
    public partial class TabBrowser : UserControl
    {
        public TabBrowser()
        {
            InitializeComponent();

            this.tableLayoutPanel1.SetColumnSpan(this.webBrowser1, 2);

            this.webBrowser1.Navigate("about:blank"); // NOTE: 없으면 ActiveXInstance가 null로 나옴.
            (this.webBrowser1.ActiveXInstance as SHDocVw.WebBrowser_V1).NewWindow += webBrowser1_NewWindow;
        }

        //###########################################################################################################################

        public delegate void NewWindowHandler(string uri);
        public event NewWindowHandler WhenPopup;

        public delegate void DocumentCompletedHandler(string title, string uri);
        public event DocumentCompletedHandler WhenDocumentCompleted;

        //###########################################################################################################################

        public void Navigate(string uri)
        {
            this.webBrowser1.Navigate(uri);
        }

        //###########################################################################################################################

        private void textBox_uri1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.webBrowser1.Navigate(this.textBox_uri1.Text);
            }
        }

        private void button_go1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(this.textBox_uri1.Text);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.textBox_uri1.Text = this.webBrowser1.Url.AbsoluteUri;

            if (WhenDocumentCompleted.GetInvocationList().Length > 0)
            {
                WhenDocumentCompleted(this.webBrowser1.DocumentTitle, this.webBrowser1.Url.AbsoluteUri);
            }
        }

        private void webBrowser1_NewWindow(string url, int fags, string targetFrameName, ref object postData, string headers, ref bool processed)
        {
            // Cancel popup.
            processed = true;

            if (WhenPopup.GetInvocationList().Length > 0)
            {
                WhenPopup(url);
            }
        }
    }
}
