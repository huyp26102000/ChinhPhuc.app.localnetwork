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
using System.Media;

namespace Server_main
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
        int val = 0;
        string str;
        public void layip()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress diachi in host.AddressList)
            {
                if (diachi.AddressFamily.ToString() == "InterNetwork")
                {
                    myIP = diachi.ToString();
                }
            }
            ipe = new IPEndPoint(IPAddress.Parse(myIP), 2001);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
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
                if (val == 0)
                {
                    byte[] data = new byte[1024];
                    int recv = clientSK.Receive(data);
                    str = encoding.GetString(data, 0, recv);
                    SoundPlayer sound = new SoundPlayer("sound.wav");

                    switch (str)
                    {
                        case "1":
                            label1.BackColor = Color.Red; val = 1; sound.Play();
                            break;
                        case "2":
                            label2.BackColor = Color.Red; val = 1; sound.Play();
                            break;
                        case "3":
                            label3.BackColor = Color.Red; val = 1; sound.Play();
                            break;
                        case "4":
                            label4.BackColor = Color.Red; val = 1; sound.Play();
                            break;
                    }
                }
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            str = "0";
            val = 0;
            label1.BackColor = Color.Aqua;
            label2.BackColor = Color.Aqua;
            label3.BackColor = Color.Aqua;
            label4.BackColor = Color.Aqua;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox6.AppendText(textBox2.Text);
            textBox7.Clear();
            textBox7.AppendText(textBox3.Text);
            textBox8.Clear();
            textBox8.AppendText(textBox4.Text);
            textBox9.Clear();
            textBox9.AppendText(textBox5.Text);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
        }
    }
}
