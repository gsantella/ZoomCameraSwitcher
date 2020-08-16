using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace ReceiveKeysApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private void Button1_Click(object sender, EventArgs e)
        {
            IntPtr calculatorHandle = FindWindow("ZPContentViewWndClass", "Zoom Meeting");

            // Verify that Calculator is a running process.
            if (calculatorHandle == IntPtr.Zero)
            {
                MessageBox.Show("Calculator is not running.");
                return;
            }

            // Make Calculator the foreground application and send it
            // a set of calculations.
            //SetForegroundWindow(calculatorHandle);
            //SendKeys.SendWait("{tab}");
            try
            {
                //SendKeys.Send("%n");
            }
            catch
            {

            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            private const int listenPort = 11000;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            string received_data;
            byte[] receive_byte_array;
        }
    }
   }
}
