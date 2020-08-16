using System;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ReceiveKeysAppConsole
{
    class Program
    {
        private const int listenPort = 11000;

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

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
                    // this is the line of code that receives the broadcase message.
                    // It calls the receive function from the object listener (class UdpClient)
                    // It passes to listener the end point groupEP.
                    // It puts the data from the broadcast message into the byte array
                    // named received_byte_array.
                    // I don't know why this uses the class UdpClient and IPEndPoint like this.
                    // Contrast this with the talker code. It does not pass by reference.
                    // Note that this is a synchronous or blocking call.
                    receive_byte_array = listener.Receive(ref groupEP);
                    Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                    Console.WriteLine("data follows \n{0}\n\n", received_data);




                    IntPtr calculatorHandle = FindWindow("ZPContentViewWndClass", "Zoom Meeting");

                    // Verify that Calculator is a running process.
                    if (calculatorHandle == IntPtr.Zero)
                    {
                        //MessageBox.Show("Calculator is not running.");
                        return 0;
                    }

                    // Make Calculator the foreground application and send it
                    // a set of calculations.
                    SetForegroundWindow(calculatorHandle);
                    //SendKeys.SendWait("{tab}");
                    //Needed to Add Reference to Forms.DLL??? for this to work.
                    SendKeys.Send("%n");
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
