using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SocketClient_20190629
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Dictionary<IPEndPoint, Socket> Sockets = null;
        TcpNet tcpNet = new TcpNet();
        private void Button1_Click(object sender, EventArgs e)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            Socket socket = null;
            socket = tcpNet.ClientConnect(ipe);
            Sockets.Add(ipe, socket);
            string text = textBox1.Text + textBox2.Text;
            textBox3.AppendText("已连接至：" + text);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            IPEndPoint ipe=new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            bool getSocket=Sockets.TryGetValue(ipe,out Socket socket);
            if (!getSocket)
            {
                textBox3.AppendText("连接不存在.");
                return;
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            string text = textBox1.Text + textBox2.Text;
            textBox3.AppendText("已关闭连接：" + text);
            return;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(textBox4.Text);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            bool getSocket = Sockets.TryGetValue(ipe, out Socket socket);
            if (!getSocket)
            {
                textBox3.AppendText("连接不存在.");
                return;
            }
            tcpNet.ClientSend(socket, bytes);

            string str = ipe.ToString();
            str += ":" + textBox4.Text;
            textBox3.AppendText(str);
            return;
        }
    }
}
