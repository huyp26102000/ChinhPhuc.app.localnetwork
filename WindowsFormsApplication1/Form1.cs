using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;




namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        static ASCIIEncoding encoding = new ASCIIEncoding();

        Socket server;
        List<Socket> LstClient = new List<Socket>();
        IPEndPoint ipe;
        Thread xulyclient;
        String myIP = "";

        
        public void layip()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress diachi in host.AddressList)
            {
                if(diachi.AddressFamily.ToString() == "InterNetwork")
                {
                    myIP = diachi.ToString();
                }
            }
            ipe = new IPEndPoint(IPAddress.Parse(myIP), 2001);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.IP);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            layip();
            xulyclient = new Thread(new ThreadStart(LangNghe));
            xulyclient.IsBackground = true;
            xulyclient.Start();
        }
        public void LangNghe()
        {
            server.Bind(ipe);
            server.Listen(4);
            while (true)
            {
                Socket sk = server.Accept();
                LstClient.Add(sk);
                textBox1.AppendText("chấp nhận kết nối từ" + sk.RemoteEndPoint.ToString());
                textBox1.ScrollToCaret();
                Thread clientProcess = new Thread(mythreadClient);
                clientProcess.IsBackground = true;
                clientProcess.Start(sk);


                
            }
        }
        public void mythreadClient(object obj)
        {
            Socket clientSK = (Socket)obj;
            while (true)
                {
               
                byte[] data = new byte[1024];
                int recv = clientSK.Receive(data);
                string str = encoding.GetString(data,0,recv);
                textBox1.AppendText(""+recv);
                if(str=="1")
                {
                    label1.BackColor = Color.Red;
                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
