using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace RemoteWebBrowserClient
{
    public class BrowserService
    {
        public BrowserService()
        {

        }

        //###########################################################################################################################

        private TcpClient m_client = null;
        private Socket m_sock = null;

        public bool IsConnected
        {
            get
            {
                return (m_onService && m_client != null && this.ClientSocket.Connected);
            }
        }

        public Socket ClientSocket
        { get { return m_client.Client; } }

        private bool m_onService = true;
        private Thread m_service = null;

        public delegate void UpdateHandler(Image image);
        public event UpdateHandler WhenReceived;

        private readonly object m_lockImageUpdate = new object();

        //###########################################################################################################################

        public void Connect(string address, string port)
        {
            // 연결
            m_client = new TcpClient();

            try
            {
                m_client.Connect(address, int.Parse(port));
                m_sock = m_client.Client;
            }
            catch (SocketException)
            {
                m_client = null;
                m_sock = null;
                return;
            }


            m_onService = true;

            m_service = new Thread(DoService);
            m_service.Start();
        }

        public void Disconnect()
        {
            SendMessage("exit");


            m_onService = false;


            // 접속 해제
            if (m_client != null)
            {
                m_client.Close();
                m_client = null;
            }

            m_sock = null;


            if (m_service != null)
            {
                m_service.Join();
                m_service = null;
            }
        }

        //###########################################################################################################################

        private void DoService()
        {
            try
            {
                while (m_onService)
                {
                    var intBytes = new byte[4];

                    int nb = m_sock.Receive(intBytes, 0, intBytes.Length, SocketFlags.None);

                    if (nb <= 0)
                    {
                        m_onService = false;
                        return;
                    }

                    int bodySize = BitConverter.ToInt32(intBytes, 0);

                    if (bodySize < 0)
                    {
                        m_onService = false;
                        return;
                    }


                    const int BUFF_SIZE = 512;
                    var buff = new byte[BUFF_SIZE];
                    int received = 0;

                    List<byte> totalBytes = new List<byte>();

                    while (received < bodySize)
                    {
                        int blockSize = (bodySize - received >= BUFF_SIZE) ? BUFF_SIZE : bodySize - received;

                        while (m_sock.Available < blockSize)
                        {
                            Thread.Sleep(50);
                        }

                        nb = m_sock.Receive(buff, 0, blockSize, SocketFlags.None);

                        if (nb <= 0)
                        {
                            m_onService = false;
                            return;
                        }

                        received += nb;

                        totalBytes.AddRange(buff);
                    }


                    UpdateImage();


                    var ms = new MemoryStream(totalBytes.ToArray());
                    var image = Image.FromStream(ms);


                    WhenReceived(image);
                }
            }
#if DEBUG
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + "\n\n" + exp.StackTrace);
            }
#else
            catch
            {
                // Ignore.
            }
#endif
        }

        //###########################################################################################################################

        private void SendMessage(string cmd, string arg = null)
        {
            if (m_sock != null)
            {
                string body = cmd;

                if (string.IsNullOrEmpty(arg) == false)
                {
                    body += '|' + arg;
                }


                var bodyBytes = Encoding.UTF8.GetBytes(body);

                var intBytes = BitConverter.GetBytes(bodyBytes.Length);
                m_sock.Send(intBytes, 0, intBytes.Length, SocketFlags.None);

                m_sock.Send(bodyBytes, 0, bodyBytes.Length, SocketFlags.None);
            }
        }

        public void UpdateImage()
        {
            lock (m_lockImageUpdate)
            {
                SendMessage("update");
            }
        }

        public void SendMouseDown(int key, int x, int y)
        {
            SendMessage("mdown", key + "|" + x + "|" + y);
        }

        public void SendMouseUp(int key, int x, int y)
        {
            SendMessage("mup", key + "|" + x + "|" + y);
        }

        public void SendKeyDown(int key)
        {
            SendMessage("kdown", key.ToString());
        }

        public void SendKeyUp(int key)
        {
            SendMessage("kup", key.ToString());
        }
    }
}
