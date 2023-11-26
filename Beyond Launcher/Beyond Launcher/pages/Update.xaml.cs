using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Beyond_Launcher.pages
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Update()
        {
            InitializeComponent();
            
           
        }

        private void Window_Activated(object sender, EventArgs e)
        {
   


          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\Beyond.Updater.exe"))
            {
                Goofygamer.Content = "Updating Beyond Launcher..";
              
                new Thread(() =>
                {
                    Process updater = new Process();
                    updater.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\Beyond.Updater.exe";

                    updater.Start();

                }).Start();






            }
            else
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile("https://cdn.discordapp.com/attachments/1110281521312567317/1126534826619572364/Beyond.Updater.exe", "Beyond.Updater.exe");
                    File.SetAttributes("Beyond.Updater.exe", File.GetAttributes("Beyond.Updater.exe") | FileAttributes.Hidden);
                    Process updater = new Process();
                    updater.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\Beyond.Updater.exe";

                    Thread.Sleep(3000);
                    updater.Start();

                    Environment.Exit(0);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
