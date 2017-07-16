using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWebBrowserClient
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();


            m_client.WhenReceived += (Image image) =>
            {
                this.pictureBox_controller.Invoke(new Action(() => this.pictureBox_controller.Image = image));
            };


#if DEBUG
            this.toolStripTextBox_ip.Text = "127.0.0.1";
            this.toolStripTextBox_port.Text = "42212";
#endif
        }

        //###########################################################################################################################

        private BrowserService m_client = new BrowserService();

        //###########################################################################################################################

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_client.Disconnect();
        }

        private void ToolStripMenuItem_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolStripMenuItem_connect_Click(object sender, EventArgs e)
        {
            m_client.Connect(this.toolStripTextBox_ip.Text, this.toolStripTextBox_port.Text);

            this.ToolStripMenuItem_update.Enabled = m_client.IsConnected;

            if (m_client.IsConnected)
            {
                this.ToolStripMenuItem_connect.Enabled = false;

                this.timer_check.Start();
            }
            else
            {
                MessageBox.Show("연결할 수 없습니다!", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripMenuItem_update_Click(object sender, EventArgs e)
        {
            m_client.UpdateImage();
        }

        private void timer_check_Tick(object sender, EventArgs e)
        {
            if (m_client.IsConnected == false)
            {
                this.timer_check.Stop();

                MessageBox.Show("서버와의 연결이 끊어졌습니다!", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //###########################################################################################################################

        private void pictureBox_controller_MouseDown(object sender, MouseEventArgs e)
        {
            m_client.SendMouseDown((int)e.Button, e.X, e.Y);
        }

        private void pictureBox_controller_MouseUp(object sender, MouseEventArgs e)
        {
            m_client.SendMouseUp((int)e.Button, e.X, e.Y);
        }

        private void Form_Main_KeyDown(object sender, KeyEventArgs e)
        {
            m_client.SendKeyDown(e.KeyValue);
        }

        private void Form_Main_KeyUp(object sender, KeyEventArgs e)
        {
            m_client.SendKeyUp(e.KeyValue);
        }
    }
}
