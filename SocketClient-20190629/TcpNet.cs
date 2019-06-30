using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient_20190629
{
    class TcpNet
    {
        private List<Socket> _sockets;

        public List<Socket> Sockets
        {
            get { return _sockets; }
        }
        private int socketBufferSize=1024;
        public Socket ClientConnect(IPEndPoint ipe)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ipe);
                _sockets.Add(socket);
                return socket;

            }catch(Exception ex)
            {
                return null;
            }
            
        }
        public byte[] ClientReceive(Socket socket,int length)
        {
            
            if (length <= 0) return null;
            byte[] bytes = new byte[length];
            int receiveCount=0;
            while (receiveCount < length)
            {
                try
                {
                    int receiveLength = (length - receiveCount) > socketBufferSize ? socketBufferSize : (length - receiveCount);
                    receiveCount += socket.Receive(bytes,receiveCount,receiveLength,0);
                }
                catch
                {
                    _sockets.Remove(socket);
                    socket?.Close();
                    return null;
                }
            }
            return bytes;
            
        }
        public bool ClientSend(Socket socket,byte[] bytes)
        {
            if (bytes == null) return false;
            try
            {
                socket.Send(bytes);
                return true;
            }
            catch
            {
                _sockets.Remove(socket);
                socket?.Close();
                return false;
            }
        }

    }
}
