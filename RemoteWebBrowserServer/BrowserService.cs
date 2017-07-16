using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Drawing.Imaging;

namespace RemoteWebBrowserServer
{
    public class BrowserService
    {
        public BrowserService(TcpClient client)
        {
            m_sock = client.Client;
        }

        //###########################################################################################################################

        private Socket m_sock;

        public bool IsConnected
        {
            get
            {
                return (m_sock.Connected && m_browser != null && m_browser.HasExited == false);
            }
        }

        public IntPtr SocketHandle
        { get { return m_sock.Handle; } }

        private Process m_browser = null;
        private Thread m_service = null;

        public IntPtr BrowserHandle
        { get { return m_browser.MainWindowHandle; } }

        //###########################################################################################################################

        public void Start()
        {
#if DEBUG
            var info = new ProcessStartInfo(Application.StartupPath + "/../../../RemoteWebBrowser/bin/Debug/RemoteWebBrowser.exe")
#else
            var info = new ProcessStartInfo(Application.StartupPath + "/RemoteWebBrowser.exe")
#endif
            {
                ErrorDialog = false,
                WindowStyle = ProcessWindowStyle.Normal,
            };

            m_browser = Process.Start(info);


            m_service = new Thread(DoService);
            m_service.Start();
        }

        public void Stop()
        {
            if (m_sock != null && m_sock.Connected)
            {
                var intBytes = BitConverter.GetBytes((int)-1);

                m_sock.Send(intBytes, 0, intBytes.Length, SocketFlags.None);
            }


            if (m_browser != null && m_browser.HasExited == false)
            {
                m_browser.CloseMainWindow();
                m_browser.WaitForExit();
            }

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
                m_browser.WaitForInputIdle();


                Thread.Sleep(1000);


                SendImage();


                while (m_browser.HasExited == false)
                {
                    var packet = new Packet();
                    packet.Read(m_sock);


                    if (packet.Command == "exit")
                    {
                        m_browser.CloseMainWindow();
                    }
                    else if (packet.Command == "update")
                    {
                        SendImage();
                    }
                    else if (packet.Command == "mdown")
                    {
                        WinApi.SendMouseDown(this.BrowserHandle, (MouseButtons)int.Parse(packet.Messages[1]),
                            new Point(int.Parse(packet.Messages[2]), int.Parse(packet.Messages[3])));
                    }
                    else if (packet.Command == "mup")
                    {
                        WinApi.SendMouseUp(this.BrowserHandle, (MouseButtons)int.Parse(packet.Messages[1]),
                            new Point(int.Parse(packet.Messages[2]), int.Parse(packet.Messages[3])));
                    }
                    else if (packet.Command == "kdown")
                    {
                        /*WinApi.PostMessage(this.BrowserHandle, WinApi.WM_KEYDOWN,
                            new IntPtr(int.Parse(packet.Messages[1])), IntPtr.Zero);

                        WinApi.SendMessage(this.BrowserHandle, WinApi.WM_CHAR,
                            new IntPtr(int.Parse(packet.Messages[1])), IntPtr.Zero);*/
                        WinApi.keybd_event(int.Parse(packet.Messages[1]), 0, 0, 0);
                    }
                    else if (packet.Command == "kup")
                    {
                        /*WinApi.PostMessage(this.BrowserHandle, WinApi.WM_KEYUP,
                            new IntPtr(int.Parse(packet.Messages[1])), new IntPtr(1));*/
                        WinApi.keybd_event(int.Parse(packet.Messages[1]), 0, 2, 0);
                    }
                }


                m_browser.Dispose();
                m_browser = null;

                
                m_sock.Close();
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

        private void SendImage()
        {
            var image = WinApi.CaptureWindow(this.BrowserHandle);

            byte[] imgBytes = null;

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);

                imgBytes = ms.GetBuffer();
            }


            var intBytes = BitConverter.GetBytes(imgBytes.Length);

            m_sock.Send(intBytes, 0, intBytes.Length, SocketFlags.None);


            const int BUFF_SIZE = 512;
            int end = imgBytes.Length;
            int start = 0;

            while (start < end)
            {
                int n = (end - start >= BUFF_SIZE) ? BUFF_SIZE : end - start;

                m_sock.Send(imgBytes, start, n, SocketFlags.None);

                start += n;
            }
        }
    }
}
