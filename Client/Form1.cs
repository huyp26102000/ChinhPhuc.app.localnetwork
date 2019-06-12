using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Media;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("3");
            comboBox1.Items.Add("4");
            comboBox1.MaxDropDownItems = 4;
            
            

        }
        static ASCIIEncoding encoding = new ASCIIEncoding();
        Socket client;
        IPEndPoint ipe;
        Thread ketnoi;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ketnoi = new Thread(new ThreadStart(ketnoidenserver));
            ketnoi.IsBackground = true;
            ketnoi.Start();

        }
        public void ketnoidenserver()
        {
            string item = comboBox1.GetItemText("192.168.1."+textBox1.Text);
            ipe = new IPEndPoint(IPAddress.Parse(item), 2001);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(ipe);
            Thread langnghe = new Thread(LangNgheDuLieu);
            langnghe.IsBackground = true;
            langnghe.Start(client);
        }
        public void LangNgheDuLieu(object obj)
        {
            Socket sk = (Socket)obj;
            while (true)
            {
                byte[] buff = new byte[1024];
                int recv = client.Receive(buff);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1024];
            string item = comboBox1.GetItemText(comboBox1.SelectedItem);
            string str = item;
            data = encoding.GetBytes(str);
            client.Send(data, data.Length, SocketFlags.None);
            Thread.Sleep(500);
            data = encoding.GetBytes(str);
            client.Send(data, data.Length, SocketFlags.None);

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        { }

        private void button3_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
        }
    }
}
