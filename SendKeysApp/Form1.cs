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
using System.Configuration;

namespace SendKeysApp
{
    public partial class Form1 : Form
    {
        private string[] Rooms;
        private string[] IPs;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SendMessage(IPs[0]);

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SendMessage(IPs[1]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Rooms = ConfigurationManager.AppSettings["RoomList"].Split(',');
            this.IPs = ConfigurationManager.AppSettings["IpList"].Split(',');
            button1.Text = Rooms[0];
            button2.Text = Rooms[1];
        }

        private void SendMessage(string ip)
        {
            Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
ProtocolType.Udp);
            IPAddress send_to_address = IPAddress.Parse(ip);
            IPEndPoint sending_end_point = new IPEndPoint(send_to_address, 11000);
            string text_to_send = "yes";
            byte[] send_buffer = Encoding.ASCII.GetBytes(text_to_send);

            try
            {
                sending_socket.SendTo(send_buffer, sending_end_point);
            }
            catch (Exception send_exception)
            {
                Console.WriteLine(" Exception {0}", send_exception.Message);
            }
        }
    }
}
