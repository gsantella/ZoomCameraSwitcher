using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ReceiveKeysAppConsole3
{
    class Program
    {
        private const int listenPort = 11000;

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Makes window active
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static int Main()
        {
            bool done = false;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            string received_data;
            byte[] receive_byte_array;
            
            try
            {
                while (!done)
                {
                    Console.WriteLine("Waiting for broadcast");

                    // Note that this is a synchronous or blocking call.
                    receive_byte_array = listener.Receive(ref groupEP);
                    Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                    Console.WriteLine("data follows \n{0}\n\n", received_data);

                    IntPtr zoomHandle = FindWindow("ZPContentViewWndClass", "Zoom Meeting");

                    // Verify that Zoom is a running process.
                    if (zoomHandle != IntPtr.Zero)
                    {
                        // Make Calculator the foreground application and send it a keypress
                        SetForegroundWindow(zoomHandle);
                        try
                        {
                            // Sends an Alt+N
                            SendKeys.SendWait("%n");
                        }
                        catch
                        {

                        }
                    }          
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            listener.Close();
            return 0;
        }
    }
}
