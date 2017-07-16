using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace RemoteWebBrowserServer
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        //###########################################################################################################################

        private bool m_acceptRun = true;
        private Thread m_listenerService = null;
        private TcpListener m_listener = null;

        private List<BrowserService> m_clients = new List<BrowserService>();

        //###########################################################################################################################

        private void Log(string message)
        {
            this.listBox_log.Invoke(new Action(() =>
            {
                this.listBox_log.Items.Add(message);
                this.listBox_log.SelectedIndex = this.listBox_log.Items.Count - 1;
            }));
        }

        //###########################################################################################################################

        private void Form_Main_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (this.button_start.Text == "Stop")
            {
                Stop();
            }
            else
            {
                Start();
            }
        }

        //###########################################################################################################################

        private void Start()
        {
            Log("Start Server.");


            this.textBox_port.Enabled = false;
            this.button_start.Text = "Stop";


            m_acceptRun = true;


            m_listener = new TcpListener(IPAddress.Any, int.Parse(this.textBox_port.Text));
            m_listener.Start();


            m_listenerService = new Thread(Listen);
            m_listenerService.Start();
        }

        private void Stop()
        {
            m_acceptRun = false;


            if (m_listenerService != null)
            {
                m_listenerService.Join();
                m_listenerService = null;
            }


            if (m_listener != null)
            {
                m_listener.Stop();
                m_listener = null;
            }


            foreach (var browser in m_clients)
            {
                browser.Stop();
            }
            m_clients.Clear();


            this.textBox_port.Enabled = true;
            this.button_start.Text = "Start";


            Log("Stop Server.");
        }

        private void Listen()
        {
            Log("Start listening.");


            while (m_acceptRun)
            {
                // 보류중인 접속이 없으면 대기
                while (!m_listener.Pending())
                {
                    if (m_acceptRun == false)
                        return;


                    // 연결 확인
                    for (int i = 0; i < m_clients.Count; ++i)
                    {
                        var client = m_clients[i];

                        if (client.IsConnected == false)
                        {
                            Log("Disconnect " + client.SocketHandle + " socket.");

                            client.Stop();

                            m_clients.RemoveAt(i);
                            --i;
                        }
                    }


                    Thread.Sleep(128);
                }


                // 접속확인
                var newClient = m_listener.AcceptTcpClient();

                // 클라이언트 생성
                if (newClient != null)
                {
                    Log("Connect " + newClient.Client.Handle + " socket.");


                    var browser = new BrowserService(newClient);
                    browser.Start();

                    m_clients.Add(browser);
                }
            }
        }
    }
}
