using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace RemoteWebBrowserServer
{
    public class Packet
    {
        public Packet()
        {

        }

        //###########################################################################################################################

        public string[] Messages
        { get; set; }

        public string Command
        { get { return this.Messages[0]; } }

        //###########################################################################################################################

        public void Read(Socket sock)
        {
            var intBytes = new byte[4];

            int recvResult = 0;

            try
            {
                recvResult = sock.Receive(intBytes, 0, intBytes.Length, SocketFlags.None);
            }
            catch (SocketException)
            {
                recvResult = -1;
            }
#if !DEBUG
            catch
            {
                recvResult = -1;
            }
#endif

            if (recvResult <= 0)
            {
                Messages = new[] { "exit" };
                return;
            }

            int bodySize = BitConverter.ToInt32(intBytes, 0);


            var bodyBytes = new byte[bodySize];

            while (sock.Available < bodySize)
            {
                Thread.Sleep(1);
            }

            sock.Receive(bodyBytes, 0, bodyBytes.Length, SocketFlags.None);

            this.Messages = Encoding.UTF8.GetString(bodyBytes).Split('|');
        }
    }
}
